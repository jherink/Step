// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;

namespace IxMilia.Step.Items
{
    public enum BSplineCurveForm
    {
        Polyline,
        CircularArc,
        EllipticalArc,
        ParabolicArc,
        HyperbolicArc,
        Unspecified
    };

    internal sealed class StepBSplineCurveFormParser : IStepEnumerationValueParser<BSplineCurveForm>
    {
        private const string POLYLINE_FORM = "POLYLINE_FORM";
        private const string CIRCULAR_ARC = "CIRCULAR_ARC";
        private const string ELLIPTIC_ARC = "ELLIPTIC_ARC";
        private const string PARABOLIC_ARC = "PARABOLIC_ARC";
        private const string HYPERBOLIC_ARC = "HYPERBOLIC_ARC";
        private const string UNSPECIFIED = "UNSPECIFIED";

        public string Get( BSplineCurveForm value )
        {
            switch ( value )
            {
                case BSplineCurveForm.Polyline:
                    return POLYLINE_FORM;
                case BSplineCurveForm.CircularArc:
                    return CIRCULAR_ARC;
                case BSplineCurveForm.EllipticalArc:
                    return ELLIPTIC_ARC;
                case BSplineCurveForm.ParabolicArc:
                    return PARABOLIC_ARC;
                case BSplineCurveForm.HyperbolicArc:
                    return HYPERBOLIC_ARC;
                case BSplineCurveForm.Unspecified:
                    return UNSPECIFIED;
            }

            throw new NotImplementedException();
        }

        public BSplineCurveForm Parse( string enumerationValue )
        {
            switch ( enumerationValue.ToUpperInvariant() )
            {
                case POLYLINE_FORM:
                    return BSplineCurveForm.Polyline;
                case CIRCULAR_ARC:
                    return BSplineCurveForm.CircularArc;
                case ELLIPTIC_ARC:
                    return BSplineCurveForm.EllipticalArc;
                case PARABOLIC_ARC:
                    return BSplineCurveForm.ParabolicArc;
                case HYPERBOLIC_ARC:
                    return BSplineCurveForm.HyperbolicArc;
                default:
                    return BSplineCurveForm.Unspecified;
            }
        }
    }
}
