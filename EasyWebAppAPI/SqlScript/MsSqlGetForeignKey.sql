SELECT obj.name AS [FkName], 
    tab1.name AS [SourceTableName],
    col1.name AS [SourceColumnName],
    col1.column_id AS [SourceColumnOrdinalPos],
    tab2.name AS [RefrencedTableName],
    col2.name AS [RefrencedColumnName],
    col2.column_id AS [RefrencedColumnOrdinalPos]
FROM sys.foreign_key_columns fkc
INNER JOIN sys.objects obj
    ON obj.object_id = fkc.constraint_object_id
INNER JOIN sys.tables tab1
    ON tab1.object_id = fkc.parent_object_id
INNER JOIN sys.schemas sch
    ON tab1.schema_id = sch.schema_id
INNER JOIN sys.columns col1
    ON col1.column_id = parent_column_id AND col1.object_id = tab1.object_id
INNER JOIN sys.tables tab2
    ON tab2.object_id = fkc.referenced_object_id
INNER JOIN sys.columns col2
    ON col2.column_id = referenced_column_id AND col2.object_id = tab2.object_id