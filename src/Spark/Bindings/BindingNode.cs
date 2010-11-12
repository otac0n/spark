//-------------------------------------------------------------------------
// <copyright file="BindingNode.cs">
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

using System.Collections.Generic;
using System.Linq;

namespace Spark.Bindings
{
    public class BindingNode
    {
    }

    public class BindingLiteral : BindingNode
    {
        public BindingLiteral(IEnumerable<char> text)
        {
            Text = new string(text.ToArray());
        }

        public string Text { get; set; }
    }

    public class BindingNameReference : BindingNode
    {
        public BindingNameReference(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public bool AssumeStringValue { get; set; }
    }

    public class BindingPrefixReference : BindingNode
    {
        public BindingPrefixReference(string prefix)
        {
            Prefix = prefix;
        }

        public string Prefix { get; set; }
        
        public bool AssumeStringValue { get; set; }

        public bool AssumeDictionarySyntax { get; set; }
    }

    public class BindingChildReference : BindingNode
    {
    }
}