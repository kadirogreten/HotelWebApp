using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotelWebApp.Models;

namespace HotelWebApp.Controllers
{


    public class SliderController : Controller
    {

        private HotelDbContext db = new HotelDbContext();

        // GET: Slider
        public ActionResult Index(MessageId? message)
        {
            ViewBag.StatusMessage = message == MessageId.Success ? "Slider ekleme başarılı" :
                message == MessageId.Error ? "Beklenmedik bir hata oluştu! dosya formatı sadece resim dosyası olmalıdır ve 2 mb geçmemelidir!" : "";

            return View(db.Slider.ToList());
        }

        // GET: Slider/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // GET: Slider/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Slider/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FileName,Row,Title_1,Title_2")] Slider slider,HttpPostedFileBase file)
        {

            MessageId message;

            string fileName = string.Empty;
            string extension = string.Empty;

            if (ModelState.IsValid)
            {

                if (file != null && file.ContentLength > 0 && file.ContentLength < 2 * 1024 * 1024)
                {
                    extension = Path.GetExtension(file.FileName);

                    if (extension.Contains("pdf") || extension.Contains("doc") || extension.Contains("docx"))
                    {
                        message = MessageId.Error;
                        return RedirectToAction("index",new {Message = message });
                    }else
                    {
                        fileName = Guid.NewGuid() + extension;
                    }

                    file.SaveAs(Path.Combine(Server.MapPath("/Content/slider/"), fileName));

                    db.Slider.Add(new Slider
                    {
                        FileName = "/Content/slider/" + fileName,
                        Row = slider.Row,
                        Title_1 = slider.Title_1,
                        Title_2 = slider.Title_2
                    });
                   

                }else
                {
                    message = MessageId.Error;
                    return RedirectToAction("index", new { Message = message });
                }

                db.SaveChanges();
                message = MessageId.Success;
                return RedirectToAction("Index",new { Message = message});
            }

            return View(slider);
        }

        // GET: Slider/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Slider/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FileName,Row,Title_1,Title_2")] Slider slider)
        {
            if (ModelState.IsValid)
            {
                db.Entry(slider).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        // GET: Slider/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Slider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider = db.Slider.Find(id);
            db.Slider.Remove(slider);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
