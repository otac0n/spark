//-------------------------------------------------------------------------
// <copyright file="CodeProcessingChunkVisitor.cs">
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

using Spark.Compiler.ChunkVisitors;
using Spark.Parser.Code;

namespace Spark.Compiler.Javascript.ChunkVisitors
{
    /// <summary>
    /// Abstract visitor which passes the properties that may contain code
    /// through a processing method. Used for things like converting anonymous
    /// type allocation from csharp syntax to javascript syntax.
    /// </summary>
    public abstract class CodeProcessingChunkVisitor : ChunkVisitor
    {
        public abstract Snippets Process(Chunk chunk, Snippets code);

        protected override void Visit(GlobalVariableChunk chunk)
        {
            chunk.Value = Process(chunk, chunk.Value);
            base.Visit(chunk);
        }

        protected override void Visit(LocalVariableChunk chunk)
        {
            chunk.Value = Process(chunk, chunk.Value);
            base.Visit(chunk);
        }

        protected override void Visit(DefaultVariableChunk chunk)
        {
            chunk.Value = Process(chunk, chunk.Value);
            base.Visit(chunk);
        }

        protected override void Visit(AssignVariableChunk chunk)
        {
            chunk.Value = Process(chunk, chunk.Value);
            base.Visit(chunk);
        }

        protected override void Visit(SendExpressionChunk chunk)
        {
            chunk.Code = Process(chunk, chunk.Code);
            base.Visit(chunk);
        }

        protected override void Visit(CodeStatementChunk chunk)
        {
            chunk.Code = Process(chunk, chunk.Code);
            base.Visit(chunk);
        }

        protected override void Visit(ConditionalChunk chunk)
        {
            chunk.Condition = Process(chunk, chunk.Condition);
            base.Visit(chunk);
        }

        protected override void Visit(ForEachChunk chunk)
        {
            chunk.Code = Process(chunk, chunk.Code);
            base.Visit(chunk);
        }
    }
}