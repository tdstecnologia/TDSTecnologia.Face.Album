using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TDSTecnologia.FaceAlbum.Web.Data;
using TDSTecnologia.FaceAlbum.Web.Models;

namespace TDSTecnologia.FaceAlbum.Web.Controllers
{
    public class AlbumController : Controller
    {

        private readonly AppContexto _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AlbumController(AppContexto context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Albuns.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albuns
                .FirstOrDefaultAsync(m => m.AlbumId == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        [HttpGet]
        public IActionResult Novo()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Novo([Bind("AlbumId,Titulo,Descricao,Capa,DataInicio,")] Album album, IFormFile arquivo)
        {
            if (ModelState.IsValid)
            {
                var linkUpload = Path.Combine(_hostingEnvironment.WebRootPath, "Imagens");

                if (arquivo != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(linkUpload, arquivo.FileName), FileMode.Create))
                    {
                        await arquivo.CopyToAsync(fileStream);
                        album.Capa = "~/Imagens/" + arquivo.FileName;
                    }
                }

                _context.Add(album);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(album);
        }

        [HttpGet]
        public async Task<IActionResult> Alterar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albuns.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            TempData["Capa"] = album.Capa;

            return View(album);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Alterar(int id, [Bind("AlbumId,Titulo,Descricao,Capa,DataInicio")] Album album, IFormFile arquivo)
        {
            if (id != album.AlbumId)
            {
                return NotFound();
            }

            if (String.IsNullOrEmpty(album.Capa))
            {
                album.Capa = TempData["Capa"].ToString();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var linkUpload = Path.Combine(_hostingEnvironment.WebRootPath, "Imagens");

                    if (arquivo != null)
                    {
                        using (var fileStream = new FileStream(Path.Combine(linkUpload, arquivo.FileName), FileMode.Create))
                        {
                            await arquivo.CopyToAsync(fileStream);
                            album.Capa = "~/Imagens/" + arquivo.FileName;
                        }
                    }

                    _context.Update(album);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.AlbumId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(album);
        }


        [HttpPost]
        public async Task<JsonResult> Excluir(int AlbumId)
        {
            var album = await _context.Albuns.FindAsync(AlbumId);
            IEnumerable<string> links = _context.Imagens.Where(i => i.AlbumId == AlbumId).Select(i => i.Link);

            foreach (var link in links)
            {
                var linkImagem = link.Replace("~", "wwwroot");
                System.IO.File.Delete(linkImagem);
            }

            _context.Imagens.RemoveRange(_context.Imagens.Where(x => x.AlbumId == AlbumId));

            string linkFotoAlbum = album.Capa;
            linkFotoAlbum = linkFotoAlbum.Replace("~", "wwwroot");
            System.IO.File.Delete(linkFotoAlbum);
            _context.Albuns.Remove(album);
            await _context.SaveChangesAsync();
            return Json("Album excluído com sucesso");
        }

        private bool AlbumExists(int id)
        {
            return _context.Albuns.Any(e => e.AlbumId == id);
        }

    }
}
