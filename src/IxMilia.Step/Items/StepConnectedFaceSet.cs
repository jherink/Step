// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using IxMilia.Step.Syntax;

namespace IxMilia.Step.Items
{
    public abstract class StepConnectedFaceSet : StepTopologicalRepresentationItem
    {       
        public List<StepFace> CfsFaces { get; }

        protected StepConnectedFaceSet( string name, IEnumerable<StepFace> cfsFaces ) : 
            base( name )
        {
            CfsFaces = new List<StepFace>( cfsFaces );
        }

        internal override IEnumerable<StepRepresentationItem> GetReferencedItems()
        {
            foreach ( var item in base.GetReferencedItems() ) yield return item;
            foreach ( var face in CfsFaces) yield return face;
        }

        internal override IEnumerable<StepSyntax> GetParameters( StepWriter writer )
        {
            foreach ( var parameter in base.GetParameters( writer ) )
            {
                yield return parameter;
            }

            yield return new StepSyntaxList( CfsFaces.Select( c => writer.GetItemSyntax( c ) ) );
        }
    }
}
