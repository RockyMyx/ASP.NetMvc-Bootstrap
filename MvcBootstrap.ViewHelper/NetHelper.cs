using System.IO;
using System.Net;
using System.Net.Sockets;

public class NetHelper
{
    public static string GetPrivateIPAddress()
    {
        string localIP = null;
        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
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
