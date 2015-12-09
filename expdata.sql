--
-- Скрипт сгенерирован Devart dbForge Studio for SQL Server, Версия 5.0.337.0
-- Домашняя страница продукта: http://www.devart.com/ru/dbforge/sql/studio
-- Дата скрипта: 09.12.2015 2:58:12
-- Версия сервера: 10.00.2531
-- Версия клиента: 
--

SET DATEFORMAT ymd
SET ARITHABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
SET NUMERIC_ROUNDABORT, IMPLICIT_TRANSACTIONS, XACT_ABORT OFF
GO

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

SET IDENTITY_INSERT dbo.BlogUser ON
GO
INSERT dbo.BlogUser(Id, UserPassword, Name, Nick) VALUES (15, N'qwerty', N'Bogdan', N'winnie')
INSERT dbo.BlogUser(Id, UserPassword, Name, Nick) VALUES (22, N'qq', N'Вася', N'w')
INSERT dbo.BlogUser(Id, UserPassword, Name, Nick) VALUES (26, N'qwerty', N'John Smith', N'user')
GO
SET IDENTITY_INSERT dbo.BlogUser OFF
GO