using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Imaging;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using PictDIFFER.Views.QualityCheck;
using Microsoft.Win32;
using System.Security.Cryptography;

namespace PictDIFFER
{
    internal static class NotifyDataText
    {
        #region for handle caption MessageBox
        public static readonly string Capt_Err = "Error";
        public static readonly string Capt_Confirm = "Confirmation";
        public static readonly string Capt_Success = "Success";
        #endregion

        #region for Other Notification/Message
        public static readonly string Err_Input_Img = "Some field is empty ! Please check again...";
        public static readonly string Clear_Input_Img = "All field was Cleared ! Process Successfully...";
        public static readonly string Confirm_Exec_Process_Img = "Confirm to Execute ? Click 'OK' to continue...";
        public static readonly string Err_Input_32bitDepth = "Only Image with 32BitDepth was supported ! Please check again...";
        #endregion

        #region for Process Complete
        public static readonly string Process_Complete_Histogram = "Building Histogram data was completed ! Process Successfully...";
        public static readonly string Process_Complete_PixelLookup = "Building Pixel Lookup data was completed ! Process Successfully...";
        public static readonly string Process_Complete_MSEandPSNR = "Building MSE & PSNR data was completed ! Process Successfully...";
        public static readonly string Process_Complete_FileChecksumVerify = "Building MD5 hash data was completed ! Process Successfully...";
        #endregion
        
        #region for Notify Pixel Lookup
        public static readonly string Err_Input_DisplayLimit = "Input is Wrong or Invalid ! Please check again...";
        public static readonly string Err_PixelLookup_DisplayLimit = "This process can run if you load DataGrid before ! Please check again...";
        public static readonly string Notify_PixelLookup_MemoryLeak = "This process can consumption a lot of memory & takes a lot of time ! Click 'OK' to continue...";
        #endregion

        #region for Notify File Checksum Verify
        public static readonly string Hash_Identic = "File is Same / Identic";
        public static readonly string Hash_Err = "File isn't Same / Identic";
        #endregion        
    }

    class DataProcess
    {
        #region Other Variable
        public int stride_cover;
        public int stride_stego;

        public byte[] pixel_cover;
        public byte[] pixel_stego;

        public BitmapSource bmp_cover_raw;
        public BitmapSource bmp_stego_raw;
        #endregion

        #region Variable PixelLookup
        public string[] Blue_Cover;
        public string[] Green_Cover;
        public string[] Red_Cover;
        public string[] Alpha_Cover;

        public string[] Blue_Stego;
        public string[] Green_Stego;
        public string[] Red_Stego;
        public string[] Alpha_Stego;

        public string[] PointXY_Cover;       
        public string[] PointXY_Stego;
        public List<PixelLookup.Point_Cover> dgrid_cover_list2 = new List<PixelLookup.Point_Cover>();
        public List<PixelLookup.Point_Stego> dgrid_stego_list2 = new List<PixelLookup.Point_Stego>();
        #endregion

        #region Variable MSEandPSNR
        public byte[] MSEandPSNR_Red_cover;
        public byte[] MSEandPSNR_Green_cover;
        public byte[] MSEandPSNR_Blue_cover;
        public byte[] MSEandPSNR_Alpha_cover;

        public byte[] MSEandPSNR_Red_stego;
        public byte[] MSEandPSNR_Green_stego;
        public byte[] MSEandPSNR_Blue_stego;
        public byte[] MSEandPSNR_Alpha_stego;        
        #endregion

        #region Other Controller
        public string View_BitDepth(string img)
        {
            string val = String.Empty;
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(img);
            val = bmp.PixelFormat.ToString();
            return val;
        }
        
        public void LookupPixel(string path_cover, string path_stego)
        {
            bmp_cover_raw = new BitmapImage(new Uri(path_cover));
            bmp_stego_raw = new BitmapImage(new Uri(path_stego));

            pixel_cover = GetPixelLookup(bmp_cover_raw);
            pixel_stego = GetPixelLookup(bmp_stego_raw);

            stride_cover = bmp_cover_raw.PixelWidth * 4;
            stride_stego = bmp_stego_raw.PixelWidth * 4;
        }

        public byte[] GetPixelLookup(BitmapSource bmp)
        {
            int stride = bmp.PixelWidth * 4;
            int size = bmp.PixelHeight * stride;
            byte[] value = new byte[size];
            bmp.CopyPixels(value, stride, 0);
            return value;
        }
        #endregion

        #region Histogram Controller
        public PointCollection Exec_Histo(int[] pointHisto)
        {
            var values = SmoothHistogram(pointHisto);

            int max = values.Max();

            PointCollection dots = new PointCollection();
            // first point (lower-left corner)
            dots.Add(new System.Windows.Point(0, max));
            // middle points
            for (int i = 0; i < values.Length; i++)
            {
                dots.Add(new System.Windows.Point(i, max - values[i]));
            }
            // last point (lower-right corner)
            dots.Add(new System.Windows.Point(values.Length - 1, max));

            return dots;
        }

        private int[] SmoothHistogram(int[] originalValues)
        {
            int[] smoothedValues = new int[originalValues.Length];

            double[] mask = new double[] { 0.25, 0.5, 0.25 };

            for (int bin = 1; bin < originalValues.Length - 1; bin++)
            {
                double smoothedValue = 0;
                for (int i = 0; i < mask.Length; i++)
                {
                    smoothedValue += originalValues[bin - 1 + i] * mask[i];
                }
                smoothedValues[bin] = (int)smoothedValue;
            }
            return smoothedValues;
        }
        #endregion

        #region PixelLookup Controller
        public void SetPixel_ARGBwithPoint(int stride_cover, int stride_stego)
        {
            PointXY_Cover = new string[bmp_cover_raw.PixelHeight * bmp_cover_raw.PixelWidth];
            Blue_Cover = new string[bmp_cover_raw.PixelHeight * bmp_cover_raw.PixelWidth];
            Green_Cover = new string[bmp_cover_raw.PixelHeight * bmp_cover_raw.PixelWidth];
            Red_Cover = new string[bmp_cover_raw.PixelHeight * bmp_cover_raw.PixelWidth];
            Alpha_Cover = new string[bmp_cover_raw.PixelHeight * bmp_cover_raw.PixelWidth];

            PointXY_Stego = new string[bmp_stego_raw.PixelHeight * bmp_stego_raw.PixelWidth];
            Blue_Stego = new string[bmp_stego_raw.PixelHeight * bmp_stego_raw.PixelWidth];
            Green_Stego = new string[bmp_stego_raw.PixelHeight * bmp_stego_raw.PixelWidth];
            Red_Stego = new string[bmp_stego_raw.PixelHeight * bmp_stego_raw.PixelWidth];
            Alpha_Stego = new string[bmp_stego_raw.PixelHeight * bmp_stego_raw.PixelWidth];

            int idx_cover = 0;
            for (int y = 0; y < bmp_cover_raw.PixelHeight; y++)
            {
                for (int x = 0; x < bmp_cover_raw.PixelWidth; x++)
                {
                    int index = y * stride_cover + 4 * x;
                    byte blue = pixel_cover[index];
                    byte green = pixel_cover[index + 1];
                    byte red = pixel_cover[index + 2];
                    byte alpha = pixel_cover[index + 3];

                    PointXY_Cover[idx_cover] = String.Format("({0},{1})", x + 1, y + 1);
                    Blue_Cover[idx_cover] = String.Format("{0}", blue);
                    Green_Cover[idx_cover] = String.Format("{0}", green);
                    Red_Cover[idx_cover] = String.Format("{0}", red);
                    Alpha_Cover[idx_cover] = String.Format("{0}", alpha);

                    idx_cover++;
                }
            }

            int idx_stego = 0;
            for (int y = 0; y < bmp_stego_raw.PixelHeight; y++)
            {
                for (int x = 0; x < bmp_stego_raw.PixelWidth; x++)
                {
                    int index = y * stride_stego + 4 * x;
                    byte blue = pixel_stego[index];
                    byte green = pixel_stego[index + 1];
                    byte red = pixel_stego[index + 2];
                    byte alpha = pixel_stego[index + 3];

                    PointXY_Stego[idx_stego] = String.Format("({0},{1})", x + 1, y + 1);
                    Blue_Stego[idx_stego] = String.Format("{0}", blue);
                    Green_Stego[idx_stego] = String.Format("{0}", green);
                    Red_Stego[idx_stego] = String.Format("{0}", red);
                    Alpha_Stego[idx_stego] = String.Format("{0}", alpha);

                    idx_stego++;
                }
            }
        }
        
        public void GetPositionXY(int PointX, int PointY, int stride_cover, int stride_stego)
        {
            int index_cover = (PointY - 1) * stride_cover + 4 * (PointX - 1); //-1 for array index
            byte blue_cover = pixel_cover[index_cover];
            byte green_cover = pixel_cover[index_cover + 1];
            byte red_cover = pixel_cover[index_cover + 2];
            byte alpha_cover = pixel_cover[index_cover + 3];
            dgrid_cover_list2.Clear();
            dgrid_cover_list2.Add(new PixelLookup.Point_Cover() { PointXY_Cover = String.Format("({0},{1})",PointX.ToString(), PointY.ToString()), B_Cover = blue_cover.ToString(), G_Cover = green_cover.ToString(), R_Cover = red_cover.ToString(), A_Cover = alpha_cover.ToString() });
            //System.Diagnostics.Debug.WriteLine("X:{6} | Y:{5} STRIDE {4} || R {0} | G {1} | B {2} | A {3}", red_cover, green_cover, blue_cover, alpha_cover, index_cover, PointY, PointX);

            int index_stego = (PointY - 1) * stride_stego + 4 * (PointX - 1); //-1 for array index
            byte blue_stego = pixel_stego[index_stego];
            byte green_stego = pixel_stego[index_stego + 1];
            byte red_stego = pixel_stego[index_stego + 2];
            byte alpha_stego = pixel_stego[index_stego + 3];
            dgrid_stego_list2.Clear();
            dgrid_stego_list2.Add(new PixelLookup.Point_Stego() { PointXY_Stego = String.Format("({0},{1})", PointX.ToString(), PointY.ToString()), B_Stego = blue_stego.ToString(), G_Stego = green_stego.ToString(), R_Stego = red_stego.ToString(), A_Stego = alpha_stego.ToString() });
            //System.Diagnostics.Debug.WriteLine("X:{6} | Y:{5} STRIDE {4} || R {0} | G {1} | B {2} | A {3}", red_stego, green_stego, blue_stego, alpha_stego, index_stego, PointY, PointX);

        }
        #endregion

        #region MSEandPSNR Controller
        public void SetPixel_ARGB(int stride_cover, int stride_stego)
        {
            MSEandPSNR_Red_cover = new byte[bmp_cover_raw.PixelWidth * bmp_cover_raw.PixelHeight];
            MSEandPSNR_Green_cover = new byte[bmp_cover_raw.PixelWidth * bmp_cover_raw.PixelHeight];
            MSEandPSNR_Blue_cover = new byte[bmp_cover_raw.PixelWidth * bmp_cover_raw.PixelHeight];
            MSEandPSNR_Alpha_cover = new byte[bmp_cover_raw.PixelWidth * bmp_cover_raw.PixelHeight];

            MSEandPSNR_Red_stego = new byte[bmp_stego_raw.PixelWidth * bmp_stego_raw.PixelHeight];
            MSEandPSNR_Green_stego = new byte[bmp_stego_raw.PixelWidth * bmp_stego_raw.PixelHeight];
            MSEandPSNR_Blue_stego = new byte[bmp_stego_raw.PixelWidth * bmp_stego_raw.PixelHeight];
            MSEandPSNR_Alpha_stego = new byte[bmp_stego_raw.PixelWidth * bmp_stego_raw.PixelHeight];

            int idx_cover = 0;
            for (int y = 0; y < bmp_cover_raw.PixelHeight; y++)
            {
                for (int x = 0; x < bmp_cover_raw.PixelWidth; x++)
                {
                    int index = y * stride_cover + 4 * x;
                    byte red = pixel_cover[index];
                    byte green = pixel_cover[index + 1];
                    byte blue = pixel_cover[index + 2];
                    byte alpha = pixel_cover[index + 3];

                    MSEandPSNR_Red_cover[idx_cover] = red;
                    MSEandPSNR_Green_cover[idx_cover] = green;
                    MSEandPSNR_Blue_cover[idx_cover] = blue;
                    MSEandPSNR_Alpha_cover[idx_cover] = alpha;

                    idx_cover++;
                }
            }

            int idx_stego = 0;
            for (int y = 0; y < bmp_stego_raw.PixelHeight; y++)
            {
                for (int x = 0; x < bmp_stego_raw.PixelWidth; x++)
                {
                    int index = y * stride_stego + 4 * x;
                    byte red = pixel_stego[index];
                    byte green = pixel_stego[index + 1];
                    byte blue = pixel_stego[index + 2];
                    byte alpha = pixel_stego[index + 3];

                    MSEandPSNR_Red_stego[idx_stego] = red;
                    MSEandPSNR_Green_stego[idx_stego] = green;
                    MSEandPSNR_Blue_stego[idx_stego] = blue;
                    MSEandPSNR_Alpha_stego[idx_stego] = alpha;

                    idx_stego++;
                }
            }
        }

        public double[] count_MSE(byte[] mse_red_cover, byte[] mse_red_stego, byte[] mse_green_cover, byte[] mse_green_stego, byte[] mse_blue_cover, byte[] mse_blue_stego, byte[] mse_alpha_cover, byte[] mse_alpha_stego)
        {
            double[] val = new double[5];
            double[] mse_result_red = new double[mse_red_cover.Length];
            double[] mse_result_green = new double[mse_green_cover.Length];
            double[] mse_result_blue = new double[mse_blue_cover.Length];
            double[] mse_result_alpha = new double[mse_alpha_cover.Length];
            double[] mse_result_bgra = new double[mse_alpha_cover.Length];

            int idx_mse = 0;
            for (int i = 0; i < mse_red_cover.Length; i++)
            {
                mse_result_red[idx_mse] = Math.Pow((mse_red_cover[idx_mse] - mse_red_stego[idx_mse]), 2);
                mse_result_green[idx_mse] = Math.Pow((mse_green_cover[idx_mse] - mse_green_stego[idx_mse]), 2);
                mse_result_blue[idx_mse] = Math.Pow((mse_blue_cover[idx_mse] - mse_blue_stego[idx_mse]), 2);
                mse_result_alpha[idx_mse] = Math.Pow((mse_alpha_cover[idx_mse] - mse_alpha_stego[idx_mse]), 2);
                idx_mse++;
            }

            val[0] = mse_result_red.Sum() / (bmp_cover_raw.PixelWidth * bmp_cover_raw.PixelHeight);
            val[1] = mse_result_green.Sum() / (bmp_cover_raw.PixelWidth * bmp_cover_raw.PixelHeight);
            val[2] = mse_result_blue.Sum() / (bmp_cover_raw.PixelWidth * bmp_cover_raw.PixelHeight);
            val[3] = mse_result_alpha.Sum() / (bmp_cover_raw.PixelWidth * bmp_cover_raw.PixelHeight);

            val[4] = (val[0] + val[1] + val[2] + val[3]) / 4;
            return val;
        }

        public double[] count_PSNR(double[] MSEvalue)
        {
            double[] val = new double[5];
            double max_r = Math.Max(MSEandPSNR_Red_cover.Max(), MSEandPSNR_Red_stego.Max());
            double max_g = Math.Max(MSEandPSNR_Green_cover.Max(), MSEandPSNR_Green_stego.Max());
            double max_b = Math.Max(MSEandPSNR_Blue_cover.Max(), MSEandPSNR_Blue_stego.Max());
            double max_a = Math.Max(MSEandPSNR_Alpha_cover.Max(), MSEandPSNR_Alpha_stego.Max());

            double mse_r_sqrt = Math.Sqrt(MSEvalue[0]);
            double mse_g_sqrt = Math.Sqrt(MSEvalue[1]);
            double mse_b_sqrt = Math.Sqrt(MSEvalue[2]);
            double mse_a_sqrt = Math.Sqrt(MSEvalue[3]);

            val[0] = 20 * Math.Log10(max_r / mse_r_sqrt);
            val[1] = 20 * Math.Log10(max_g / mse_g_sqrt);
            val[2] = 20 * Math.Log10(max_b / mse_b_sqrt);
            val[3] = 20 * Math.Log10(max_a / mse_a_sqrt);

            val[4] = (val[0] + val[1] + val[2] + val[3]) / 4;
            return val;
        }
        #endregion

        #region File Verify Controller
        public string[] getFileInfo(OpenFileDialog file)
        {
            string[] val = new string[4];
            val[0] = file.SafeFileName;
            val[1] = new FileInfo(file.FileName).Length.ToString();
            val[2] = new FileInfo(file.FileName).Extension;
            val[3] = file.FileName.Replace("\\" + file.SafeFileName, String.Empty);
            return val;
        }

        public string checkMD5(string pathFile)
        {
            byte[] data;
            using (MD5 check_md5 = MD5.Create())
            {
                using (Stream streamFile = File.OpenRead(pathFile))
                {
                    data = check_md5.ComputeHash(streamFile);
                }
            }

            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        #endregion

    }
}
