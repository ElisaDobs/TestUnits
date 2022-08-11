using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace IntegrationTesting
{
    [TestClass]
    public class IntegrationTest
    {
        private string _url = "https://localhost:44346/api/";
        private string _api_account = "account";
        private string _api_ayo = "ayo";
        private string _api_audit = "audit";
        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
        private Task<HttpResponseMessage> Post<T>(HttpClient client, string url, T inputOject)
        {
            using (var formData = new MultipartFormDataContent())
            {
                Task<HttpResponseMessage> response;
                formData.Add(new StringContent(JsonConvert.SerializeObject(inputOject)), typeof(T).Name);
                response = client.PostAsync(url, formData);
                return response;
            }
        }
        private Task<HttpResponseMessage> Post(HttpClient client, string url)
        {
                Task<HttpResponseMessage>  response = client.PostAsync(url, null);
                return response;            
        }
        [TestMethod]
        public void Ayo_convert_Metric_Imperial_Testing()
        {
            using (var client = GetClient())
            {
                decimal unitvalue = 20;
                int conversion_type = 1;
                string username = "David",
                       lastname = "Zama",
                       password = "Mdeva08",
                       unit = "cm",
                       endPoint_Create_User = "CreateUser",
                       endPoint_Validate_User = "ValidateUser",
                       endPoint_Convert_Metric_Imperial_Unit_Rate = "ConvertUnits",
                       endPoint_Create_User_Audit = "AccountCreateAudit",
                       endPoint_Validate_User_Audit = "ValidateUserAudit",
                       endPoint_Convert_Metric_Imperial_Unit_Rate_Audit = "UnitConversionAudit";
                Task<HttpResponseMessage> response = client.GetAsync($"{_api_account}/{endPoint_Create_User}?username={username}&lastname={lastname}&password={password}");
                Assert.AreEqual(true, response.Result.IsSuccessStatusCode);
                User user = JsonConvert.DeserializeObject<User>(response.Result.Content.ReadAsStringAsync().Result);
                Assert.IsNotNull(user?.UserId);
                response = Post(client, $"{_api_audit}/{endPoint_Create_User_Audit}", user);
                Assert.AreEqual(true, response.Result.IsSuccessStatusCode);
                response = client.GetAsync($"{_api_account}/{endPoint_Validate_User}?username={username}&lastname={lastname}&password={password}");
                Assert.AreEqual(true, response.Result.IsSuccessStatusCode);
                user = JsonConvert.DeserializeObject<User>(response.Result.Content.ReadAsStringAsync().Result);
                Assert.IsNotNull(user?.UserId);
                response = Post(client, $"{_api_audit}/{endPoint_Validate_User_Audit}", user);
                Assert.AreEqual(true, response.Result.IsSuccessStatusCode);
                response = client.GetAsync($"{_api_ayo}/{endPoint_Convert_Metric_Imperial_Unit_Rate}?unit={unit}&unitvalue={unitvalue}&conversion_type={conversion_type}");
                Assert.AreEqual(true, response.Result.IsSuccessStatusCode);
                ResponseMessage responseMessage = JsonConvert.DeserializeObject<ResponseMessage>(response.Result.Content.ReadAsStringAsync().Result);
                Assert.IsNotNull(responseMessage.Result);
                response = Post(client, $"{_api_audit}/{endPoint_Convert_Metric_Imperial_Unit_Rate_Audit}?user_id={user.UserId}&unit_value={unitvalue}&unit_ret={responseMessage.Result}&unit={unit}&conversion_type={conversion_type}");
                Assert.AreEqual(true, response.Result.IsSuccessStatusCode);
            }
        }
    }
}