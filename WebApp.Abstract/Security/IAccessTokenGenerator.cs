namespace WebApp.Abstract.Security
{
    public interface IAccessTokenGenerator
    {
        string GenerateToken(int userId, string nick);
    }
}