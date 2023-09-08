USE [master]
GO

CREATE DATABASE [docmed]
GO

USE [docmed]
GO

CREATE TABLE [dbo].[roles] ( 
idRol int identity(1,1) primary key, 
nombreRol varchar(20) not null, 
);
 
CREATE TABLE [dbo].[departamentos] ( 
idDepartamento int identity(1,1) primary key not null 
, nombreDepartamento varchar(50) not null
, descripcion varchar(50)
);
 
CREATE TABLE [dbo].[usuarios] ( 
idUsuario int identity(1,1) primary key, 
cedula bigint, 
genero varchar(15),
nombre varchar(50) , 
primerApellido varchar(50) , 
segundoApellido varchar(50) , 
correo varchar(50) not null, 
contrasena varchar(50) not null, 
idRol int , 
activo bit, 
idDepartamento int, 
foreign key(idRol) references roles(idRol), 
foreign key (idDepartamento) references departamentos(idDepartamento) 
);
 
CREATE TABLE [dbo].[informacionPsicologos] ( 
idInformacionPsicologo int identity(1,1) primary key
, idUsuario int
, foto varchar(50)
, universidad varchar(50)
, gradoAcademico varchar(15)
, descripcion varchar(MAX)
foreign key(idUsuario) references usuarios (idUsuario) 
);

CREATE TABLE [dbo].[errores] ( 
idError int identity(1,1) primary key, 
descripcion varchar(50) , 
fecha datetime , 
idUsuario int, 
foreign key(idUsuario) references usuarios (idUsuario) 
); 
 
CREATE TABLE [dbo].[citas] ( 
idCita int identity(1,1) primary key, 
idPaciente int not null,
asunto varchar(50) not null, 
nombrePaciente varchar(100) not null,
fecha datetime not null, 
departamento int not null,
idPsicologo int not null,
numeroTelefonico varchar(50) not null, 
correoPaciente varchar(50) not null, 
comentarioDeSesion varchar(50) null, 
realizada bit,
codigoCita varchar(10) not null, 
foreign key(idPaciente) references usuarios (idUsuario),
foreign key(departamento) references departamentos (idDepartamento),  
foreign key(idPsicologo) references usuarios (idUsuario) 
); 
 
CREATE TABLE [dbo].[comentarios] (  
idComentario int identity(1,1) primary key not null,
idPaciente int not null,
fecha datetime not null ,
asunto varchar(50) not null,
comentario varchar(50) not null,
foreign key(idPaciente) references usuarios (idUsuario)  
); 

CREATE TABLE categoria(
idCategoria int identity(1,1) primary key not null
, nombre varchar(30) not null
);

CREATE TABLE blog(
idBlog int identity(1,1) primary key not null
, idUsuario int not null
, idCategoria int not null
, titulo nvarchar(50) not null
, descripcion nvarchar(max) not null
, fecha date not null
, foto varchar(30) not null
, foreign key (idUsuario) references usuarios(idUsuario)
, foreign key (idCategoria) references categoria(idCategoria)
);

INSERT INTO [dbo].[categoria]
			([nombre])
		VALUES
			('Salud'),('Bienestar'),('Redes sociales');
GO

INSERT INTO [dbo].[roles]
           ([nombreRol])
     VALUES
           ('Administrador'), ('Psicólogo'), ('Paciente');
GO

INSERT INTO [dbo].[departamentos]
           ([nombreDepartamento])
     VALUES
           ('Guía mental'), ('Depresión'), ('Familiar y Pareja');
GO

INSERT INTO [dbo].[usuarios]
           ([cedula]
		   ,[genero]
           ,[nombre]
           ,[primerApellido]
           ,[segundoApellido]
           ,[correo]
           ,[contrasena]
           ,[idRol]
           ,[activo])
     VALUES
           (104594337
		   ,'Masculino'
           ,'Dr. Carlos'
           ,'Salas'
           ,'Romero'
           ,'carlosromero@gmail.com'
           ,'12345'
           ,2
           ,1),
		(607834552
		,'Femenino'
           ,'Dra. Andrea'
           ,'Ramírez'
           ,'Chacón'
           ,'andreachacon@gmail.com'
           ,'12345'
           ,2
           ,1),
		(306499117
		   ,'Masculino'
           ,'Dr. Alejandro'
           ,'Gomez'
           ,'Morales'
           ,'carlosromero@gmail.com'
           ,'12345'
           ,2
           ,1),
		(507190412
		   ,'Masculino'
           ,'Dr. Brandon'
           ,'Gutierrez'
           ,'Sanchez'
           ,'brandong@gmail.com'
           ,'12345'
           ,2
           ,1)
GO

INSERT INTO [dbo].[blog]
           ([idUsuario]
           ,[idCategoria]
           ,[titulo]
           ,[descripcion]
           ,[fecha]
           ,[foto])
     VALUES
           (4
           ,1
           ,'Google inks pact for new 35-storey office'
           ,'That dominion stars lights dominion divide years for fourth have do not stars is that he earth it first without heaven in place seed it second morning saying.'
           ,'2023-04-22'
           ,'single_blog_1.png')
GO

-- STORED PROCEDURES --

CREATE PROCEDURE [dbo].[ValidarUsuario] (
@correo varchar(50)
, @contrasena varchar(50)
)
AS BEGIN
SELECT idUsuario
, cedula
, nombre +' '+ primerApellido +' '+ segundoApellido as nombre
, correo
, idRol
, idDepartamento
FROM usuarios
WHERE correo = @correo
AND contrasena = @contrasena
AND activo = 1;
END
GO
 
CREATE PROCEDURE [dbo].[RegistrarCredenciales] (
 @correo VARCHAR(50)
, @contrasena VARCHAR(50)
)
AS BEGIN
INSERT INTO usuarios(
 correo
, contrasena
, idRol
, activo)
VALUES(
 @correo
, @contrasena
, 3
, 1);
END
GO

CREATE PROCEDURE [dbo].[ValidarCorreo] (@correo varchar(50))
AS BEGIN
SELECT correo
FROM usuarios
WHERE correo = @correo;
END
GO

CREATE PROCEDURE [dbo].[RegistrarError] (
@descripcion varchar(50)
, @idUsuario int
)
AS BEGIN
INSERT INTO [dbo].[errores]
           ([descripcion]
           ,[fecha]
           ,[idUsuario])
     VALUES
           (@descripcion
           ,GETDATE()
           ,@idUsuario);
END
GO

CREATE PROCEDURE [dbo].[RecuperarContrasena] (@correo varchar(50))
AS BEGIN
SELECT contrasena 
FROM usuarios
WHERE correo = @correo;
END
GO

--deleted
 
CREATE PROCEDURE [dbo].[AgendarCita] ( 
@idPaciente int 
, @asunto varchar(50) 
, @nombrePaciente varchar(100) 
, @fecha datetime 
, @departamento int 
, @idPsicologo int 
, @numeroTelefonico varchar(50) 
, @correoPaciente varchar(50)
, @comentarioDeSesion varchar(50) 
, @respuesta varchar(10) out
) 
AS BEGIN 
DECLARE @codigoCita varchar(10); 
SELECT @codigoCita = FLOOR(RAND() * (100000 - 1 + 1)) + 1;
INSERT INTO [dbo].[citas] 
           ([idPaciente] 
           ,[asunto] 
		,[nombrePaciente] 
          	,[fecha] 
           	,[departamento] 
		,[idPsicologo] 
		,[numeroTelefonico] 
		,[correoPaciente] 
          	,[comentarioDeSesion] 
          	,[realizada] 
		,[codigoCita]) 
     VALUES 
           (@idPaciente 
		, @asunto 
		, @nombrePaciente 
		, @fecha 
		, @departamento 
		, @idPsicologo 
		, @numeroTelefonico 
		, @correoPaciente 
		, @comentarioDeSesion 
		, 0 
		, @codigoCita); 
SET @respuesta = @codigoCita; 
END 
GO

CREATE PROCEDURE [dbo].[RegistrarUsuario]( 
@cedula BIGINT 
, @nombre VARCHAR(50) 
, @primerApellido VARCHAR(50) 
, @segundoApellido VARCHAR(50) 
, @correo VARCHAR(50) 
, @contrasena VARCHAR(50) 
, @idRol int 
, @idDepartamento int 
) 
AS BEGIN 
INSERT INTO usuarios(cedula 
, nombre 
, primerApellido 
, segundoApellido 
, correo 
, contrasena 
, idRol 
, activo 
, idDepartamento) 
VALUES(@cedula 
, @nombre 
, @primerApellido 
, @segundoApellido 
, @correo 
, @contrasena 
, @idRol
, 1 
, @idDepartamento); 
END
GO 
 
CREATE PROCEDURE [dbo].[ConsultarPsicologos] 
AS BEGIN  
SELECT US.nombre +' '+ US.primerApellido +' '+ US.segundoApellido as nombre
, PS.idInformacionPsicologo
, US.idUsuario
, PS.foto
, PS.universidad
, PS.gradoAcademico
, PS.descripcion
FROM usuarios US
LEFT JOIN informacionPsicologos PS ON US.idUsuario = PS.idUsuario
WHERE US.idRol = 2;
END 
GO 
  
CREATE PROCEDURE ConsultarPsicologo(@idPsicologo int)
AS BEGIN  
SELECT US.nombre +' '+ US.primerApellido +' '+ US.segundoApellido as nombre
, PS.idInformacionPsicologo
, US.idUsuario
, PS.foto
, PS.universidad
, PS.gradoAcademico
, PS.descripcion
FROM usuarios US
INNER JOIN informacionPsicologos PS ON US.idUsuario = PS.idUsuario
WHERE US.idUsuario = @idPsicologo;
END 
GO

CREATE PROCEDURE InsertarBlog(
@idUsuario int
, @idCategoria int
, @titulo nvarchar(50)
, @descripcion nvarchar(max)
, @foto varchar(30)
)
AS BEGIN
DECLARE @fecha date = GETDATE();
INSERT INTO [dbo].[blog]
   ([idUsuario]
   ,[idCategoria]
   ,[titulo]
   ,[descripcion]
   ,[fecha]
   ,[foto])
 VALUES
   (@idUsuario
   ,@idCategoria
   ,@titulo
   ,@descripcion
   ,@fecha
   ,@foto);
END
GO
 
CREATE PROCEDURE ConsultarBlogs
AS BEGIN
SELECT [idBlog]
  ,[idUsuario]
  ,c.nombre
  ,[titulo]
  ,[descripcion]
  ,[fecha]
  ,[foto]
  FROM [dbo].[blog] b
  inner join categoria c on b.idCategoria = c.idCategoria 
  ORDER BY fecha desc;
END
GO
 
CREATE PROCEDURE ActualizarBlog(
@idBlog int
, @titulo nvarchar(50)
, @descripcion nvarchar(max)
, @foto varchar(30)
)
AS BEGIN
DECLARE @fecha date = GETDATE();
UPDATE [dbo].[blog]
   SET [titulo] = @titulo
  ,[descripcion] = @descripcion
  ,[fecha] = @fecha
  ,[foto] = @foto
 WHERE idBlog = @idBlog;
END
GO
 
CREATE PROCEDURE EliminarBlog(@idBlog int)
AS BEGIN
DELETE FROM [dbo].[blog]
  WHERE idBlog = @idBlog;
END
GO

CREATE PROCEDURE ConsultarCategorias
AS BEGIN
 
SELECT [idCategoria]
  ,[nombre]
  FROM [dbo].[categoria]
END
GO