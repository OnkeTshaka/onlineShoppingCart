using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace ReturnGym.Models.OnlineShopping
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }
        public PartialViewResult ProductListPartial(string searching,int? page, int? category)
        {
            var pageNumber = page ?? 1;
            var pageSize = 6;
            if (category != null)
            {
                ViewBag.category = category;
                var productList = db.Products
                    .OrderByDescending(x => x.ProductID)
                    .Where(x => x.CategoryID == category)
                    .ToPagedList(pageNumber, pageSize);
                return PartialView(productList);
            }
            else
            {
                var productList = db.Products.OrderByDescending(x => x.ProductID).Where(m => m.Name.Contains(searching) || m.CatName.Contains(searching) || searching == null).ToPagedList(pageNumber, pageSize);
                return PartialView(productList);
            }
        }
        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            Product A1 = new Product();
            return View(A1);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,CategoryID,Name,Picture,Price,Time,Quantity")] Product product, HttpPostedFileBase image1)
        {
            if (image1 != null)
            {
                product.Picture = new byte[image1.ContentLength];
                image1.InputStream.Read(product.Picture, 0, image1.ContentLength);
            }
            if (ModelState.IsValid)
            {
                product.CatName = product.getCategory();
                product.Time = DateTime.Now.ToString();
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,CategoryID,Name,Picture,Price,Time,Quantity")] Product product, HttpPostedFileBase image1)
        {
            if (ModelState.IsValid)
            {
                Product productInDB = db.Products.Single(c => c.ProductID == product.ProductID);
                if (image1 != null)
                {
                    product.Picture = new byte[image1.ContentLength];
                    image1.InputStream.Read(product.Picture, 0, image1.ContentLength);
                    productInDB.Picture = product.Picture;
                }
                productInDB.CategoryID = product.CategoryID;
                productInDB.Name = product.Name;
                productInDB.Price = product.Price;
                productInDB.Quantity = product.Quantity;
                productInDB.Time = DateTime.Now.ToString();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
