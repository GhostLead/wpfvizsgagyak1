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

namespace wpfvizsgagyak1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Esemeny> esemenyek = new List<Esemeny>();
        public MainWindow()
        {
            InitializeComponent();
            cbGorduloListaInit();
            LabelInit();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                esemenyek = File.ReadAllLines(filePath)
                    .Select(line => new Esemeny(line))
                    .ToList();
                dgTablazat.ItemsSource = esemenyek;
            }

            LabelFill(esemenyek);

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretné a táblázat tartalmát?", "Törlés megerősítése", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                dgTablazat.ItemsSource = null;
                dgTablazat.Items.Clear();
                LabelInit();
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
            List<Esemeny> szurtEsemenyek = new List<Esemeny>();
            string esemenyFajta = cbGorduloLista.SelectedItem.ToString();
            foreach (var esemeny in esemenyek)
            {
                if (esemeny.EsemenyKod == esemenyFajta)
                {
                    szurtEsemenyek.Add(esemeny);
                }
            }
            dgTablazat.ItemsSource = szurtEsemenyek;

            LabelFill(szurtEsemenyek);

        }

        private void cbGorduloListaInit()
        {
            cbGorduloLista.Items.Add("Belépés");
            cbGorduloLista.Items.Add("Kilépés");
            cbGorduloLista.Items.Add("Ebéd");
            cbGorduloLista.Items.Add("Könyvtár");
        }

        public void LabelInit()
        {
            lblBelepes.Content = "Belépés: 0";
            lblKilepes.Content = "Kilépés: 0";
            lblEbed.Content = "Ebéd: 0";
            lblKonyvtar.Content = "Könyvtár: 0";
        }

        void LabelFill(List<Esemeny> list)
        {
            lblBelepes.Content = "Belépés: " + list.Count(x => x.EsemenyKod == "Belépés");
            lblKilepes.Content = "Kilépés: " + list.Count(x => x.EsemenyKod == "Kilépés");
            lblEbed.Content = "Ebéd: " + list.Count(x => x.EsemenyKod == "Ebéd");
            lblKonyvtar.Content = "Könyvtár: " + list.Count(x => x.EsemenyKod == "Könyvtár");
        }
    }
}