create table AspNetRoles
(
    Id               varchar(255) not null
        primary key,
    Name             varchar(256) null,
    NormalizedName   varchar(256) null,
    ConcurrencyStamp longtext     null,
    constraint RoleNameIndex
        unique (NormalizedName)
);

create table AspNetRoleClaims
(
    Id         int auto_increment
        primary key,
    RoleId     varchar(255) not null,
    ClaimType  longtext     null,
    ClaimValue longtext     null,
    constraint FK_AspNetRoleClaims_AspNetRoles_RoleId
        foreign key (RoleId) references AspNetRoles (Id)
            on delete cascade
);

create index IX_AspNetRoleClaims_RoleId
    on AspNetRoleClaims (RoleId);

create table AspNetUsers
(
    Id                   varchar(255) not null
        primary key,
    EmployeeCode         longtext     null,
    FirstName            longtext     null,
    MiddleName           longtext     null,
    LastName             longtext     null,
    IsHasAvatar          tinyint(1)   not null,
    UserName             varchar(256) null,
    NormalizedUserName   varchar(256) null,
    Email                varchar(256) null,
    NormalizedEmail      varchar(256) null,
    EmailConfirmed       tinyint(1)   not null,
    PasswordHash         longtext     null,
    SecurityStamp        longtext     null,
    ConcurrencyStamp     longtext     null,
    PhoneNumber          longtext     null,
    PhoneNumberConfirmed tinyint(1)   not null,
    TwoFactorEnabled     tinyint(1)   not null,
    LockoutEnd           datetime(6)  null,
    LockoutEnabled       tinyint(1)   not null,
    AccessFailedCount    int          not null,
    constraint UserNameIndex
        unique (NormalizedUserName)
);

create table AspNetUserClaims
(
    Id         int auto_increment
        primary key,
    UserId     varchar(255) not null,
    ClaimType  longtext     null,
    ClaimValue longtext     null,
    constraint FK_AspNetUserClaims_AspNetUsers_UserId
        foreign key (UserId) references AspNetUsers (Id)
            on delete cascade
);

create index IX_AspNetUserClaims_UserId
    on AspNetUserClaims (UserId);

create table AspNetUserLogins
(
    LoginProvider       varchar(255) not null,
    ProviderKey         varchar(255) not null,
    ProviderDisplayName longtext     null,
    UserId              varchar(255) not null,
    primary key (LoginProvider, ProviderKey),
    constraint FK_AspNetUserLogins_AspNetUsers_UserId
        foreign key (UserId) references AspNetUsers (Id)
            on delete cascade
);

create index IX_AspNetUserLogins_UserId
    on AspNetUserLogins (UserId);

create table AspNetUserRoles
(
    UserId varchar(255) not null,
    RoleId varchar(255) not null,
    primary key (UserId, RoleId),
    constraint FK_AspNetUserRoles_AspNetRoles_RoleId
        foreign key (RoleId) references AspNetRoles (Id)
            on delete cascade,
    constraint FK_AspNetUserRoles_AspNetUsers_UserId
        foreign key (UserId) references AspNetUsers (Id)
            on delete cascade
);

create index IX_AspNetUserRoles_RoleId
    on AspNetUserRoles (RoleId);

create table AspNetUserTokens
(
    UserId        varchar(255) not null,
    LoginProvider varchar(255) not null,
    Name          varchar(255) not null,
    Value         longtext     null,
    primary key (UserId, LoginProvider, Name),
    constraint FK_AspNetUserTokens_AspNetUsers_UserId
        foreign key (UserId) references AspNetUsers (Id)
            on delete cascade
);

create index EmailIndex
    on AspNetUsers (NormalizedEmail);

create table SystemMasterConfigs
(
    Id           int auto_increment
        primary key,
    ConfigName   varchar(200) not null,
    ConfigValue  varchar(200) null,
    IsActive     tinyint(1)   not null,
    CreatedDate  datetime(6)  not null,
    ModifiedDate datetime(6)  not null,
    IsDeleted    tinyint(1)   not null,
    constraint IX_SystemMasterConfigs_ConfigName
        unique (ConfigName)
);

create table SystemTableConfigs
(
    Id           int auto_increment
        primary key,
    Name         varchar(200) not null,
    ExplicitName varchar(200) not null,
    IsHidden     tinyint(1)   not null,
    ActionGroup  varchar(10)  not null,
    CreatedDate  datetime(6)  not null,
    ModifiedDate datetime(6)  not null,
    IsDeleted    tinyint(1)   not null,
    constraint IX_SystemTableConfigs_Name
        unique (Name)
);

create table SystemTableColumnConfigs
(
    Id                     int auto_increment
        primary key,
    TableId                int          not null,
    Name                   varchar(200) not null,
    ExplicitName           varchar(200) null,
    DataType               varchar(200) not null,
    ExplicitDataType       varchar(200) null,
    OrdinalPosition        int          not null,
    ColumnDefault          varchar(200) null,
    CharacterMaximumLength int          not null,
    CharacterOctetLength   int          not null,
    DisplayComponent       int          not null,
    IsNullable             varchar(200) null,
    IsPrimaryKey           tinyint(1)   not null,
    IsForeignKey           tinyint(1)   not null,
    IsHidden               tinyint(1)   not null,
    CreatedDate            datetime(6)  not null,
    ModifiedDate           datetime(6)  not null,
    IsDeleted              tinyint(1)   not null,
    constraint IX_SystemTableColumnConfigs_TableId_Name
        unique (TableId, Name),
    constraint FK_SystemTableColumnConfigs_SystemTableConfigs_TableId
        foreign key (TableId) references SystemTableConfigs (Id)
            on delete cascade
);

create table SystemTableForeingKeyConfigs
(
    Id                              int auto_increment
        primary key,
    FkName                          varchar(200) null,
    SourceTableName                 varchar(200) null,
    SourceColumnName                varchar(200) null,
    SourceColumnOrdinalPos          int          not null,
    RefrencedTableName              varchar(200) null,
    RefrencedColumnName             varchar(200) null,
    RefrencedColumnOrdinalPos       int          not null,
    MappedRefrencedColumnName       varchar(200) null,
    MappedRefrencedColumnOrdinalPos int          not null,
    CreatedDate                     datetime(6)  not null,
    ModifiedDate                    datetime(6)  not null,
    IsDeleted                       tinyint(1)   not null
);

create table __EFMigrationsHistory
(
    MigrationId    varchar(95) not null
        primary key,
    ProductVersion varchar(32) not null
);

