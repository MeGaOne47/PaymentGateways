using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaymentGateways.Models;

namespace PaymentGateways.Controllers
{
    public class SanphamController : Controller
    {
        Qlbanhang db = new Qlbanhang();

        // GET: Sanpham
        public ActionResult nhchanelpartial()
        {
            var chanel = db.Sanphams.Where(n => n.Mahang == 1).Take(4).ToList();
            return PartialView(chanel);
        }
        public ActionResult nhdiorpartial()
        {
            var dior = db.Sanphams.Where(n => n.Mahang == 3).Take(4).ToList();
            return PartialView(dior);
        }
        public ActionResult nhguccipartial()
        {
            var gucci = db.Sanphams.Where(n => n.Mahang == 2).Take(4).ToList();
            return PartialView(gucci);
        }
        public ActionResult xemchitiet(int Masp = 0)
        {
            var chitiet = db.Sanphams.SingleOrDefault(n => n.Masp == Masp);
            if (chitiet == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(chitiet);
        }
        // GET: Sanpham
        public ActionResult Danhsachsanpham(int page = 1, int pageSize = 6)
        {
            var allProducts = db.Sanphams.ToList();
            var totalItems = allProducts.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var products = allProducts.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(products);
        }


    }

}