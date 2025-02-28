using FA.Emit;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace FA;

public class FaAccess
{
    private static readonly Type[] s_InvokeArguments = [typeof(object), typeof(object[])];
    private static readonly Type[] s_GetArguments = [typeof(object)];
    private static readonly Type[] s_SetArguments = [typeof(object), typeof(object)];

    private static readonly Dictionary<MethodInfo, FaInvokeAccessHandler> s_InvokeAccessors = [];
    private static readonly Dictionary<MemberInfo, FaValueGetAccessHandler> s_ValueGetAccessors = [];
    private static readonly Dictionary<MemberInfo, FaValueSetAccessHandler> s_ValueSetAccessors = [];

    public static FaInvokeAccessHandler Accessor_invoke(MethodInfo info) =>
              s_InvokeAccessors.TryGetValue(info, out FaInvokeAccessHandler accessor)
            ? accessor
            : (s_InvokeAccessors[info] = Handler_invoke(info));

    public static FaValueGetAccessHandler Accessor_get(PropertyInfo info) =>
              s_ValueGetAccessors.TryGetValue(info, out FaValueGetAccessHandler accessor)
            ? accessor
            : (s_ValueGetAccessors[info] = Handler_get(info));

    public static FaValueGetAccessHandler Accessor_get(FieldInfo info) =>
              s_ValueGetAccessors.TryGetValue(info, out FaValueGetAccessHandler accessor)
            ? accessor
            : (s_ValueGetAccessors[info] = Handler_get(info));

    public static FaValueSetAccessHandler Accessor_set(PropertyInfo info) =>
              s_ValueSetAccessors.TryGetValue(info, out FaValueSetAccessHandler accessor)
            ? accessor
            : (s_ValueSetAccessors[info] = Handler_set(info));

    public static FaValueSetAccessHandler Accessor_set(FieldInfo info) =>
              s_ValueSetAccessors.TryGetValue(info, out FaValueSetAccessHandler accessor)
            ? accessor
            : (s_ValueSetAccessors[info] = Handler_set(info));

    private static FaInvokeAccessHandler Handler_invoke(MethodInfo info)
    {
        DynamicMethod dynamicMethod = new("FA_" + info.Name, typeof(object), s_InvokeArguments, info.DeclaringType, true);
        ILGenerator il = dynamicMethod.GetILGenerator();

        ParameterInfo[] ps = info.GetParameters();
        int paramLength = ps.Length;
        Type[] paramTypes = new Type[paramLength];

        for(int i = 0; i < paramLength; i++)
            paramTypes[i] = ps[i].ParameterType.IsByRef ? ps[i].ParameterType.GetElementType()! : ps[i].ParameterType;

        LocalBuilder[] locals = new LocalBuilder[paramLength];

        for(int i = 0; i < paramLength; i++) locals[i] = il.DeclareLocal(paramTypes[i], true);

        for(int i = 0; i < paramLength; i++)
        {
            il.Emit(OpCodes.Ldarg_1);
            FaEmitHelper.EmitFastInt(il, i);
            il.Emit(OpCodes.Ldelem_Ref);
            FaEmitHelper.EmitCastToReference(il, paramTypes[i]);
            il.Emit(OpCodes.Stloc, locals[i]);
        }

        if(!info.IsStatic) il.Emit(OpCodes.Ldarg_0);

        for(int i = 0; i < paramLength; i++)
        {
            if(ps[i].ParameterType.IsByRef) il.Emit(OpCodes.Ldloca_S, locals[i]);
            else il.Emit(OpCodes.Ldloc, locals[i]);
        }

        if(info.IsStatic) il.EmitCall(OpCodes.Call, info, null);
        else il.EmitCall(OpCodes.Callvirt, info, null);

        if(info.ReturnType == typeof(void)) il.Emit(OpCodes.Ldnull);
        else FaEmitHelper.EmitBoxIfNeeded(il, info.ReturnType);

        for(int i = 0; i < paramLength; i++)
        {
            if(ps[i].ParameterType.IsByRef)
            {
                il.Emit(OpCodes.Ldarg_1);
                FaEmitHelper.EmitFastInt(il, i);
                il.Emit(OpCodes.Ldloc, locals[i]);
                if(locals[i].LocalType.IsValueType) il.Emit(OpCodes.Box, locals[i].LocalType);
                il.Emit(OpCodes.Stelem_Ref);
            }
        }

        il.Emit(OpCodes.Ret);

        return (FaInvokeAccessHandler)dynamicMethod.CreateDelegate(typeof(FaInvokeAccessHandler));
    }

    private static FaValueGetAccessHandler Handler_get(PropertyInfo propertyInfo)
    {
        if(!propertyInfo.CanRead) throw new ArgumentException($"Not {nameof(PropertyInfo.CanRead)}", nameof(propertyInfo));
        MethodInfo getMethodInfo = propertyInfo.GetGetMethod(true);
        DynamicMethod dynamicGet = new("FA_get_" + propertyInfo.Name, typeof(object), s_GetArguments, propertyInfo.DeclaringType, true);
        ILGenerator il = dynamicGet.GetILGenerator();

        if(!getMethodInfo.IsStatic) il.Emit(OpCodes.Ldarg_0);
        il.Emit(OpCodes.Call, getMethodInfo);
        FaEmitHelper.EmitBoxIfNeeded(il, getMethodInfo.ReturnType);
        il.Emit(OpCodes.Ret);

        return (FaValueGetAccessHandler)dynamicGet.CreateDelegate(typeof(FaValueGetAccessHandler));
    }

    private static FaValueGetAccessHandler Handler_get(FieldInfo fieldInfo)
    {
        DynamicMethod dynamicGet = new("FA_get_" + fieldInfo.Name, typeof(object), s_GetArguments, fieldInfo.DeclaringType, true);
        ILGenerator il = dynamicGet.GetILGenerator();

        if(fieldInfo.IsStatic) il.Emit(OpCodes.Ldsfld, fieldInfo);
        else
        {
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, fieldInfo);
        }

        FaEmitHelper.EmitBoxIfNeeded(il, fieldInfo.FieldType);
        il.Emit(OpCodes.Ret);

        return (FaValueGetAccessHandler)dynamicGet.CreateDelegate(typeof(FaValueGetAccessHandler));
    }

    private static FaValueSetAccessHandler Handler_set(PropertyInfo propertyInfo)
    {
        if(!propertyInfo.CanWrite) throw new ArgumentException($"Not {nameof(PropertyInfo.CanWrite)}", nameof(propertyInfo));
        MethodInfo setMethodInfo = propertyInfo.GetSetMethod(true);
        DynamicMethod dynamicSet = new("FA_set_" + propertyInfo.Name, typeof(void), s_SetArguments, propertyInfo.DeclaringType, true);
        ILGenerator il = dynamicSet.GetILGenerator();

        if(!setMethodInfo.IsStatic) il.Emit(OpCodes.Ldarg_0);
        il.Emit(OpCodes.Ldarg_1);
        FaEmitHelper.EmitUnboxIfNeeded(il, setMethodInfo.GetParameters()[0].ParameterType);
        il.Emit(OpCodes.Call, setMethodInfo);
        il.Emit(OpCodes.Ret);

        return (FaValueSetAccessHandler)dynamicSet.CreateDelegate(typeof(FaValueSetAccessHandler));
    }

    private static FaValueSetAccessHandler Handler_set(FieldInfo fieldInfo)
    {
        DynamicMethod dynamicSet = new("FA_set_" + fieldInfo.Name, typeof(void), s_SetArguments, fieldInfo.DeclaringType, true);
        ILGenerator il = dynamicSet.GetILGenerator();

        if(!dynamicSet.IsStatic) il.Emit(OpCodes.Ldarg_0);
        il.Emit(OpCodes.Ldarg_1);
        FaEmitHelper.EmitUnboxIfNeeded(il, fieldInfo.FieldType);
        il.Emit(OpCodes.Stfld, fieldInfo);
        il.Emit(OpCodes.Ret);

        return (FaValueSetAccessHandler)dynamicSet.CreateDelegate(typeof(FaValueSetAccessHandler));
    }
}
