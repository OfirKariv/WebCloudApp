using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using UserService.Models;

namespace UserService.Helpers
{
    public static class CouchDBConnect
    {
        private static string host = "https://625dba61-fcf0-4b84-9a0a-7e78b82d1b7f-bluemix:81c4c4ec9376ee32ee3cb5df98b5033fa6a0d554b590a6b21198cd1a031280d0@625dba61-fcf0-4b84-9a0a-7e78b82d1b7f-bluemix.cloudant.com/{0}";
        public static HttpClient GetClient(string db) {
            var hc = new HttpClient();
            hc.BaseAddress = new Uri(string.Format(host,db));
            Console.WriteLine( "base adress-heleprs: "+ hc.BaseAddress);
            hc.DefaultRequestHeaders.Clear();
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Console.WriteLine(hc.DefaultRequestHeaders);
            return hc;
        }
    }
}