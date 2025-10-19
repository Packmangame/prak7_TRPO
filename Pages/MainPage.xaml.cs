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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        string folderPath = @"C:\Users\artur\Desktop\prak7_TRPO\bin\Debug\net8.0-windows\";
        MainViewModel viewModel;
        public MainPage()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            this.DataContext = viewModel;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new RegisterScreen());
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (long.TryParse(viewModel.LoginId, out long doctorId))
            {
                string fullPath = $"{folderPath}D_{doctorId:D5}.json";

                if (System.IO.File.Exists(fullPath))
                {
                    string json = System.IO.File.ReadAllText(fullPath);
                    var doctor = JsonSerializer.Deserialize<Doctor>(json);

                    if (doctor.Password == viewModel.LoginPassword)
                    {
                        viewModel.CurrentDoctor = doctor;
                        MessageBox.Show($"Добро пожаловать, {doctor.Name} {doctor.LastName} {doctor.MiddleName}!");
                    }
                    else
                    {
                        MessageBox.Show("Неверный пароль!");
                    }
                }
                else
                {
                    MessageBox.Show("Врач с таким ID не найден!");
                }
            }
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (long.TryParse(viewModel.SearchPacientId, out long pacientId))
            {
                string fullPath = $"{folderPath}P_{pacientId.ToString("D7")}.json";

                if (System.IO.File.Exists(fullPath))
                {
                    string json = System.IO.File.ReadAllText(fullPath);
                    var pacient = JsonSerializer.Deserialize<Pacient>(json);
                    viewModel.CurrentPacient = pacient;
                    MessageBox.Show("Пациент найден!");
                }
                else
                {
                    MessageBox.Show("Пациент не найден!");
                }
            }
        }
        private void AddPacient_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(viewModel.CurrentPacient.Name) ||
            string.IsNullOrEmpty(viewModel.CurrentPacient.LastName))
            {
                MessageBox.Show("Заполните обязательные поля: Имя и Фамилия!");
                return;
            }
            WriteAndGeneratePacientID();
            viewModel.Statistics.StatisticsUpdate();
            viewModel.CurrentPacient = new Pacient();
        }
        private void SavePacient_Click(object sender, RoutedEventArgs e)
        {
            if (!System.IO.File.Exists($"{folderPath}P_0000001.json"))
            {
                viewModel.CurrentPacient.Id = 1;
            }
            string json = JsonSerializer.Serialize(viewModel.CurrentPacient);
            System.IO.File.WriteAllText($"P_{viewModel.CurrentPacient.Id.ToString("D7")}.json", json);
            MessageBox.Show("Изменения сохранены!");

        }
        private void ResetPacient_Click(object sender, RoutedEventArgs e)
        {

            if (viewModel.CurrentPacient.Id > 0)
            {

                string fullPath = $"{folderPath}P_{viewModel.CurrentPacient.Id.ToString("D7")}.json";

                if (System.IO.File.Exists(fullPath))
                {
                    string json = System.IO.File.ReadAllText(fullPath);
                    var originalPacient = JsonSerializer.Deserialize<Pacient>(json);
                    viewModel.CurrentPacient = originalPacient;
                    MessageBox.Show("Данные сброшены!");
                }
            }
        }
       
        private void WriteAndGeneratePacientID()
        {
            var patientFiles = Directory.GetFiles(folderPath, "P_*.json");

            if (patientFiles.Length == 0)
            {
                viewModel.CurrentPacient.Id = 1;
            }
            else
            {
                long maxId = patientFiles
        .Select(file => System.IO.Path.GetFileNameWithoutExtension(file))
        .Where(name => name.StartsWith("P_"))
        .Select(name => long.Parse(name.Substring(2)))
        .Max();

                viewModel.CurrentPacient.Id = maxId + 1;
            }
            string json = JsonSerializer.Serialize(viewModel.CurrentPacient);
            System.IO.File.WriteAllText($"P_{viewModel.CurrentPacient.Id.ToString("D7")}.json", json);
            MessageBox.Show($"Пациент добавлен! ID: {viewModel.CurrentPacient.Id}");
        }
    }
}
