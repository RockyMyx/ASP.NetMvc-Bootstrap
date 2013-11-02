using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcBootstrapManage.Models
{
    public class MyViewModel
    {
        public int Year { get; set; }
        public IEnumerable<SelectListItem> Years
        {
            get
            {
                return Enumerable.Range(1980, 40).Select(x => new SelectListItem
                {
                    Value = x.ToString(),
                    Text = x.ToString()
                });
            }
        }

        public IList<TheData> Data { get; set; }
    }

    public class TheData
    {
        public int Year { get; set; }
        public string Foo { get; set; }
        public string Bar { get; set; }
    }
}