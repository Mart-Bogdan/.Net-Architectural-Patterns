--
-- Скрипт сгенерирован Devart dbForge Studio for SQL Server, Версия 5.0.337.0
-- Домашняя страница продукта: http://www.devart.com/ru/dbforge/sql/studio
-- Дата скрипта: 09.12.2015 3:00:04
-- Версия сервера: 10.00.2531
-- Версия клиента: 
--


USE master
GO

IF DB_NAME() <> N'master' SET NOEXEC ON

--
-- Создать базу данных "SampleBlog"
--
PRINT (N'Создать базу данных "SampleBlog"')
GO
CREATE DATABASE SampleBlog

GO

--
-- Изменить базу данных
--
PRINT (N'Изменить базу данных')
GO
ALTER DATABASE SampleBlog
  SET
    ANSI_NULL_DEFAULT OFF,
    ANSI_NULLS OFF,
    ANSI_PADDING OFF,
    ANSI_WARNINGS OFF,
    ARITHABORT OFF,
    AUTO_CLOSE OFF,
    AUTO_CREATE_STATISTICS ON,
    AUTO_SHRINK OFF,
    AUTO_UPDATE_STATISTICS ON,
    AUTO_UPDATE_STATISTICS_ASYNC OFF,
    COMPATIBILITY_LEVEL = 100,
    CONCAT_NULL_YIELDS_NULL OFF,
    CURSOR_CLOSE_ON_COMMIT OFF,
    CURSOR_DEFAULT GLOBAL,
    DATE_CORRELATION_OPTIMIZATION OFF,
    DB_CHAINING OFF,
    HONOR_BROKER_PRIORITY OFF,
    MULTI_USER,
    NUMERIC_ROUNDABORT OFF,
    PAGE_VERIFY CHECKSUM,
    PARAMETERIZATION SIMPLE,
    QUOTED_IDENTIFIER OFF,
    READ_COMMITTED_SNAPSHOT ON,
    RECOVERY FULL,
    RECURSIVE_TRIGGERS OFF,
    TRUSTWORTHY OFF
    WITH ROLLBACK IMMEDIATE
GO

ALTER DATABASE SampleBlog
  SET DISABLE_BROKER
GO

ALTER DATABASE SampleBlog
  SET ALLOW_SNAPSHOT_ISOLATION ON
GO

USE SampleBlog
GO

IF DB_NAME() <> N'SampleBlog' SET NOEXEC ON
GO

--
-- Создать таблицу "dbo.BlogUser"
--
PRINT (N'Создать таблицу "dbo.BlogUser"')
GO
CREATE TABLE dbo.BlogUser (
  Id int IDENTITY,
  UserPassword nvarchar(40) NOT NULL,
  Name nvarchar(40) NOT NULL,
  Nick nvarchar(40) NOT NULL,
  PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

--
-- Создать индекс "UK_BlogUser_Nick" для объекта типа таблица "dbo.BlogUser"
--
PRINT (N'Создать индекс "UK_BlogUser_Nick" для объекта типа таблица "dbo.BlogUser"')
GO
CREATE UNIQUE INDEX UK_BlogUser_Nick
  ON dbo.BlogUser (Nick)
  ON [PRIMARY]
GO

--
-- Создать таблицу "dbo.BlogPost"
--
PRINT (N'Создать таблицу "dbo.BlogPost"')
GO
CREATE TABLE dbo.BlogPost (
  Id int IDENTITY,
  Title nvarchar(255) NOT NULL,
  Content ntext NULL,
  Created datetimeoffset NOT NULL,
  UserId int NOT NULL,
  PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
TEXTIMAGE_ON [PRIMARY]
GO
-- 
-- Вывод данных для таблицы BlogPost
--
SET IDENTITY_INSERT dbo.BlogPost ON
GO
INSERT dbo.BlogPost(Id, Title, Content, Created, UserId) VALUES (1, N'Post1', N'This is super cool post!


lalalala
1111', '2015-12-08 21:55:29.6207740 +02:00', 26)
INSERT dbo.BlogPost(Id, Title, Content, Created, UserId) VALUES (2, N'Second post', N'This is some text		
sdfgsdg
sdgsdg
', '2015-12-09 02:51:14.3508966 +02:00', 15)
INSERT dbo.BlogPost(Id, Title, Content, Created, UserId) VALUES (3, N'Third post', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis tristique interdum consectetur. Vestibulum erat diam, aliquet eu quam eu, sollicitudin convallis tellus. Etiam pharetra nulla et efficitur vestibulum. Pellentesque malesuada elit quis placerat sagittis. Donec non interdum mi. Sed leo arcu, fermentum vitae ipsum vitae, dictum convallis dui. Curabitur dapibus tristique lacus quis imperdiet. Aliquam erat volutpat. Donec consectetur lobortis nisi, quis eleifend massa auctor ut. Cras commodo tortor sit amet dolor vehicula pulvinar. Ut sollicitudin elit ac mauris tristique lobortis. Suspendisse potenti. Suspendisse efficitur eget sem a aliquam.

Duis ac diam hendrerit, egestas tortor quis, iaculis mauris. Etiam dignissim, elit ac ornare ultricies, dui leo consectetur dui, a posuere urna diam a ligula. Sed viverra, ex vel efficitur molestie, tortor sem aliquam quam, at vestibulum elit est a ipsum. Nunc ante ipsum, cursus quis quam eu, facilisis laoreet leo. Quisque pretium egestas est eget luctus. Donec porta elit tortor, placerat rutrum nulla rhoncus quis. Donec a mauris vitae lorem varius dictum in sed sem. Quisque et vehicula risus. Donec id neque in sem convallis commodo vel ut tellus. In lacus libero, condimentum vitae libero vel, maximus accumsan velit. Mauris porta egestas nisl eget gravida. Vestibulum lobortis justo sed ligula pulvinar imperdiet. Phasellus commodo metus sit amet urna tempus ultrices. Maecenas accumsan odio sit amet efficitur faucibus.

Donec dignissim faucibus tortor, a volutpat risus dapibus sit amet. Ut cursus purus eros, a vehicula justo sollicitudin vitae. Donec interdum id felis quis elementum. Integer accumsan commodo vehicula. Phasellus malesuada purus non arcu tincidunt tincidunt. Aenean eleifend sem ut congue auctor. Vivamus vitae turpis eget tortor sodales posuere eget non metus. Morbi imperdiet luctus justo. Quisque mi purus, pellentesque vel mollis in, rhoncus ultrices neque.

Ut scelerisque mi dui, eget pellentesque enim suscipit et. Praesent suscipit vestibulum euismod. Maecenas at vehicula purus. Fusce luctus, orci quis maximus ullamcorper, eros turpis cursus ipsum, sodales aliquet mi augue in odio. Suspendisse ac sapien enim. Sed non consequat dolor. Maecenas interdum eros nulla. Nunc pellentesque orci et fermentum volutpat. Ut fringilla bibendum massa sit amet lacinia.

Nam ut tellus faucibus, imperdiet tortor et, consectetur tellus. Phasellus maximus velit non euismod laoreet. Ut aliquam semper elit at iaculis. Vivamus viverra congue elit, non euismod lacus volutpat vitae. Nunc faucibus ligula sed ligula sodales malesuada. Nunc arcu ligula, sollicitudin in placerat nec, varius ac ligula. Integer congue, est eu ullamcorper maximus, diam enim pretium mi, non fermentum leo turpis eu nisi. Donec maximus quam nec odio auctor, sed consequat elit porta. Cras in mauris libero. Sed condimentum luctus erat, vel pretium ante posuere vel. Sed felis orci, commodo a justo sed, varius sodales sem. Etiam porta justo id ante aliquet commodo. Mauris eu erat vel sapien dignissim blandit eget nec diam. In ut sem et neque facilisis tristique in sit amet massa. Aliquam ac diam sit amet justo faucibus luctus. Fusce eleifend risus ex, et ultricies lacus vestibulum sit amet.', '2015-12-09 02:54:32.1986124 +02:00', 26)
GO
SET IDENTITY_INSERT dbo.BlogPost OFF
GO
-- 
-- Вывод данных для таблицы BlogUser
--
SET IDENTITY_INSERT dbo.BlogUser ON
GO
INSERT dbo.BlogUser(Id, UserPassword, Name, Nick) VALUES (15, N'qwerty', N'Bogdan', N'winnie')
INSERT dbo.BlogUser(Id, UserPassword, Name, Nick) VALUES (22, N'qq', N'Вася', N'w')
INSERT dbo.BlogUser(Id, UserPassword, Name, Nick) VALUES (26, N'qwerty', N'John Smith', N'user')
GO
SET IDENTITY_INSERT dbo.BlogUser OFF
GO

USE SampleBlog
GO

IF DB_NAME() <> N'SampleBlog' SET NOEXEC ON
GO

--
-- Создать внешний ключ для объекта типа таблица "dbo.BlogPost"
--
PRINT (N'Создать внешний ключ для объекта типа таблица "dbo.BlogPost"')
GO
ALTER TABLE dbo.BlogPost
  ADD FOREIGN KEY (UserId) REFERENCES dbo.BlogUser (Id)
GO
SET NOEXEC OFF
GO