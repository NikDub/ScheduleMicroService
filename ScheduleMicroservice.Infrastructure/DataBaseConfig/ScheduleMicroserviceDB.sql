IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'ScheduleMSDb') 
BEGIN   
	CREATE DATABASE ScheduleMSDb  
END

use ScheduleMSDb
go

CREATE TABLE Appointments
(
	[Id] uniqueidentifier PRIMARY KEY NOT NULL,
	[PatientId] uniqueidentifier NOT NULL,
	[DoctorId] uniqueidentifier NOT NULL,
	[ServiceId] uniqueidentifier NOT NULL,
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
	[Id] uniqueidentifier PRIMARY KEY NOT NULL,
	[AppointmentsId] uniqueidentifier NOT NULL,
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
	@Id uniqueidentifier
AS
	BEGIN
		SELECT * from [Appointments] where PatientId = @Id
	END

---
GO
CREATE PROCEDURE GetAppointmentsAsDoctor
	@Id uniqueidentifier
AS
	BEGIN
		SELECT * from [Appointments] where DoctorId = @Id
	END

---
GO
CREATE PROCEDURE GetAppointmentById
	@Id uniqueidentifier
AS
	BEGIN
		SELECT * from [Appointments] WHERE Id = @Id
	END

---
GO
CREATE PROCEDURE CreateAppointment
	@Id uniqueidentifier,
	@PatientId uniqueidentifier,
	@DoctorId uniqueidentifier,
	@ServiceId uniqueidentifier,
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
	@Id uniqueidentifier,
	@PatientId uniqueidentifier,
	@DoctorId uniqueidentifier,
	@ServiceId uniqueidentifier,
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
	@ServiceId uniqueidentifier,
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
	@DoctorId uniqueidentifier,
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
	@PatientId uniqueidentifier,
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
	@Id uniqueidentifier,
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
	@Id uniqueidentifier
AS 
	BEGIN
		DELETE [Appointments] WHERE Id = @Id
	END

---
GO
CREATE PROCEDURE GetAppointmentsWithResult
	@Id uniqueidentifier
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
	@Id uniqueidentifier
AS
	BEGIN
		SELECT * from [Results] WHERE Id = @Id
	END

---
GO
CREATE PROCEDURE CreateResult
	@Id uniqueidentifier,
	@AppointmentsId uniqueidentifier,
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
	@Id uniqueidentifier,
	@AppointmentsId uniqueidentifier,
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
	@Id uniqueidentifier
AS 
	BEGIN
		DELETE [Results] WHERE Id = @Id
	END