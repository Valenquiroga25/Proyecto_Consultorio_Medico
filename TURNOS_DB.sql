
CREATE DATABASE TURNOS_DB

USE TURNOS_DB

CREATE TABLE ACCESO(
	idAcceso INT primary key identity not null,
	nombreUsuario VARCHAR(50) not null,
	contrasenia VARCHAR(50) not null
	)

CREATE TABLE PACIENTE(
    idPaciente INT IDENTITY,
    nombreCompleto VARCHAR(50) NOT NULL,
	documento VARCHAR(8) NOT NULL PRIMARY KEY,
	fechaNacimiento DATE,
	obraSocial VARCHAR(50),
	telefono VARCHAR(10),
	direccion VARCHAR(50),
	correo VARCHAR(100)
	)

CREATE TABLE HISTORIA_CLINICA(
	idHistoria INT IDENTITY PRIMARY KEY,
	documentoPaciente varchar(8) NOT NULL,
	CONSTRAINT FK_HISTORIA_PACIENTE FOREIGN KEY (documentoPaciente) REFERENCES PACIENTE(documento)
	)

CREATE TABLE ITEM_HISTORIA(
	idItem INT IDENTITY PRIMARY KEY,
	idHistoria INT NOT NULL,
	descripcion varchar(5000),
	imagen varbinary(max),
	CONSTRAINT FK_HISTORIA FOREIGN KEY(idHistoria) REFERENCES HISTORIA_CLINICA(idHistoria)
	)
	
CREATE TABLE TURNO(
	idTurno INT NOT NULL IDENTITY PRIMARY KEY,
	documentoPaciente VARCHAR(8) NOT NULL,
	fecha Date NOT NULL,
	descripcion varchar(50),
	CONSTRAINT FK_PACIENTE FOREIGN KEY (documentoPaciente) REFERENCES PACIENTE(documento),
	)
	
CREATE TABLE TURNOSLOT(
    idTurnoSlot INT NOT NULL IDENTITY PRIMARY KEY,
	hora TIME NOT NULL,
	fecha DATE NOT NULL,
	idTurno INT
	CONSTRAINT FK_TURNO FOREIGN KEY (idTurno) REFERENCES TURNO(idTurno)
)

DROP TABLE TURNO

