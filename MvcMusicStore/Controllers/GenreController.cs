using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using Microsoft.Azure.Mobile.Server;
using MvcMusicStore.Models;
using MvcMusicStore.Models.Dtos;

namespace MvcMusicStore.Controllers
{
    public class GenreController : TableController<GenreDto>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            MusicStoreEntities storeDB = new MusicStoreEntities();
            DomainManager = new MusicStoreDomainManager<GenreDto, Genre>(storeDB, controllerContext.Request);
        }

        public IQueryable<GenreDto> GetAllAlbumDtos()
        {
            return Query();
        }

        public SingleResult<GenreDto> GetAlbumDto(string id)
        {
            return Lookup(id);
        }
    }
}
