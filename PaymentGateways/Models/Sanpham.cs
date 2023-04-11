namespace PaymentGateways.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Sanpham")]
    public partial class Sanpham
    {

        [Key]
        public int Masp { get; set; }

        [StringLength(50)]
        [Display(Name = "Tên sản phẩm")]

        public string Tensp { get; set; }
        [Display(Name = "Giá tiền")]

        public decimal? Giatien { get; set; }
        [Display(Name = "Số lượng")]

        public int? Soluong { get; set; }
        [Display(Name = "Mô tả")]

        [Column(TypeName = "ntext")]
        public string Mota { get; set; }
        [Display(Name = "Dung tích")]

        public int? Dungtich { get; set; }

        [Display(Name = "Là sản phẩm mới")]

        public bool? Sanphammoi { get; set; }

        [Display(Name = "Ảnh sản phẩm")]

        [StringLength(50)]
        public string Anhbia { get; set; }
        [Display(Name = "Hãng sản xuất")]

        public int? Mahang { get; set; }
        [Display(Name = "Thương hiệu")]

        public int? Math { get; set; }

        public virtual Hangsanxuat Hangsanxuat { get; set; }

        public virtual Thuonghieu Thuonghieu { get; set; }
    }
}
