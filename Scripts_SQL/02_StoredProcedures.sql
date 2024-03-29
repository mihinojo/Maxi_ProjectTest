USE [MAXI_DB]
GO
/****** Object:  StoredProcedure [dbo].[spr_AddBeneficiary]    Script Date: 2/26/2024 4:51:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Miguel Hinojo
-- Create date: 23/02/2024
-- Description:	Add Beneficiary
-- =============================================
CREATE PROCEDURE [dbo].[spr_AddBeneficiary]
(
	@FirstName NVARCHAR(100) ,
	@LastName NVARCHAR(100) ,
	@DateOfBirth datetime,
	@EmployeeId int,
	@Curp NVARCHAR(18),
	@Ssn NVARCHAR(11) ,
	@PhoneNumber NVARCHAR(10) ,
	@Nationality NVARCHAR(50),
	@ParticipationPercentage decimal(5,2) = NULL
)
AS          
SET NOCOUNT ON
BEGIN
	DECLARE @Id AS INT 
	-- RECALCULATE THE PARTICIPATION PERCENTAGE
	DECLARE @NoBeneficiaries AS INT
	SET @NoBeneficiaries = ISNULL((SELECT COUNT(1) FROM Beneficiaries WITH(NOLOCK) WHERE EmployeeId = @EmployeeId AND IsDeleted = 0),0)
	SET @ParticipationPercentage = CONVERT(decimal(5,2),100) / CONVERT(decimal(5,2),(@NoBeneficiaries + 1))
	INSERT INTO Beneficiaries ([FirstName],[LastName],[DateOfBirth],[EmployeeId],[Curp],[Ssn],[PhoneNumber],[Nationality],[ParticipationPercentage]) 
	VALUES(@FirstName,@LastName,@DateOfBirth,@EmployeeId,@Curp,@Ssn,@PhoneNumber,@Nationality,@ParticipationPercentage)
	SELECT @Id = @@IDENTITY
	UPDATE Beneficiaries SET ParticipationPercentage = @ParticipationPercentage WHERE EmployeeId = @EmployeeId AND IsDeleted = 0
	-- ADD THE DIFFERENCE OF 100% TO FIRST RECORD
	DECLARE @SUM AS DECIMAL(5,2)
	DECLARE @DIF AS DECIMAL(5,2)
	SELECT @SUM = SUM(ParticipationPercentage) FROM Beneficiaries WITH(NOLOCK) WHERE EmployeeId = @EmployeeId AND IsDeleted = 0
	SET @DIF = 100 - ISNULL(@SUM,0)
	IF (@DIF > 0 AND @DIF < 100)
	BEGIN
		UPDATE Beneficiaries SET ParticipationPercentage = ParticipationPercentage + @DIF WHERE
		Id = (SELECT TOP 1 Id FROM Beneficiaries WITH(NOLOCK) WHERE EmployeeId= @EmployeeId AND IsDeleted = 0)
	END
	SELECT Id, FirstName, LastName, DateOfBirth, EmployeeId, Curp, Ssn, PhoneNumber, Nationality, ParticipationPercentage, IsDeleted FROM Beneficiaries WITH(NOLOCK) WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[spr_AddEmployee]    Script Date: 2/26/2024 4:51:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Miguel Hinojo
-- Create date: 23/02/2024
-- Description:	Add Employee
-- =============================================
CREATE PROCEDURE [dbo].[spr_AddEmployee]
(
	@FirstName NVARCHAR(100) ,
	@LastName NVARCHAR(100) ,
	@DateOfBirth datetime,
	@EmployeeNumber int,
	@Curp NVARCHAR(18),
	@Ssn NVARCHAR(11) ,
	@PhoneNumber NVARCHAR(10) ,
	@Nationality NVARCHAR(50)
)
AS          
SET NOCOUNT ON
BEGIN
	DECLARE @Id AS INT 
	INSERT INTO Employees ([FirstName],[LastName],[DateOfBirth],[EmployeeNumber],[Curp],[Ssn],[PhoneNumber],[Nationality]) 
	VALUES(@FirstName,@LastName,@DateOfBirth,@EmployeeNumber,@Curp,@Ssn,@PhoneNumber,@Nationality)
	SELECT @Id = @@IDENTITY
	SELECT Id, FirstName, LastName, DateOfBirth, EmployeeNumber, Curp, Ssn, PhoneNumber, Nationality, IsDeleted FROM Employees WITH(NOLOCK) WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[spr_DeleteBeneficiary]    Script Date: 2/26/2024 4:51:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Miguel Hinojo
-- Create date: 23/02/2024
-- Description:	Delete Beneficiary
-- =============================================
CREATE PROCEDURE [dbo].[spr_DeleteBeneficiary]
(
	@Id INT
)
AS          
SET NOCOUNT ON
BEGIN
	DECLARE @ParticipationPercentage decimal(5,2)
	DECLARE @NoBeneficiaries AS INT
	DECLARE @EmployeeId AS INT 
	SET @EmployeeId = (SELECT EmployeeId FROM Beneficiaries WITH(NOLOCK) WHERE Id = @Id)
	UPDATE Beneficiaries SET [IsDeleted] = 1 WHERE Id = @Id
	-- RECALCULATE THE PARTICIPATION PERCENTAGE
	SET @NoBeneficiaries = ISNULL((SELECT COUNT(1) FROM Beneficiaries WITH(NOLOCK) WHERE EmployeeId = @EmployeeId AND IsDeleted = 0),0)
	SET @ParticipationPercentage = CONVERT(decimal(5,2),100) / CONVERT(decimal(5,2),@NoBeneficiaries)
	UPDATE Beneficiaries SET ParticipationPercentage = @ParticipationPercentage WHERE EmployeeId = @EmployeeId AND [IsDeleted] = 0
	-- ADD THE DIFFERENCE OF 100% TO FIRST RECORD
	DECLARE @SUM AS DECIMAL(5,2)
	DECLARE @DIF AS DECIMAL(5,2)
	SELECT @SUM = SUM(ParticipationPercentage) FROM Beneficiaries WITH(NOLOCK) WHERE EmployeeId = @EmployeeId AND IsDeleted = 0
	SET @DIF = 100 - ISNULL(@SUM,0)
	IF (@DIF > 0 AND @DIF < 100)
	BEGIN
		UPDATE Beneficiaries SET ParticipationPercentage = ParticipationPercentage + @DIF WHERE
		Id = (SELECT TOP 1 Id FROM Beneficiaries WITH(NOLOCK) WHERE EmployeeId= @EmployeeId AND IsDeleted = 0)
	END
	SELECT Id, FirstName, LastName, DateOfBirth, EmployeeId, Curp, Ssn, PhoneNumber, Nationality, ParticipationPercentage, IsDeleted FROM Beneficiaries WITH(NOLOCK) WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[spr_DeleteEmployee]    Script Date: 2/26/2024 4:51:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Miguel Hinojo
-- Create date: 23/02/2024
-- Description:	Delete Employee
-- =============================================
CREATE PROCEDURE [dbo].[spr_DeleteEmployee]
(
	@Id INT
)
AS          
SET NOCOUNT ON
BEGIN
	UPDATE Beneficiaries SET [IsDeleted] = 1 WHERE EmployeeId = @Id
	UPDATE Employees SET [IsDeleted] = 1 WHERE Id = @Id
	SELECT Id, FirstName, LastName, DateOfBirth, EmployeeId, Curp, Ssn, PhoneNumber, Nationality, ParticipationPercentage, IsDeleted FROM Beneficiaries WITH(NOLOCK) WHERE EmployeeId = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[spr_GetBeneficiaries]    Script Date: 2/26/2024 4:51:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Miguel Hinojo
-- Create date: 23/02/2024
-- Description:	Get Beneficiaries
-- =============================================
CREATE PROCEDURE [dbo].[spr_GetBeneficiaries]
(
	@EmployeeId INT
)
AS          
SET NOCOUNT ON
BEGIN
	SELECT Id, FirstName, LastName, DateOfBirth, EmployeeId, Curp, Ssn, PhoneNumber, Nationality, ParticipationPercentage, IsDeleted FROM Beneficiaries WITH(NOLOCK) 
	WHERE EmployeeId = @EmployeeId AND IsDeleted = 0
END
GO
/****** Object:  StoredProcedure [dbo].[spr_GetEmployees]    Script Date: 2/26/2024 4:51:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Miguel Hinojo
-- Create date: 23/02/2024
-- Description:	Get Employees
-- =============================================
CREATE PROCEDURE [dbo].[spr_GetEmployees]
AS          
SET NOCOUNT ON
BEGIN
	SELECT Id, FirstName, LastName, DateOfBirth, EmployeeNumber, Curp, Ssn, PhoneNumber, Nationality, IsDeleted FROM Employees WITH(NOLOCK)
	WHERE IsDeleted = 0
END
GO
/****** Object:  StoredProcedure [dbo].[spr_UpdateBeneficiary]    Script Date: 2/26/2024 4:51:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Miguel Hinojo
-- Create date: 23/02/2024
-- Description:	Update Beneficiary
-- =============================================
CREATE PROCEDURE [dbo].[spr_UpdateBeneficiary]
(
	@Id INT,
	@FirstName NVARCHAR(100) ,
	@LastName NVARCHAR(100) ,
	@DateOfBirth datetime,
	@EmployeeId int,
	@Curp NVARCHAR(18),
	@Ssn NVARCHAR(11) ,
	@PhoneNumber NVARCHAR(10) ,
	@Nationality NVARCHAR(50),
	@ParticipationPercentage decimal(5,2) = NULL
)
AS          
SET NOCOUNT ON
BEGIN
	UPDATE Beneficiaries SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, EmployeeId = @EmployeeId, Curp = @Curp, Ssn = @Ssn, PhoneNumber = @PhoneNumber, Nationality = @Nationality WHERE Id = @Id
	SELECT Id, FirstName, LastName, DateOfBirth, EmployeeId, Curp, Ssn, PhoneNumber, Nationality, ParticipationPercentage, IsDeleted FROM Beneficiaries WITH(NOLOCK) WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[spr_UpdateEmployee]    Script Date: 2/26/2024 4:51:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Miguel Hinojo
-- Create date: 23/02/2024
-- Description:	Update Employee
-- =============================================
CREATE PROCEDURE [dbo].[spr_UpdateEmployee]
(
	@Id INT,
	@FirstName NVARCHAR(100) ,
	@LastName NVARCHAR(100) ,
	@DateOfBirth datetime,
	@EmployeeNumber int,
	@Curp NVARCHAR(18),
	@Ssn NVARCHAR(11) ,
	@PhoneNumber NVARCHAR(10) ,
	@Nationality NVARCHAR(50)
)
AS          
SET NOCOUNT ON
BEGIN
	UPDATE Employees SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, EmployeeNumber = @EmployeeNumber, Curp = @Curp, Ssn = @Ssn, PhoneNumber = @PhoneNumber, Nationality = @Nationality WHERE Id = @Id
	SELECT Id, FirstName, LastName, DateOfBirth, EmployeeNumber, Curp, Ssn, PhoneNumber, Nationality, IsDeleted FROM Employees WITH(NOLOCK) WHERE Id = @Id
END
GO
