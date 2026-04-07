-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2026. Ápr 07. 14:41
-- Kiszolgáló verziója: 10.4.32-MariaDB
-- PHP verzió: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `etteremsharp`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `etel`
--

CREATE TABLE `etel` (
  `id` int(11) NOT NULL,
  `nev` varchar(40) DEFAULT NULL,
  `allergenek` varchar(50) DEFAULT NULL,
  `kaloria` int(11) DEFAULT NULL,
  `ar` int(11) DEFAULT NULL,
  `kategoria` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `etel`
--

INSERT INTO `etel` (`id`, `nev`, `allergenek`, `kaloria`, `ar`, `kategoria`) VALUES
(6, 'Grillezett csirkemell', 'nincs', 520, 2800, 'husos'),
(7, 'Marhaburger', 'gluten, tej', 850, 3500, 'husos'),
(8, 'Rantott porkoltszelet', 'gluten, tojas', 780, 3200, 'husos'),
(9, 'BBQ marhasult', 'nincs', 900, 4200, 'husos'),
(10, 'Kacsamell narancsmartassal', 'nincs', 700, 4500, 'husos'),
(11, 'Vegan buddha bowl', 'nincs', 500, 2600, 'vegan'),
(12, 'Lencse curry', 'nincs', 650, 2400, 'vegan'),
(13, 'Grillezett zoldseg wrap', 'gluten', 550, 2200, 'vegan'),
(14, 'Quinoa salata avokadoval', 'nincs', 600, 3000, 'vegan'),
(15, 'Tofu stir fry', 'szója', 700, 2700, 'vegan');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `leadott`
--

CREATE TABLE `leadott` (
  `id` int(11) NOT NULL,
  `rendelesid` int(11) DEFAULT NULL,
  `etelid` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `leadott`
--

INSERT INTO `leadott` (`id`, `rendelesid`, `etelid`) VALUES
(1, 1, 6),
(2, 1, 7),
(3, 2, 8),
(4, 3, 7),
(5, 3, 6),
(6, 4, 7),
(7, 4, 9),
(8, 5, 6),
(9, 6, 6),
(10, 7, 10),
(11, 7, 9),
(12, 8, 7),
(13, 8, 8),
(14, 8, 9),
(15, 9, 12),
(16, 10, 6),
(17, 10, 8),
(18, 11, 11),
(19, 12, 13),
(20, 12, 14),
(21, 13, 7),
(22, 13, 10),
(23, 14, 9),
(24, 14, 12),
(25, 15, 14),
(26, 15, 8),
(27, 15, 9);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `rendelesek`
--

CREATE TABLE `rendelesek` (
  `id` int(11) NOT NULL,
  `ugyfelid` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `rendelesek`
--

INSERT INTO `rendelesek` (`id`, `ugyfelid`) VALUES
(1, 1),
(4, 1),
(8, 1),
(13, 1),
(2, 2),
(6, 2),
(11, 2),
(3, 3),
(9, 3),
(14, 3),
(5, 4),
(10, 4),
(15, 4),
(7, 5),
(12, 5);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `ugyfel`
--

CREATE TABLE `ugyfel` (
  `id` int(11) NOT NULL,
  `veznev` varchar(50) DEFAULT NULL,
  `kernev` varchar(50) DEFAULT NULL,
  `email` varchar(30) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `ugyfel`
--

INSERT INTO `ugyfel` (`id`, `veznev`, `kernev`, `email`) VALUES
(1, 'Kovacs', 'Anna', 'anna.kovacs@email.com'),
(2, 'Nagy', 'Pista', 'pista.nagy@email.com'),
(3, 'Erős', 'Pista', 'pista.eros@email.com'),
(4, 'Toth', 'Bence', 'bence.toth@email.com'),
(5, 'Kis', 'Pista', 'pista.kis@email.com');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `etel`
--
ALTER TABLE `etel`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `leadott`
--
ALTER TABLE `leadott`
  ADD PRIMARY KEY (`id`),
  ADD KEY `rendelesid` (`rendelesid`),
  ADD KEY `etelid` (`etelid`);

--
-- A tábla indexei `rendelesek`
--
ALTER TABLE `rendelesek`
  ADD PRIMARY KEY (`id`),
  ADD KEY `ugyfelid` (`ugyfelid`);

--
-- A tábla indexei `ugyfel`
--
ALTER TABLE `ugyfel`
  ADD PRIMARY KEY (`id`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `etel`
--
ALTER TABLE `etel`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT a táblához `leadott`
--
ALTER TABLE `leadott`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;

--
-- AUTO_INCREMENT a táblához `rendelesek`
--
ALTER TABLE `rendelesek`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT a táblához `ugyfel`
--
ALTER TABLE `ugyfel`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `leadott`
--
ALTER TABLE `leadott`
  ADD CONSTRAINT `leadott_ibfk_1` FOREIGN KEY (`rendelesid`) REFERENCES `rendelesek` (`id`),
  ADD CONSTRAINT `leadott_ibfk_2` FOREIGN KEY (`etelid`) REFERENCES `etel` (`id`);

--
-- Megkötések a táblához `rendelesek`
--
ALTER TABLE `rendelesek`
  ADD CONSTRAINT `rendelesek_ibfk_1` FOREIGN KEY (`ugyfelid`) REFERENCES `ugyfel` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
