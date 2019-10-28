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
            if(File.Exists("LithoGratosSettings.xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<LGsettings>));
                using (Stream reader = new FileStream("LithoGratosSettings.xml", FileMode.Open)) {
                    List<LGsettings> setList = (List<LGsettings>)serializer.Deserialize(reader); 
                }
            }
        }
    }
}