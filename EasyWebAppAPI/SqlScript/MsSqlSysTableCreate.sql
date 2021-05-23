create table AspNetRoles
(
    Id               nvarchar(450) not null
        constraint PK_AspNetRoles
            primary key,
    Name             nvarchar(256),
    NormalizedName   nvarchar(256),
    ConcurrencyStamp nvarchar(max)
)

create table AspNetRoleClaims
(
    Id         int identity
        constraint PK_AspNetRoleClaims
            primary key,
    RoleId     nvarchar(450) not null
        constraint FK_AspNetRoleClaims_AspNetRoles_RoleId
            references AspNetRoles
            on delete cascade,
    ClaimType  nvarchar(max),
    ClaimValue nvarchar(max)
)

create index IX_AspNetRoleClaims_RoleId
    on AspNetRoleClaims (RoleId)

create unique index RoleNameIndex
    on AspNetRoles (NormalizedName)
    where [NormalizedName] IS NOT NULL

create table AspNetUsers
(
    Id                   nvarchar(450) not null
        constraint PK_AspNetUsers
            primary key,
    EmployeeCode         nvarchar(max),
    FirstName            nvarchar(max),
    MiddleName           nvarchar(max),
    LastName             nvarchar(max),
    IsHasAvatar          bit           not null,
    UserName             nvarchar(256),
    NormalizedUserName   nvarchar(256),
    Email                nvarchar(256),
    NormalizedEmail      nvarchar(256),
    EmailConfirmed       bit           not null,
    PasswordHash         nvarchar(max),
    SecurityStamp        nvarchar(max),
    ConcurrencyStamp     nvarchar(max),
    PhoneNumber          nvarchar(max),
    PhoneNumberConfirmed bit           not null,
    TwoFactorEnabled     bit           not null,
    LockoutEnd           datetimeoffset,
    LockoutEnabled       bit           not null,
    AccessFailedCount    int           not null
)

create table AspNetUserClaims
(
    Id         int identity
        constraint PK_AspNetUserClaims
            primary key,
    UserId     nvarchar(450) not null
        constraint FK_AspNetUserClaims_AspNetUsers_UserId
            references AspNetUsers
            on delete cascade,
    ClaimType  nvarchar(max),
    ClaimValue nvarchar(max)
)

create index IX_AspNetUserClaims_UserId
    on AspNetUserClaims (UserId)

create table AspNetUserLogins
(
    LoginProvider       nvarchar(450) not null,
    ProviderKey         nvarchar(450) not null,
    ProviderDisplayName nvarchar(max),
    UserId              nvarchar(450) not null
        constraint FK_AspNetUserLogins_AspNetUsers_UserId
            references AspNetUsers
            on delete cascade,
    constraint PK_AspNetUserLogins
        primary key (LoginProvider, ProviderKey)
)

create index IX_AspNetUserLogins_UserId
    on AspNetUserLogins (UserId)

create table AspNetUserRoles
(
    UserId nvarchar(450) not null
        constraint FK_AspNetUserRoles_AspNetUsers_UserId
            references AspNetUsers
            on delete cascade,
    RoleId nvarchar(450) not null
        constraint FK_AspNetUserRoles_AspNetRoles_RoleId
            references AspNetRoles
            on delete cascade,
    constraint PK_AspNetUserRoles
        primary key (UserId, RoleId)
)

create index IX_AspNetUserRoles_RoleId
    on AspNetUserRoles (RoleId)

create table AspNetUserTokens
(
    UserId        nvarchar(450) not null
        constraint FK_AspNetUserTokens_AspNetUsers_UserId
            references AspNetUsers
            on delete cascade,
    LoginProvider nvarchar(450) not null,
    Name          nvarchar(450) not null,
    Value         nvarchar(max),
    constraint PK_AspNetUserTokens
        primary key (UserId, LoginProvider, Name)
)

create index EmailIndex
    on AspNetUsers (NormalizedEmail)

create unique index UserNameIndex
    on AspNetUsers (NormalizedUserName)
    where [NormalizedUserName] IS NOT NULL

create table SystemMasterConfigs
(
    Id           int identity
        constraint PK_SystemMasterConfigs
            primary key,
    ConfigName   varchar(200) not null,
    ConfigValue  varchar(200),
    IsActive     bit          not null,
    CreatedDate  datetime2    not null,
    ModifiedDate datetime2    not null,
    IsDeleted    bit          not null
)

create unique index IX_SystemMasterConfigs_ConfigName
    on SystemMasterConfigs (ConfigName)

create table SystemTableConfigs
(
    Id           int identity
        constraint PK_SystemTableConfigs
            primary key,
    Name         varchar(200) not null,
    ExplicitName varchar(200) not null,
    ModelName    varchar(200),
    IsHidden     bit          not null,
    ActionGroup  varchar(10)  not null,
    CreatedDate  datetime2    not null,
    ModifiedDate datetime2    not null,
    IsDeleted    bit          not null
)

create table SystemTableColumnConfigs
(
    Id                     int identity
        constraint PK_SystemTableColumnConfigs
            primary key,
    TableId                int          not null
        constraint FK_SystemTableColumnConfigs_SystemTableConfigs_TableId
            references SystemTableConfigs
            on delete cascade,
    Name                   varchar(200) not null,
    ExplicitName           varchar(200),
    PropertyName           varchar(200),
    DataType               varchar(200) not null,
    ExplicitDataType       varchar(200),
    OrdinalPosition        int          not null,
    ColumnDefault          varchar(200),
    CharacterMaximumLength int          not null,
    CharacterOctetLength   int          not null,
    DisplayComponent       int          not null,
    IsNullable             varchar(200),
    IsPrimaryKey           bit          not null,
    IsForeignKey           bit          not null,
    IsHidden               bit          not null,
    CreatedDate            datetime2    not null,
    ModifiedDate           datetime2    not null,
    IsDeleted              bit          not null
)

create unique index IX_SystemTableColumnConfigs_TableId_Name
    on SystemTableColumnConfigs (TableId, Name)


create unique index IX_SystemTableConfigs_Name
    on SystemTableConfigs (Name)

create table SystemTableForeingKeyConfigs
(
    Id                              int identity
        constraint PK_SystemTableForeingKeyConfigs
            primary key,
    FkName                          varchar(200),
    SourceTableName                 varchar(200),
    SourceColumnName                varchar(200),
    SourceColumnOrdinalPos          int       not null,
    RefrencedTableName              varchar(200),
    RefrencedColumnName             varchar(200),
    RefrencedColumnOrdinalPos       int       not null,
    MappedRefrencedColumnName       varchar(200),
    MappedRefrencedColumnOrdinalPos int       not null,
    CreatedDate                     datetime2 not null,
    ModifiedDate                    datetime2 not null,
    IsDeleted                       bit       not null
)

create table __EFMigrationsHistory
(
    MigrationId    nvarchar(150) not null
        constraint PK___EFMigrationsHistory
            primary key,
    ProductVersion nvarchar(32)  not null
)
