--Note Remove Semester from section section uniqueness = sectionId, CourseId, Name
--SERVER NAME : LAPTOP-SRLIJTT6\SQLEXPRESS
SELECT Class_Id FROM CLASS WHERE Section_Id = 1 AND Date = '2020-1-1'
delete from section
select * from section

select * from users
select * from useraccount
select * from Student

SELECT * FROM REGISTERATION

SELECT R.Course_Id, C.Course_Code, C.Name FROM REGISTERATION R INNER JOIN OFFEREDCOURSE O ON 
O.OfferCourse_Id = R.Course_Id INNER JOIN COURSE C ON C.Course_Id = O.Course_Id
WHERE Student_Id = 9 AND Semester = 'Spring 2020'

drop table audittrail

CREATE TABLE [dbo].[AUDITTRAIL] (
    [Audit_Id]  INT          IDENTITY (1, 1) NOT NULL,
    [User_Id]   INT          NOT NULL,
    [Activity]  VARCHAR (50)  NULL,
    [Instance]  DATETIME    DEFAULT GETDATE() NOT NULL,
    CONSTRAINT [AT_ID] PRIMARY KEY CLUSTERED ([Audit_Id] ASC),
    CONSTRAINT [AT_UR] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[USERACCOUNT] ([User_Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

select * from registeration

select * from student

select * from users

SELECT * FROM TRANSCRIPT
SELECT * FROM MARKS
SELECT * FROM REGISTERATION
SELECT R.Course_Id, C.Course_Code, C.Name FROM REGISTERATION R INNER JOIN OFFEREDCOURSE O ON 
                        O.OfferCourse_Id = R.Course_Id INNER JOIN COURSE C ON C.Course_Id = O.Course_Id
                        WHERE Student_Id = 9 AND Semester = Spring 2020'
SELECT OfferCourse_Id, C.Course_Code, C.Name, CrdHrs FROM OFFEREDCOURSE O INNER JOIN COURSE C ON C.Course_Id = O.Course_Id 
            INNER JOIN DEPARTMENT D ON D.Dept_Id = O.Dept_Id WHERE O.OfferedIn = Spring 2020'AND O.Dept_Id = (SELECT Dept_Id FROM STUDENT WHERE User_Id = 9) AND NOT EXISTS ( SELECT C.Course_Id FROM PREREQ PR WHERE PR.Course_Id = C.Course_Id AND PR.PreReq_Id NOT IN (SELECT SC.Course_Id FROM STUDENT
            S JOIN TRANSCRIPT T ON S.User_Id = T.Student_Id JOIN SECTION SC ON T.Section_Id = SC.Section_Id WHERE S.User_Id = 9 AND T.Percentage >= 50))

SELECT OfferCourse_Id, C.Course_Code, C.Name, CrdHrs FROM OFFEREDCOURSE O INNER JOIN COURSE C ON C.Course_Id = O.Course_Id 
            INNER JOIN DEPARTMENT D ON D.Dept_Id = O.Dept_Id WHERE O.OfferedIn = 'Spring 2020'
            AND O.Dept_Id = (SELECT Dept_Id FROM STUDENT WHERE User_Id = 9) AND NOT EXISTS ( SELECT C.Course_Id FROM PREREQ PR 
            WHERE PR.Course_Id = C.Course_Id AND PR.PreReq_Id NOT IN (SELECT SC.Course_Id FROM STUDENT S JOIN TRANSCRIPT T ON 
            S.User_Id = T.Student_Id JOIN SECTION SC ON T.Section_Id = SC.Section_Id WHERE S.User_Id = 9 AND T.Percentage >= 50));


SELECT OfferCourse_Id, C.Course_Code, C.Name, CrdHrs FROM FLEX.DBO.OFFEREDCOURSE O INNER JOIN COURSE C ON C.Course_Id = O.Course_Id 
            INNER JOIN DEPARTMENT D ON D.Dept_Id = O.Dept_Id WHERE O.OfferedIn = 'Spring 2020' AND O.Dept_Id = (SELECT Dept_id FROM STUDENT WHERE User_Id = 9) AND NOT EXISTS ( SELECT C.Course_Id FROM Flex.dbo.PREREQ PR 
            WHERE PR.Course_Id = C.Course_Id AND PR.PreReq_Id NOT IN (SELECT SC.Course_Id FROM FLEX.DBO.STUDENT S JOIN FLEX.DBO.TRANSCRIPT T ON 
            S.User_Id = T.Student_Id JOIN FLEX.DBO.SECTION SC ON T.Section_Id = SC.Section_Id WHERE S.User_Id = 9 AND T.Grade >= 50));

SELECT * FROM REGISTERATION

select * from COURSE
select * from OFFEREDCOURSE
insert into offeredcourse values
(1, 3, NULL,  30, 'Spring 2020')


UPDATE OFFEREDCOURSE set Dept_Id = 1

            SELECT * FROM OFFEREDCOURSE
UPDATE TRANSCRIPT SET Percentage = null

SELECT OfferCourse_Id, C.Course_Code, C.Name AS Course_Name, D.Name FROM FLEX.DBO.OFFEREDCOURSE O INNER JOIN 
COURSE C ON C.Course_Id = O.Course_Id INNER JOIN DEPARTMENT D ON D.Dept_Id = O.Dept_Id WHERE O.OfferedIn = (
SELECT TOP(1) Semester FROM SEMESTERRECORD ORDER BY Start_Date DESC) AND O.Dept_Id = (SELECT Dept_id FROM STUDENT
WHERE User_Id = 9) AND NOT EXISTS ( SELECT C.Course_Id FROM Flex.dbo.PREREQ PR WHERE PR.Course_Id = C.Course_Id 
AND PR.PreReq_Id NOT IN (SELECT SC.Course_Id FROM STUDENT S JOIN TRANSCRIPT T ON 
S.User_Id = T.Student_Id JOIN SECTION SC ON T.Section_Id = SC.Section_Id WHERE S.User_Id = 9 AND
T.Percentage >= 50));



SELECT T.Section_Id, T.Student_Id, SUM(AbsolutesScored) FROM MARKS M
INNER JOIN TRANSCRIPT T ON T.Student_Id = M.Student_id
WHERE T.Section_Id = 1
GROUP BY T.Student_Id, T.Section_Id



SELECT Class_id FROM CLASS WHERE LectureNo = 
AND Section_Id = 1

SELECT Course_Code, OfferCourse_Id, COURSE.Name, TRANSCRIPT.Section_Id FROM COURSE 
INNER JOIN OFFEREDCOURSE ON OFFEREDCOURSE.OfferCourse_Id = COURSE.Course_Id 
INNER JOIN SECTION ON SECTION.Course_Id = OFFEREDCOURSE.OfferCourse_Id 
INNER JOIN TRANSCRIPT ON TRANSCRIPT.Section_Id = SECTION.Section_id 
WHERE Student_Id = 9 AND OfferedIn = (
SELECT TOP(1) Semester FROM SEMESTERRECORD ORDER BY Start_Date DESC)

SELECT * FROM COURSE
INNER JOIN OFFEREDCOURSE ON OFFEREDCOURSE.OfferCourse_Id = COURSE.Course_Id 
INNER JOIN SECTION ON SECTION.Course_Id = OFFEREDCOURSE.OfferCourse_Id 
INNER JOIN TRANSCRIPT ON TRANSCRIPT.Section_Id = SECTION.Section_id 
WHERE Student_Id = 9 AND OfferedIn = (
SELECT TOP(1) Semester FROM SEMESTERRECORD ORDER BY Start_Date)


SELECT C.Course_Id, C.Course_Code, C.Name, D.Name
FROM FLEX.DBO.OFFEREDCOURSE O INNER JOIN 
COURSE C ON C.Course_Id = O.Course_Id
INNER JOIN DEPARTMENT D ON D.Dept_Id = O.Dept_Id
WHERE O.OfferedIn = (SELECT TOP(1) Semester FROM SEMESTERRECORD ORDER BY Start_Date DESC)
    AND O.Dept_Id = (SELECT Dept_id FROM STUDENT WHERE User_Id = 9)
	AND NOT EXISTS (
    SELECT C.Course_Id
    FROM Flex.dbo.PREREQ PR
    WHERE PR.Course_Id = C.Course_Id AND PR.PreReq_Id NOT IN (
        SELECT SC.Course_Id
        FROM FLEX.DBO.STUDENT S
        JOIN FLEX.DBO.TRANSCRIPT T ON S.User_Id = T.Student_Id
        JOIN FLEX.DBO.SECTION SC ON T.Section_Id = SC.Section_Id
        WHERE S.User_Id = 9 AND T.Grade >= 50
    )
);
SELECT Name, Weightage, Range FROM EVALUATION WHERE Eval_Id = 8

SELECT U.User_Id AS ID, RegNo, CONCAT(First_Name, ' ', Last_Name) AS Student_Name FROM USERS U INNER JOIN\r\n                        USERACCOUNT A ON U.User_Id = A.User_Id INNER JOIN TRANSCRIPT T ON T.Student_Id = U.User_Id INNER JOIN SECTION S\r\n                        ON S.Section_Id = T.Section_Id WHERE S.Section_Id = 4"	string

select * from transcript
select * from section

select * from evaluation
select * from marks

update marks set Obtained = 
case When student_Id = 9 then 12
when student_Id = 10 then 13
end, AbsolutesScored = Obtained*3/15
where Eval_Id = 8

update marks set AbsolutesScored = Obtained*3/15
where Eval_Id = 8


update marks set Obtained = null, AbsolutesScored = null
insert into marks(student_Id, Eval_Id) values (10, 8)

alter table marks add AbsolutesScored FLOAT

UPDATE MARKS Set Obtained =
CASE 
    WHEN Student_Id = 9 THEN 7 WHEN Student_Id = 8 THEN 8
END 
WHERE Eval_Id = 8

SELECT Student_Id FROM MARKS WHERE Student_Id = 9 AND Eval_Id = 2

update Section set Instructor_Id = 1 where section_Id = 1 

SELECT USERS.User_Id AS ID, RegNo, CONCAT(First_Name, ' ', Last_Name) AS Student_Name FROM USERS INNER JOIN USERACCOUNT 
ON USERS.User_id = USERACCOUNT.User_Id INNER JOIN ATTENDENCE ON ATTENDENCE.Student_Id = USERS.User_Id INNER JOIN CLASS 
ON CLASS.Class_Id = ATTENDENCE.Class_Id WHERE CLASS.Class_Id = 1 AND Date = '2'

SELECT U.User_Id AS ID, RegNo, CONCAT(First_Name, ' ', Last_Name) AS Student_Name FROM USERS U INNER JOIN 
USERACCOUNT A ON U.User_id = A.User_Id INNER JOIN TRANSCRIPT T ON T.Student_Id = U.User_Id INNER JOIN SECTION S
ON S.Section_Id = T.Section_Id WHERE S.Section_Id = 4

SELECT * FROM SECTION
insert into transcript(Student_Id, Section_Id) values
(9, 1)

select * from transcript
select * from section

CREATE PROCEDURE GetAttendanceBySectionName
    @sectionId NVARCHAR(255)
AS
BEGIN
    SELECT s.User_Id, CONCAT(u.First_name, ' ', u.Last_name) AS Name, 
           (COUNT(CASE WHEN a.Status='P' THEN 1 ELSE 0 END) * 1.0 /COUNT(*)) AS Attendance_Percentage
    FROM STUDENT s 
    INNER JOIN USERS u ON s.User_Id = u.User_Id 
    INNER JOIN ATTENDENCE a ON a.Student_Id = s.User_Id 
    INNER JOIN CLASS c ON a.Class_Id = c.Class_Id 
    INNER JOIN SECTION sec ON sec.Section_id = c.Section_id
    WHERE sec.Section_Id = @sectionId
    GROUP BY s.User_Id, sec.Name, u.First_name, u.Last_name;
END

SELECT * FROM SECTION
SELECT * FROM ATTENDENCE

select * from OFFEREDCOURSE

SELECT DISTINCT OfferedIn FROM SECTION S INNER JOIN OFFEREDCOURSE O ON S.Course_Id = O.OfferCourse_Id WHERE Instructor_Id = 2

select * from student

SELECT OfferCourse_Id, Course_Code, Name AS Course_Name, CrdHrs, OfferedIn FROM COURSE INNER JOIN OFFEREDCOURSE ON
COURSE.Course_Id = OFFEREDCOURSE.Course_Id INNER JOIN STUDENT ON STUDENT.Dept_Id = OFFEREDCOURSE.Dept_Id WHERE 
STUDENT.User_Id = 9 AND OfferedIn = 'Spring 2020';

EXEC GetAttendanceBySectionName 1

SELECT CONCAT(First_name,' ',Last_name,' (',Username,') ') AS UN FROM FACULTY INNER JOIN USERS ON FACULTY.User_Id = USERS.User_Id
INNER JOIN USERACCOUNT ON USERACCOUNT.User_Id = FACULTY.User_Id WHERE FACULTY.Dept_Id IN (SELECT DEPARTMENT.Dept_Id FROM CAMPUS
INNER JOIN ADMIN ON ADMIN.Campus_Id = CAMPUS.Campus_Id INNER JOIN DEPARTMENT ON DEPARTMENT.Campus_Id = CAMPUS.Campus_Id 
WHERE ADMIN.User_Id = 1) AND FACULTY.JobTitle LIKE '%Professor' AND FACULTY.User_Id NOT IN (SELECT Coordinator_Id FROM
OFFEREDCOURSE WHERE OfferedIn = 'Spring 2020' AND Coordinator_Id  IS NOT NULL)

SELECT CONCAT(First_name,' ', Last_name,' (', Username, ') ') AS I FROM FACULTY INNER JOIN USERS ON FACULTY.User_Id = USERS.User_Id 
INNER JOIN USERACCOUNT ON USERACCOUNT.User_Id = USERS.User_Id WHERE Dept_Id IN (SELECT Dept_Id FROM SECTION INNER JOIN OFFEREDCOURSE 
ON SECTION.Course_Id = OFFEREDCOURSE.OfferCourse_Id WHERE Section_Id = 1) AND FACULTY.User_Id NOT IN( SELECT Instructor_Id FROM SECTION
INNER JOIN OFFEREDCOURSE ON OFFEREDCOURSE.OfferCourse_Id = SECTION.Course_Id WHERE OfferedIn = 'Spring 2020' GROUP BY Instructor_Id HAVING 
COUNT(Instructor_Id) >= 3) 

SELECT DEPARTMENT.Name AS Dept_Code, Course_Code, SECTION.Name AS Section_Name, CONCAT(First_name, ' ', Last_Name) AS Instructor_Name,
SECTION.Section_Id FROM SECTION INNER JOIN OFFEREDCOURSE ON SECTION.Course_Id = OFFEREDCOURSE.OfferCourse_Id INNER JOIN COURSE ON
COURSE.Course_Id = OFFEREDCOURSE.Course_Id INNER JOIN DEPARTMENT ON DEPARTMENT.Dept_Id = OFFEREDCOURSE.Dept_Id INNER JOIN CAMPUS ON
CAMPUS.Campus_Id = DEPARTMENT.Campus_Id LEFT JOIN USERS ON USERS.User_Id = SECTION.Instructor_Id WHERE OfferedIn = 'Spring 2020' AND
CAMPUS.Campus_Id = ( SELECT Campus_Id FROM ADMIN WHERE User_Id = 1) 

SELECT CONCAT(First_name,' ', Last_name,' (', Username, ') ') AS I FROM FACULTY INNER JOIN USERS ON FACULTY.User_Id = USERS.User_Id
INNER JOIN USERACCOUNT ON USERACCOUNT.User_Id = USERS.User_Id WHERE Dept_Id IN(SELECT Dept_Id FROM SECTION INNER JOIN OFFEREDCOURSE ON
SECTION.Course_Id = OFFEREDCOURSE.OfferCourse_Id WHERE Section_Id = 1) AND FACULTY.User_Id NOT IN( SELECT Instructor_Id FROM SECTION 
INNER JOIN OFFEREDCOURSE ON OFFEREDCOURSE.OfferCourse_Id = SECTION.Course_Id WHERE OfferedIn = 'Spring 2020'GROUP BY Instructor_Id HAVING COUNT(Instructor_Id) >= 3) 

SELECT CONCAT(First_name,' ', Last_name,' (', Username, ') ') AS I FROM FACULTY INNER JOIN USERS ON FACULTY.User_Id = USERS.User_Id 
INNER JOIN USERACCOUNT ON USERACCOUNT.User_Id = USERS.User_Id WHERE Dept_Id IN (SELECT Dept_Id FROM SECTION INNER JOIN OFFEREDCOURSE ON
SECTION.Course_Id = OFFEREDCOURSE.OfferCourse_Id WHERE Section_Id = 2) AND FACULTY.User_Id NOT IN( SELECT Instructor_Id FROM SECTION 
INNER JOIN OFFEREDCOURSE ON OFFEREDCOURSE.OfferCourse_Id = SECTION.Course_Id WHERE OfferedIn = 'Spring 2020' GROUP BY Instructor_Id 
HAVING COUNT(Instructor_Id) >= 3) 


select * from section

update faculty set Dept_Id = 1 

SELECT YEAR(GETDATE())
SELECT * FROM STUDENT

--Department, Parent Section, BatchNumber, Degree, 

SELECT '21I-1366' WHERE '21I-1366' LIKE '__[ILKPF]-____'

SELECT * FROM STUDENT

SELECT Name FROM DEPARTMENT INNER JOIN CAMPUS
ON CAMPUS.Campus_Id = DEPARTMENT.Campus_Id
WHERE CAMPUS.Campus_Id = (SELECT Campus_Id FROM 
ADMIN WHERE User_Id = 1)

SELECT * FROM STUDENT


SELECT OfferCourse_Id, Course_Code, Name AS Course_Name, CrdHrs, OfferedIn FROM COURSE INNER JOIN OFFEREDCOURSE ON
COURSE.Course_Id = OFFEREDCOURSE.Course_Id INNER JOIN STUDENT ON STUDENT.Dept_Id = OFFEREDCOURSE.Dept_Id WHERE
STUDENT.User_Id = 9 AND OfferedIn = 'Spring 2020';




SELECT MAX(User_Id) FROM USERS
INSERT INTO USERACCOUNT (, Email, Password, Type) VALUES
--FirstName, LastName, CNIC, Phone, Gender, Address, City

SELECT Course_Code, OfferCourse_Id FROM COURSE INNER JOIN OFFEREDCOURSE ON OFFEREDCOURSE.OfferCourse_Id = COURSE.Course_Id
INNER JOIN SECTION ON SECTION.Course_Id = OFFEREDCOURSE.OfferCourse_Id INNER JOIN TRANSCRIPT ON
TRANSCRIPT.Section_Id = SECTION.Section_id WHERE Student_Id = 9 AND Semester = (
SELECT TOP(1) Semester_Name FROM SEMESTER ORDER BY Start_Date)

SELECT * FROM SECTION INNER JOIN TRANSCRIPT
ON TRANSCRIPT.Section_Id = SECTION.Section_Id WHERE Student_Id = 9
INNER JOIN OFFEREDCOURSE ON OFFEREDCOURSE.Offer_Id = SECTION.Course_Id
INNER JOIN COURSE ON COURSE.

update transcript set Grade = 'A'


SELECT LectureNo,Duration, Date, Status FROM ATTENDENCE 
INNER JOIN CLASS ON CLASS.Class_Id = ATTENDENCE.Class_Id
INNER JOIN SECTION ON SECTION.Section_id = CLASS.Section_id
WHERE Course_Id = 1 AND Student_Id = 9


SELECT C.Course_Code, C.Course_Name, S.Section_Name, C.CrdHrs, T.Grade, D.Points

SELECT O.OfferedIn, C.Course_Code, C.Name, S.Name, C.CrdHrs, T.Grade, 
FROM TRANSCRIPT T 
INNER JOIN SECTION S ON S.Section_Id = T.Section_Id
INNER JOIN OFFEREDCOURSE O ON O.OfferCourse_Id = S.Course_Id
INNER JOIN COURSE C ON C.Course_Id = O.Course_Id
WHERE Student_Id = 9 ORDER BY OfferedIn ASC



SELECT * FROM TRANSCRIPT
SELECT * FROM SECITON
SELECT * FROM OFFEREDCOURSE
SELECT * FROM COURSE



SELECT * FROM EVALUATION
SELECT * FROM MARKS

SELECT Name, Weightage, Range, Obtained FROM EVALUATION INNER JOIN MARKS
ON EVALUATION.Eval_Id = MARKS.Eval_Id WHERE Student_Id = 9
AND Course_Id = 1

SELECT OfferCourse_Id, CONCAT(Course_Code, ' - ', COURSE.Name) AS Code, Section.Name FROM SECTION INNER JOIN OFFEREDCOURSE
ON OFFEREDCOURSE.OfferCourse_Id = SECTION.Course_Id INNER JOIN COURSE ON COURSE.Course_Id = OFFEREDCOURSE.Course_Id 
WHERE SECTION.Instructor_Id = 1 AND OfferedIn = 'Spring 2020'

SELECT Name, Section_Id FROM SECTION WHERE Course_Id = 1 AND Instructor_Id = 1

        SELECT DISTINCT O.OfferCourse_Id, C.Course_Id AS CourseId, CONCAT(C.Course_Code, ' - ' , C.Name , ' - ' , O.OfferCourse_Id) AS CourseDisplay
        FROM OFFEREDCOURSE O
        JOIN COURSE C ON O.Course_Id = C.Course_Id
        JOIN SECTION S ON O.OfferCourse_Id = S.Course_Id
        WHERE S.Instructor_Id = 1


SELECT * FROM SECTION



select * from offeredcourse o inner join evaluation e on o.OfferCourse_Id = e.Course_Id

SELECT Name FROM EVALUATION Where course_Id = 1 ORDER BY Name

SELECT Course_Code FROM COURSE INNER JOIN OFFEREDCOURSE ON OFFEREDCOURSE.OfferCourse_Id = COURSE.Course_Id
INNER JOIN SECTION ON SECTION.Course_Id = OFFEREDCOURSE.OfferCourse_Id INNER JOIN TRANSCRIPT ON 
TRANSCRIPT.Section_Id = SECTION.Section_id WHERE Student_Id = 9 AND Semester = (
SELECT TOP(1) Semester_Name FROM SEMESTER ORDER BY Start_Date)

SELECT TOP(1) Semester FROM SEMESTERRECORD ORDER BY Start_Date DESC

TRANSCRIPT.Student_Id


select * from registeration

select * from transcript

select * from attendence

select * from class

AND Date = '2020-1-1'

SELECT USERS.User_Id AS ID, RegNo, CONCAT(First_Name, ' ', Last_Name) AS Student_Name 
FROM TRANSCRIPT INNER JOIN USERACCOUNT ON TRANSCRIPT.Student_Id = USERACCOUNT.User_Id 
INNER JOIN USERS ON USERS.User_Id = TRANSCRIPT.Student_Id INNER JOIN ATTENDECNCE
WHERE TRANSCRIPT.Section_Id = 1

SELECT USERS.User_Id AS ID, RegNo, CONCAT(First_Name, ' ', Last_Name) AS Student_Name
FROM USERS INNER JOIN USERACCOUNT ON USERS.User_id = USERACCOUNT.User_Id
INNER JOIN ATTENDENCE ON ATTENDENCE.Student_Id = USERS.User_Id
INNER JOIN CLASS ON CLASS.Class_Id = ATTENDENCE.Class_Id
WHERE CLASS.Class_Id = 8 AND Date = '2020-01-11'

SELECT USERS.User_Id AS ID, RegNo, CONCAT(First_Name, ' ', Last_Name) AS Student_Name FROM USERS INNER JOIN USERACCOUNT ON USERS.User_id = USERACCOUNT.User_Id INNER JOIN ATTENDENCE ON ATTENDENCE.Student_Id = USERS.User_Id INNER JOIN CLASS ON CLASS.Class_Id = ATTENDENCE.Class_Id WHERE CLASS.Class_Id = 8 AND Date = '2020-1-11'


SELECT * FROM ATTENDENCE INNER JOIN CLASS ON CLASS.Class_Id = ATTENDENCE.Class_Id
WHERE Class_Id = AND DATE = 

SELECT * FROM ATTENDENCE 

UPDATE ATTENDENCE
SET Status = 'A'
WHERE Student_Id IN 



SELECT * FROM CLASS


WHERE Class_Id = 

SELECT MAX(LectureNo) FROM CLASS
WHERE CLASS.Section_Id = '1'

INSERT INTO CLASS (Section_Id, LectureNo, Duration, Date) VALUES 
(section, LectureNo, duration, date)

SELECT Class_Id FROM CLASS WHERE Section_Id = 1 AND Date = '2020-2-1'

INSERT INTO USERS (First_Name, Last_Name, CNIC, DoB, Gender, Address, City, Phone ) VALUES
();


SELECT Student_Id FROM TRANSCRIPT WHERE Section_Id = 1

SELECt * from USERS

INSERT INTO ATTENDENCE (Class_Id, Student_Id, Status) VALUES 
(),



/*
select USERACCOUNT.Password from useraccount 
left join student on useraccount.user_id = student.user_id
where student.RollNo = '21I-1366'

delete from ADMIN 
where user_id = 2=
SELECT First_name, Last_name, CNIC, DOB, Gender, Address, USERS.Phone, Username, Email, JobTitle, Salary FROM FACULTY INNER JOIN USERS ON FACULTY.User_Id = USERS.User_Id INNER JOIN USERACCOUNT ON USERACCOUNT.User_Id = FACULTY.User_Id LEFT JOIN DEPARTMENT ON DEPARTMENT.Dept_Id = FACULTY.Dept_Id WHERE FACULTY.User_Id = 2
SELECT Course_Code, SECTION.Name AS Section_Name, Instructor_Id FROM SECTION
INNER JOIN OFFEREDCOURSE ON SECTION.Course_Id = OFFEREDCOURSE.OfferCourse_Id 
INNER JOIN COURSE ON COURSE.Course_Id = OFFEREDCOURSE.Course_Id
WHERE Semester = 'Spring 2020';

select * from users
select * from Section

select * from OfferedCourse

SELECT Location FROM CAMPUS

SELECT CONCAT(First_name,' ',Last_name,' (',Username,') ') AS UN
FROM FACULTY INNER JOIN USERS ON FACULTY.User_Id = USERS.User_Id
INNER JOIN USERACCOUNT ON USERACCOUNT.User_Id = FACULTY.User_Id
WHERE FACULTY.Dept_Id IN (SELECT DEPARTMENT.Dept_Id FROM CAMPUS INNER JOIN
ADMIN ON ADMIN.Campus_Id = CAMPUS.Campus_Id INNER JOIN DEPARTMENT
ON DEPARTMENT.Campus_Id = CAMPUS.Campus_Id WHERE ADMIN.User_Id = 1)
AND FACULTY.JobTitle LIKE '% Professor'
AND FACULTY.User_Id NOT IN (SELECT Coordinator_Id FROM OFFEREDCOURSE
WHERE OfferedIn = 'Spring 2020' AND Coordinator_Id  IS NOT NULL)

SELECT CONCAT(First_name, ' ', Last_name, ' (', Username, ') ')
FROM FACULTY INNER JOIN USERS ON FACULTY.User_Id = USERS.User_Id
INNER JOIN USERACCOUNT ON USERS.User_Id = USERACCOUNT.User_Id
WHERE Dept_Id IN (SELECT Dept_Id FROM DEPARTMENT INNER JOIN CAMPUS
ON CAMPUS.Campus_Id = DEPARTMENT.Campus_Id WHERE CAMPUS.Campus_Id = (SELECT 
Campus_Id FROM ADMIN WHERE User_Id = 1) AND Name = 'CS')


SELECT CONCAT(First_name, ' ', Last_name, ' (', Username, ') ') 
FROM FACULTY INNER JOIN USERS ON FACULTY.User_Id = USERS.User_Id
INNER JOIN USERACCOUNT ON USERACCOUNT.User_Id = USERS.User_Id
WHERE Dept_Id IN (SELECT Dept_Id FROM SECTION INNER JOIN 
OFFEREDCOURSE ON SECTION.Course_Id = OFFEREDCOURSE.OfferCourse_Id
WHERE Section_Id = 12) AND FACULTY.User_Id NOT IN (
SELECT Instructor_Id FROM SECTION WHERE Semester = 'Spring 2020' 
GROUP BY Instructor_Id  HAVING COUNT(Instructor_Id) > 3 )


SELECT DISTINCT CONCAT(Course_Code, ' ', COURSE.Name) AS Code FROM SECTION
INNER JOIN OFFEREDCOURSE ON OFFEREDCOURSE.OfferCourse_Id = SECTION.Course_Id
INNER JOIN COURSE ON COURSE.Course_Id = OFFEREDCOURSE.Course_Id
WHERE SECTION.Instructor_Id = 1 AND OfferedIn = 

select * from section inner join OFFEREDCOURSE
on OFFEREDCOURSE.OfferCourse_Id = Section.Course_Id
inner join course
on course.Course_id = OFFEREDCOURSE.Course_Id

SELECT SECTION.Section_id, SECTION.Name FROM SECTION INNER JOIN OFFEREDCOURSE
ON SECTION.Course_Id = OFFEREDCOURSE.OfferCourse_Id
INNER JOIN COURSE ON COURSE.Course_Id = OFFEREDCOURSE.Course_Id
WHERE Instructor_Id = 1 AND OfferedIn = 'Spring 2020'
AND Course_Code = 'CL2001'

SELECT SECTION.Section_Id, SECTION.Name FROM SECTION INNER JOIN OFFEREDCOURSE ON SECTION.Course_Id = OFFEREDCOURSE.OfferCourse_Id INNER JOIN COURSE ON COURSE.Course_Id = OFFEREDCOURSE.Course_Id WHERE Instructor_Id = 1 AND OfferedIn = 'Spring 2020' AND Course_Code = 'CL2001'

Select * from transcript where Section_Id = 1

select * from USERACCOUNT

SELECT RegNo, CONCAT(First_Name, ' ', Last_Name) AS Student_Name FROM TRANSCRIPT 
INNER JOIN USERACCOUNT ON TRANSCRIPT.Student_Id = USERACCOUNT.User_Id
INNER JOIN USERS ON USERS.User_Id = TRANSCRIPT.Student_Id
WHERE TRANSCRIPT.Section_Id = 1


select * from registeration

select * from transcript

select * from attendence

select * from class

SELECT MAX(LectureNo) FROM CLASS
WHERE CLASS.Section_Id = '1'




INNER JOIN FACULTY ON FACULTY.Dept_Id = OFFEREDCOURSE.Dept_Id
INNER JOIN USERS ON USERS.User_Id = FACULTY.User_Id

SELECT User_Id FROM USERACCOUNT WHERE Username = 

ALTER TABLE ADMIN ADD
	Campus_Id	TINYINT,
	CONSTRAINT A_C FOREIGN KEY (Campus_Id) REFERENCES CAMPUS (Campus_Id)


CREATE TABLE SEMESTER (
    Semester_Name      VARCHAR(12),
    Start_Date         DATE,
    CONSTRAINT SM_ID PRIMARY KEY (Semester_Name),
    CONSTRAINT SM_UD CHECK (Semester_Name LIKE 'Spring ____' OR 
    Semester_Name LIKE 'Summer ____' OR Semester_Name LIKE 'Fall ____' )
)
drop table semester

insert into semester VALUES
('Spring 2020', '2020/01/14'),
('Fall 2020', '2020/07/12');

SELECT TOP(1) Semester_Name FROM SEMESTER
ORDER BY Start_Date

SELECT DISTINCT TOP(3) Semester_Name FROM SEMESTER INNER JOIN
SECTION ON SECTION.Semester = SEMESTER.Semester_Name
INNER JOIN FACULTY ON FACULTY.User_Id = SECTION.Instructor_Id
WHERE User_Id = 2;

SELECT DEPARTMENT.Name AS Dept_Code, Course_Code, SECTION.Name AS Section_Name,
CONCAT(First_name, ' ', Last_Name) AS Instructor_Name, SECTION.Section_Id
FROM SECTION INNER JOIN OFFEREDCOURSE ON SECTION.Course_Id = OFFEREDCOURSE.OfferCourse_Id
INNER JOIN COURSE ON COURSE.Course_Id = OFFEREDCOURSE.Course_Id 
INNER JOIN DEPARTMENT ON DEPARTMENT.Dept_Id = OFFEREDCOURSE.Dept_Id 
INNER JOIN CAMPUS ON CAMPUS.Campus_Id = DEPARTMENT.Campus_Id
LEFT JOIN USERS ON USERS.User_Id = SECTION.Instructor_Id 
WHERE Semester = 'Spring 2020' AND CAMPUS.Campus_Id = ( SELECT Campus_Id FROM ADMIN WHERE User_Id = 1) 


select * from offeredcourse

SELECT Location FROM CAMPUS 
INNER JOIN DEPARTMENT ON CAMPUS.Campus_Id = DEPARTMENT.Dept_Id
INNER JOIN FACULTY ON FACULTY.Dept_Id = DEPARTMENT.Dept_Id
WHERE FACULTY.User_Id = 2

select * from section

SELECT DISTINCT Semester FROM SECTION 
WHERE Instructor_Id = 1

--SERVER NAME : LAPTOP-SRLIJTT6\SQLEXPRESS
/*
"SELECT CONCAT(USERACCOUNT.User_Id, USERACCOUNT.Type) AS Result
FROM USERACCOUNT WHERE USERACCOUNT.Password = '"+ password +"'
AND ((USERACCOUNT.RegNo IS NULL AND USERACCOUNT.Username = '" + username + "')
OR (USERACCOUNT.Username IS NULL AND USERACCOUNT.RegNo = '" + username + "'))"
*/

/*
"SELECT CONCAT(USERACCOUNT.User_Id, USERACCOUNT.Type) AS Result
FROM USERACCOUNT WHERE USERACCOUNT.Password = '"+ password +"'
AND ((USERACCOUNT.RegNo IS NULL AND USERACCOUNT.Username = '" + username + "')
OR (USERACCOUNT.Username IS NULL AND USERACCOUNT.RegNo = '" + username + "'))"
*/

SELECT First_name, Last_name, CNIC, DoB, Gender, USERS.Address, 
USERS.Phone, City, RegNo, Email, PSection, BatchNo, Degree, Status, 
Location FROM USERS 
INNER JOIN USERACCOUNT ON USERS.User_Id = USERACCOUNT.User_Id
INNER JOIN STUDENT ON USERS.User_Id = STUDENT.User_id
INNER JOIN DEPARTMENT ON DEPARTMENT.Dept_Id = STUDENT.Dept_Id
INNER JOIN CAMPUS ON DEPARTMENT.Campus_Id = CAMPUS.Campus_Id
WHERE USERS.User_Id = 1;


SELECT First_name, Last_name, CNIC, DoB, Gender, USERS.Address,
City, RegNo, Email, PSection, BatchNo, Degree, Status, Location,
USERS.Phone FROM USERS
INNER JOIN USERACCOUNT ON USERS.User_Id = USERACCOUNT.User_Id
INNER JOIN STUDENT ON USERS.User_Id = STUDENT.User_id
INNER JOIN DEPARTMENT ON DEPARTMENT.Dept_Id = STUDENT.Dept_Id
INNER JOIN CAMPUS ON DEPARTMENT.Campus_Id = CAMPUS.Campus_Id
WHERE USERS.User_Id = 1


SELECT First_Name, Last_Name, CNIC, DOB, Gender, Address, City,
Phone, Username, Email, JobTitle, Salary FROM USERS 
INNER JOIN USERACCOUNT ON USERS.User_Id = USERACCOUNT.User_Id
INNER JOIN ADMIN ON USERS.User_Id = ADMIN.User_id
WHERE USERS.User_Id = 3;

select * from offeredcourse

select * from department

SELECT * FROM OFFEREDCOURSE WHERE Dept_Id = 4 AND Course_Id = 3 AND OfferedIn = 'Spring 2020';

SELECT Dept_Id FROM DEPARTMENT
WHERE DEPARTMENT.Name = Deptcode;

SELECT * FROM REGISTERATION

SELECT Course_Id FROM COURSE
WHERE COURSE.Course_Code = 'CL1002';

SELECT TotalSeats from offeredcourse

SELECT Course_Id, TotalSeats, COUNT(Course_Id) TotalRegistered FROM REGISTERATION
WHERE Semester = 'Spring 2020' GROUP BY Course_Id

SELECT Course_Id, TotalSeats, COUNT(Course_Id) TotalRegistered FROM REGISTERATION
WHERE Semester = 'sPRING 2020' GROUP BY Course_Id
select * from OfferedCourse

SELECT 't'
where
'Spring 2021' LIKE 'Spring 20__' 
OR 'Spring 2021' LIKE 'Summer 20__'
OR 'Spring 2021' LIKE 'Fall 20__'

ALTER TABLE FLEX.DBO.OFFEREDCOURSE ADD
	CONSTRAINT OC_Sem CHECK (OfferedIn LIKE 'Spring 20__' 
							OR OfferedIn LIKE 'Summer 20__'
							OR OfferedIn LIKE 'Fall 20__');


BULK INSERT FLEX.DBO.COURSE
FROM 'C:\Users\devil\Documents\SQL Server Management Studio\Project\Course.csv'
WITH ( FIRSTROW = 2, FORMAT = 'csv')

BULK INSERT FLEX.DBO.CAMPUS
FROM 'C:\Users\devil\Documents\SQL Server Management Studio\Project\Campus.csv'
WITH ( FIRSTROW = 2, FORMAT = 'csv')




SELECT * FROM COURSE

ALTER TABLE OFFEREDCOURSE 
	ALTER 

INSERT INTO FLEX.DBO.OFFEREDCOURSE (Dept_Id, Course_Id, OfferedIn) VALUES
(4, 2, 'Spring 2020');
delete from offeredcourse

select First_name, Last_name, CNIC, DOB, Gender, Address,Phone,
Username, Email, JobTitle, Salary
from admin INNER JOIN Users on admin.User_Id = Users.User_id 
INNER JOIN useraccount on useraccount.User_Id = admin.User_Id


SELECT First_name, Last_name, CNIC, DOB, Gender, Address, 
Phone, Username, Email, JobTitle, Salary 
SELECT * FROM ADMIN 
INNER JOIN USERS ON ADMIN.User_Id = USERS.User_Id
INNER JOIN USERACCOUNT ON USERACCOUNT.User_Id = ADMIN.User_Id



select * from department
select * from offeredcourse
select * from course
select * from registeration

select * from student

--SERVER NAME : LAPTOP-SRLIJTT6\SQLEXPRESS
SELECT OfferCourse_Id, Course_Code, DEPARTMENT.Name AS Dept_Code, COURSE.Name 
AS Course_Name, CONCAT(First_name, ' ', Last_name) AS Coordinator,OfferedIn FROM COURSE 
INNER JOIN OFFEREDCOURSE ON COURSE.Course_Id = OFFEREDCOURSE.Course_Id
INNER JOIN STUDENT ON STUDENT.Dept_Id = OFFEREDCOURSE.Dept_Id 
INNER JOIN DEPARTMENT ON DEPARTMENT.Dept_Id = STUDENT.Dept_Id
INNER JOIN USERS ON STUDENT.User_Id = USERS.User_Id
WHERE STUDENT.User_Id = 9 AND OfferedIn = 'Spring 2020'



SELECT OfferCourse_Id, Course_Code, DEPARTMENT.Name AS Dept_name, 
CONCAT (First_name, ' ', Last_name) AS Coordinator FROM OFFEREDCOURSE 
INNER JOIN DEPARTMENT ON DEPARTMENT.Dept_Id = OFFEREDCOURSE.Dept_Id 
INNER JOIN COURSE ON COURSE.Course_Id =  OFFEREDCOURSE.Course_Id 
LEFT JOIN USERS ON OFFEREDCOURSE.Coordinator_Id = USERS.User_Id 
WHERE OfferedIn = 'Spring 2020' AND DEPARTMENT.Campus_Id IN (
SELECT Campus_Id FROM ADMIN WHERE User_Id = 1)


SELECT CONCAT(First_name,' ',Last_name,' (',Username,') ') AS UN
FROM FACULTY INNER JOIN USERS ON FACULTY.User_Id = USERS.User_Id
INNER JOIN USERACCOUNT ON USERACCOUNT.User_Id = FACULTY.User_Id
WHERE FACULTY.Dept_Id IN (SELECT Dept_In FROM CAMPUS INNER JOIN
ADMIN ON ADMIN.Campus_Id = CAMPUS.Campus_Id 
WHERE ADMIN.User_Id = 1)

update Student
	set Dept_id = 1

SELECT OfferCourse_Id, Course_Code, Name AS Course_Name, OfferedIn, Coordinator_Id " +
                        "FROM COURSE INNER JOIN OFFEREDCOURSE ON COURSE.Course_Id = OFFEREDCOURSE.Course_Id " +
                        "INNER JOIN STUDENT ON STUDENT.Dept_Id = OFFEREDCOURSE.Dept_Id WHERE STUDENT.User_Id = " + 
                        User_Id + " AND OfferedIn = '" + semester + "';"

SELECT FACULTY.User_Id AS Faculty_Id FROM OFFEREDCOURSE 
INNER JOIN FACULTY ON FACULTY.Dept_Id = OFFEREDCOURSE.Dept_Id
INNER JOIN USERS ON FACULTY.User_Id = USERS.User_Id
INNER JOIN USERACCOUNT ON USERACCOUNT.User_Id = USERS.User_Id




/*    public int Course_Id { get; set; }
    public string Course_Code { get; set; }
    public string Course_Name { get; set; }
    public string Semester { get; set; }
    public int CrdHrs { get; set; }*/

"INSERT INTO REGISTERATION VALUES (" + User_Id + ", " + Course_Id + ", '" + Semester + "');"

"SELECT Reg_Id FROM REGISTERATION WHERE Student_Id = " User_Id +
" AND Course_Id = " + Course_Id + " AND Semester = '" + Semester + "';";

SELECT Reg_Id FROM REGISTERATION WHERE Student_Id = 9
AND Course_Id = 3 AND Semester = 'Spring 2020';

select * from registeration

delete from registeration

DELETE FROM OFFEREDCOURSE WHERE Course_Id = Course_Id

DELETE FROM REGISTERATIONS WHERE Course_Id = Course_Id


SELECT DEPARTMENT.Name AS Dept_Code , Course_Code, SECTION.Name AS Section_Name, Instructor_Id FROM SECTION 
INNER JOIN OFFEREDCOURSE ON SECTION.Course_Id = OFFEREDCOURSE.OfferCourse_Id
INNER JOIN COURSE ON COURSE.Course_Id = OFFEREDCOURSE.Course_Id
INNER JOIN DEPARTMENT ON DEPARTMENT.Dept_Id = OFFEREDCOURSE.Dept_Id
select * from offeredcourse

SELECT DEPARTMENT.Name AS Dept_Code, Course_Code, SECTION.Name AS Section_Name,
CONCAT(First_name, ' ', Last_Name) AS Instructor_Name FROM SECTION
INNER JOIN OFFEREDCOURSE ON SECTION.Course_Id = OFFEREDCOURSE.OfferCourse_Id 
INNER JOIN COURSE ON COURSE.Course_Id = OFFEREDCOURSE.Course_Id
INNER JOIN DEPARTMENT ON DEPARTMENT.Dept_Id = OFFEREDCOURSE.Dept_Id
INNER JOIN CAMPUS ON CAMPUS.Campus_Id = DEPARTMENT.Campus_Id
LEFT JOIN USERS ON USERS.User_Id = SECTION.Instructor_Id
WHERE Semester = 'Spring 2020' AND CAMPUS.Campus_Id = (
SELECT Campus_Id FROM ADMIN WHERE User_Id = 1)

INSERT INTO SECTION (Course_Id, Name, Semester) VALUES 
(13, 'A', 'Spring 2020'),
(13, 'B', 'Spring 2020'),
(13, 'C', 'Spring 2020'),
(13, 'D', 'Spring 2020');
        <div class="COTxtBoxes">
            <div style="width: 30%">
                <asp:Label ID="DaySelectlabel" runat="server" Text="Day" Width="40px"></asp:Label>
                <asp:DropDownList ID="DaySelect" runat="server" Width="80%"></asp:DropDownList>
            </div>
            <div style="width: 30%">
                <asp:Label ID="MonthSelectlabel" runat="server" Text="Month" Width="60px"></asp:Label>
                <asp:DropDownList ID="MonthSelect" runat="server" Width="70%"></asp:DropDownList>
            </div>
            <div style="width: 30%">
                <asp:Label ID="Label1" runat="server" Text="Semester" Width="80px"></asp:Label>
                <asp:DropDownList ID="DropDownList1" runat="server" Width="30%"></asp:DropDownList>
        </div>

SELECT USERS.User_Id, First_Name, Last_Name FROM USERS 
INNER JOIN USERACCOUNT ON USERACCOUNT.User_Id = USERS.User_Id
WHERE USERACCOUNT.Type = 'F'

SELECT CONCAT(First_name, ' ', Last_Name) AS Instructor_Name FROM USERS
INNER JOIN USERACCOUNT ON USERS.User_Id = USERACCOUNT.User_Id 
LEFT JOIN SECTION ON SECTION.Instructor_Id = USERS.User_Id
WHERE Type = 'F'
GROUP BY Instructor_Id, First_name, Last_name
HAVING COUNT(Instructor_Id) < 3 


SELECT Course_Id, TotalSeats, COUNT(Course_Id) TotalRegistered FROM REGISTERATION
WHERE Semester = 'Spring 2020' GROUP BY Course_Id, Dept_Id

select * from REGISTERATION

/*List of Queries to Fix

Course Registeration:
	Showing all Registerable courses 


SELECT Section_Id, Name FROM SECTION WHERE Semester = '" + Semester + "';";

*/