using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Reflection;
using System.Xml.Linq;
using System.Collections.ObjectModel;

namespace orai0310
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public partial class MainWindow : Window
    {

        string fajlNev = "naplo.csv";
        List<Osztalyzatok> jegyek = new List<Osztalyzatok>();
       
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            if (ofd.ShowDialog() == true)
            {
                fajlNev = ofd.FileName;
                string dirPath = System.IO.Path.GetDirectoryName(fajlNev);
                string fullPath = System.IO.Path.Combine(dirPath, fajlNev); 
                lblPath.Content = fullPath;
            }
            else
            {
                fajlNev = "naplo.csv";
                string dirPath = System.IO.Path.GetDirectoryName(fajlNev);
                string fullPath = System.IO.Path.Combine(dirPath, fajlNev);
                lblPath.Content = fullPath;
            }
           

        }

        private void OsztalyzatokBetoltese(string fajlNev)
        {
            


            jegyek.Clear();
            StreamReader sr = new StreamReader(fajlNev);
            while (!sr.EndOfStream)
            {
                string[] mezok = sr.ReadLine().Split(";");

                string fullName = mezok[0];
                var names = fullName.Split(' ');
                string firstName = names[0];
                string lastName = names[1];

                Osztalyzatok ujJegy = new Osztalyzatok(mezok[0], mezok[1], mezok[2], int.Parse(mezok[3]), names[0]);

                jegyek.Add(ujJegy);
            }
            sr.Close();
            dgJegyek.ItemsSource = jegyek;
            dgJegyek.Items.Refresh();
            MessageBox.Show("Az állomány beolvasása befejeződött!");

            
        }

        private void sliJegy_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblErtek.Content = sliJegy.Value;
        }

        private void btnRogzit_Click(object sender, RoutedEventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            DateTime selectedTime;

            string name = txtNev.Text;

            string[] words = name.Split(' ');

            bool isValid = true;

            if (words.Length < 2)
            {
                isValid = false;
            }
            else
            {
                foreach (string word in words)
                {
                    if (word.Length < 3)
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            if (isValid)
            {
                if (DateTime.TryParse(datDatum.Text, out selectedTime))
                {
                    if (selectedTime > currentTime)
                    {
                        MessageBox.Show("Kérlek adj meg valós dátumot!");
                        datDatum.Text = DateTime.Now.ToString("d");
                    }
                    else
                    {
                        string selectedDateString = selectedTime.ToShortDateString();

                        var save = ($"{txtNev.Text};{selectedDateString};{cboTantargy.Text};{sliJegy.Value}");

                        StreamWriter sw = new StreamWriter(fajlNev, append: true);
                        sw.WriteLine(save);
                        sw.Close();
                        dgJegyek.Items.Refresh();
                        MessageBox.Show("Az adatok sikeresen rögzítve lettek.");
                    }
                }
                else
                {
                    MessageBox.Show("Nem megfelelő dátum formátum!");
                }
            }
            else
            {
                MessageBox.Show("Nem megfelelő név. Kérlek olyan nevet adj meg ami legalább 2 szó," +
                    " és mindegyik szó legalább 3 karakter hosszú.");
            }
        }

        private void btnTolt_Click(object sender, RoutedEventArgs e)
        {
            OsztalyzatokBetoltese(fajlNev);
            lblJegyC.Content = dgJegyek.Items.Count;
            lblAtlag.Content = jegyek.Average(x => x.Jegy);
            
        }

        private void rdoVezToKer_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rdoKerToVez_Checked(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
