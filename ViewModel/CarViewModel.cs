using Lesson.Command;
using Lesson.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Packaging;
using System.Windows.Input;

namespace Lesson.ViewModel
{
    public class CarViewModel : INotifyPropertyChanged
    {
        public CarModel _carName;

        // Коллекция машин для DataGrid, так же Cars нужен для биндинга
        public ObservableCollection<CarModel> Cars { get; set; }

        // Создаем команды для кнопок
        public ICommand ButtonClickCommand { get; }
        public ICommand ButtonClearCarNameCommand { get; }
        public string CarName
        {
            get => _carName.NameCar;
            set
            {
                _carName.NameCar = value;
                OnPropertyChanged(nameof(CarName));
            }
        }

        public string Model
        {
            get => _carName.Model;
            set
            {
                _carName.Model = value;
                OnPropertyChanged(nameof(Model));
            }
        }

        public string Country
        {
            get => _carName.Country;
            set
            {
                _carName.Country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        public CarViewModel()
        {
            _carName = new CarModel { NameCar = string.Empty };
            Cars = new ObservableCollection<CarModel>();
            ButtonClickCommand = new RelayCommand(OnButtonClick);
            ButtonClearCarNameCommand = new RelayCommand(DeleteCarNameClick);
        }

        public void OnButtonClick()
        {
            // Добавляем новую машину в коллекцию
            var newCar = new CarModel
            {
                NameCar = CarName,
                Model = Model,
                Country = Country
            };

            Cars.Add(newCar); // Добавляем в ObservableCollection
            
            //Занулим данные в текстбоксе
            CarName = string.Empty;
            Model = string.Empty;
            Country = string.Empty;
        }

        /// <summary>
        /// 
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
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
