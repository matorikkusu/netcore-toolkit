using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace Matorikkusu.Toolkit.ValidationAttributes;

[AttributeUsage(
    AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
    AllowMultiple = false)]
public class FileSignaturesAttribute : ValidationAttribute
{
    private readonly string[] _extensions;
    private readonly bool _allowNullable;

    // For more file signatures, see the File Signatures Database (https://www.filesignatures.net/)
    // and the official specifications for the file types you wish to add.
    private readonly IDictionary<string, List<byte?[]>> _fileSignature = new Dictionary<string, List<byte?[]>>
    {
        {
            ".doc", [
                [0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1],
                [0x0D, 0x44, 0x4F, 0x43],
                [0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1, 0x00],
                [0xDB, 0xA5, 0x2D, 0x00],
                [0xEC, 0xA5, 0xC1, 0x00]
            ]
        },
        {
            ".xls", [
                [0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1],
                [0x09, 0x08, 0x10, 0x00, 0x00, 0x06, 0x05, 0x00],
                [0xFD, 0xFF, 0xFF, 0xFF, 0x10],
                [0xFD, 0xFF, 0xFF, 0xFF, 0x1F],
                [0xFD, 0xFF, 0xFF, 0xFF, 0x22],
                [0xFD, 0xFF, 0xFF, 0xFF, 0x23],
                [0xFD, 0xFF, 0xFF, 0xFF, 0x28],
                [0xFD, 0xFF, 0xFF, 0xFF, 0x29]
            ]
        },
        {
            ".docx", [
                [0x50, 0x4B, 0x03, 0x04],
                [0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06, 0x00]
            ]
        },
        {
            ".xlsx", [
                [0x50, 0x4B, 0x03, 0x04],
                [0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06, 0x00]
            ]
        },
        {
            ".ppt", [
                [0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1],
                [0x00, 0x6E, 0x1E, 0xF0],
                [0x0F, 0x00, 0xE8, 0x03],
                [0xA0, 0x46, 0x1D, 0xF0],
                [0xFD, 0xFF, 0xFF, 0xFF, 0x0E, 0x00, 0x00, 0x00],
                [0xFD, 0xFF, 0xFF, 0xFF, 0x1C, 0x00, 0x00, 0x00],
                [0xFD, 0xFF, 0xFF, 0xFF, 0x43, 0x00, 0x00, 0x00]
            ]
        },
        {
            ".pptx", [
                [0x50, 0x4B, 0x03, 0x04],
                [0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06, 0x00]
            ]
        },
        { ".gif", [[0x47, 0x49, 0x46, 0x38]] },
        { ".pdf", [[0x25, 0x50, 0x44, 0x46]] },
        { ".fdf", [[0x25, 0x50, 0x44, 0x46]] },
        { ".png", [[0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A]] },
        {
            ".jpeg", [
                [0xFF, 0xD8, 0xFF, 0xE0],
                [0xFF, 0xD8, 0xFF, 0xE2],
                [0xFF, 0xD8, 0xFF, 0xE3],
                [0xFF, 0xD8, 0xFF, 0xE1],
                [0xFF, 0xD8, 0xFF, 0xE8]
            ]
        },
        {
            ".jpg", [
                [0xFF, 0xD8, 0xFF, 0xE0],
                [0xFF, 0xD8, 0xFF, 0xE2],
                [0xFF, 0xD8, 0xFF, 0xE3],
                [0xFF, 0xD8, 0xFF, 0xE1],
                [0xFF, 0xD8, 0xFF, 0xE8]
            ]
        },
        {
            ".zip", [
                [0x50, 0x4B, 0x03, 0x04],
                [0x50, 0x4B, 0x4C, 0x49, 0x54, 0x45],
                [0x50, 0x4B, 0x53, 0x70, 0x58],
                [0x50, 0x4B, 0x05, 0x06],
                [0x50, 0x4B, 0x07, 0x08],
                [0x57, 0x69, 0x6E, 0x5A, 0x69, 0x70]
            ]
        },
        {
            ".mp4", [
                [null, null, null, null, 0x66, 0x74, 0x79, 0x70, 0x4D, 0x53, 0x4E, 0x56],
                [null, null, null, null, 0x66, 0x74, 0x79, 0x70, 0x69, 0x73, 0x6F, 0x6D],
                // extended from m4v
                [null, null, null, null, 0x66, 0x74, 0x79, 0x70, 0x4D, 0x34, 0x56, 0x20],
                [null, null, null, null, 0x66, 0x74, 0x79, 0x70, 0x6D, 0x70, 0x34, 0x32],
                // extended from mov
                [null, null, null, null, 0x66, 0x74, 0x79, 0x70, 0x71, 0x74, 0x20, 0x20]
            ]
        },
        {
            ".m4v", [
                [null, null, null, null, 0x66, 0x74, 0x79, 0x70, 0x4D, 0x34, 0x56, 0x20],
                [null, null, null, null, 0x66, 0x74, 0x79, 0x70, 0x6D, 0x70, 0x34, 0x32],
                // extended from mp4
                [null, null, null, null, 0x66, 0x74, 0x79, 0x70, 0x4D, 0x53, 0x4E, 0x56],
                [null, null, null, null, 0x66, 0x74, 0x79, 0x70, 0x69, 0x73, 0x6F, 0x6D],
                // extended from mov
                [null, null, null, null, 0x66, 0x74, 0x79, 0x70, 0x71, 0x74, 0x20, 0x20]
            ]
        },
        {
            ".mpeg", [
                [0x00, 0x00, 0x01, 0xBA],
                [0x00, 0x00, 0x01, 0xB3]
            ]
        },
        {
            ".avi", [
                [0x52, 0x49, 0x46, 0x46],
                [0x41, 0x56, 0x49, 0x20, 0x4C, 0x49, 0x53, 0x54]
            ]
        },
        {
            ".mov", [
                [null, null, null, null, 0x66, 0x74, 0x79, 0x70, 0x71, 0x74, 0x20, 0x20],
                [null, null, null, null, 0x6D, 0x6F, 0x6F, 0x76],
                // extended from mp4
                [null, null, null, null, 0x66, 0x74, 0x79, 0x70, 0x4D, 0x53, 0x4E, 0x56],
                [null, null, null, null, 0x66, 0x74, 0x79, 0x70, 0x69, 0x73, 0x6F, 0x6D],
                // extended from m4v
                [null, null, null, null, 0x66, 0x74, 0x79, 0x70, 0x4D, 0x34, 0x56, 0x20],
                [null, null, null, null, 0x66, 0x74, 0x79, 0x70, 0x6D, 0x70, 0x34, 0x32]
            ]
        },
        {
            ".svg", [
                [0x3C, 0x3F, 0x78, 0x6D, 0x6C],
                [0x3C, 0x73, 0x76, 0x67]
            ]
        }
    };

    public FileSignaturesAttribute(string[] extensions, bool allowNullable = false)
    {
        _extensions = extensions;
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
            case IFormFile formFile:
                return IsValid(formFile);

            case IEnumerable<IFormFile> formFiles:
                foreach (var formFile in formFiles)
                {
                    var result = IsValid(formFile);
                    if (result != ValidationResult.Success) return result;
                }

                return ValidationResult.Success;

            default:
                return new ValidationResult("Please select files to upload: File is empty.");
        }
    }

    private ValidationResult IsValid(IFormFile formFile)
    {
        var trustedFileNameForDisplay = WebUtility.HtmlEncode(formFile.FileName);
        if (formFile.Length == 0)
        {
            return new ValidationResult($"{trustedFileNameForDisplay} is empty");
        }

        try
        {
            using var memoryStream = new MemoryStream();
            formFile.CopyTo(memoryStream);
            if (memoryStream.Length == 0)
            {
                return new ValidationResult($"{trustedFileNameForDisplay} is empty");
            }

            if (!IsValidFileExtensionAndSignature(formFile.FileName, memoryStream))
            {
                if (string.IsNullOrEmpty(ErrorMessageString))
                {
                    return new ValidationResult(
                        $"{trustedFileNameForDisplay} file type isn't permitted or the file's signature doesn't match the file's extension");
                }

                return new ValidationResult(ErrorMessageString);
            }
        }
        catch
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }

    private bool IsValidFileExtensionAndSignature(string fileName, Stream data)
    {
        if (string.IsNullOrEmpty(fileName) || data == null || data.Length == 0)
        {
            return false;
        }

        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        if (string.IsNullOrEmpty(extension) || !_extensions.Contains(extension))
        {
            return false;
        }

        // Uncomment the following code block if you must permit
        // files whose signature isn't provided in the _fileSignature
        // dictionary. We recommend that you add file signatures
        // for files (when possible) for all file types you intend
        // to allow on the system and perform the file signature
        // check.
        var signatures = _fileSignature[extension];
        data.Position = 0;
        using var reader = new BinaryReader(data);
        var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

        return signatures
            .Select(signature => !signature.Where((t, i) => t is not null && t != headerBytes[i]).Any())
            .Any(result => result);
    }
}