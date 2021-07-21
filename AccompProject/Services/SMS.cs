using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Web;



namespace AccompProject.Services
{
    public class SMS
    {
        SerialPort sp = new SerialPort();
        public  void sendSMS(string mobNo, string msg)
        {
            string telNo = Char.ConvertFromUtf32(34) + mobNo + Char.ConvertFromUtf32(34);
            string msg1 = "";
            string msg2 = "";

        //    sp.PortName = "COM3";
           sp.PortName = "COM7";

            var cnt = 1;
            if (msg.Length >= 140)
            {
                msg1 = msg.Substring(0, 140);
                msg2 = msg.Substring(msg.Length - (msg.Length-140), (msg.Length - 140));
                cnt = 2;

            }
            




            for (int i = 1; i <= cnt; i++)
            {

                sp.Open();
                sp.Write("AT+CMGF=1" + Char.ConvertFromUtf32(13));
                sp.Write("AT+CMGS=" + telNo + Char.ConvertFromUtf32(13));
                if (cnt == 2)
                {
                    if (i == 1)
                    {
                        sp.Write(msg1 + Char.ConvertFromUtf32(26) + Char.ConvertFromUtf32(13));
                    }

                    if (i == 2)
                    {
                        sp.Write(msg2 + Char.ConvertFromUtf32(26) + Char.ConvertFromUtf32(13));
                    }
                }
                else {
                    sp.Write(msg + Char.ConvertFromUtf32(26) + Char.ConvertFromUtf32(13));
                
                }
                sp.Close();

                if (i < 2)
                {
                    System.Threading.Thread.Sleep(3000);
                }
            }
          
        }


        public string ReceiveSMS() 
        {
        //    string telNo = Char.ConvertFromUtf32(34) + mobNo + Char.ConvertFromUtf32(34);
            //string msg1 = "";
            //string msg2 = "";

            //    sp.PortName = "COM3";

            //var cnt = 1;
            //if (msg.Length >= 140)
            //{
            //    msg1 = msg.Substring(0, 140);
            //    msg2 = msg.Substring(msg.Length - (msg.Length - 140), (msg.Length - 140));
            //    cnt = 2;

            //}
            var received = "";
            try
            {
                sp.PortName = "COM5";


                sp.Open();

                sp.Write("AT+CMGF=1" + Char.ConvertFromUtf32(13));
           //     System.Threading.Thread.Sleep(2000);

                sp.Write("AT+CMGL=\"ALL\"" + Char.ConvertFromUtf32(13));
                System.Threading.Thread.Sleep(2000);
                
                
                
                received = sp.ReadExisting();
                sp.Close();


            }
            catch (Exception ex) {

                received = ex.Message;
            }

            return received.ToString();


            //for (int i = 1; i <= cnt; i++)
            //{

            //    sp.Open();
            //    sp.Write("AT+CMGF=1" + Char.ConvertFromUtf32(13));
            //    sp.Write("AT+CMGS=" + telNo + Char.ConvertFromUtf32(13));
            //    if (cnt == 2)
            //    {
            //        if (i == 1)
            //        {
            //            sp.Write(msg1 + Char.ConvertFromUtf32(26) + Char.ConvertFromUtf32(13));
            //        }

            //        if (i == 2)
            //        {
            //            sp.Write(msg2 + Char.ConvertFromUtf32(26) + Char.ConvertFromUtf32(13));
            //        }
            //    }
            //    else
            //    {
            //        sp.Write(msg + Char.ConvertFromUtf32(26) + Char.ConvertFromUtf32(13));

            //    }
            //    sp.Close();

            //    if (i < 2)
            //    {
            //        System.Threading.Thread.Sleep(3000);
            //    }
            //}

        }
    }
}