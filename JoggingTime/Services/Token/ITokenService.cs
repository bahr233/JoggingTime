namespace JoggingTime.Services.Token
{
    public interface ITokenService
    {
        string Add(Models.Token token);
        Models.Token Get(int userId);
        bool IsValidToken(string token);
        void Remove(int userId);
    }
}
