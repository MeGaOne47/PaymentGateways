namespace PaymentGateways.Models
{
    using PaymentGateways.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Thuonghieu")]
    public partial class Thuonghieu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Thuonghieu()
        {
            Sanpham = new HashSet<Sanpham>();
        }

        [Key]
        public int Math { get; set; }

        [StringLength(10)]
        [Display(Name = "Tên thương hiệu")]

        public string Tenth { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sanpham> Sanpham { get; set; }
    }
}
