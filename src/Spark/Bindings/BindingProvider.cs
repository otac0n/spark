//-------------------------------------------------------------------------
// <copyright file="BindingProvider.cs">
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
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Spark.Compiler;
using Spark.FileSystem;
using Spark.Parser;

namespace Spark.Bindings
{
    public abstract class BindingProvider : IBindingProvider
    {
        public IEnumerable<Binding> LoadStandardMarkup(TextReader reader)
        {
            var document = XDocument.Load(reader);
            var elements = document.Elements("bindings").Elements("element");

            var grammar = new BindingGrammar();
            var bindings = elements.Select(element => ParseBinding(element, grammar));

            return bindings;
        }

        private static Binding ParseBinding(XElement element, BindingGrammar grammar)
        {
            var binding = new Binding
                          {
                              ElementName = (string)element.Attribute("name")
                          };

            var start = element.Element("start");
            var end = element.Element("end");

            if (start != null && end != null)
            {
                binding.Phrases = new[]
                                  {
                                      ParsePhrase(start, grammar),
                                      ParsePhrase(end, grammar)
                                  };
            }
            else
            {
                binding.Phrases = new[]
                                  {
                                      ParsePhrase(element, grammar)
                                  };
            }

            binding.HasChildReference = binding.Phrases
                .SelectMany(phrase => phrase.Nodes)
                .OfType<BindingChildReference>()
                .Any();
            
            if (binding.Phrases.Count() > 1 && binding.HasChildReference)
            {
                throw new CompilerException("Binding element '" + element.Attribute("name") +
                                            "' can not have child::* in start or end phrases.");
            }

            return binding;
        }

        private static BindingPhrase ParsePhrase(XElement element, BindingGrammar grammar)
        {
            return grammar.Phrase(new Position(new SourceContext(element.Value))).Value;
        }

        public abstract IEnumerable<Binding> GetBindings(IViewFolder viewFolder);
    }
}
