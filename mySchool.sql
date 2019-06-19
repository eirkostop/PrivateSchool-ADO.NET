--Delete database
use master;
go
drop database if exists School;
go
--Create new database
Create database School;
go
use School;
go
--Create tables
CREATE TABLE [Student]
(
[StudentId]     int IDENTITY (1, 1) primary key ,
[FirstName]     varchar(50) ,
[LastName]       varchar(50) ,
[DateOfBirth]    date ,
[TuitionFees]	money 
);
create table [Trainer]
(
[TrainerId]     int IDENTITY (1, 1) primary key ,
[FirstName]     varchar(50) ,
[LastName]       varchar(50) ,
[subject]    varchar(50)
);
CREATE TABLE [Assignment]
(
 [AssignmentId] int IDENTITY (1, 1) primary key ,
 [Title]        varchar (50),
 [Description]  varchar(50) null ,
 [SubmissionDate] datetime 
);
GO

CREATE TABLE [Course]
(
	 [CourseId]     int IDENTITY (1, 1) primary key ,
	 [Title]        varchar(50) Unique  ,
	 [Stream]       varchar(50) ,
	 [Type]         varchar (50),
	 [StartDate]    datetime ,
	 [EndDate]      datetime ,
);
GO

create table [Student in Course]
(
	[CourseId]    int references [Course] ,
	[StudentId]    int references [Student] ,
);
create table [Assignment in Course]
(
	[CourseId]    int references [Course] ,
	[AssignmentId]    int references [Assignment] ,
);
create table [Trainer in Course]
(
	[CourseId]    int references [Course] ,
	[TrainerId]    int references [Trainer] ,
);
go


--Create a table for marks
create table [Marks](
[StudentId] int references Student,
[AssignmentId] int references Assignment,
[OralMark] int null,
[TotalMark] int null);
go

--Create some Students
insert into Student(FirstName,LastName,DateOfBirth,TuitionFees) values 
	('John', 'Doe', '1991/ 5/ 5', 2250),
	('Sarah', 'Doe', '1991/ 12/ 5', 2400),
	('George', 'Doe', '1991/ 6/ 5', 2500),
	('Anna', 'Doe', '1991/12/3', 2500),
	('John', 'Smith', '1991/ 12/ 3', 2400),
	('Sarah', 'Smith', '1991/ 1/ 5', 2500),
	('George', 'Smith', '1991/ 2/ 10', 2500),
	('Anna', 'Smith', '1991/ 12/ 3', 2500),
	('John', 'Curie', '1991/ 1/ 3', 2500),
	('Sarah', 'Curie', '1991/ 10/ 7', 2400),
	('George', 'Curie', '1991/ 4/ 11', 2500),
	('Anna', 'Curie', '1991/ 11/ 3', 2500);

--Create some trainers
insert into Trainer([FirstName],[LastName],[subject]) values 
	('Albert', 'Einstein', 'Physics'),
	('Max', 'Planck', 'Mathematics'),
	('Niels', 'Bohr', 'Physics'),
	('Enrico', 'Fermi', 'Chemistry');
--Create some assignments
insert into Assignment([Title],[Description],[SubmissionDate]) values 
	('Matrices',  'Individual Project', CONVERT(nvarchar,'2019- 5-30',23)),
	('Tensor Analysis',  'Individual Project', '2019/ 6/ 28'),
	('Non Linear Algebra',  'Group Project', '2019/ 7/ 26'),
	('Vector Analysis',  'Group Project', '2019/ 8/ 30');

--create some courses
 insert into Course([Title],[Stream],[Type],[StartDate],[EndDate]) values 
	('Physics', 'Fluid Dynamics', 'Full Time', '2019/ 6/ 30','2019/ 7/ 30'),
	('Mathematics', 'Algebra', 'Part Time', '2019/ 6/ 30','2019/ 7/ 30'),
	('Chemistry', 'Organic', 'Full Time', '2019/ 6/ 30','2019/ 7/ 30'),
	('Biology', 'Genetics', 'Part Time', '2019/ 6/ 30','2019/ 7/ 30');
--enroll students to courses
insert into [Student in Course] values 
	(1,1),(1,2),(1,3),(1,4),(2,5),(2,6),(2,7),(2,8),
	(3,9),(3,10),(3,5),(3,6),(4,1),(4,2),(4,11),(4,12);
go

--assign trainers to courses
insert into [Trainer in Course] values 
	(1,1),(1,3),(2,2),(2,4),(3,1),(4,2);

   --assign assignments to courses	
   insert into [Assignment in Course] values
   (1,2),(1,3),(2,3),(2,4),(3,4),(4,1),(4,2);
go
--•A list of all the students [2 marks]
create procedure GetStudent as
begin
select * from Student;
end
go
--• A list of all the trainers [2 marks]
create procedure GetTrainer as
begin
select * from Trainer;
end
go
--• A list of all the assignments [2 marks]
create procedure GetAssignment as
begin
select * from Assignment;
end
go
--• A list of all the courses [2 marks]
create procedure GetCourse as
begin
select * from Course;
end
go
--• All the students per course [2 marks]
create procedure GetStudentPerCourse as
	begin select  c.CourseId, c.Title as [Course], s.StudentId, concat(s.FirstName,' ', s.LastName) as [Name] from Student s
	join [Student in Course] sc on sc.StudentId=s.StudentId
	join [Course] c on sc.CourseId=c.CourseId
	order by c.CourseId, s.StudentId;
	end
go
--• All the trainers per course [2 marks]
create procedure GetTrainerPerCourse as
begin
	select c.CourseId,c.Title  as [Course], t.TrainerId, concat(t.FirstName,' ',t.LastName) as [Name] from Trainer t
	join [Trainer in Course] tc on tc.TrainerId=t.TrainerId
	join [Course] c on tc.CourseId=c.CourseId
	order by c.CourseId, t.TrainerId;
end
go
--• All the assignments per course [2 marks]
create procedure GetAssignmentPerCourse as
	begin
	select  c.CourseId, c.Title as [Course], a.AssignmentId,a.Title as [Assignment] from Assignment a
	join [Assignment in Course] ac on ac.AssignmentId=a.AssignmentId
	join [Course] c on ac.CourseId=c.CourseId
	order by c.CourseId, a.AssignmentId;
	end
go
--• All the assignments per course per student [2 marks]

create procedure GetAssignmentPerCoursePerStudent as
begin
	select  distinct (s.StudentId), concat(s.FirstName, s.LastName) as Name,c.Title as [Course], a.Title as[Assignments], isnull(m.OralMark,0) as OralMark, isnull(m.TotalMark,0) as TotalMark from Student s
	join [Student in Course] sc on sc.StudentId=s.StudentId
	join Course c on c.CourseId=sc.CourseId
	join [Assignment in Course] ac on c.CourseId=ac.CourseId
	join Assignment a on a.AssignmentId=ac.AssignmentId
	left join Marks m on m.AssignmentId=a.AssignmentId and s.StudentId=m.StudentId
	order by s.StudentId ,c.Title;
end
go
create procedure addMark (@studentId int, @AssignmentId int, @oralMark int, @totalMark int)  as
	begin
	if exists (select * from  Assignment a 
	join [Assignment in Course]  ac	on a.AssignmentId=ac.AssignmentId
	join Course c on c.CourseId=ac.CourseId
	join [Student In Course] sc on c.CourseId=sc.CourseId
	join Student s on s.StudentId=sc.StudentId
    where s.studentId=@studentId and a.AssignmentId=@AssignmentId)
	begin
		if not  exists (select * from Marks where studentId=@studentId and AssignmentId=@AssignmentId)
		begin
			insert into [Marks] values (@studentId,@AssignmentId,@oralMark,@totalMark);
		end
		else
		begin
			update  [Marks] set oralMark=@oralMark, totalMark=@totalMark where studentId=@studentId and AssignmentId=@AssignmentId;
		end
	end
	end
go

exec addMark 1,1,40,80;
exec addMark 2,3,30,70;
exec addMark 1,2,50,100;
exec addMark 12,2,30,70;
exec addMark 12,2,40,85; --example where new mark overrides last mark
exec addMark 11,3,50,100; --example where mark is not inserted because student doesn't have such assignature.
go

--• A list of students that belong to more than one courses [3 marks]
create procedure GetStudentInMoreCourses as
begin
	select top 100 percent s.StudentId, concat(s.FirstName,' ', s.LastName) as Name, count(c.CourseId) as NumberOfCourses from Student s
	join [Student in Course] sc on s.StudentId=sc.StudentId
	join Course c on c.CourseId=sc.CourseId
	group by s.StudentId, s.FirstName, s.LastName having count(c.courseId)>1;
end
go

create procedure AddStudent(@firstName varchar,@lastName varchar,@DateOfBirth Date,@TuitionFees decimal) as
begin
insert into Student(FirstName,LastName,DateOfBirth,TuitionFees) values(@firstName,@lastName,@DateOfBirth,@TuitionFees)
end

go
create procedure AddTrainer (@firstName varchar,@lastName varchar,@subject varchar)  as
begin
insert into Trainer(FirstName,LastName,[subject]) values(@firstName,@lastName,@subject)
end
go
create procedure AddAssignment(@Title varchar,@Description varchar,@SubmissionDate DateTime) as
begin
insert into Assignment([Title],[Description],[SubmissionDate]) values (@Title,@Description,@SubmissionDate)
end
go
create procedure AddCourse (@Title varchar,@Type varchar,@Stream varchar,@StartDate DateTime,@EndDate DateTime) as
begin
insert into Course([Title],[Type],[Stream],[StartDate],[EndDate]) values (@Title,@Type,@Stream,@StartDate,@EndDate)
end
go

create procedure checkIfInserted @courseId int,@studentId int as
begin
	declare @inserted bit;
	if not exists (select * from [Student in Course] where studentId=@studentId and courseId=@courseId)
	and @courseId in (select CourseId from Course) and @studentId in (select StudentId from Student)
	begin
		set @inserted=1;
	end	
	else 
	begin
		set @inserted=0;
	end
	select @inserted as isInserted;
end
go

create procedure addStudentToCourse @courseId int,@studentId int  as
begin
	if not exists (select * from [Student in Course] where studentId=@studentId and courseId=@courseId)
	and @courseId in (select CourseId from Course) and @studentId in (select StudentId from Student)
	begin
		insert into [Student in Course] values (@courseId,@studentId);
	end	
end
go
create procedure addTrainerToCourse @courseId int,@trainerId int as
begin
	if not exists (select * from [Trainer in Course] where trainerId=@trainerId and courseId=@courseId)
	and @courseId in (select CourseId from Course) and @trainerId in (select TrainerId from Trainer)
	begin
		insert into [Trainer in Course] values (@courseId,@trainerId);
	end
end
go
create procedure addAssignmentToCourse(@courseId int,@assignmentId int) as
begin
	if @courseId in (select CourseId from Course) and @assignmentId in (select AssignmentId from Assignment)
	and not exists (select * from [Assignment in Course] where AssignmentId=@assignmentId and courseId=@courseId)
	begin
		insert into [Assignment in Course] values (@courseId,@assignmentId)
	end
end
go

