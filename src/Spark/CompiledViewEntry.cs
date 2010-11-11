//-------------------------------------------------------------------------
// <copyright file="CompiledViewEntry.cs">
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
using Spark.Compiler;
using Spark.Parser;

namespace Spark
{
    public class CompiledViewEntry : ISparkViewEntry
    {
        public Guid ViewId { get { return Compiler.GeneratedViewId; } }

        public SparkViewDescriptor Descriptor { get; set; }
        public ViewLoader Loader { get; set; }
        public ViewCompiler Compiler { get; set; }
        public IViewActivator Activator { get; set; }
        public ISparkLanguageFactory LanguageFactory { get; set; }

        public string SourceCode
        {
            get { return Compiler.SourceCode; }
        }

        public IList<SourceMapping> SourceMappings
        {
            get { return Compiler.SourceMappings; }
        }

        public ISparkView CreateInstance()
        {
            var view = Activator.Activate(Compiler.CompiledType);
            if (LanguageFactory != null)
                LanguageFactory.InstanceCreated(Compiler, view);
            return view;
        }

        public void ReleaseInstance(ISparkView view)
        {
            if (LanguageFactory != null)
                LanguageFactory.InstanceReleased(Compiler, view);
            Activator.Release(Compiler.CompiledType, view);
        }

        public bool IsCurrent() { return Loader.IsCurrent(); }
    }
}