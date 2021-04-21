SELECT
       CONSTRAINT_NAME AS FkName,
       kcu.TABLE_NAME AS SourceTableName,
       kcu.COLUMN_NAME AS SourceColumnName,
       colsrc.ORDINAL_POSITION AS SourceColumnOrdinalPos,
       REFERENCED_TABLE_NAME AS RefrencedTableName,
       REFERENCED_COLUMN_NAME AS RefrencedColumnName,
       colref.ORDINAL_POSITION AS RefrencedColumnOrdinalPos
FROM
  INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu
      INNER JOIN information_schema.COLUMNS colref ON kcu.REFERENCED_TABLE_SCHEMA = colref.TABLE_SCHEMA
                                                        AND kcu.REFERENCED_TABLE_NAME = colref.TABLE_NAME
                                                        AND kcu.REFERENCED_COLUMN_NAME = colref.COLUMN_NAME
     INNER JOIN information_schema.COLUMNS colsrc ON kcu.TABLE_SCHEMA = colsrc.TABLE_SCHEMA
                                                        AND kcu.TABLE_NAME = colsrc.TABLE_NAME
                                                        AND kcu.COLUMN_NAME = colsrc.COLUMN_NAME
WHERE
  REFERENCED_TABLE_SCHEMA = '{0}'