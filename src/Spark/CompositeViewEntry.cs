//-------------------------------------------------------------------------
// <copyright file="CompositeViewEntry.cs">
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
using Spark.Compiler;
using Spark.Parser;

namespace Spark
{
    public class CompositeViewEntry : ISparkViewEntry
    {
        public CompositeViewEntry()
        {
            ViewId = Guid.NewGuid();
        }

        public Guid ViewId { get; set; }

        public SparkViewDescriptor Descriptor { get; set; }

        public ViewLoader Loader { get; set; }

        public ViewCompiler Compiler { get; set; }

        public IViewActivator Activator { get; set; }

        public ISparkLanguageFactory LanguageFactory { get; set; }

        public ISparkView CreateInstance()
        {
            throw new System.NotImplementedException();
        }

        public void ReleaseInstance(ISparkView view)
        {
            throw new System.NotImplementedException();
        }

        public bool IsCurrent()
        {
            throw new System.NotImplementedException();
        }

        public string SourceCode
        {
            get { throw new System.NotImplementedException(); }
        }

        public IList<SourceMapping> SourceMappings
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
