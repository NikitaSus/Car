using Lesson.ViewModel;
using System.ComponentModel;
using System.Windows;

namespace Lesson.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new CarViewModel();
        }
    }
}
