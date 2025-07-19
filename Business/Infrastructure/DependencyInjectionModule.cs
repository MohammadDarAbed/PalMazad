
using Autofac;
using Business.Categorys;
using Business.Products;
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
                .RegisterGeneric(typeof(BaseRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();
        }

    }
}
