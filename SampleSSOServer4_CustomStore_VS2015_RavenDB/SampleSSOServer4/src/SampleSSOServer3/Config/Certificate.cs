namespace SampleSSOServer4.Config
{
    using System.Security.Cryptography.X509Certificates;

    public class Certificate
    {
        public static X509Certificate2 GetCertificate()
        {
            return new X509Certificate2(
               string.Format(@"C:\Projects\Extra\Thinktecture.IdentityServer.v3.Samples-master\source\Certificates\idsrv3test.pfx"), "idsrv3test");
        }
    }
}