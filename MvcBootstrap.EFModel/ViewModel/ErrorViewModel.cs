using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcBootstrap.Models.ViewModel
{
    public class ErrorViewModel
    {
        public ErrorViewModel(string controllerName, string actionName, Exception exception)
        {
            this.ControllerName = controllerName;
            this.ActionName = actionName;
            this.Exception = exception;
        }

        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public Exception Exception { get; set; }
    }
}