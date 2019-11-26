namespace ProjectNotifier.XPlace.WebServer
{
    using System;
    using System.Collections.Generic;

    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Provides a way to iterate over an <see cref="IEnumerable{T}"/> without having to write a foreach loop
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"> The list to iterate over </param>
        /// <param name="action"> The action to perform on the element </param>
        /// <returns></returns>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            // Iterate over the enumerable's elements
            foreach (var element in enumerable)
            {
                // Invoke action
                action.Invoke(element);
            };
        }
    }
}
