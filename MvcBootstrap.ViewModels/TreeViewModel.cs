using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcBootstrap.ViewModels
{
    //zTree节点（大小写不可改）
    public class TreeViewModel
    {
        public string id { get; set; }
        public string pId { get; set; }
        public string name { get; set; }
        public bool open { get; set; }
        public bool isParent { get { return true; } }
        public bool isChecked { get; set; }
    }
}
