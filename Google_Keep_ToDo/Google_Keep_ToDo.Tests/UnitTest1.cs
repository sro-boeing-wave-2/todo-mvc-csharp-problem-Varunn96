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
using FluentAssertions;

namespace Google_Keep_ToDo.Tests
{
    public class UnitTest1
    {
        private ToDoController _controller;
        public UnitTest1()
        {
            var OptionsBuilder = new DbContextOptionsBuilder<Google_Keep_ToDoContext>();
            OptionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            Google_Keep_ToDoContext dbContext = new Google_Keep_ToDoContext(OptionsBuilder.Options);
            _controller = new ToDoController(dbContext);
            AddTestData(dbContext);
        }

        private void AddTestData(Google_Keep_ToDoContext dbContext)
        {
            List<MyNote> TestNotes = new List<MyNote>()
            {
                new MyNote()
                {
                    Name = "First note",
                    Text = "This is the first note",
                    PinStatus = true,
                    CheckLists = new List<CheckList>()
                    {
                        new CheckList()
                        {
                            CheckListName = "1st task",
                            CheckListStatus = false
                        },
                        new CheckList()
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
                new MyNote()
                {
                    Name = "Second note",
                    Text = "This is the second note",
                    PinStatus = false,
                    CheckLists = new List<CheckList>()
                    {
                        new CheckList()
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
                new MyNote()
                {
                    Name = "Third note",
                    Text = "This is the third note",
                    PinStatus = false,
                    CheckLists = new List<CheckList>()
                    {
                        new CheckList()
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
                new MyNote()
                {
                    Name = "Fourth note",
                    Text = "This is the fourth note",
                    PinStatus = false,
                    CheckLists = new List<CheckList>()
                    {
                        new CheckList()
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
            dbContext.MyNote.AddRange(TestNotes);
            dbContext.SaveChanges();
        }

        //Works
        [Fact]
        public void Test_GetMyNote()
        {
            var res = _controller.GetMyNote().ToList();
            Assert.Equal(4, res.Count);
        }

        //Works
        [Fact]
        public async void Test_GetById()    
        {
            int id = _controller.GetMyNote().ToList()[0].Id;
            var result = await _controller.GetById(id);
            var status = result as OkObjectResult;
            var note = status.Value as MyNote;
            Assert.Equal(id, note.Id);
        }

        //Works
        [Fact]
        public async void Test_GetByTitle()
        {
            var result = await _controller.GetByTitle("First note");
            var status = result as OkObjectResult;
            var note = status.Value as MyNote;
            note.Name.Should().Be("First note");
        }

        //Works
        [Fact]
        public async void Test_GetByPinStatus()
        {
            var okResult = await _controller.GetByPinStatus(true) ;
            var status = okResult as OkObjectResult;
            var notes = status.Value as List<MyNote>;
            Assert.True(notes[0].PinStatus);
        }

        [Fact]
        public async void Test_GetByLabel()
        {
            var okresult = await _controller.GetByLabel("Important");
            var OkObj = okresult as OkObjectResult;
            var Notes = OkObj.Value as List<MyNote>;
            Notes[0].Name.Should().Be("First note");
            Notes[0].Text.Should().Be("This is the first note");
        }

        //Works
        [Fact]
        public async void Test_DeleteById()
        {
            int id = _controller.GetMyNote().ToList()[0].Id;
            var result = await _controller.DeleteMyNote(id);
            Assert.Equal(3, _controller.GetMyNote().Count());
        }

        //Works
        [Fact]
        public async void Test_DeleteByTitle()
        {
            var result = await _controller.DeleteByTitle("Fourth note");
            Assert.Equal(3, _controller.GetMyNote().Count());
        }

        //Works
        [Fact]
        public void Test_DeleteByLabel()
        {
            var result = _controller.DeleteByLabel("Important");
            Assert.Equal(2, _controller.GetMyNote().Count());
        }

        //Works
        [Fact]
        public async void Test_PostMyNote()
        {
            MyNote note = new MyNote
            {
                Name = "Posted note",
                Text = "This is the posted note",
                PinStatus = true,
                CheckLists = new List<CheckList>()
                { new CheckList
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
            var notes = status.Value as MyNote;
            Assert.Equal("Posted note", notes.Name);
        }

        //[Fact]
        //public async void Test_PutMyNote()
        //{
        //    MyNote note = new MyNote
        //    {
        //        Name = "Edited note",
        //        Text = "This is the edited note",
        //        PinStatus = false,
        //        CheckLists = new List<CheckList>()
        //        { new CheckList
        //            {
        //                CheckListName = "list 1",
        //                CheckListStatus = false
        //            }
        //        },
        //        Labels = new List<Label>()
        //        { new Label
        //            {
        //                LabelName = "label name"
        //            }
        //        }
        //    };
        //    int id = _controller.GetMyNote().ToList()[0].Id;
        //    var result = await _controller.PutMyNote(id, note);
        //    _controller.GetMyNote().Where(p => p.Id == id).ToList().Name.Should().Be("Edited note");
        //}


        //[Fact]
        //public async void Test_GetByTitle()
        //{
        //    var result = await _controller.GetByTitle("First Note");

        //}

        //[Fact]
        //public void GetById_ExistingIdPassed_ReturnsRightItem()
        //{
        //    // Arrange
        //    int testId = 1;

        //    // Act
        //    var okResult = _controller.GetById(testId).Result as OkObjectResult;

        //    // Assert
        //    Assert.IsType<MyNote>(okResult.Value);
        //    Assert.Equal(testId, (okResult.Value as MyNote).Id);
        //}

        //[Fact]
        //public void GetByTitle_UnknownTitlePassed_ReturnsNotFoundResult()
        //{
        //    // Act
        //    var notFoundResult = _controller.GetByTitle("RandomTitle");

        //    // Assert
        //    Assert.IsType<NotFoundResult>(notFoundResult.Result);
        //}

        //[Fact]
        //public void GetByTitle_ExistingTitlePassed_ReturnsOkResult()
        //{
        //    // Arrange
        //    string testString = "Everyday";

        //    // Act
        //    var okResult = _controller.GetByTitle(testString);

        //    // Assert
        //    Assert.IsType<OkObjectResult>(okResult.Result);
        //}

        //[Fact]
        //public void GetByTitle_ExistingTitlePassed_ReturnsRightItem()
        //{
        //    // Arrange
        //    string testString = "Everyday";

        //    // Act
        //    var okResult = _controller.GetByTitle(testString).Result as OkObjectResult;

        //    // Assert
        //    Assert.IsType<MyNote>(okResult.Value);
        //    Assert.Equal(testString, (okResult.Value as MyNote).Name);
        //}

        //[Fact]
        //public void GetByPinStatus_IfBoolDoesNotExist_ReturnsNotFoundResult()
        //{
        //    // Act
        //    var notFoundResult = _controller.GetByPinStatus(false);

        //    // Assert
        //    Assert.IsType<NotFoundResult>(notFoundResult.Result);
        //}

        //[Fact]
        //public void GetByPinStatus_IfBoolExists_ReturnsOkResult()
        //{

        //    // Act
        //    var okResult = _controller.GetByPinStatus(false);

        //    // Assert
        //    Assert.IsType<OkObjectResult>(okResult.Result);
        //}

        //[Fact]
        //public void GetByPinStatus_ExistingPinStatusPassed_ReturnsRightItem()
        //{

        //    // Act
        //    var okResult = _controller.GetByPinStatus(false).Result as OkObjectResult;

        //    // Assert
        //    Assert.IsType<MyNote>(okResult.Value);
        //    Assert.False((okResult.Value as MyNote).PinStatus);
        //}
    }
}

