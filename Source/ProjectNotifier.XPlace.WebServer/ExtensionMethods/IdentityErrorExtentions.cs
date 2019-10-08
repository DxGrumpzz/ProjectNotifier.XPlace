namespace ProjectNotifier.XPlace.WebServer
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    public static class IdentityErrorExtentions
    {

        /// <summary>
        /// "Converts" an error list to a string, Seperated by a newline
        /// </summary>
        /// <param name="errorList"></param>
        /// <returns></returns>
        public static string ToErrorString(this IEnumerable<IdentityError> errorList)
        {
            // Convert creation error list to a string
            var errors = errorList
            // "Convert" IdentityError to string
            .Select(error => error.Description)
            // Accumulate errors by appending them as strings
            .Aggregate((accumulator, item) => $"{accumulator}{Environment.NewLine}{item}");

            return errors;
        }

    };
};