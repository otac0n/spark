//-------------------------------------------------------------------------
// <copyright file="CacheExpires.cs">
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

namespace Spark
{
    public class CacheExpires
    {
        private static CacheExpires _empty = new CacheExpires();

        public CacheExpires()
        {
            Absolute = NoAbsoluteExpiration;
            Sliding = NoSlidingExpiration;
        }

        public CacheExpires(DateTime absolute)
        {
            Absolute = absolute;
            Sliding = NoSlidingExpiration;
        }

        public CacheExpires(TimeSpan sliding)
        {
            Absolute = NoAbsoluteExpiration;
            Sliding = sliding;
        }

        public CacheExpires(double sliding)
            : this(TimeSpan.FromSeconds(sliding))
        {
        }

        public DateTime Absolute { get; set; }

        public TimeSpan Sliding { get; set; }

        public static DateTime NoAbsoluteExpiration { get { return System.Web.Caching.Cache.NoAbsoluteExpiration; } }

        public static TimeSpan NoSlidingExpiration { get { return System.Web.Caching.Cache.NoSlidingExpiration; } }

        public static CacheExpires Empty { get { return _empty; } }
    }
}