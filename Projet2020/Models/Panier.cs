using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projet2020.Models
{
    public class Panier
    {
        [Key]
        public int id_pan { get; set; }
        public virtual int Id_prod { get; set; }
        public virtual int Id_commande { get; set; }
        public Produits p { get; set; }
    }
}