select kcu.table_schema as "SchemaName",
       tco.constraint_name as "PKName",
	   kcu.column_name as "ColumnName",
       kcu.table_name as "TableName"
from information_schema.table_constraints tco
join information_schema.key_column_usage kcu 
     on kcu.constraint_name = tco.constraint_name
     and kcu.constraint_schema = tco.constraint_schema
     and kcu.constraint_name = tco.constraint_name
where tco.constraint_type = 'PRIMARY KEY'
order by kcu.table_schema,
         kcu.table_name