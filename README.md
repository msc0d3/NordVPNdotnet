<a href="https://tienichmmo.net"><img src="https://user-images.githubusercontent.com/44217992/147784913-a4d9b218-d83c-4cdc-a9ba-e20d076bf976.png" align="top" height="200" /></a>
## NordVPNdotnet
Nord Vpn Library for .NET , using for automation softwares
## Requirements

- [Newtonsoft.Json.dll](https://github.com/JamesNK/Newtonsoft.Json)

Json library , using for api

## Documentation

❗All api using in this project, i get it from [sleeplessbeastie.eu](https://sleeplessbeastie.eu/2019/02/18/how-to-use-public-nordvpn-api/)

if you need update more feature, you can visit that page, and check that document !


 - ✅ Create new Nordserver

 ```
 RemoteNordServer nordServer = new RemoteNordServer();
 ```
- ⬇️ Get available countries

```js
List<NordModel.CountrieModel> countries = nordServer.GetCountries();
```
- ⬇️ Get available techonologies

```js
List<NordModel.CountrieModel> countries = nordServer.GetTechnologies();
```
#### ⚠️ Get available servers
| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `limit` | `int` | **Required**. limit servers to get |
- Get servers by Recommendation
```js
var servers = nordServer.GetServers(ServerContext.Filters.Recommendation(), 100);
//100 is limit Parameter
```
#### OR
```js
var servers = nordServer.GetServers(ServerContext.Filters.Recommendation(), 100).SortBy(Func<>);
// Func is delegate method to sort servers
// example .SortBy(x => x.LoadPercent <= 40); // sort servers by LoadPercent <= 40%
//100 is limit Parameter
```
- get servers by limit

```js
var servers = nordServer.GetServers(1000);
/// 1000 is limit servers
```
- get servers by country
Parameter `countryid` is country id extract from nordServer.GetCountries();
```js
var servers = nordServer.GetServers(ServerContext.Filters.ByCountry(countryid),100);
/// 1000 is limit servers
// example .GetServers(ServerContext.Filters.ByCountry("234"),100); // 234 is countryid of Vietnam
```
- get servers by country
Parameter `identifier_name` is identifier_name , extract from nordServer.GetTechnologies() 
or nordServer.GetCountries(); , example : openvpn_udp
```js
var servers = nordServer.GetServers(ServerContext.Filters.RecommendationBy("openvpn_udp"), 100);
/// 1000 is limit servers
/// openvpn_udp is identifier_name
```
- ✅ Create new Nord Remote
Parameter `NordVpnFolder` is NordVPN installed folder
```js
RemoteNord nord = new RemoteNord(NordVpnFolder);
// example : NordVpnFolder is C:\Program Files\NordVPN is NordVPN folder
```
- 🪢 Connect to server
Parameter `Server` is Server to connect , extract from GetServers method

```js
var cn = nord.FindServerBy(servers[1]).Connect();
```
- 🔃 Wait nord connect to server
Parameter `timeout` is time wait connect
```js
NordModel.ConnectionInfo info = cn.WaitConnect(TimeSpan.FromSeconds(60)); // wait 1 minutes
```
##### OR

```js
NordModel.ConnectionInfo info = nord.FindServerBy(servers[1]).Connect().WaitConnect(TimeSpan.FromSeconds(60));
// connect , then wait 1 minutes
```

- ✅ Check ConnectionInfo

`info.ElapsedTime` is elapsed time to connect

`info.Error` is exception ( if connect error )

`info.isConnected` connect status ( `bool` ) , `true` is connect success , `false` is connect failed

- 🔴 Disconnect nord

```js
nord.Dispose();
```

### ❤️ Donate
if this project helpful you , you can donate me at :
## Bank
7778889992001 / MB BANK / NGUYEN DAC TAI
## Paypal : nguyendactaidn@gmail.com or [Link Paypal](https://www.paypal.com/paypalme/nguyendactai)
