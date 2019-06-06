using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClaimsConsole_Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Setup();
            CheckCompatibility();

            CheckNewClaimsUsage();

            Console.ReadLine();
        }

        private static void Setup()
        {
            IList<Claim> claimCollection = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Fagner Gomes")
                , new Claim(ClaimTypes.Country, "BH")
                , new Claim(ClaimTypes.Gender, "M")
                , new Claim(ClaimTypes.Surname, "Egidio")
                , new Claim(ClaimTypes.Email, "fagner.gomes@me.com")
                , new Claim(ClaimTypes.Role, "TI")
            };

            //ClaimsIdentity claimsIdentity = new ClaimsIdentity(claimCollection, "My e-commerce website");

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claimCollection, "My e-commerce website", ClaimTypes.Email, ClaimTypes.Role);


            Console.WriteLine("Usuário está autenticado? {0} \n", claimsIdentity.IsAuthenticated ? "Sim" : "Não");

            ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);
            Thread.CurrentPrincipal = principal;
        }

        private static void CheckCompatibility()
        {
            IPrincipal currentPrincipal = Thread.CurrentPrincipal;

            Console.WriteLine("Name Identity: {0} \n", currentPrincipal.Identity.Name);

            Console.WriteLine("Usuário é do perfil Ti? {0} \n", currentPrincipal.IsInRole("IT") ? "Sim" : "Não");

        }

        private static void CheckNewClaimsUsage()
        {
            //ClaimsPrincipal currentClaimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
            //ou podemos declarar assim
            ClaimsPrincipal currentClaimsPrincipal = ClaimsPrincipal.Current;


            Claim nameClaim = currentClaimsPrincipal.FindFirst(ClaimTypes.Name);
            Claim Cidade = currentClaimsPrincipal.FindFirst(ClaimTypes.Country);
            Console.WriteLine("Nome: {0}\n Cidade: {1}\n", nameClaim.Value, Cidade.Value);

            //Ou podemos usar o foreach
            foreach (ClaimsIdentity ci in currentClaimsPrincipal.Identities)
            {
                Console.WriteLine("\n{0}",ci.Name);
            }
        }
    }
}
