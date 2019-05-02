// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Generic;
using IxMilia.Step.Syntax;

namespace IxMilia.Step.Items
{
    public class StepConicalSurface : StepElementarySurface
    {
        public double Radius { get; set; }

        public double SemiAngle { get; set; }

        public override StepItemType ItemType => StepItemType.ConicalSurface;

        internal override IEnumerable<StepSyntax> GetParameters( StepWriter writer )
        {
            foreach ( var parameter in base.GetParameters( writer ) ) yield return parameter;

            yield return new StepRealSyntax( Radius );
            yield return new StepRealSyntax( SemiAngle );
        }

        internal static StepConicalSurface CreateFromSyntaxList( StepBinder binder, StepSyntaxList syntaxList )
        {
            syntaxList.AssertListCount( 4 );
            var surface = new StepConicalSurface();
            surface.Name = syntaxList.Values[0].GetStringValue();
            binder.BindValue( syntaxList.Values[1], v => surface.Position = v.AsType<StepAxis2Placement3D>() );
            surface.Radius = syntaxList.Values[2].GetRealVavlue();
            surface.SemiAngle = syntaxList.Values[3].GetRealVavlue();
            return surface;
        }
    }
}
