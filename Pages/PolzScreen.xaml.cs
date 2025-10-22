using prak7_romanov.Classes;
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
    /// Логика взаимодействия для PolzScreen.xaml
    /// </summary>
    public partial class PolzScreen : Page
    {
        string folderPath = @"C:\Users\artur\Desktop\prak7_TRPO\bin\Debug\net8.0-windows\";
        MainViewModel viewModel;
        public PolzScreen(MainViewModel mainViewModel)
        {
            InitializeComponent();
            viewModel = mainViewModel;
            this.DataContext = viewModel;
            LoadPatients();
            if (viewModel.CurrentDoctor != null)
            {
                Console.WriteLine($"Загружен врач: {viewModel.CurrentDoctor.Name} {viewModel.CurrentDoctor.LastName}");
            }
        }
        private void LoadPatients()
        {
            var patientFiles = Directory.GetFiles(folderPath, "P_*.json");
            foreach (var file in patientFiles)
            {
               
                    string json = File.ReadAllText(file);
                    var pacient = JsonSerializer.Deserialize<Pacient>(json);
                    if (pacient != null)
                    {
                        viewModel.Patients.Add(pacient);
                    }
            }
           
        }

        private void CreatePatient_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Pages.CreatePacient(viewModel));
        }

        private void StartAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedPacient != null)
            {
                NavigationService?.Navigate(new PriemScreen(viewModel));
            }
            else
            {
                MessageBox.Show("Выберите пациента для начала приема!");
            }
        }

        private void EditPatient_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedPacient != null)
            {
                NavigationService?.Navigate(new EditPacient(viewModel));
            }
            else
            {
                MessageBox.Show("Выберите пациента для редактирования!");
            }
        }
    }
}
