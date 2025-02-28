using static FA.Tests.TestVariables;

namespace FA.Tests;

[TestClass]
public sealed class Reflection : TestBase
{
    [TestMethod]
    public override void AccessProperty() => s_MemberPropertyInfo.GetValue(s_Target);

    [TestMethod]
    public override void AccessField() => s_MemberFieldInfo.GetValue(s_Target);

    [TestMethod]
    public override void AccessMethod() => s_MemberMethodInfo.Invoke(s_Target, null);

    [TestMethod]
    public override void AccessStaticProperty() => s_StaticPropertyInfo.GetValue(null);

    [TestMethod]
    public override void AccessStaticField() => s_StaticFieldInfo.GetValue(null);

    [TestMethod]
    public override void AccessStaticMethod() => s_StaticMethodInfo.Invoke(null, null);
}

