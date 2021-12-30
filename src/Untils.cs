using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace NordVPNdotnet
{
    /// <summary>
    /// extensions
    /// </summary>
    public class Untils
    {
        /// <summary>
        /// get now ip address
        /// </summary>
        public static string CurrentIp
        {
            get
            {
                try
                {
                    WebClient webClient= new WebClient();
                    return webClient.DownloadString("https://api.ipify.org/?format=text");
                }
                catch
                {
                    return "";
                }
            }
        }
        /// <summary>
        /// check internet is connected or not using <see cref="PingReply"/> to google.com
        /// </summary>
        public static bool IsConnectedToInternet
        {
            get
            {
                Ping p = new Ping();
                try
                {
                    PingReply reply = p.Send("google.com", 3000); // send ping to google
                    return reply.Status == IPStatus.Success ? true : false;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    p.Dispose();
                }
            }
        }
    }
}
