//-------------------------------------------------------------------------
// <copyright file="StringWriterOriginator.cs">
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
using System.IO;

namespace Spark.Caching
{
    public class StringWriterOriginator : TextWriterOriginator
    {
        private readonly StringWriter _writer;
        private int _priorLength;

        public StringWriterOriginator(StringWriter writer)
        {
            _writer = writer;
        }

        public override TextWriterMemento CreateMemento()
        {
            return new TextWriterMemento { Written = new[] { _writer.ToString() } };
        }

        public override void BeginMemento()
        {
            _priorLength = _writer.GetStringBuilder().Length;
        }

        public override TextWriterMemento EndMemento()
        {
            var currentLength = _writer.GetStringBuilder().Length;
            var written = _writer.GetStringBuilder().ToString(_priorLength, currentLength - _priorLength);
            return new TextWriterMemento { Written = new[] { written } };
        }

        public override void DoMemento(TextWriterMemento memento)
        {
            foreach (var written in memento.Written)
                _writer.Write(written);
        }
    }
}
