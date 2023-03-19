using Autofac;
using JoggingTime.Repositories;
using JoggingTime.UnitOfWork;

namespace JoggingTime.Configrations
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IUnitOfWork).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
        }
    }
}
