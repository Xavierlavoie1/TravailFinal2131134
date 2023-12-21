using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaclulFinancementApp.Model
{
    public class Paiement
    {
        public double nbMois { get; set; }
        public double paiement { get; set; }
        public double Capital { get; set; }
        public double Interet { get; set; }
        public double Balance { get; set; }
        public Paiement()
        {

        }
        public Paiement(double nbMois, double paiement, double capital, double interet, double balance)
        {
            this.nbMois = nbMois;
            this.paiement = paiement;
            Capital = capital;
            Interet = interet;
            Balance = balance;
        }
    }
}
