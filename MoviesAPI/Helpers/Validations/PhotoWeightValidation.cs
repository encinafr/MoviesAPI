using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Helpers.Validations
{
    public class PhotoWeightValidation : ValidationAttribute
    {
        private readonly int _maxWeightMG;

        public PhotoWeightValidation(int maxWeightMG)
        {
            _maxWeightMG = maxWeightMG;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            IFormFile formFile = value as IFormFile;

            if (formFile == null)
                return ValidationResult.Success;

            if (formFile.Length > _maxWeightMG * 1024 * 1024)
                return new ValidationResult($"The weight of the file must not be greater than: {_maxWeightMG} mb");

            return ValidationResult.Success;
        }
    }
}
