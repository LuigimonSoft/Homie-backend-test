-- Create Database Homie 

CREATE DATABASE [Homie]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Homie', FILENAME = N'C:\SQL\DATA\Homie.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
 LOG ON 
( NAME = N'Homie_log', FILENAME = N'C:\SQL\DATA\Homie_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Homie] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Homie].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Homie] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Homie] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Homie] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Homie] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Homie] SET ARITHABORT OFF 
GO
ALTER DATABASE [Homie] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Homie] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Homie] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Homie] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Homie] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Homie] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Homie] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Homie] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Homie] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Homie] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Homie] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Homie] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Homie] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Homie] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Homie] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Homie] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Homie] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Homie] SET RECOVERY FULL 
GO
ALTER DATABASE [Homie] SET  MULTI_USER 
GO
ALTER DATABASE [Homie] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Homie] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Homie] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Homie] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Homie] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Homie] SET QUERY_STORE = OFF
GO
USE [Homie]
GO

ALTER DATABASE [Homie] SET  READ_WRITE 
GO



CREATE DATABASE [Homie]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Homie', FILENAME = N'C:\SQL\DATA\Homie.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
 LOG ON 
( NAME = N'Homie_log', FILENAME = N'C:\SQL\DATA\Homie_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Homie] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Homie].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Homie] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Homie] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Homie] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Homie] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Homie] SET ARITHABORT OFF 
GO
ALTER DATABASE [Homie] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Homie] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Homie] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Homie] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Homie] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Homie] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Homie] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Homie] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Homie] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Homie] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Homie] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Homie] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Homie] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Homie] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Homie] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Homie] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Homie] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Homie] SET RECOVERY FULL 
GO
ALTER DATABASE [Homie] SET  MULTI_USER 
GO
ALTER DATABASE [Homie] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Homie] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Homie] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Homie] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Homie] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Homie] SET QUERY_STORE = OFF
GO
USE [Homie]
GO

ALTER DATABASE [Homie] SET  READ_WRITE 
GO


-- Create tables 


CREATE TABLE Owners
( 
	OwnerId              varchar(20)  NOT NULL ,
	[Name]                 varchar(100)  NOT NULL ,
	AvailabilityFrom     datetime  NOT NULL ,
	AvailabilityTo       datetime  NOT NULL ,
	Email                varchar(100)  NULL ,
	Phone                varchar(20)  NULL ,
	CONSTRAINT XPKOwners PRIMARY KEY  CLUSTERED (OwnerId ASC)
)
go



CREATE TABLE Status
( 
	StatusId             integer  NOT NULL ,
	StatusText           varchar(20)  NOT NULL ,
	CONSTRAINT XPKStatus PRIMARY KEY  CLUSTERED (StatusId ASC)
)
go



CREATE TABLE Tenants
( 
	TenantId             uniqueidentifier  NOT NULL 
	CONSTRAINT GUID_1811012331
		 DEFAULT  NEWID(),
	[Name]                 varchar(100)  NOT NULL ,
	Email                varchar(50)  NOT NULL ,
	Phone                varchar(20)  NULL ,
	AvailabilityFrom     datetime  NOT NULL ,
	AvailabilityTo       datetime  NOT NULL ,
	CONSTRAINT XPKTenants PRIMARY KEY  CLUSTERED (TenantId ASC)
)
go



CREATE TABLE Propertys
( 
	PropertyId           uniqueidentifier  NOT NULL 
	CONSTRAINT GUID_359148913
		 DEFAULT  NEWID(),
	[Name]                 varchar(100)  NOT NULL ,
	[Description]          varchar(500)  NOT NULL ,
	StatusId             integer  NOT NULL ,
	TenantId             uniqueidentifier  NULL ,
	CreatedBy            uniqueidentifier  NOT NULL ,
	CreatedOn            datetime  NOT NULL 
	CONSTRAINT CURRENT_TIMESTAMP_183546053
		 DEFAULT  CURRENT_TIMESTAMP,
	ModifiedBy           uniqueidentifier  NULL ,
	ModifiedOn           datetime  NULL ,
	CONSTRAINT XPKPropertys PRIMARY KEY  CLUSTERED (PropertyId ASC),
	CONSTRAINT R_2 FOREIGN KEY (StatusId) REFERENCES Status(StatusId),
CONSTRAINT R_3 FOREIGN KEY (TenantId) REFERENCES Tenants(TenantId)
)
go



CREATE NONCLUSTERED INDEX XIF2Propertys ON Propertys
( 
	StatusId              ASC
)
go



CREATE NONCLUSTERED INDEX XIF3Propertys ON Propertys
( 
	TenantId              ASC
)
go



CREATE TABLE OwnersPropertys
( 
	OwnerPropertyId      integer IDENTITY ( 1,1 ) ,
	OwnerId              varchar(20)  NULL ,
	PropertyId           uniqueidentifier  NULL ,
	Active               bit  NOT NULL 
	CONSTRAINT True_1653377069
		 DEFAULT  1,
	CreatedOn            datetime  NOT NULL 
	CONSTRAINT CURRENT_TIMESTAMP_545409795
		 DEFAULT  CURRENT_TIMESTAMP,
	CONSTRAINT XPKOwnersPropertys PRIMARY KEY  CLUSTERED (OwnerPropertyId ASC),
	CONSTRAINT R_4 FOREIGN KEY (OwnerId) REFERENCES Owners(OwnerId),
CONSTRAINT R_5 FOREIGN KEY (PropertyId) REFERENCES Propertys(PropertyId)
)
go



CREATE NONCLUSTERED INDEX XIF1OwnersPropertys ON OwnersPropertys
( 
	OwnerId               ASC
)
go



CREATE NONCLUSTERED INDEX XIF2OwnersPropertys ON OwnersPropertys
( 
	PropertyId            ASC
)
go



CREATE TABLE PartnerTypes
( 
	PartnerTypeId         integer IDENTITY ( 1,1 ) ,
	PartnerType           varchar(50)  NOT NULL ,
	Active               bit  NOT NULL DEFAULT 1 ,
	CONSTRAINT XPKPartherTypes PRIMARY KEY  CLUSTERED (PartnerTypeId ASC)
)
go



CREATE TABLE Partners
( 
	PartnerId            uniqueidentifier  NOT NULL DEFAULT NEWID() ,
	[Partner]              varchar(100)  NOT NULL ,
	[User]                 varchar(100)  NOT NULL ,
	[Password]             varchar(100)  NOT NULL ,
	PartnerTypeId         integer  NULL ,
	Active               bit  NOT NULL 
	CONSTRAINT True_1379669004
		 DEFAULT  1,
	CONSTRAINT XPKPartners PRIMARY KEY  CLUSTERED (PartnerId ASC),
	CONSTRAINT R_6 FOREIGN KEY (PartnerTypeId) REFERENCES PartnerTypes(PartnerTypeId)
)
go



CREATE NONCLUSTERED INDEX XIF1Partners ON Partners
( 
	PartnerTypeId          ASC
)
go



CREATE TABLE RentalPrices
( 
	RentalPriceId        integer IDENTITY ( 1,1 ) ,
	PropertyId           uniqueidentifier  NOT NULL ,
	RentalPrice          numeric(18,2)  NOT NULL ,
	Active               bit  NOT NULL 
	CONSTRAINT True_315209118
		 DEFAULT  1,
	CreatedOn            datetime  NOT NULL 
	CONSTRAINT CURRENT_TIMESTAMP_792758157
		 DEFAULT  CURRENT_TIMESTAMP,
	CONSTRAINT XPKRentalPrices PRIMARY KEY  CLUSTERED (RentalPriceId ASC),
	CONSTRAINT R_7 FOREIGN KEY (PropertyId) REFERENCES Propertys(PropertyId)
)
go



CREATE NONCLUSTERED INDEX XIF1RentalPrices ON RentalPrices
( 
	PropertyId            ASC
)
go



-- Create initial catalogs
INSERT INTO [dbo].[Status]
           ([StatusId]
           ,[StatusText])
     VALUES
           (1
           ,'published')
GO


INSERT INTO [dbo].[Status]
           ([StatusId]
           ,[StatusText])
     VALUES
           (2
           ,'available')
GO

INSERT INTO [dbo].[Status]
           ([StatusId]
           ,[StatusText])
     VALUES
           (3
           ,'deleted')
GO

-- Create Procedures

-- testing Partners

INSERT INTO [dbo].[PartnerTypes]
           ([PartnerType]
           )
     VALUES
			(
				'Partner basic'
			)
INSERT INTO [dbo].[PartnerTypes]
           ([PartnerType]
           )
     VALUES
			(
				'Partner '
			)
INSERT INTO [dbo].[PartnerTypes]
           ([PartnerType]
           )
     VALUES
			(
				'Partner full'
			)


INSERT INTO [dbo].[Partners]
           ([Partner]
           ,[User]
           ,[Password]
		   ,PartnerTypeId
           )
     VALUES
           (
		   			'inmuebles24'
           ,'inmuebles24'
           ,'1rodbtbo7IYO3bURrVJsYGWEjxoZskeZ' -- inmuebles24-12345
		   ,3
           )
GO

INSERT INTO [dbo].[Partners]
           ([Partner]
           ,[User]
           ,[Password]
		   ,PartnerTypeId
           )
     VALUES
           (
		   			'Metros cubicos'
           ,'metroscubicos'
           ,'nHLWcufU5a8b3avpRKXrJZ4VhO9perdo' -- metroscubicos-12345
		   		,2
           )
GO

INSERT INTO [dbo].[Partners]
           ([Partner]
           ,[User]
           ,[Password]
		   ,PartnerTypeId
           )
     VALUES
           (
		   			'Segunda mano'
           ,'segundamano'
           ,'BShX6VXNTkRN62kgdO8aSKbFWDb0DHKk' -- segundamano-12345
		   		,1
           )
GO




