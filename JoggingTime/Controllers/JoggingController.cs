using JoggingTime.Enums;
using JoggingTime.Filters;
using JoggingTime.Services.Jogging;
using JoggingTime.UnitOfWork;
using JoggingTime.ViewModels;
using JoggingTime.ViewModels.Jogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace JoggingTime.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class JoggingController : ControllerBase
    {
        private readonly IJoggingService _joggingService;
        private readonly IUnitOfWork _unitOfWork;
        public JoggingController(IJoggingService joggingService, IUnitOfWork unitOfWork)
        {
            _joggingService = joggingService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        //[TypeFilter(typeof(AuthorizeRoleFilter), Arguments = new object[] {UserRole.Manager })]

        public ResponseViewModel<List<JoggingViewModel>> Get(DateTime fromDate, DateTime toDate)
        {
            return new ResponseViewModel<List<JoggingViewModel>>(_joggingService.Get( fromDate,  toDate));
        }
        [HttpGet]
        public ResponseViewModel<JoggingViewModel> GetById(int id)
        {
            return new ResponseViewModel<JoggingViewModel>(_joggingService.GetById(id));
        }
        [HttpPost]
        public ResponseViewModel<int> Create(JoggingCreateViewModel viewmodel)
        {
            var model = _joggingService.Create(viewmodel);
            _unitOfWork.Save();
            return new ResponseViewModel<int>(model.ID, "Jogging Added Successfuly");
        }

        [HttpPut]
        public ResponseViewModel<bool> Update(JoggingUpdateViewModel viewmodel)
        {
            _joggingService.Update(viewmodel);
            _unitOfWork.Save();
            return new ResponseViewModel<bool>(true, "Jogging Updated Successfuly");
        }
        [HttpDelete]
        public ResponseViewModel<bool> Delete(int id)
        {
            _joggingService.Delete(id);
            _unitOfWork.Save();
            return new ResponseViewModel<bool>(true, "Jogging Deleted Successfuly");
        }
    }
}
