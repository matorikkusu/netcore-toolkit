# Matorikkusu.Toolkit

A comprehensive .NET toolkit providing validation attributes and useful extensions for modern .NET applications.

## Overview

Matorikkusu.Toolkit consists of two main packages:

- **Matorikkusu.Toolkit.ValidationAttributes** - Custom validation attributes for ASP.NET Core applications
- **Matorikkusu.Toolkit.Extensions** - Utility extensions and helper classes

## Features

### Validation Attributes

#### ValidNumberStringAttribute

Validates that a string value can be parsed as a specific numeric type, with optional positive number validation.

```csharp
    public class MyModel
    {
        [ValidNumberString(typeof(int), true, "Please enter a valid positive integer")]
        public string Age { get; set; }
    
        [ValidNumberString(typeof(int), false)]
        public string Temperature { get; set; }
    }
```

#### Other Validation Attributes

- **NotEmptyAttribute** - Validates that a value is not empty
- **CoordinatesAttribute** - Validates geographic coordinates
- **MaxFileSizeAttribute** - Validates file size limits
- **HttpClientUrlAttribute** - Validates HTTP URLs
- **FileSignaturesAttribute** - Validates file signatures/types
- **DecimalPrecisionAttribute** - Validates decimal precision
- **ValidBooleanStringAttribute** - Validates boolean string representations
- **ValidDateTimeStringAttribute** - Validates DateTime string formats

#### JSON Converters

- **JsonDatetimeUtcConverter** - UTC DateTime JSON converter
- **JsonDatetimeNullUtcConverter** - Nullable UTC DateTime JSON converter

### Extensions

The toolkit includes various extension methods for:

- **Enum operations** - Enhanced enum functionality
- **String manipulations** - Additional string utilities
- **Hash operations** - Cryptographic hash extensions
- **Queryable operations** - LINQ query enhancements with sorting and pagination
- **Enumerable operations** - Collection utilities
- **Expression building** - Dynamic expression construction

#### Key Extension Classes

- **AsyncRepository** - Asynchronous repository pattern implementation
- **PaginationResult** - Pagination support for queries
- **SortExpression** - Dynamic sorting capabilities
- **QueryableExtensions** - Enhanced LINQ operations

## Installation

### Package Manager

`
Install-Package Matorikkusu.Toolkit.ValidationAttributes
Install-Package Matorikkusu.Toolkit.Extensions
`

### .NET CLI

`
dotnet add package Matorikkusu.Toolkit.ValidationAttributes 
dotnet add package Matorikkusu.Toolkit.Extensions
`

## Usage Examples

### Validation Attributes in ASP.NET Core

```csharp
public class UserRegistrationModel
{
    [ValidNumberString(typeof(int), true, "Age must be a positive integer")]
    public string Age { get; set; }
    
    [NotEmpty("Name is required")]
    public string Name { get; set; }
    
    [HttpClientUrl("Please provide a valid URL")]
    public string Website { get; set; }
}
```

### Using Extensions

```csharp
// Queryable extensions with pagination
var result = users.AsQueryable()
    .ApplyPagination(pageNumber: 1, pageSize: 10)
    .ApplySort("Name", SortDirection.Ascending);

// String extensions
var hash = "password".ToSha256Hash();

// Enum extensions
var enumValue = MyEnum.Value1.GetDescription();
```

## Requirements

- **.NET 8.0** or later
- **ASP.NET Core** (for validation attributes)

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the [LICENSE](LICENSE) - see the LICENSE file for details.

## Support

For questions, issues, or contributions, please visit the [GitHub repository](https://github.com/matorikkusu/netcore-toolkit).

