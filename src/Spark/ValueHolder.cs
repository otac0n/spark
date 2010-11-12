//-------------------------------------------------------------------------
// <copyright file="ValueHolder.cs">
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
    /// <summary>
    /// Utility methods to create value holders with inferred types
    /// </summary>
    public static class ValueHolder
    {
        public static ValueHolder<TValue> For<TValue>(Func<TValue> acquire)
        {
            return new ValueHolder<TValue>(acquire);
        }

        public static ValueHolder<TKey, TValue> For<TKey, TValue>(TKey key, Func<TValue> acquire)
        {
            return new ValueHolder<TKey, TValue>(key, acquire);
        }

        public static ValueHolder<TKey, TValue> For<TKey, TValue>(TKey key, Func<TKey, TValue> acquire)
        {
            return new ValueHolder<TKey, TValue>(key, acquire);
        }
    }

    /// <summary>
    /// Class holds a value to be acquired just-in-time when Value property accessed
    /// </summary>
    /// <typeparam name="TValue">Type for the value property</typeparam>
    public class ValueHolder<TValue>
    {
        private TValue _value;
        private Func<TValue> _acquire;

        public ValueHolder(Func<TValue> acquire)
        {
            _acquire = acquire;
        }

        public TValue Value
        {
            get
            {
                if (_acquire != null)
                {
                    lock (this)
                    {
                        if (_acquire != null)
                        {
                            _value = _acquire();
                            _acquire = null;
                        }
                    }
                }

                return _value;
            }
        }
    }

    /// <summary>
    /// Extends the base value holder by adding a typed Key property. Acquire 
    /// method may optionally take the key as an argument
    /// </summary>
    /// <typeparam name="TKey">Type for the key property</typeparam>
    /// <typeparam name="TValue">Type for the value property</typeparam>
    public class ValueHolder<TKey, TValue> : ValueHolder<TValue>
    {
        private readonly TKey _key;

        public ValueHolder(TKey key, Func<TValue> acquire)
            : base(acquire)
        {
            _key = key;
        }

        public ValueHolder(TKey key, Func<TKey, TValue> acquire)
            : base(() => acquire(key))
        {
            _key = key;
        }

        public TKey Key
        {
            get { return _key; }
        }
    }
}
