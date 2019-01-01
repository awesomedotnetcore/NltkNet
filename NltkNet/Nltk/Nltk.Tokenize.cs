﻿using IronPython.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NltkNet
{
    public static partial class Nltk
    {
        public static class Tokenize
        {
            private static NltkResultListString TokenizeCall(string funcName, string text)
            {
                IronPython.Runtime.List list = Call(funcName, text);
                var result = list.Cast<string>().ToList();

                return new NltkResultListString()
                {
                    AsNet = result,
                    AsPython = list,
                };
            }

            public static NltkResultListString WordTokenize(string text) => TokenizeCall("word_tokenize", text);
            
            public static NltkResultListString WordpunctTokenize(string text) => TokenizeCall("wordpunct_tokenize", text);

            public static NltkResultListString SentTokenize(string text) => TokenizeCall("sent_tokenize", text);


            public static class Util
            {
                public static NltkResultListTupleIntInt RegexpSpanTokenize(string text, string regexp)
                {
                    PythonGenerator generator = Call("regexp_span_tokenize", text, regexp);
                    var result = ToListTuple<int,int>(generator);

                    return new NltkResultListTupleIntInt()
                    {
                        AsNet = result,
                        AsPython = generator,
                    };
                }
            }
        }
    }
}
