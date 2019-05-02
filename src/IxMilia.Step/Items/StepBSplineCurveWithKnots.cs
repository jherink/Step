// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using IxMilia.Step.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IxMilia.Step.Items
{
    public class StepBSplineCurveWithKnots : StepBSplineCurve
    {
        private List<int> _knotMultiplicities = new List<int>();
        public List<int> KnotMultiplicities
        {
            get => _knotMultiplicities;
            set
            {
                if ( value == null )
                {
                    throw new ArgumentNullException();
                }

                _knotMultiplicities = value;
            }
        }

        private List<double> _knots = new List<double>();
        public List<double> Knots
        {
            get => _knots;
            set
            {
                if ( value == null )
                {
                    throw new ArgumentNullException();
                }

                _knots = value;
            }
        }

        public KnotType KnotSpec { get; set; } = KnotType.Unspecified;

        public StepBSplineCurveWithKnots( string name,
                                          IEnumerable<StepCartesianPoint> controlPoints ) : base( name, controlPoints )
        {
        }

        public StepBSplineCurveWithKnots( string name, params StepCartesianPoint[] controlPoints ) : base( name, controlPoints )
        {
        }

        public override StepItemType ItemType => StepItemType.BSplineCurveWithKnots;
        
        internal override IEnumerable<StepSyntax> GetParameters( StepWriter writer )
        {
            foreach ( var parameter in base.GetParameters( writer ) )
            {
                yield return parameter;
            }

            yield return new StepSyntaxList( KnotMultiplicities.Select( m => new StepIntegerSyntax( m ) ) );
            yield return new StepSyntaxList( Knots.Select( k => new StepRealSyntax( k ) ) );
            yield return new StepEnumerationValueSyntax( ( new StepKnotTypeValueParser() ).Get( KnotSpec ) );
        }

        internal static StepBSplineCurveWithKnots CreateFromSyntaxList( StepBinder binder, StepSyntaxList syntaxList )
        {
            syntaxList.AssertListCount( 9 );
            var controlPointsList = syntaxList.Values[2].GetValueList();

            var spline = new StepBSplineCurveWithKnots( string.Empty, new StepCartesianPoint[controlPointsList.Values.Count] );
            spline.Name = syntaxList.Values[0].GetStringValue();
            spline.Degree = syntaxList.Values[1].GetIntegerValue();

            for ( int i = 0; i < controlPointsList.Values.Count; i++ )
            {
                var j = i; // capture to avoid rebinding
                binder.BindValue( controlPointsList.Values[j], v => spline.ControlPointsList[j] = v.AsType<StepCartesianPoint>() );
            }

            spline.CurveForm = new StepBSplineCurveFormParser().Parse( syntaxList.Values[3].GetEnumerationValue() );
            spline.ClosedCurve = syntaxList.Values[4].GetBooleanValue();
            spline.SelfIntersect = syntaxList.Values[5].GetBooleanValue();

            var knotMultiplicitiesList = syntaxList.Values[6].GetValueList();
            spline.KnotMultiplicities = new List<int>();
            for ( int i = 0; i < knotMultiplicitiesList.Values.Count; i++ )
            {
                spline.KnotMultiplicities.Add( knotMultiplicitiesList.Values[i].GetIntegerValue() );
            }

            var knotslist = syntaxList.Values[7].GetValueList();
            spline.Knots = new List<double>();
            for ( int i = 0; i < knotslist.Values.Count; i++ )
            {
                spline.Knots.Add( knotslist.Values[i].GetRealVavlue() );
            }

            spline.KnotSpec = ( new StepKnotTypeValueParser() ).Parse( syntaxList.Values[8].GetEnumerationValue() );

            return spline;
        }
    }
}
