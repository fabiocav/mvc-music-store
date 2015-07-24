using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.Azure.Mobile.Server.Tables.Config;
using MvcMusicStore.Models;
using MvcMusicStore.Models.Dtos;

namespace MvcMusicStore
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            HttpConfiguration config = new HttpConfiguration();

            new MobileAppConfiguration()
               .AddTables(new MobileAppTableConfiguration().AddEntityFramework().MapTableControllers())
               .ApplyTo(config);

            System.Data.Entity.Database.SetInitializer(new MvcMusicStore.Models.SampleData());

            

            CreateMappings();
        }

        private void CreateMappings()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Album, AlbumDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.AlbumId));
            });
        }
    }
}