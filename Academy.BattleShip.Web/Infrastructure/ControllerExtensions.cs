using System.Linq;
using System.Web.Http.ModelBinding;
using Academy.BattleShip.Service;
using Academy.BattleShip.Service.Services;

namespace Academy.BattleShip.Web.Infrastructure
{
    public static class ControllerExtensions
    {
        public static void AddValidationErrors(this ModelStateDictionary modelState, EntityValidationException exception)
        {
            exception.Errors.ToList().ForEach(t => modelState.AddModelError(string.Empty, t.ErrorMessage));
        }
    }
}