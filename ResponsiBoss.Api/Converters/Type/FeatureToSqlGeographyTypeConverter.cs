using AutoMapper;
using GeoJSON.Net.Contrib.MsSqlSpatial;
using GeoJSON.Net.Feature;
using Microsoft.SqlServer.Types;

namespace ResponsiBoss.Api.Converters.Type
{
    public class FeatureToSqlGeographyTypeConverter : ITypeConverter<Feature, SqlGeography>
    {
        public FeatureToSqlGeographyTypeConverter()
        {
        }

        public SqlGeography Convert(Feature feature, SqlGeography sqlGeography, ResolutionContext context)
        {
            if (feature == null)
                return sqlGeography;

            sqlGeography = feature.ToSqlGeography().MakeValidIfInvalid();

            return sqlGeography.EnvelopeAngle() > 90 ? sqlGeography.ReorientObject().MakeValidIfInvalid()
                                                     : sqlGeography;
        }
    }
}