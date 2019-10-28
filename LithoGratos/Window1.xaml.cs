/*
 * Created by SharpDevelop.
 * User: Owner
 * Date: 10/25/2019
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using ImageMagick;

namespace LithoGratos
{
    public class LGsettings
    {
        public string L1default { get; set; }
        public string L2default { get; set; }
        public string L3default { get; set; }
        public string L4default { get; set; }
        public string LOGOdefault { get; set; }
        public string fontPath { get; set; }
    }
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
        void button1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fdPole = new OpenFileDialog();
            fdPole.Filter = "JPEG files (*.jpg or *.jpeg)|*.jpg;*.jpeg";
            if (fdPole.ShowDialog() == true) {
                poleLoc.Text = fdPole.FileName;
                pole.Source = new BitmapImage(new Uri(fdPole.FileName));
            }
        }
        void settings_Click(object sender, RoutedEventArgs e)
        {
            WindowSet settingsd = new WindowSet();
            settingsd.ShowDialog();
        }
        void about_Click(object sender, RoutedEventArgs e)
        {
            if (bdrAbout.Visibility == Visibility.Collapsed) {
                bdrAbout.Visibility = Visibility.Visible;
            } else {
                bdrAbout.Visibility = Visibility.Collapsed;
            }
        }
        void magic_Click(object sender, RoutedEventArgs e)
        {
            string strOutput = poleLoc.Text.Insert(poleLoc.Text.Length - 5, "(1)");
            using (MagickImage image = new MagickImage(poleLoc.Text)){
                new Drawables()
                    .FontPointSize(100)
                    .Font("Tahoma")
                    .TextUnderColor(new MagickColor("#CCCCCCA0"))
                    .FillColor(new MagickColor("black"))
                    .Text(10,100, lineOne.Text + "\n" + lineTwo.Text + "\n" + lineThree.Text + "\n" + lineFour.Text)
                    .Draw(image);
                new Drawables().FillColor(new MagickColor("white"))
                    .Rectangle(0,3381,750,3781);
// Place logo code here followed by .Draw(image);
                image.Write(strOutput);
            }
        }
    }
}