using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;


namespace UserService.Controllers
{
    [Route("api/[controller]")]
    public class Songs : Controller
    {
        [HttpPost]
        [Route("addSong")]
        public async Task<int> addSong([FromBody] Song s) {
        
            var hc = Helpers.CouchDBConnect.GetClient("songs");
            string json = JsonConvert.SerializeObject(s);
            HttpContent htc = new StringContent(json,System.Text.Encoding.UTF8,"application/json");
            var response = await hc.PostAsync("",htc);
            Console.WriteLine("***************************************************************");
            Console.WriteLine(s._id);
            Console.WriteLine("***************************************************************");
            Console.WriteLine(response);
            Console.WriteLine("***************************************************************");

            return 1;
        
        
        }

        //[HttpGet]
        [Route("getSong")]
           public async  Task<Song> getSong(string s) {
                Console.Write("in getSong");
               // s="1";
            var hc = Helpers.CouchDBConnect.GetClient("songs/"+s);
            var response = await hc.GetAsync(hc.BaseAddress);
           
            Console.WriteLine(s);
            var jsonobject=await response.Content.ReadAsStringAsync();
            var song= JsonConvert.DeserializeObject<Song>(jsonobject);
         //   Console.WriteLine(jsonobject);
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Console.WriteLine(song.songUrl);
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            return song;
            }

            
     
        /* 
                HttpClient hc = new HttpClient();
                // init right path
                hc.BaseAddress= new Uri(tringGet("getUser")+"/"+userId);
                hc.DefaultRequestHeaders.Clear()routingCache.S;
                hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // send and get response
                using (HttpResponseMessage response = await hc.GetAsync(""))
                {
                    using (HttpContent resContent = response.Content)
                    {
                        string jsonAns = await response.Content.ReadAsStringAsync();
                        // if contains id it's user else error
                        if (jsonAns.Contains("_id"))
                            user = (User) JsonConvert.DeserializeObject(jsonAns);
    }

 */
//remove song

        //get song(playsong?)
        


}}