using prak7_romanov.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        MainViewModel viewModel;
        public PolzScreen(long id)
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            this.DataContext = viewModel;
        }
    }
}
