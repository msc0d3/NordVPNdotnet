using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NordVPNdotnet.Command;
using NordVPNdotnet.Support.Wait;
using NordVPNdotnet.Support;
using System.Threading;

namespace NordVPNdotnet.Remote
{
    /// <summary>
    /// Main remote nord class
    /// </summary>
    public class RemoteNord : IConnectContext, IDisposable
    {
        /// <summary>
        /// <see cref="IConnect"/> implementation
        /// </summary>
        private IConnect connect;
        /// <summary>
        /// Current server
        /// </summary>
        private NordModel.ServerModel CurrentServer;
        /// <summary>
        /// connected status
        /// </summary>
        public bool IsConnected => connect.IsConnected;

        public RemoteNord(string NordVpnFolder)
        {
            Command.NordVpnFolder = NordVpnFolder;
        }
        public IConnect FindServerBy(NordModel.ServerModel Server)
        {
            CurrentServer = Server;
            connect = new ChangeIpRmt(CurrentServer);
            return connect.FindServerBy(Server);
        }
        public IConnect FindServerBy(string groupname)
        {
            connect = new ChangeIpRmt(groupname);
            return connect.FindServerBy(groupname);
        }
        public void Dispose()
        {
            connect.Dispose();
        }

        public NordModel.ConnectionInfo WaitConnect(TimeSpan timeout)
        {
            return connect.WaitConnect(timeout);
        }

        private class ChangeIpRmt : IConnect,IDisposable
        {
            private NordModel.ServerModel CurrentServer;
            private string group_name;
            private Command NordCmd;
            private string CurrentIp = string.Empty;
            public ChangeIpRmt(NordModel.ServerModel serverModel)
            {
                CurrentServer = serverModel;
                InitNordVpnProcess();
            }
            public ChangeIpRmt(string groupname)
            {
                group_name = groupname;
            }
            public IConnectWait Connect()
            {
                if (CurrentServer == null && string.IsNullOrEmpty(group_name))
                {
                    throw new ArgumentNullException("group_name");
                }
                if(CurrentServer == null && !string.IsNullOrEmpty(group_name))
                {
                    NordCmd.ExecuteCMD($"nordvpn -c -g {group_name}");
                }
                if(CurrentServer != null)
                {
                    string command = $"nordvpn -c -n \"{CurrentServer.Name}\"";
                    NordCmd.ExecuteCMD(command);
                }
                else
                {
                    throw new ArgumentNullException("CurrentServer");
                }
                return this;
            }

            public void Disconnect()
            {
                NordCmd.ExecuteCMD("nordvpn -d");
            }
            public IConnect FindServerBy(NordModel.ServerModel Server)
            {
                CurrentServer = Server;
                return this;
            }

            public IConnect FindServerBy(string groupname)
            {
                group_name = groupname;
                CurrentServer = null;
                return this;
            }

            public NordModel.ConnectionInfo WaitConnect(TimeSpan timeout)
            {
                NordModel.ConnectionInfo info = new NordModel.ConnectionInfo();
                Stopwatch stopwatch = new Stopwatch();
                try
                {
                    stopwatch.Start();
                    NordWait wait = new NordWait(this,timeout);
                    info.isConnected = wait.Until(x => x.IsConnected);
                    //if(Untils.CurrentIp == CurrentIp || string.IsNullOrEmpty(CurrentIp))
                    //{
                        //info.isConnected = false;
                    //}
                }
                catch(Exception ex)
                {
                    info.isConnected = false;
                    info.Error = ex;
                }
                stopwatch.Stop();
                info.ElapsedTime = stopwatch.Elapsed;
                return info;
            }

            bool IConnectWait.IsConnected => Untils.IsConnectedToInternet;

            public void Dispose()
            {
                InitNordVpnProcess();
                Disconnect();
                NordCmd.Dispose();
            }
            private void InitNordVpnProcess()
            {
                Process process = new Process();
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    WorkingDirectory = Command.NordVpnFolder
                };
                process.Start();
                NordCmd = new Command(process);
            }
        }
    }
}
