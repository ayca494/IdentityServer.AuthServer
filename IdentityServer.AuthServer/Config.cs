using IdentityServer4.Models;

namespace IdentityServer.AuthServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource("resource_api1"){
                    Scopes = {"api1.read","api1.write","api1.update"},
                    ApiSecrets = new []{new Secret("secretapi1".Sha256())}
                },
                new ApiResource("resource_api2"){
                    Scopes= {"api2.read","api2.write","api2.update"},
                    ApiSecrets=new[] {new Secret("secretapi2".Sha256())}
                }
            };
        }
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>()
            {
                new ApiScope("api1.read", "API 1 için okuma izni"),
                new ApiScope("api1.write", "API 1 için yazma izni"),
                new ApiScope("api1.update", "API 1 için güncelleme izni"),
                new ApiScope("api2.read", "API 2 için okuma izni"),
                new ApiScope("api2.write", "API 2 için yazma izni"),
                new ApiScope("api2.update", "API 2 için güncelleme izni")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client()
                {
                    ClientId="Client1",
                    ClientName ="Client 1 app uygulaması",
                    ClientSecrets=new[]{new Secret("secret".Sha256())}, //hashler geriye dönülmez. Gelen değeri karşılaştırmak istersek onun da hashini alıp öyle karşılaştırız.
                    AllowedGrantTypes=GrantTypes.ClientCredentials, //Dönen tokenda kullanıcı ile ilgili bilgi olmayacak sadece ilgili clientin apiye bağlanması ile ilgili izinler olacak.
                    AllowedScopes = {"api1.read"} //Clientın hangi apiler için hangi izinleri olacağını belirtiyoruz
                },
                new Client()
                {
                    ClientId="Client2",
                    ClientName="Client 2 app uygulaması",
                    ClientSecrets = new[]{new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes={"api1.read","api2.write","api2.update", "api1.update" }
                }
            };
        }
    }
}
