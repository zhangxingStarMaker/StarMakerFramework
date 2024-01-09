﻿using System;
using System.Collections.Generic;

namespace Module.Utility
{
    public static class EnumerableEx
    {
        /// <summary>
        ///     从集合中找到
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iEnumerable"></param>
        /// <param name="fliter"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T FindFirst<T>(this IEnumerable<T> iEnumerable, Func<T, bool> fliter, T defaultValue = default(T))
        {
            foreach (var v in iEnumerable)
                if (fliter(v))
                    return v;
            return defaultValue;
        }

        /// <summary>
        ///     遍历迭代器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iEnumerable"></param>
        /// <param name="action"></param>
        public static void Foreach<T>(this IEnumerable<T> iEnumerable, Action<T> action)
        {
            var e = iEnumerable.GetEnumerator();
            while (e.MoveNext())
            {
                if (action == null)
                    continue;
                action(e.Current);
            }

            e.Dispose();
        }
    }
}