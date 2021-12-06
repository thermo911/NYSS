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
using System.Windows.Shapes;
using Parser.Entities;

namespace Parser
{
    /// <summary>
    /// Interaction logic for DiffWindow.xaml
    /// </summary>
    public partial class DiffWindow : Window
    { 
        public DiffWindow(List<CyberDangerInfo> infos)
        {
            InitializeComponent();
            ListBox.ItemsSource = infos;
            TextBlock.Text = $"Количество записей: {infos.Count}";
        }

        private void ListBox_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var info = ListBox.SelectedItem as CyberDangerInfo;
            var infoWindow = new DangerInfoWindow(info);
            infoWindow.Show();
        }
    }
}
