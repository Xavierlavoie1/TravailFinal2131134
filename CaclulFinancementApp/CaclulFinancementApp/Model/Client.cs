using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using System.Xml.Serialization;
namespace CaclulFinancementApp.Model
{
    [XmlRoot(ElementName = "Client")]
    public class Client
    {
        private static int _cptParticipant { get; set; }

        private string _nom;
        [XmlElement(ElementName = "Nom")]
        public string nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        private string _prenom;
        [XmlElement(ElementName = "Prénom")]
        public string prenom
        {
            get { return _prenom; }
            set { _prenom = value; }
        }
        private string _description;
        [XmlElement(ElementName = "Description")]
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _randomId;
        [XmlElement(ElementName = "randomId")]
        public string randomId
        {
            get { return _randomId; }
            set { _randomId = value; }
        }

        public Client(string nom, string prenom,string description)
        {
            if (nom == null || prenom == null || nom == "" || prenom == "" || description == null || description == "" )
            {
                this.nom = "Doe";
                this.prenom = "John";
                this.description = "Maison";
            }
            else
            {
                this.nom = nom;
                this.prenom = prenom;
                this.description = description;
            }

            _cptParticipant++;
            _randomId = CreateRandomId();
        }

        public Client()
        {

        }

        public string CreateRandomId()
        {
            string id;
            id = _cptParticipant + "" + DateTime.Now.Ticks + this.nom[0];
            return id;
        }
    }
}
