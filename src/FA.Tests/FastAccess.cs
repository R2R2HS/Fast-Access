using static FA.Tests.TestVariables;

namespace FA.Tests;

[TestClass]
public sealed class FastAccess : TestBase
{
    [TestMethod]
    public override void AccessProperty() => FaAccess.Accessor_get(s_MemberPropertyInfo)!.Invoke(s_Target);

    [TestMethod]
    public override void AccessField() => FaAccess.Accessor_get(s_MemberFieldInfo)!.Invoke(s_Target);

    [TestMethod]
    public override void AccessMethod() => FaAccess.Accessor_invoke(s_MemberMethodInfo).Invoke(s_Target, null);

    [TestMethod]
    public override void AccessStaticProperty() => FaAccess.Accessor_get(s_StaticPropertyInfo)!.Invoke(null);

    [TestMethod]
    public override void AccessStaticField() => FaAccess.Accessor_get(s_StaticFieldInfo)!.Invoke(null);

    [TestMethod]
    public override void AccessStaticMethod() => FaAccess.Accessor_invoke(s_StaticMethodInfo).Invoke(null, null);
}

