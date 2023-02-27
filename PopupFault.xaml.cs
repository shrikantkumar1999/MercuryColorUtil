using MercuryColorSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
    /// Interaction logic for PopupFault.xaml
    /// </summary>
    public partial class PopupFault : Window
    {
        FaultDiagnosisClass fault = new FaultDiagnosisClass();
        const int PORT_NUMBER = 5005;
        string[] GetUDPData = null;
        string OctopusIP = string.Empty; int PortNos = 5005;
        public PopupFault(string OctopusIPAddress, int PortNo)
        {
            InitializeComponent();
            OctopusIP = OctopusIPAddress;
            PortNos = PortNo;
            this.DataContext = fault;
            ReciveUDPData(OctopusIPAddress, PortNo);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {



        }
        UdpClient Client = null;
        IPEndPoint RemoteIpEndPoint = null;

        public void ReciveUDPData(string OctopusIPAddress, int PortNo)
        {
            try
            {
                Client = new UdpClient(PortNo);
                IPAddress serverAddr = IPAddress.Parse(OctopusIPAddress);
                RemoteIpEndPoint = new IPEndPoint(serverAddr, PortNo);
                Client.BeginReceive(new AsyncCallback(recv), null);


            }
            catch (Exception e)
            {
                MessageBox.Show("ReciveUDPData Error =" + e.Message);
            }

        }

        private void recv(IAsyncResult res)
        {
            if (Client.Client != null)
            {
                byte[] received = Client.EndReceive(res, ref RemoteIpEndPoint);
                try
                {
                    GetUDPData = Encoding.UTF8.GetString(received).Split(',');
                    MessageBox.Show("Record Updated!");
                    BindValues();
                }
                catch (Exception e)
                {
                    MessageBox.Show("recv Error =" + e.Message);

                }
                Client.BeginReceive(new AsyncCallback(recv), null);
            }
        }



        public void SendUDPData(string OctopusIPAddress, int PortNo)
        {
            try
            {
                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                IPAddress serverAddr = IPAddress.Parse(OctopusIPAddress);
                IPEndPoint endPoint = new IPEndPoint(serverAddr, PortNo);
                byte[] send_buffer = null;

                send_buffer = Encoding.ASCII.GetBytes("HELLO ");
                sock.SendTo(send_buffer, endPoint);
                sock.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show("SendUDPData Error =" + e.Message);

            }
        }


        private void BindValues()
        {
            try
            {
                //FaultDiagnosisClass fault = new FaultDiagnosisClass();
                if (GetUDPData.Count() > 0)
                {
                  //  MessageBox.Show(GetUDPData[0] + " " + GetUDPData[1]);
                    fault.txtdatetime = GetUDPData[0] + " " + GetUDPData[1];
                    fault.txtROW1 = GetUDPData[2] == "1" ? "OK" : "Fault";
                    fault.txtROW2 = GetUDPData[3] == "1" ? "OK" : "Fault";
                    fault.txtROW3 = GetUDPData[4] == "1" ? "OK" : "Fault";
                    fault.txtROW4 = GetUDPData[5] == "1" ? "OK" : "Fault";
                    fault.txtROW5 = GetUDPData[6] == "1" ? "OK" : "Fault";
                    fault.txtROW6 = GetUDPData[7] == "1" ? "OK" : "Fault";
                    fault.txtROW7 = GetUDPData[8] == "1" ? "OK" : "Fault";
                    fault.txtROW8 = GetUDPData[9] == "1" ? "OK" : "Fault";
                    fault.txtROW9 = GetUDPData[10] == "1" ? "OK" : "Fault";
                    fault.txtROW10 = GetUDPData[11] == "1" ? "OK" : "Fault";
                    fault.txtROW11 = GetUDPData[12] == "1" ? "OK" : "Fault";
                    fault.txtROW12 = GetUDPData[13] == "1" ? "OK" : "Fault";
                    fault.txtDOOR1 = GetUDPData[14] == "1" ? "Open" : "Close";
                    fault.txtDOOR2 = GetUDPData[15] == "1" ? "Open" : "Close";
                    fault.txtShock1 = GetUDPData[16] == "1" ? "Yes" : "No";
                    fault.txtShock2 = GetUDPData[17] == "1" ? "Yes" : "No";
                    fault.txtTemp = GetUDPData[18];
                    fault.txtHumidity = GetUDPData[19];
                    fault.txtLPF = GetUDPData[20].Replace('T', ' ');
                    fault.txtLPR = GetUDPData[21].Replace('T', ' ');

                }


            }
            catch (Exception e)
            {
                MessageBox.Show("BindValues Error =" + e.Message);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SendUDPData(OctopusIP, PortNos);
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                fault = new FaultDiagnosisClass();
                Client.Close();
                //Client.Dispose();

            }
            catch { }
        }
    }
}
