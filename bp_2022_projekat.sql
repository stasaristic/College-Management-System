-- phpMyAdmin SQL Dump
-- version 5.0.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Feb 09, 2022 at 10:31 PM
-- Server version: 10.4.17-MariaDB
-- PHP Version: 8.0.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `bp_2022_projekat`
--

-- --------------------------------------------------------

--
-- Table structure for table `fakultet`
--

CREATE TABLE `fakultet` (
  `idFakultet` int(11) NOT NULL,
  `nazivFakultet` varchar(45) NOT NULL,
  `adresaFakultet` varchar(45) NOT NULL,
  `idUniverzitet` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `fakultet`
--

INSERT INTO `fakultet` (`idFakultet`, `nazivFakultet`, `adresaFakultet`, `idUniverzitet`) VALUES
(1, 'Fakultet inzenjerskih nauka', 'Sestre Janic 6', 1);

-- --------------------------------------------------------

--
-- Table structure for table `grad`
--

CREATE TABLE `grad` (
  `ptt` int(11) NOT NULL,
  `naziv` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `grad`
--

INSERT INTO `grad` (`ptt`, `naziv`) VALUES
(11000, 'Beograd'),
(34000, 'Kragujevac'),
(18000, 'Nis'),
(21000, 'Novi Sad');

-- --------------------------------------------------------

--
-- Table structure for table `ispit`
--

CREATE TABLE `ispit` (
  `idPredavac` int(11) NOT NULL,
  `idStudent` int(11) NOT NULL,
  `idPredmet` int(11) NOT NULL,
  `datum` date NOT NULL,
  `ocena` int(11) NOT NULL DEFAULT 5,
  `osvojeniPoeni` int(11) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `predaje`
--

CREATE TABLE `predaje` (
  `idPredavac` int(11) NOT NULL,
  `idPredmet` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `predavac`
--

CREATE TABLE `predavac` (
  `idPredavac` int(11) NOT NULL,
  `imePredavac` varchar(45) NOT NULL,
  `prezimePredavac` varchar(45) NOT NULL,
  `jmbgPredavac` varchar(45) NOT NULL,
  `datumRodjenjaPredavac` date NOT NULL,
  `emailPredavac` varchar(30) NOT NULL,
  `polPredavac` varchar(10) NOT NULL,
  `adresaPredavac` varchar(45) NOT NULL,
  `titula` varchar(45) NOT NULL,
  `ptt` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `predmet`
--

CREATE TABLE `predmet` (
  `idPredmet` int(11) NOT NULL,
  `nazivPredmet` varchar(45) NOT NULL,
  `brEspb` int(11) NOT NULL,
  `idSmer` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `slusa`
--

CREATE TABLE `slusa` (
  `idStudent` int(11) NOT NULL,
  `idPredmet` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `smer`
--

CREATE TABLE `smer` (
  `idSmer` int(11) NOT NULL,
  `nazivSmer` varchar(45) NOT NULL,
  `stepenObr` int(11) NOT NULL,
  `idFakultet` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `smer`
--

INSERT INTO `smer` (`idSmer`, `nazivSmer`, `stepenObr`, `idFakultet`) VALUES
(15, 'rtsi', 240, 1),
(16, 'masinski', 240, 1),
(17, 'industrijsko', 180, 1),
(18, 'urbano', 180, 1);

-- --------------------------------------------------------

--
-- Table structure for table `student`
--

CREATE TABLE `student` (
  `idStudent` int(11) NOT NULL,
  `imeStudent` varchar(45) NOT NULL,
  `prezimeStudent` varchar(45) NOT NULL,
  `jmbgStudent` int(11) NOT NULL,
  `emailStudent` varchar(30) NOT NULL,
  `polStudent` varchar(10) NOT NULL,
  `datumRodjenjaStudent` date NOT NULL,
  `adresaStudent` varchar(45) NOT NULL,
  `godUpisa` int(11) NOT NULL DEFAULT 2000,
  `ostvareniEspb` int(11) DEFAULT 0,
  `ptt` int(11) NOT NULL,
  `idSmer` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `ucionica`
--

CREATE TABLE `ucionica` (
  `idUcionica` int(11) NOT NULL,
  `kapacitet` int(11) NOT NULL,
  `tip` varchar(45) DEFAULT 'standardna',
  `idFakultet` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `univerzitet`
--

CREATE TABLE `univerzitet` (
  `idUniverzitet` int(11) NOT NULL,
  `ptt` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `univerzitet`
--

INSERT INTO `univerzitet` (`idUniverzitet`, `ptt`) VALUES
(1, 34000);

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `userId` int(11) NOT NULL,
  `username` varchar(30) NOT NULL,
  `password` varchar(30) NOT NULL,
  `email` varchar(30) NOT NULL,
  `imeUser` varchar(30) NOT NULL,
  `prezimeUser` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`userId`, `username`, `password`, `email`, `imeUser`, `prezimeUser`) VALUES
(1, 'admin', 'password', 'admin@gmail.com', 'admin', 'adminic');

-- --------------------------------------------------------

--
-- Table structure for table `zaposljava`
--

CREATE TABLE `zaposljava` (
  `idFakultet` int(11) NOT NULL,
  `idPredavac` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `fakultet`
--
ALTER TABLE `fakultet`
  ADD PRIMARY KEY (`idFakultet`),
  ADD UNIQUE KEY `idFakultet_UNIQUE` (`idFakultet`),
  ADD UNIQUE KEY `nazivFakultet_UNIQUE` (`nazivFakultet`),
  ADD KEY `fk_Fakultet_Univerzitet1_idx` (`idUniverzitet`);

--
-- Indexes for table `grad`
--
ALTER TABLE `grad`
  ADD PRIMARY KEY (`ptt`),
  ADD UNIQUE KEY `ptt_UNIQUE` (`ptt`),
  ADD UNIQUE KEY `naziv_UNIQUE` (`naziv`);

--
-- Indexes for table `ispit`
--
ALTER TABLE `ispit`
  ADD PRIMARY KEY (`idStudent`,`idPredmet`),
  ADD KEY `fk_predaje_has_slusa_slusa1_idx` (`idStudent`,`idPredmet`),
  ADD KEY `fk_predaje_has_slusa_predaje1_idx` (`idPredavac`);

--
-- Indexes for table `predaje`
--
ALTER TABLE `predaje`
  ADD PRIMARY KEY (`idPredavac`,`idPredmet`),
  ADD KEY `fk_Profesor_has_Predmet_Predmet1_idx` (`idPredmet`),
  ADD KEY `fk_Profesor_has_Predmet_Profesor1_idx` (`idPredavac`);

--
-- Indexes for table `predavac`
--
ALTER TABLE `predavac`
  ADD PRIMARY KEY (`idPredavac`),
  ADD UNIQUE KEY `idPredavac_UNIQUE` (`idPredavac`),
  ADD UNIQUE KEY `jmbg_UNIQUE` (`jmbgPredavac`),
  ADD UNIQUE KEY `adresa_UNIQUE` (`adresaPredavac`),
  ADD KEY `fk_Predavac_Grad_idx` (`ptt`);

--
-- Indexes for table `predmet`
--
ALTER TABLE `predmet`
  ADD PRIMARY KEY (`idPredmet`),
  ADD UNIQUE KEY `idPredmet_UNIQUE` (`idPredmet`),
  ADD UNIQUE KEY `nazivPredmet_UNIQUE` (`nazivPredmet`),
  ADD KEY `fk_Predmet_Smer1_idx` (`idSmer`);

--
-- Indexes for table `slusa`
--
ALTER TABLE `slusa`
  ADD PRIMARY KEY (`idStudent`,`idPredmet`),
  ADD KEY `fk_Student_has_Predmet_Predmet1_idx` (`idPredmet`),
  ADD KEY `fk_Student_has_Predmet_Student1_idx` (`idStudent`);

--
-- Indexes for table `smer`
--
ALTER TABLE `smer`
  ADD PRIMARY KEY (`idSmer`),
  ADD UNIQUE KEY `idSmer_UNIQUE` (`idSmer`),
  ADD UNIQUE KEY `nazivSmer_UNIQUE` (`nazivSmer`),
  ADD KEY `fk_Smer_Fakultet1_idx` (`idFakultet`);

--
-- Indexes for table `student`
--
ALTER TABLE `student`
  ADD PRIMARY KEY (`idStudent`),
  ADD UNIQUE KEY `jmb_UNIQUE` (`jmbgStudent`),
  ADD UNIQUE KEY `adresa_UNIQUE` (`adresaStudent`),
  ADD UNIQUE KEY `idStudent_UNIQUE` (`idStudent`),
  ADD UNIQUE KEY `emailStudent` (`emailStudent`),
  ADD KEY `fk_Student_Grad_idx` (`ptt`),
  ADD KEY `fk_Student_Smer1_idx` (`idSmer`);

--
-- Indexes for table `ucionica`
--
ALTER TABLE `ucionica`
  ADD PRIMARY KEY (`idUcionica`),
  ADD UNIQUE KEY `idUcionica_UNIQUE` (`idUcionica`),
  ADD KEY `fk_Ucionica_Fakultet1_idx` (`idFakultet`);

--
-- Indexes for table `univerzitet`
--
ALTER TABLE `univerzitet`
  ADD PRIMARY KEY (`idUniverzitet`),
  ADD UNIQUE KEY `idUniverzitet_UNIQUE` (`idUniverzitet`),
  ADD KEY `fk_Univerzitet_Grad1_idx` (`ptt`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`userId`);

--
-- Indexes for table `zaposljava`
--
ALTER TABLE `zaposljava`
  ADD PRIMARY KEY (`idFakultet`,`idPredavac`),
  ADD KEY `fk_Fakultet_has_Predavac_Predavac1_idx` (`idPredavac`),
  ADD KEY `fk_Fakultet_has_Predavac_Fakultet1_idx` (`idFakultet`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `fakultet`
--
ALTER TABLE `fakultet`
  MODIFY `idFakultet` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `grad`
--
ALTER TABLE `grad`
  MODIFY `ptt` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=550602;

--
-- AUTO_INCREMENT for table `predavac`
--
ALTER TABLE `predavac`
  MODIFY `idPredavac` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `predmet`
--
ALTER TABLE `predmet`
  MODIFY `idPredmet` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;

--
-- AUTO_INCREMENT for table `smer`
--
ALTER TABLE `smer`
  MODIFY `idSmer` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- AUTO_INCREMENT for table `student`
--
ALTER TABLE `student`
  MODIFY `idStudent` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=27;

--
-- AUTO_INCREMENT for table `ucionica`
--
ALTER TABLE `ucionica`
  MODIFY `idUcionica` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `univerzitet`
--
ALTER TABLE `univerzitet`
  MODIFY `idUniverzitet` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `user`
--
ALTER TABLE `user`
  MODIFY `userId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `fakultet`
--
ALTER TABLE `fakultet`
  ADD CONSTRAINT `fk_Fakultet_Univerzitet1` FOREIGN KEY (`idUniverzitet`) REFERENCES `univerzitet` (`idUniverzitet`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `ispit`
--
ALTER TABLE `ispit`
  ADD CONSTRAINT `fk_predaje_has_slusa_predaje1` FOREIGN KEY (`idPredavac`) REFERENCES `predaje` (`idPredavac`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_predaje_has_slusa_slusa1` FOREIGN KEY (`idStudent`,`idPredmet`) REFERENCES `slusa` (`idStudent`, `idPredmet`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `predaje`
--
ALTER TABLE `predaje`
  ADD CONSTRAINT `fk_Predavac_has_Predmet_Predavac1` FOREIGN KEY (`idPredavac`) REFERENCES `predavac` (`idPredavac`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_Predavac_has_Predmet_Predmet1` FOREIGN KEY (`idPredmet`) REFERENCES `predmet` (`idPredmet`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `predavac`
--
ALTER TABLE `predavac`
  ADD CONSTRAINT `fk_Predavac_Grad` FOREIGN KEY (`ptt`) REFERENCES `grad` (`ptt`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `predmet`
--
ALTER TABLE `predmet`
  ADD CONSTRAINT `fk_Predmet_Smer1` FOREIGN KEY (`idSmer`) REFERENCES `smer` (`idSmer`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `slusa`
--
ALTER TABLE `slusa`
  ADD CONSTRAINT `fk_Student_has_Predmet_Predmet1` FOREIGN KEY (`idPredmet`) REFERENCES `predmet` (`idPredmet`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_Student_has_Predmet_Student1` FOREIGN KEY (`idStudent`) REFERENCES `student` (`idStudent`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `smer`
--
ALTER TABLE `smer`
  ADD CONSTRAINT `fk_Smer_Fakultet1` FOREIGN KEY (`idFakultet`) REFERENCES `fakultet` (`idFakultet`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `student`
--
ALTER TABLE `student`
  ADD CONSTRAINT `fk_Student_Grad` FOREIGN KEY (`ptt`) REFERENCES `grad` (`ptt`) ON DELETE NO ACTION ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_Student_Smer1` FOREIGN KEY (`idSmer`) REFERENCES `smer` (`idSmer`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `ucionica`
--
ALTER TABLE `ucionica`
  ADD CONSTRAINT `fk_Ucionica_Fakultet1` FOREIGN KEY (`idFakultet`) REFERENCES `fakultet` (`idFakultet`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `univerzitet`
--
ALTER TABLE `univerzitet`
  ADD CONSTRAINT `fk_Univerzitet_Grad1` FOREIGN KEY (`ptt`) REFERENCES `grad` (`ptt`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `zaposljava`
--
ALTER TABLE `zaposljava`
  ADD CONSTRAINT `fk_Fakultet_has_Predavac_Fakultet1` FOREIGN KEY (`idFakultet`) REFERENCES `fakultet` (`idFakultet`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_Fakultet_has_Predavac_Predavac1` FOREIGN KEY (`idPredavac`) REFERENCES `predavac` (`idPredavac`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
