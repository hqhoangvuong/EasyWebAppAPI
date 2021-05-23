SELECT
    tc.constraint_name AS "FkName", 
    sc.table_name AS "SourceTableName", 
    sc.column_name AS "SourceColumnName",
	sc.ordinal_position AS "SourceColumnOrdinalPos",
    rc.table_name AS "RefrencedTableName",
    rc.column_name AS "RefrencedColumnName",
	rc.ordinal_position AS "RefrencedColumnOrdinalPos"
FROM 
    information_schema.table_constraints AS tc 
    JOIN information_schema.key_column_usage AS kcu
		ON tc.constraint_name = kcu.constraint_name
		AND tc.table_schema = kcu.table_schema
	JOIN information_schema.columns sc
		ON sc.column_name = kcu.column_name
		AND sc.table_name =  tc.table_name
    JOIN information_schema.constraint_column_usage AS ccu
		ON ccu.constraint_name = tc.constraint_name
		AND ccu.table_schema = tc.table_schema
	JOIN information_schema.columns rc
		ON rc.column_name = ccu.column_name
		AND rc.table_name = ccu.table_name
WHERE tc.constraint_type = 'FOREIGN KEY'