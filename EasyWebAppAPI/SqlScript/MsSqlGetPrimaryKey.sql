select schema_name(tab.schema_id) as [SchemaName], 
    pk.[name] as PKName,
    substring(column_names, 1, len(column_names) - 1) as [ColumnName],
    tab.[name] as [TableName]
from sys.tables tab
    inner join sys.indexes pk
        on tab.object_id = pk.object_id 
        and pk.is_primary_key = 1
   cross apply (select col.[name] + ','
                    from sys.index_columns ic
                        inner join sys.columns col
                            on ic.object_id = col.object_id
                            and ic.column_id = col.column_id
                    where ic.object_id = tab.object_id
                        and ic.index_id = pk.index_id
                            order by col.column_id
                            for xml path ('') ) D (column_names)
order by TableName
