using Autofac;
using JoggingTime.Mediator.User;
using JoggingTime.Repositories;
using JoggingTime.Services.Jogging;
using JoggingTime.Services.Token;
using JoggingTime.Services.User;
using JoggingTime.UnitOfWork;
using System.ComponentModel.Design;

namespace JoggingTime.Configrations
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IUnitOfWork).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<JoggingService>().As<IJoggingService>().InstancePerLifetimeScope();
            builder.RegisterType<UserMediator>().As<IUserMediator>().InstancePerLifetimeScope();
            builder.RegisterType<TokenService>().As<ITokenService>().InstancePerLifetimeScope();

        }
    }
}
