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

        #region for Variable & Initialize
        string getPathCover;
        string getPathStego;
        string Detect_BitDepth;
        double[] result_mse;
        double[] result_psnr;

        DataProcess dp = new DataProcess();
        OpenFileDialog opendlg;
        #endregion

        #region for Button Event Handler
        void MSE_btn_cover_Click(object sender, RoutedEventArgs e)
        {
            opendlg = new OpenFileDialog();
            opendlg.Filter = "Image Files|*.png";
            if (opendlg.ShowDialog() == true)
            {
                getPathCover = opendlg.FileName;
                Detect_BitDepth = dp.View_BitDepth(getPathCover);
                if (Detect_BitDepth == "Format32bppArgb")
                {
                    txt_cover.Text = String.Format("Cover Image ({0})", opendlg.SafeFileName);
                    mse_img_cover.Source = new BitmapImage(new Uri(getPathCover));
                }
                else
                {
                    FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Err_Input_32bitDepth, NotifyDataText.Capt_Err, MessageBoxButton.OK);
                }
            }
            else
            {
                txt_cover.Text = "Cover Image";
                mse_img_cover.Source = null;
            }
        }
        
        void MSE_btn_stego_Click(object sender, RoutedEventArgs e)
        {
            opendlg = new OpenFileDialog();
            opendlg.Filter = "Image Files|*.png";
            if (opendlg.ShowDialog() == true)
            {
                getPathStego = opendlg.FileName;
                Detect_BitDepth = dp.View_BitDepth(getPathStego);
                if (Detect_BitDepth == "Format32bppArgb")
                {
                    txt_stego.Text = String.Format("Stego Image ({0})", opendlg.SafeFileName);
                    mse_img_stego.Source = new BitmapImage(new Uri(getPathStego));
                }
                else
                {
                    FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Err_Input_32bitDepth, NotifyDataText.Capt_Err, MessageBoxButton.OK);
                }
            }
            else
            {
                txt_cover.Text = "Stego Image";
                mse_img_stego.Source = null;
            }
        }

        void MSE_btn_ClearAll_Click(object sender, RoutedEventArgs e)
        {
            mse_img_cover.Source = null;
            mse_img_stego.Source = null;

            txt_cover.Text = "Cover Image";
            txt_stego.Text = "Stego Image";

            mse_R.Text = "MSE | Red: -";
            mse_G.Text = "MSE | Green: -";
            mse_B.Text = "MSE | Blue: -";
            mse_A.Text = "MSE | Alpha: -";
            mse_All.Text = "MSE | All: -";

            psnr_R.Text = "PSNR | Red: -";
            psnr_G.Text = "PSNR | Green: -";
            psnr_B.Text = "PSNR | Blue: -";
            psnr_A.Text = "PSNR | Alpha: -";
            psnr_All.Text = "PSNR | All: -";

            FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Clear_Input_Img, NotifyDataText.Capt_Success, MessageBoxButton.OK);
        }

        void MSE_btn_Exec_Click(object sender, RoutedEventArgs e)
        {
            if (mse_img_cover.Source != null && mse_img_stego.Source != null)
            {
                if (FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Confirm_Exec_Process_Img, NotifyDataText.Capt_Confirm, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    Exec_Process();
                    FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Process_Complete_MSEandPSNR, NotifyDataText.Capt_Success, MessageBoxButton.OK);
                }
            }
            else
            {
                FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Err_Input_Img, NotifyDataText.Capt_Err, MessageBoxButton.OK);
            }
        }
        #endregion

        #region for Other function to handle Event
        void Exec_Process()
        {
            dp.LookupPixel(getPathCover, getPathStego);
            dp.SetPixel_ARGB(dp.stride_cover, dp.stride_stego);
            result_mse = dp.count_MSE(dp.MSEandPSNR_Red_cover, dp.MSEandPSNR_Red_stego, dp.MSEandPSNR_Green_cover, dp.MSEandPSNR_Green_stego, dp.MSEandPSNR_Blue_cover, dp.MSEandPSNR_Blue_stego, dp.MSEandPSNR_Alpha_cover, dp.MSEandPSNR_Alpha_stego);
            result_psnr = dp.count_PSNR(result_mse);
            setText_Result();
        }

        void setText_Result()
        {
            mse_R.Text = String.Format("MSE | Red: {0}", result_mse[0]);
            mse_G.Text = String.Format("MSE | Green: {0}", result_mse[1]);
            mse_B.Text = String.Format("MSE | Blue: {0}", result_mse[2]);
            mse_A.Text = String.Format("MSE | Alpha: {0}", result_mse[3]);
            mse_All.Text = String.Format("MSE | All: {0}", result_mse[4]);

            psnr_R.Text = String.Format("PSNR | Red: {0} dB", result_psnr[0]);
            psnr_G.Text = String.Format("PSNR | Green: {0} dB", result_psnr[1]);
            psnr_B.Text = String.Format("PSNR | Blue: {0} dB", result_psnr[2]);
            psnr_A.Text = String.Format("PSNR | Alpha: {0} dB", result_psnr[3]);
            psnr_All.Text = String.Format("PSNR | All: {0} dB", result_psnr[4]);
        }
        #endregion

    }
}
