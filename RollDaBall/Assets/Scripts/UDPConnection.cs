using System.Net;
using System.Net.Sockets;

namespace Assets.Scripts
{

    public class UDPConnection
    {
        UdpClient client = new UdpClient();
        IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2000); // endpoint where server is listening (testing localy)

        public UDPConnection()
        {
            client.Connect(ep);
        }

        public byte[] SendBytes(byte[] data)
        {
            client.Send(data, data.Length);
            return client.Receive(ref ep);
        }

    }
}
