﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for EditPolygon.xaml
    /// </summary>
    public partial class EditPolygon : Window
    {
        public PointCollection listaPointa = new PointCollection();

        public EditPolygon(double stariUnos, string bojaStara, string bojaTeksta, string samTekst, PointCollection points)
        {
            InitializeComponent();

            cmbColor.ItemsSource = typeof(Colors).GetProperties();
            cmbColor2.ItemsSource = typeof(Colors).GetProperties();

            debljinaKonturneLinije.Text = stariUnos.ToString();
            cmbColor.SelectedIndex = 5;

            int l = -1;
            while (true)
            {
                l++;
                cmbColor.SelectedIndex = l;
                var nekaBoja = cmbColor.SelectedValue; //dobije ime boje
                string plz = nekaBoja.ToString();
                plz = plz.Substring(27, plz.Length - 27);

                var dobioSamVrednost = new SolidColorBrush((Color)ColorConverter.ConvertFromString(plz));
                if (dobioSamVrednost.ToString() == bojaStara.ToString())
                {
                    break;
                }
                else if (l == 500)
                {
                    break;
                }
            }


            //textBlock
            int o = -1;
            while (true)
            {
                o++;
                cmbColor2.SelectedIndex = o;
                var nekaBoja = cmbColor2.SelectedValue; //dobije ime boje
                string plz = nekaBoja.ToString();
                plz = plz.Substring(27, plz.Length - 27);

                var dobioSamVrednost = new SolidColorBrush((Color)ColorConverter.ConvertFromString(plz));
                if (dobioSamVrednost.ToString() == bojaTeksta.ToString())
                {
                    break;
                }
                else if (o == 500)
                {
                    break;
                }
            }
            tekstUnutarPoligona.Text = samTekst;

            //tacke poligona
            for (int i = 0; i < points.Count; i++)
            {
                listaPointa = points;
            }
        }

        private void NacrtajPoligon_Click(object sender, RoutedEventArgs e)
        {
            double debljinaKonturneLinijeee = 1, LineThicknessParse;
            if (validate())
            {
                if (!double.TryParse(debljinaKonturneLinije.Text, out LineThicknessParse)) { }
                debljinaKonturneLinijeee = LineThicknessParse;

                Polygon polygon = new Polygon();
                polygon.StrokeThickness = debljinaKonturneLinijeee;
                Canvas.SetZIndex(polygon, 3);

                string boja = cmbColor.SelectedItem.ToString();
                // dobijem System.Windows.Media.Color Blue -> 27 viska
                boja = boja.Substring(27, boja.Length - 27);
                //tekstUnutarElipse.Text = boja;
                polygon.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(boja));
                //polygon.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(boja));
                polygon.Stroke = Brushes.Black;

                //Koordinate poligona
                double koordinataX = 0, koordinataY = 0;
                double poXmin = 0, poXmax = 0, poYmin = 0, poYmax = 0, prolaz = 0;
                for (int i = 0; i < listaPointa.Count; i++)
                {
                    polygon.Points.Add(listaPointa[i]);
                    koordinataX = listaPointa[i].X;
                    koordinataY = listaPointa[i].Y;

                    if (prolaz == 0)
                    {
                        poXmax = koordinataX;
                        poXmin = koordinataX;
                        poYmax = koordinataY;
                        poYmin = koordinataY;
                        prolaz++;
                    }
                    else
                    {
                        //CheckXY X
                        if (koordinataX > poXmax)
                        {
                            poXmax = koordinataX;
                        }

                        if (koordinataX < poXmin)
                        {
                            poXmin = koordinataX;
                        }

                        //CheckXY Y 
                        if (koordinataY > poYmax)
                        {
                            poYmax = koordinataY;
                        }

                        if (koordinataY < poYmin)
                        {
                            poYmin = koordinataY;
                        }
                    }
                }


                //ODELJUJEM IME zbog ono poslednjeg zahteva za edit
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                var stringChars = new char[8];
                var random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                var finalString = new String(stringChars);
                polygon.Name = finalString;
                //kraj

                //tekst i boja unutar poligona
                TextBlock prosledjujemTekst = new TextBlock();
                prosledjujemTekst.Margin = new Thickness(koordinataX - (poXmax - poXmin) / 2, koordinataY - (poYmax - poYmin) / 2, 0, 0);
                prosledjujemTekst.FontSize = 10;
                //sada ce tekst biti na elipsi jer sam stavio na 4 layer
                Canvas.SetZIndex(prosledjujemTekst, 4);

                string tekstZaPoligon = "";
                if (tekstUnutarPoligona.Text.Length != 0)
                {
                    tekstZaPoligon = tekstUnutarPoligona.ToString();
                    tekstZaPoligon = tekstZaPoligon.Substring(33, tekstZaPoligon.Length - 33);
                }

                if (cmbColor2.SelectedItem != null)
                {
                    string boja2 = cmbColor2.SelectedItem.ToString();
                    boja2 = boja2.Substring(27, boja2.Length - 27);
                    prosledjujemTekst.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(boja2));
                }
                //Prosledjivanje teksta
                prosledjujemTekst.Text = tekstZaPoligon;
                prosledjujemTekst.Name = finalString + "pgtb";

                ((MainWindow)Application.Current.MainWindow).canvas.Children.Add(prosledjujemTekst);
                ((MainWindow)Application.Current.MainWindow).canvas.Children.Add(polygon);


                ((MainWindow)Application.Current.MainWindow).koordinatePoX.Clear();
                ((MainWindow)Application.Current.MainWindow).koordinatePoY.Clear();
                this.Close();
            }
            else
            {
                MessageBox.Show("Popunite sva obavezna polja", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private bool validate()
        {
            bool result = true;

            //Debljina konturne linije
            double LineThicknessParse;

            if (debljinaKonturneLinije.Text.Trim().Equals(""))
            {
                result = false;
                debljinaKonturneLinije.BorderBrush = Brushes.Red;
                debljinaKonturneLinije.BorderThickness = new Thickness(1);
                lblKonturnaLinijaGreska.Content = "Ne moze biti prazno";
            }
            else if (debljinaKonturneLinije.Text.Length > 10)
            {
                result = false;
                debljinaKonturneLinije.BorderBrush = Brushes.Red;
                debljinaKonturneLinije.BorderThickness = new Thickness(1);
                lblKonturnaLinijaGreska.Content = "Morate uneti manji broj";
            }
            else if (!double.TryParse(debljinaKonturneLinije.Text, out LineThicknessParse))
            {
                result = false;
                debljinaKonturneLinije.BorderBrush = Brushes.Red;
                debljinaKonturneLinije.BorderThickness = new Thickness(1);
                lblKonturnaLinijaGreska.Content = "Unesite broj bez karaktera";
            }
            else if (LineThicknessParse <= 0)
            {
                result = false;
                debljinaKonturneLinije.BorderBrush = Brushes.Red;
                debljinaKonturneLinije.BorderThickness = new Thickness(1);
                lblKonturnaLinijaGreska.Content = "Mora biti veci od 0";
            }
            else
            {
                debljinaKonturneLinije.BorderBrush = Brushes.Green;
                lblKonturnaLinijaGreska.Content = string.Empty;
            }

            //boja
            if (cmbColor.SelectedItem == null)
            {
                result = false;
                cmbColor.BorderBrush = Brushes.Red;
                cmbColor.BorderThickness = new Thickness(1);
                lblcmdGreska.Content = "Morate izabrati boju";
            }
            else
            {
                cmbColor.BorderBrush = Brushes.Green;
                lblcmdGreska.Content = string.Empty;
            }

            return result;
        }

        private void cmbColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbColor.SelectedItem != null)
            {
                try
                {
                    var izabrana = (PropertyInfo)cmbColor.SelectedItem;
                    var boja = (Color)izabrana.GetValue(null, null);
                }
                catch (Exception)
                {
                    MessageBox.Show("Boja nije izabrana", "Greska!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
            }
        }
    }
}
