using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projet2020.Models
{
    public class Clients
    {
        
        [Key]
        public int Id_cli { get ; set ; }
        public string Firstname { get; set; }
        public string Name { get ; set; }
        public string Email { get ; set; }
        public string Adress { get ; set; }
    }
}