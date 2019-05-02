// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;

namespace IxMilia.Step.Items
{
    public enum KnotType
    {
        UniformKnots,
        QuasiUniformKnots,
        PiecewiseBezierKnots,
        Unspecified
    };

    internal sealed class StepKnotTypeValueParser : IStepEnumerationValueParser<KnotType>
    {
        private const string UNIFORM_KNOTS = "UNIFORM_KNOTS";
        private const string QUASI_UNIFORM_KNOTS = "QUASI_UNIFORM_KNOTS";
        private const string PIECEWISE_BEZIER_KNOTS = "PIECEWISE_BEZIER_KNOTS";
        private const string UNSPECIFIED = "UNSPECIFIED";

        public string Get( KnotType value )
        {
            switch ( value )
            {
                case KnotType.UniformKnots:
                    return UNIFORM_KNOTS;
                case KnotType.QuasiUniformKnots:
                    return QUASI_UNIFORM_KNOTS;
                case KnotType.PiecewiseBezierKnots:
                    return PIECEWISE_BEZIER_KNOTS;
                case KnotType.Unspecified:
                    return UNSPECIFIED;
            }

            throw new NotImplementedException();
        }

        public KnotType Parse( string enumerationValue )
        {
            switch ( enumerationValue.ToUpperInvariant() )
            {
                case UNIFORM_KNOTS:
                    return KnotType.UniformKnots;
                case QUASI_UNIFORM_KNOTS:
                    return KnotType.QuasiUniformKnots;
                case PIECEWISE_BEZIER_KNOTS:
                    return KnotType.PiecewiseBezierKnots;
                default:
                    return KnotType.Unspecified;
            }
        }
    }
}
