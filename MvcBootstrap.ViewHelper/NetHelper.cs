using System.IO;
using System.Net;
using System.Net.Sockets;

public class NetHelper
{
    public static string GetPrivateIPAddress()
    {
        string localIP = null;
        IPAddress[] addresses = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
        foreach (IPAddress address in addresses)
        {
            if (address.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = address.ToString();
                break;
            }
        }

        return localIP;
    }

    public static string GetPublicIPAddress()
    {
        var request = (HttpWebRequest)WebRequest.Create("http://ifconfig.me");
        request.UserAgent = "curl";
        request.Method = "GET";
        string ipAddress;
        using (WebResponse response = request.GetResponse())
        {
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                ipAddress = reader.ReadToEnd();
            }
        }

        return ipAddress.Replace("\n", "");
    }
}
