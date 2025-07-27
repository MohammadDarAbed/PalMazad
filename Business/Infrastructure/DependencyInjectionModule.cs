
using Autofac;
using Business.Categories;
using Business.Managers;
using Business.Products;
using Business.Users;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PalMazadStore.Migrations;
using Shared.Infrastructure;
using Shared.Interfaces;

namespace Business.Infrastructure
{
    public class DependencyInjectionModule : Module
    {
        private readonly IConfiguration _configuration;

        public DependencyInjectionModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            builder.Register(c =>
            {
                var options = new DbContextOptionsBuilder<BaseDbContext>()
                    .UseSqlServer(connectionString)
                    .Options;

                return new AppDbContext(options);
            })
            .As<BaseDbContext>()    // for base usage
            .As<AppDbContext>()     // for specific usage
            .InstancePerLifetimeScope();


            // managers

            builder
                .RegisterType<ProductManager>()
                .As<IProductManager>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<CategoryManager>()
                .As<ICategoryManager>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<UserManager>()
                .As<IUserManager>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<CartManager>()
                .As<ICartManager>()
                .InstancePerLifetimeScope();

            // Repositories:
            builder
                .RegisterType<ProductRepository>()
                .As<IProductRepository>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<CategoryRepository>()
                .As<ICategoryRepository>()
                .InstancePerLifetimeScope(); 
            builder
                .RegisterType<UserRepository>()
                .As<IUserRepository>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<CartRepository>()
                .As<ICartRepository>()
                .InstancePerLifetimeScope();

            builder
                .RegisterGeneric(typeof(BaseRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();
        }

    }
}
