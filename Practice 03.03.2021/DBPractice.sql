create database DBPractice;

use DBPractice;

create table Users(
	id int identity(1,1),
	[login] nvarchar(50) primary key not null,
	[password] nvarchar(50) not null
);

create table Profiles(
	[login] nvarchar(50), 
	[Email] nvarchar(50) not null,
	[FullName] nvarchar(50) not null,
	[url] nvarchar(max),
	foreign key ([login]) references Users([login])
);
