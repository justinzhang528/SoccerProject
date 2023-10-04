CREATE TABLE MatchDetail
(
    Id varchar(80) NOT NULL PRIMARY KEY,
	FirstHalf_H int,
	FirstHalf_A int,
	SecondHalf_H int,
	SecondHalf_A int,
	RegularTime_H int,
	RegularTime_A int,
	Corners_H int,
	Corners_A int,
	Penalties_H int,
	Penalties_A int,
	YellowCards_H int,
	YellowCards_A int,
	RedCards_H int,
	RedCards_A int,
	FirstET_H int,
	FirstET_A int,
	SecondET_H int,
	SecondET_A int,
	PenaltiesShootout_H int,
	PenaltiesShootout_A int
);