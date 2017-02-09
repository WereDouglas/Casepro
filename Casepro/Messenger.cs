using GsmComm.GsmCommunication;
using GsmComm.PduConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Casepro
{
    public class Messenger
    {
        private static GsmCommMain comm;
        private delegate void SetTextCallback(string text);
        private static string cmbCOM;
      
        public static void Send( string message, string number)
        {
            try
            {

                SmsSubmitPdu pdu;
                byte dcs = (byte)DataCodingScheme.GeneralCoding.Alpha7BitDefault;
                pdu = new SmsSubmitPdu(message, Convert.ToString(number), dcs);
                int times = 1;
                for (int i = 0; i < times; i++)
                {
                    comm.SendMessage(pdu);
                }

              

            }
            catch
            {
               


            }
        }
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);

        public static bool IsInternetAvailable()
        {
            int description;
            return InternetGetConnectedState(out description, 0);
        }


        public static string SendUpdate(string message, string number)
        {
            // Messenger.SendUpdate(App.amApp, u.Id, u.Content, u.Contact);
            string state;
            try
            {
                SmsSubmitPdu pdu;
                byte dcs = (byte)DataCodingScheme.GeneralCoding.Alpha7BitDefault;
                pdu = new SmsSubmitPdu(message, Convert.ToString(number), dcs);
                int times = 1;
                for (int i = 0; i < times; i++)
                {
                    comm.SendMessage(pdu);
                  
                }
                state = "sent";

            }
            catch (Exception r)
            {
                state = "not sent " + r;
            }
            return state;
        }
        public static bool ports = false;
        public static bool state;
        public static bool connect()
        {
            state = false;
            int d = 0;
            do
            {
                d++;
                cmbCOM = "COM" + d.ToString();
                comm = new GsmCommMain(cmbCOM, 9600, 150);
                Console.WriteLine(cmbCOM);

                if (comm.IsConnected())
                {
                    Console.WriteLine("comm is already open");
                    state = true;
                    break;
                }
                else
                {
                    Console.WriteLine("comm is not open");
                    try
                    {
                        comm.Open();
                        state = true;
                    }
                    catch (Exception)
                    {

                        state = false;
                        continue;

                    }
                }
            }
            while (!comm.IsConnected() && d < 30);

            Console.WriteLine(cmbCOM);
            return state;

        }
        public static bool connects()
        {

            cmbCOM = "COM4";
            comm = new GsmCommMain(cmbCOM, 9600, 150);
            Console.WriteLine(cmbCOM);

            if (comm.IsConnected())
            {
                Console.WriteLine("comm is already open");
                state = true;

            }
            else
            {
                Console.WriteLine("comm is not open");
                try
                {
                    comm.Open();
                    state = true;
                }
                catch (Exception)
                {

                    state = false;


                }

            }

            Console.WriteLine(cmbCOM);
            return state;

        }

    }
}

