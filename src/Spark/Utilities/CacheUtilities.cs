//-------------------------------------------------------------------------
// <copyright file="CacheUtilities.cs">
// Copyright 2008-2010 Louis DeJardin - http://whereslou.com
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Louis DeJardin</author>
//-------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spark.Utilities
{
    public static class CacheUtilities
    {
        public static string ToIdentifier(string site, object[] key)
        {
            if (key.Length == 0)
                return site;

            if (key.Length == 1)
                return site + key[0];

            const string unitSeperator = "\u001f";
            var parts = new object[key.Length * 2];
            parts[0] = site;
            parts[1] = key[0];
            for (var index = 1; index != key.Length; ++index)
            {
                parts[index * 2] = unitSeperator;
                parts[index * 2 + 1] = key[index];
            }
            return string.Concat(parts);
        }
    }
}
