using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordVPNdotnet
{
    /// <summary>
    /// remote nord command ( cmd )
    /// </summary>
    public class Command
    {
        /// <summary>
        /// nord vpn folder , example : C:\Program Files\NordVPN
        /// </summary>
        public static string NordVpnFolder;
        private Process cmd;
        public Command(Process session)
        {
            cmd = session;
        }
        /// <summary>
        /// create new cmd process
        /// </summary>
        public static Process DefaultProcess
        {
            get
            {
                Process process = new Process();
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true
                };
                process.Start();
                return process;
            }
        }
        /// <summary>
        /// run cmd
        /// </summary>
        /// <param name="cmdCommand"></param>
        /// <returns></returns>
        public string ExecuteCMD(string cmdCommand)
        {
            string result;
            try
            {
                cmd.StandardInput.WriteLine(cmdCommand);
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
                cmd.WaitForExit();
                string text = cmd.StandardOutput.ReadToEnd();
                result = text;
            }
            catch
            {
                result = null;
            }
            return result;
        }
        public void Dispose()
        {
            if(cmd != null)
            {
                cmd.Dispose();
            }
        }
        ~Command()
        {
            Dispose();
        }
    }
}
