using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TestWpfAppAndSQL.Data;

namespace TestWpfAppAndSQL.MVVM
{
    class NomenclatureViewModel : INotifyPropertyChanged
    {
        private Nomenclature selectedNomenclature;
        private Command add;
        private SQLManager sqlComponent = SQLManager.GetInstance();

        public ObservableCollection<Nomenclature> Nomenclatures { get; set; }

        public NomenclatureViewModel()
        {
            Nomenclatures = sqlComponent.LoadData();
        }

        public void Load()
        {
            Nomenclatures = sqlComponent.LoadData();
        }

        public Command Add
        {
            get
            {
                return add ??
                  (add = new Command(obj =>
                  {
                      Nomenclature nomenclature = new Nomenclature();
                      sqlComponent.Add(nomenclature);
                  }));
            }
        }

        public Nomenclature SelectedNomenclature
        {
            get { return selectedNomenclature; }
            set
            {
                selectedNomenclature = value;
                OnPropertyChanged("SelectedNomenclature");
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
