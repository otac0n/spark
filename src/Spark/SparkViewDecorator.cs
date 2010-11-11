//-------------------------------------------------------------------------
// <copyright file="SparkViewDecorator.cs">
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
using Spark.Spool;

namespace Spark
{
    public abstract class SparkViewDecorator : SparkViewBase
    {
        private readonly SparkViewBase _decorated;

        protected SparkViewDecorator(SparkViewBase decorated)
        {
            _decorated = decorated;
        }

        public override SparkViewContext SparkViewContext
        {
            get
            {
                return _decorated != null ? _decorated.SparkViewContext : base.SparkViewContext;
            }
            set
            {
                if (_decorated != null)
                    _decorated.SparkViewContext = value;
                else
                    base.SparkViewContext = value;
            }
        }

        public override void RenderView(System.IO.TextWriter writer)
        {
            if (_decorated != null)
            {
                var spooled = new SpoolWriter();
                _decorated.RenderView(spooled);
                Content["view"] = spooled;
            }
            base.RenderView(writer);
        }
    }
}
