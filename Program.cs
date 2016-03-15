using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PortSIP;
using SoftPhone;

namespace SIPSample
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static SoftPhone.SoftPhone thisPhone = new SoftPhone.SoftPhone();
        private static bool isStarted = false;
        public static bool isActive = true;


        [STAThread]
        static void Main(string[] args)
        {
            if (!isStarted)
            {
                
                thisPhone.StartPhone(args);
                isStarted = true;
            }
        

            while (isActive)
            {
               
                var inputString = Console.ReadLine();
                if (inputString.StartsWith("quit"))
                {
                    Console.WriteLine("Exiting");
                    thisPhone.deRegisterFromServer();
                    break;
                }
                else {
                    ProccessInput(inputString);
                }

            }

        }

        static void ProccessInput(string inputString)
        {
            var tempString = inputString.Split(' ');
            try
            {
                if (tempString[0].Length > 1 && tempString[1].Length > 1)
                {
                    var cmd = tempString[0];
                    var subject = tempString[1];
                    switch (cmd)
                    {
                        case "call":
                            thisPhone.makeCall(subject.Trim());
                            break;
                        case "end":
                            thisPhone.EndCall();
                            break;
                        default:
                            Console.WriteLine("command not found [" + cmd + "]", Console.ForegroundColor = ConsoleColor.Red);
                            break;


                    }
                }else{
                    Console.WriteLine("COULD NOT PROCCESS THIS COMMAND [" + inputString + "]", Console.ForegroundColor=ConsoleColor.Red);
                }
            }
            catch (Exception) {
               // Console.WriteLine(e);
                
            }
            
                       
        }
    }

}