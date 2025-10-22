using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace prak7_romanov.Classes
{
    public class PacientStory:INotifyPropertyChanged
    {
        string _date = "12.12.12";
        long _doctorId;
        string _diagnosis = "ОРВИ";
        string _recommendations = "Обильное питье и сон";

        public string Date
        {
            get => _date;
            set { _date = value; OnPropertyChanged(); }
        } 

        public long DoctorId
        {
            get => _doctorId;
            set { _doctorId = value; OnPropertyChanged(); }
        }

        public string Diagnosis
        {
            get => _diagnosis;
            set { _diagnosis = value; OnPropertyChanged(); }
        }

        public string Recommendations
        {
            get => _recommendations;
            set { _recommendations = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
