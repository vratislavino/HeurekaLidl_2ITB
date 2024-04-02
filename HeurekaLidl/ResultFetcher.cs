using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeurekaLidl
{
    public class ResultFetcher
    {
        static Random rand = new Random();

        public async Task<EshopFetchData> GetPriceFromAlza(string productId)
        {
            await Task.Delay(1000);

            return new EshopFetchData() { name = "Alza", price = 1999 };
        }

        public async Task<EshopFetchData> GetPriceFromCzc(string productId)
        {
            await Task.Delay(700);

            return new EshopFetchData() { name = "Czc", price = 1599};
        }

        public async Task<EshopFetchData> GetPriceFromMironet(string productId)
        {
            await Task.Delay(2000);

            return new EshopFetchData() { name = "Mironet", price = 2599 };
        }

        public async Task FetchData(Eshop eshop)
        {
            await Task.Delay(eshop.responseTime);
            int cena = rand.Next(1000, 3000);
            eshop.price = cena;
        }
    }
}
