use master

go
create database OLAM

go
use OLAM

go
create table USERLOGIN (
	id int primary key identity(1,1),
	username nvarchar(200),
	password nvarchar(200),
	name nvarchar(200),
)

insert into USERLOGIN values ('user1','123','user1')
insert into USERLOGIN values ('user2','123','user2')
insert into USERLOGIN values ('user3','123','user3')
insert into USERLOGIN values ('user4','123','user4')

go
create table PEELING (
	id int primary key identity(1,1),
	ss_pressure nvarchar(200),
	value_pressure float,
	time_update datetime,
	ss_speeddrum nvarchar(200),
	Value_speeddrum float,
	timer_action int
)

insert into PEELING values ('ss_pressure 1', 100, '2020-12-31 13:59:59', 'ss_speeddrum 1', 0, 0)
insert into PEELING values ('ss_pressure 2', 200, '2020-12-30 13:59:59', 'ss_speeddrum 2', 0, 0)
insert into PEELING values ('ss_pressure 3', 300, '2020-12-29 13:59:59', 'ss_speeddrum 3', 0, 0)
insert into PEELING values ('ss_pressure 4', 400, '2020-12-28 13:59:59', 'ss_speeddrum 4', 0, 0)

go
create table CUTTING_DRYING (
	id int primary key identity(1,1),
	ss_temper float,
	time_update datetime,
	ss_humidity nvarchar(200),
	value_humidity float,
	value_temper float,
	timer1 int,
	timer2 int
)

insert into CUTTING_DRYING values (10, '2020-12-28 13:59:59', 'ss_humidity 1', 10, 10, 0, 0)
insert into CUTTING_DRYING values (20, '2020-12-29 13:59:59', 'ss_humidity 2', 20, 20, 0, 0)
insert into CUTTING_DRYING values (30, '2020-12-30 13:59:59', 'ss_humidity 3', 30, 30, 0, 0)
insert into CUTTING_DRYING values (40, '2020-12-31 13:59:59', 'ss_humidity 4', 40, 40, 0, 0)