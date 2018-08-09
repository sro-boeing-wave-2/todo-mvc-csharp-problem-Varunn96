using System;
using Google_Keep_ToDo.Controllers;
using Google_Keep_ToDo.Models;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace Google_Keep_ToDo.Tests
{
    public class IntegrationTest
    {
        List<Note> TestNotes = new List<Note>()
        {
            new Note()
            {
                Name = "First note",
                Text = "This is the first note",
                PinStatus = true,
                CheckList = new List<CheckList_Item>()
                {
                    new CheckList_Item()
                    {
                        CheckListName = "1st task",
                        CheckListStatus = false
                    },
                    new CheckList_Item()
                    {
                        CheckListName = "2nd task",
                        CheckListStatus = false
                    }
                },
                Labels = new List<Label>()
                {
                    new Label()
                    {
                        LabelName = "Important"
                    }
                }
            },
            new Note()
            {
                Name = "Second note",
                Text = "This is the second note",
                PinStatus = false,
                CheckList = new List<CheckList_Item>()
                {
                    new CheckList_Item()
                    {
                        CheckListName = "3rd task",
                        CheckListStatus = true
                    }
                },
                Labels = new List<Label>()
                {
                    new Label()
                    {
                        LabelName ="Important"
                    }
                }
            },
            new Note()
            {
                Name = "Third note",
                Text = "This is the third note",
                PinStatus = false,
                CheckList = new List<CheckList_Item>()
                {
                    new CheckList_Item()
                    {
                        CheckListName = "3rd task",
                        CheckListStatus = true
                    }
                },
                Labels = new List<Label>()
                {
                    new Label()
                    {
                        LabelName ="Very Important"
                    }
                }
            },
            new Note()
            {
                Name = "Fourth note",
                Text = "This is the fourth note",
                PinStatus = false,
                CheckList = new List<CheckList_Item>()
                {
                    new CheckList_Item()
                    {
                        CheckListName = "3rd task",
                        CheckListStatus = true
                    }
                },
                Labels = new List<Label>()
                {
                    new Label()
                    {
                        LabelName ="Not So Important"
                    }
                }
            }
        };

        private HttpClient _client;
        private readonly Google_Keep_ToDoContext _context;
        public IntegrationTest()
        {
            var host = new TestServer(new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>());

            _client = host.CreateClient();

            _context = host.Host.Services.GetService(typeof(Google_Keep_ToDoContext)) as Google_Keep_ToDoContext;
            _context.AddRange(TestNotes);
            _context.SaveChanges();
        }

        [Fact]
        public async Task TestGetMyNote()
        {
            var Response = await _client.GetAsync("/api/todo");
            var ResponseContent = await Response.Content.ReadAsStringAsync();
            Response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetById_Fail()
        {
            var Response = await _client.GetAsync("/api/todo/99");
            Assert.Equal("NotFound", Response.StatusCode.ToString());
        }

        [Fact]
        public async Task TestGetByTitle()
        {
            var Response = await _client.GetAsync("/api/todo/title?title=First%20note");
            Assert.Equal("OK", Response.StatusCode.ToString());
        }

        [Fact]
        public async Task TestGetByPinStatus()
        {
            var Response = await _client.GetAsync("/api/todo/pinstatus?pinstatus=true");
            Assert.Equal("OK", Response.StatusCode.ToString());
        }

        [Fact]
        public async Task TestGetByLabel()
        {
            var Response = await _client.GetAsync("/api/todo/label?label=Important");
            Assert.Equal("OK", Response.StatusCode.ToString());
        }

        [Fact]
        public async Task TestPostMyNote()
        {
            var notes = new Note
            {
                Name = "Posted note",
                Text = "This is the posted note",
                PinStatus = true,
                CheckList = new List<CheckList_Item>()
                    {
                        new CheckList_Item()
                        {
                            CheckListName = "posted list entry",
                            CheckListStatus = true
                        }
                    },
                Labels = new List<Label>()
                    {
                        new Label()
                        {
                            LabelName = "posted label name"
                        }
                    }
            };

            var json = JsonConvert.SerializeObject(notes);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var Response = await _client.PostAsync("/api/todo", stringContent);
            var ResponseGet = await _client.GetAsync("/api/todo");
            ResponseGet.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestDeleteByLabel()
        {
            var Response = await _client.DeleteAsync("/api/todo/label?label=Important");
            Assert.Equal("OK", Response.StatusCode.ToString());
        }

        [Fact]
        public async Task TestDeleteByTitle()
        {
            var Response = await _client.DeleteAsync("/api/todo/title?title=First note");
            Assert.Equal("OK", Response.StatusCode.ToString());
        }

        [Fact]
        public async Task TestDeleteById()
        {
            var Response = await _client.GetAsync("/api/todo/99");
            Assert.Equal("NotFound", Response.StatusCode.ToString());
        }

        [Fact]
        public async void TestingPutMyNote()
        {
            var note = new Note()
            {
                Id = 2,
                Name = "Updated"
            };
            var json = JsonConvert.SerializeObject(note);
            var stringnote = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var Response = await _client.PutAsync("/api/todo/2", stringnote);
            Assert.Equal("OK", Response.StatusCode.ToString());
        }
    }

}