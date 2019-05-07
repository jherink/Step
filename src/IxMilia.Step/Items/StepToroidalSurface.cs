// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;
using IxMilia.Step.Syntax;

namespace IxMilia.Step.Items
{
    public class StepToroidalSurface : StepElementarySurface
    {
        public override StepItemType ItemType => StepItemType.ToroidalSurface;

        private double _majorRadius;
        public double MajorRadius
        {
            get => _majorRadius;
            set
            {
                // Major radius of type "positive_length_measure"
                if ( value < 0 )
                {
                    throw new ArgumentException( "Major radius value must be greater than 0." );
                }
                _majorRadius = value;
            }
        }

        private double _minorRadius;
        public double MinorRadius
        {
            get => _minorRadius;
            set
            {
                // Minor radius of type "positive_length_measure"
                if ( value < 0 )
                {
                    throw new ArgumentException( "Minor radius value must be greater than 0." );
                }
                _minorRadius = value;
            }
        }

        public StepToroidalSurface( string name, 
                                    StepAxis2Placement3D position, 
                                    double majorRadius, 
                                    double minorRadius ) : base( name, position )
        {
            MajorRadius = majorRadius; MinorRadius = minorRadius;
        }

        public StepToroidalSurface()
        {
        }

        internal override IEnumerable<StepSyntax> GetParameters( StepWriter writer )
        {
            foreach ( var parameter in base.GetParameters( writer ) ) yield return parameter;

            yield return new StepRealSyntax( MajorRadius );
            yield return new StepRealSyntax( MinorRadius );
        }

        internal static StepToroidalSurface CreateFromSyntaxList( StepBinder binder, StepSyntaxList syntaxList )
        {
            syntaxList.AssertListCount( 4 );
            var surface = new StepToroidalSurface();
            surface.Name = syntaxList.Values[0].GetStringValue();
            binder.BindValue( syntaxList.Values[1], v => surface.Position = v.AsType<StepAxis2Placement3D>() );
            surface.MajorRadius = syntaxList.Values[2].GetRealVavlue();
            surface.MinorRadius = syntaxList.Values[3].GetRealVavlue();
            return surface;
        }
    }
}
