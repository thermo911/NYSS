using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Interaction logic for DangerInfoWindow.xaml
    /// </summary>
    public partial class DangerInfoWindow : Window
    {
        private CyberDangerInfo _info;

        public DangerInfoWindow(CyberDangerInfo info)
        {
            InitializeComponent();

            _info = info;
            Title = _info.ToString();
            SetupMainGrid();
        }

        private void SetupMainGrid()
        {
            Id.Text = _info.Id.ToString();
            Name.Text = _info.Name;
            Description.Text = _info.Description;
            Source.Text = _info.Source;
            Target.Text = _info.Target;
            ConfViolation.Text = _info.ConfidentialityViolation ? "Присутствует" : "Отсутствует";
            IntViolation.Text = _info.IntegrityViolation ? "Присутствует" : "Отсутствует";
            AvaViolation.Text = _info.AvailabilityViolation ? "Присутствует" : "Отсутствует";
        }
    }
}
