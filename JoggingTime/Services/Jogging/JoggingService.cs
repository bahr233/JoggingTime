﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using JoggingTime.Repositories;
using JoggingTime.ViewModels.Jogging;
using JoggingTime.ViewModels.User;
using LinqKit;

namespace JoggingTime.Services.Jogging
{
    public class JoggingService: IJoggingService
    {
        private readonly IRepository<Models.UserJoggingTime> _repository;
        private readonly IMapper _mapper;

        public JoggingService(IRepository<Models.UserJoggingTime> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<JoggingViewModel> Get(DateTime fromDate, DateTime toDate)
        {
            var predicate = PredicateBuilder.New<Models.UserJoggingTime>();
            predicate = predicate.And(dm => dm.JoggingDate.Date >= fromDate.Date && dm.JoggingDate.Date <= toDate.Date);

            return _repository.Get(predicate)
                .ProjectTo<JoggingViewModel>(_mapper.ConfigurationProvider).ToList();
        }
        public JoggingViewModel GetById(int Id)
        {
            return _repository.Get(i => i.ID == Id).ProjectTo<JoggingViewModel>(_mapper.ConfigurationProvider).FirstOrDefault();

        }
        public Models.UserJoggingTime Create(JoggingCreateViewModel viewmodel)
        {
            var model = _mapper.Map<Models.UserJoggingTime>(viewmodel);
            return _repository.Add(model);
        }
        public void Update(JoggingUpdateViewModel viewmodel)
        {
            var model = _mapper.Map<Models.UserJoggingTime>(viewmodel);
            _repository.Update(model);
        }

        public void Delete(int Id)
        {
            _repository.Delete(Id);
        }
    }
}
