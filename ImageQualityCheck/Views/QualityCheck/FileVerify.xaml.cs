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
using System.IO;

namespace PictDIFFER.Views.QualityCheck
{
    /// <summary>
    /// Interaction logic for FileVerify.xaml
    /// </summary>
    public partial class FileVerify : UserControl
    {
        public FileVerify()
        {
            InitializeComponent();
            checksum_btn_origin.Click += checksum_btn_origin_Click;
            checksum_btn_embed.Click += checksum_btn_embed_Click;
            checksum_btn_ClearAll.Click += checksum_btn_ClearAll_Click;
            checksum_btn_Exec.Click += checksum_btn_Exec_Click;
        }

        #region for Variable & Initialize
        string getPathOrigin;
        string getPathEmbed;
        string md5_hash_origin;
        string md5_hash_embed;

        DataProcess dp = new DataProcess();
        OpenFileDialog opendlg;
        #endregion

        #region for Button Event Handler
        void checksum_btn_origin_Click(object sender, RoutedEventArgs e)
        {
            opendlg = new OpenFileDialog();
            opendlg.Filter = "All Files (Original File)|*.*";
            if (opendlg.ShowDialog() == true)
            {
                getPathOrigin = opendlg.FileName;
                string[] Origin_fileInfo = dp.getFileInfo(opendlg);
                SetTextOrigin(Origin_fileInfo);
            }
            else
            {
                SetTextOrigin_Clear();
            }
        }

        void checksum_btn_embed_Click(object sender, RoutedEventArgs e)
        {
            opendlg = new OpenFileDialog();
            opendlg.Filter = "All Files (Embed File)|*.*";
            if (opendlg.ShowDialog() == true)
            {
                getPathEmbed = opendlg.FileName;
                string[] Embed_fileInfo = dp.getFileInfo(opendlg);
                SetTextEmbed(Embed_fileInfo);
            }
            else
            {
                SetTextEmbed_Clear();
            }
        }

        void checksum_btn_ClearAll_Click(object sender, RoutedEventArgs e)
        {
            SetTextOrigin_Clear();
            SetTextEmbed_Clear();
            SetHashValue_Clear();
            FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Clear_Input_Img, NotifyDataText.Capt_Success, MessageBoxButton.OK);
        }
        
        void checksum_btn_Exec_Click(object sender, RoutedEventArgs e)
        {
            if (name_1.Text != "File Name : -" && name_2.Text != "File Name : -")
            {
                if (FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Confirm_Exec_Process_Img, NotifyDataText.Capt_Confirm, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    Exec_Process();
                    FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(NotifyDataText.Process_Complete_FileChecksumVerify, NotifyDataText.Capt_Success, MessageBoxButton.OK);
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
            md5_hash_origin = dp.checkMD5(getPathOrigin);
            md5_hash_embed = dp.checkMD5(getPathEmbed);
            SetHashValue(md5_hash_origin, md5_hash_embed);
        }
        #endregion

        #region for Set File Info Property
        void SetTextOrigin(string[] val)
        {
            name_1.Text = String.Format("File Name : {0}", val[0]);
            size_1.Text = String.Format("Size : {0} bytes", val[1]);
            ftype_1.Text = String.Format("File Type : {0}", val[2]);
            path_1.Text = String.Format("Path : {0}", val[3]);
        }

        void SetTextEmbed(string[] val)
        {
            name_2.Text = String.Format("File Name : {0}", val[0]);
            size_2.Text = String.Format("Size : {0} bytes", val[1]);
            ftype_2.Text = String.Format("File Type : {0}", val[2]);
            path_2.Text = String.Format("Path : {0}", val[3]);
        }

        void SetTextOrigin_Clear()
        {
            name_1.Text = "File Name : -";
            size_1.Text = "Size : -";
            ftype_1.Text = "File Type : -";
            path_1.Text = "Path : -";
        }

        void SetTextEmbed_Clear()
        {
            name_2.Text = "File Name : -";
            size_2.Text = "Size : -";
            ftype_2.Text = "File Type : -";
            path_2.Text = "Path : -";
        }

        void SetHashValue(string md5_origin, string md5_embed)
        {
            hash_origin.Text = String.Format("MD5 | {0}", md5_origin);
            hash_embed.Text = String.Format("MD5 | {0}", md5_embed);

            if (md5_origin == md5_embed)
            {
                hash_compare.Text = NotifyDataText.Hash_Identic;
            }
            else
            {
                hash_compare.Text = NotifyDataText.Hash_Err;
            }

        }

        void SetHashValue_Clear()
        {
            hash_origin.Text = "MD5 | -";
            hash_embed.Text = "MD5 | -";
            hash_compare.Text = "Unidentified";
        }
        #endregion
    }
}
