using System.Windows;
using TestWpfAppAndSQL.MVVM;


namespace TestWpfAppAndSQL
{
    /// <summary>
    /// Interaction logic for DeleteModal.xaml
    /// </summary>
    public partial class DeleteModal : Window
    {
        private Nomenclature nomenclatureToDelete;
        public NomenclatureViewList Model { get; set; }

        public Nomenclature NomenclatureToDelete
        {
            get { return nomenclatureToDelete; }
            set
            {
                nomenclatureToDelete = value;
            }
        }

        public DeleteModal()
        {
            InitializeComponent();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            NomenclatureView nomView = new NomenclatureView(NomenclatureToDelete);
            nomView.Delete.Execute(nomenclatureToDelete);
            this.DialogResult = true;

        }

        private void CanсelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
