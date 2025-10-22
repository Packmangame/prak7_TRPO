using prak7_romanov.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace prak7_romanov.Pages
{
    /// <summary>
    /// Логика взаимодействия для CreatePacient.xaml
    /// </summary>
    public partial class CreatePacient : Page
    {
        private string folderPath = @"C:\Users\artur\Desktop\prak7_TRPO\bin\Debug\net8.0-windows\";
        private MainViewModel viewModel;
        public CreatePacient(MainViewModel mainViewModel)
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            this.DataContext = viewModel;
            viewModel.CurrentPacient = new Classes.Pacient();
        }

        private void SavePatient_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(viewModel.CurrentPacient.Name) ||
                string.IsNullOrEmpty(viewModel.CurrentPacient.LastName))
            {
                MessageBox.Show("Заполните обязательные поля: Имя и Фамилия!");
                return;
            }

            WriteAndGeneratePacientID();
            viewModel.Statistics.StatisticsUpdate();
            NavigationService?.GoBack();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void WriteAndGeneratePacientID()
        {
            var patientFiles = Directory.GetFiles(folderPath, "P_*.json");
            long newId = 1;

            if (patientFiles.Length > 0)
            {
                long maxId = 0;
                foreach (var file in patientFiles)
                {
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(file);
                    if (fileName.StartsWith("P_") && long.TryParse(fileName.Substring(2), out long fileId))
                    {
                        if (fileId > maxId) maxId = fileId;
                    }
                }
                newId = maxId + 1;
            }

            viewModel.CurrentPacient.Id = newId;

            string json = JsonSerializer.Serialize(viewModel.CurrentPacient);
            string filePath = System.IO.Path.Combine(folderPath, $"P_{newId:D7}.json");
            File.WriteAllText(filePath, json);

            
            viewModel.Patients.Add(viewModel.CurrentPacient);

            MessageBox.Show($"Пациент добавлен! ID: {viewModel.CurrentPacient.Id}");
            NavigationService?.GoBack();
        }
    }
}
