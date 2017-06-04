using System;
using System.ComponentModel;
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
            btn_displayData.Click += btn_displayData_Click;
            btn_displayData_Point.Click += btn_displayData_Point_Click;
        }

        #region for Variable & Initialize
        string getPathCover;
        string getPathStego;
        string Detect_BitDepth;

        List<Point_Cover> dgrid_cover_list = new List<Point_Cover>();
        List<Point_Stego> dgrid_stego_list = new List<Point_Stego>();

        DataProcess dp = new DataProcess();
        OpenFileDialog opendlg;
        #endregion

        #region for Clear Variable/Property
        void clear_DataGrid()
        {
            dgrid_cover.ItemsSource = null;
            dgrid_stego.ItemsSource = null;
            dgrid_cover_list.Clear();
            dgrid_stego_list.Clear();
        }

        void clear_Img()
        {
            pixel_img_cover.Source = null;
            pixel_img_stego.Source = null;
        }

        void clearAndSetDefault()
        {
            clear_Img();
            clear_DataGrid();
            txt_cover.Text = "Cover Image";
            txt_stego.Text = "Stego Image";
            txt_displayData.Text = "25";
            txt_displayData_X.Text = "1";
            txt_displayData_Y.Text = "1";
            infoDisplayData_cover.Text = "Total of Data (Cover Image) : -";
            infoDisplayData_stego.Text = "Total of Data (Stego Image) : -";
        }
        #endregion

        #region for Button Event Handler
        void pixel_btn_cover_Click(object sender, RoutedEventArgs e)
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
                    pixel_img_cover.Source = new BitmapImage(new Uri(getPathCover));
                }
                else
                {
                    FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Err_Input_32bitDepth, NotifyDataText.Capt_Err, MessageBoxButton.OK);
                }
            }
            else
            {
                txt_cover.Text = "Cover Image";
                pixel_img_cover.Source = null;
            }
        }

        void pixel_btn_stego_Click(object sender, RoutedEventArgs e)
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
                    pixel_img_stego.Source = new BitmapImage(new Uri(getPathStego));
                }
                else
                {
                    FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Err_Input_32bitDepth, NotifyDataText.Capt_Err, MessageBoxButton.OK);
                }
            }
            else
            {
                txt_stego.Text = "Stego Image";
                pixel_img_stego.Source = null;
            }
        }

        void pixel_btn_ClearAll_Click(object sender, RoutedEventArgs e)
        {
            clearAndSetDefault();
            FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Clear_Input_Img, NotifyDataText.Capt_Success, MessageBoxButton.OK);
        }

        void pixel_btn_Exec_Click(object sender, RoutedEventArgs e)
        {
            if (pixel_img_cover.Source != null && pixel_img_stego.Source != null)
            {
                if (FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Confirm_Exec_Process_Img, NotifyDataText.Capt_Confirm, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    Exec_Process();
                    FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Process_Complete_PixelLookup, NotifyDataText.Capt_Success, MessageBoxButton.OK);
                }
            }
            else
            {
                FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Err_Input_Img, NotifyDataText.Capt_Err, MessageBoxButton.OK);
            }
        }
        #endregion

        #region for View Data By Limit
        void btn_displayData_Click(object sender, RoutedEventArgs e)
        {
            if (dgrid_cover.ItemsSource != null || dgrid_stego.ItemsSource != null)
            {
                if (txt_displayData.Text == String.Empty || int.Parse(txt_displayData.Text) <= 0)
                {
                    FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Err_Input_DisplayLimit, NotifyDataText.Capt_Err, MessageBoxButton.OK);
                }
                else
                {
                    if (int.Parse(txt_displayData.Text) > 100)
                    {
                        if (FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Notify_PixelLookup_MemoryLeak, NotifyDataText.Capt_Confirm, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                        {
                            clear_DataGrid();
                            Generate_List_Pixel();
                            dgrid_cover.ItemsSource = dgrid_cover_list.Take(int.Parse(txt_displayData.Text));
                            dgrid_stego.ItemsSource = dgrid_stego_list.Take(int.Parse(txt_displayData.Text));
                            FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Process_Complete_PixelLookup, NotifyDataText.Capt_Success, MessageBoxButton.OK);
                        }
                    }
                    else
                    {
                        clear_DataGrid();
                        Generate_List_Pixel();
                        dgrid_cover.ItemsSource = dgrid_cover_list.Take(int.Parse(txt_displayData.Text));
                        dgrid_stego.ItemsSource = dgrid_stego_list.Take(int.Parse(txt_displayData.Text));
                        FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Process_Complete_PixelLookup, NotifyDataText.Capt_Success, MessageBoxButton.OK);
                    }

                }
            }
            else
            {
                FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Err_PixelLookup_DisplayLimit, NotifyDataText.Capt_Err, MessageBoxButton.OK);
            }
        }
        #endregion

        #region for View Data with PointXY
        void btn_displayData_Point_Click(object sender, RoutedEventArgs e)
        {
            int x_point = int.Parse(txt_displayData_X.Text);
            int y_point = int.Parse(txt_displayData_Y.Text);
            if (txt_displayData_X.Text == String.Empty || txt_displayData_Y.Text == String.Empty || x_point <= 0 || y_point <= 0)
            {
                FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Err_Input_DisplayLimit, NotifyDataText.Capt_Err, MessageBoxButton.OK);
            }
            else
            {
                if (dgrid_cover.ItemsSource != null && dgrid_stego.ItemsSource != null)
                {
                    if ((dp.bmp_cover_raw.PixelWidth < x_point == true) || (dp.bmp_cover_raw.PixelHeight < y_point == true) || (dp.bmp_stego_raw.PixelWidth < x_point == true) || (dp.bmp_stego_raw.PixelHeight < y_point == true))
                    {
                        FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Err_Input_DisplayLimit, NotifyDataText.Capt_Err, MessageBoxButton.OK);
                    }
                    else
                    {
                        clear_DataGrid();
                        dp.GetPositionXY(x_point, y_point, (dp.bmp_cover_raw.PixelWidth * 4), (dp.bmp_stego_raw.PixelWidth * 4));
                        dgrid_cover.ItemsSource = dp.dgrid_cover_list2;
                        dgrid_stego.ItemsSource = dp.dgrid_stego_list2;

                        FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Process_Complete_PixelLookup, NotifyDataText.Capt_Success, MessageBoxButton.OK);
                    }
                }
                else
                {
                    FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Err_PixelLookup_DisplayLimit, NotifyDataText.Capt_Err, MessageBoxButton.OK);
                }
            }
        }
        #endregion

        #region for Other function to handle Event
        void Exec_Process()
        {
            Generate_List_Pixel();
            infoDisplayData_cover.Text = String.Format("Total of Data (Cover Image) : {0} ({1}x{2})", dp.PointXY_Cover.Length, dp.bmp_cover_raw.PixelWidth, dp.bmp_cover_raw.PixelHeight);
            infoDisplayData_stego.Text = String.Format("Total of Data (Stego Image) : {0} ({1}x{2})", dp.PointXY_Stego.Length, dp.bmp_stego_raw.PixelWidth, dp.bmp_stego_raw.PixelHeight); 
            dgrid_cover.ItemsSource = dgrid_cover_list.Take(25);
            dgrid_stego.ItemsSource = dgrid_stego_list.Take(25);
        }

        void Generate_List_Pixel()
        {
            dp.LookupPixel(getPathCover, getPathStego);

            for (int i = 0; i < dp.PointXY_Cover.Length; i++)
            {
                dgrid_cover_list.Add(new Point_Cover() { PointXY_Cover = dp.PointXY_Cover[i], B_Cover = dp.Blue_Cover[i], G_Cover = dp.Green_Cover[i], R_Cover = dp.Red_Cover[i], A_Cover = dp.Alpha_Cover[i] });
            }

            dgrid_stego_list = new List<Point_Stego>();
            for (int i = 0; i < dp.PointXY_Stego.Length; i++)
            {
                dgrid_stego_list.Add(new Point_Stego() { PointXY_Stego = dp.PointXY_Stego[i], B_Stego = dp.Blue_Stego[i], G_Stego = dp.Green_Stego[i], R_Stego = dp.Red_Stego[i], A_Stego = dp.Alpha_Stego[i] });
            }
        }
        #endregion

        #region for Data Binding
        public class Point_Cover
        {
            public string PointXY_Cover { get; set; }
            public string B_Cover { get; set; }
            public string G_Cover { get; set; }
            public string R_Cover { get; set; }
            public string A_Cover { get; set; }
        }

        public class Point_Stego
        {
            public string PointXY_Stego { get; set; }
            public string B_Stego { get; set; }
            public string G_Stego { get; set; }
            public string R_Stego { get; set; }
            public string A_Stego { get; set; }
        }
        #endregion

    }
}
