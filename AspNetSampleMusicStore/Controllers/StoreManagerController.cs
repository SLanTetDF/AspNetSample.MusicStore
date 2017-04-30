using AspNetSampleMusicStore.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace AspNetSampleMusicStore.Controllers
{
    //[Authorize(Roles ="Administrator")]
    public class StoreManagerController : Controller
    {
        private MusicStoreContext myMusicStore;

        public StoreManagerController()
        {
            myMusicStore = new MusicStoreContext();
        }

        // GET: /StoreManager/
        public ActionResult Index()
        {
            var albums = myMusicStore.Albums.Include(a => a.Artist).Include(a => a.Genre);
            return View(albums.ToList());
        }

        // GET: /StoreManager/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = myMusicStore.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // GET: /StoreManager/Create
        public ActionResult Create()
        {
            ViewBag.ArtistId = new SelectList(myMusicStore.Artists, "ArtistId", "Name");
            ViewBag.GenreId = new SelectList(myMusicStore.Genres, "GenreId", "Name");
            return View();
        }

        // POST: /StoreManager/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl")] Album album)
        {
            if (ModelState.IsValid)
            {
                myMusicStore.Albums.Add(album);
                myMusicStore.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtistId = new SelectList(myMusicStore.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(myMusicStore.Genres, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        // GET: /StoreManager/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = myMusicStore.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtistId = new SelectList(myMusicStore.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(myMusicStore.Genres, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        // POST: /StoreManager/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl")] Album album)
        {
            if (ModelState.IsValid)
            {
                myMusicStore.Entry(album).State = EntityState.Modified;
                myMusicStore.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistId = new SelectList(myMusicStore.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(myMusicStore.Genres, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        // GET: /StoreManager/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = myMusicStore.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: /StoreManager/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = myMusicStore.Albums.Find(id);
            myMusicStore.Albums.Remove(album);
            myMusicStore.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AdressAndPayment(FormCollection collection)
        {
            var newOrder = new Order();

            if (TryUpdateModel(newOrder))
            {
                newOrder.Username = User.Identity.Name;
                newOrder.OrderDate = DateTime.Now;
                myMusicStore.Orders.Add(newOrder);
                myMusicStore.SaveChanges();

                return RedirectToAction("Complete", new { id = newOrder.OrderId });
            }
            return View(newOrder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                myMusicStore.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
