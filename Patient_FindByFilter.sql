DELIMITER ;;
CREATE PROCEDURE `Patient_FindByFilter`(IN FirstName VARCHAR(100), IN LastName VARCHAR(100))
    SQL SECURITY INVOKER
BEGIN
  SET @sql = CONCAT_WS(' ',
    'SELECT p.Id, p.FirstName, p.LastName, p.Gender, p.DateOfBirth, p.ZipCode, p.City, p.State',
	'FROM Patients p WHERE',
	'IsDeleted = 0',
	CONCAT('AND p.LastNameNormalized LIKE \'%', LOWER(REPLACE(LastName, '_', '\_')), '%\'' ),
	IF(FirstName IS NOT NULL AND FirstName <> '', CONCAT('AND p.FirstNameNormalized LIKE \'%', LOWER(REPLACE(FirstName, '_', '\_')), '%\''),''));

  PREPARE q FROM @sql;
  EXECUTE q;
  DEALLOCATE PREPARE q;
END ;;
DELIMITER ;