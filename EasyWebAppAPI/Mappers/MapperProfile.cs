using AutoMapper;
using EasyWebApp.Data.Entities.QueryResultEntities;
using EasyWebApp.Data.Entities.SystemEntities;
using EasyWebApp.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWebApp.API.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            IMappingExpression<DataRow, TableSchemaQueryResult> mappingExpression;

            mappingExpression = CreateMap<DataRow, TableSchemaQueryResult>();
            mappingExpression.ForMember(d => d.TableCatalog, o => o.MapFrom(s => s["TABLE_CATALOG"]));
            mappingExpression.ForMember(d => d.TableSchema, o => o.MapFrom(s => s["TABLE_SCHEMA"]));
            mappingExpression.ForMember(d => d.TableName, o => o.MapFrom(s => s["TABLE_NAME"]));
            mappingExpression.ForMember(d => d.TableType, o => o.MapFrom(s => s["TABLE_TYPE"]));

            IMappingExpression<DataRow, TableColumnSchemaQueryResult> mappingExpression1;
            mappingExpression1 = CreateMap<DataRow, TableColumnSchemaQueryResult>();
            mappingExpression1.ForMember(d => d.TableCatalog, o => o.MapFrom(s => s["TABLE_CATALOG"]));
            mappingExpression1.ForMember(d => d.TableSchema, o => o.MapFrom(s => s["TABLE_SCHEMA"]));
            mappingExpression1.ForMember(d => d.TableName, o => o.MapFrom(s => s["TABLE_NAME"]));
            mappingExpression1.ForMember(d => d.ColumnName, o => o.MapFrom(s => s["COLUMN_NAME"]));
            mappingExpression1.ForMember(d => d.OrdinalPosition, o => o.MapFrom(s => s["ORDINAL_POSITION"]));
            mappingExpression1.ForMember(d => d.ColumnDefault, o => o.MapFrom(s => s["COLUMN_DEFAULT"]));
            mappingExpression1.ForMember(d => d.IsNullable, o => o.MapFrom(s => s["IS_NULLABLE"]));
            mappingExpression1.ForMember(d => d.DataType, o => o.MapFrom(s => s["DATA_TYPE"]));
            mappingExpression1.ForMember(d => d.ColumnKey, o => o.MapFrom(s => s["COLUMN_KEY"]));
            // mappingExpression1.ForMember(d => d.CharacterMaximumLength, o => o.MapFrom(s => s["CHARACTER_MAXIMUM_LENGTH"]));


            IMappingExpression<DataRow, ForeignKeyQueryResult> mappingExpression2;
            mappingExpression2 = CreateMap<DataRow, ForeignKeyQueryResult>();
            mappingExpression2.ForMember(d => d.FkName, o => o.MapFrom(s => s["FkName"]));
            mappingExpression2.ForMember(d => d.SourceTableName, o => o.MapFrom(s => s["SourceTableName"]));
            mappingExpression2.ForMember(d => d.SourceColumnName, o => o.MapFrom(s => s["SourceColumnName"]));
            mappingExpression2.ForMember(d => d.SourceColumnOrdinalPos, o => o.MapFrom(s => s["SourceColumnOrdinalPos"]));
            mappingExpression2.ForMember(d => d.RefrencedTableName, o => o.MapFrom(s => s["RefrencedTableName"]));
            mappingExpression2.ForMember(d => d.RefrencedColumnName, o => o.MapFrom(s => s["RefrencedColumnName"]));
            mappingExpression2.ForMember(d => d.RefrencedColumnOrdinalPos, o => o.MapFrom(s => s["RefrencedColumnOrdinalPos"]));

            IMappingExpression<DataRow, PrimaryKeyQueryResult> mappingExpression3;
            mappingExpression3 = CreateMap<DataRow, PrimaryKeyQueryResult>();
            mappingExpression3.ForMember(d => d.SchemaName, o => o.MapFrom(s => s["SchemaName"]));
            mappingExpression3.ForMember(d => d.PKName, o => o.MapFrom(s => s["PKName"]));
            mappingExpression3.ForMember(d => d.ColumnName, o => o.MapFrom(s => s["ColumnName"]));
            mappingExpression3.ForMember(d => d.TableName, o => o.MapFrom(s => s["TableName"]));

        }
    }
}
