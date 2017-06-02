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
using Microsoft.Win32;

namespace ImageQualityCheck.Views.QualityCheck
{
    /// <summary>
    /// Interaction logic for Histogram.xaml
    /// </summary>
    public partial class Histogram : UserControl
    {
        public Histogram()
        {
            InitializeComponent();
            histo_btn_cover.Click += histo_btn_cover_Click;
            histo_btn_stego.Click += histo_btn_stego_Click;
            histo_btn_ClearAll.Click += histo_btn_ClearAll_Click;
            histo_btn_Exec.Click += histo_btn_Exec_Click;
        }

        OpenFileDialog opendlg;

        void histo_btn_cover_Click(object sender, RoutedEventArgs e)
        {
            opendlg = new OpenFileDialog();
            opendlg.Filter = "Image Files|*.png";
            if (opendlg.ShowDialog() == true)
            {
                histo_img_cover.Source = new BitmapImage(new Uri(opendlg.FileName));
            }
            else
            {
                histo_img_cover.Source = null;
            }
        }
        
        void histo_btn_stego_Click(object sender, RoutedEventArgs e)
        {
            opendlg = new OpenFileDialog();
            opendlg.Filter = "Image Files|*.png";
            if (opendlg.ShowDialog() == true)
            {
                histo_img_stego.Source = new BitmapImage(new Uri(opendlg.FileName));
            }
            else
            {
                histo_img_stego.Source = null;
            }
        }

        void histo_btn_ClearAll_Click(object sender, RoutedEventArgs e)
        {
            histo_img_cover.Source = null;
            histo_img_stego.Source = null;
        }

        void histo_btn_Exec_Click(object sender, RoutedEventArgs e)
        {
            if (histo_img_cover.Source != null && histo_img_stego.Source != null)
            {
                FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Confirm_Exec_Process_Img, NotifyDataText.Capt_Confirm, MessageBoxButton.OK);
            }
            else
            {
                FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Err_Input_Img, NotifyDataText.Capt_Err, MessageBoxButton.OK);
            }
        }

    }
}
