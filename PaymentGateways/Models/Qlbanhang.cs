using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PaymentGateways.Models
{
    public partial class Qlbanhang : DbContext
    {
        public Qlbanhang()
            : base("name=Qlbanhang")
        {
        }
        public virtual DbSet<Hangsanxuat> Hangsanxuats { get; set; }
        public virtual DbSet<Thuonghieu> Thuonghieus { get; set; }
        public virtual DbSet<Sanpham> Sanphams { get; set; }
        public virtual DbSet<ThanhToan> ThanhToans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Hangsanxuat>()
                .Property(e => e.Tenhang)
                .IsFixedLength();

            modelBuilder.Entity<Thuonghieu>()
                .Property(e => e.Tenth)
                .IsFixedLength();

            modelBuilder.Entity<Sanpham>()
                .Property(e => e.Giatien)
                .HasPrecision(18, 0);

        }
    }
}