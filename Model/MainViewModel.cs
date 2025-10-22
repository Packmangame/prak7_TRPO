using prak7_romanov.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace prak7_romanov.Model
{
    public class MainViewModel: INotifyPropertyChanged
    {
        Doctor _currentDoctor;
        Statistics _statistics;
        Pacient _currentPacient;
        Pacient _selectedPacient;
        PacientStory _currrntPacientStory;
        string _loginId;
        string _loginPassword;
        string _searchPacientId;


        public Doctor CurrentDoctor
        {
            get => _currentDoctor;
            set { _currentDoctor = value; OnPropertyChanged(); }
        }

        public Statistics Statistics
        {
            get => _statistics;
            set { _statistics = value; OnPropertyChanged(); }
        }

        public Pacient CurrentPacient
        {
            get => _currentPacient;
            set { _currentPacient = value; OnPropertyChanged(); }
        }

        public Pacient SelectedPacient
        {
            get => _selectedPacient;
            set { _selectedPacient = value; OnPropertyChanged(); }
        }

        public PacientStory CurrentPacientStory
        {
            get => _currrntPacientStory;
            set {_currrntPacientStory=value; OnPropertyChanged();}    
            
        }

        public ObservableCollection<Pacient> Patients { get; set; } = new ObservableCollection<Pacient>();

        public string LoginId
        {
            get => _loginId;
            set { _loginId = value; OnPropertyChanged(); }
        }
        

        public string LoginPassword
        {
            get => _loginPassword;
            set { _loginPassword = value; OnPropertyChanged(); }
        }

        public string SearchPacientId
        {
            get => _searchPacientId;
            set { _searchPacientId = value; OnPropertyChanged(); }
        }
        public MainViewModel()
        {
            CurrentDoctor = new Doctor();
            Statistics = new Statistics();
            CurrentPacient = new Pacient();
            CurrentPacientStory = new PacientStory();
            Statistics.StatisticsUpdate();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
