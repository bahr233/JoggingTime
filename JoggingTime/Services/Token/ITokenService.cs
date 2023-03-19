namespace JoggingTime.Services.Token
{
    public interface ITokenService
    {
        string Add(Models.Token token);
        Models.Token Get(int userId);
        void Remove(int userId);
    }
}
