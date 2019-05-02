// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

namespace IxMilia.Step.Items
{
    internal interface IStepEnumerationValueParser<T>
    {
        T Parse( string enumerationValue );
        string Get( T value );
    }
}
