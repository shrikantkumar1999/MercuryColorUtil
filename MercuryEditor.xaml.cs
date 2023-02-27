using MercuryColorSDK;
using MercuryColorSDK.ProgFiles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MercuryColorUtil
{
    /// <summary>
    /// Interaction logic for MercuryEditor.xaml
    /// </summary>
    public partial class MercuryEditor : Window
    {
        #region Declare Global Variables

        public static int MessageID { get; set; }
        public static int EditMessageID = 0;
        List<GenericClass> genericClasses = new List<GenericClass>();
        string fileName = string.Empty;
        string destinationPath = string.Empty;
        int MsgType = 8;
        MercuryClass mercuryCls = new MercuryClass();
        string[] resize;
        int width = 0;
        int height = 0;
        ushort port = 0;
        string str = string.Empty;

        #endregion


        public MercuryEditor()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                width = Convert.ToInt16(ConfigurationManager.AppSettings["Width"]);
                height = Convert.ToInt16(ConfigurationManager.AppSettings["Height"]);
                port = Convert.ToUInt16(ConfigurationManager.AppSettings["Port"]);
                BindBrightness();
                dpPattern.SelectedIndex = 0;
                int nWidth = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
                int nHieght = (int)System.Windows.SystemParameters.PrimaryScreenHeight;
                this.LayoutTransform = new ScaleTransform(nWidth / 1920, nHieght / 1080);
                resize = System.Configuration.ConfigurationManager.AppSettings["LEDResize"].Split(',');
            }
            catch
            {

            }
        }

        private void BindBrightness()
        {
            try
            {
                List<BindComboboxItems> lst = new List<BindComboboxItems>();
                lst.Add(new BindComboboxItems() { DisplayPath = "--Select--", Displayvalue = "0" });

                for (int i = 1; i < 256; i++)
                {
                    lst.Add(new BindComboboxItems() { DisplayPath = i.ToString(), Displayvalue = i.ToString() });

                }
                cmbbrightness.DisplayMemberPath = "DisplayPath";
                cmbbrightness.SelectedValuePath = "Displayvalue";
                cmbbrightness.ItemsSource = lst;

                cmbbrightness.SelectedIndex = 0;

            }
            catch
            {
            }


        }

        /// <summary>
        /// To Set VMS IP
        /// </summary>
        /// <param name="IPAddress"></param>
        /// <param name="Submask"></param>
        /// <param name="gatewayIP"></param>
        /// <param name="dnsServer"></param>

        public void SetVMSIP()
        {
            SetDefaultIP setDefaultIP = new SetDefaultIP();
            setDefaultIP.ShowDialog();
        }

        private void cmbbrightness_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (cmbbrightness.SelectedIndex > 0)
                {
                    mercuryCls.BrightnessSetting(txtIPAddress.Text.Trim(), Convert.ToUInt16(ConfigurationManager.AppSettings["Port"]), Convert.ToInt16(cmbbrightness.SelectedValue));
                }

            }
            catch { }
        }



        private void dpPattern_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int value = dpPattern.SelectedIndex;
                if (value != 0)
                {
                    mercuryCls.SendTestPatter(Convert.ToInt32(width), Convert.ToInt32(height), txtIPAddress.Text.Trim(), Convert.ToUInt16(port), value);
                }

            }
            catch
            {

            }
        }



        private void btnSetIP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SetVMSIP();
            }
            catch { }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {

                mercuryCls.GetParameters();
            }

            catch { }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string ip = txtIPAddress.Text.Trim();
            ushort port = Convert.ToUInt16(ConfigurationManager.AppSettings["Port"]);
            //int Timer = Convert.ToInt16(txtsetTime.Text.Trim());

            //GenericClass Gclass = new GenericClass();
            //Gclass.Title = ConfigurationManager.AppSettings["FilepathImage"];
            //Gclass.PrimaryMessageID = MessageID;
            //Gclass.Type = 0;
            //MsgType = 0;
            ////Gclass.Timmer = Convert.ToInt16(txtsetTime.Text.Trim());
            //genericClasses.Add(Gclass);

            GenericClass Gclass = new GenericClass();

            Gclass.Title = @"E:\TrafikView\ColorVMS\Image\Img_29422022044227.png";
            Gclass.PrimaryMessageID = MessageID;
            Gclass.Type = 0;
            MsgType = 0;
            //Gclass.Timmer = Convert.ToInt16(txtsetTime.Text.Trim());
            genericClasses.Add(Gclass);



            GenericClass Gclass1 = new GenericClass();

            Gclass1.Title = @"E:\TrafikView\ColorVMS\Image\Img_31092022050928.jpeg";
            Gclass1.PrimaryMessageID = MessageID;
            Gclass1.Type = 0;
            MsgType = 0;
            //Gclass.Timmer = Convert.ToInt16(txtsetTime.Text.Trim());
            genericClasses.Add(Gclass1);

            
            mercuryCls.SendMessage(genericClasses, MsgType, ip, port, Convert.ToInt64(txtsetTime.Text.Trim()), height, width);
        }

        private void btnVideo_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string ip = txtIPAddress.Text.Trim();
                ushort port = Convert.ToUInt16(ConfigurationManager.AppSettings["Port"]);
                //int Timer = Convert.ToInt16(txtsetTime.Text.Trim());

                GenericClass Gclass = new GenericClass();
                Gclass.Title = ConfigurationManager.AppSettings["FilepathVideo"];
                Gclass.PrimaryMessageID = MessageID;

                Gclass.Type = 3;
                MsgType = 3;
                //Gclass.Timmer = Convert.ToInt16(txtsetTime.Text.Trim());
                genericClasses.Add(Gclass);

                mercuryCls.SendMessage(genericClasses, MsgType, ip, port, Convert.ToInt64(txtsetTime.Text.Trim()), height, width);


            }
            catch { }
        }

        private void btnfault_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Net.IPAddress ip;
                bool ValidateFIP1 = System.Net.IPAddress.TryParse(txtFDIPAddress.Text.Trim(), out ip);
                if (!ValidateFIP1)
                {
                    MessageBox.Show("Enter Octopus IP Address/Enter Valid Octopus IP Address");
                    txtFDIPAddress.Focus();
                }
                else
                {
                    PopupFault popfault = new PopupFault(txtFDIPAddress.Text, 5005);
                    popfault.ShowDialog();
                }

            }
            catch
            {

            }
        }

        private void BtnText_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string ip = txtIPAddress.Text.Trim();
                ushort port = Convert.ToUInt16(ConfigurationManager.AppSettings["Port"]);

                GenericClass Gclass = new GenericClass();
                Gclass.Text = "Welcome To Vulcan";
                Gclass.FontFamily = "Times New Roman";
                Gclass.FontSize = "32";
                Gclass.FontColor = "Blue";
                Gclass.PrimaryMessageID = MessageID;

                Gclass.Type = 4;
                MsgType = 4;
                //Gclass.Timmer = Convert.ToInt16(txtsetTime.Text.Trim());
                genericClasses.Add(Gclass);



                GenericClass Gclass1 = new GenericClass();
                Gclass1.Text = "Welcome To Production Department";
                Gclass1.FontFamily = "Times New Roman";
                Gclass1.FontSize = "32";
                Gclass1.FontColor = "Red";
                Gclass1.PrimaryMessageID = MessageID;

                Gclass1.Type = 4;
                MsgType = 4;
                //Gclass.Timmer = Convert.ToInt16(txtsetTime.Text.Trim());
                genericClasses.Add(Gclass1);

                mercuryCls.SendMessage(genericClasses, MsgType, ip, port, Convert.ToInt64(txtsetTime.Text.Trim()), height, width);


            }
            catch
            {

            }
        }
    }
    public class BindComboboxItems
    {
        public string DisplayPath { get; set; }
        public string Displayvalue { get; set; }

    }



}
