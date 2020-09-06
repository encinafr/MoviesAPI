using Microsoft.AspNetCore.Http;
using MoviesAPI.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Helpers.Validations
{
    public class FileTypeValidation : ValidationAttribute
    {
        private readonly string[] _validTypes;

        public FileTypeValidation(string[] validTypes)
        {
            _validTypes = validTypes;
        }

        public FileTypeValidation(FileTypeGruopEnum fileTypeGruop)
        {
            if(fileTypeGruop == FileTypeGruopEnum.Image)
            {
                _validTypes = new string[] { "image/jpeg", "image/png", "image.gif" };
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            IFormFile formFile = value as IFormFile;

            if (formFile == null)
                return ValidationResult.Success;

            if (!_validTypes.Contains(formFile.ContentType))
                return new ValidationResult($"The file type must be one of the following: {string.Join(",", _validTypes)}");

            return ValidationResult.Success;
        }
    }
}
