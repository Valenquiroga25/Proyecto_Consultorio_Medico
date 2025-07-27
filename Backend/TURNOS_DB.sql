CREATE DATABASE TURNOS_DB

USE TURNOS_DB

CREATE TABLE ACCESO(
    idAcceso INT primary key identity not null,
    nombreUsuario VARCHAR(50) not null,
    contrasenia VARCHAR(50) not null
)

drop table PACIENTE

CREATE TABLE PACIENTE(
    idPaciente INT IDENTITY,
    nombres VARCHAR(50) NOT NULL,
    apellidos VARCHAR(50) NOT NULL,
    documento VARCHAR(8) NOT NULL PRIMARY KEY,
    fechaNacimiento DATE,
    codArea VARCHAR(6),
    telefono VARCHAR(6),
    direccion VARCHAR(50),
    correo VARCHAR(100)
)

CREATE TABLE HISTORIA(
    idHistoria INT IDENTITY,
    documentoPaciente VARCHAR(8) NOT NULL PRIMARY KEY,
    descripcion NVARCHAR(MAX) NOT NULL,
    CONSTRAINT FK_HISTORIA_PACIENTE FOREIGN KEY (documentoPaciente) REFERENCES PACIENTE(documento)
)

CREATE TABLE itemEstudios(
    idItem INT IDENTITY,
    documentoPaciente VARCHAR(8) NOT NULL,
    fechaItem DATE NOT NULL,
    CONSTRAINT PK_ITEM_ESTUDIOS PRIMARY KEY (documentoPaciente, fechaItem),
    CONSTRAINT FK_ITEM_HISTORIA FOREIGN KEY (documentoPaciente) REFERENCES HISTORIA(documentoPaciente)
)


/*Se ponen 3 campos como primary key porque no hacemos referencia a un estudio, sino a una instancia determinada, por lo que decimos el estudio con tal nombre, de tal item, de tal historia*/
CREATE TABLE ESTUDIO(
    idEstudio INT IDENTITY,
    nombre VARCHAR(50) NOT NULL,
    documentoPaciente VARCHAR(8) NOT NULL,
    fechaItem DATE NOT NULL,
    CONSTRAINT PK_ESTUDIO PRIMARY KEY (nombre, documentoPaciente, fechaItem),
    CONSTRAINT FK_ESTUDIO_ITEM FOREIGN KEY (documentoPaciente, fechaItem) REFERENCES itemEstudios(documentoPaciente, fechaItem)
)

CREATE TABLE imagenEstudio(
    idImagen INT IDENTITY PRIMARY KEY,
    nombreEstudio VARCHAR(50) NOT NULL,
    documentoPacienteHistoria VARCHAR(8) NOT NULL,
    fechaItem DATE NOT NULL,
    titulo VARCHAR(100) NOT NULL,
    imagen VARBINARY(MAX) NOT NULL,
    CONSTRAINT FK_IMAGEN_ESTUDIO FOREIGN KEY (nombreEstudio, documentoPacienteHistoria, fechaItem) REFERENCES ESTUDIO(nombre, documentoPaciente, fechaItem)
)

CREATE TABLE TURNO(
    idTurno INT NOT NULL IDENTITY PRIMARY KEY,
    documentoPaciente VARCHAR(8) NOT NULL,
    fecha Date NOT NULL,
    descripcion varchar(50),
    CONSTRAINT FK_PACIENTE FOREIGN KEY (documentoPaciente) REFERENCES PACIENTE(documento)
)

CREATE TABLE turnoSlot(
    idTurnoSlot INT NOT NULL IDENTITY PRIMARY KEY,
    hora TIME NOT NULL,
    fecha DATE NOT NULL,
    idTurno INT
    CONSTRAINT FK_TURNO FOREIGN KEY (idTurnoSlot) REFERENCES TURNO(idTurno)
)