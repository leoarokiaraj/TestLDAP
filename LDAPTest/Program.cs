using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LDAPTest
{
    class Program
    {
        private static string LDAPPATH = "LDAP://WIN-HDK5C57D6C7.ig.local/DC=ig,DC=local";

        static void Main(string[] args)
        {
            Program pg = new Program();
            pg.GetAllUsers();
            Console.ReadLine();
        }

        private void GetAllUsers()
        {
            try
            {
                SearchResultCollection results;
                DirectorySearcher ds = null;
                Console.WriteLine(GetCurrentDomainPath());


                //var match2 = LDAPPATH.Substring(LDAPPATH.LastIndexOf(":") + 1, 3);

                //var match3 = LDAPPATH.Substring(1 + 1, 3);


                //Not Working
                //LdapConnection conn = new LdapConnection("WIN-HDK5C57D6C7.ig.local:636");
                //var op = conn.SessionOptions;
                //op.ProtocolVersion = 3;
                //op.SecureSocketLayer = true;
                //op.VerifyServerCertificate += delegate { return true; };

                //conn.AuthType = AuthType.Negotiate;
                //var cred = new NetworkCredential("leo", "Pa$$word1");

                ////conn.Credential = cred;
                //try
                //{
                //    conn.Bind(cred);

                //    if (op.SecureSocketLayer)
                //    {
                //        Console.WriteLine("SSL for encryption is enabled - SSL information:");
                //    }
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.ToString());
                //    throw ex;
                //}

                //Working
                try
                {
                    DirectoryEntry directoryEntry = new DirectoryEntry(LDAPPATH,
                               "test1", "Pa$$word1", AuthenticationTypes.SecureSocketsLayer);

                    //directoryEntry.Options

                    DirectorySearcher searcher = new DirectorySearcher(directoryEntry)
                    {
                        PageSize = int.MaxValue,
                        Filter = "(&(objectCategory=person)(objectClass=user))"
                    };
                    Console.WriteLine("searcher");

                    var result = searcher.FindOne();
                    Console.WriteLine("FindOne");


                    if (result == null)
                    {
                        Console.WriteLine("Nothing");
                    }

                    SearchResultCollection allresilt = searcher.FindAll();
                    Console.WriteLine("searcher " + allresilt.Count);

                    foreach (SearchResult item in allresilt)
                    {
                        Console.WriteLine("item " + item.Properties["name"][0].ToString());

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw ex;
                }



                //var credential = new NetworkCredential("leo", "Pa$$word1", "WIN-HDK5C57D6C7.ig.local:636");
                //var server = "WIN-HDK5C57D6C7.ig.local";
                //// Use round-robin DNS if it's available.
                //var ips = Dns.GetHostAddresses(server);
                //Console.WriteLine("ips  " + ips.Count());
                //foreach (var ip in ips)
                //{
                //    Console.WriteLine("ips  " + ip.ToString());
                //    using (var ldap = new LdapConnection(server))
                //    {
                //        try
                //        {
                //            ldap.Bind(credential);
                //            SearchRequest searchRequest = new SearchRequest
                //                            ("OU=UDUser1",
                //                             "(&(objectClass=user)(uid=leo))",
                //                             System.DirectoryServices.Protocols.SearchScope.Subtree,
                //                             new string[] { "DistinguishedName" });
                //            var response = (SearchResponse)ldap.SendRequest(searchRequest);
                //            string userDN = response.Entries[0].Attributes["DistinguishedName"][0].ToString();

                //        }
                //        catch (Exception ex)
                //        {
                //            Console.WriteLine(ex.ToString());
                //        }
                //    }
                //}


                //Not Working
                //DirectoryEntry de = new DirectoryEntry("LDAP://WIN-HDK5C57D6C7.ig.local:636");
                //de.AuthenticationType = AuthenticationTypes.SecureSocketsLayer;

                //ds = new DirectorySearcher(de);
                //ds.Filter = "(&(objectCategory=User)(objectClass=person))";

                //results = ds.FindAll();
                //Console.WriteLine(results.Count.ToString());

                //foreach (SearchResult sr in results)
                //{
                //     Using the index zero (0) is required!
                //    Console.WriteLine(sr.Properties["name"][0].ToString());
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
           
        }

        private string GetCurrentDomainPath()
        {
            DirectoryEntry de = new DirectoryEntry(LDAPPATH);

            return LDAPPATH;
        }
    }
}
