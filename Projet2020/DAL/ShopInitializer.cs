using Projet2020.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projet2020.DAL
{
    public class ShopInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ShopContext>
    {
        protected override void Seed(ShopContext context)
        {
            var Client = new List<Clients>
            {
                new Clients{ Id_cli=1, Firstname="Guillaume",Name="Godart" ,Email="guigodart9@gmail.com", Adress="Rue de la mort" },
                new Clients{ Id_cli=2, Firstname="Alexis",Name="Calens" ,Email="ulvent@hotmail.com", Adress="rue du parc" },
            };
            Client.ForEach(c => context.Client.Add(c));
            context.SaveChanges();


            var Produit = new List<Produits>
            {
                new Produits{ Id_prod=1, Name_produits="Nimbus 2000", Price=200, Stk=10 },
                new Produits{ Id_prod=2, Name_produits="Nimbus 2001", Price=5000, Stk=2 },
            };
            Produit.ForEach(p => context.Product.Add(p));
            context.SaveChanges();

            var Comm = new List<Commandes>
            {
               new Commandes{ Id_cli=2, Id_commande=1, Prod=Produit },
            };
            Comm.ForEach(o => context.orders.Add(o));
            context.SaveChanges();

            var co = new List<Comments>
            {
                new Comments{ Id_com=1, Id_prod=2, Comment="ma méne il est chère" },
            };
            co.ForEach(comms => context.Com.Add(comms));
            context.SaveChanges();

            var pan = new List<Panier>
            {
                new Panier{id_pan=1, Id_commande=1, Id_prod=2},
            };
            pan.ForEach(comms => context.Paniers.Add(comms));
            context.SaveChanges();
        }
    }
}