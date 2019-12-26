using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotelWebApp.Models;

namespace HotelWebApp.Controllers
{
    public class ReservationController : Controller
    {
        private HotelDbContext db = new HotelDbContext();

        // GET: Reservation
        public ActionResult Index(MessageId? message)
        {
            var reservation = db.Reservation.Include(r => r.Customer).Include(r => r.Room);
            ViewBag.StatusMessage = message == MessageId.Success ? "Ekleme Başarılı" :
               message == MessageId.Error ? "Beklenmedik bir hata gerçekleşti!" :
               "";
            return View(reservation.ToList());
        }

        // GET: Reservation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservation/Create
        public ActionResult Create()
        {

           

            ViewBag.CustomerID = new SelectList(db.Customer, "ID", "NameSurname");
            ViewBag.RoomID = new SelectList(db.Room, "ID", "ShowRoomNumber");
            return View();
        }

        // POST: Reservation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ReservationNumber,CheckIn,CheckOut,IsComplete,CustomerID,RoomID,ShowReservationNumber")] Reservation reservation)
        {

            MessageId message;

            if (ModelState.IsValid)
            {

                //var result = db.Reservation.Where(entry => entry.CheckIn >= minDate
                // && entry.CheckOut <= maxDate).ToList();
                var availableRooms = db.Room.Where(m => m.Reservations.All(r => r.CheckOut <= reservation.CheckIn || r.CheckIn >= reservation.CheckOut)).ToList();

                var room = availableRooms.Any(a => a.ID == reservation.RoomID);

                if (room == true)
                {

                    reservation.IsComplete = true;
                    db.Reservation.Add(reservation);
                    
                    db.SaveChanges();

                    message = MessageId.Success;

                    return RedirectToAction("Index", new { Message = message});
                }
                else
                {
                    ViewBag.CustomerID = new SelectList(db.Customer, "ID", "NameSurname", reservation.CustomerID);
                    ViewBag.RoomID = new SelectList(db.Room, "ID", "ShowRoomNumber", reservation.RoomID);

                    var roomName = db.Room.Where(a => a.ID == reservation.RoomID).FirstOrDefault();
                    ViewBag.Message = $"Seçtiğiniz {roomName.ShowRoomNumber} oda bu tarihler arasında müsait değildir!";


                    return View(reservation);
                }



            }

            ViewBag.CustomerID = new SelectList(db.Customer, "ID", "NameSurname", reservation.CustomerID);
            ViewBag.RoomID = new SelectList(db.Room, "ID", "ShowRoomNumber", reservation.RoomID);
            return View(reservation);
        }

        // GET: Reservation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customer, "ID", "NameSurname", reservation.CustomerID);
            ViewBag.RoomID = new SelectList(db.Room, "ID", "RoomNumber", reservation.RoomID);
            return View(reservation);
        }

        // POST: Reservation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ReservationNumber,CheckIn,CheckOut,IsComplete,CustomerID,RoomID,ShowReservationNumber")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customer, "ID", "NameSurname", reservation.CustomerID);
            ViewBag.RoomID = new SelectList(db.Room, "ID", "RoomNumber", reservation.RoomID);
            return View(reservation);
        }

        // GET: Reservation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservation.Find(id);
            db.Reservation.Remove(reservation);
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

    #region Helper 
    public enum MessageId
    {
        Success,
        Error
    }

    #endregion
}
