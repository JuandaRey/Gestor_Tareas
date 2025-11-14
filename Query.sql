USE TaskManagerAPI;
GO

--Tabla de estudiantes
CREATE TABLE Students (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Career NVARCHAR(100) NOT NULL
);
GO

--Tabla de cursos
CREATE TABLE Courses (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Teacher NVARCHAR(100) NULL
);
GO

-- Tabla de tareas
CREATE TABLE Tasks (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Title NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255) NULL,
    DueDate DATETIME NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    StudentId INT NOT NULL,
    CourseId INT NOT NULL,
    CONSTRAINT FK_Tasks_Students FOREIGN KEY (StudentId) REFERENCES Students(Id),
    CONSTRAINT FK_Tasks_Courses FOREIGN KEY (CourseId) REFERENCES Courses(Id)
);
GO

-- Insertar estudiantes
INSERT INTO Students (FullName, Email, Career)
VALUES
('Juan Pérez', 'juan.perez@correo.com', 'Ingeniería de Software'),
('Valeria Garcés', 'valeria.garces@correo.com', 'Construcción de Software IV');

-- Insertar cursos
INSERT INTO Courses (Name, Teacher)
VALUES
('Desarrollo Web', 'Ing. López'),
('Bases de Datos', 'Ing. Ramírez');

-- Insertar tarea de ejemplo
INSERT INTO Tasks (Title, Description, DueDate, Status, StudentId, CourseId)
VALUES
('Proyecto final', 'Desarrollar la API en .NET', '2025-11-10', 'Pendiente', 2, 1);
