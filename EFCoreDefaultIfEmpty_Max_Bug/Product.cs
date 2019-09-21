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
        public int CategoryId { get; set; }        
    }
}