using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using System.Xml.Serialization;
namespace CaclulFinancementApp.Model
{
    [XmlRoot(ElementName = "Financement")]
    public class Financement
    {
        private double _financement { get; set; }
        [XmlElement(ElementName = "Financement")]
        public double Financements
        {
            get { return _financement; }
            set { _financement = value; }
        }
        private double _interet { get; set; }
        [XmlElement(ElementName = "Interet")]
        public double interet
        {
            get { return _interet; }
            set { _interet = value; }
        }
        private string _amortissement { get; set; }
        [XmlElement(ElementName = "Amortissement")]
        public string amortissement
        {
            get { return _amortissement; }
            set { _amortissement = value; }
        }
        private string _frequence { get; set; }
        [XmlElement(ElementName = "Frequence")]
        public string Frequence
        {
            get { return _frequence; }
            set { _frequence = value; }
        }
        private double _paiementMensuel { get; set; }
        [XmlElement(ElementName = "PaiementMensuel")]
        public double paiementMensuel
        {
            get { return _paiementMensuel; }
            set { _paiementMensuel = value; }
        }
        private double _versementHypothecaire { get; set; }
        [XmlElement(ElementName = "VersementHypothécaire")]
        public double versementHypothecaire
        {
            get { return _versementHypothecaire; }
            set { _versementHypothecaire = value; }
        }
        private double _versementTotalCapital { get; set; }
        [XmlElement(ElementName = "VersementTotalCapital")]
        public double versementTotalCapital
        {
            get { return _versementTotalCapital; }
            set { _versementTotalCapital = value; }
        }
        private double _fraisInteret { get; set; }
        [XmlElement(ElementName = "FraisInteret")]
        public double fraisInteret
        {
            get { return _fraisInteret; }
            set { _fraisInteret = value; }
        }
        private double _coutTotal { get; set; }
        [XmlElement(ElementName = "CoutTotal")]
        public double coutTotal
        {
            get { return _coutTotal; }
            set { _coutTotal = value; }
        }
        private double _remboursementCapitalMensuel { get; set; }
        [XmlElement(ElementName = "RemboursementCapitalMensuel")]
        public double remboursementCapitalMensuel
        {
            get { return _remboursementCapitalMensuel; }
            set { _remboursementCapitalMensuel = value; }
        }
        private double _interetMensuel { get; set; }
        [XmlElement(ElementName = "InteretMensuel")]
        public double interetMensuel
        {
            get { return _interetMensuel; }
            set { _interetMensuel = value; }
        }
        private double _balanceHypothequeMensuel { get; set; }
        [XmlElement(ElementName = "BalanceHypothequeMensuel")]
        public double balanceHypothequeMensuel
        {
            get { return _balanceHypothequeMensuel; }
            set { _balanceHypothequeMensuel = value; }
        }
        private string _randomId;
        [XmlElement(ElementName = "randomId")]
        public string randomId
        {
            get { return _randomId; }
            set { _randomId = value; }
        }
        private List<Paiement> _listFinancementMensuel { get; set; }
        public List<Paiement> listFinancementMensuel
        {
            get { return _listFinancementMensuel; }
            set { _listFinancementMensuel = value; }
        }
        private double _frequenceNombre { get; set; }
        public double frequenceNombre
        {
            get { return _frequenceNombre; }
            set { _frequenceNombre = value; }
        }
        private double _amortissementNombre { get; set; }
        public double amortissementNombre
        {
            get { return _amortissementNombre; }
            set { _amortissementNombre = value; }
        }
        
        public Financement()
        {

        }
        public Financement(double financement, double interet,double nbAmortissement, string amortissement, string frequence,string randomId)
        {
            this.Financements = financement;
            this.interet = interet;
            this.amortissement = amortissement;
            this.Frequence = frequence;
            this.randomId = randomId;
        }

        public void Calcul(double financement, double interet, double amortissementNombres, string periodeAmortissement, string frequence)
        {
            Frequence = frequence.Trim();
            Financements = financement;
            Financements = CutNumber(Financements);
            double nbPaiement;
            listFinancementMensuel = new List<Paiement>();
            CalculFrequence(frequence);
            CalculAmortissement(periodeAmortissement, amortissementNombres);
            nbPaiement = CalculerNbPaiement(amortissementNombre, frequenceNombre);
            interet = interet / 100;
            int freqNb = 0;
            if (frequence.ToLower().Trim() == "mensuel")
            {
                freqNb = 12;
            }
            else if (frequence.ToLower().Trim() == "aux deux semaines")
            {
                freqNb = 24;
            }
            else if (frequence.ToLower().Trim() == "hebdomadaire")
            {
                freqNb = 24;
            }
            double tauxPeriode = interet / freqNb;
            balanceHypothequeMensuel = financement;
            paiementMensuel = (financement * tauxPeriode) / (1 - (Math.Pow((1 + tauxPeriode), amortissementNombre * -1)));
            paiementMensuel = CutNumber(paiementMensuel);
            for (int i = 0; i < amortissementNombre; i++)
            {
                double interetMensuelTmp = 0;
                remboursementCapitalMensuel = 0;
                interetMensuelTmp += tauxPeriode * balanceHypothequeMensuel;
                remboursementCapitalMensuel = paiementMensuel - interetMensuelTmp;
                balanceHypothequeMensuel = balanceHypothequeMensuel - remboursementCapitalMensuel;
                if (balanceHypothequeMensuel < 0)
                {
                    balanceHypothequeMensuel = 0;
                }
                interetMensuelTmp = CutNumber(interetMensuelTmp);
                remboursementCapitalMensuel = CutNumber(remboursementCapitalMensuel);
                balanceHypothequeMensuel = CutNumber(balanceHypothequeMensuel);
                Paiement lePaiement = new Paiement(i + 1, paiementMensuel, remboursementCapitalMensuel, interetMensuelTmp, balanceHypothequeMensuel);
                listFinancementMensuel.Add(lePaiement);
                fraisInteret += interetMensuelTmp;
            }
            fraisInteret = CutNumber(fraisInteret);
            coutTotal = financement + fraisInteret;
            coutTotal = CutNumber(coutTotal);
        }
        public double CutNumber(double num)
        {
            string numString = num.ToString("N2");
            return Convert.ToDouble(numString);
        }
        public void CalculFrequence(string frequence)
        {
            if ("hebdomadaire" == frequence.ToLower().Trim())
            {
                frequenceNombre = 52;
            }
            else if ("mensuel" == frequence.ToLower().Trim())
            {
                frequenceNombre = 1;
            }
            else if ("aux deux semaines" == frequence.ToLower().Trim())
            {
                frequenceNombre = 26;
            }
        }
        public void CalculAmortissement(string periodeAmortissement,double amortissementNombres)
        {
            if ("annee" == periodeAmortissement.ToLower().Trim())
            {
                if (Frequence.ToLower().Trim() == "mensuel")
                {
                    amortissementNombre = amortissementNombres * 12;
                }
                else if (Frequence.ToLower().Trim() == "aux deux semaines")
                {
                    amortissementNombre = amortissementNombres * 24;
                }
                else if (Frequence.ToLower().Trim() == "hebdomadaire")
                {
                    amortissementNombre = amortissementNombres * 52;
                }

            }
            if ("mois" == periodeAmortissement.ToLower().Trim())
            {
                amortissementNombre = amortissementNombres;
            }
            
        }
        public double CalculerNbPaiement(double amortissementNb, double frequenceNb)
        {
            return amortissementNb * frequenceNb;
        }
    }
}
