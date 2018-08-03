using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Google_Keep_ToDo.Models
{
    public class MyNote
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public bool PinStatus { get; set; }
        public List<CheckList> CheckLists { get; set; }
        public List<Label> Labels { get; set; }
    }
}
