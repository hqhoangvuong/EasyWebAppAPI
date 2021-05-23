CREATE TABLE public."AspNetRoles"
(
    "Id" text COLLATE pg_catalog."default" NOT NULL,
    "Name" character varying(256) COLLATE pg_catalog."default",
    "NormalizedName" character varying(256) COLLATE pg_catalog."default",
    "ConcurrencyStamp" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_AspNetRoles" PRIMARY KEY ("Id")
);

CREATE TABLE public."AspNetRoleClaims"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "RoleId" text COLLATE pg_catalog."default" NOT NULL,
    "ClaimType" text COLLATE pg_catalog."default",
    "ClaimValue" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_AspNetRoleClaims" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_AspNetRoleClaims_AspNetRoles_RoleId" FOREIGN KEY ("RoleId")
        REFERENCES public."AspNetRoles" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

CREATE INDEX "IX_AspNetRoleClaims_RoleId"
    ON public."AspNetRoleClaims" USING btree
    ("RoleId" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;

CREATE UNIQUE INDEX "RoleNameIndex"
    ON public."AspNetRoles" USING btree
    ("NormalizedName" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;

CREATE TABLE public."AspNetUsers"
(
    "Id" text COLLATE pg_catalog."default" NOT NULL,
    "EmployeeCode" text COLLATE pg_catalog."default",
    "FirstName" text COLLATE pg_catalog."default",
    "MiddleName" text COLLATE pg_catalog."default",
    "LastName" text COLLATE pg_catalog."default",
    "IsHasAvatar" boolean NOT NULL,
    "UserName" character varying(256) COLLATE pg_catalog."default",
    "NormalizedUserName" character varying(256) COLLATE pg_catalog."default",
    "Email" character varying(256) COLLATE pg_catalog."default",
    "NormalizedEmail" character varying(256) COLLATE pg_catalog."default",
    "EmailConfirmed" boolean NOT NULL,
    "PasswordHash" text COLLATE pg_catalog."default",
    "SecurityStamp" text COLLATE pg_catalog."default",
    "ConcurrencyStamp" text COLLATE pg_catalog."default",
    "PhoneNumber" text COLLATE pg_catalog."default",
    "PhoneNumberConfirmed" boolean NOT NULL,
    "TwoFactorEnabled" boolean NOT NULL,
    "LockoutEnd" timestamp with time zone,
    "LockoutEnabled" boolean NOT NULL,
    "AccessFailedCount" integer NOT NULL,
    CONSTRAINT "PK_AspNetUsers" PRIMARY KEY ("Id")
);

CREATE TABLE public."AspNetUserClaims"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "UserId" text COLLATE pg_catalog."default" NOT NULL,
    "ClaimType" text COLLATE pg_catalog."default",
    "ClaimValue" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_AspNetUserClaims" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_AspNetUserClaims_AspNetUsers_UserId" FOREIGN KEY ("UserId")
        REFERENCES public."AspNetUsers" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

CREATE INDEX "IX_AspNetUserClaims_UserId"
    ON public."AspNetUserClaims" USING btree
    ("UserId" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;



CREATE TABLE public."AspNetUserLogins"
(
    "LoginProvider" text COLLATE pg_catalog."default" NOT NULL,
    "ProviderKey" text COLLATE pg_catalog."default" NOT NULL,
    "ProviderDisplayName" text COLLATE pg_catalog."default",
    "UserId" text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "PK_AspNetUserLogins" PRIMARY KEY ("LoginProvider", "ProviderKey"),
    CONSTRAINT "FK_AspNetUserLogins_AspNetUsers_UserId" FOREIGN KEY ("UserId")
        REFERENCES public."AspNetUsers" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

CREATE INDEX "IX_AspNetUserLogins_UserId"
    ON public."AspNetUserLogins" USING btree
    ("UserId" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;
	
CREATE TABLE public."AspNetUserRoles"
(
    "UserId" text COLLATE pg_catalog."default" NOT NULL,
    "RoleId" text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "PK_AspNetUserRoles" PRIMARY KEY ("UserId", "RoleId"),
    CONSTRAINT "FK_AspNetUserRoles_AspNetRoles_RoleId" FOREIGN KEY ("RoleId")
        REFERENCES public."AspNetRoles" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE,
    CONSTRAINT "FK_AspNetUserRoles_AspNetUsers_UserId" FOREIGN KEY ("UserId")
        REFERENCES public."AspNetUsers" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

CREATE INDEX "IX_AspNetUserRoles_RoleId"
    ON public."AspNetUserRoles" USING btree
    ("RoleId" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;


CREATE TABLE public."AspNetUserTokens"
(
    "UserId" text COLLATE pg_catalog."default" NOT NULL,
    "LoginProvider" text COLLATE pg_catalog."default" NOT NULL,
    "Name" text COLLATE pg_catalog."default" NOT NULL,
    "Value" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_AspNetUserTokens" PRIMARY KEY ("UserId", "LoginProvider", "Name"),
    CONSTRAINT "FK_AspNetUserTokens_AspNetUsers_UserId" FOREIGN KEY ("UserId")
        REFERENCES public."AspNetUsers" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
);

CREATE INDEX "EmailIndex"
    ON public."AspNetUsers" USING btree
    ("NormalizedEmail" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;


CREATE UNIQUE INDEX "UserNameIndex"
    ON public."AspNetUsers" USING btree
    ("NormalizedUserName" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;

CREATE TABLE public."SystemMasterConfigs"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "ConfigName" character varying(200) COLLATE pg_catalog."default" NOT NULL,
    "ConfigValue" character varying(200) COLLATE pg_catalog."default",
    "IsActive" boolean NOT NULL,
    "CreatedDate" timestamp without time zone NOT NULL,
    "ModifiedDate" timestamp without time zone NOT NULL,
    "IsDeleted" boolean NOT NULL,
    CONSTRAINT "PK_SystemMasterConfigs" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

CREATE UNIQUE INDEX "IX_SystemMasterConfigs_ConfigName"
    ON public."SystemMasterConfigs" USING btree
    ("ConfigName" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;

CREATE TABLE public."SystemTableConfigs"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "Name" character varying(200) COLLATE pg_catalog."default" NOT NULL,
    "ExplicitName" character varying(200) COLLATE pg_catalog."default" NOT NULL,
    "ModelName" character varying(200) COLLATE pg_catalog."default",
    "IsHidden" boolean NOT NULL,
    "ActionGroup" character varying(10) COLLATE pg_catalog."default" NOT NULL,
    "CreatedDate" timestamp without time zone NOT NULL,
    "ModifiedDate" timestamp without time zone NOT NULL,
    "IsDeleted" boolean NOT NULL,
    CONSTRAINT "PK_SystemTableConfigs" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

CREATE UNIQUE INDEX "IX_SystemTableConfigs_Name"
    ON public."SystemTableConfigs" USING btree
    ("Name" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;
	
CREATE TABLE public."SystemTableColumnConfigs"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "TableId" integer NOT NULL,
    "Name" character varying(200) COLLATE pg_catalog."default" NOT NULL,
    "ExplicitName" character varying(200) COLLATE pg_catalog."default",
    "PropertyName" character varying(200) COLLATE pg_catalog."default",
    "DataType" character varying(200) COLLATE pg_catalog."default" NOT NULL,
    "ExplicitDataType" character varying(200) COLLATE pg_catalog."default",
    "OrdinalPosition" integer NOT NULL,
    "ColumnDefault" character varying(200) COLLATE pg_catalog."default",
    "CharacterMaximumLength" integer NOT NULL,
    "CharacterOctetLength" integer NOT NULL,
    "DisplayComponent" integer NOT NULL,
    "IsNullable" character varying(200) COLLATE pg_catalog."default",
    "IsPrimaryKey" boolean NOT NULL,
    "IsForeignKey" boolean NOT NULL,
    "IsHidden" boolean NOT NULL,
    "CreatedDate" timestamp without time zone NOT NULL,
    "ModifiedDate" timestamp without time zone NOT NULL,
    "IsDeleted" boolean NOT NULL,
    CONSTRAINT "PK_SystemTableColumnConfigs" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_SystemTableColumnConfigs_SystemTableConfigs_TableId" FOREIGN KEY ("TableId")
        REFERENCES public."SystemTableConfigs" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

CREATE UNIQUE INDEX "IX_SystemTableColumnConfigs_TableId_Name"
    ON public."SystemTableColumnConfigs" USING btree
    ("TableId" ASC NULLS LAST, "Name" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;

CREATE TABLE public."SystemTableForeingKeyConfigs"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "FkName" character varying(200) COLLATE pg_catalog."default",
    "SourceTableName" character varying(200) COLLATE pg_catalog."default",
    "SourceColumnName" character varying(200) COLLATE pg_catalog."default",
    "SourceColumnOrdinalPos" integer NOT NULL,
    "RefrencedTableName" character varying(200) COLLATE pg_catalog."default",
    "RefrencedColumnName" character varying(200) COLLATE pg_catalog."default",
    "RefrencedColumnOrdinalPos" integer NOT NULL,
    "MappedRefrencedColumnName" character varying(200) COLLATE pg_catalog."default",
    "MappedRefrencedColumnOrdinalPos" integer NOT NULL,
    "CreatedDate" timestamp without time zone NOT NULL,
    "ModifiedDate" timestamp without time zone NOT NULL,
    "IsDeleted" boolean NOT NULL,
    CONSTRAINT "PK_SystemTableForeingKeyConfigs" PRIMARY KEY ("Id")
);

CREATE TABLE public."__EFMigrationsHistory"
(
    "MigrationId" character varying(150) COLLATE pg_catalog."default" NOT NULL,
    "ProductVersion" character varying(32) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);