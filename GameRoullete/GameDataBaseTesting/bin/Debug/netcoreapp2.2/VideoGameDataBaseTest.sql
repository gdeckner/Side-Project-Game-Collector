
DELETE FROM UserLogin;
DELETE FROM UserGameInfo;
DELETE FROM Platforms;
DELETE FROM Genres;
DELETE FROM Covers;
DELETE FROM Franchises;
DELETE FROM GameRating;
DELETE FROM GameInfo;



insert into Covers (cover_Id,cover_Url) values (55403,'https://images.igdb.com/igdb/image/upload/t_cover_big/bcotwv6rapvdglcahxi3.jpg'),(1234,'TESTTT')
insert into Franchises (franchise_Id,franchise_Name) values (137,'Halo'),(234,'tesst')
insert into Genres (genre_Id,genre_Name) values (5,'Shooter'),(28,'pew')
insert into Platforms (platform_Id,platform_Name) values (6,'Xbox'),(3,'360scope')


Insert into GameInfo (game_Id,game_Name,game_Description,genre_Id,platform_Id,franchise_Id,cover_Id)
values(740,'Halo: Combat Evolved','Bent on Humankinds extermination, a powerful fellowship of alien races known as the Covenant is wiping out Earths fledgling interstellar empire. Climb into the boots of Master Chief, a biologically altered super-soldier, as you and the other surviving defenders of a devastated colony-world make a desperate attempt to lure the alien fleet away from earth. Shot down and marooned on the ancient ring-world Halo, you begin a guerilla-war against the Covenant. Fight for humanity against an alien onslaught as you race to uncover the mysteries of Halo.',
5,6,137,55403),(9000,'Halo: Coder Simulator 9000','Spending all free time on this program',28,3,234,1234)

insert into GameRating(game_Id,game_Popularity,game_Total_Rating,game_Total_Rating_Count) values(740,10.3,85.7,328)
Insert into UserLogin (userId,userName,userPassword) values (123,'sparky1','password'),(41,'foxtrot','password')
Insert into UserGameInfo(game_Id,game_isOwned,game_onWish,game_Progress,userID) values (9000,0,0,0,123),(740,1,1,50,123)


			