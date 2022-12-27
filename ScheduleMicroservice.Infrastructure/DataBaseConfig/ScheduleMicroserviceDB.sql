IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'ScheduleMSDb') 
BEGIN   
	CREATE DATABASE ScheduleMSDb  
END

use ScheduleMSDb
go

CREATE TABLE Appointments
(
	[Id] NVARCHAR(450) PRIMARY KEY NOT NULL,
	[PatientId] NVARCHAR(450) NOT NULL,
	[DoctorId] NVARCHAR(450) NOT NULL,
	[ServiceId] NVARCHAR(450) NOT NULL,
	[Date] date NOT NULL,
	[Time] time NOT NULL,
	[Status] bit NOT NULL,
	[ServiceName] NVARCHAR(MAX) NULL,
	[DoctorFirstName] NVARCHAR(MAX) NULL,
	[DoctorLastName] NVARCHAR(MAX) NULL,
	[DoctorMiddleName] NVARCHAR(MAX) NULL,
	[PatientFirstName] NVARCHAR(MAX) NULL,
	[PatientLastName] NVARCHAR(MAX) NULL,
	[PatientMiddleName] NVARCHAR(MAX) NULL
)

CREATE TABLE Results
(
	[Id] NVARCHAR(450) PRIMARY KEY NOT NULL,
	[AppointmentsId] NVARCHAR(450) NOT NULL,
	[Complaints] NVARCHAR(MAX) NULL,
	[Conclusion] NVARCHAR(MAX) NULL,
	[Recommendations] NVARCHAR(MAX) NULL,

	FOREIGN KEY (AppointmentsId) REFERENCES Appointments (Id) ON DELETE CASCADE,
)

---
GO
CREATE PROCEDURE GetAppointments
AS
	BEGIN
		SELECT * from [Appointments]
	END

---
GO
CREATE PROCEDURE GetAppointmentsAsPatient
	@ID NVARCHAR(450)
AS
	BEGIN
		SELECT * from [Appointments] where PatientId = @ID
	END

---
GO
CREATE PROCEDURE GetAppointmentsAsDoctor
	@ID NVARCHAR(450)
AS
	BEGIN
		SELECT * from [Appointments] where DoctorId = @ID
	END

---
GO
CREATE PROCEDURE GetAppointmentsById
	@ID NVARCHAR(450)
AS
	BEGIN
		SELECT * from [Appointments] WHERE Id = @ID
	END

---
GO
CREATE PROCEDURE CreateAppointments
	@ID NVARCHAR(450),
	@PatientId NVARCHAR(450),
	@DoctorId NVARCHAR(450),
	@ServiceId NVARCHAR(450),
	@Date DATE,
	@Time TIME,
	@Status BIT,
	@ServiceName NVARCHAR(MAX),
	@DoctorFirstName NVARCHAR(MAX),	
	@DoctorLastName NVARCHAR(MAX),
	@DoctorMiddleName NVARCHAR(MAX),
	@PatientFirstName NVARCHAR(MAX),
	@PatientLastName NVARCHAR(MAX),
	@PatientMiddleName NVARCHAR(MAX)
AS
	BEGIN
		INSERT INTO [Appointments]
		(
			ID, 
			PatientId, 
			DoctorId, 
			ServiceId, 
			Date, 
			Time, 
			Status, 
			ServiceName, 
			DoctorFirstName, 
			DoctorLastName, 
			DoctorMiddleName, 
			PatientFirstName,
			PatientLastName,
			PatientMiddleName
		) 
		VALUES
		(
			@ID,
			@PatientId,
			@DoctorId,
			@ServiceId,
			@Date,
			@Time,
			@Status,
			@ServiceName,
			@DoctorFirstName,
			@DoctorLastName,
			@DoctorMiddleName,
			@PatientFirstName,
			@PatientLastName,
			@PatientMiddleName
		)
	END

---
GO
CREATE PROCEDURE UpdateAppointments
	@ID NVARCHAR(450),
	@PatientId NVARCHAR(450),
	@DoctorId NVARCHAR(450),
	@ServiceId NVARCHAR(450),
	@Date DATE,
	@Time TIME,
	@Status BIT,
	@ServiceName NVARCHAR(MAX),
	@DoctorFirstName NVARCHAR(MAX),	
	@DoctorLastName NVARCHAR(MAX),
	@DoctorMiddleName NVARCHAR(MAX),
	@PatientFirstName NVARCHAR(MAX),
	@PatientLastName NVARCHAR(MAX),
	@PatientMiddleName NVARCHAR(MAX)
AS
	BEGIN
		UPDATE [Appointments] 
		SET 
			[PatientId] = @PatientId,
			[DoctorId] = @DoctorId,
			[ServiceId] = @ServiceId,
			[Date] = @Date,
			[Time] = @Time,
			[Status] = @Status,
			[ServiceName] = @ServiceName,
			[DoctorFirstName] = @DoctorFirstName,
			[DoctorLastName] = @DoctorLastName,
			[DoctorMiddleName] = @DoctorMiddleName,
			[PatientFirstName] = @PatientFirstName,
			[PatientLastName] = @PatientLastName,
			[PatientMiddleName] = @PatientMiddleName
		WHERE Id = @ID
	END
	
---
GO
CREATE PROCEDURE UpdateAppointmentsStatus
	@ID NVARCHAR(450),
	@Status BIT
AS
	BEGIN
		UPDATE [Appointments] 
		SET 
			[Status] = @Status
		WHERE Id = @ID
	END

---
GO
CREATE PROCEDURE DeleteAppointments
	@ID NVARCHAR(450)
AS 
	BEGIN
		DELETE [Appointments] WHERE Id = @ID
	END

---
GO
CREATE PROCEDURE GetAppointmentsWithResult
	@ID NVARCHAR(450)
AS
	BEGIN
		SELECT * FROM Appointments WHERE Id = @Id
		SELECT * FROM Results WHERE AppointmentsId = @Id
	END

-----------------------------------------

GO
CREATE PROCEDURE GetResult
AS
	BEGIN
		SELECT * from [Results]
	END

---
GO
CREATE PROCEDURE GetResultById
	@ID NVARCHAR(450)
AS
	BEGIN
		SELECT * from [Results] WHERE Id = @ID
	END

---
GO
CREATE PROCEDURE CreateResult
	@ID NVARCHAR(450),
	@AppointmentsId NVARCHAR(450),
	@Complaints NVARCHAR(MAX),
	@Conclusion NVARCHAR(MAX),
	@Recommendations NVARCHAR(MAX)
AS
	BEGIN
		INSERT INTO [Results]
		(
			ID, 
			AppointmentsId, 
			Complaints, 
			Conclusion, 
			Recommendations
		) 
		VALUES
		(
			@ID,
			@AppointmentsId,
			@Complaints,
			@Conclusion,
			@Recommendations
		)
	END

---
GO
CREATE PROCEDURE UpdateResult
	@ID NVARCHAR(450),
	@AppointmentsId NVARCHAR(450),
	@Complaints NVARCHAR(MAX),
	@Conclusion NVARCHAR(MAX),
	@Recommendations NVARCHAR(MAX)
AS
	BEGIN
		UPDATE [Results] 
		SET 
			AppointmentsId = @AppointmentsId,
			Complaints = @Complaints,
			Conclusion = @Conclusion,
			Recommendations = @Recommendations
		WHERE Id = @ID
	END
	
---
GO
CREATE PROCEDURE DeleteResult
	@ID NVARCHAR(450)
AS 
	BEGIN
		DELETE [Results] WHERE Id = @ID
	END