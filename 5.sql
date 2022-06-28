-- MySQL Script generated by MySQL Workbench
-- Tue Jun  7 01:10:33 2022
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema SAEDB
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema SAEDB
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `SAEDB` DEFAULT CHARACTER SET utf8mb4 ;
USE `SAEDB` ;

-- -----------------------------------------------------
-- Table `SAEDB`.`USER`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SAEDB`.`USER` (
  `Id` MEDIUMINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(20) NOT NULL,
  `Email` VARCHAR(30) NOT NULL,
  `PasswordHach` VARCHAR(68) NOT NULL,
  `TupeUser` ENUM('None', 'Admin', 'Scientist') NOT NULL DEFAULT 'None',
  `RegistrationDataTime` DATETIME NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `Email_UNIQUE` (`Email` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SAEDB`.`EXOPLANET_DETECTION_METHOD`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SAEDB`.`EXOPLANET_DETECTION_METHOD` (
  `Id` TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(35) NOT NULL,
  `Description` TEXT(300) NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `Name_UNIQUE` (`Name` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SAEDB`.`EXOPLANET_TYPE`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SAEDB`.`EXOPLANET_TYPE` (
  `Id` TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(35) NOT NULL,
  `Description` TEXT(300) NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `Name_UNIQUE` (`Name` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SAEDB`.`DISCOVERER`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SAEDB`.`DISCOVERER` (
  `Id` MEDIUMINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(35) NOT NULL,
  `Description` VARCHAR(300) NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `Name_UNIQUE` (`Name` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SAEDB`.`EXOPLANET`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SAEDB`.`EXOPLANET` (
  `Id` BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(35) NOT NULL,
  `Status` ENUM('Confirmed', 'NotConfirmed') NOT NULL,
  `DateTimeAdded` DATETIME NOT NULL,
  `DateTimeConfirmation` DATETIME NULL,
  `Mass` FLOAT NULL CHECK (`Mass` > 0),
  `Radius` FLOAT NULL CHECK (`Radius` > 0),
  `OrbitalRadius` FLOAT NULL CHECK (`OrbitalRadius` > 0),
  `Description` TEXT(300) NULL,
  `UserWhoAdded` MEDIUMINT UNSIGNED NULL,
  `UserWhoConfirmed` MEDIUMINT UNSIGNED NULL,
  `DetectionMethod` TINYINT UNSIGNED NULL,
  `Type` TINYINT UNSIGNED NULL,
  `Discoverer` MEDIUMINT UNSIGNED NULL,
  PRIMARY KEY (`Id`),
  INDEX `fk_EXOPLANET__USER_WHO_ADDED_idx` (`UserWhoAdded` ASC) INVISIBLE,
  UNIQUE INDEX `Exoplanet_Name_UNIQUE` (`Name` ASC) INVISIBLE,
  INDEX `fk_EXOPLANET__EXOPLANET_DETECTION_METHOD_idx` (`DetectionMethod` ASC) VISIBLE,
  INDEX `fk_EXOPLANET__EXOPLANET_TYPE_idx` (`Type` ASC) VISIBLE,
  INDEX `fk_EXOPLANET__DISCOVERER_idx` (`Discoverer` ASC) VISIBLE,
  INDEX `fk_EXOPLANET__USER_WHO_CONFIRMED_idx` (`UserWhoConfirmed` ASC) VISIBLE,
  CONSTRAINT `fk_EXOPLANET__USER_WHO_ADDED`
    FOREIGN KEY (`UserWhoAdded`)
    REFERENCES `SAEDB`.`USER` (`Id`)
    ON DELETE SET NULL
    ON UPDATE CASCADE,
  CONSTRAINT `fk_EXOPLANET__EXOPLANET_DETECTION_METHOD`
    FOREIGN KEY (`DetectionMethod`)
    REFERENCES `SAEDB`.`EXOPLANET_DETECTION_METHOD` (`Id`)
    ON DELETE SET NULL
    ON UPDATE CASCADE,
  CONSTRAINT `fk_EXOPLANET__EXOPLANET_TYPE`
    FOREIGN KEY (`Type`)
    REFERENCES `SAEDB`.`EXOPLANET_TYPE` (`Id`)
    ON DELETE SET NULL
    ON UPDATE CASCADE,
  CONSTRAINT `fk_EXOPLANET__DISCOVERER`
    FOREIGN KEY (`Discoverer`)
    REFERENCES `SAEDB`.`DISCOVERER` (`Id`)
    ON DELETE SET NULL
    ON UPDATE CASCADE,
  CONSTRAINT `fk_EXOPLANET__USER_WHO_CONFIRMED`
    FOREIGN KEY (`UserWhoConfirmed`)
    REFERENCES `SAEDB`.`USER` (`Id`)
    ON DELETE SET NULL
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SAEDB`.`STAR_TYPE`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SAEDB`.`STAR_TYPE` (
  `Id` TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(35) NOT NULL,
  `Description` TEXT(300) NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `Name_UNIQUE` (`Name` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SAEDB`.`STAR_DETECTION_METHOD`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SAEDB`.`STAR_DETECTION_METHOD` (
  `Id` TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(35) NOT NULL,
  `Description` TEXT(300) NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `Name_UNIQUE` (`Name` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SAEDB`.`STAR`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SAEDB`.`STAR` (
  `Id` BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(35) NOT NULL,
  `Status` ENUM('Confirmed', 'NotConfirmed') NOT NULL,
  `DateTimeAdded` DATETIME NOT NULL,
  `DateTimeConfirmation` DATETIME NULL,
  `Mass` FLOAT NULL CHECK (`Mass` > 0),
  `Radius` FLOAT NULL CHECK (`Radius` > 0),
  `Description` TEXT(300) NULL,
  `UserWhoAdded` MEDIUMINT UNSIGNED NULL,
  `UserWhoConfirmed` MEDIUMINT UNSIGNED NULL,
  `DetectionMethod` TINYINT UNSIGNED NULL,
  `Type` TINYINT UNSIGNED NULL,
  `Discoverer` MEDIUMINT UNSIGNED NULL,
  PRIMARY KEY (`Id`),
  INDEX `fk_STAR__STAR_TYPE_idx` (`Type` ASC) VISIBLE,
  INDEX `fk_STAR__STAR_DETECTION_METHOD_idx` (`DetectionMethod` ASC) VISIBLE,
  INDEX `fk_STAR__USER_WHO_ADDED_idx` (`UserWhoAdded` ASC) INVISIBLE,
  UNIQUE INDEX `Star_Name_UNIQUE` (`Name` ASC) INVISIBLE,
  INDEX `fk_STAR__DISCOVERER_idx` (`Discoverer` ASC) VISIBLE,
  INDEX `fk_STAR__USER_WHO_CONFIRMED_idx` (`UserWhoConfirmed` ASC) VISIBLE,
  CONSTRAINT `fk_STAR__STAR_TYPE`
    FOREIGN KEY (`Type`)
    REFERENCES `SAEDB`.`STAR_TYPE` (`Id`)
    ON DELETE SET NULL
    ON UPDATE CASCADE,
  CONSTRAINT `fk_STAR__STAR_DETECTION_METHOD`
    FOREIGN KEY (`DetectionMethod`)
    REFERENCES `SAEDB`.`STAR_DETECTION_METHOD` (`Id`)
    ON DELETE SET NULL
    ON UPDATE CASCADE,
  CONSTRAINT `fk_STAR__USER_WHO_ADDED`
    FOREIGN KEY (`UserWhoAdded`)
    REFERENCES `SAEDB`.`USER` (`Id`)
    ON DELETE SET NULL
    ON UPDATE CASCADE,
  CONSTRAINT `fk_STAR__DISCOVERER`
    FOREIGN KEY (`Discoverer`)
    REFERENCES `SAEDB`.`DISCOVERER` (`Id`)
    ON DELETE SET NULL
    ON UPDATE CASCADE,
  CONSTRAINT `fk_STAR__USER_WHO_CONFIRMED`
    FOREIGN KEY (`UserWhoConfirmed`)
    REFERENCES `SAEDB`.`USER` (`Id`)
    ON DELETE SET NULL
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SAEDB`.`STAR_AND_EXOPLANET`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SAEDB`.`STAR_AND_EXOPLANET` (
  `Star` BIGINT UNSIGNED NOT NULL,
  `Exoplane` BIGINT UNSIGNED NOT NULL,
  PRIMARY KEY (`Star`, `Exoplane`),
  INDEX `fk_STAR_AND_EXOPLANET__STAR_idx` (`Star` ASC) VISIBLE,
  INDEX `fk_STAR_AND_EXOPLANET__EXOPLANET_idx` (`Exoplane` ASC) INVISIBLE,
  CONSTRAINT `fk_STAR_AND_EXOPLANET__STAR`
    FOREIGN KEY (`Star`)
    REFERENCES `SAEDB`.`STAR` (`Id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_STAR_AND_EXOPLANET__EXOPLANET`
    FOREIGN KEY (`Exoplane`)
    REFERENCES `SAEDB`.`EXOPLANET` (`Id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SAEDB`.`RESEARCH_GROUP`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SAEDB`.`RESEARCH_GROUP` (
  `Id` MEDIUMINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(35) NOT NULL,
  `Description` TEXT(300) NULL,
  UNIQUE INDEX `Name_UNIQUE` (`Name` ASC) VISIBLE,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SAEDB`.`SESSION`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SAEDB`.`SESSION` (
  `Id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `User` MEDIUMINT UNSIGNED NOT NULL,
  `DeviceIdHash` VARCHAR(68) NOT NULL,
  INDEX `fk_SESSION_DATA__USER_idx` (`User` ASC) INVISIBLE,
  PRIMARY KEY (`Id`),
  CONSTRAINT `fk_SESSION_DATA__USER`
    FOREIGN KEY (`User`)
    REFERENCES `SAEDB`.`USER` (`Id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SAEDB`.`USER_HAS_RESEARCH_GROUP`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SAEDB`.`USER_HAS_RESEARCH_GROUP` (
  `User` MEDIUMINT UNSIGNED NOT NULL,
  `ResearchGroup` MEDIUMINT UNSIGNED NOT NULL,
  PRIMARY KEY (`User`, `ResearchGroup`),
  INDEX `fk_USER_HAS_RESEARCH_GROUP__RESEARCH_GROUP_idx` (`ResearchGroup` ASC) INVISIBLE,
  INDEX `fk_USER_HAS_RESEARCH_GROUP__USER_idx` (`User` ASC) VISIBLE,
  CONSTRAINT `fk_USER_HAS_RESEARCH_GROUP__USER`
    FOREIGN KEY (`User`)
    REFERENCES `SAEDB`.`USER` (`Id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_USER_HAS_RESEARCH_GROUP__RESEARCH_GROUP`
    FOREIGN KEY (`ResearchGroup`)
    REFERENCES `SAEDB`.`RESEARCH_GROUP` (`Id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
