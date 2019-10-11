using Microsoft.EntityFrameworkCore;
using TDSTecnologia.FaceAlbum.Web.Models;

namespace TDSTecnologia.FaceAlbum.Web.Data
{
    public class AppContexto : DbContext
    {
        public AppContexto(DbContextOptions<AppContexto> opcoes) : base(opcoes)
        {
        }
        public DbSet<Album> Albuns { get; set; }

        public DbSet<Imagem> Imagens { get; set; }
    }
}
