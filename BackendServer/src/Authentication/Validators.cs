using System.ComponentModel.DataAnnotations;
namespace BackendServer.Authentication;

public class OnlyNumber : RegularExpressionAttribute
{
    public OnlyNumber()
        : base("^[0-9]*$")
    {
    }
}
public class ContainNumber : RegularExpressionAttribute
{
    public ContainNumber()
        : base(@".*\d.*")
    {
    }
}

public class ContainUpper : RegularExpressionAttribute
{
    public ContainUpper()
        : base(".*[A-Z].*")
    {
    }
}
public class Containlower : RegularExpressionAttribute
{
    public Containlower()
        : base(".*[a-z].*")
    {
    }
}
public class ContainSymbol : RegularExpressionAttribute
{
    public ContainSymbol()
        : base(@".*\W.*")
    {
    }
}
public class ContainSpace : RegularExpressionAttribute
{
    public ContainSpace()
        : base(@"^[^\s\,]+$")
    {
    }
}