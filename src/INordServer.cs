using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordVPNdotnet
{
    /// <summary>
    /// Interface describing a class designed to wait for a condition.
    /// </summary>
    public interface INordServer:INordServerContext
    {
        /// <summary>
        /// Sort result <seealso cref="List{NordModel.ServerModel}"/> By <paramref name="condition"/> delegate
        /// </summary>
        /// <param name="condition"></param>
        /// <returns><see cref="List{NordModel.NordGroup}"/> Sorted !</returns>
        List<NordModel.ServerModel> SortBy(Func<NordModel.ServerModel, bool> condition);
        /// <summary>
        /// Sort result <seealso cref="List{NordModel.NordGroup}"/> By <paramref name="condition"/> delegate
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        List<NordModel.NordGroup> SortBy(Func<NordModel.NordGroup, bool> condition);
        /// <summary>
        /// Sort result <seealso cref="List{NordModel.Technology}"/> By <paramref name="condition"/> delegate
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        List<NordModel.Technology> SortBy(Func<NordModel.Technology, bool> condition);
        /// <summary>
        /// Get raw result !
        /// </summary>
        /// <returns>raw list <see cref="NordModel.ServerModel"/></returns>
        List<NordModel.ServerModel> Raw();
    }
    /// <summary>
    /// Nord Server Context interface
    /// </summary>
    public interface INordServerContext
    {
        /// <summary>
        /// Get Nord Available Servers
        /// </summary>
        /// <param name="limit">limit servers to get</param>
        /// <returns><see cref="INordServer"/> implementation</returns>
        INordServer GetServers(int limit = 5000);
        /// <summary>
        /// Get Nord Available Servers by <see cref="ServerContext.Filters"/> support !
        /// </summary>
        /// <param name="filter">filter param</param>
        /// <param name="limit">limit servers to get</param>
        /// <returns></returns>
        INordServer GetServers(ServerContext.Filters filter, int limit);
        /// <summary>
        /// get countries
        /// </summary>
        /// <returns>list <see cref="NordModel.CountrieModel"/></returns>
        List<NordModel.CountrieModel> GetCountries();
        /// <summary>
        /// get groups
        /// </summary>
        /// <returns></returns>
        List<NordModel.NordGroup> GetGroups();
        /// <summary>
        /// Get Technologies
        /// </summary>
        /// <returns></returns>
        List<NordModel.Technology> GetTechnologies();
    }
}
