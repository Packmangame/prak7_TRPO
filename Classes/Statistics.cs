using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace prak7_romanov.Classes
{
    public class Statistics : INotifyPropertyChanged
    {
        private int _totalFiles;
        private int _doctorFiles;
        private int _patientFiles;

        public int TotalFiles
        {
            get => _totalFiles;
            set { _totalFiles = value; OnPropertyChanged(); }
        }

        public int DoctorFiles
        {
            get => _doctorFiles;
            set { _doctorFiles = value; OnPropertyChanged(); }
        }

        public int PacientFiles
        {
            get => _patientFiles;
            set { _patientFiles = value; OnPropertyChanged(); }
        }

        public void StatisticsUpdate()
        {
            string folderPath = @"C:\Users\artur\Desktop\prak7_TRPO\bin\Debug\net8.0-windows\";

            if (!Directory.Exists(folderPath))
            {
                TotalFiles = 0;
                DoctorFiles = 0;
                PacientFiles = 0;
                return;
            }

            var allFiles = Directory.GetFiles(folderPath, "*.json");
            var doctorFiles = Directory.GetFiles(folderPath, "D_*.json");
            var patientFiles = Directory.GetFiles(folderPath, "P_*.json");

            TotalFiles = allFiles.Length-2;
            DoctorFiles = doctorFiles.Length;
            PacientFiles = patientFiles.Length;

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
