using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesAPI.Helpers
{
    public class TypeBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var propertyName = bindingContext.ModelName;
            var valueProvider = bindingContext.ValueProvider.GetValue(propertyName);

            if (valueProvider == ValueProviderResult.None)
                return Task.CompletedTask;

            try
            {
                var deserializedObject = JsonConvert.DeserializeObject<List<int>>(valueProvider.FirstValue);
                bindingContext.Result = ModelBindingResult.Success(deserializedObject);
            }
            catch (Exception)
            {
                bindingContext.ModelState.TryAddModelError(propertyName, "Invalid value for type List<int>");
            }

            return Task.CompletedTask;
        }
    }
}
