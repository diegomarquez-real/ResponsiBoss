using AutoMapper;
using ResponsiBoss.Api.Converters.Type;

namespace ResponsiBoss.Api.Profiles
{
    public class SqlGeographyProfile : Profile
    {
        public SqlGeographyProfile()
        {
            CreateMap<Microsoft.SqlServer.Types.SqlGeography, GeoJSON.Net.Feature.Feature>()
                .ConvertUsing<SqlGeographyToFeatureTypeConverter>();
            CreateMap<GeoJSON.Net.Feature.Feature, Microsoft.SqlServer.Types.SqlGeography>()
                .ConvertUsing<FeatureToSqlGeographyTypeConverter>();
        }
    }
}