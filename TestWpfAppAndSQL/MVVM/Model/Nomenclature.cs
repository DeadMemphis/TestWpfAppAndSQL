using System;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace TestWpfAppAndSQL.MVVM
{
    class Nomenclature : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private decimal price;
        private DateTime dateFrom;
        private DateTime dateTo;

        //private Nomenclature nomenclature;

        //public Nomenclature(Nomenclature nomenclature)
        //{
        //    this.nomenclature = nomenclature;
        //}

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public decimal Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }

        public DateTime DateFrom
        {
            get { return dateFrom; }
            set
            {
                dateFrom = value;
                OnPropertyChanged("DateFrom");
            }
        }
        public DateTime DateTo
        {
            get { return dateTo; }
            set
            {
                dateTo = value;
                OnPropertyChanged("DateTo");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
