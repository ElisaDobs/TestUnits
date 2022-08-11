using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AyoTestUnits
{
    [TestClass]
    public class TestApi
    {
        private string _url = "https://localhost:44346/api/";
        private string _api_acccount = "Account";
        private string _api_ayo = "Ayo";
        private string _api_audit = "Audit";
        public HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        [TestMethod]
        public void Validate_User()
        {
            using (var client = GetClient())
            {
                string username = "Memory",
                       password = "Test302",
                       endPoint = "ValidateUser";
                Task<HttpResponseMessage> response = client.GetAsync($"{_api_acccount}/{endPoint}?username={username}&password={password}");
                Assert.AreEqual(true, response.Result.IsSuccessStatusCode);
            }
        }

        [TestMethod]
        public void Create_User()
        {
            using (var client = GetClient())
            {
                string username = "andile",
                       lastname = "Maswanganye",
                       password = "Test401",
                       endPoint = "CreateUser";
                Task<HttpResponseMessage> response = client.GetAsync($"{_api_acccount}/{endPoint}?username={username}&lastname={lastname}&password={password}");
                Assert.AreEqual(true, response.Result.IsSuccessStatusCode);
            }
        }

        [TestMethod]
        public void Convert_Metric_To_Imperial_Unit_Rates()
        {
            using (var client = GetClient())
            {
                string unit = "cm",
                       imperial = "",
                       endPoint = "ConvertUnits";
                decimal unitValue = 5;
                int conversion_type = 1;
                Task<HttpResponseMessage> response = client.GetAsync($"{_api_ayo}/{endPoint}?unit={unit}&unitValue={unitValue}&conversion_type={conversion_type}");
                Assert.AreEqual(true, response.Result.IsSuccessStatusCode);
                var responseContent = JsonConvert.DeserializeObject<ResponseMessage>(response.Result.Content.ReadAsStringAsync().Result);
                imperial = responseContent.Result;
                Assert.AreEqual("2inch", imperial);
            }
        }
        [TestMethod]
        public void Convert_Imperial_To_Metric_Unit_Rates()
        {
            using (var client = GetClient())
            {
                string unit = "inch",
                       metric = "",
                       endPoint = "ConvertUnits";
                decimal unitValue = 55;
                int conversion_type = 2;
                Task<HttpResponseMessage> response = client.GetAsync($"{_api_ayo}/{endPoint}?unit={unit}&unitValue={unitValue}&conversion_type={conversion_type}");
                Assert.AreEqual(true, response.Result.IsSuccessStatusCode);
                var responseContent = JsonConvert.DeserializeObject<ResponseMessage>(response.Result.Content.ReadAsStringAsync().Result);
                metric = responseContent.Result;
                Assert.AreEqual("137,50cm", metric);
            }
        }
        [TestMethod]
        public void Get_Metric_Rate_Units()
        {
            using (var client = GetClient())
            {
                string endPoint = "GetMetricRateUnits";
                Task<HttpResponseMessage> response = client.GetAsync($"{_api_ayo}/{endPoint}");
                Assert.AreEqual(true, response.Result.IsSuccessStatusCode);
                var responseContent = JsonConvert.DeserializeObject<List<MetricUnit>>(response.Result.Content.ReadAsStringAsync().Result);
                Assert.AreEqual("centimetre", responseContent.Find(a => a.UnitName == "cm")?.UnitDesc);
                Assert.AreEqual("Celsius(°C)", responseContent.Find(a => a.UnitName == "°C")?.UnitDesc);
            }
        }
        [TestMethod]
        public void Get_Imperial_Rate_Units()
        {
            using (var client = GetClient())
            {
                string endPoint = "GetImperialRateUnits";
                Task<HttpResponseMessage> response = client.GetAsync($"{_api_ayo}/{endPoint}");
                Assert.AreEqual(true, response.Result.IsSuccessStatusCode);
                var responseContent = JsonConvert.DeserializeObject<List<ImperialUnit>>(response.Result.Content.ReadAsStringAsync().Result);
                Assert.IsNotNull(responseContent);
                Assert.AreEqual("gallon", responseContent.Find(a => a.UnitName == "gallon")?.UnitDesc);
                Assert.AreEqual("Fahrenheit(°F)", responseContent.Find(a => a.UnitName == "°F")?.UnitDesc);
            }
        }
    }
}