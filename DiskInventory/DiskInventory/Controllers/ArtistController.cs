using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DiskInventory.Models;

namespace DiskInventory.Controllers
{
    public class ArtistController : Controller
    {
        private diskInventoryv2bmContext context { get; set; }

        public ArtistController(diskInventoryv2bmContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var artists = context.Artists.OrderBy(a => a.Fname).ThenBy(a => a.Lname).ToList();
            return View(artists);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(t => t.Description).ToList();
            return View("Edit", new Artist());
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(t => t.Description).ToList();
            var artist = context.Artists.Find(id);
            return View(artist);
        }
        [HttpPost]
        public IActionResult Edit(Artist theartist)
        {
            if (ModelState.IsValid)
            {
                if (theartist.ArtistId == 0)
                {
                    //context.Artists.Add(theartist);
                    context.Database.ExecuteSqlRaw("execute sp_ins_artist @p0, @p1, @p2",
                        parameters: new[] { theartist.Fname, theartist.Lname, theartist.ArtistTypeId.ToString() });
                }
                else
                {
                    //context.Artists.Update(theartist);
                    context.Database.ExecuteSqlRaw("execute sp_upd_artist @p0, @p1, @p2, @p3",
                        parameters: new[] { theartist.ArtistId.ToString(), theartist.Fname, theartist.Lname, theartist.ArtistTypeId.ToString() });
                }
                //context.SaveChanges();
                return RedirectToAction("Index", "Artist");
            }
            else
            {
                ViewBag.Action = (theartist.ArtistId == 0) ? "Add" : "Edit";
                ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(t => t.Description).ToList();
                return View(theartist);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var artist = context.Artists.Find(id);
            return View(artist);
        }
        [HttpPost]
        public IActionResult Delete(Artist artist)
        {
            //context.Artists.Remove(artist);
            //context.SaveChanges();
            context.Database.ExecuteSqlRaw("execute sp_del_artist @p0",
                parameters: new[] { artist.ArtistId.ToString() });
            return RedirectToAction("Index", "Artist");
        }
    }
}
