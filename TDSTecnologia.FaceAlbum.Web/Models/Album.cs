using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TDSTecnologia.FaceAlbum.Web.Models
{
    [Table("tb01_album")]
    public class Album
    {
        [Key]
        [Column("id")]
        public int AlbumId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(50, ErrorMessage = "Limite de 50 caracteres")]
        [Column("destino")]
        public string Destino { get; set; }

        [Column("fototopo")]
        public string FotoTopo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [Column("dt_inicio")]
        public DateTime Inicio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [Column("dt_fim")]
        public DateTime Fim { get; set; }

        //public ICollection<Imagem> Imagens { get; set; }
    }
}
