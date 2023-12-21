using CaclulFinancementApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Diagnostics;
using System.Windows.Resources;
using System.IO;
using CaclulFinancementApp.Model;

namespace CaclulFinancementApp.ViewModel
{
    internal class GestionClientVM : INotifyPropertyChanged
    {
        #region Declaration
        public GestionClientVM _gestionClient { get; set; }
        public Système _leSystem { get; set; }
        private Client _clientSelectionner { get; set; }
        private List<Client> _listClient { get; set; }
        private List<Paiement> _listFinancementMensuel { get; set; }
        public string _messageErreur { get; set; }
        public string _messageErreurVisibility { get; set; }
        private bool _boutonSupprimerActState;
        private double _financement { get; set; }
        
        private double _paiementMensuel { get; set; }
        public double paiementMensuel
        {
            get { return _paiementMensuel; }
            set
            {
                _paiementMensuel = value;
                ValeurChangee("paiementMensuel");
            }
        }
        private double _versementHypothecaire { get; set; }
        public double versementHypothecaire
        {
            get { return _versementHypothecaire; }
            set
            {
                _versementHypothecaire = value;
                ValeurChangee("versementHypothecaire");
            }
        }
        private double _versementTotalCapital { get; set; }
        public double versementTotalCapital
        {
            get { return _versementTotalCapital; }
            set
            {
                _versementTotalCapital = value;
                ValeurChangee("versementTotalCapital");
            }
        }
        private double _fraisInteret { get; set; }
        public double fraisInteret
        {
            get { return _fraisInteret; }
            set
            {
                _fraisInteret = value;
                ValeurChangee("fraisInteret");
            }
        }
        private double _coutTotal { get; set; }
        public double coutTotal
        {
            get { return _coutTotal; }
            set
            {
                _coutTotal = value;
                ValeurChangee("coutTotal");
            }
        }
        private double _remboursementCapitalMensuel { get; set; }
        public double remboursementCapitalMensuel
        {
            get { return _remboursementCapitalMensuel; }
            set
            {
                _remboursementCapitalMensuel = value;
                ValeurChangee("remboursementCapitalMensuel");
            }
        }
        private double _interetMensuel { get; set; }
        public double interetMensuel
        {
            get { return _interetMensuel; }
            set
            {
                _interetMensuel = value;
                ValeurChangee("interetMensuel");
            }
        }
        private double _balanceHypothequeMensuel { get; set; }
        public double balanceHypothequeMensuel
        {
            get { return _balanceHypothequeMensuel; }
            set
            {
                _balanceHypothequeMensuel = value;
                ValeurChangee("balanceHypothequeMensuel");
            }
        }
        public List<Paiement> listFinancementMensuel
        {
            get { return new List<Paiement>(_listFinancementMensuel); }
            set
            {
                _listFinancementMensuel = value;
                ValeurChangee("listFinancementMensuel");
            }
        }
        private double _frequenceNombre { get; set; }
        public double frequenceNombre
        {
            get { return _frequenceNombre; }
            set
            {
                _frequenceNombre = value;
                ValeurChangee("frequenceNombre");
            }
        }
        private double _amortissementNombre { get; set; }
        public double amortissementNombre
        {
            get { return _amortissementNombre; }
            set
            {
                _amortissementNombre = value;
                ValeurChangee("amortissementNombre");
            }
        }


        public string messageErreur
        {
            get { return _messageErreur; }
            set
            {
                _messageErreur = value;
                ValeurChangee("messageErreur");
            }
        }
        public string messageErreurVisibility
        {
            get { return _messageErreurVisibility; }
            set
            {
                _messageErreurVisibility = value;
                ValeurChangee("messageErreurVisibility");
            }
        }
        public bool boutonSupprimerActState
        {
            get { return _boutonSupprimerActState; }
            set
            {
                _boutonSupprimerActState = value;
                ValeurChangee("boutonSupprimerActState");
            }
        }
        public List<Client> clientList
        {
            get { return _listClient; }
            set
            {
                _listClient = value;
                ValeurChangee("clientList");
            }
        }
        public Client clientSelectionner
        {
            get { return _clientSelectionner; }
            set
            {
                _clientSelectionner = value;
                ValeurChangee("clientSelectionner");
            }
        }
        public double financement
        {
            get { return _financement; }
            set
            {
                _financement = value;
                ValeurChangee("financement");
            }
        }
        private double _interet { get; set; }
        public double interet
        {
            get { return _interet; }
            set
            {
                _interet = value;
                ValeurChangee("interet");
            }
        }
        private string _amortissement { get; set; }
        public string amortissement
        {
            get { return _amortissement; }
            set
            {
                _amortissement = value;
                ValeurChangee("amortissement");
            }
        }
        private string _frequence { get; set; }
        public string frequence
        {
            get { return _frequence; }
            set
            {
                _frequence = value;
                ValeurChangee("frequence");
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void ValeurChangee(string nomPropriete)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(nomPropriete));
            }
        }
        #endregion 
        public GestionClientVM()
        {
            try
            {
                _leSystem = new Système();
            }
            catch (System.InvalidOperationException e)
            {
                messageErreur = "⚠️ Impossible de récuperer la sauvegarde, les fichiers ont été corrompus";
                messageErreurVisibility = "Visible";
                ValeurChangee("messageErreur");
                ValeurChangee("messageErreurVisibility");

                try
                {
                    File.Delete("Clients.xml");
                    File.Delete("Financement.xml");

                    _leSystem = new Système();
                }
                catch (System.InvalidOperationException i)
                {
                    _leSystem = new Système();
                }
            }
        }
        #region Declaration 
        private string _nomAjout, _prenomAjout, _descAjout, _amortissementAjout,_amortissementModif,_frequenceAjout,_frequenceModif,_randomId;
        private Client _clientAjout, _clientSelectioner;
        private List<Client> _client;
        private Système sysPrincipal;
        private bool _boutonSupprimerState, _boutonModifierState;
        private double _financementAjout, _interetAjout,_amortissementNb;

        public double amortissementNb
        {
            get { return _amortissementNb; }
            set
            {
                _amortissementNb = value;
                ValeurChangee("amortissementNb");
            }
        }
        public bool boutonModifierState
        {
            get { return _boutonModifierState; }
            set
            {
                _boutonModifierState = value;
                ValeurChangee("boutonModifierState");
            }
        }
        public bool boutonSupprimerState
        {
            get { return _boutonSupprimerState; }
            set
            {
                _boutonSupprimerState = value;
                ValeurChangee("boutonSupprimerState");
            }
        }
        public List<Client> clients
        {
            get
            {
                return new List<Client>(_client);
            }
            set
            {
                _client = value;
                ValeurChangee("clients");
            }
        }
        public string frequenceAjout
        {
            get { return _frequenceAjout; }
            set
            {
                ValeurChangee("frequenceAjout");
                _frequenceAjout = value;
            }
        }
        public string frequenceModif
        {
            get { return _frequenceModif; }
            set
            {
                ValeurChangee("frequenceModif");
                _frequenceModif = value;
            }
        }
        private bool _isOption1Selected;
        public bool IsOption1Selected
        {
            get { return _isOption1Selected; }
            set
            {
                if (_isOption1Selected != value)
                {
                    _isOption1Selected = value;
                    ValeurChangee(nameof(IsOption1Selected));
                    if (value)
                    {
                        SelectedOption = "Mois";
                    }
                }
            }
        }

        private bool _isOption2Selected;
        public bool IsOption2Selected
        {
            get { return _isOption2Selected; }
            set
            {
                if (_isOption2Selected != value)
                {
                    _isOption2Selected = value;
                    ValeurChangee(nameof(IsOption2Selected));
                    if (value)
                    {
                        SelectedOption = "Annee";
                    }
                }
            }
        }
        private string _selectedOption;
        public string SelectedOption
        {
            get { return _selectedOption; }
            set
            {
                if (_selectedOption != value)
                {
                    _selectedOption = value;
                    ValeurChangee(nameof(SelectedOption));
                }
            }
        }
        public string amortissementAjout
        {
            get { return _amortissementAjout; }
            set
            {
                ValeurChangee("amortissementAjout");
                _amortissementAjout = value;
            }
        }
        public string amortissementModif
        {
            get { return _amortissementModif; }
            set
            {
                ValeurChangee("amortissementModif");
                _amortissementModif = value;
            }
        }
        public double interetAjout
        {
            get { return _interetAjout; }
            set
            {
                ValeurChangee("interetAjout");
                _interetAjout = value;
            }
        }
        public double financementAjout
        {
            get { return _financementAjout; }
            set
            {
                ValeurChangee("financementAjout");
                _financementAjout = value;
            }
        }

        public string descAjout
        {
            get { return _descAjout; }
            set
            {
                ValeurChangee("descAjout");
                _descAjout = value;
            }
        }
        public string nomAjout
        {
            get { return _nomAjout; }
            set
            {
                ValeurChangee("nomAjout");
                _nomAjout = value;
            }
        }

        public string prenomAjout
        {
            set
            {
                ValeurChangee("prenomAjout");
                _prenomAjout = value;

            }
            get { return _prenomAjout; }
        }
        public Client clientAjout
        {
            get { return _clientAjout; }
            set
            {
                ValeurChangee("clientAjouter");
                _clientAjout = value;
            }
        }
        public Client clientSelectioner
        {
            get { return _clientSelectionner; }
            set
            {
                _clientSelectionner = value;
                ValeurChangee("clientSelectionner");
                setModif();
            }
        }
        #endregion

        #region Commande

        //Calculer un financement
        private ICommand _calculerFinancement;

        public ICommand calculerFinancement
        {
            get { return _calculerFinancement; }
            private set { _calculerFinancement = value; }
        }

        private void calculerFinancement_Execute(object parSelect)
        {
            if (financementAjout > 0 && interetAjout > 0 && amortissementNb > 0 && frequenceAjout != "")
            {
                Financement financement = new Financement();
                string[] separatedString;
                separatedString = frequenceAjout.Split(':');
                frequenceAjout = separatedString[1];
                financement.Calcul(financementAjout, interetAjout, amortissementNb, SelectedOption, frequenceAjout);
                listFinancementMensuel = financement.listFinancementMensuel;
                financementAjout = financement.Financements;
                fraisInteret = financement.fraisInteret;
                coutTotal = financement.coutTotal;
                ValeurChangee("coutTotal");
                ValeurChangee("fraisInteret");
                ValeurChangee("financementAjout");
                ValeurChangee("listFinancementMensuel");
            }
            
        }

        private bool calculerFinancement_CanExecute(object param)
        {
            return true;
        }
        //calculer un financement

        //Supprimer Participant
        private ICommand _SupprimerClient;

        public ICommand SupprimerClient
        {
            get { return _SupprimerClient; }
            private set { _SupprimerClient = value; }
        }

        private void SupprimerClient_Execute(object parSelect)
        {
            //ajouter participant
            _client.Remove(clientSelectioner);
            baseSelectPart();
            EnableDisableBouton();
            ValeurChangee("clients");
        }

        private bool SupprimerClient_CanExecute(object param)
        {
            return true;
        }
        private ICommand _AjouterClient;

        public ICommand AjouterClient
        {
            get { return _AjouterClient; }
            private set { _AjouterClient = value; }
        }

        private void AjouterClient_Execute(object parSelect)
        {
            if (clientSelectioner == null)
            {
                sysPrincipal.ListClients.Add(new Client(nomAjout, prenomAjout, descAjout));
                prenomAjout = "";
                nomAjout = "";
                descAjout = "";
                EnableDisableBouton();
                ValeurChangee("clients");
            }
        }

        private bool AjouterClient_CanExecute(object param)
        {
            return true;
        }
        //ajouter Participant


        //modifier Participant
        private ICommand _ModifierClient;

        public ICommand ModifierClient
        {
            get { return _ModifierClient; }
            private set { _ModifierClient = value; }
        }




        private void ModifierClient_Execute(object parSelect)
        {
            if (clientSelectionner != null)
            {
                clientSelectionner.prenom = nomAjout;
                clientSelectionner.nom = prenomAjout;
                clientSelectionner.description = descAjout;
                EnableDisableBouton();
                ValeurChangee("clients");
            }
        }

        private bool ModifierClient_CanExecute(object param)
        {
            return true;
        }


        #endregion

        #region fonctionAide
        public void setModif()
        {
            EnableDisableBouton();
            if (_clientSelectionner != null)
            {
                prenomAjout = clientSelectionner.prenom;
                nomAjout = clientSelectionner.nom;
                descAjout = clientSelectionner.description;
            }
            else
            {
                prenomAjout = "";
                nomAjout = "";
                descAjout = "";
            }

        }
        public Client GetParticipantById(string id)
        {
            foreach (Client client in _client)
            {
                if (client.randomId == id)
                {
                    return client;
                }
            }
            return null;
        }

        private void baseSelectPart()
        {
            if (clients.Count != 0)
            {
                clientSelectionner = clients[0];
            }
            else
            {
                EnableDisableBouton();
            }
        }


        public void EnableDisableBouton()
        {
            if (_clientSelectionner == null || clients.Count == 0)
            {
                boutonSupprimerState = false;
                boutonModifierState = false;
            }
            else
            {
                boutonModifierState = true;
                boutonSupprimerState = true;
            }


        }
        #endregion
        public GestionClientVM(Système sysIn)
        {
            _listFinancementMensuel = new List<Paiement>();
            sysPrincipal = sysIn;
            _client = sysPrincipal.ListClients;
            EnableDisableBouton();
            this._AjouterClient = new CommandeRelais(AjouterClient_Execute, AjouterClient_CanExecute);
            this._ModifierClient = new CommandeRelais(ModifierClient_Execute, ModifierClient_CanExecute);
            this._SupprimerClient = new CommandeRelais(SupprimerClient_Execute, SupprimerClient_CanExecute);
            this.calculerFinancement = new CommandeRelais(calculerFinancement_Execute, calculerFinancement_CanExecute);
            this._UpdateCommand = new CommandeRelais(UpdateCommand_Execute, UpdateCommand_CanExecute);
        }

        private AutoUpdateService _autoUpdateService = new AutoUpdateService();
        private string _updateMessage;
        private bool _isUpdateAvailable;

        public string UpdateMessage
        {
            get { return _updateMessage; }
            set { _updateMessage = value; ValeurChangee(nameof(UpdateMessage)); }
        }

        public bool IsUpdateAvailable
        {
            get { return _isUpdateAvailable; }
            set { _isUpdateAvailable = value; ValeurChangee(nameof(IsUpdateAvailable)); }
        }

        public async Task CheckForUpdates()
        {
            try
            {
                var updateInfo = await _autoUpdateService.CheckForUpdatesAsync();
                if (updateInfo != null)
                {
                    IsUpdateAvailable = true;
                    UpdateMessage = $"Update available: {updateInfo.TagName}";
                }
                else
                {
                    UpdateMessage = "Your application is up to date.";
                }
            }
            catch (Exception ex)
            {
                UpdateMessage = "Error checking for updates.";
            }
        }

        private ICommand _UpdateCommand;
        public ICommand UpdateCommand
        {
            get { return _UpdateCommand; }
            private set { _UpdateCommand = value; }
        }

        private void UpdateCommand_Execute(object parSelect)
        {
            CheckForUpdates();
        }








        private bool UpdateCommand_CanExecute(object param)
        {
            return true;
        }
    }
}
