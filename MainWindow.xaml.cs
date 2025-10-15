using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.WebRequestMethods;

namespace prak7_romanov
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string folderPath = @"C:\Users\artur\Desktop\prak7_TRPO\bin\Debug\net8.0-windows\";
        MainViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = (MainViewModel)DataContext;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (Password1.Password !=Password2.Password)
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }
            viewModel.CurrentDoctor.Password = Password1.Password;
            WriteAndGenerateDoctorID();
            viewModel.Statistics.StatisticsUpdate();
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
                string fullPath = $"{folderPath}P_{ pacientId.ToString("D7")}.json";

                if (System.IO.  File.Exists(fullPath))
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