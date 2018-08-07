//using System;
//using Google_Keep_ToDo.Controllers;
//using Google_Keep_ToDo.Models;
//using Xunit;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.InMemory;
//using System.Collections.Generic;
//using System.Net;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.TestHost;
//using System.Net.Http;
//using Microsoft.AspNetCore.Hosting;
//using System.Threading.Tasks;
//using Newtonsoft.Json;
//using System.Text;
//using Google_Keep_ToDo;

//namespace ToDoAssignment.Tests
//{
//    public class IntegrationTest
//    {
//        private HttpClient _client;
//        public IntegrationTest()
//        {
//            var host = new TestServer(new WebHostBuilder()
//                .UseEnvironment("Testing")
//                .UseStartup<Startup>());
//            _client = host.CreateClient();
//        }

//        [Fact]
//        public async Task Test_GetMyNote()
//        {
//            var Response = await _client.GetAsync("/api/notes");
//            var ResponseContent = await Response.Content.ReadAsStringAsync();
//            Assert.Equal(2, ResponseContent.Length);
//        }

//        [Fact]
//        public async Task Test_GetById()
//        {
//            var Response = await _client.GetAsync("/api/notes/201");
//            Assert.Equal("NotFound", Response.StatusCode.ToString());
//        }

//        [Fact]
//        public async Task Test_PostMyNote()
//        {
//            var notes =
//                new MyNote()
//                {
//                    Name = "First note",
//                    Text = "This is the first note",
//                    PinStatus = true,
//                    CheckLists = new List<CheckList>()
//                    {
//                        new CheckList()
//                        {
//                            CheckListName = "first entry",
//                            CheckListStatus = false
//                        }
//                    },
//                    Labels = new List<Label>()
//                    {
//                        new Label()
//                        {
//                            LabelName = "label name"
//                        }
//                    }
//                };
//            var json = JsonConvert.SerializeObject(notes);
//            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
//            var Response = await _client.PostAsync("/api/notes", stringContent);
//            var ResponseGet = await _client.GetAsync("/api/notes");
//            ResponseGet.EnsureSuccessStatusCode();
//        }
//    }
//}