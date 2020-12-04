using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projet2020.Models
{
    public class Commandes
    {
        
        [Key]
        public int Id_commande { get; set; }
        public virtual int Id_cli { get; set; }
        public virtual ICollection<Produits> Prod { get; set; }

        

    }
}