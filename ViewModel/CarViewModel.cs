using Lesson.Command;
using Lesson.Data;
using Lesson.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace Lesson.ViewModel
{
    public class CarViewModel : INotifyPropertyChanged
    {
        private CarModel _newCar;

        public CarModel NewCar
        {
            get => _newCar;
            set
            {
                if (_newCar != value)
                {
                    _newCar = value;
                    OnPropertyChanged(nameof(NewCar));
                }
            }
        }

        /// <summary>
        /// Коллекция машин для DataGrid, так же Cars нужен для биндинга
        /// </summary>
        public ObservableCollection<CarModel> Cars { get; set; }
        const string _connectionString = "Server=192.168.1.165,1433; Database=CarsShop; User Id=user; Password=password123456; TrustServerCertificate=True; Encrypt=False;";

        // Создаем команды для кнопок
        public ICommand ButtonAddCarCommand { get; }
        public ICommand ButtonClearCarNameCommand { get; }

        public CarViewModel()
        {
            NewCar = new CarModel();
            Cars = new ObservableCollection<CarModel>();
            ButtonAddCarCommand = new RelayCommand(AddCarеToDatabaseButton);
            ButtonClearCarNameCommand = new RelayCommand(DeleteCarNameClick);
        }

        /// <summary>
        /// Добавляем машину в БД
        /// </summary>
        public void AddCarеToDatabaseButton()
        {
            try
            {
                using (DbConnectionManager manager = new DbConnectionManager(_connectionString))
                {
                    
                    manager.OpenConnection();

                    // Запрос в БД по добавление данных
                    string query = "INSERT INTO Cars (Name, Country, Model) VALUES (@Name, @Country, @Model)";

                    using (SqlCommand command = new SqlCommand(query, manager.Connection))
                    {
                        command.Parameters.AddWithValue("@Name", NewCar.Name);
                        command.Parameters.AddWithValue("@Model", NewCar.Model);
                        command.Parameters.AddWithValue("@Country", NewCar.Country);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            // Сообщение о том, что автомобиль добавлен
                            MessageBox.Show("Тачанка добавлена!");
                        }
                        else
                        {
                            MessageBox.Show("Это повозка, а не машина.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Удалить машину из списка (Удаляется последняя)
        /// </summary>
        public void DeleteCarNameClick()
        {
            Cars.RemoveAt(Cars.Count - 1);
        }

        /// <summary>
        /// 
        /// </summary>
        public void ButtonExportCars()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
