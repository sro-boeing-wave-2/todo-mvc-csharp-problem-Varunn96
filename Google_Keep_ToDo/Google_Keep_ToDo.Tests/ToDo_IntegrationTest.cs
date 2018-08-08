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
//using Newtonsoft.Json.Linq;
//using FluentAssertions;

//namespace Google_Keep_ToDo.Tests
//{
//    public class ToDo_IntegrationTest
//    {
//        private HttpClient _client;
//        public ToDo_IntegrationTest()
//        {
//            var host = new TestServer(new WebHostBuilder()
//                .UseEnvironment("Testing")
//                .UseStartup<Startup>());
//            _client = host.CreateClient();
//        }

//        [Fact]
//        public async Task TestGetMyNote()
//        {
//            var Response = await _client.GetAsync("/api/todo");
//            var ResponseContent = await Response.Content.ReadAsStringAsync();
//            Response.EnsureSuccessStatusCode();
//        }

//        [Fact]
//        public async Task TestGetById()
//        {
//            var Response = await _client.GetAsync("/api/todo/99");
//            //Console.WriteLine(await Response.Content.ReadAsStringAsync());
//            //var ResponseBody = await Response.Content.ReadAsStringAsync();
//            Assert.Equal("NotFound", Response.StatusCode.ToString());
//        }

//        [Fact]
//        public async Task TestPostMyNote()
//        {
//            var notes =
//                new MyNote()
//                {
//                    Id = 1,
//                    Name = "First",
//                    Text = "This is the first note",
//                    PinStatus = true,
//                    CheckLists = new List<CheckList>()
//                    {
//                        new CheckList()
//                        {
//                            CheckListName = "Entry 1",
//                            CheckListStatus = true
//                        }
//                    },
//                    Labels = new List<Label>()
//                    {
//                        new Label()
//                        {
//                            LabelName ="label name"
//                        }
//                    }
//                };
//            var json = JsonConvert.SerializeObject(notes);
//            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
//            var Response = await _client.PostAsync("/api/todo", stringContent);
//            var ResponseGet = await _client.GetAsync("/api/todo");
//            //Console.WriteLine(await ResponseGet.Content.ReadAsStringAsync());
//            ResponseGet.EnsureSuccessStatusCode();
//        }

//        [Fact]
//        public async Task TestGetByIdAfterPost()
//        {
//            var notes =
//                new MyNote()
//                {
//                    Id = 1,
//                    Name = "First",
//                    Text = "This is the first note",
//                    PinStatus = true,
//                    CheckLists = new List<CheckList>()
//                    {
//                        new CheckList()
//                        {
//                            CheckListName = "Entry 1",
//                            CheckListStatus = true
//                        }
//                    },
//                    Labels = new List<Label>()
//                    {
//                        new Label()
//                        {
//                           LabelName ="label name"
//                        }
//                    }
//                };
//            var json = JsonConvert.SerializeObject(notes);
//            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
//            var Response = await _client.PostAsync("/api/todo", stringContent);

//            var ResponseForGetOne = await _client.GetAsync("/api/todo/1");
//            ResponseForGetOne.EnsureSuccessStatusCode();

//            var ResponseForGetFirst = await _client.GetAsync("/api/todo/title?title=First");
//            var result = await ResponseForGetFirst.Content.ReadAsStringAsync();
//            Console.WriteLine(await ResponseForGetFirst.Content.ReadAsStringAsync());
//            Console.WriteLine("JSON" + result);
//            var JArrayN = JArray.Parse(result);
//            var JObjectNotes = JArrayN[0];
//            Assert.Equal("1", JObjectNotes["Id"].ToString());
//            Assert.Equal("First", JObjectNotes["Name"].ToString());
//            Assert.Equal("This is the first note", JObjectNotes["Text"].ToString());
//            Assert.Equal("true", JObjectNotes["PinStatus"].ToString());

//            var DeleteResponse = await _client.DeleteAsync("/api/todo/1");
//            DeleteResponse.EnsureSuccessStatusCode();

//            var ResponsePost = await _client.PostAsync("/api/todo", stringContent);

//            MyNote note = new MyNote
//            {
//                Id = 1,
//                Name = "Second note",
//                Text = "This is the second note",
//                PinStatus = false,

//            };
//            var dataPut = JsonConvert.SerializeObject(note);
//            var stringContentPut = new StringContent(dataPut, UnicodeEncoding.UTF8, "application/json");
//            var TestPut = await _client.PutAsync("/api/todo/1", stringContentPut);
//            Assert.Equal("NoContent", TestPut.StatusCode.ToString());
//            //var TestGetByIdAfterPut = await _client.GetAsync("/api/Notes/1");
//            //var contentAfterPut = await TestGetByIdAfterPut.Content.ReadAsStringAsync();
//            //Console.WriteLine(contentAfterPut);
//            //var ActualDataToTestGetAfterPut = JObject.Parse(contentAfterPut);
//            ////var ActualDataToTestGet = ActualData[0];
//            //Assert.Equal(ActualDataToTestGetAfterPut["Id"].ToString(), "1");
//            //Assert.Equal(ActualDataToTestGetAfterPut["Title"], "Young Sheldon");

//        }
//    }
//}