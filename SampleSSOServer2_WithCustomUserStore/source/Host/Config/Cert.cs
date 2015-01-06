﻿/*
 * Copyright 2014 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace SampleSSOServer2.Config
{
    using System.IO;
    using System.Security.Cryptography.X509Certificates;

    public class Cert
    {
        public static X509Certificate2 Load()
        {
            return new X509Certificate2(
                ////string.Format(@"{0}\bin\identityServer\idsrv3test.pfx", AppDomain.CurrentDomain.BaseDirectory), "idsrv3test");
               string.Format(@"C:\Projects\Extra\Thinktecture.IdentityServer.v3.Samples-master\source\Certificates\idsrv3test.pfx"), "idsrv3test");
            
            ////var assembly = typeof(Cert).Assembly;
            ////using (var stream = assembly.GetManifestResourceStream("Thinktecture.IdentityServer.Host.Config.idsrv3test.pfx"))
            ////{
            ////    return new X509Certificate2(ReadStream(stream), "idsrv3test");
            ////}
        }

        private static byte[] ReadStream(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}