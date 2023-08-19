namespace Wardrobe.CrossCutting.Exceptions;

public class UnauthorizedUser : UnauthorizedAccessException
{
    public UnauthorizedUser(string email) : base($"User {email} is not authorized")
    {}
}