using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CUSTOMSOFT.API.Filters.Extensions
{
    public static class ModelStateExtensions
    {
        public static void AddValidationErrors(this ModelStateDictionary modelState, Dictionary<string, string[]> errors)
        {
            if (errors == null || !errors.Any())
            {
                return;
            }

            foreach (var (key, value) in errors)
            {
                modelState.AddModelError(key, value[0]);
            }
        }

    }
}
