using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Proje.Models;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using static Proje.Models.WeatherViewModel;

namespace Proje.Controllers
{
    public class DefaultController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var client1 = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://booking-com.p.rapidapi.com/v1/metadata/exchange-rates?currency=TRY&locale=en-gb"),
                Headers =
    {
        { "X-RapidAPI-Key", "2ce6a48157msh92dd410e475d903p1ecdf8jsn707a1ffacec7" },
        { "X-RapidAPI-Host", "booking-com.p.rapidapi.com" },
    },
            };
            using (var response = await client1.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                var values1 = JsonConvert.DeserializeObject<ExchangeViewModel>(body);

                var liste1 = values1.exchange_rates.ToList();

                var dolar = liste1.Where(x => x.currency == "USD").Select(x => x.exchange_rate_buy).FirstOrDefault();

                ViewBag.dolar = 1 / Convert.ToDecimal(dolar) * 100000000;

                var euro = liste1.Where(x => x.currency == "EUR").Select(x => x.exchange_rate_buy).FirstOrDefault();

                ViewBag.euro = 1 / Convert.ToDecimal(euro) * 100000000;

                var gbp = liste1.Where(x => x.currency == "GBP").Select(x => x.exchange_rate_buy).FirstOrDefault();

                ViewBag.sterlin = 1 / Convert.ToDecimal(gbp) * 100000000;
            }




            var request1 = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://yahoo-weather5.p.rapidapi.com/weather?location=sunnyvale&format=json&u=c"),
                Headers =
    {
        { "X-RapidAPI-Key", "2ce6a48157msh92dd410e475d903p1ecdf8jsn707a1ffacec7" },
        { "X-RapidAPI-Host", "yahoo-weather5.p.rapidapi.com" },
    },
            };
            using (var response1 = await client1.SendAsync(request1))
            {
                response1.EnsureSuccessStatusCode();
                var body1 = await response1.Content.ReadAsStringAsync();

                var weatherViewModel = JsonConvert.DeserializeObject<WeatherViewModel>(body1);

                ViewBag.v = weatherViewModel.current_observation.condition.temperature;
            }




           
            var client = new HttpClient();
            var request2 = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://exchange-rates-real-time-and-historical-data.p.rapidapi.com/ExchangeTimeSeries"),
                Headers =
    {
        { "X-RapidAPI-Key", "2ce6a48157msh92dd410e475d903p1ecdf8jsn707a1ffacec7" },
        { "X-RapidAPI-Host", "exchange-rates-real-time-and-historical-data.p.rapidapi.com" },
    },
                Content = new StringContent("{\r\n    \"start_date\": \"2023-12-08\",\r\n    \"end_date\": \"2023-12-22\",\r\n    \"currency_base\": \"USD\",\r\n    \"currencies_compare\": \"TRY\"\r\n}")
                {
                    Headers =
        {
            ContentType = new MediaTypeHeaderValue("application/json")
        }
                }
            };
            using (var response2 = await client.SendAsync(request2))
            {
                response2.EnsureSuccessStatusCode();
                var body2 = await response2.Content.ReadAsStringAsync();
                
            var data = JsonConvert.DeserializeObject<ExchangeHistoricalViewModel>(body2);

                ViewBag.historical = data.Rates;

                var deger = JsonConvert.SerializeObject(data.Rates.OrderByDescending(x => x.Value.TRY).Take(3));


                ViewBag.MyJson = deger;
            }

            return View();

        }

    }
}





