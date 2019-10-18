using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TDSTecnologia.FaceAlbum.Web.Models
{
    [Table("tb02_imagem")]
    public class Imagem
    {
        [Key]
        [Column("id")]
        public int ImagemId { get; set; }

        [Column("link")]
        public string Link { get; set; }

        [Column("album")]
        public int AlbumId { get; set; }

        public Album Album { get; set; }
    }
}
