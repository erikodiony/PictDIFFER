using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using Microsoft.Win32;
using AForge.Imaging;
using System.Collections.Generic;


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

        #region for Variable & Initialize
        string getPathCover;
        string getPathStego;
        string Detect_BitDepth;

        DataProcess dp = new DataProcess();
        OpenFileDialog opendlg;

        public PointCollection Blue_Point_Cover;
        public PointCollection Green_Point_Cover;
        public PointCollection Red_Point_Cover;
        public PointCollection Alpha_Point_Cover;

        public PointCollection Blue_Point_Stego;
        public PointCollection Green_Point_Stego;
        public PointCollection Red_Point_Stego;
        public PointCollection Alpha_Point_Stego;
        #endregion

        #region for Button Event Handler
        void histo_btn_cover_Click(object sender, RoutedEventArgs e)
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
                    histo_img_cover.Source = new BitmapImage(new Uri(getPathCover));
                }
                else
                {
                    FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Err_Input_32bitDepth, NotifyDataText.Capt_Err, MessageBoxButton.OK);
                }
            }
            else
            {
                txt_cover.Text = "Cover Image";
                histo_img_cover.Source = null;
            }
        }
        
        void histo_btn_stego_Click(object sender, RoutedEventArgs e)
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
                    histo_img_stego.Source = new BitmapImage(new Uri(getPathStego));
                }
                else
                {
                    FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Err_Input_32bitDepth, NotifyDataText.Capt_Err, MessageBoxButton.OK);
                }
            }
            else
            {
                txt_stego.Text = "Stego Image";
                histo_img_stego.Source = null;
            }
        }

        void histo_btn_ClearAll_Click(object sender, RoutedEventArgs e)
        {
            histo_img_cover.Source = null;
            histo_img_stego.Source = null;

            txt_cover.Text = "Cover Image";
            txt_stego.Text = "Stego Image";

            blue_cover.Points = null;
            green_cover.Points = null;
            red_cover.Points = null;
            alpha_cover.Points = null;

            blue_stego.Points = null;
            green_stego.Points = null;
            red_stego.Points = null;
            alpha_stego.Points = null;

            FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Clear_Input_Img, NotifyDataText.Capt_Success, MessageBoxButton.OK);
        }

        void histo_btn_Exec_Click(object sender, RoutedEventArgs e)
        {
            if (histo_img_cover.Source != null && histo_img_stego.Source != null)
            {
                if (FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Confirm_Exec_Process_Img, NotifyDataText.Capt_Confirm, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    Exec_Process();
                    FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Process_Complete_Histogram, NotifyDataText.Capt_Success, MessageBoxButton.OK);
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
            using (System.Drawing.Bitmap bmp_cover = new System.Drawing.Bitmap(getPathCover))
            {
                //Alpha
                ImageStatisticsHSL alpha_Statistics_Cover = new ImageStatisticsHSL(bmp_cover);
                Alpha_Point_Cover = dp.Exec_Histo(alpha_Statistics_Cover.Luminance.Values);

                //BGR
                ImageStatistics rgb_Statistics_Cover = new ImageStatistics(bmp_cover);
                Blue_Point_Cover = dp.Exec_Histo(rgb_Statistics_Cover.Blue.Values);
                Green_Point_Cover = dp.Exec_Histo(rgb_Statistics_Cover.Green.Values);
                Red_Point_Cover = dp.Exec_Histo(rgb_Statistics_Cover.Red.Values);

                //Set Histogram Cover
                blue_cover.Points = Blue_Point_Cover;
                green_cover.Points = Green_Point_Cover;
                red_cover.Points = Red_Point_Cover;
                alpha_cover.Points = Alpha_Point_Cover;
            }

            using (System.Drawing.Bitmap bmp_stego = new System.Drawing.Bitmap(getPathStego))
            {
                //Alpha
                ImageStatisticsHSL alpha_Statistics_Stego = new ImageStatisticsHSL(bmp_stego);
                Alpha_Point_Stego = dp.Exec_Histo(alpha_Statistics_Stego.Luminance.Values);

                //BGR
                ImageStatistics rgb_Statistics_Stego = new ImageStatistics(bmp_stego);
                Blue_Point_Stego = dp.Exec_Histo(rgb_Statistics_Stego.Blue.Values);
                Green_Point_Stego = dp.Exec_Histo(rgb_Statistics_Stego.Green.Values);
                Red_Point_Stego = dp.Exec_Histo(rgb_Statistics_Stego.Red.Values);

                //Set Histogram Stego
                blue_stego.Points = Blue_Point_Stego;
                green_stego.Points = Green_Point_Stego;
                red_stego.Points = Red_Point_Stego;
                alpha_stego.Points = Alpha_Point_Stego;
            }

        }
        #endregion
    }
}
