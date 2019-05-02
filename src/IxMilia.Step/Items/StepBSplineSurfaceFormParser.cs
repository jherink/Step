// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;

namespace IxMilia.Step.Items
{
    public enum BSplineSurfaceForm
    {
        PlaneSurf,
        CylindricalSurf,
        ConicalSurf,
        SphericalSurf,
        ToroidalSurf,
        SurfOfRevolution,
        RuledSurf,
        GeneralisedCone,
        QuadricSurf,
        SurfOfLinearExtrusion,
        Unspecified
    };

    internal sealed class StepBSplineSurfaceFormParser : IStepEnumerationValueParser<BSplineSurfaceForm>
    {
        private const string PLANE_SURF = "PLANE_SURF";
        private const string CYLINDRICAL_SURF = "CYLINDRICAL_SURF";
        private const string CONICAL_SURF = "CONICAL_SURF";
        private const string SPHERICAL_SURF = "SPHERICAL_SURF";
        private const string TOROIDAL_SURF = "TOROIDAL_SURF";
        private const string SURF_OF_REVOLUTION = "SURF_OF_REVOLUTION";
        private const string RULED_SURF = "RULED_SURF";
        private const string GENERALISED_CONE = "GENERALISED_CONE";
        private const string QUADRIC_SURF = "QUADRIC_SURF";
        private const string SURF_OF_LINEAR_EXTRUSION = "SURF_OF_LINEAR_EXTRUSION";
        private const string UNSPECIFIED = "UNSPECIFIED";

        public string Get( BSplineSurfaceForm value )
        {
            switch ( value )
            {
                case BSplineSurfaceForm.PlaneSurf:
                    return PLANE_SURF;
                case BSplineSurfaceForm.CylindricalSurf:
                    return CYLINDRICAL_SURF;
                case BSplineSurfaceForm.ConicalSurf:
                    return CONICAL_SURF;
                case BSplineSurfaceForm.SphericalSurf:
                    return SPHERICAL_SURF;
                case BSplineSurfaceForm.ToroidalSurf:
                    return TOROIDAL_SURF;
                case BSplineSurfaceForm.SurfOfRevolution:
                    return SURF_OF_REVOLUTION;
                case BSplineSurfaceForm.RuledSurf:
                    return RULED_SURF;
                case BSplineSurfaceForm.GeneralisedCone:
                    return GENERALISED_CONE;
                case BSplineSurfaceForm.QuadricSurf:
                    return QUADRIC_SURF;
                case BSplineSurfaceForm.SurfOfLinearExtrusion:
                    return SURF_OF_LINEAR_EXTRUSION;
                case BSplineSurfaceForm.Unspecified:
                    return UNSPECIFIED;
            }

            throw new NotImplementedException();
        }

        public BSplineSurfaceForm Parse( string enumerationValue )
        {
            switch ( enumerationValue.ToUpperInvariant() )
            {
                case PLANE_SURF:
                    return BSplineSurfaceForm.PlaneSurf;
                case CYLINDRICAL_SURF:
                    return BSplineSurfaceForm.CylindricalSurf;
                case CONICAL_SURF:
                    return BSplineSurfaceForm.ConicalSurf;
                case SPHERICAL_SURF:
                    return BSplineSurfaceForm.SphericalSurf;
                case TOROIDAL_SURF:
                    return BSplineSurfaceForm.ToroidalSurf;
                case SURF_OF_REVOLUTION:
                    return BSplineSurfaceForm.SurfOfRevolution;
                case RULED_SURF:
                    return BSplineSurfaceForm.RuledSurf;
                case GENERALISED_CONE:
                    return BSplineSurfaceForm.GeneralisedCone;
                case QUADRIC_SURF:
                    return BSplineSurfaceForm.QuadricSurf;
                case SURF_OF_LINEAR_EXTRUSION:
                    return BSplineSurfaceForm.SurfOfLinearExtrusion;
                default:
                    return BSplineSurfaceForm.Unspecified;
            }
        }
    }
}
