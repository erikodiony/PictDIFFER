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
    /// Interaction logic for PixelLookup.xaml
    /// </summary>
    public partial class PixelLookup : UserControl
    {
        public PixelLookup()
        {
            InitializeComponent();

            pixel_btn_cover.Click += pixel_btn_cover_Click;
            pixel_btn_stego.Click += pixel_btn_stego_Click;
            pixel_btn_ClearAll.Click += pixel_btn_ClearAll_Click;
            pixel_btn_Exec.Click += pixel_btn_Exec_Click;
        }

        OpenFileDialog opendlg;

        void pixel_btn_cover_Click(object sender, RoutedEventArgs e)
        {
            opendlg = new OpenFileDialog();
            opendlg.Filter = "Image Files|*.png";
            if (opendlg.ShowDialog() == true)
            {
                pixel_img_cover.Source = new BitmapImage(new Uri(opendlg.FileName));
            }
            else
            {
                pixel_img_cover.Source = null;
            }
        }

        void pixel_btn_stego_Click(object sender, RoutedEventArgs e)
        {
            opendlg = new OpenFileDialog();
            opendlg.Filter = "Image Files|*.png";
            if (opendlg.ShowDialog() == true)
            {
                pixel_img_stego.Source = new BitmapImage(new Uri(opendlg.FileName));
            }
            else
            {
                pixel_img_stego.Source = null;
            }
        }

        void pixel_btn_ClearAll_Click(object sender, RoutedEventArgs e)
        {
            pixel_img_cover.Source = null;
            pixel_img_stego.Source = null;
        }

        void pixel_btn_Exec_Click(object sender, RoutedEventArgs e)
        {
            if (pixel_img_cover.Source != null && pixel_img_stego.Source != null)
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
