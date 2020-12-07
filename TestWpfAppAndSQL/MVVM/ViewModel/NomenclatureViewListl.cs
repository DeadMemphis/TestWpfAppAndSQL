using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TestWpfAppAndSQL.Data;

namespace TestWpfAppAndSQL.MVVM
{
    public class NomenclatureViewList : INotifyPropertyChanged
    {
        private Nomenclature selectedNomenclature;
        private SQLManager sqlComponent = SQLManager.GetInstance();

        public ObservableCollection<Nomenclature> Nomenclatures { get; set; }

        public NomenclatureViewList()
        {
            Nomenclatures = sqlComponent.LoadData();
        }

        public void Load()
        {
            Nomenclatures = sqlComponent.LoadData();
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
