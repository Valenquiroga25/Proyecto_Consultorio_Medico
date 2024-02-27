CREATE DATABASE TURNOS_DB
USE TURNOS_DB

CREATE TABLE PACIENTE(
	idPaciente INT NOT NULL IDENTITY PRIMARY KEY,
	nombreCompleto varchar(50),
	documento int,
	obraSocial VARCHAR(50),
	fechaNacimiento date,
	telefono VARCHAR(50),
	)

CREATE TABLE CONSULTA(
	IdConsulta INT NOT NULL IDENTITY PRIMARY KEY,
	descripcion VARCHAR(200),
	precio float
	)

CREATE TABLE TURNO(
	IdTurno INT NOT NULL IDENTITY PRIMARY KEY,
	fecha Date NOT NULL,
	descripcion varchar(50) NOT NULL,
	idPaciente INT NOT NULL,
	idConsulta INT NOT NULL,
	CONSTRAINT FK_PACIENTE FOREIGN KEY (idPaciente) REFERENCES PACIENTE(idPaciente),
	CONSTRAINT FK_CONSULTA FOREIGN KEY (idConsulta) REFERENCES CONSULTA(IdConsulta),
	)

CREATE TABLE ACCESO(
	idAcceso int primary key identity not null,
	nombreUsuario varchar(50) not null,
	contrase�a varchar(50) not null
	)

select * from PACIENTE
select * from CONSULTA
select * from TURNO
select * from ACCESO
insert into ACCESO (nombreUsuario,contrase�a) values('Valentin','contrase�a');

drop table ACCESO


drop table PACIENTE