using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NordVPNdotnet.Remote
{
    /// <summary>
    /// An implementation of the <see cref="INordServerContext;"/>
    /// configured on the fly.
    /// </summary>
    public class RemoteNordServer : INordServerContext
    {
        private INordServer _server;
        public RemoteNordServer()
        {
            _server = new NordServer();
        }

        public List<NordModel.CountrieModel> GetCountries()
        {
            return _server.GetCountries();
        }

        public List<NordModel.NordGroup> GetGroups()
        {
            return _server.GetGroups();
        }

        public INordServer GetServers(int limit = 5000)
        {
            return _server.GetServers(limit);
        }
        public INordServer GetServers(ServerContext.Filters filter, int limit)
        {
            return _server.GetServers(filter, limit);
        }

        public List<NordModel.Technology> GetTechnologies()
        {
            return _server.GetTechnologies();
        }

        private class NordServer : INordServer
        {
            private HttpClient httpClient;
            private readonly string _nordApi_endpoint = "https://api.nordvpn.com";
            private readonly string _get_servers_path = "/v1/servers";
            private readonly string _get_countries_path = "/v1/servers/countries";
            List<NordModel.ServerModel> CurentServers;
            public NordServer()
            {
                httpClient = new HttpClient();
                CurentServers = new List<NordModel.ServerModel>();
            }
            public INordServer GetServers(int limit = 5000)
            {
                List<NordModel.ServerModel> servers = new List<NordModel.ServerModel>();
                try
                {
                    HttpResponseMessage response = httpClient.GetAsync(_nordApi_endpoint + _get_servers_path + "?limit=" + limit.ToString()).Result;
                    JArray server_array = JArray.Parse(response.Content.ReadAsStringAsync().Result);
                    List<NordServerJson> Servers = JsonConvert.DeserializeObject<List<NordServerJson>>(server_array.ToString());
                    foreach(NordServerJson sv in Servers)
                    {
                        servers.Add(new NordModel.ServerModel
                        {
                            HostName = sv.hostname,
                            Ip = sv.station,
                            LoadPercent = sv.load,
                            Name = sv.name,
                            Status = sv.status
                        });
                    }
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                CurentServers.Clear();
                CurentServers = servers.ToList();
                return this;
            }
            public INordServer GetServers(ServerContext.Filters filter, int limit)
            {
                string url = string.Empty;
                List<NordModel.ServerModel> servers = new List<NordModel.ServerModel>();
                if (filter.Filter == ServerContext.Filters.FilterType.Bycountry)
                {
                    url = _nordApi_endpoint + $"/v1/servers?filters[country_id]={filter.PatternName}&limit={limit}";
                }
                else if(filter.Filter == ServerContext.Filters.FilterType.Recommendation)
                {
                    url = _nordApi_endpoint + $"/v1/servers/recommendations?limit={limit}";
                }
                else
                {
                    url = _nordApi_endpoint + $"/v1/servers/recommendations?filters[identifier]={filter.PatternName}&limit={limit}";
                }
                if (string.IsNullOrEmpty(url))
                {
                    throw new ArgumentNullException(url);
                }
                HttpResponseMessage response = new HttpResponseMessage();
                try
                {
                    response = httpClient.GetAsync(url).Result;
                    JArray server_array = JArray.Parse(response.Content.ReadAsStringAsync().Result);
                    List<NordServerJson> Servers = JsonConvert.DeserializeObject<List<NordServerJson>>(server_array.ToString());
                    foreach (NordServerJson sv in Servers)
                    {
                        servers.Add(new NordModel.ServerModel
                        {
                            HostName = sv.hostname,
                            Ip = sv.station,
                            LoadPercent = sv.load,
                            Name = sv.name,
                            Status = sv.status
                        });
                    }
                    server_array.Clear(); // clear server array
                    Servers.Clear();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    response.Dispose(); // dispose temp response
                    url = string.Empty;
                }
                CurentServers.Clear(); // refresh current servers
                CurentServers = servers;
                return this;
            }
            public List<NordModel.CountrieModel> GetCountries()
            {
                List<NordModel.CountrieModel> countries = new List<NordModel.CountrieModel>();
                try
                {
                    HttpResponseMessage response = httpClient.GetAsync(_nordApi_endpoint + _get_countries_path).Result;
                    JArray server_array = JArray.Parse(response.Content.ReadAsStringAsync().Result);
                    List<NordCountryJson> Servers = JsonConvert.DeserializeObject<List<NordCountryJson>>(server_array.ToString());
                    foreach (NordCountryJson ct in Servers)
                    {
                        countries.Add(new NordModel.CountrieModel
                        {
                            Name = ct.name,
                            Code = ct.code,
                            Id = ct.id
                        });
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return countries;
            }
            public List<NordModel.ServerModel> SortBy(Func<NordModel.ServerModel, bool> condition)
            {
                List<NordModel.ServerModel> sorted_servers = new List<NordModel.ServerModel>();
                foreach (NordModel.ServerModel server in CurentServers)
                {
                    var result = condition(server);
                    var boolResult = result as bool?;
                    if (boolResult.HasValue && boolResult.Value)
                    {
                        sorted_servers.Add(server);
                    }
                }
                return sorted_servers;
            }
            public List<NordModel.ServerModel> Raw()
            {
                return CurentServers;
            }

            public List<NordModel.NordGroup> GetGroups()
            {
                List<NordModel.NordGroup> groups = new List<NordModel.NordGroup>();
                try
                {
                    HttpResponseMessage response = httpClient.GetAsync(_nordApi_endpoint + "/v1/servers/groups").Result;
                    JArray server_array = JArray.Parse(response.Content.ReadAsStringAsync().Result);
                    groups = JsonConvert.DeserializeObject<List<NordModel.NordGroup>>(server_array.ToString());
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return groups;
            }

            public List<NordModel.Technology> GetTechnologies()
            {
                List<NordModel.Technology> technologies = new List<NordModel.Technology>();
                try
                {
                    HttpResponseMessage response = httpClient.GetAsync(_nordApi_endpoint + "/v1/technologies").Result;
                    JArray server_array = JArray.Parse(response.Content.ReadAsStringAsync().Result);
                    technologies = JsonConvert.DeserializeObject<List<NordModel.Technology>>(server_array.ToString());
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return technologies;
            }

            public List<NordModel.NordGroup> SortBy(Func<NordModel.NordGroup, bool> condition)
            {
                throw new NotImplementedException();
            }

            public List<NordModel.Technology> SortBy(Func<NordModel.Technology, bool> condition)
            {
                throw new NotImplementedException();
            }

            ~NordServer()
            {
                if(httpClient == null)
                {
                    httpClient.Dispose();
                    CurentServers.Clear();
                }
            }
        }
        private class NordServerJson
        {
            public string name {  get; set; }
            public string station { get; set; }
            public string hostname { get; set; }
            public int load {  get; set; }
            public string status { get; set; }
        }
        private class NordCountryJson
        {
            public string name {  get; set; }
            public string code { get; set; }
            public int id { get; set; }
        }
    }
}
