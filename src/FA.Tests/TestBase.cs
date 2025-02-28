using static FA.Tests.TestVariables;

namespace FA.Tests;

[TestClass]
public abstract class TestBase
{
    [TestMethod]
    public abstract void AccessProperty();

    [TestMethod]
    public abstract void AccessField();

    [TestMethod]
    public abstract void AccessMethod();

    [TestMethod]
    public abstract void AccessStaticProperty();

    [TestMethod]
    public abstract void AccessStaticField();

    [TestMethod]
    public abstract void AccessStaticMethod();

    [TestMethod]
    public void IterationAccessProperty()
    {
        for(int i = 0; i < ITERATIONS_COUNT; i++) AccessProperty();
    }

    [TestMethod]
    public void IterationAccessField()
    {
        for(int i = 0; i < ITERATIONS_COUNT; i++) AccessField();
    }

    [TestMethod]
    public void IterationAccessMethod()
    {
        for(int i = 0; i < ITERATIONS_COUNT; i++) AccessMethod();
    }

    [TestMethod]
    public void IterationAccessStaticProperty()
    {
        for(int i = 0; i < ITERATIONS_COUNT; i++) AccessStaticProperty();
    }

    [TestMethod]
    public void IterationAccessStaticField()
    {
        for(int i = 0; i < ITERATIONS_COUNT; i++) AccessStaticField();
    }

    [TestMethod]
    public void IterationAccessStaticMethod()
    {
        for(int i = 0; i < ITERATIONS_COUNT; i++) AccessStaticMethod();
    }
}

