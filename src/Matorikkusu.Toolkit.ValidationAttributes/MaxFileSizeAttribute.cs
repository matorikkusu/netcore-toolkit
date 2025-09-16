using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Matorikkusu.Toolkit.ValidationAttributes;

[AttributeUsage(
    AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
    AllowMultiple = false)]
public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly bool _allowNullable;

    private readonly int _maxSize;

    public MaxFileSizeAttribute(int maxSize, bool allowNullable = false)
    {
        _maxSize = maxSize;
        _allowNullable = allowNullable;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (_allowNullable && value == null)
        {
            return ValidationResult.Success;
        }

        switch (value)
        {
            case IFormFile file:
                return IsValid(file);

            case IEnumerable<IFormFile> files:
            {
                foreach (var fileItem in files)
                {
                    var result = IsValid(fileItem);
                    if (result != ValidationResult.Success) return result;
                }

                return ValidationResult.Success;
            }
            default:
                return new ValidationResult("Please select files to upload: File is empty.");
        }
    }

    private ValidationResult IsValid(IFormFile file)
    {
        var fileSize = _maxSize / 1024 / 1024;
        return file.Length <= _maxSize
            ? ValidationResult.Success
            : new ValidationResult($"The maximum file size is {fileSize} MB. Your uploaded file, {file.FileName}, exceeds this limit.");
    }
}