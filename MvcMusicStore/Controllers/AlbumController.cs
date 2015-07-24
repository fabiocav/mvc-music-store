using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using Microsoft.Azure.Mobile.Server;
using MvcMusicStore.Models;
using MvcMusicStore.Models.Dtos;

namespace MvcMusicStore.Controllers
{
    public class AlbumController : TableController<AlbumDto>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            MusicStoreEntities storeDB = new MusicStoreEntities();
            DomainManager = new MusicStoreDomainManager<AlbumDto, Album>(storeDB, controllerContext.Request);
        }

        public IQueryable<AlbumDto> GetAllAlbumDtos()
        {
            return Query();
        }
        public SingleResult<AlbumDto> GetAlbumDto(string id)
        {
            return Lookup(id);
        }

    }
}
