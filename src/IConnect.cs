using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace NordVPNdotnet
{
    /// <summary>
    /// Connect manager interface
    /// </summary>
    public interface IConnect: IConnectContext, IConnectWait
    {
        /// <summary>
        /// connect to server
        /// </summary>
        /// <returns><see cref="IConnectWait"/></returns> implementation
        IConnectWait Connect();
        /// <summary>
        /// disconnect
        /// </summary>
        void Disconnect();
        /// <summary>
        /// Disconnect an clean temp data
        /// </summary>
        void Dispose();
    }
    /// <summary>
    /// <see cref="IConnect"/> Context
    /// </summary>
    public interface IConnectContext:IConnectWait
    {
        /// <summary>
        /// Find server
        /// </summary>
        /// <param name="Server"></param>
        /// <returns></returns>
        IConnect FindServerBy(NordModel.ServerModel Server);
        /// <summary>
        /// find server by group name , country name, technology name
        /// </summary>
        /// <param name="groupname"></param>
        /// <returns></returns>
        IConnect FindServerBy(string groupname);
    }
    /// <summary>
    /// wait connect interface
    /// </summary>
    public interface IConnectWait
    {
        /// <summary>
        /// wait internet connected
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        NordModel.ConnectionInfo WaitConnect(TimeSpan timeout);
        /// <summary>
        /// internet connect status
        /// </summary>
        bool IsConnected {  get; }
    }
}
