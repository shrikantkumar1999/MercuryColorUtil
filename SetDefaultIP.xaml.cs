using MercuryColorSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MercuryColorUtil
{
    /// <summary>
    /// Interaction logic for SetDefaultIP.xaml
    /// </summary>
    public partial class SetDefaultIP : Window
    {
        MercuryClass mercuryClass = new MercuryClass();
        public SetDefaultIP()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                ChangeIPAddress();
            }
            catch { }
        }


        private void ChangeIPAddress()
        {
            try
            {
                mercuryClass.SetVMSIP(txtIPAddress.Text.Trim(), txtSubnetmask.Text.Trim(), txtDefaultgateway.Text.Trim(), txtDNSServer.Text.Trim());
            }
            catch
            {
            }
        }
    }
}
