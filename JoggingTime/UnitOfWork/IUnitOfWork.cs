using JoggingTime.Models;

namespace JoggingTime.UnitOfWork
{
    public interface IUnitOfWork
    {
        ApplicationDBContext context { get; }

        bool Save(bool isPartial = false);
        bool SavePartial();
    }
}