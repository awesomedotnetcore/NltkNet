﻿using IronPython.Runtime;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NltkNet
{
    public static partial class Nltk
    {
        public static IEnumerable<T> ToEnumerable<T>(this IEnumerator<T> enumerator)
        {
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        #region From Python To .NET types converters

        public static Dictionary<T1, T2> ToDictionary<T1, T2>(IronPython.Runtime.List list)
        {
            Dictionary<T1, T2> result = new Dictionary<T1, T2>();
            foreach (PythonTuple item in list)
            {
                result.Add((T1)item[0], (T2)item[1]);
            }

            return result;
        }


        public static List<T> ToList<T>(this PythonGenerator generatorObj, Func<object, T> converter)
        {
            var result = new List<T>();
            foreach (T item in generatorObj)
                result.Add(converter(item));
            return result;
        }

        
        public static List<Tuple<T1, T2>> ToListTuple<T1, T2>(this PythonGenerator generatorObj)
        {
            List<Tuple<T1, T2>> result = new List<Tuple<T1, T2>>();
            foreach (PythonTuple item in generatorObj)
            {
                result.Add(new Tuple<T1, T2>((T1)item[0], (T2)item[1]));
            }

            return result;
        }

        public static List<Tuple<int, int>> ToListTupleIntInt(dynamic generatorObj) => ToListTuple<int, int>(generatorObj);

        #endregion


        #region from C# to Python extensions

        public static IronPython.Runtime.List ToPythonList<T>(this IEnumerable<T> list)
        {
            var result = new IronPython.Runtime.List();

            foreach (var item in list)
                result.Add(item);

            return result;
        }

        #endregion
    }
}
