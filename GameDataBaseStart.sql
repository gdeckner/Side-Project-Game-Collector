
USE master;
GO

--
IF EXISTS(select * from sys.databases where name='GameDataBaseStorage')
DROP DATABASE World;
GO


CREATE DATABASE GameDataBaseStorage;
GO


USE GameDataBaseStorage
GO

---------------------------------------------------------------
create table Covers
(
cover_Id int primary key,
cover_Url varchar(200)
)
create table Franchises
(
franchise_Id int primary key,
franchise_Name varchar(50)
)
create table Genres
(
genre_Id int primary key,
genre_Name varchar(50) not null
)
create table Platforms
(
	platform_Id int primary key,
	platform_Name varchar(50) not null
)
create table GameInfo
(
	game_Id int primary key,
	game_Name varchar(50) not null,
	game_Description varchar(900) not null,
	genre_Id int foreign key references Genres(genre_Id),
	platform_Id int foreign key references Platforms(platform_Id),
	franchise_Id int foreign key references Franchises(franchise_Id),
	cover_Id int foreign key references Covers(cover_Id)
)
Create table UserLogin
(
	userId int primary key,
	userName varchar(100) Unique,
	userPassword varchar(100)

)
Create Table UserGameInfo
(
	id int identity (1,1) primary key,
	game_Id int foreign key references GameInfo(game_Id),
	game_isOwned bit default 0,
	game_onWish bit default 0,
	game_Progress decimal,
	userId int foreign key references UserLogin(userId)
)
create table GameRating
(
	game_Id int foreign key references GameInfo(game_Id),
	game_Total_Rating decimal,
	game_Total_Rating_Count decimal,
	game_Hype decimal,
	game_Popularity decimal

	
)



