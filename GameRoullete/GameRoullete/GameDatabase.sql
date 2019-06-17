Use Master;
go

drop database if exists GameDataBaseStorage;
go

create database GameDataBaseStorage
go

use GameDataBaseStorage;
go

create table Covers
(
cover_ID int primary key,
cover_Url varchar(300)
)
create table Franchises
(
franchise_Id int primary key,
franchise_Name varchar (200) not null
)
create table Genres
(
genre_Id int primary key,
genre_Name varchar(50) not null,

)
create table Platforms
(
platform_Id int primary key,
platform_Name varchar(50) not null,

)
create table GameInfo
(
game_ID int primary key,
game_Name varchar(100) not null,
game_Description varchar(1000),
game_Genre_Id int foreign key references Genres(genre_Id),
game_Platform_Id int foreign key references Platforms(platform_Id),
game_Franchises int foreign key references Franchises(franchise_Id),
game_Cover_Id int foreign key references Covers(cover_Id)


)
create table GameRating
(	
	game_Total_Rating int,
	game_Total_Rating_Count int,
	game_Popularity int,
	game_Hype int,
	game_Id int foreign key references GameInfo(game_Id),
	Constraint PK_GameRating primary key (game_Id)

)
