using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projet2020.Models
{
    public class Produits
    {
        
        [Key]
        public int Id_prod { get; set; }
        public string Name_produits { get; set; }
        public double Price { get; set; }
        public int Stk { get; set; }
    }
}