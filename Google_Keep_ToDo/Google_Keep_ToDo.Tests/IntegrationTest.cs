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

//namespace Google_Keep_ToDo.Tests
//{
//    public class IntegrationTest
//    {
//        List<MyNote> TestNotes = new List<MyNote>()
//        {
//            new MyNote()
//            {
//                Name = "First note",
//                Text = "This is the first note",
//                PinStatus = true,
//                CheckLists = new List<CheckList>()
//                {
//                    new CheckList()
//                    {
//                        CheckListName = "1st task",
//                        CheckListStatus = false
//                    },
//                    new CheckList()
//                    {
//                        CheckListName = "2nd task",
//                        CheckListStatus = false
//                    }
//                },
//                Labels = new List<Label>()
//                {
//                    new Label()
//                    {
//                        LabelName = "Important"
//                    }
//                }
//            },
//            new MyNote()
//            {
//                Name = "Second note",
//                Text = "This is the second note",
//                PinStatus = false,
//                CheckLists = new List<CheckList>()
//                {
//                    new CheckList()
//                    {
//                        CheckListName = "3rd task",
//                        CheckListStatus = true
//                    }
//                },
//                Labels = new List<Label>()
//                {
//                    new Label()
//                    {
//                        LabelName ="Important"
//                    }
//                }
//            },
//            new MyNote()
//            {
//                Name = "Third note",
//                Text = "This is the third note",
//                PinStatus = false,
//                CheckLists = new List<CheckList>()
//                {
//                    new CheckList()
//                    {
//                        CheckListName = "3rd task",
//                        CheckListStatus = true
//                    }
//                },
//                Labels = new List<Label>()
//                {
//                    new Label()
//                    {
//                        LabelName ="Very Important"
//                    }
//                }
//            },
//            new MyNote()
//            {
//                Name = "Fourth note",
//                Text = "This is the fourth note",
//                PinStatus = false,
//                CheckLists = new List<CheckList>()
//                {
//                    new CheckList()
//                    {
//                        CheckListName = "3rd task",
//                        CheckListStatus = true
//                    }
//                },
//                Labels = new List<Label>()
//                {
//                    new Label()
//                    {
//                        LabelName ="Not So Important"
//                    }
//                }
//            }
//        };

//        private HttpClient _client;
//        public IntegrationTest()
//        {
//            var host = new TestServer(new WebHostBuilder()
//                .UseEnvironment("Testing")
//                .UseStartup<Startup>());
//            _client = host.CreateClient();

//            Google_Keep_ToDoContext _context = host.Host.Services.GetService(typeof(Google_Keep_ToDoContext)) as Google_Keep_ToDoContext;
//            _context.AddRange(TestNotes);
//            _context.SaveChanges();
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
//            Assert.Equal("NotFound", Response.StatusCode.ToString());
//        }

//        [Fact]
//        public async Task TestGetByTitle()
//        {
//            var Response = await _client.GetAsync("/api/todo/title?title=null");
//            Assert.Equal("NotFound", Response.StatusCode.ToString());
//        }

//        //    [Fact]
//        //    public async Task TestGetByPinStatus()
//        //    {
//        //        var Response = await _client.GetAsync("/api/todo/pinstatus?pinstatus=true");
//        //        Assert.Equal("NotFound", Response.StatusCode.ToString());
//        //    }

//        //    [Fact]
//        //    public async Task TestGetByLabel()
//        //    {
//        //        var Response = await _client.GetAsync("/api/todo/label?label=Important");
//        //        Assert.Equal("NotFound", Response.StatusCode.ToString());
//        //    }

//        //    [Fact]
//        //    public async Task TestPostMyNote()
//        //    {
//        //        var notes = new MyNote
//        //        {
//        //            Name = "First note",
//        //            Text = "This is the first note",
//        //            PinStatus = true,
//        //            CheckLists = new List<CheckList>()
//        //            {
//        //                new CheckList()
//        //                {
//        //                    CheckListName = "checklist data 1",
//        //                    CheckListStatus = true
//        //                }
//        //            },
//        //            Labels = new List<Label>()
//        //            {
//        //                new Label()
//        //                {
//        //                    LabelName = "label name"
//        //                }
//        //            }
//        //        };

//        //        var json = JsonConvert.SerializeObject(notes);
//        //        var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
//        //        var Response = await _client.PostAsync("/api/todo", stringContent);
//        //        var ResponseGet = await _client.GetAsync("/api/todo");
//        //        ResponseGet.EnsureSuccessStatusCode();
//        //    }
//    }

//}