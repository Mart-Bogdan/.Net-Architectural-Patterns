namespace WebApp.Abstract.Security
{
    public interface ICredentialsChecker
    {
        int? CheckUserExist(string userName,string userPassword);
    }
}