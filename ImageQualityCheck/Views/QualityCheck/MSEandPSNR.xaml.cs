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
    /// Interaction logic for MSEandPSNR.xaml
    /// </summary>
    public partial class MSEandPSNR : UserControl
    {
        public MSEandPSNR()
        {
            InitializeComponent();
            MSE_btn_cover.Click += MSE_btn_cover_Click;
            MSE_btn_stego.Click += MSE_btn_stego_Click;
            MSE_btn_ClearAll.Click += MSE_btn_ClearAll_Click;
            MSE_btn_Exec.Click += MSE_btn_Exec_Click;
        }

        OpenFileDialog opendlg;

        void MSE_btn_cover_Click(object sender, RoutedEventArgs e)
        {
            opendlg = new OpenFileDialog();
            opendlg.Filter = "Image Files|*.png";
            if (opendlg.ShowDialog() == true)
            {
                mse_img_cover.Source = new BitmapImage(new Uri(opendlg.FileName));
            }
            else
            {
                mse_img_cover.Source = null;
            }
        }
        
        void MSE_btn_stego_Click(object sender, RoutedEventArgs e)
        {
            opendlg = new OpenFileDialog();
            opendlg.Filter = "Image Files|*.png";
            if (opendlg.ShowDialog() == true)
            {
                mse_img_stego.Source = new BitmapImage(new Uri(opendlg.FileName));
            }
            else
            {
                mse_img_stego.Source = null;
            }
        }

        void MSE_btn_ClearAll_Click(object sender, RoutedEventArgs e)
        {
            mse_img_cover.Source = null;
            mse_img_stego.Source = null;
        }

        void MSE_btn_Exec_Click(object sender, RoutedEventArgs e)
        {
            if (mse_img_cover.Source != null && mse_img_stego.Source != null)
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
