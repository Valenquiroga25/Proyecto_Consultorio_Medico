create database TURNOS_DB

-- ACCESO
CREATE TABLE ACCESO (
    idAcceso INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nombreUsuario VARCHAR(50) NOT NULL,
    contrasenia VARCHAR(50) NOT NULL
)

-- PACIENTE
    CREATE TABLE PACIENTE (
    idPaciente INT NOT NULL AUTO_INCREMENT,
    nombres VARCHAR(50) NOT NULL,
    apellidos VARCHAR(50) NOT NULL,
    documento VARCHAR(8) NOT NULL,
    fechaNacimiento DATE,
    codArea VARCHAR(6),
    telefono VARCHAR(6),
    direccion VARCHAR(50),
    correo VARCHAR(100),
    CONSTRAINT PK_PACIENTE PRIMARY KEY (documento),
    CONSTRAINT UQ_PACIENTE_idPaciente UNIQUE (idPaciente) /*UNIQUE sirve para darle un índice al campo AUTO-INCREMENT de la tabla. Obligatorio si el autoincrementado no es PK*/
)

-- HISTORIA
CREATE TABLE HISTORIA (
    idHistoria INT NOT NULL AUTO_INCREMENT,
    documentoPaciente VARCHAR(8) NOT NULL,
    descripcion LONGTEXT NOT NULL,
    CONSTRAINT PK_HISTORIA PRIMARY KEY (idHistoria),
    CONSTRAINT FK_HISTORIA_PACIENTE FOREIGN KEY (documentoPaciente) REFERENCES PACIENTE(documento)
)

-- itemEstudios
CREATE TABLE itemEstudio (
    idItem INT NOT NULL AUTO_INCREMENT,
    documentoPaciente VARCHAR(8) NOT NULL,
    fechaItem DATE NOT NULL,
    CONSTRAINT PK_ITEM_ESTUDIOS PRIMARY KEY (documentoPaciente, fechaItem),
    CONSTRAINT UQ_itemEstudios_idItem UNIQUE (idItem),
    CONSTRAINT FK_ITEM_HISTORIA
    FOREIGN KEY (documentoPaciente) REFERENCES HISTORIA(documentoPaciente)
)

-- ESTUDIO
/* PK compuesta porque identifica nombre + item (fecha) + paciente */
CREATE TABLE ESTUDIO (
    idEstudio INT NOT NULL AUTO_INCREMENT,
    nombre VARCHAR(50) NOT NULL,
    documentoPaciente VARCHAR(8) NOT NULL,
    fecha DATE NOT NULL,
    CONSTRAINT PK_ESTUDIO PRIMARY KEY (nombre, documentoPaciente, fecha),
    CONSTRAINT UQ_ESTUDIO_idEstudio UNIQUE (idEstudio),
    CONSTRAINT FK_ESTUDIO_ITEM FOREIGN KEY (documentoPaciente, fecha) REFERENCES itemEstudio(documentoPaciente, fecha)
)

-- imagenEstudio
CREATE TABLE imagenEstudio (
    idImagen INT AUTO_INCREMENT PRIMARY KEY,
    nombreEstudio VARCHAR(50) NOT NULL,
    documentoPacienteHistoria VARCHAR(8) NOT NULL,
    fecha DATE NOT NULL,
    titulo VARCHAR(100) NOT NULL,
    imagen LONGBLOB NOT NULL,
    CONSTRAINT FK_IMAGEN_ESTUDIO FOREIGN KEY (nombreEstudio, documentoPacienteHistoria, fecha) REFERENCES ESTUDIO(nombre, documentoPaciente, fecha)
)

-- TURNO
CREATE TABLE TURNO (
    idTurno INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    documentoPaciente VARCHAR(8) NOT NULL,
    fecha DATE NOT NULL,
    descripcion VARCHAR(50),
    CONSTRAINT FK_TURNO_PACIENTE FOREIGN KEY (documentoPaciente) REFERENCES PACIENTE(documento)
)

-- turnoSlot
CREATE TABLE turnoSlot (
    idTurnoSlot INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    hora TIME NOT NULL,
    fecha DATE NOT NULL,
    idTurno INT,
    CONSTRAINT FK_turnoSlot_TURNO FOREIGN KEY (idTurno) REFERENCES TURNO(idTurno)
)
