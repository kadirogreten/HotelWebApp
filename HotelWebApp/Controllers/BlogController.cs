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
    public class BlogController : Controller
    {
        private HotelDbContext db = new HotelDbContext();

        // GET: Blog
        public ActionResult Index()
        {
            return View(db.Blog.ToList());
        }

        // GET: Blog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blog.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: Blog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Title,Content,Comment,ImagePath")] Blog blog)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Blog.Add(blog);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(blog);
        //}


        public ActionResult Create([Bind(Include = "Id,Title,Content,Comment,ImagePath")] Blog blog, HttpPostedFileBase file)
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
                        return RedirectToAction("index", new { Message = message });
                    }
                    else
                    {
                        fileName = Guid.NewGuid() + extension;
                    }

                    file.SaveAs(Path.Combine(Server.MapPath("/Content/blogimages/"), fileName));

                    db.Blog.Add(new Blog
                    {
                        ImagePath = "/Content/blogimages/" + fileName,
                        Title = blog.Title,
                        Content = blog.Content,
                        Comment= blog.Comment
                      
                    });


                }
                else
                {
                    message = MessageId.Error;
                    return RedirectToAction("index", new { Message = message });
                }

                db.SaveChanges();
                message = MessageId.Success;
                return RedirectToAction("Index", new { Message = message });
            }

            return View(blog);
        }













        // GET: Blog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blog.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.





        //public ActionResult Edit([Bind(Include = "Id,Title,Content,Comment,ImagePath")] Blog blog)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(blog).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(blog);
        //}



        [HttpPost]
        [ValidateAntiForgeryToken]

        /// RESİM EDİTLEEME VE YUKARIDA SLİDER EKLEME VS HERŞEY İÇİN KULLANILABİLİR!!!!!!!!!!!!!!!!!!

        //public ActionResult Edit([Bind(Include = "Id,Title,Content,Comment,ImagePath")] Blog blog, HttpPostedFileBase file)
        //{

        //    MessageId message;

        //    string fileName = string.Empty;
        //    string extension = string.Empty;

        //    if (ModelState.IsValid)
        //    {

        //        if (file != null && file.ContentLength > 0 && file.ContentLength < 2 * 1024 * 1024)
        //        {
        //            extension = Path.GetExtension(file.FileName);

        //            if (extension.Contains("pdf") || extension.Contains("doc") || extension.Contains("docx"))
        //            {
        //                message = MessageId.Error;
        //                return RedirectToAction("index", new { Message = message });
        //            }
        //            else
        //            {
        //                fileName = Guid.NewGuid() + extension;
        //            }

        //            file.SaveAs(Path.Combine(Server.MapPath("/Content/blogimages/"), fileName));

        //            if (ModelState.IsValid)
        //            {
        //                db.Entry(blog).State = EntityState.Modified;
        //                db.SaveChanges();
        //                return RedirectToAction("Index");
        //            }


        //        }
        //        else
        //        {
        //            message = MessageId.Error;
        //            return RedirectToAction("index", new { Message = message });
        //        }


        //        message = MessageId.Success;
        //        return RedirectToAction("Index", new { Message = message });
        //    }

        //    return View(blog);
        //}





        public ActionResult Edit(Blog blog, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath("/Content/blogimages/") + file.FileName);
                    blog.ImagePath = file.FileName;

                }


                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blog);
        }









        // GET: Blog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blog.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Blog.Find(id);
            db.Blog.Remove(blog);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult Blog_Getir()
        {
            return View(db.Blog.ToList());
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
