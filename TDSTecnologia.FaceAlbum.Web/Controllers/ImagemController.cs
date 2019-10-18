using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TDSTecnologia.FaceAlbum.Web.Data;
using TDSTecnologia.FaceAlbum.Web.Models;

namespace TDSTecnologia.FaceAlbum.Web.Controllers
{
    public class ImagemController : Controller
    {
        private readonly AppContexto _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ImagemController(AppContexto context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }


        [HttpGet]
        public IActionResult Novo(int id)
        {
            ViewBag.Destinos = _context.Albuns.FirstOrDefault(x => x.AlbumId == id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Novo([Bind("ImagemId,Link,AlbumId")] Imagem imagem, IFormFile arquivo)
        {
            if (ModelState.IsValid)
            {
                var linkUpload = Path.Combine(_hostingEnvironment.WebRootPath, "Imagens");

                if (arquivo != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(linkUpload, arquivo.FileName), FileMode.Create))
                    {
                        await arquivo.CopyToAsync(fileStream);
                        imagem.Link = "~/Imagens/" + arquivo.FileName;
                    }
                }

                _context.Add(imagem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Detalhes/", "Album", new { id = imagem.AlbumId });
            }
            ViewData["AlbumId"] = new SelectList(_context.Albuns, "AlbumId", "Titulo", imagem.AlbumId);
            return View(imagem);
        }


    }
}
