use A_DB30_2018


insert into HEARSE(PRIORITY_) values
	(1),
	(2),
	(3);

insert into CALENDER_ENTRY(START_AT, END_AT, VEHICLE, AT_ADDRESS, COMMENT) values
	('2019-01-01 12:00', '2019-01-01 16:00', 1,		'Kagegården',		'Biver'),
	('2019-01-03 19:00', '2019-01-03 23:59', null,	'den lokale pop',	'SKÅL'),
	('2019-01-04 06:00', '2019-01-04 10:00', 1,		'Pandekagehuset',	'Børge'),
	('2019-01-10 12:00', '2019-01-11 12:00', 1,		'Den Høje Have',	'Peter'),
	('2019-01-04 08:00', '2019-01-04 12:00', 2,		'Rosenbusken',		'Peter'),
	('2019-01-04 09:00', '2019-01-04 13:00', 3,		'Kagegården',		'Biver'),
	('2019-01-06 14:00', '2019-01-06 15:00', null,	'Sebals gade',		'måde på datamatikerer')
	;