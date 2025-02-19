CREATE TABLE PermissionTypes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Description NVARCHAR(255) NOT NULL
);

CREATE TABLE Permissions (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeForename NVARCHAR(100) NOT NULL,
    EmployeeSurname NVARCHAR(100) NOT NULL,
    PermissionType INT NOT NULL,
    PermissionDate DATE NOT NULL,
    FOREIGN KEY (PermissionType) REFERENCES PermissionTypes(Id)
);

![image](https://github.com/user-attachments/assets/c1d10c7e-70eb-4220-b709-f949c095f3ee)

![image](https://github.com/user-attachments/assets/a021373f-dd52-4516-952c-8edd1812591a)

![image](https://github.com/user-attachments/assets/0c4c3665-695b-478c-b324-98ad5b72176b)
