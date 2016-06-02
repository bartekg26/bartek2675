using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjektPortale.Models
{
    public class Produkt
    {
        //[Required(ErrorMessage = "Proszę wprowadzić imię")]
        [DisplayName("Nazwa produktu"), Required(ErrorMessage = "Puste pole.")]
        public string nazwaProduktu { get; set; }

        
        [DisplayName("Opis produktu"), Required(ErrorMessage = "Puste pole.")]
        public string opisProduktu { get; set; }

        [DisplayName("Cena"), Required(ErrorMessage = "Puste pole.")]
        public int cena { get; set; }

        [DisplayName("Ścieżka do obrazka")]
        public string sciezkaObrazka { get; set; }

        //[DisplayName("Styl obrazka")]
        //public string stylObrazka { get; set; }

        [DisplayName("Id produktu")]
        [Key]
        public int idProduct { get; set; }

        public ICollection<ApplicationUser> Users { get; set; } //koszyk

        public virtual ApplicationUser Produkty_User { get; set; } //do odwołania się bezpośrednio do użytkownika
        public string Produkty_UserId { get; set; }//do trzymania w bazie danych
    }
}