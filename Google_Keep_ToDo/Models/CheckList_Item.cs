using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Google_Keep_ToDo.Models
{
    public class CheckList_Item
    {
        public int Id { get; set; }
        public string CheckListName { get; set; }
        public bool CheckListStatus { get; set; }
    }
}
