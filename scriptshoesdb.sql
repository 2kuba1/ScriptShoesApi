-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: fal4.serverproject.eu:3306
-- Czas generowania: 13 Sty 2023, 10:19
-- Wersja serwera: 10.5.18-MariaDB-0+deb11u1
-- Wersja PHP: 8.0.19

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Baza danych: `scriptshoesdb`
--
CREATE DATABASE IF NOT EXISTS `scriptshoesdb` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `scriptshoesdb`;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `Cart`
--

CREATE TABLE `Cart` (
  `Id` int(11) NOT NULL,
  `UserId` int(11) NOT NULL,
  `ShoesId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `EmailCodes`
--

CREATE TABLE `EmailCodes` (
  `Id` int(11) NOT NULL,
  `GeneratedCode` longtext NOT NULL,
  `CodeCreated` datetime(6) NOT NULL,
  `CodeExpires` datetime(6) NOT NULL,
  `UserId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `Favorites`
--

CREATE TABLE `Favorites` (
  `Id` int(11) NOT NULL,
  `UserId` int(11) NOT NULL,
  `ShoesId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `Images`
--

CREATE TABLE `Images` (
  `Id` int(11) NOT NULL,
  `Img` longtext DEFAULT NULL,
  `ImgName` longtext DEFAULT NULL,
  `ShoesId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `MainImages`
--

CREATE TABLE `MainImages` (
  `Id` int(11) NOT NULL,
  `MainImg` longtext DEFAULT NULL,
  `ImageName` longtext DEFAULT NULL,
  `ShoesId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `Reviews`
--

CREATE TABLE `Reviews` (
  `Id` int(11) NOT NULL,
  `Username` longtext NOT NULL,
  `Review` longtext NOT NULL,
  `Rate` int(11) NOT NULL,
  `ReviewLikes` int(11) NOT NULL,
  `Title` longtext NOT NULL,
  `ShoesId` int(11) NOT NULL,
  `UserId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `ReviewsLikes`
--

CREATE TABLE `ReviewsLikes` (
  `Id` int(11) NOT NULL,
  `UserId` int(11) NOT NULL,
  `ShoesId` int(11) NOT NULL,
  `ReviewId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `Roles`
--

CREATE TABLE `Roles` (
  `Id` int(11) NOT NULL,
  `Name` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Zrzut danych tabeli `Roles`
--

INSERT INTO `Roles` (`Id`, `Name`) VALUES
(1, 'User'),
(2, 'Admin');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `Shoes`
--

CREATE TABLE `Shoes` (
  `Id` int(11) NOT NULL,
  `Name` longtext NOT NULL,
  `PreviousPrice` float DEFAULT NULL,
  `CurrentPrice` double NOT NULL,
  `AverageRating` double DEFAULT NULL,
  `CreatedBy` int(11) DEFAULT NULL,
  `Brand` longtext NOT NULL,
  `ReviewsNum` int(11) DEFAULT NULL,
  `ShoeType` longtext NOT NULL,
  `SizesList` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `ShoeSizes`
--

CREATE TABLE `ShoeSizes` (
  `Id` int(11) NOT NULL,
  `Sizes` longtext NOT NULL,
  `ShoesId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `Users`
--

CREATE TABLE `Users` (
  `Id` int(11) NOT NULL,
  `Username` longtext NOT NULL,
  `HashedPassword` longtext NOT NULL,
  `Email` longtext NOT NULL,
  `AvailableFounds` double NOT NULL,
  `Name` longtext NOT NULL,
  `Surname` longtext NOT NULL,
  `IsActivated` tinyint(1) NOT NULL,
  `RefreshToken` longtext NOT NULL,
  `ProfilePictureUrl` longtext NOT NULL,
  `TokenCreated` datetime(6) NOT NULL,
  `TokenExpires` datetime(6) NOT NULL,
  `RoleId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `__EFMigrationsHistory`
--

CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Zrzut danych tabeli `__EFMigrationsHistory`
--

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`) VALUES
('20230101014820_MySQL', '7.0.1');

--
-- Indeksy dla zrzutów tabel
--

--
-- Indeksy dla tabeli `Cart`
--
ALTER TABLE `Cart`
  ADD PRIMARY KEY (`Id`);

--
-- Indeksy dla tabeli `EmailCodes`
--
ALTER TABLE `EmailCodes`
  ADD PRIMARY KEY (`Id`);

--
-- Indeksy dla tabeli `Favorites`
--
ALTER TABLE `Favorites`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Favorites_ShoesId` (`ShoesId`);

--
-- Indeksy dla tabeli `Images`
--
ALTER TABLE `Images`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Images_ShoesId` (`ShoesId`);

--
-- Indeksy dla tabeli `MainImages`
--
ALTER TABLE `MainImages`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_MainImages_ShoesId` (`ShoesId`);

--
-- Indeksy dla tabeli `Reviews`
--
ALTER TABLE `Reviews`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Reviews_ShoesId` (`ShoesId`),
  ADD KEY `IX_Reviews_UserId` (`UserId`);

--
-- Indeksy dla tabeli `ReviewsLikes`
--
ALTER TABLE `ReviewsLikes`
  ADD PRIMARY KEY (`Id`);

--
-- Indeksy dla tabeli `Roles`
--
ALTER TABLE `Roles`
  ADD PRIMARY KEY (`Id`);

--
-- Indeksy dla tabeli `Shoes`
--
ALTER TABLE `Shoes`
  ADD PRIMARY KEY (`Id`);

--
-- Indeksy dla tabeli `ShoeSizes`
--
ALTER TABLE `ShoeSizes`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_ShoeSizes_ShoesId` (`ShoesId`);

--
-- Indeksy dla tabeli `Users`
--
ALTER TABLE `Users`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Users_RoleId` (`RoleId`);

--
-- Indeksy dla tabeli `__EFMigrationsHistory`
--
ALTER TABLE `__EFMigrationsHistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT dla zrzuconych tabel
--

--
-- AUTO_INCREMENT dla tabeli `Cart`
--
ALTER TABLE `Cart`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT dla tabeli `EmailCodes`
--
ALTER TABLE `EmailCodes`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT dla tabeli `Favorites`
--
ALTER TABLE `Favorites`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT dla tabeli `Images`
--
ALTER TABLE `Images`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT dla tabeli `MainImages`
--
ALTER TABLE `MainImages`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT dla tabeli `Reviews`
--
ALTER TABLE `Reviews`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT dla tabeli `ReviewsLikes`
--
ALTER TABLE `ReviewsLikes`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT dla tabeli `Roles`
--
ALTER TABLE `Roles`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT dla tabeli `Shoes`
--
ALTER TABLE `Shoes`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT dla tabeli `ShoeSizes`
--
ALTER TABLE `ShoeSizes`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT dla tabeli `Users`
--
ALTER TABLE `Users`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Ograniczenia dla zrzutów tabel
--

--
-- Ograniczenia dla tabeli `Favorites`
--
ALTER TABLE `Favorites`
  ADD CONSTRAINT `FK_Favorites_Shoes_ShoesId` FOREIGN KEY (`ShoesId`) REFERENCES `Shoes` (`Id`) ON DELETE CASCADE;

--
-- Ograniczenia dla tabeli `Images`
--
ALTER TABLE `Images`
  ADD CONSTRAINT `FK_Images_Shoes_ShoesId` FOREIGN KEY (`ShoesId`) REFERENCES `Shoes` (`Id`) ON DELETE CASCADE;

--
-- Ograniczenia dla tabeli `MainImages`
--
ALTER TABLE `MainImages`
  ADD CONSTRAINT `FK_MainImages_Shoes_ShoesId` FOREIGN KEY (`ShoesId`) REFERENCES `Shoes` (`Id`) ON DELETE CASCADE;

--
-- Ograniczenia dla tabeli `Reviews`
--
ALTER TABLE `Reviews`
  ADD CONSTRAINT `FK_Reviews_Shoes_ShoesId` FOREIGN KEY (`ShoesId`) REFERENCES `Shoes` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Reviews_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE;

--
-- Ograniczenia dla tabeli `ShoeSizes`
--
ALTER TABLE `ShoeSizes`
  ADD CONSTRAINT `FK_ShoeSizes_Shoes_ShoesId` FOREIGN KEY (`ShoesId`) REFERENCES `Shoes` (`Id`) ON DELETE CASCADE;

--
-- Ograniczenia dla tabeli `Users`
--
ALTER TABLE `Users`
  ADD CONSTRAINT `FK_Users_Roles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `Roles` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
