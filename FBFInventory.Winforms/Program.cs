using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FBFInventory.Domain.Entity;
using FBFInventory.Infrastructure.Dto;
using FBFInventory.Infrastructure.EntityFramework;
using FBFInventory.Infrastructure.Service;
using FBFInventory.Winforms.Helper;
using log4net;
using log4net.Config;

[assembly: XmlConfigurator]

namespace FBFInventory.Winforms
{
    internal static class Program
    {
        private static ILog Log = LogManager.GetLogger(typeof (Program));

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(){
            Log.Info("Program Started - " + DateTime.Now);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try{
                MainForm f = new MainForm();
                Application.Run(f);

                if (f.HasError){
                    LogError(f);
                }
            }
            catch (Exception ex){
                if (ex.InnerException != null){
                    Log.Error(ex.InnerException);
                    if (IsNetworkRelated(ex.InnerException.Message)){
                        ShowNetworkRelatedMessage();
                    }
                    else{
                        MessageBox.Show(ex.InnerException.Message, "Error", MessageBoxButtons.OK);
                    }

                    if (ex.InnerException.InnerException != null)
                    {
                        Log.Error(ex.InnerException.InnerException);
                        if (IsNetworkRelated(ex.InnerException.InnerException.Message)){
                            ShowNetworkRelatedMessage();
                        }
                        else{
                            MessageBox.Show(ex.InnerException.InnerException.Message, "Error", MessageBoxButtons.OK);
                        }
                    }
                }
                else{
                    Log.Error(ex);
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                }
                throw;
            }
            Log.Info("Program Ended - " + DateTime.Now);
        }

        private static void LogError(MainForm f){
            if (f.Exception.InnerException != null){
                Log.Error(f.Exception.InnerException);
                if (IsNetworkRelated(f.Exception.InnerException.Message)){
                    ShowNetworkRelatedMessage();
                }
                else{
                    MessageBox.Show(f.Exception.InnerException.Message, "Error",
                        MessageBoxButtons.OK);
                }
            }
            else{
                Log.Error(f.Exception);
                MessageBox.Show(f.Exception.Message, "Error", MessageBoxButtons.OK);
            }
            if (f.Exception.InnerException.InnerException != null){
                Log.Error(f.Exception.InnerException.InnerException);
                if (IsNetworkRelated(f.Exception.InnerException.InnerException.Message)){
                    ShowNetworkRelatedMessage();
                }
                else{
                    MessageBox.Show(f.Exception.InnerException.InnerException.Message, "Error",
                        MessageBoxButtons.OK);
                }
            }
        }

        private static bool IsNetworkRelated(string message){
            return (message.Contains("A network-related or instance-specific error")
                   ||
                   message.Contains("The provider did not return")
                   ||
                   message.Contains("The semaphore timeout period has expired"));
        }

        private static void ShowNetworkRelatedMessage(){
            MessageBox.Show(GetNetworkRelatedMessage(), "Error Connecting to database.", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static string GetNetworkRelatedMessage(){
            return
                "Cannot connect to Database server. Make sure the IP address is correct or the instance of SQL server is running";
        }
    }
}
