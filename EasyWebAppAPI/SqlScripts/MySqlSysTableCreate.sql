CREATE TABLE `AspNetRoleClaims`
(
    `Id`         int          NOT NULL AUTO_INCREMENT,
    `RoleId`     varchar(255) NOT NULL,
    `ClaimType`  longtext,
    `ClaimValue` longtext,
    PRIMARY KEY (`Id`),
    KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
    CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_0900_ai_ci;

CREATE TABLE `AspNetRoles`
(
    `Id`               varchar(255) NOT NULL,
    `Name`             varchar(256) DEFAULT NULL,
    `NormalizedName`   varchar(256) DEFAULT NULL,
    `ConcurrencyStamp` longtext,
    PRIMARY KEY (`Id`),
    UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_0900_ai_ci;

CREATE TABLE `AspNetUserClaims`
(
    `Id`         int          NOT NULL AUTO_INCREMENT,
    `UserId`     varchar(255) NOT NULL,
    `ClaimType`  longtext,
    `ClaimValue` longtext,
    PRIMARY KEY (`Id`),
    KEY `IX_AspNetUserClaims_UserId` (`UserId`),
    CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_0900_ai_ci;

CREATE TABLE `AspNetUserLogins`
(
    `LoginProvider`       varchar(255) NOT NULL,
    `ProviderKey`         varchar(255) NOT NULL,
    `ProviderDisplayName` longtext,
    `UserId`              varchar(255) NOT NULL,
    PRIMARY KEY (`LoginProvider`, `ProviderKey`),
    KEY `IX_AspNetUserLogins_UserId` (`UserId`),
    CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_0900_ai_ci;

CREATE TABLE `AspNetUserRoles`
(
    `UserId` varchar(255) NOT NULL,
    `RoleId` varchar(255) NOT NULL,
    PRIMARY KEY (`UserId`, `RoleId`),
    KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
    CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_0900_ai_ci;

CREATE TABLE `AspNetUserTokens`
(
    `UserId`        varchar(255) NOT NULL,
    `LoginProvider` varchar(255) NOT NULL,
    `Name`          varchar(255) NOT NULL,
    `Value`         longtext,
    PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
    CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_0900_ai_ci;

CREATE TABLE `AspNetUsers`
(
    `Id`                   varchar(255) NOT NULL,
    `EmployeeCode`         longtext,
    `FirstName`            longtext,
    `MiddleName`           longtext,
    `LastName`             longtext,
    `IsHasAvatar`          tinyint(1)   NOT NULL,
    `UserName`             varchar(256) DEFAULT NULL,
    `NormalizedUserName`   varchar(256) DEFAULT NULL,
    `Email`                varchar(256) DEFAULT NULL,
    `NormalizedEmail`      varchar(256) DEFAULT NULL,
    `EmailConfirmed`       tinyint(1)   NOT NULL,
    `PasswordHash`         longtext,
    `SecurityStamp`        longtext,
    `ConcurrencyStamp`     longtext,
    `PhoneNumber`          longtext,
    `PhoneNumberConfirmed` tinyint(1)   NOT NULL,
    `TwoFactorEnabled`     tinyint(1)   NOT NULL,
    `LockoutEnd`           datetime(6)  DEFAULT NULL,
    `LockoutEnabled`       tinyint(1)   NOT NULL,
    `AccessFailedCount`    int          NOT NULL,
    PRIMARY KEY (`Id`),
    UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
    KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_0900_ai_ci;

CREATE TABLE `SystemMasterConfigs`
(
    `Id`           int          NOT NULL AUTO_INCREMENT,
    `ConfigName`   varchar(200) NOT NULL,
    `ConfigValue`  varchar(200) DEFAULT NULL,
    `IsActive`     tinyint(1)   NOT NULL,
    `CreatedDate`  datetime(6)  NOT NULL,
    `ModifiedDate` datetime(6)  NOT NULL,
    `IsDeleted`    tinyint(1)   NOT NULL,
    PRIMARY KEY (`Id`),
    UNIQUE KEY `IX_SystemMasterConfigs_ConfigName` (`ConfigName`)
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_0900_ai_ci;

CREATE TABLE `SystemTableColumnConfigs`
(
    `Id`                     int          NOT NULL AUTO_INCREMENT,
    `TableId`                int          NOT NULL,
    `Name`                   varchar(200) NOT NULL,
    `ExplicitName`           varchar(200) DEFAULT NULL,
    `DataType`               varchar(200) NOT NULL,
    `ExplicitDataType`       varchar(200) DEFAULT NULL,
    `OrdinalPosition`        int          NOT NULL,
    `ColumnDefault`          varchar(200) DEFAULT NULL,
    `CharacterMaximumLength` int          NOT NULL,
    `CharacterOctetLength`   int          NOT NULL,
    `DisplayComponent`       int          NOT NULL,
    `IsNullable`             varchar(200) DEFAULT NULL,
    `IsPrimaryKey`           tinyint(1)   NOT NULL,
    `IsForeignKey`           tinyint(1)   NOT NULL,
    `IsHidden`               tinyint(1)   NOT NULL,
    `CreatedDate`            datetime(6)  NOT NULL,
    `ModifiedDate`           datetime(6)  NOT NULL,
    `IsDeleted`              tinyint(1)   NOT NULL,
    PRIMARY KEY (`Id`),
    UNIQUE KEY `IX_SystemTableColumnConfigs_TableId_Name` (`TableId`, `Name`),
    CONSTRAINT `FK_SystemTableColumnConfigs_SystemTableConfigs_TableId` FOREIGN KEY (`TableId`) REFERENCES `SystemTableConfigs` (`Id`) ON DELETE CASCADE
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_0900_ai_ci;

CREATE TABLE `SystemTableConfigs`
(
    `Id`           int          NOT NULL AUTO_INCREMENT,
    `Name`         varchar(200) NOT NULL,
    `ExplicitName` varchar(200) NOT NULL,
    `IsHidden`     tinyint(1)   NOT NULL,
    `ActionGroup`  varchar(10)  NOT NULL,
    `CreatedDate`  datetime(6)  NOT NULL,
    `ModifiedDate` datetime(6)  NOT NULL,
    `IsDeleted`    tinyint(1)   NOT NULL,
    PRIMARY KEY (`Id`),
    UNIQUE KEY `IX_SystemTableConfigs_Name` (`Name`)
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_0900_ai_ci;

CREATE TABLE `SystemTableForeingKeyConfigs`
(
    `Id`                              int         NOT NULL AUTO_INCREMENT,
    `FkName`                          varchar(200) DEFAULT NULL,
    `SourceTableName`                 varchar(200) DEFAULT NULL,
    `SourceColumnName`                varchar(200) DEFAULT NULL,
    `SourceColumnOrdinalPos`          int         NOT NULL,
    `RefrencedTableName`              varchar(200) DEFAULT NULL,
    `RefrencedColumnName`             varchar(200) DEFAULT NULL,
    `RefrencedColumnOrdinalPos`       int         NOT NULL,
    `MappedRefrencedColumnName`       varchar(200) DEFAULT NULL,
    `MappedRefrencedColumnOrdinalPos` int         NOT NULL,
    `CreatedDate`                     datetime(6) NOT NULL,
    `ModifiedDate`                    datetime(6) NOT NULL,
    `IsDeleted`                       tinyint(1)  NOT NULL,
    PRIMARY KEY (`Id`)
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_0900_ai_ci;

CREATE TABLE `__EFMigrationsHistory`
(
    `MigrationId`    varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4
  COLLATE = utf8mb4_0900_ai_ci;

