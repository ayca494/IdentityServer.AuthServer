using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

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
               
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.OpenId(), //token döndüğünde kullanıcın id'si olmalı(subject id) 
                new IdentityResources.Profile()  //kullanıcı ile ilgili claim tutulur
            };
        }

        public static IEnumerable<TestUser> GetUsers()
        {
            return new List<TestUser>()
            {
                new TestUser{SubjectId="1", Username="aycatrkmn", Password="Passw0rd.",Claims= new List<Claim>(){
                new Claim("given_name","Ayça"),
                new Claim("family_name","Türkmen") }},

                new TestUser{SubjectId="2", Username="ferideclk", Password="Passw0rd.",Claims= new List<Claim>(){
                new Claim("given_name","Feride"),
                new Claim("family_name","Çolak") }},
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
                },
                new Client()
                {
                    ClientId="Client1-Mvc",
                    ClientName = "Client 1 app mvc yugulaması",
                    ClientSecrets= new[]{new Secret("secret".Sha256())},
                    AllowedGrantTypes=GrantTypes.Hybrid,
                    RedirectUris = new List<string>{"https://localhost:7131/sign-oidc" },  //token alma işini gerçekleştiren url,cookie 
                    AllowedScopes = {IdentityServerConstants.StandardScopes.OpenId,IdentityServerConstants.StandardScopes.Profile} //Client hangi izinlere sahip olacak 
                    //sabit olarak yazdık izinleri fakat "openid" yazsak da olur. Ya da "profile" :
                    //AllowedScopes = {"openid","profile"},
                }
            };
        }
    }
}
