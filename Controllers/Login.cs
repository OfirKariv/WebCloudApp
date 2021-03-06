﻿using System;
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
    public class Login : Controller
    {

        static Dictionary<string,Token> ActiveLogins = new Dictionary<string, Token>();
        static List<User> Users = new List<User>();
       
        [HttpGet]
        [Route("ValidateSession/{tokenId}")]
        public async Task<Boolean> ValidateSession(string tokenId) {
            var hc = Helpers.CouchDBConnect.GetClient("users");
            var response = await hc.GetAsync("/users/"+tokenId);
            if (!response.IsSuccessStatusCode)
                return false;
            
            var token = (Token) JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync(),typeof(Token));

            //if (token.create + token.ttl > now)

            if (token.create.AddSeconds(token.ttl).CompareTo(DateTime.Now) > 0) {
                return true;
            }

            return false;
        }

        // POST api/values
        [HttpPost]
        public async Task<dynamic> Post([FromBody]User u)
        {

            var hc = Helpers.CouchDBConnect.GetClient("users");
            var response = await hc.GetAsync("users/"+u._id);
            if (response.IsSuccessStatusCode) {
                User user = (User) JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync(),typeof(User));
                if (user.password.Equals(u.password)) {
                    Token t = new Token();
                    t._id = u._id+":token:"+Guid.NewGuid();
                    t.create = DateTime.Now;
                    t.ttl = 600;

                    HttpContent htc = new StringContent(
                        JsonConvert.SerializeObject(t),
                        System.Text.Encoding.UTF8,
                        "application/json"
                    );

                    await hc.PostAsync("users", htc);

                    return t;
                }
            };

            return -1;

        }

        async  Task<Boolean> DoesUserExist(User u) {
            var hc = Helpers.CouchDBConnect.GetClient("users");
            var response = await hc.GetAsync("users/"+u._id);
            if (response.IsSuccessStatusCode) {
                return true;
            }

            return false;
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<int> CreateUser([FromBody] User u) {
            var doesExist = await DoesUserExist(u);
            if (doesExist) {
                return -1;
            }

            var hc = Helpers.CouchDBConnect.GetClient("users");
            string json = JsonConvert.SerializeObject(u);
            HttpContent htc = new StringContent(json,System.Text.Encoding.UTF8,"application/json");
            var response = await hc.PostAsync("",htc);
            Console.WriteLine("***************************************************************");
            Console.WriteLine(u._id);
            Console.WriteLine("***************************************************************");

            return 1;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [Route("DeleteUser")]
        [HttpDelete("{id}")]
        public async Task<Boolean> Delete(int id)
        {
            id=1;
            String st="1";
            Console.WriteLine("im in delete");
            /*var doesExist = await DoesUserExist(u);
                if (!doesExist) {
                    Console.WriteLine("user doesn't exist");  
                    return false;              
                }*/
                var hc = Helpers.CouchDBConnect.GetClient("users");
            //    string json = JsonConvert.SerializeObject(u);
                var response= await hc.DeleteAsync(hc.BaseAddress+"/"+st);
                Console.WriteLine(response);
            return true;
        }


    }
}
