//-------------------------------------------------------------------------
// <copyright file="DefaultCacheService.cs">
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
using System.Web.Caching;

namespace Spark.Caching
{
    public class DefaultCacheService : ICacheService
    {
        private readonly Cache _cache;

        public DefaultCacheService(Cache cache)
        {
            _cache = cache;
        }

        public object Get(string identifier)
        {
            return _cache.Get(identifier);
        }

        public void Store(string identifier, CacheExpires expires, ICacheSignal signal, object item)
        {
            _cache.Insert(
                identifier,
                item,
                SignalDependency.For(signal),
                (expires ?? CacheExpires.Empty).Absolute,
                (expires ?? CacheExpires.Empty).Sliding);
        }

        class SignalDependency : CacheDependency
        {
            private readonly ICacheSignal _signal;

            SignalDependency(ICacheSignal signal)
            {
                _signal = signal;
                _signal.Changed += SignalChanged;
            }

            ~SignalDependency()
            {
                _signal.Changed -= SignalChanged;
            }

            public static CacheDependency For(ICacheSignal signal)
            {
                return signal == null ? null : new SignalDependency(signal);
            }

            void SignalChanged(object sender, EventArgs e)
            {
                NotifyDependencyChanged(this, e);
            }

            protected override void DependencyDispose()
            {
                _signal.Changed -= SignalChanged;
            }
        }
    }
}
