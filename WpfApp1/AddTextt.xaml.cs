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
    /// Interaction logic for AddText.xaml
    /// </summary>
    public partial class AddText : Window
    {
        public AddText()
        {
            InitializeComponent();
            cmbFontSize.ItemsSource = new List<double>() { 2, 3, 4, 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            cmbColor.ItemsSource = typeof(Colors).GetProperties();
        }

        private void NacrtajTekst(object sender, RoutedEventArgs e)
        {
            if (Vaalidate())
            {
                // pozicija misa
                double left = ((MainWindow)Application.Current.MainWindow).poX;
                double top = ((MainWindow)Application.Current.MainWindow).poY;

                //tekst i boja unutar elipse
                TextBlock prosledjujemTekst = new TextBlock();
                prosledjujemTekst.Margin = new Thickness(left, top, 0, 0);
                Canvas.SetZIndex(prosledjujemTekst, 4);

                //Dodeljujem ime zbog ono poslednjeg zahteva za edit
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                var stringChars = new char[8];
                var random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                var finalString = new String(stringChars);
                prosledjujemTekst.Name = finalString;
                //kraj

                string ispisTeksta = "";
                if (txtUnos.ToString() != "")
                {
                    ispisTeksta = txtUnos.ToString();
                    ispisTeksta = ispisTeksta.Substring(33, ispisTeksta.Length - 33);
                }
                if (cmbColor.SelectedItem != null)
                {
                    string boja2 = cmbColor.SelectedItem.ToString();
                    boja2 = boja2.Substring(27, boja2.Length - 27);
                    prosledjujemTekst.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(boja2));
                }
                if (cmbFontSize.SelectedItem != null)
                {
                    int velicinaFonta=2;
                    string fontic = cmbFontSize.SelectedItem.ToString();
                    try
                    {
                        Int32.TryParse(fontic, out velicinaFonta);
                    }
                    catch(Exception excep)
                    {
                        MessageBox.Show(excep.ToString(),"Greska", MessageBoxButton.OK);
                    }
                    prosledjujemTekst.FontSize = velicinaFonta;
                    
                }
                //Prosledjivanje teksta
                prosledjujemTekst.Text = ispisTeksta;

                ((MainWindow)Application.Current.MainWindow).canvas.Children.Add(prosledjujemTekst);

                this.Close();
            }
            else
            {
                MessageBox.Show("Fill in all the mandatory fields", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private bool Vaalidate()
        {
            bool result = true;

            //tekst
            if (txtUnos.Text.Trim().Equals(""))
            {
                result = false;
                txtUnos.BorderBrush = Brushes.Red;
                txtUnos.BorderThickness = new Thickness(1);
                lblTekstGreska.Content = "Cannot be empty";
            }
            else if (txtUnos.Text.Length > 30)
            {
                result = false;
                txtUnos.BorderBrush = Brushes.Red;
                txtUnos.BorderThickness = new Thickness(1);
                lblTekstGreska.Content = "Text is too big";
            }
            else
            {
                txtUnos.BorderBrush = Brushes.Green;
                lblTekstGreska.Content = string.Empty;
            }

            //boja
            if (cmbColor.SelectedItem == null)
            {
                result = false;
                cmbColor.BorderBrush = Brushes.Red;
                cmbColor.BorderThickness = new Thickness(1);
                lblcmbBojaGreska.Content = "Color must be selected";
            }
            else
            {
                cmbColor.BorderBrush = Brushes.Green;
                lblcmbBojaGreska.Content = string.Empty;
            }

            //velicina slova
            if (cmbFontSize.SelectedItem == null)
            {
                result = false;
                cmbFontSize.BorderBrush = Brushes.Red;
                cmbFontSize.BorderThickness = new Thickness(1);
                lblcmbVelicinaGreska.Content = "You need to select the font";
            }
            else
            {
                cmbFontSize.BorderBrush = Brushes.Green;
                lblcmbVelicinaGreska.Content = string.Empty;
            }

            return result;
        } 

        private void CmbColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                    MessageBox.Show("You need to select a color", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        
        private void CmbFontSize_Changed(object sender, TextChangedEventArgs e)
        {
            if (cmbFontSize.SelectedItem != null)
            {
                 try
                 {
                    
                 }
                catch (Exception)
                 {
                    MessageBox.Show("Choose font size", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                 }
            }
        } 
    }
}
