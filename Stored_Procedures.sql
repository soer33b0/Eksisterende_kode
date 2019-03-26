USE A_DB30_2018

drop procedure if exists insert_event
drop procedure if exists insert_event2
drop procedure if exists update_event
drop procedure if exists delete_event
drop procedure if exists get_hearse
drop procedure if exists free_at
drop procedure if exists GET_ALL_HEARSE
drop procedure if exists GET_ALL_EVENTS



GO
create procedure insert_event 
	@START_AT	datetime2, 
	@END_AT		datetime2, 
	@VEHICLE	int = null, 
	@AT_ADDRESS nvarchar(50), 
	@COMMENT	nvarchar(250) = null
as
begin
	insert into CALENDER_ENTRY VALUES 
	(
		@START_AT,
		@END_AT,
		@VEHICLE,
		@AT_ADDRESS,
		@COMMENT
	)
end

GO
create procedure insert_event2 
	@START_AT	datetime2, 
	@END_AT		datetime2, 
	@AT_ADDRESS nvarchar(50), 
	@COMMENT	nvarchar(250) = null
as
begin
	insert into CALENDER_ENTRY VALUES 
	(
		@START_AT,
		@END_AT,
		null,
		@AT_ADDRESS,
		@COMMENT
	)
end

go
create procedure update_event 
	@KEY		int, 
	@START_AT	nvarchar(24) = null, 
	@END_AT		nvarchar(24) = null, 
	@VEHICLE	int = null, 
	@AT_ADDRESS nvarchar(50) = null, 
	@COMMENT	nvarchar(250) = null
as
begin
	UPDATE 
		CALENDER_ENTRY
	set
		START_AT =		case when @START_AT != null		then CAST(@START_AT as datetime2)	else START_AT end,
		END_AT =		case when @END_AT != null		then CAST(@END_AT as datetime2)		else END_AT end,
		VEHICLE =		case when @VEHICLE != null		then @VEHICLE						else VEHICLE end,
		AT_ADDRESS =	case when @AT_ADDRESS != null	then @AT_ADDRESS					else AT_ADDRESS end,
		COMMENT =		case when @COMMENT != null		then @COMMENT						else COMMENT end
	where 
		SURROGATE_KEY = @KEY
end

go
create procedure delete_event
	@KEY int
as
begin
	delete
	from 
		CALENDER_ENTRY
	where
		SURROGATE_KEY = @KEY
end

go
create procedure get_hearse
as
begin
	select
		PRIORITY_
	from
		HEARSE
	order by 
		PRIORITY_
end

go
create procedure free_at
	@PRIORITY_ int
as
begin
	select
		START_AT,
		END_AT
	from
		CALENDER_ENTRY
	where
		VEHICLE = @PRIORITY_
end

go
create procedure GET_ALL_HEARSE
as
begin
	select
		SURROGATE_KEY,
		PRIORITY_
	from
		Hearse
	order by [PRIORITY_] asc
end

go
create procedure GET_ALL_EVENTS
as
begin
	select
		SURROGATE_KEY,
		START_AT,
		END_AT,
		VEHICLE,
		AT_ADDRESS,
		COMMENT
	from
		calender_entry
end