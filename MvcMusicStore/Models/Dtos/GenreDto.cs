using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Mobile.Server;

namespace MvcMusicStore.Models.Dtos
{
    public class GenreDto : EntityData
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
