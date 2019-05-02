// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IxMilia.Step.Syntax;

namespace IxMilia.Step.Items
{
    public class StepBSplineSurfaceWithKnots : StepBSplineSurface
    {

        #region UMultiplicities

        private List<int> _uMultiplicities = new List<int>();
        public List<int> UMultiplicities
        {
            get => _uMultiplicities;
            set
            {
                if ( value == null )
                {
                    throw new ArgumentNullException();
                }

                _uMultiplicities = value;
            }
        }

        #endregion

        #region VMultiplicities

        private List<int> _vMultiplicities = new List<int>();
        public List<int> VMultiplicities
        {
            get => _vMultiplicities;
            set
            {
                if ( value == null )
                {
                    throw new ArgumentNullException();
                }

                _vMultiplicities = value;
            }
        }

        #endregion

        #region UKnots

        private List<double> _uKnots = new List<double>();
        public List<double> UKnots
        {
            get => _uKnots;
            set
            {
                if ( value == null )
                {
                    throw new ArgumentNullException();
                }

                _uKnots = value;
            }
        }

        #endregion

        #region VKnots

        private List<double> _vKnots = new List<double>();
        public List<double> VKnots
        {
            get => _vKnots;
            set
            {
                if ( value == null )
                {
                    throw new ArgumentNullException();
                }

                _vKnots = value;
            }
        }

        #endregion

        public KnotType KnotSpec { get; set; } = KnotType.Unspecified;

        public StepBSplineSurfaceWithKnots( string name,
                                            IEnumerable<StepCartesianPoint[]> controlPoints ) : base( name, controlPoints )
        {
        }

        public StepBSplineSurfaceWithKnots( string name,
                                            params StepCartesianPoint[][] controlPoints ) : base( name, controlPoints )
        {
        }

        public override StepItemType ItemType => StepItemType.BSplineSurfaceWithKnots;

        internal override IEnumerable<StepSyntax> GetParameters( StepWriter writer )
        {
            foreach ( var parameter in base.GetParameters( writer ) )
            {
                yield return parameter;
            }

            yield return new StepSyntaxList( UMultiplicities.Select( m => new StepIntegerSyntax( m ) ) );
            yield return new StepSyntaxList( VMultiplicities.Select( m => new StepIntegerSyntax( m ) ) );
            yield return new StepSyntaxList( UKnots.Select( k => new StepRealSyntax( k ) ) );
            yield return new StepSyntaxList( VKnots.Select( k => new StepRealSyntax( k ) ) );
            yield return new StepEnumerationValueSyntax( new StepKnotTypeValueParser().Get( KnotSpec ) );
        }

        internal static StepBSplineSurfaceWithKnots CreateFromSyntaxList( StepBinder binder, StepSyntaxList syntaxList )
        {
            syntaxList.AssertListCount( 13 );
            var controlPointsList = syntaxList.Values[3].GetValueList();

            var surface = new StepBSplineSurfaceWithKnots( syntaxList.Values[0].GetStringValue(),
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

            var uMultiplicitiesList = syntaxList.Values[8].GetValueList();
            for( int i = 0; i < uMultiplicitiesList.Values.Count; i++ )
            {
                surface.UMultiplicities.Add( uMultiplicitiesList.Values[i].GetIntegerValue() );
            }

            var vMultiplicitiesList = syntaxList.Values[9].GetValueList();
            for ( int i = 0; i < vMultiplicitiesList.Values.Count; i++ )
            {
                surface.VMultiplicities.Add( vMultiplicitiesList.Values[i].GetIntegerValue() );
            }

            var uKnotsList = syntaxList.Values[10].GetValueList();
            for ( int i = 0; i < uKnotsList.Values.Count; i++ )
            {
                surface.UKnots.Add( uKnotsList.Values[i].GetRealVavlue() );
            }

            var vKnotsList = syntaxList.Values[11].GetValueList();
            for ( int i = 0; i < vKnotsList.Values.Count; i++ )
            {
                surface.VKnots.Add( vKnotsList.Values[i].GetRealVavlue() );
            }

            surface.KnotSpec = new StepKnotTypeValueParser().Parse( syntaxList.Values[12].GetEnumerationValue() );
            return surface;
        }
    }
}
