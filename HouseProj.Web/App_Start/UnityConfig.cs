using System.Web.Mvc;
using HouseProj.Service;
using Unity;
using Unity.Mvc5;
using HouseProj.Service.Contracts;
using HouseProj.Data;
using HouseProj.Data.Contracts;

namespace HouseProj.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IElasticService, ElasticService>();
            container.RegisterType<IElasticContext, ElasticContext>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}