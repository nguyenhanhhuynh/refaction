using Microsoft.Practices.Unity;
using refactor_me.Interface;
using refactor_me.Repository;
using System.Web.Http;
using Unity.WebApi;

namespace refactor_me
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IProductOptionRepository, ProductOptionRepository>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}