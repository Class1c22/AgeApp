using System;
using System.ComponentModel;
using System.Windows;

namespace BirthdayZodiacApp.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private DateTime _birthDate = DateTime.Today;
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                OnPropertyChanged(nameof(BirthDate));
                CalculateInfo();
            }
        }

        private string _age;
        public string Age
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        private string _westernZodiac;
        public string WesternZodiac
        {
            get => _westernZodiac;
            set
            {
                _westernZodiac = value;
                OnPropertyChanged(nameof(WesternZodiac));
            }
        }

        private string _chineseZodiac;
        public string ChineseZodiac
        {
            get => _chineseZodiac;
            set
            {
                _chineseZodiac = value;
                OnPropertyChanged(nameof(ChineseZodiac));
            }
        }

        private void CalculateInfo()
        {
            var today = DateTime.Today;
            int years = today.Year - BirthDate.Year;
            if (BirthDate > today.AddYears(-years)) years--;

            if (years < 0 || years > 135)
            {
                MessageBox.Show("Некоректний вік. Будь ласка, перевірте дату народження.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                Age = "";
                WesternZodiac = "";
                ChineseZodiac = "";
                return;
            }

            Age = years.ToString();

            if (BirthDate.Day == today.Day && BirthDate.Month == today.Month)
            {
                MessageBox.Show("Вітаю з днем народження!", "Привітання", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            WesternZodiac = GetWesternZodiac(BirthDate);
            ChineseZodiac = GetChineseZodiac(BirthDate.Year);
        }

        private string GetWesternZodiac(DateTime date)
        {
            int day = date.Day;
            int month = date.Month;

            return month switch
            {
                1 => (day <= 19) ? "Козоріг" : "Водолій",
                2 => (day <= 18) ? "Водолій" : "Риби",
                3 => (day <= 20) ? "Риби" : "Овен",
                4 => (day <= 19) ? "Овен" : "Телець",
                5 => (day <= 20) ? "Телець" : "Близнюки",
                6 => (day <= 20) ? "Близнюки" : "Рак",
                7 => (day <= 22) ? "Рак" : "Лев",
                8 => (day <= 22) ? "Лев" : "Діва",
                9 => (day <= 22) ? "Діва" : "Терези",
                10 => (day <= 22) ? "Терези" : "Скорпіон",
                11 => (day <= 21) ? "Скорпіон" : "Стрілець",
                12 => (day <= 21) ? "Стрілець" : "Козоріг",
                _ => ""
            };
        }

        private string GetChineseZodiac(int year)
        {
            string[] zodiac = {
        "Миша", "Бик", "Тигр", "Кролик", "Дракон", "Змія",
        "Кінь", "Коза", "Мавпа", "Півень", "Собака", "Свиня"
    };

            int baseYear = 2008;
            int index = (year - baseYear) % 12;
            if (index < 0) index += 12;

            return zodiac[index];
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
