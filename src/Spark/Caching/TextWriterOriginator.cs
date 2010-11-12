//-------------------------------------------------------------------------
// <copyright file="TextWriterOriginator.cs">
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
using System.IO;
using Spark.Spool;

namespace Spark.Caching
{
    public abstract class TextWriterOriginator
    {
        public static TextWriterOriginator Create(TextWriter writer)
        {
            if (writer is SpoolWriter)
                return new SpoolWriterOriginator((SpoolWriter)writer);
            if (writer is StringWriter)
                return new StringWriterOriginator((StringWriter)writer);
            throw new InvalidCastException("writer is unknown type " + writer.GetType().FullName);
        }

        public abstract TextWriterMemento CreateMemento();
        
        public abstract void BeginMemento();

        public abstract TextWriterMemento EndMemento();

        public abstract void DoMemento(TextWriterMemento memento);
    }

    public class TextWriterMemento
    {
        public IEnumerable<string> Written { get; set; }
    }
}