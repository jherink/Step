// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Generic;

namespace IxMilia.Step.Items
{
    public sealed class StepComplexRepresentation : StepRepresentationItem
    {
        public List<StepRepresentationItem> Items { get; private set; } = new List<StepRepresentationItem>();

        public StepComplexRepresentation() : base( string.Empty ) { }

        public StepComplexRepresentation( string name ) : base( name )
        {
        }

        public override StepItemType ItemType => StepItemType.ComplexRepresentation;


    }
}
