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
using System.IO;
using System.Xml.Serialization;


namespace LithoGratos
{
    public class LGsetProfile
    {
        public string ProfileName { get; set; }
        public bool active { get; set; }
        public LGsettings values { get; set; }
    }
    public class LGsettings
    {
        public string L1default { get; set; }
        public string L2default { get; set; }
        public string L3default { get; set; }
        public string L4default { get; set; }
        public string L5default { get; set; }
        public string DTdefault { get; set; }
        public string L1font { get; set; }
        public string L2font { get; set; }
        public string L3font { get; set; }
        public string L4font { get; set; }
        public string L5font { get; set; }
        public string DTfont { get; set; }
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
            if (File.Exists("LithoGratosSettings.xml")) {
                XmlSerializer serializer = new XmlSerializer(typeof(LGsettings));
                using (Stream reader = new FileStream("LithoGratosSettings.xml", FileMode.Open)) {
                    LGsettings setList = (LGsettings)serializer.Deserialize(reader);
                    lineOne.Text = setList.L1default;
                    lineTwo.Text = setList.L2default;
                    lineThree.Text = setList.L3default;
                    lineFour.Text = setList.L4default;
                    lineFive.Text = setList.L5default;
                    lineDT.Text = setList.DTdefault;
                    fontOne.Text = setList.L1font;
                    fontTwo.Text = setList.L2font;
                    fontThree.Text = setList.L3font;
                    fontFour.Text = setList.L4font;
                    fontFive.Text = setList.L5font;
                    fontDT.Text = setList.DTfont;
                    
                    //setFonts.Text = setList.fontPath;
                }
            }
        }
        void button1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fdPole = new OpenFileDialog();
            fdPole.Filter = "JPEG files (*.jpg or *.jpeg)|*.jpg;*.jpeg";
            if (fdPole.ShowDialog() == true) {
                poleLoc.Text = fdPole.FileName;
                pole.Source = new BitmapImage(new Uri(fdPole.FileName));
                ExifProfile edata = new MagickImage(fdPole.FileName).GetExifProfile();
                if (edata != null) {
                    string date = edata.GetValue(ExifTag.DateTimeOriginal).ToString();
                    lineDT.Text = date.Substring(5, 2) + "-" + date.Substring(8, 2) + "-" + date.Substring(0, 4);
                }
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
                LicTXT.Text = File.ReadAllText("LICENSE");
            } else {
                bdrAbout.Visibility = Visibility.Collapsed;
            }
        }
        void magic_Click(object sender, RoutedEventArgs e)
        {
            string strOutput = poleLoc.Text.Insert(poleLoc.Text.Length - 5, "(1)");
            Double mF1 = Convert.ToDouble(fontOne.Text);
            Double mF2 = Convert.ToDouble(fontTwo.Text);
            Double mF3 = Convert.ToDouble(fontThree.Text);
            Double mF4 = Convert.ToDouble(fontFour.Text);
            Double mF5 = Convert.ToDouble(fontFive.Text);
            Double mFDT = Convert.ToDouble(fontDT.Text);
            using (MagickImage image = new MagickImage(poleLoc.Text)) {
                new Drawables()
                    .Font("Tahoma")
                    .TextUnderColor(new MagickColor("#CCCCCCA0"))
                    .FillColor(new MagickColor("black"))
                    .FontPointSize(mF1)
                    .Text(10, mF1, lineOne.Text)
                    .FontPointSize(mF2)
                    .Text(10, (mF1 *= 1.2) + mF2, lineTwo.Text)
                    .FontPointSize(mF3)
                    .Text(10, mF1 + (mF2 *= 1.2) + mF3, lineThree.Text)
                    .FontPointSize(mF4)
                    .Text(10, mF1 + mF2 + (mF3 *= 1.2) + mF4, lineFour.Text)
                    .FontPointSize(mF5)
                    .Text(10, mF1 + mF2 + mF3 + (mF4 *= 1.2) + mF5, lineFive.Text)
                    .FontPointSize(mFDT)
                    .Text(110, 3381 - mFDT, lineDT.Text)
                    .Draw(image);
                new Drawables()
                    .FillColor(new MagickColor("white"))
                    .Rectangle(0, 3381, 750, 3781);
// Place logo code here followed by .Draw(image);
                    
                image.Write(strOutput);
            }
        }
    }
}