// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using IxMilia.Step.Syntax;

namespace IxMilia.Step.Items
{
    public class StepSphericalSurface : StepElementarySurface
    {
        public override StepItemType ItemType => StepItemType.SphericalSurface;

        private double _radius;
        public double Radius { get => _radius;
            set
            {
                // Radius of type "positive_length_measure"
                if ( value < 0 )
                {
                    throw new ArgumentException( "Radius value must be greater than 0." );
                }
                _radius = value;
            }
        }

        public StepSphericalSurface( string name, StepAxis2Placement3D position, double radius ) : base( name, position )
        {
            Radius = radius;
        }

        protected StepSphericalSurface()
        {
        }

        internal override IEnumerable<StepSyntax> GetParameters( StepWriter writer )
        {
            foreach ( var parameter in base.GetParameters( writer ) ) yield return parameter;

            yield return new StepRealSyntax( Radius );
        }

        internal static StepSphericalSurface CreateFromSyntaxList( StepBinder binder, StepSyntaxList syntaxList )
        {
            syntaxList.AssertListCount( 3 );
            var surface = new StepSphericalSurface();
            surface.Name = syntaxList.Values[0].GetStringValue();
            binder.BindValue( syntaxList.Values[1], v => surface.Position = v.AsType<StepAxis2Placement3D>() );
            surface.Radius = syntaxList.Values[2].GetRealVavlue();
            return surface;
        }
    }
}
