using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StellarAnchor.Models;
using stellar_dotnet_sdk;
using System.Net.Http;

namespace StellarAnchor.Controllers{
    public class HomeController : Controller{
        private static readonly HttpClient client = new HttpClient();

        public IActionResult Index(){
            return View();
        }

        public IActionResult Privacy(){
            return View();
        }

        public async Task<IActionResult> DoThing(){
            Network.UseTestNetwork();
            var server = new Server("https://horizon-testnet.stellar.org");

            var issuePair = KeyPair.FromSecretSeed("SDXT6S752NXEZTZWNLZAWNIN4TGPHKWD3NS3RJSZPZ34EVZJW52C4AVG");
            var basePair = KeyPair.FromSecretSeed("SAWAFZCYEQPEYKGGNTHKHF5WCSYPXL5YI27M63YNWKJLTLCQY5M356CR");

            //Send 100 XLM from the issuing account to the base account
            try{
                await MakePayment(issuePair, basePair.AccountId, "GCFXHS4GXL6BVUCXBWXGTITROWLVYXQKQLF4YH5O5JT3YZXCYPAFBJZB");
            }
            catch(Exception e){
                Console.WriteLine(e);
            }

            return Json("Did the thing");
        }

        private async Task<bool> MakePayment(KeyPair issuer, string baseId, string destination){
            var values = new Dictionary<string, string> {
               { "id", "unique_payment_id" },
               { "amount", "1" },
               { "asset_code", "USD" },
               { "asset_issuer", issuer.AccountId },
               { "destination", destination },
               { "source", baseId }
            };
            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("http://localhost:8006/payment", content);
            var responseString = await response.Content.ReadAsStringAsync();

            Console.WriteLine(responseString);
            return true;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(){
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
