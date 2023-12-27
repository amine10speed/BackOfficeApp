using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOfficeApp.Models
{
    public class Emprunt
    {
        public int EmpruntID { get; set; }
        public int AdherentID { get; set; }
        public string ISBN { get; set; }
        public DateTime DateEmprunt { get; set; }
        public DateTime DateRetour { get; set; }

        public Adherent Adherent { get; set; }
        public Livre Livre { get; set; }
    }

}
