//-------------------------------------------------------------------------
// <copyright file="BindingGrammar.cs">
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
using Spark.Parser;

namespace Spark.Bindings
{
    // ReSharper disable InconsistentNaming
    public class BindingGrammar : CharGrammar
    {
        public BindingGrammar()
        {
            //[4]   	NameChar	   ::=   	 Letter | Digit | '.' | '-' | '_' | ':' | CombiningChar | Extender  
            var NameChar = Ch(char.IsLetterOrDigit).Or(Ch('.', '-', '_', ':')); // .Or(CombiningChar).Or(Extender)

            //[5]   	Name	   ::=   	(Letter | '_' | ':') (NameChar)*
            var Name =
                Ch(char.IsLetter).Or(Ch('_', ':')).And(Rep(NameChar))
                .Build(hit => hit.Left + new string(hit.Down.ToArray()));

            var stringPrefixReference =
                Ch("\"@").And(Opt(Name)).And(Ch("*\""))
                .Or(Ch("'@").And(Opt(Name)).And(Ch("*'")))
                .Build(hit => new BindingPrefixReference(hit.Left.Down) { AssumeStringValue = true });

            var rawPrefixReference = Ch('@').And(Opt(Name)).And(Ch('*'))
                .Build(hit => new BindingPrefixReference(hit.Left.Down));

            var dictionaryPrefixReference = Ch("{{").And(stringPrefixReference.Or(rawPrefixReference)).And(Ch("}}"))
                .Build(hit =>
                       {
                           hit.Left.Down.AssumeDictionarySyntax = true;
                           return hit.Left.Down;
                       });

            PrefixReference = stringPrefixReference.Or(rawPrefixReference).Or(dictionaryPrefixReference)
                .Build(hit => (BindingNode)hit);

            var stringNameReference = Ch("\"@").And(Name).And(Ch('\"'))
                .Or(Ch("'@").And(Name).And(Ch('\'')))
                .Build(hit => (BindingNode)new BindingNameReference(hit.Left.Down) { AssumeStringValue = true });

            var rawNameReference = Ch('@').And(Name)
                .Build(hit => (BindingNode)new BindingNameReference(hit.Down));

            var childReference = Ch("child::*").Or(Ch("'child::*'")).Or(Ch("\"child::*\""))
                .Build(hit => (BindingNode)new BindingChildReference());

            NameReference = stringNameReference.Or(rawNameReference);

            var anyReference = PrefixReference.Or(NameReference).Or(childReference);

            var escapedLiteral = Ch("[[").Build(hit => '<').Or(Ch("]]").Build(hit => '>'));
            var plainLiteral = Ch(ch => true).Unless(anyReference).Unless(escapedLiteral);

            Literal = Rep1(escapedLiteral.Or(plainLiteral))
                .Build(hit => (BindingNode)new BindingLiteral(hit));

            Nodes = Rep(anyReference.Or(Literal));

            Phrase =
                Ch("#").And(Nodes).Build(hit => new BindingPhrase { Type = BindingPhrase.PhraseType.Statement, Nodes = hit.Down })
                .Or(Nodes.Build(hit => new BindingPhrase { Type = BindingPhrase.PhraseType.Expression, Nodes = hit }));
        }

        public ParseAction<BindingNode> PrefixReference { get; set; }
        
        public ParseAction<BindingNode> NameReference { get; set; }
        
        public ParseAction<BindingNode> Literal { get; set; }
        
        public ParseAction<IList<BindingNode>> Nodes { get; set; }

        public ParseAction<BindingPhrase> Phrase { get; set; }
    }
}
