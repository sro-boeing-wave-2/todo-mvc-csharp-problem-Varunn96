using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Google_Keep_ToDo.Models;
using System.Collections.Generic;
using Google_Keep_ToDo.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.AspNetCore;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;

namespace Google_Keep_ToDo.Tests
{
    public class UnitTest1
    {
        //private Google_Keep_ToDoContext dbContext;
        //public UnitTest1()
        //{
        //    var OptionsBuilder = new DbContextOptionsBuilder<Google_Keep_ToDoContext>();
        //    OptionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        //    dbContext = new Google_Keep_ToDoContext(OptionsBuilder.Options);
        //    _controller = new ToDoController(dbContext);
        //    AddTestData(dbContext);
        //}

        public ToDoController GetController()
        {
            var optionsBuilder = new DbContextOptionsBuilder<Google_Keep_Context>();
            optionsBuilder.UseInMemoryDatabase<Google_Keep_Context>(Guid.NewGuid().ToString());
            var dbContext = new Google_Keep_Context(optionsBuilder.Options);
            CreateTestData(optionsBuilder.Options);
            return new ToDoController(dbContext);
        }

        public void CreateTestData(DbContextOptions<Google_Keep_Context> options)
        {
            using (var dbContext = new Google_Keep_Context(options))
            {
                List<Note> TestNotes = new List<Note>
                {
                    new Note()
                    {
                        Id = 1,
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
                        Id = 2,
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
                        Id = 3,
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
                        Id = 4,
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
                dbContext.Note.AddRange(TestNotes);
                dbContext.SaveChanges();
            }
        }

        //Works
        [Fact]
        public void Test_GetMyNote()
        {
            var _controller = GetController();
            var res = _controller.GetMyNote().ToList();
            Assert.Equal(4, res.Count);
        }

        //Works
        [Fact]
        public async Task Test_GetById()
        {
            var _controller = GetController();
            //int id = _controller.GetMyNote().ToList()[0].Id;
            var result = await _controller.GetById(1);
            var status = result as OkObjectResult;
            var note = status.Value as Note;
            Assert.Equal(1, note.Id);
        }

        //Works
        [Fact]
        public async Task Test_GetByTitle()
        {
            var _controller = GetController();
            var result = await _controller.GetByTitle("First note");
            var status = result as OkObjectResult;
            var note = status.Value as Note;
            note.Name.Should().Be("First note");
        }

        //Works
        [Fact]
        public async Task Test_GetByPinStatus()
        {
            var _controller = GetController();
            var okResult = await _controller.GetByPinStatus(true);
            var status = okResult as OkObjectResult;
            var notes = status.Value as List<Note>;
            Assert.True(notes[0].PinStatus);
        }

        //Works
        [Fact]
        public async Task Test_GetByLabel()
        {
            var _controller = GetController();
            var okresult = await _controller.GetByLabel("Important");
            var OkObj = okresult as OkObjectResult;
            var Notes = OkObj.Value as List<Note>;
            Notes[0].Name.Should().Be("First note");
            Notes[0].Text.Should().Be("This is the first note");
        }

        //Works
        [Fact]
        public async Task Test_DeleteById()
        {
            var _controller = GetController();
            //int id = _controller.GetMyNote().ToList()[0].Id;
            var result = await _controller.DeleteMyNote(2);
            Assert.Equal(3, _controller.GetMyNote().Count());
        }

        //Works
        [Fact]
        public async Task Test_DeleteByTitle()
        {
            var _controller = GetController();
            var result = await _controller.DeleteByTitle("Fourth note");
            Assert.Equal(3, _controller.GetMyNote().Count());
        }

        //Works
        [Fact]
        public void Test_DeleteByLabel()
        {
            var _controller = GetController();
            var result = _controller.DeleteByLabel("Important");
            Assert.Equal(2, _controller.GetMyNote().Count());
        }

        //Works
        [Fact]
        public async Task Test_PostMyNote()
        {
            var _controller = GetController();
            Note note = new Note
            {
                Id = 5,
                Name = "Posted note",
                Text = "This is the posted note",
                PinStatus = true,
                CheckList = new List<CheckList_Item>()
                { new CheckList_Item
                    {
                        CheckListName = "list 1",
                        CheckListStatus = false
                    }
                },
                Labels = new List<Label>()
                { new Label
                    {
                        LabelName = "label name"
                    }
                }
            };
            var result = await _controller.PostMyNote(note);
            var status = result as CreatedAtActionResult;
            var notes = status.Value as Note;
            Assert.Equal("Posted note", notes.Name);
        }

        [Fact]
        public async Task Test_PutMyNote()
        {
            var _controller = GetController();
            Note note = new Note
            {
                Id = 2,
                Name = "Edited note",
            };
            var result = await _controller.PutMyNote(2, note);
            var status = result as OkObjectResult;
            var notes = status.Value as Note;
            Assert.Equal("Edited note", notes.Name);
        }
    }
}

