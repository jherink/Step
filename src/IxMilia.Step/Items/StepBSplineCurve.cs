// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using IxMilia.Step.Syntax;

namespace IxMilia.Step.Items
{
    public abstract class StepBSplineCurve : StepBoundedCurve
    {
        public int Degree { get; set; }

        public List<StepCartesianPoint> ControlPointsList { get; private set; }

        public BSplineCurveForm CurveForm { get; set; } = BSplineCurveForm.Unspecified;

        public bool ClosedCurve { get; set; }

        public bool SelfIntersect { get; set; }

        public StepBSplineCurve( string name, IEnumerable<StepCartesianPoint> controlPoints ) : base( name )
        {
            ControlPointsList = new List<StepCartesianPoint>( controlPoints );
        }

        public StepBSplineCurve( string name, params StepCartesianPoint[] controlPoints )
            : this( name, (IEnumerable<StepCartesianPoint>)controlPoints )
        {
        }

        internal override IEnumerable<StepRepresentationItem> GetReferencedItems()
        {
            return ControlPointsList;
        }

        internal override IEnumerable<StepSyntax> GetParameters( StepWriter writer )
        {
            foreach (var parameter in base.GetParameters( writer ) )
            {
                yield return parameter;
            }

            yield return new StepIntegerSyntax( Degree );
            yield return new StepSyntaxList( ControlPointsList.Select( c => writer.GetItemSyntax( c ) ) );
            yield return new StepEnumerationValueSyntax( ( new StepBSplineCurveFormParser().Get( CurveForm ) ) );
            yield return new StepEnumerationValueSyntax( ClosedCurve ? "T" : "F" );
            yield return new StepEnumerationValueSyntax( SelfIntersect ? "T" : "F" );
        }
    }
}
