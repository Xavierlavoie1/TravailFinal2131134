using System.Collections.Generic;
using System.Windows.Documents;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.IO;
using System.Windows.Shapes;
using System.Runtime.ConstrainedExecution;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace CaclulFinancementApp.Model
{
    public class Système
    {
        private List<Client> _listClients;
        public List<Client> ListClients
        {
            get { return _listClients; }
            private set { _listClients = value; }
        }
        private List<Financement> _financement;
        public List<Financement> financement
        {
            get { return _financement; }
            private set { _financement = value; }
        }
        public Système()
        {
            DeserialiserClient();
            DeserialiserFinancement();
        }
        public void SerializeAll()
        {
            this.SerialiserClient();
            this.SerialiserFinancement();
        }
        public void AjouterClient(Client nouveauClient)
        {
            ListClients.Add(nouveauClient);
        }
        public void Ajouterfinancement(Financement nouveauFinancement)
        {
            financement.Add(nouveauFinancement);
        }
        public void SerialiserClient()
        {
            System.Xml.Serialization.XmlSerializer mySerialiser = new System.Xml.Serialization.XmlSerializer(ListClients.GetType());
            XmlWriter writer = XmlWriter.Create("Clients.xml");
            mySerialiser.Serialize(writer, ListClients);
            writer.Close();
        }
        public void DeserialiserClient()
        {
            XmlSerializer ser = null;
            XmlReader reader = null;
            try
            {
                ser = new XmlSerializer(typeof(List<Client>));
                reader = XmlReader.Create("Clients.xml");
                _listClients = (List<Client>)ser.Deserialize(reader);
            }
            catch (FileNotFoundException e)
            {
                _listClients = new List<Client>();
            }
            catch (System.InvalidOperationException e)
            {
                throw (e);
                //Ouvrire la page d'erreur
                //écrire : Fichier Corompu : e.message
            }
            finally
            {
                reader?.Close();
            }


        }
        public void SerialiserFinancement()
        {
            System.Xml.Serialization.XmlSerializer mySerialiser = new System.Xml.Serialization.XmlSerializer(financement.GetType());
            XmlWriter writer = XmlWriter.Create("Financement.xml");
            mySerialiser.Serialize(writer, financement);
            writer.Close();
        }
        public void DeserialiserFinancement()
        {
            XmlSerializer ser = null;
            XmlReader reader = null;
            try
            {
                ser = new XmlSerializer(typeof(List<Financement>));
                reader = XmlReader.Create("Financement.xml");
                _financement = (List<Financement>)ser.Deserialize(reader);
            }
            catch (FileNotFoundException e)
            {
                _financement = new List<Financement>();
            }
            catch (System.InvalidOperationException e)
            {
                throw (e);
                //Ouvrire la page d'erreur
                //écrire : Fichier Corompu : e.message
            }
            finally
            {
                reader?.Close();
            }


        }
    }

}
