// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using IxMilia.Step.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace IxMilia.Step.Items
{
    public class StepBSplineSurface : StepBoundedSurface
    {
        public override StepItemType ItemType => StepItemType.BSplineSurface;

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
            yield return new StepEnumerationValueSyntax( SelfIntersect ? "T" : "F" );
        }

        internal static StepBSplineSurface CreateFromSyntaxList( StepBinder binder, StepSyntaxList syntaxList )
        {
            syntaxList.AssertListCount( 8 );
            var controlPointsList = syntaxList.Values[3].GetValueList();

            var surface = new StepBSplineSurface( syntaxList.Values[0].GetStringValue(),
                                                  new StepCartesianPoint[controlPointsList.Values.Count][] )
            {
                UDegree = syntaxList.Values[1].GetIntegerValue(),
                VDegree = syntaxList.Values[2].GetIntegerValue()
            };

            // control points
            for ( int i = 0; i < controlPointsList.Values.Count; i++ )
            {
                var array = controlPointsList.Values[i].GetValueList();
                var bindedArray = new StepCartesianPoint[array.Values.Count];
                for ( int j = 0; j < array.Values.Count; j++ )
                {
                    var k = j; // capture to avoid rebinding
                    binder.BindValue( array.Values[k], v => bindedArray[k] = v.AsType<StepCartesianPoint>() );
                }
                surface.ControlPointsList[i] = bindedArray;
            }

            surface.SurfaceForm = new StepBSplineSurfaceFormParser().Parse( syntaxList.Values[4].GetEnumerationValue() );
            surface.UClosed = syntaxList.Values[5].GetBooleanValue();
            surface.VClosed = syntaxList.Values[6].GetBooleanValue();
            surface.SelfIntersect = syntaxList.Values[7].GetBooleanValue();
            
            return surface;
        }
    }
}
