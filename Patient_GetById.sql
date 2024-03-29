DELIMITER ;;
CREATE PROCEDURE `Patient_GetById`(IN Id int)
    SQL SECURITY INVOKER
BEGIN
	SELECT
		p.Id,
		p.FirstName,
		p.LastName,
		p.Gender,
		p.DateOfBirth,
		p.ZipCode,
		p.City,
		p.State
	FROM Patients p
	WHERE p.Id = Id;
	
	SELECT
		h.Number,
		h.Code
	FROM Hins h
	WHERE h.PatientId = Id;
END ;;
DELIMITER ;