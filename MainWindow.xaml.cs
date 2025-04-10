using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace vizsgaWPFgyak1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            cbGorduloListaInit();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                List<Esemeny> esemenyek = File.ReadAllLines(filePath)
                    .Select(line => new Esemeny(line))
                    .ToList();
                dgTablazat.ItemsSource = esemenyek;
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretné a táblázat tartalmát?", "Törlés megerősítése", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                dgTablazat.ItemsSource = null;
                dgTablazat.Items.Clear();
            }
            else
            {
                MessageBox.Show("A törlés megszakítva.", "Törlés megszakítva", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnSaveTo_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                List<Esemeny> esemenyek = (List<Esemeny>)dgTablazat.ItemsSource;
                StringBuilder sb = new StringBuilder();
                foreach (var esemeny in esemenyek)
                {
                    sb.AppendLine($"{esemeny.TanuloKod} {esemeny.EsemenyIdopont} {esemeny.EsemenyKod}");
                }
                File.WriteAllText(filePath, sb.ToString());
            }
            MessageBox.Show("A fájl mentése sikerült.", "Mentés", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgTablazat.SelectedIndex != -1)
            {
                List<Esemeny> esemenyek = (List<Esemeny>)dgTablazat.ItemsSource;
                Esemeny selectedEsemeny = (Esemeny)dgTablazat.SelectedItem;
                esemenyek.Remove(selectedEsemeny);
                dgTablazat.ItemsSource = null;
                dgTablazat.ItemsSource = esemenyek;
            }
            else
            {
                MessageBox.Show("Kérjük, válasszon ki egy sort a törléshez.", "Nincs kiválasztva", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void cbGorduloLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EsemenyKezelo esemenyKezelo = new EsemenyKezelo();
            dgTablazat.ItemsSource = esemenyKezelo.GetEsemenyek(int.Parse(cbGorduloLista.SelectedItem.ToString()));
        }

        private void cbGorduloListaInit()
        {
            cbGorduloLista.Items.Add("1");
            cbGorduloLista.Items.Add("2");
            cbGorduloLista.Items.Add("3");
            cbGorduloLista.Items.Add("4");
        }
    }
}