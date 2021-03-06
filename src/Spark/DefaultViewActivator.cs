//-------------------------------------------------------------------------
// <copyright file="DefaultViewActivator.cs">
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
    public class DefaultViewActivator : IViewActivatorFactory, IViewActivator
    {
        public IViewActivator Register(Type type)
        {
            return this;
        }

        public void Unregister(Type type, IViewActivator activator)
        {
        }

        public ISparkView Activate(Type type)
        {
            return (ISparkView)Activator.CreateInstance(type);
        }

        public void Release(Type type, ISparkView view)
        {
        }
    }
}
