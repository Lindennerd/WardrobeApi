namespace Wardrobe.CrossCutting.Exceptions;

public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException(string email) : base($"This user {email} already exists")
    {
        
    }
}