using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordVPNdotnet
{
    /// <summary>
    /// server context
    /// </summary>
    public class ServerContext
    {
        /// <summary>
        /// servers seach filters
        /// </summary>
        public class Filters
        {
            /// <summary>
            /// 
            /// </summary>
            public string PatternName { get; set; }
            /// <summary>
            /// filer type <see cref="FilterType"/>
            /// </summary>
            public FilterType Filter { get; set; }
            public enum FilterType
            {
                Bycountry,
                Recommendation,
                RecommendationBy
            }
            /// <summary>
            /// filter result by country
            /// </summary>
            /// <param name="countryid"></param>
            /// <returns></returns>
            public static Filters ByCountry(string countryid)
            {
                return new Filters
                {
                    PatternName = countryid,
                    Filter = FilterType.Bycountry
                };
            }
            /// <summary>
            /// recommend servers
            /// </summary>
            /// <returns></returns>
            public static Filters Recommendation()
            {
                return new Filters() { Filter = FilterType.Recommendation };
            }
            /// <summary>
            /// recommend servers by identifier name , example : openvpn_udp , openvpn_tcp , pptp , ...v.v.v
            /// </summary>
            /// <param name="identifier_name"></param>
            /// <returns></returns>
            public static Filters RecommendationBy(string identifier_name)
            {
                return new Filters
                {
                    PatternName = identifier_name,
                    Filter = FilterType.RecommendationBy
                };
            }
        }
    }
}
