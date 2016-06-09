using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using Academy.BattleShip.Service;

namespace Academy.BattleShip.Web.Classes
{
    public static class ControllerExtensions
    {
        public static void AddValidationErrors(this ModelStateDictionary modelState, EntityValidationException exception)
        {
            exception.Errors.ToList().ForEach(t => modelState.AddModelError(string.Empty, t.ErrorMessage));
        }
    }
}