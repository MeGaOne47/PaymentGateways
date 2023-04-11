using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentGateways.Models
{
    public class GioHang
    {
        Qlbanhang db = new Qlbanhang();
        public int nhMasp { get; set; }
        public string sTensp { get; set; }
        public string sAnhBia { get; set; }
        public double dDonGia { get; set; }
        public int nhSoLuong { get; set; }
        public double ThanhTien
        {
            get { return nhSoLuong * dDonGia; }
        }
        //Hàm tạo cho giỏ hàng
        public GioHang(int Masp)
        {
            nhMasp = Masp;
            Sanpham sp = db.Sanphams.Single(n => n.Masp == nhMasp);
            sTensp = sp.Tensp;
            sAnhBia = sp.Anhbia;
            dDonGia = double.Parse(sp.Giatien.ToString());
            nhSoLuong = 1;
        }

    }
}