using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordVPNdotnet
{
    public class NordModel
    {
        /// <summary>
        /// Nord server infomations
        /// </summary>
        public class ServerModel
        {
            /// <summary>
            /// name of server , example : Canada #944
            /// </summary>
            public string Name
            {
                get
                {
                    return _name;
                }
                set
                {
                    _name = value;
                }
            }
            private string _name;
            /// <summary>
            /// ip address , example : 172.83.40.21
            /// </summary>
            public string Ip
            {
                get
                {
                    return _ip;
                }
                set
                {
                    _ip = value;
                }
            }
            private string _ip;
            /// <summary>
            /// ping load of server , example : 50%
            /// </summary>
            public int LoadPercent
            {
                get
                {
                    return _loadPercent;
                }
                set
                {
                    _loadPercent = value;
                }
            }
            private int _loadPercent;
            /// <summary>
            /// status of server , example : online
            /// </summary>
            public string Status
            {
                get
                {
                    return _status;
                }
                set
                {
                    _status = value;
                }
            }
            private string _status;
            /// <summary>
            /// host name , exampe : ca944.nordvpn.com
            /// </summary>
            public string HostName
            {
                get
                {
                    return _hostname;
                }
                set
                {
                    _hostname = value;
                }
            }
            private string _hostname;
        }
        /// <summary>
        /// nord country infomations
        /// </summary>
        public class CountrieModel
        {
            /// <summary>
            /// name of sountry , example : Vietnam
            /// </summary>
            public string Name
            {
                get
                {
                    return _name;
                }
                set
                {
                    _name = value;
                }
            }
            private string _name;
            /// <summary>
            /// code of country , example VN
            /// </summary>
            public string Code
            {
                get
                {
                    return _code;
                }
                set
                {
                    _code = value;
                }
            }
            private string _code;
            /// <summary>
            /// id of country , example : 234
            /// </summary>
            public int Id
            {
                get;set;
            }
        }
        /// <summary>
        /// connection infomations
        /// </summary>
        public class ConnectionInfo
        {
            /// <summary>
            /// connected or not
            /// </summary>
            public bool isConnected
            {
                get;set;
            }
            /// <summary>
            /// exception if connect error
            /// </summary>
            public Exception Error
            {
                get;set;
            }
            /// <summary>
            /// Elapsed Time
            /// </summary>
            public TimeSpan ElapsedTime
            {
                get;set;
            }
        }
        public class NordGroup
        {
            public int id {  get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public string title { get; set; }
            public string identifier { get; set; }
        }
        public class Technology
        {
            public int id { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public string name { get; set; }
            public string identifier { get; set; }
        }
    }
}
