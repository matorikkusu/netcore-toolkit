using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Matorikkusu.Toolkit.Extensions;

public static class QueryableExtensions
{
    public static async Task<PaginationResult<T>> GetPagination<T>(this IQueryable<T> query,
        int currentPage, int itemsPerPage) where T : class
    {
        if (itemsPerPage <= 0) itemsPerPage = 1;

        var skip = (currentPage - 1) * itemsPerPage;

        var queryCount = await query.CountAsync();

        return new PaginationResult<T>
        {
            CurrentPage = currentPage,
            ItemsPerPage = itemsPerPage,
            TotalItems = queryCount,
            TotalPages = (int)Math.Ceiling(queryCount / (double)itemsPerPage),
            Items = await query.Skip(skip).Take(itemsPerPage).ToListAsync()
        };
    }

    public static async Task<PaginationResult<TResult>> GetPagination<TSource, TResult>(
        this IQueryable<TSource> query,
        Expression<Func<TSource, TResult>> selector,
        int currentPage, int itemsPerPage)
    {
        if (itemsPerPage <= 0) itemsPerPage = 1;

        var skip = (currentPage - 1) * itemsPerPage;

        var queryCount = await query.CountAsync();

        return new PaginationResult<TResult>
        {
            CurrentPage = currentPage,
            ItemsPerPage = itemsPerPage,
            TotalItems = queryCount,
            TotalPages = (int)Math.Ceiling(queryCount / (double)itemsPerPage),
            Items = await query.Select(selector).Skip(skip).Take(itemsPerPage).ToListAsync()
        };
    }

    public static IQueryable<T> OrderByName<T>(this IQueryable<T> source, string propertyName, bool isDescending)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new ArgumentException("Order by property should not empty", nameof(propertyName));
        }

        propertyName = propertyName.Trim();

        var type = typeof(T);
        var arg = Expression.Parameter(type, "x");
        var propertyInfo = type.GetProperty(propertyName);
        var expression = Expression.Property(arg, propertyInfo);
        type = propertyInfo.PropertyType;

        var delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
        var lambda = Expression.Lambda(delegateType, expression, arg);

        var methodName = isDescending ? "OrderByDescending" : "OrderBy";
        var result = typeof(Queryable).GetMethods().Single(
                method => method.Name == methodName
                          && method.IsGenericMethodDefinition
                          && method.GetGenericArguments().Length == 2
                          && method.GetParameters().Length == 2)
            .MakeGenericMethod(typeof(T), type)
            .Invoke(null, new object[] { source, lambda });
        return (IQueryable<T>)result;
    }
}