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
    /// Логика взаимодействия для PriemScreen.xaml
    /// </summary>
    public partial class PriemScreen : Page
    {
        string folderPath = @"C:\Users\artur\Desktop\prak7_TRPO\bin\Debug\net8.0-windows\";
        MainViewModel viewModel;
       

        public PriemScreen(MainViewModel mainViewModel)
        {
            InitializeComponent();
            viewModel = mainViewModel;
            this.DataContext = viewModel;
            viewModel.CurrentPacientStory = new PacientStory();
        }

        
        private void SaveAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedPacient != null)
            {
                
                viewModel.CurrentPacientStory.DoctorId = viewModel.CurrentDoctor.Id;
                if (string.IsNullOrEmpty(viewModel.CurrentPacientStory.Date))
                {
                    viewModel.CurrentPacientStory.Date = DateTime.Now.ToString("dd.MM.yyyy");
                }

                // Добавляем прием в историю
                viewModel.SelectedPacient.PacientStories.Add(viewModel.CurrentPacientStory);

                // Сохраняем пациента
                string json = JsonSerializer.Serialize(viewModel.SelectedPacient);
                string filePath = System.IO.Path.Combine(folderPath, $"P_{viewModel.SelectedPacient.Id:D7}.json");
                File.WriteAllText(filePath, json);

                MessageBox.Show("Прием сохранен!");

                // Создаем новый объект для следующего приема
                viewModel.CurrentPacientStory = new PacientStory();
            }
            else
            {
                MessageBox.Show("Пациент не выбран!");
            }
        }

        private void EditPatient_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new EditPacient(viewModel));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}
