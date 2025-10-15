using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace prak7_romanov
{
    internal class MainViewModel: INotifyPropertyChanged
    {
        Doctor _currentDoctor;
        Statistics _statistics;
        Pacient _currentPacient;
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
            Statistics.StatisticsUpdate();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
