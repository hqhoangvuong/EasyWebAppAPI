SELECT table_name AS "TABLENAME",
       column_name AS "COLUMNNAME"
FROM information_schema.columns
WHERE table_schema NOT IN ('pg_catalog', 'information_schema')
AND (column_default LIKE '%AutoIncrement%' OR is_identity = 'YES')
AND dtd_identifier = '1'