// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using IxMilia.Step.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace IxMilia.Step.Items
{


    public abstract class StepBSplineSurface : StepBoundedSurface
    {
        public int UDegree { get; set; }

        public int VDegree { get; set; }

        public BSplineSurfaceForm SurfaceForm { get; set; } = BSplineSurfaceForm.Unspecified;

        public List<StepCartesianPoint[]> ControlPointsList { get; private set; } = new List<StepCartesianPoint[]>();

        public bool UClosed { get; set; }

        public bool VClosed { get; set; }
        
        public bool SelfIntersect { get; set; }

        public StepBSplineSurface( string name, IEnumerable<StepCartesianPoint[]> controlPoints ) 
            : base( name )
        {
            ControlPointsList = new List<StepCartesianPoint[]>( controlPoints );
        }

        public StepBSplineSurface( string name, params StepCartesianPoint[][] controlPoints ) 
            : this( name, (IEnumerable<StepCartesianPoint[]>)controlPoints )
        {
        }

        internal override IEnumerable<StepRepresentationItem> GetReferencedItems()
        {
            foreach ( var item in base.GetReferencedItems() )
            {
                yield return item;
            }
            foreach ( var array_of_pts in ControlPointsList )
            {
                foreach ( var pt in array_of_pts )
                {
                    yield return pt;
                }
            }
        }

        internal override IEnumerable<StepSyntax> GetParameters( StepWriter writer )
        {
            foreach ( var parameter in base.GetParameters( writer ) )
            {
                yield return parameter;
            }

            yield return new StepIntegerSyntax( UDegree );
            yield return new StepIntegerSyntax( VDegree );
            // returning an array of arrays
            yield return new StepSyntaxList( ControlPointsList.Select( array_of_array => 
                                                                       new StepSyntaxList( array_of_array.Select( array => 
                                                                                                                  writer.GetItemSyntax( array ) ) ) ) );
            yield return new StepEnumerationValueSyntax( new StepBSplineSurfaceFormParser().Get( SurfaceForm ) );
            yield return new StepEnumerationValueSyntax( UClosed ? "T" : "F" );
            yield return new StepEnumerationValueSyntax( VClosed ? "T" : "F" );
        }
    }
}
