using System;
using System.Reflection;

namespace FA.Tests;

internal class TestVariables
{
    internal const BindingFlags INSTANCE_FLAGS = BindingFlags.Public | BindingFlags.Instance;

    internal const BindingFlags STATIC_FLAGS = BindingFlags.Public | BindingFlags.Static;

    internal const int ITERATIONS_COUNT = 1_000_000;

    internal static readonly RoflType s_Target = new();

    internal static readonly Type s_ClassType = typeof(RoflType);

    internal static readonly FieldInfo s_MemberFieldInfo = s_ClassType.GetField(nameof(RoflType.m_MemberField), INSTANCE_FLAGS);

    internal static readonly FieldInfo s_StaticFieldInfo = s_ClassType.GetField(nameof(RoflType.s_StaticField), STATIC_FLAGS);

    internal static readonly PropertyInfo s_MemberPropertyInfo = s_ClassType.GetProperty(nameof(RoflType.MemberProperty), INSTANCE_FLAGS);

    internal static readonly PropertyInfo s_StaticPropertyInfo = s_ClassType.GetProperty(nameof(RoflType.StaticProperty), STATIC_FLAGS);

    internal static readonly MethodInfo s_MemberMethodInfo = s_ClassType.GetMethod(nameof(RoflType.MemberMethod), INSTANCE_FLAGS);

    internal static readonly MethodInfo s_StaticMethodInfo = s_ClassType.GetMethod(nameof(RoflType.StaticMethod), STATIC_FLAGS);
}

