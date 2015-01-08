
namespace SampleSSOServer2.Config
{
    using System.IO;
    using System.Security.Cryptography.X509Certificates;

    public class Certficate
    {
        public static X509Certificate2 GetCertficate()
        {
            return new X509Certificate2(
               string.Format(@"C:\Projects\Extra\Thinktecture.IdentityServer.v3.Samples-master\source\Certificates\idsrv3test.pfx"), "idsrv3test");
        }
    }
}