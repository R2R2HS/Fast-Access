using System;
using System.Reflection.Emit;

namespace FA.Emit;

public class FaEmitHelper
{
    public static void EmitUnboxIfNeeded(ILGenerator il, Type type)
    {
        if(type.IsValueType) il.Emit(OpCodes.Unbox_Any, type);
    }

    public static void EmitBoxIfNeeded(ILGenerator il, Type type)
    {
        if(type.IsValueType) il.Emit(OpCodes.Box, type);
    }

    public static void EmitInt(ILGenerator il, int value)
    {
        if(value is >(-129) and <128) il.Emit(OpCodes.Ldc_I4_S, (sbyte)value);
        else il.Emit(OpCodes.Ldc_I4, value);
    }

    public static void EmitFastInt(ILGenerator il, int value)
    {
        switch(value)
        {
            case -1:
            il.Emit(OpCodes.Ldc_I4_M1);
            return;
            case 0:
            il.Emit(OpCodes.Ldc_I4_0);
            return;
            case 1:
            il.Emit(OpCodes.Ldc_I4_1);
            return;
            case 2:
            il.Emit(OpCodes.Ldc_I4_2);
            return;
            case 3:
            il.Emit(OpCodes.Ldc_I4_3);
            return;
            case 4:
            il.Emit(OpCodes.Ldc_I4_4);
            return;
            case 5:
            il.Emit(OpCodes.Ldc_I4_5);
            return;
            case 6:
            il.Emit(OpCodes.Ldc_I4_6);
            return;
            case 7:
            il.Emit(OpCodes.Ldc_I4_7);
            return;
            case 8:
            il.Emit(OpCodes.Ldc_I4_8);
            return;
        }

        EmitInt(il, value);
    }

    public static void EmitCastToReference(ILGenerator il, Type type)
    {
        if(type.IsValueType) il.Emit(OpCodes.Unbox_Any, type);
        else il.Emit(OpCodes.Castclass, type);
    }
}
