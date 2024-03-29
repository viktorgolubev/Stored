DROP TABLE IF EXISTS `hins`;
DROP TABLE IF EXISTS `patients`;

CREATE TABLE `patients` (
  `Id` int(10) NOT NULL AUTO_INCREMENT,
  `LastName` varchar(100) NOT NULL,
  `LastNameNormalized` varchar(100) AS (LOWER(`LastName`)) STORED,
  `FirstName` varchar(100) DEFAULT NULL,
  `FirstNameNormalized` varchar(100) AS (LOWER(`FirstName`)) STORED,
  `DateOfBirth` date DEFAULT NULL,
  `Gender` char(1) DEFAULT NULL,
  `ZipCode` varchar(12) DEFAULT NULL,
  `State` varchar(40) DEFAULT NULL,
  `City` varchar(100) DEFAULT NULL,
  `IsVerified` tinyint(1) NOT NULL DEFAULT '0',
  `IsDeleted` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  KEY `IDX_patient_FirstName` (`FirstNameNormalized`),
  KEY `IDX_patient_LastName` (`LastNameNormalized`)
  );

CREATE TABLE `hins` (
  `Id` int(10) NOT NULL AUTO_INCREMENT,
  `PatientId` int(10) NOT NULL,
  `Number` varchar(16) NOT NULL,
  `Code` varchar(4) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_hin_patient` (`PatientId`),
  CONSTRAINT `FK_hin_patient` FOREIGN KEY (`PatientId`) REFERENCES `patients` (`Id`)
  );
