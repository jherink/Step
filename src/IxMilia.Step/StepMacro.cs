﻿// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using IxMilia.Step.Tokens;

namespace IxMilia.Step
{
    internal class StepMacro
    {
        public StepKeywordToken Keyword { get; }
        public StepValueList Values { get; }

        public StepMacro(StepKeywordToken keyword, StepValueList values)
        {
            Keyword = keyword;
            Values = values;
        }
    }
}