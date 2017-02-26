using Ozeki.Media;
using Ozeki.Media.MediaHandlers;
using Ozeki.Network.Nat;
using Ozeki.VoIP;
using Ozeki.VoIP.SDK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WhatsAppApi;

namespace Casepro
{
    public partial class PhoneForm : Form
    {
        static ISoftPhone softphone;   // softphone object
        static IPhoneLine phoneLine;   // phoneline object
        static IPhoneCall call;
        static string caller;
        static Microphone microphone;
        static Speaker speaker;
        static PhoneCallAudioSender mediaSender;
        static PhoneCallAudioReceiver mediaReceiver;
        static MediaConnector connector;
        static string ipAddress;
        static string config;

        public PhoneForm()
        {
            InitializeComponent();
            softphone = SoftPhoneFactory.CreateSoftPhone(5000, 10000);

            microphone = Microphone.GetDefaultDevice();
            speaker = Speaker.GetDefaultDevice();
            mediaSender = new PhoneCallAudioSender();
            mediaReceiver = new PhoneCallAudioReceiver();
            connector = new MediaConnector();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var ipAddress = myIPTxt.Text;
            var config = new DirectIPPhoneLineConfig(ipAddress, 5060);
            phoneLine = softphone.CreateDirectIPPhoneLine(config);
            phoneLine.RegistrationStateChanged += line_RegStateChanged;
            softphone.IncomingCall += softphone_IncomingCall;
            softphone.RegisterPhoneLine(phoneLine);
        }

        private  void line_RegStateChanged(object sender, RegistrationStateChangedArgs e)
        {
            if (e.State == RegState.NotRegistered || e.State == RegState.Error)
                Console.WriteLine("Registration failed!");
          
            Invoke((MethodInvoker)delegate
            {
                infoTxt.Text = infoTxt.Text + ("Call state:" + e.State.ToString()) + "\r\n";
            });

            if (e.State == RegState.RegistrationSucceeded)
            {
                Console.WriteLine("Registration succeeded - Online!");
                Invoke((MethodInvoker)delegate
                {
                    infoTxt.Text = infoTxt.Text + ("Registration succeeded - Online!") + "\r\n";
                });
               /// Console.WriteLine("Enter the IP address of the destination: ");
                //infoTxt.Text = ("Enter the IP address of the destination: ");
                string ipToDial = destinationIP.Text;
                StartCall(ipToDial);
            }
        }

        private  void StartCall(string numberToDial)
        {
            if (call == null)
            {
                call = softphone.CreateDirectIPCallObject(phoneLine, new DirectIPDialParameters("5060"), numberToDial);
                call.CallStateChanged += call_CallStateChanged;
                call.Start();
            }
        }

         void softphone_IncomingCall(object sender, VoIPEventArgs<IPhoneCall> e)
        {
            call = e.Item;
            caller = call.DialInfo.CallerID;
            call.CallStateChanged += call_CallStateChanged;
            call.Answer();
        }

         void call_CallStateChanged(object sender, CallStateChangedArgs e)
        {
            Console.WriteLine("Call state: {0}.", e.State);
            
            Invoke((MethodInvoker)delegate
            {
                infoTxt.Text = infoTxt.Text + ("Call state:" + e.State.ToString()) + "\r\n";
            });

            if (e.State == CallState.Answered)
                SetupDevices();

            if (e.State.IsCallEnded())
                CloseDevices();
        }

        static void SetupDevices()
        {
            microphone.Start();
            connector.Connect(microphone, mediaSender);

            speaker.Start();
            connector.Connect(mediaReceiver, speaker);

            mediaSender.AttachToCall(call);
            mediaReceiver.AttachToCall(call);
        }

        static void CloseDevices()
        {
            microphone.Dispose();
            speaker.Dispose();

            mediaReceiver.Detach();
            mediaSender.Detach();
            connector.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string from = "9199876543210"; //(Enter Your Mobile Number)
            string to = destinationIP.Text;
            string msg = infoTxt.Text;
            WhatsApp wa = new WhatsApp(from, "WhatsAppPassword", "NickName", false, false);
            wa.OnConnectSuccess += () =>
            {
                MessageBox.Show("Connected to WhatsApp...");
                wa.OnLoginSuccess += (phonenumber, data) =>
                {
                    wa.SendMessage(to, msg);
                    MessageBox.Show("Message Sent...");
                };
                wa.OnLoginFailed += (data) =>
                {
                    MessageBox.Show("Login Failed : {0} : ", data);
                };

                wa.Login();
            };
            wa.OnConnectFailed += (Exception) =>
            {
                MessageBox.Show("Connection Failed...");
            };
        }
    }
}