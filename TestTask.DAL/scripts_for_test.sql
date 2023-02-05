CREATE DATABASE studydb
use studydb

CREATE TABLE teachers(
id INT PRIMARY KEY IDENTITY,
fio NVARCHAR(150) NOT NULL,
email VARCHAR(100) NOT NULL,
)
CREATE TABLE courses(
id INT PRIMARY KEY IDENTITY,
course_name NVARCHAR(50) NOT NULL
)

CREATE TABLE study_groups(
id INT PRIMARY KEY IDENTITY,
study_group_name NVARCHAR(50) NOT NULL,
teacher_id INT REFERENCES teachers(id) ON DELETE SET NULL ON UPDATE CASCADE,
course_id INT REFERENCES courses(id) ON DELETE SET NULL ON UPDATE CASCADE
)

CREATE TABLE organizations(
id INT PRIMARY KEY IDENTITY,
organization_name NVARCHAR(50) NOT NULL,
inn CHAR(10) NOT NULL,
teacher_id INT REFERENCES teachers(id) ON DELETE SET NULL ON UPDATE CASCADE
)

CREATE TABLE employees(
id INT PRIMARY KEY IDENTITY,
FIO NVARCHAR(150) NOT NULL,
organization_id INT REFERENCES organizations(id) ON DELETE CASCADE ON UPDATE CASCADE
)

CREATE TABLE study_groups_employees(
id INT PRIMARY KEY IDENTITY,
study_group_id INT REFERENCES study_groups(id) ON DELETE CASCADE ON UPDATE CASCADE,
employee_id INT REFERENCES employers(id) ON DELETE CASCADE ON UPDATE CASCADE
);

go
CREATE VIEW groups_view AS
SELECT        dbo.study_groups.study_group_name, dbo.teachers.fio AS teacher_fio, COUNT(dbo.study_groups_employees.study_group_id) AS employee_count, dbo.study_groups.id AS study_group_id
FROM            dbo.study_groups INNER JOIN
                         dbo.teachers ON dbo.study_groups.teacher_id = dbo.teachers.id LEFT OUTER JOIN
                         dbo.study_groups_employees ON dbo.study_groups.id = dbo.study_groups_employees.study_group_id
GROUP BY dbo.study_groups.study_group_name, dbo.teachers.fio, dbo.study_groups.id


go
CREATE VIEW organizations_employee_groups AS
SELECT    dbo.employees.FIO, dbo.organizations.organization_name, dbo.employees.id AS employee_id, dbo.study_groups_employees.study_group_id
FROM            dbo.employees INNER JOIN
                         dbo.organizations ON dbo.employees.organization_id = dbo.organizations.id INNER JOIN
                         dbo.study_groups_employees ON dbo.employees.id = dbo.study_groups_employees.employee_id

go
INSERT INTO teachers VALUES(N'Kalinin A.M.','kalinin@yandex.ru'),
                           (N'Bakunin A.F.','baka@gmail.ru'),
						   (N'Nikulin E.O.', 'niku@mail.ru');

INSERT INTO organizations VALUES(N'OOO Roga i Kopyita','1234567890',1),
                                (N'OOO Kopyita i Roga','6789012345',2),
						        (N'OOO Chappi', '6789067890',3),
                                (N'OOO AAA','6789267391',1);

INSERT INTO employees VALUES(N'Puchkov D.Y.', 1),
                            (N'Meshkov A.M.', 2),
							(N'Chaushin L.K.', 3);

INSERT INTO employees VALUES(N'Averin E.F',4),
							(N'Shukshin M.C',4),
							(N'Lixodei Y.N.',1);
							
INSERT INTO courses VALUES(N'Tokar'),
                          (N'Junior frontend 100k'),
						  (N'FishTaker');

go
--Make selection: Group-Teacher-student count
CREATE PROCEDURE SelectGroupsTeachersAndStudentCount AS  --- 1 view
SELECT * FROM groups_view 

go
CREATE PROCEDURE SelectTeachers AS --- 2.1 view
SELECT * FROM teachers

go
CREATE PROCEDURE AddNewStudyGroup 
@groupName NVARCHAR(50),
@teacherId INT
AS
INSERT INTO study_groups VALUES(@groupName, @teacherId,NULL) --- 2.2

go
-- Return group name, id, teacher's fio and his id
CREATE PROCEDURE GetGroupNameAndGroupTeacher --- 3.1
@groupId INT
AS
	SELECT study_group_name, fio,study_groups.id as study_group_id, teacher_id FROM study_groups INNER JOIN teachers ON teacher_id = teachers.id WHERE study_groups.id =@groupId

go
CREATE PROCEDURE UpdateGroupName  --3.2
	@updatedGroupId INT,
	@newStudyGroupName NVARCHAR(50)
AS
UPDATE study_groups 
SET study_group_name = @newStudyGroupName
WHERE id = @updatedGroupId

go
-- Make selection: Employee-Organizaton for current group
CREATE PROCEDURE SelectEmployeesAndOrgNameByGroupId --3.3
	@studyGroupId INT
	AS
	SELECT * FROM organizations_employee_groups WHERE study_group_id = @studyGroupId
	
go
CREATE PROCEDURE RemoveEmployeeFromStudyGroup  ---3.4
	@employeeId INT,
	@studyGroupId INT
AS
DELETE FROM study_groups_employees
WHERE employee_id = @employeeId AND study_group_id = @studyGroupId 

go
CREATE PROCEDURE AddEmployeeToStudyGroup --4.1
@employeeId INT,
@studyGroupId INT
AS
INSERT INTO study_groups_employees VALUES(@studyGroupId,@employeeId)

go
CREATE PROCEDURE GetOrganizationsAttachedToTeacher -- 
@teacherId INT
AS
SELECT * FROM organizations WHERE teacher_id = @teacherId

go
CREATE PROCEDURE GetEmployeesAttachedToOrganization
@orgId INT,
@studyGroupId INT
AS
SELECT * FROM employees WHERE id not in 
(SELECT employee_id FROM study_groups_employees WHERE study_group_id = @studyGroupId) 
AND employees.organization_id = @orgId

