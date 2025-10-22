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
    /// Логика взаимодействия для EditPacient.xaml
    /// </summary>
    public partial class EditPacient : Page
    {
        private string folderPath = @"C:\Users\artur\Desktop\prak7_TRPO\bin\Debug\net8.0-windows\";
        MainViewModel viewModel;
        public EditPacient(MainViewModel mainViewModel)
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            this.DataContext = viewModel;
        }

        private void SavePatient_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedPacient != null)
            {
                string json = JsonSerializer.Serialize(viewModel.SelectedPacient);
                System.IO.File.WriteAllText($"{folderPath}P_{viewModel.SelectedPacient.Id:D7}.json", json);
                MessageBox.Show("Изменения сохранены!");
                NavigationService?.GoBack();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void DeletePatient_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedPacient != null)
            {
                var result = MessageBox.Show("Вы действительно хотите удалить пациента?",
                    "Подтверждение удаления",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    string filePath = $"{folderPath}P_{viewModel.SelectedPacient.Id:D7}.json";
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                        viewModel.Patients.Remove(viewModel.SelectedPacient);
                        MessageBox.Show("Пациент удален!");
                        NavigationService?.GoBack();
                    }
                }
            }
        }

        private void AddAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedPacient != null)
            {
                var appointment = new PacientStory();
                viewModel.SelectedPacient.PacientStories.Add(appointment);
            }
        }

        //Создать приемы!!!
        private void EditAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is PacientStory appointment)
            {
                
                MessageBox.Show("Редактирование приема");
            }
        }
    }
}
