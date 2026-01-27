using System;
using System.Linq.Expressions;

namespace Examples.Features.CSharp60.Tests.NameOfOperators.Fixtures
{
    public static class Helper
    {
        /// <summary>
        /// Helper methods for nameof before C# 6.
        /// </summary>
        /// <typeparam name="MemberType"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string NameOf<MemberType>(Expression<Func<MemberType>> expression)
        {
            return (expression.Body as MemberExpression).Member.Name;
        }
    }
}

