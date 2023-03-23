using JoggingTime.Enums;
using JoggingTime.Models;

namespace JoggingTime.UnitOfWork
{
    public interface IUnitOfWork
    {
        ApplicationDBContext context { get; }
        UserRole RoleID { get; set; }

        bool Save(bool isPartial = false);
        bool SavePartial();
    }
}