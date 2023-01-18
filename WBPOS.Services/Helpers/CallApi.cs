using WBPOS.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks; 
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace WBPOS.Services.Helpers
{
    public class CallApi
    {
        //Hosted web API REST Service base url
       // static string Baseurl = "http://54.208.249.191:8081/api/";
         
        public static async Task<string> ConvertVideo(RequestParam param)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();
            var Baseurl = config["ConvertVideoBaseUrl"];

            string returnValue = "";
            //List<RequestParam> EmpInfo = new List<RequestParam>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                // HttpResponseMessage Res = await client.PostAsync("ConvertVideo", param);
                HttpResponseMessage Res = await client.PostAsJsonAsync<RequestParam>("ConvertVideo", param);
                //var postTask = await client.PostAsJsonAsync<RequestParam>("ConvertVideo", param);


                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var ApiResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    // EmpInfo = JsonConvert.DeserializeObject<List<Employee>>(ApiResponse);

                    returnValue = ApiResponse;
                }
                //returning the employee list to view
                return returnValue;
            }
        }
    }
}
