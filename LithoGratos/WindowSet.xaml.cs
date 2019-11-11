/*
 * Created by SharpDevelop.
 * User: Owner
 * Date: 10/27/2019
 * 
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace LithoGratos
{
    /// <summary>
    /// Interaction logic for WindowSet.xaml
    /// </summary>
    public partial class WindowSet : Window
    {
        public WindowSet()
        {
            InitializeComponent();
            if (File.Exists("LithoGratosSettings.xml")) {
                XmlSerializer serializer = new XmlSerializer(typeof(LGsettings));
                using (Stream reader = new FileStream("LithoGratosSettings.xml", FileMode.Open)) {
                    LGsettings setList = (LGsettings)serializer.Deserialize(reader);
                    setLineOne.Text = setList.L1default;
                    setLineTwo.Text = setList.L2default;
                    setLineThree.Text = setList.L3default;
                    setLineFour.Text = setList.L4default;
                    setLineFive.Text = setList.L5default;
                    setLineDT.Text = setList.DTdefault;
                    setFontOne.Text = setList.L1font;
                    setFontTwo.Text = setList.L2font;
                    setFontThree.Text = setList.L3font;
                    setFontFour.Text = setList.L4font;
                    setFontFive.Text = setList.L5font;
                    setFontDT.Text = setList.DTfont;
                    
                    setFonts.Text = setList.fontPath;
                }
            }
        }
        void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        void btnSave_Click(object sender, RoutedEventArgs e)
        {
            LGsettings setList = new LGsettings{ };
            setList.L1default = setLineOne.Text;
            setList.L2default = setLineTwo.Text;
            setList.L3default = setLineThree.Text;
            setList.L4default = setLineFour.Text;
            setList.L5default = setLineFive.Text;
            setList.DTdefault = setLineDT.Text;
            setList.L1font = setFontOne.Text;
            setList.L2font = setFontTwo.Text;
            setList.L3font = setFontThree.Text;
            setList.L4font = setFontFour.Text;
            setList.L5font = setFontFive.Text;
            setList.DTfont = setFontDT.Text;
            setList.fontPath = setFonts.Text;
            XmlSerializer serializer = new XmlSerializer(typeof(LGsettings));
            using (Stream writer = new FileStream("LithoGratosSettings.xml", FileMode.Create)) {
                serializer.Serialize(writer, setList);
            }
        }
    }
}