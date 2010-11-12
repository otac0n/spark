//-------------------------------------------------------------------------
// <copyright file="CacheSignal.cs">
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
    public class CacheSignal : ICacheSignal
    {
        private EventHandler _changed;
        private bool _enabled;

        /// <summary>
        /// Changed event calls Enable and Disable when 
        /// any handlers are added and all handlers are
        /// removed respectively.
        /// </summary>
        public event EventHandler Changed
        {
            add
            {
                lock (this)
                {
                    _changed += value;
                    if (_enabled) 
                        return;

                    Enable();
                    _enabled = true;
                }
            }

            remove
            {
                lock (this)
                {
                    _changed -= value;
                    if (_enabled != true || ChangedIsEmpty() == false) 
                        return;

                    Disable();
                    _enabled = false;
                }
            }
        }

        private bool ChangedIsEmpty()
        {
            return _changed == null || 
                   _changed.GetInvocationList().Length == 0;
        }

        /// <summary>
        /// Optionally implemented by descendant to wire up the infrastructure
        /// to produce Changed events. That infrastructure is expected
        /// to call FireChanged.
        /// </summary>
        protected virtual void Enable()
        {            
        }

        /// <summary>
        /// Optionally implemented by descendant to tear down listening infrastructure
        /// when no cache dependencies remain listenning to the signal.
        /// </summary>
        protected virtual void Disable()
        {            
        }

        /// <summary>
        /// Called externally or from a descendant class to signal cache entries
        /// connected to this signal should all be removed
        /// </summary>
        public void FireChanged()
        {
            if (_changed != null) 
                _changed(this, EventArgs.Empty);
        }
    }
}
