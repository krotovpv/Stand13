using OPCAutomation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stand13
{
    public static class ConnectToOPC
    {
        /// <summary>
        /// Имя OPC сервера
        /// </summary>
        readonly static string Stand13_OPCServerName = "Matrikon.OPC.Simulation.1";
        //readonly static string Stand13_OPCServerName = "OMRON.OpenDataServer.1";
        /// <summary>
        /// IP адрес хоста где расположен OPC сервер
        /// </summary>
        readonly static string Stand13_OPCServerHost = "localhost";

        static OPCServer Stand13_OPCServer = new OPCServer();
        static OPCGroup Stand13_OPCGroup;

        /// <summary>
        /// Сторожевой таймер
        /// </summary>
        public static System.Timers.Timer WatchDog = new System.Timers.Timer(3000);


        static ConnectToOPC()
        {
            WatchDog.Elapsed += WatchDog_Elapsed;
        }

        private static void WatchDog_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if ((OPCServerState)Stand13_OPCServer.ServerState != OPCServerState.OPCRunning)
                    Connect();
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                Connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Подключение к OPC серверу
        /// </summary>
        public static void Connect()
        {
            try
            {
                Stand13_OPCServer.Connect(Stand13_OPCServerName, Stand13_OPCServerHost);
                Stand13_OPCGroup = Stand13_OPCServer.OPCGroups.Add("Stand13_Group");
                Stand13_OPCGroup.IsActive = true;
                Stand13_OPCGroup.IsSubscribed = true;
                Stand13_OPCGroup.UpdateRate = 500;
                Tags.AddTagsToOPCGroup(Stand13_OPCGroup);

                WatchDog.Start();
            }
            catch (Exception)
            {
                WatchDog.Stop();
                Thread.Sleep(2000);
                Connect();
            }
        }

        /// <summary>
        /// Разрыв соединения
        /// </summary>
        public static void Disconnect()
        {
            WatchDog.Stop();
            Stand13_OPCGroup.IsSubscribed = false;
            Stand13_OPCGroup.IsActive = false;
            Stand13_OPCServer.OPCGroups.RemoveAll();
            Stand13_OPCServer.Disconnect();
        }
    }
}
