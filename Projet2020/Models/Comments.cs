using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projet2020.Models
{
    public class Comments
    {
        [Key]
        public int Id_com { get; set;  }
        public virtual int Id_prod { get; set; }
        public string Comment { get; set; }

    }
}