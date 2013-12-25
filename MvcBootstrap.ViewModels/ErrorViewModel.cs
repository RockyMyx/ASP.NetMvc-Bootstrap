using System;

namespace MvcBootstrap.ViewModels
{
    public class ErrorViewModel
    {
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public Exception Exception { get; set; }

        public ErrorViewModel(string controllerName, string actionName, Exception exception)
        {
            this.ControllerName = controllerName;
            this.ActionName = actionName;
            this.Exception = exception;
        }
    }
}