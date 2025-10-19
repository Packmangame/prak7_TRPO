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
    /// Логика взаимодействия для RegisterScreen.xaml
    /// </summary>
    public partial class RegisterScreen : Page
    {
        string folderPath = @"C:\Users\artur\Desktop\prak7_TRPO\bin\Debug\net8.0-windows\";
        MainViewModel viewModel;
        public RegisterScreen()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            this.DataContext = viewModel;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (Password1.Password != Password2.Password)
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }
            viewModel.CurrentDoctor.Password = Password1.Password;
            WriteAndGenerateDoctorID();
            viewModel.Statistics.StatisticsUpdate();
            NavigationService?.Navigate(new LoginScreen());
        }

        private void WriteAndGenerateDoctorID()
        {

            if (!System.IO.File.Exists($"{folderPath}D_00001.json"))
            {
                viewModel.CurrentDoctor.Id = 1;
            }
            else
            {
                var latestFile = Directory.GetFiles(folderPath, "D_*.json")
                .OrderByDescending(f => new FileInfo(f).CreationTime)
                .FirstOrDefault();
                if (latestFile != null)
                {
                    string fileName = System.IO.Path.GetFileName(latestFile);
                    fileName = fileName.Substring(2, fileName.IndexOf(".") - 2);
                    viewModel.CurrentDoctor.Id = Convert.ToInt32(fileName);
                    viewModel.CurrentDoctor.Id++;

                }
                else
                {
                    viewModel.CurrentDoctor.Id = 1;
                }

            }
            string json = JsonSerializer.Serialize(viewModel.CurrentDoctor);
            System.IO.File.WriteAllText($"D_{viewModel.CurrentDoctor.Id.ToString("D5")}.json", json);
            MessageBox.Show($"Врач зарегистрирован! ID: {viewModel.CurrentDoctor.Id}");

        }

    }
}
