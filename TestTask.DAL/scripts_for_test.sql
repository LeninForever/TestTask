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
study_group_name NVARCHAR(50) NOT NULL
teacher_id INT REFERENCES teachers(id) ON DELETE SET NULL ON UPDATE CASCADE
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

)

INSERT INTO teachers VALUES(N'������� �.�.','kalinin@yandex.ru')
                           (N'������� �.�.','baka@gmail.ru')
						   (N'������� �.�.', 'niku@mail.ru'

INSERT INTO organizations VALUES(N'OOO ���� � ������','1234567890',1)
                                (N'��� ������ � ����','6789012345',2)
						        (N'��� �����', '6789067890',3)
                                (N'��� ���','6789267391',1)

INSERT INTO employees VALUES(N'������ �.�.', 1)
                            (N'������ �.�.', 2)
							(N'������ �.�.', 3)

INSERT INTO employees VALUES(N'������ �.�',4),
							(N'������ �.�',4),
							(N'������� �.�.',1)
							
INSERT INTO courses VALUES(N'������')
                          (N'Junior frontend 100k')
						  (N'���������')

--������ ������� : ������-�������������-���������� ���������
CREATE PROCEDURE SelectGroupsTeachersAndStudentCount AS  --- 1 view
SELECT * FROM groups_view 

CREATE PROCEDURE SelectTeachers AS --- 2.1 view
SELECT * FROM teachers

CREATE PROCEDURE AddNewStudyGroup 
@groupName NVARCHAR(50),
@teacherId INT
AS
INSERT INTO study_groups VALUES(@groupName, @teacherId,NULL) --- 2.2

-- ���������� �������� ������, id, ��� ������� � ��� id
CREATE PROCEDURE GetGroupNameAndGroupTeacher --- 3.1
@groupId INT
AS
	SELECT study_group_name, fio,study_groups.id as study_group_id, teacher_id FROM study_groups INNER JOIN teachers ON teacher_id = teachers.id WHERE study_groups.id =@groupId

CREATE PROCEDURE UpdateGroupName  --3.2
	@updatedGroupId INT,
	@newStudyGroupName NVARCHAR(50)
AS
UPDATE study_groups 
SET study_group_name = @newStudyGroupName
WHERE id = @updatedGroupId

-- ����� ������: �������-����������� ��� ����������� ������
CREATE PROCEDURE SelectEmployeesAndOrgNameByGroupId --3.3
	@studyGroupId INT
	AS
	SELECT * FROM organizations_employee_groups WHERE study_group_id = @studyGroupId
	
CREATE PROCEDURE RemoveEmployeeFromStudyGroup  ---3.4
	@employeeId INT,
	@studyGroupId INT
AS
DELETE FROM study_groups_employees
WHERE employee_id = @employeeId AND study_group_id = @studyGroupId 


CREATE PROCEDURE AddEmployeeToStudyGroup --4.1
@employeeId INT,
@studyGroupId INT
AS
INSERT INTO study_groups_employees VALUES(@studyGroupId,@employeeId)


CREATE PROCEDURE GetOrganizationsAttachedToTeacher -- 
@teacherId INT
AS
SELECT * FROM organizations WHERE teacher_id = @teacherId
SELECT * FROM study_groups_employees
USE studydb
CREATE PROCEDURE GetEmployeesAttachedToOrganization
@orgId INT,
@studyGroupId INT
AS
SELECT * FROM employees WHERE id not in 
(SELECT employee_id FROM study_groups_employees WHERE study_group_id = @studyGroupId) 
AND employees.organization_id = @orgId

