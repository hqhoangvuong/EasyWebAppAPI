using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace EasyWebApp.Data.Exception
{
    public class ModelStateException : RankException
    {
        public ModelStateDictionary ModelState { get; }

        public ModelStateException(string key, string errorMessage)
        {
            ModelState = new ModelStateDictionary();
            ModelState.AddModelError(key, errorMessage);
        }

        public ModelStateException(ModelStateDictionary modelState)
        {
            ModelState = modelState;
        }
    }
}
