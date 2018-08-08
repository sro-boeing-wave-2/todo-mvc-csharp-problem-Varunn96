//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Google_Keep_ToDo.Models;
//using Google_Keep_ToDo.Controllers;
//using System.Net.Http;
//using System.Net;

//namespace Google_Keep_ToDo.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ToDoTestController : ControllerBase
//    {
//        public HttpResponseMessage Get(int id)
//        {
//            var note = ToDoController._context.MyNote.Where(p => p.Id == id)
//                .FirstOrDefault();
//            return Request.CreateResponse(HttpStatusCode.OK, note);
//        }
//    }
//}