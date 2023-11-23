using AutoMapper;
using GeoJSON.Net.Contrib.MsSqlSpatial;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Microsoft.SqlServer.Types;

namespace ResponsiBoss.Api.Converters.Type
{
    public class SqlGeographyToFeatureTypeConverter : ITypeConverter<SqlGeography, Feature>
    {
        public Feature Convert(SqlGeography sqlGeography, Feature feature, ResolutionContext context)
        {
            if (sqlGeography == null)
                return feature;

            if (Enum.TryParse(sqlGeography.STGeometryType().ToString(), out SqlGeometryTypeEnum enumVal))
            {
                var geoJSONObjectType = (GeoJSON.Net.GeoJSONObjectType)enumVal;

                switch (geoJSONObjectType)
                {
                    case GeoJSON.Net.GeoJSONObjectType.Point:
                        var point = sqlGeography.ToGeoJSONObject<Point>();
                        feature = new Feature(point);
                        break;
                    case GeoJSON.Net.GeoJSONObjectType.MultiPoint:
                        var multiPoint = sqlGeography.ToGeoJSONObject<MultiPoint>();
                        feature = new Feature(multiPoint);
                        break;
                    case GeoJSON.Net.GeoJSONObjectType.LineString:
                        var lineString = sqlGeography.ToGeoJSONObject<LineString>();
                        feature = new Feature(lineString);
                        break;
                    case GeoJSON.Net.GeoJSONObjectType.MultiLineString:
                        var multiLineString = sqlGeography.ToGeoJSONObject<MultiLineString>();
                        feature = new Feature(multiLineString);
                        break;
                    case GeoJSON.Net.GeoJSONObjectType.Polygon:
                        var polygon = sqlGeography.ToGeoJSONObject<Polygon>();
                        feature = new Feature(polygon);
                        break;
                    case GeoJSON.Net.GeoJSONObjectType.MultiPolygon:
                        var multiPolygon = sqlGeography.ToGeoJSONObject<MultiPolygon>();
                        feature = new Feature(multiPolygon);
                        break;
                    case GeoJSON.Net.GeoJSONObjectType.GeometryCollection:
                        var geometryCollection = sqlGeography.ToGeoJSONObject<GeometryCollection>();
                        feature = new Feature(geometryCollection);
                        break;
                    case GeoJSON.Net.GeoJSONObjectType.Feature:
                        feature = sqlGeography.ToGeoJSONObject<Feature>();
                        break;
                    case GeoJSON.Net.GeoJSONObjectType.FeatureCollection:
                        // Not In Use At The Moment.
                        break;
                }
            }

            return feature;
        }

        #region Sql GeometryType Enum

        public enum SqlGeometryTypeEnum
        {
            Point = GeoJSON.Net.GeoJSONObjectType.Point,
            LineString = GeoJSON.Net.GeoJSONObjectType.LineString,
            Polygon = GeoJSON.Net.GeoJSONObjectType.Polygon,
            MultiPoint = GeoJSON.Net.GeoJSONObjectType.MultiPoint,
            MultiPolygon = GeoJSON.Net.GeoJSONObjectType.MultiPolygon,
            MultiLineString = GeoJSON.Net.GeoJSONObjectType.MultiLineString,
            GeometryCollection = GeoJSON.Net.GeoJSONObjectType.GeometryCollection,
        }

        #endregion
    }
}