using prak7_romanov.Classes;
using prak7_romanov.Model;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Page
    {
        string folderPath = @"C:\Users\artur\Desktop\prak7_TRPO\bin\Debug\net8.0-windows\";
        MainViewModel viewModel;
        public LoginScreen()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            this.DataContext = viewModel;
        }

        private void GoToRegister_Click(object sender, RoutedEventArgs e)
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
                        NavigationService?.Navigate(new Pages.PolzScreen(viewModel));
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
    }
}
