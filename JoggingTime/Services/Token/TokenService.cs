using AutoMapper;
using JoggingTime.Models;
using JoggingTime.Repositories;

namespace JoggingTime.Services.Token
{
    public class TokenService: ITokenService
    {
        private readonly IRepository<Models.Token> _repository;

        public TokenService(IRepository<Models.Token> repository)
        {
            _repository = repository;
        }

        public Models.Token Get(int userId)
        {
            return _repository.Get(i => i.UserID == userId && i.IsDeleted==false
            && i.ExpirationDate!=DateTime.Now).FirstOrDefault();
        }
        public string Add(Models.Token token)
        {
            var model = _repository.Add(token);
            return model.Code;
        }
        public void Remove(int userId)
        {
            var models = _repository.Get(i=>i.UserID == userId && i.IsDeleted==false).ToList();
            models.ForEach(model => { 
                _repository.Delete(model); 
            });
        }

    }
}

