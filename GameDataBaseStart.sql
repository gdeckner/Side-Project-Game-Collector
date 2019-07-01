
USE master;
GO

--
IF EXISTS(select * from sys.databases where name='GameDataBaseStorage')
DROP DATABASE GameDataBaseStorage;
GO


CREATE DATABASE GameDataBaseStorage;
GO


USE GameDataBaseStorage
GO

---------------------------------------------------------------

Create table Franchises
(
franchise_id int primary key,
franchise_name varchar(100) not null
)
Create table Genres
(
genre_id int primary key,
genre_name varchar(100) not null
)
Create table Platforms
(
platform_id int primary key,
platform_name varchar(100) not null
)
Create table Covers
(
cover_id int primary key,
cover_url varchar(100) not null
)
Create table Ratings
(
rating_id int identity (1,1) primary key,
popularity int null,
hype int null,
rating int null,
rating_count int null
)
Create table Games
(
game_id int primary key,
game_description varchar(8000) not null,
game_name varchar(100) not null,
rating_id int foreign key references Ratings(rating_id),
platform_id_array varchar(100),
cover_id int foreign key references Covers(cover_id),
genre_id_array varchar(100),
franchise_id int foreign key references Franchises(franchise_id)
)

Create table UserInfo
(
userName varchar(200) primary key,
password varchar(200)COLLATE Latin1_General_CI_AS not null,
salt varchar (200) not null
)
create table UserGameInfo
(
EntryId int identity (1,1) primary key,
userName varchar(200) foreign key references UserInfo(userName),
game_id int  foreign key references Games(game_id),
progress int default 0 null,
owned bit default 0,
wishlist bit default 0

)
select *
from UserInfo



			