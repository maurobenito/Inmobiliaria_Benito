-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 29-05-2025 a las 16:12:08
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `inmob_benito`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contrato`
--

CREATE TABLE `contrato` (
  `ContratoId` int(11) NOT NULL,
  `id_inquilino` int(11) DEFAULT NULL,
  `id_inmueble` int(11) DEFAULT NULL,
  `fecha_desde` date DEFAULT NULL,
  `fecha_hasta` date DEFAULT NULL,
  `monto` decimal(10,2) DEFAULT NULL,
  `MultaPorRescision` decimal(10,2) DEFAULT NULL,
  `RescindidoAnticipadamente` tinyint(1) NOT NULL DEFAULT 0,
  `UsuarioCreacionId` int(11) DEFAULT NULL,
  `UsuarioFinalizacionId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `contrato`
--

INSERT INTO `contrato` (`ContratoId`, `id_inquilino`, `id_inmueble`, `fecha_desde`, `fecha_hasta`, `monto`, `MultaPorRescision`, `RescindidoAnticipadamente`, `UsuarioCreacionId`, `UsuarioFinalizacionId`) VALUES
(1, 1, 1, '2024-01-01', '2025-05-27', 50000.00, 50000.00, 1, NULL, NULL),
(2, 2, 3, '2024-03-01', '2025-05-27', 65000.00, 3.00, 1, NULL, NULL),
(7, 1, 1, '2025-01-01', '2025-05-27', 5000070.00, 82.00, 1, NULL, NULL),
(8, 1, 1, '2025-01-01', '2025-05-26', 5000058.00, 25802.00, 1, NULL, NULL),
(10, 1, 1, '2025-05-27', '2025-05-27', 5000058.00, 55.00, 1, NULL, NULL),
(12, 2, 15, '2026-05-27', '2025-05-27', 5000070.00, 345.00, 1, NULL, NULL),
(13, 2, 15, '2027-05-28', '2025-05-27', 5000080.00, 55.00, 1, NULL, NULL),
(19, 2, 1, '2025-05-29', '2025-05-28', 5000070.00, 5000.00, 1, NULL, NULL),
(20, 2, 1, '2026-05-31', '2025-05-28', 5000070.00, 10.00, 1, NULL, NULL),
(22, 2, 15, '2025-05-29', '2026-05-29', 500000.00, NULL, 0, NULL, NULL),
(23, 2, 15, '2026-05-30', '2027-05-29', 500000.00, NULL, 0, NULL, NULL),
(24, 1, 3, '2025-05-28', '2026-05-28', 50000.00, NULL, 0, NULL, NULL),
(25, 1, 1, '2029-05-28', '2030-05-28', 5000058.00, NULL, 0, NULL, NULL),
(26, 2, 3, '2027-05-28', '2025-05-28', 55000.00, 6000.00, 1, NULL, 3),
(27, 1, 1, '2030-05-29', '2031-05-28', 5000058.00, NULL, 0, 3, NULL);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmueble`
--

CREATE TABLE `inmueble` (
  `InmuebleId` int(11) NOT NULL,
  `IdPropietario` int(11) DEFAULT NULL,
  `id_tipo` int(11) DEFAULT NULL,
  `uso` enum('comercial','residencial') DEFAULT NULL,
  `direccion` varchar(200) DEFAULT NULL,
  `ambientes` int(11) DEFAULT NULL,
  `coordenadas` varchar(100) DEFAULT NULL,
  `precio` decimal(10,2) DEFAULT NULL,
  `Estado` varchar(50) NOT NULL DEFAULT 'Disponible'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inmueble`
--

INSERT INTO `inmueble` (`InmuebleId`, `IdPropietario`, `id_tipo`, `uso`, `direccion`, `ambientes`, `coordenadas`, `precio`, `Estado`) VALUES
(1, 1, 1, 'residencial', 'Calle Falsa 123', 3, '-34.6037,-58.3816', 1200000.00, 'Disponible'),
(3, 2, 2, 'residencial', 'Pasaje Luna 456', 4, '-34.6112,-58.3901', 180000.00, 'Disponible'),
(15, 2, 2, 'residencial', 'd ferrari 678', 4, '2545 658467', 800000.00, 'Disponible');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilino`
--

CREATE TABLE `inquilino` (
  `InquilinoId` int(11) NOT NULL,
  `nombre` varchar(100) DEFAULT NULL,
  `apellido` varchar(100) DEFAULT NULL,
  `dni` varchar(20) DEFAULT NULL,
  `telefono` varchar(20) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inquilino`
--

INSERT INTO `inquilino` (`InquilinoId`, `nombre`, `apellido`, `dni`, `telefono`, `email`) VALUES
(1, 'María', 'López', '30998877', '5555-66665', 'maria.lopez@mail.com'),
(2, 'Jorge', 'Fernández', '28777222', '7777-8888', 'jorge.fernandez@mail.com');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pago`
--

CREATE TABLE `pago` (
  `PagoId` int(11) NOT NULL,
  `id_contrato` int(11) DEFAULT NULL,
  `numero_pago` int(11) DEFAULT NULL,
  `fecha_pago` date DEFAULT NULL,
  `importe` decimal(10,2) DEFAULT NULL,
  `UsuarioCreacion` varchar(255) DEFAULT NULL,
  `UsuarioAnulacion` varchar(255) DEFAULT NULL,
  `UsuarioCreacionId` int(11) DEFAULT NULL,
  `UsuarioAnulacionId` int(11) DEFAULT NULL,
  `Anulado` tinyint(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `pago`
--

INSERT INTO `pago` (`PagoId`, `id_contrato`, `numero_pago`, `fecha_pago`, `importe`, `UsuarioCreacion`, `UsuarioAnulacion`, `UsuarioCreacionId`, `UsuarioAnulacionId`, `Anulado`) VALUES
(24, 10, 2, '2025-05-27', 820.00, 'sistema', NULL, NULL, 3, 1),
(25, 10, 3, '2025-05-27', 600.00, 'sistema', NULL, NULL, NULL, 0),
(26, 10, 4, '2025-05-27', 6000.00, 'sistema', NULL, NULL, NULL, 0),
(28, 10, 6, '2025-05-27', 0.00, 'sistema', NULL, NULL, NULL, 0),
(29, 10, 7, '2025-05-27', 500.00, 'sistema', NULL, NULL, NULL, 0),
(30, 2, 5, '2025-05-27', 3.00, 'sistema', NULL, NULL, NULL, 0),
(31, 10, 8, '2025-05-27', 55.00, 'sistema', NULL, NULL, NULL, 0),
(32, 10, 9, '2025-05-27', 55.00, 'sistema', NULL, NULL, NULL, 0),
(33, 7, 1, '2025-05-27', 82.00, 'sistema', NULL, NULL, NULL, 0),
(41, 20, 1, '2025-05-28', 10.00, NULL, NULL, NULL, NULL, 0),
(42, 2, NULL, '2025-05-28', 600000.00, NULL, NULL, NULL, 3, 1),
(44, 19, 1, '2025-05-28', 5000.00, NULL, NULL, NULL, NULL, 0),
(45, 2, NULL, '2025-05-29', 600000.00, NULL, NULL, NULL, NULL, 0),
(46, 23, NULL, '2025-05-28', 650000.00, NULL, NULL, NULL, NULL, 0),
(47, 2, NULL, '2025-05-28', 600000.00, NULL, NULL, NULL, NULL, 0),
(48, 2, NULL, '2025-05-28', 8888.00, NULL, NULL, 3, NULL, 0),
(49, 26, NULL, '2025-05-28', 60006.00, NULL, NULL, NULL, NULL, 0),
(50, 10, NULL, '2025-05-28', 6000050.00, NULL, NULL, 3, 3, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietario`
--

CREATE TABLE `propietario` (
  `PropietarioId` int(11) NOT NULL,
  `nombre` varchar(100) DEFAULT NULL,
  `apellido` varchar(100) DEFAULT NULL,
  `dni` varchar(20) DEFAULT NULL,
  `telefono` varchar(20) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `propietario`
--

INSERT INTO `propietario` (`PropietarioId`, `nombre`, `apellido`, `dni`, `telefono`, `email`) VALUES
(1, 'Carlos', 'Pérez5', '20345678', '1111-2222', 'carlos.perez@mail.com'),
(2, 'Laura', 'Gómez', '27444333', '3333-4444', 'laura.gomez@mail.com');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipo_inmueble`
--

CREATE TABLE `tipo_inmueble` (
  `TipoId` int(11) NOT NULL,
  `nombre` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tipo_inmueble`
--

INSERT INTO `tipo_inmueble` (`TipoId`, `nombre`) VALUES
(2, 'Casa'),
(1, 'Departamento'),
(4, 'Depósito'),
(3, 'Local');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE `usuario` (
  `UsuarioId` int(11) NOT NULL,
  `email` varchar(100) DEFAULT NULL,
  `password_hash` varchar(255) DEFAULT NULL,
  `rol` enum('administrador','empleado') DEFAULT NULL,
  `nombre` varchar(100) DEFAULT NULL,
  `apellido` varchar(100) DEFAULT NULL,
  `foto_perfil` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuario`
--

INSERT INTO `usuario` (`UsuarioId`, `email`, `password_hash`, `rol`, `nombre`, `apellido`, `foto_perfil`) VALUES
(1, 'admin@inmo.com', '$2a$11$D9ui1gBfrCQVOMDq1mLu3eh8XEMpU6RXkFcWPG0spIaAb7SRr94zO\r\n', 'administrador', 'Admin', 'General', NULL),
(2, 'empleado@inmo.com', '$2b$10$xyz123fakehashvalueabc', 'empleado', 'Empleado', 'Uno', NULL),
(3, 'mauro@g.com', '$2a$11$034../EafkHLVG8u.D4bOO58eq44yzNNAJATH.uYD6z1IMqGCCXmK', 'administrador', 'Mauro', 'Benito', '/uploads/868dd021-a2bc-41c3-af4a-a86d8b8784cb.jpg'),
(4, 'asd@g.com', '$2a$11$2mZejUzYRk1dIFIxYfqmiOPt0i1XifocIMcM4nuzLtU98Ade7haIK', 'empleado', 'gabi', 'asdfg', '/uploads/192842d0-3f0b-44f3-8c60-ce0b23533d0c.jpg'),
(5, 'mmmm@gmm.com', '$2a$11$ChhxDxX2Ox6OECNiFXCDGeetQsKLC3h/uYQzz2kRhRUIRiPIaKxri', 'empleado', 'María', 'asdfg', NULL),
(6, 'mmmm@gmmg.com', '$2a$11$58CoXe2zKy0C34MQoU1iAOktWjlh74zb3UIyUa0y7QQFgGBnom97u', 'administrador', 'María', 'Pérez5', NULL),
(7, 'emp@g.com', '$2a$11$09F12rAa36n8eTJ6U2TxAev3TFX2G71jJTrqisuoafjclyw917qTu', 'empleado', 'Empleado1', 'paz', '/uploads/54239b9b-ff0f-4bdc-91fc-e0b905946b9e.jpg'),
(8, 'sistema@g.com', '$2a$11$wMTM0HT5VFt92/w1zKGSPuqEGvmKKIJv3degRX80BkDh5AqFBUs7i', 'administrador', 'Sistema', 'Sistema', NULL);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD PRIMARY KEY (`ContratoId`),
  ADD KEY `id_inquilino` (`id_inquilino`),
  ADD KEY `id_inmueble` (`id_inmueble`),
  ADD KEY `FK_Contrato_UsuarioCreacion` (`UsuarioCreacionId`),
  ADD KEY `FK_Contrato_UsuarioFinalizacion` (`UsuarioFinalizacionId`);

--
-- Indices de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD PRIMARY KEY (`InmuebleId`),
  ADD KEY `id_propietario` (`IdPropietario`),
  ADD KEY `id_tipo` (`id_tipo`);

--
-- Indices de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  ADD PRIMARY KEY (`InquilinoId`),
  ADD UNIQUE KEY `dni` (`dni`);

--
-- Indices de la tabla `pago`
--
ALTER TABLE `pago`
  ADD PRIMARY KEY (`PagoId`),
  ADD KEY `id_contrato` (`id_contrato`),
  ADD KEY `FK_Pago_UsuarioCreacion` (`UsuarioCreacionId`),
  ADD KEY `FK_pago_usuario_anulacion` (`UsuarioAnulacionId`);

--
-- Indices de la tabla `propietario`
--
ALTER TABLE `propietario`
  ADD PRIMARY KEY (`PropietarioId`),
  ADD UNIQUE KEY `dni` (`dni`);

--
-- Indices de la tabla `tipo_inmueble`
--
ALTER TABLE `tipo_inmueble`
  ADD PRIMARY KEY (`TipoId`),
  ADD UNIQUE KEY `nombre` (`nombre`);

--
-- Indices de la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`UsuarioId`),
  ADD UNIQUE KEY `email` (`email`);

--
-- Indices de la tabla `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contrato`
--
ALTER TABLE `contrato`
  MODIFY `ContratoId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=28;

--
-- AUTO_INCREMENT de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `InmuebleId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `InquilinoId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT de la tabla `pago`
--
ALTER TABLE `pago`
  MODIFY `PagoId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=51;

--
-- AUTO_INCREMENT de la tabla `propietario`
--
ALTER TABLE `propietario`
  MODIFY `PropietarioId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `tipo_inmueble`
--
ALTER TABLE `tipo_inmueble`
  MODIFY `TipoId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `UsuarioId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD CONSTRAINT `FK_Contrato_UsuarioCreacion` FOREIGN KEY (`UsuarioCreacionId`) REFERENCES `usuario` (`UsuarioId`),
  ADD CONSTRAINT `FK_Contrato_UsuarioFinalizacion` FOREIGN KEY (`UsuarioFinalizacionId`) REFERENCES `usuario` (`UsuarioId`),
  ADD CONSTRAINT `contrato_ibfk_1` FOREIGN KEY (`id_inquilino`) REFERENCES `inquilino` (`Inquilinoid`),
  ADD CONSTRAINT `contrato_ibfk_2` FOREIGN KEY (`id_inmueble`) REFERENCES `inmueble` (`InmuebleId`);

--
-- Filtros para la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD CONSTRAINT `inmueble_ibfk_1` FOREIGN KEY (`IdPropietario`) REFERENCES `propietario` (`Propietarioid`),
  ADD CONSTRAINT `inmueble_ibfk_2` FOREIGN KEY (`id_tipo`) REFERENCES `tipo_inmueble` (`TipoId`);

--
-- Filtros para la tabla `pago`
--
ALTER TABLE `pago`
  ADD CONSTRAINT `FK_Pago_UsuarioAnulacion` FOREIGN KEY (`UsuarioAnulacionId`) REFERENCES `usuario` (`UsuarioId`),
  ADD CONSTRAINT `FK_Pago_UsuarioCreacion` FOREIGN KEY (`UsuarioCreacionId`) REFERENCES `usuario` (`UsuarioId`),
  ADD CONSTRAINT `FK_pago_usuario_anulacion` FOREIGN KEY (`UsuarioAnulacionId`) REFERENCES `usuario` (`UsuarioId`),
  ADD CONSTRAINT `pago_ibfk_1` FOREIGN KEY (`id_contrato`) REFERENCES `contrato` (`ContratoId`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
