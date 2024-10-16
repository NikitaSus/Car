using Lesson.ViewModel;
using System.ComponentModel;
using System.Windows;
using System.Data.SqlClient;
using System;

namespace Lesson.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //User Id=<Пользователь>; Password=<Пароль>;
            InitializeComponent();
            DataContext = new CarViewModel();
            
        }
    }
}
