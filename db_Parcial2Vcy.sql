CREATE DATABASE Parcial2Vcy
GO
USE master
GO
CREATE LOGIN usrparcial2 WITH PASSWORD=N'12345678',
	DEFAULT_DATABASE=Parcial2Vcy,
	CHECK_EXPIRATION=OFF,
	CHECK_POLICY=ON
GO
USE Parcial2Vcy
GO
CREATE USER usrparcial2 FOR LOGIN usrparcial2
GO
ALTER ROLE [db_owner] ADD MEMBER usrparcial2
GO

DROP TABLE IF EXISTS HorarioPrograma;
DROP TABLE IF EXISTS Canal;


CREATE TABLE Canal (
	id INT PRIMARY KEY IDENTITY(1,1),
	nombre VARCHAR(50) NOT NULL,
    frecuencia VARCHAR(20) NOT NULL,
    estado SMALLINT NOT NULL DEFAULT 1
);

CREATE TABLE HorarioPrograma(
	id INT PRIMARY KEY IDENTITY(1,1),
    idCanal INT NOT NULL,
    titulo VARCHAR(100) NOT NULL,
    horaInicio TIME NOT NULL,
    horaFin TIME NOT NULL,
    diaSemana VARCHAR(20) NOT NULL,
    conductorPrincipal VARCHAR(100) NOT NULL,
    estado SMALLINT NOT NULL DEFAULT 1,
    CONSTRAINT fk_HorarioPrograma_Canal FOREIGN KEY(idCanal) REFERENCES Canal(id)
);

GO
DROP PROC IF EXISTS paHorarioProgramaListar;
GO
CREATE PROC paHorarioProgramaListar @parametro VARCHAR(50)
AS
    SELECT hp.id, hp.idCanal, c.nombre AS canal, c.frecuencia , hp.titulo, hp.horaInicio, hp.diaSemana, hp.conductorPrincipal, hp.estado
    FROM HorarioPrograma hp
    INNER JOIN Canal c ON c.id = hp.idCanal
    WHERE hp.estado = 1 AND hp.titulo + hp.diaSemana + hp.conductorPrincipal + c.nombre LIKE '%' + REPLACE(@parametro, ' ', '%') + '%'
    ORDER BY hp.titulo;
GO
EXEC paHorarioProgramaListar 'atb';


--DML
INSERT INTO Canal (nombre, frecuencia, estado) 
VALUES
('Red Uno',       'Canal 11 VHF',  1),
('Unitel',        'Canal 13 VHF',  1),
('ATB',           'Canal 9 VHF',   1),
('Bolivisión',    'Canal 5 VHF',   1);

INSERT INTO HorarioPrograma (idCanal, titulo, horaInicio, horaFin, diaSemana, conductorPrincipal, estado)
VALUES
(1, 'El Mañanero', '06:00:00', '09:30:00', 'Lunes','Luisito Comunica (invitado)', 1),
(1, 'El Mañanero', '06:00:00', '09:30:00', 'Martes', 'Equipo Red Uno',    1),
(1, 'El Mañanero', '06:00:00', '09:30:00', 'Miércoles', 'Equipo Red Uno',    1),
(1, 'El Mañanero', '06:00:00', '09:30:00', 'Jueves','Equipo Red Uno',    1),
(1, 'El Mañanero', '06:00:00', '09:30:00', 'Viernes', 'Equipo Red Uno',    1),

(2, 'Telepaís', '19:00:00', '20:30:00', 'Lunes','Carolina Córdova',  1),
(2, 'Telepaís', '19:00:00', '20:30:00', 'Martes', 'Carolina Córdova',  1),
(2, 'Telepaís', '19:00:00', '20:30:00', 'Miércoles', 'Carolina Córdova',  1),
(2, 'Telepaís', '19:00:00', '20:30:00', 'Jueves',  'Carolina Córdova',  1),
(2, 'Telepaís', '19:00:00', '20:30:00', 'Viernes', 'Carolina Córdova',  1),

(3, 'La Mañana de Todos','06:30:00', '09:30:00', 'Lunes', 'Equipo ATB',        1),
(3, 'La Mañana de Todos','06:30:00', '09:30:00', 'Martes', 'Equipo ATB',        1),
(3, 'La Mañana de Todos', '06:30:00', '09:30:00', 'Miércoles', 'Equipo ATB',        1),
(3, 'La Mañana de Todos', '06:30:00', '09:30:00', 'Jueves', 'Equipo ATB',        1),
(3, 'La Mañana de Todos', '06:30:00', '09:30:00', 'Viernes', 'Equipo ATB',        1),

(4, 'Noticiero Bolivisión', '19:00:00', '20:00:00', 'Lunes', 'Equipo Bolivisión', 1),
(4, 'Noticiero Bolivisión', '19:00:00', '20:00:00', 'Martes', 'Equipo Bolivisión', 1),
(4, 'Noticiero Bolivisión', '19:00:00', '20:00:00', 'Miércoles', 'Equipo Bolivisión', 1),
(4, 'Noticiero Bolivisión',  '19:00:00', '20:00:00', 'Jueves', 'Equipo Bolivisión', 1),
(4, 'Noticiero Bolivisión', '19:00:00', '20:00:00', 'Viernes', 'Equipo Bolivisión', 1);

SELECT * FROM Canal;
SELECT * FROM HorarioPrograma;
