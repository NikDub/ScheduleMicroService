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
	[Status] bit NULL,
	[ServiceName] NVARCHAR(MAX) NOT NULL,
	[DoctorFirstName] NVARCHAR(MAX) NOT NULL,
	[DoctorLastName] NVARCHAR(MAX) NOT NULL,
	[DoctorMiddleName] NVARCHAR(MAX) NOT NULL,
	[PatientFirstName] NVARCHAR(MAX) NOT NULL,
	[PatientLastName] NVARCHAR(MAX) NOT NULL,
	[PatientMiddleName] NVARCHAR(MAX) NOT NULL
)

CREATE TABLE Results
(
	[Id] NVARCHAR(450) PRIMARY KEY NOT NULL,
	[AppointmentsId] NVARCHAR(450) NOT NULL,
	[Complaints] NVARCHAR(MAX) NOT NULL,
	[Conclusion] NVARCHAR(MAX) NOT NULL,
	[Recommendations] NVARCHAR(MAX) NOT NULL,

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
	@Id NVARCHAR(450)
AS
	BEGIN
		SELECT * from [Appointments] where PatientId = @Id
	END

---
GO
CREATE PROCEDURE GetAppointmentsAsDoctor
	@Id NVARCHAR(450)
AS
	BEGIN
		SELECT * from [Appointments] where DoctorId = @Id
	END

---
GO
CREATE PROCEDURE GetAppointmentById
	@Id NVARCHAR(450)
AS
	BEGIN
		SELECT * from [Appointments] WHERE Id = @Id
	END

---
GO
CREATE PROCEDURE CreateAppointment
	@Id NVARCHAR(450),
	@PatientId NVARCHAR(450),
	@DoctorId NVARCHAR(450),
	@ServiceId NVARCHAR(450),
	@Date DATE,
	@Time TIME,
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
			Id, 
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
			@Id,
			@PatientId,
			@DoctorId,
			@ServiceId,
			@Date,
			@Time,
			null,
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
CREATE PROCEDURE UpdateAppointment
	@Id NVARCHAR(450),
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
		WHERE Id = @Id
	END
	
---

GO
CREATE PROCEDURE UpdateAppointmentServiceName
	@ServiceId NVARCHAR(450),
	@ServiceName NVARCHAR(MAX)
AS
	BEGIN
		UPDATE [Appointments] 
		SET 
			[ServiceName] = @ServiceName
		WHERE [ServiceId] = @ServiceId
	END
	
---
GO
CREATE PROCEDURE UpdateAppointmentDoctorName
	@DoctorId NVARCHAR(450),
	@DoctorFirstName NVARCHAR(MAX),	
	@DoctorLastName NVARCHAR(MAX),
	@DoctorMiddleName NVARCHAR(MAX)
AS
	BEGIN
		UPDATE [Appointments] 
		SET 
			[DoctorFirstName] = @DoctorFirstName,
			[DoctorLastName] = @DoctorLastName,
			[DoctorMiddleName] = @DoctorMiddleName
		WHERE [DoctorId] = @DoctorId
	END
	
---
GO
CREATE PROCEDURE UpdateAppointmentPatientName
	@PatientId NVARCHAR(450),
	@PatientFirstName NVARCHAR(MAX),
	@PatientLastName NVARCHAR(MAX),
	@PatientMiddleName NVARCHAR(MAX)

AS
	BEGIN
		UPDATE [Appointments] 
		SET 
			[PatientFirstName] = @PatientFirstName,
			[PatientLastName] = @PatientLastName,
			[PatientMiddleName] = @PatientMiddleName
		WHERE [PatientId] = @PatientId
	END
	
---
GO
CREATE PROCEDURE UpdateAppointmentStatus
	@Id NVARCHAR(450),
	@Status BIT
AS
	BEGIN
		UPDATE [Appointments] 
		SET 
			[Status] = @Status
		WHERE Id = @Id
	END

---
GO
CREATE PROCEDURE DeleteAppointment
	@Id NVARCHAR(450)
AS 
	BEGIN
		DELETE [Appointments] WHERE Id = @Id
	END

---
GO
CREATE PROCEDURE GetAppointmentsWithResult
	@Id NVARCHAR(450)
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
	@Id NVARCHAR(450)
AS
	BEGIN
		SELECT * from [Results] WHERE Id = @Id
	END

---
GO
CREATE PROCEDURE CreateResult
	@Id NVARCHAR(450),
	@AppointmentsId NVARCHAR(450),
	@Complaints NVARCHAR(MAX),
	@Conclusion NVARCHAR(MAX),
	@Recommendations NVARCHAR(MAX)
AS
	BEGIN
		INSERT INTO [Results]
		(
			Id, 
			AppointmentsId, 
			Complaints, 
			Conclusion, 
			Recommendations
		) 
		VALUES
		(
			@Id,
			@AppointmentsId,
			@Complaints,
			@Conclusion,
			@Recommendations
		)
	END

---
GO
CREATE PROCEDURE UpdateResult
	@Id NVARCHAR(450),
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
		WHERE Id = @Id
	END
	
---
GO
CREATE PROCEDURE DeleteResult
	@Id NVARCHAR(450)
AS 
	BEGIN
		DELETE [Results] WHERE Id = @Id
	END