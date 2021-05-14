using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace InventoryCore.Models
{
    public class ItemMaster
    {      
            [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Item_ID { get; set; }
       
        [StringLength(100)]
        public string Item_Name { get; set; }

        [DataType("decimal(18 ,2)")]
        public decimal Price { get; set; }
        
        [StringLength(500)]
        public string Description { get; set; }
        public int Add_By { get; set; }
        public DateTime Add_Date { get; set; }
        public int Edit_By { get; set; }
        public DateTime Edit_Date { get; set; }
        public int Delete_By { get; set; }
        public DateTime Delete_Date { get; set; }
        public bool Status { get; set; } = false;
        public bool Deleted { get; set; } = false;
    }
}
