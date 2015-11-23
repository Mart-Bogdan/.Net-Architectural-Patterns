create database SampleDB
use SampleDB
create table BlogUser 
(
	Id int not null identity primary key,
	UserPassword nvarchar(40) not null,
	Name nvarchar(40) not null,  
	Nick nvarchar(40) not null
);


create table BlogPost 
(
	Id int not null identity primary key,
	Content text not null,
	Created datetimeoffset(7) not null,
	UserId int not null references BlogUser(Id)  
);
