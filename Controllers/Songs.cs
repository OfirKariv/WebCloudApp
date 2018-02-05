using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using System.Net.Http;
using Newtonsoft.Json;

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
          //  Console.WriteLine(response);
            return 1;
        
        
        }

        //[HttpGet]
        [Route("getSong")]
            async  Task<Song> getSong(string s) {
            var hc = Helpers.CouchDBConnect.GetClient("songs");
            var response = await hc.GetAsync("songs/"+s);
            Console.WriteLine(s);
            Console.WriteLine(response);
            return null;

        //remove song

        //get song(playsong?)
    }





}}