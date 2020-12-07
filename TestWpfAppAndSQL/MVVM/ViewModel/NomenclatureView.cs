using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TestWpfAppAndSQL.Data;

namespace TestWpfAppAndSQL.MVVM
{
    public class NomenclatureView : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private decimal price;
        private DateTime dateFrom;
        private DateTime dateTo;

        private Command edit;
        private Command delete;
        private Command add;

        private Nomenclature nomenclature;

        private SQLManager sqlComponent = SQLManager.GetInstance();

        public NomenclatureView(Nomenclature nomenclature)
        {
            this.nomenclature = nomenclature;
        }

        public NomenclatureView()
        {

        }

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

        public Command Add
        {
            get
            {
                return add ??
                  (add = new Command(obj =>
                  {
                      if (nomenclature != null) sqlComponent.Add(nomenclature);
                  }));
            }
        }

        public Command Edit
        {
            get
            {
                return edit ??
                  (edit = new Command(obj =>
                  {
                      Nomenclature nomenclature = obj as Nomenclature;
                      if (nomenclature != null)
                      {
                          sqlComponent.Edit(nomenclature);
                      }
                  }));
            }
        }

        public Command Delete
        {
            get
            {
                return delete ??
                  (delete = new Command(obj =>
                  {
                      Nomenclature nomenclature = obj as Nomenclature;
                      if (nomenclature != null)
                      {
                          sqlComponent.Delete(nomenclature.Id);
                      }
                  }));
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
