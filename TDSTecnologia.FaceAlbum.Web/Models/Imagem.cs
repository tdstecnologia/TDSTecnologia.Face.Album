﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDSTecnologia.FaceAlbum.Web.Models
{
    public class Imagem
    {
        public int ImagemId { get; set; }


        public string Link { get; set; }

        public int AlbumId { get; set; }
        public Album Album { get; set; }
    }
}
