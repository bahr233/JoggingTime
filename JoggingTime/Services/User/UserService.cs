using AutoMapper;
using AutoMapper.QueryableExtensions;
using JoggingTime.Repositories;
using JoggingTime.ViewModels.User;

namespace JoggingTime.Services.User
{
    public class UserService: IUserService
    {
        private readonly IRepository<Models.User> _repository;
        private readonly IMapper _mapper;

        public UserService(IRepository<Models.User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper; 
        }

        public List<UserViewModel> Get()
        {
            return _repository.Get()
                .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider).ToList();
        }
        public Models.User Login(LoginViewModel viewmodel)
        {
            var model =_repository.Get(i=>i.Email == viewmodel.Email && i.Password == viewmodel.Password).FirstOrDefault();
            return model;
        }
        public UserViewModel GetById(int Id)
        {
            return _repository.Get(i=>i.ID==Id).ProjectTo<UserViewModel>(_mapper.ConfigurationProvider).FirstOrDefault();

        }
        public Models.User Create(UserCreateViewModel viewmodel)
        {
            var model = _mapper.Map<Models.User>(viewmodel);
            return _repository.Add(model);
        }
        public void Update(UserUpdateViewModel viewmodel)
        {
            var model = _mapper.Map<Models.User>(viewmodel);
            _repository.Update(model);
        }

        public void Delete(int Id)
        {
            _repository.Delete(Id);
        }

    }
}
