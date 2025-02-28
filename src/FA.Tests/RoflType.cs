namespace FA.Tests;

public class RoflType
{
    public static int s_StaticField = 5050;

    public static int StaticProperty => s_StaticField;

    public int m_MemberField = 5050;

    public int MemberProperty => m_MemberField;

    public void MemberMethod() => m_MemberField++;

    public static void StaticMethod() => s_StaticField++;
}

