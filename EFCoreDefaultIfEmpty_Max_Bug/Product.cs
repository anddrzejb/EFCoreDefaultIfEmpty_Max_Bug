using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreDefaultIfEmpty_Max_Bug.Ext
{
    [Table("tmpExtProduct", Schema = "ext")]
    public partial class Product
    {
        public int Version { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ImporDate { get; set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        
        [InverseProperty(nameof(Ext.Category.Products))]
        public virtual Category Category { get; set; }
    }
}