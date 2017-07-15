namespace eCommerce.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "VetOffice_dbo.Appointments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TimeStart = c.DateTime(nullable: false),
                        TimeEnd = c.DateTime(nullable: false),
                        Title = c.String(nullable: false, maxLength: 255),
                        DateTimeConfirmed = c.DateTime(),
                        State = c.Int(nullable: false),
                        IsPotentiallyConflicting = c.Boolean(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        AppointmentTypeFKId = c.Int(nullable: false),
                        DoctorFKId = c.Int(),
                        PatientFKId = c.Int(nullable: false),
                        RoomFKId = c.Int(nullable: false),
                        ScheduleFKId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("VetOffice_dbo.AppointmentTypes", t => t.AppointmentTypeFKId, cascadeDelete: true)
                .ForeignKey("VetOffice_dbo.Doctors", t => t.DoctorFKId)
                .ForeignKey("VetOffice_dbo.Patients", t => t.PatientFKId, cascadeDelete: true)
                .ForeignKey("VetOffice_dbo.Rooms", t => t.RoomFKId, cascadeDelete: true)
                .ForeignKey("VetOffice_dbo.Schedules", t => t.ScheduleFKId, cascadeDelete: true)
                .Index(t => t.AppointmentTypeFKId, name: "IX_AppointmentType_Id")
                .Index(t => t.DoctorFKId, name: "IX_Doctor_Id")
                .Index(t => t.PatientFKId, name: "IX_Patient_Id")
                .Index(t => t.RoomFKId, name: "IX_Room_Id")
                .Index(t => t.ScheduleFKId, name: "IX_Schedule_Id");
            
            CreateTable(
                "VetOffice_dbo.AppointmentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Code = c.String(nullable: false, maxLength: 128),
                        Duration = c.Int(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "VetOffice_dbo.Doctors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        DateModified = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, name: "DoctorName");
            
            CreateTable(
                "VetOffice_dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Gender = c.Int(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        AnimalType_Species = c.String(nullable: false, maxLength: 255),
                        AnimalType_Breed = c.String(nullable: false, maxLength: 255),
                        OwnerFKId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("VetOffice_dbo.AnimalTypes", t => new { t.AnimalType_Species, t.AnimalType_Breed }, cascadeDelete: true)
                .ForeignKey("VetOffice_dbo.Clients", t => t.OwnerFKId, cascadeDelete: true)
                .Index(t => t.Name, name: "PatientName")
                .Index(t => new { t.AnimalType_Species, t.AnimalType_Breed })
                .Index(t => t.OwnerFKId, name: "IX_Owner_Id");
            
            CreateTable(
                "VetOffice_dbo.AnimalTypes",
                c => new
                    {
                        Species = c.String(nullable: false, maxLength: 255),
                        Breed = c.String(nullable: false, maxLength: 255),
                        DateModified = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.Species, t.Breed })
                .Index(t => t.Species, name: "AnimalTypeSpecies")
                .Index(t => t.Breed, name: "AnimalTypeBreed");
            
            CreateTable(
                "VetOffice_dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 255),
                        LastName = c.String(nullable: false, maxLength: 255),
                        CompanyName = c.String(nullable: false, maxLength: 255),
                        Email = c.String(nullable: false, maxLength: 255),
                        PhoneNumbers = c.String(nullable: false, maxLength: 255),
                        PreferredName = c.String(maxLength: 255),
                        BirthDay = c.DateTime(),
                        Salutation = c.String(maxLength: 255),
                        DateModified = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        PreferedDoctorFKId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("VetOffice_dbo.Doctors", t => t.PreferedDoctorFKId)
                .Index(t => t.FirstName, name: "ClientFirstName")
                .Index(t => t.LastName, name: "ClientLastName")
                .Index(t => t.CompanyName, name: "ClientCompanyName")
                .Index(t => t.Email, name: "ClientEmail")
                .Index(t => t.PreferedDoctorFKId, name: "IX_PreferedDoctor_Id");
            
            CreateTable(
                "VetOffice_dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        DateModified = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, name: "RoomName");
            
            CreateTable(
                "VetOffice_dbo.Schedules",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ClinicId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateStoredProcedure(
                "VetOffice_dbo.Appointments_Insert",
                p => new
                    {
                        Id = p.Guid(),
                        TimeStart = p.DateTime(),
                        TimeEnd = p.DateTime(),
                        Title = p.String(maxLength: 255),
                        DateTimeConfirmed = p.DateTime(),
                        State = p.Int(),
                        IsPotentiallyConflicting = p.Boolean(),
                        DateModified = p.DateTime(),
                        DateCreated = p.DateTime(),
                        AppointmentType_Id = p.Int(),
                        Doctor_Id = p.Int(),
                        Patient_Id = p.Int(),
                        Room_Id = p.Int(),
                        Schedule_Id = p.Guid(),
                    },
                body:
                    @"INSERT [VetOffice_dbo].[Appointments]([Id], [TimeStart], [TimeEnd], [Title], [DateTimeConfirmed], [State], [IsPotentiallyConflicting], [DateModified], [DateCreated], [AppointmentTypeFKId], [DoctorFKId], [PatientFKId], [RoomFKId], [ScheduleFKId])
                      VALUES (@Id, @TimeStart, @TimeEnd, @Title, @DateTimeConfirmed, @State, @IsPotentiallyConflicting, @DateModified, @DateCreated, @AppointmentType_Id, @Doctor_Id, @Patient_Id, @Room_Id, @Schedule_Id)"
            );
            
            CreateStoredProcedure(
                "VetOffice_dbo.Appointments_Update",
                p => new
                    {
                        Id = p.Guid(),
                        TimeStart = p.DateTime(),
                        TimeEnd = p.DateTime(),
                        Title = p.String(maxLength: 255),
                        DateTimeConfirmed = p.DateTime(),
                        State = p.Int(),
                        IsPotentiallyConflicting = p.Boolean(),
                        DateModified = p.DateTime(),
                        DateCreated = p.DateTime(),
                        AppointmentType_Id = p.Int(),
                        Doctor_Id = p.Int(),
                        Patient_Id = p.Int(),
                        Room_Id = p.Int(),
                        Schedule_Id = p.Guid(),
                    },
                body:
                    @"UPDATE [VetOffice_dbo].[Appointments]
                      SET [TimeStart] = @TimeStart, [TimeEnd] = @TimeEnd, [Title] = @Title, [DateTimeConfirmed] = @DateTimeConfirmed, [State] = @State, [IsPotentiallyConflicting] = @IsPotentiallyConflicting, [DateModified] = @DateModified, [DateCreated] = @DateCreated, [AppointmentTypeFKId] = @AppointmentType_Id, [DoctorFKId] = @Doctor_Id, [PatientFKId] = @Patient_Id, [RoomFKId] = @Room_Id, [ScheduleFKId] = @Schedule_Id
                      WHERE ((((([Id] = @Id) AND ([AppointmentTypeFKId] = @AppointmentType_Id)) AND ([PatientFKId] = @Patient_Id)) AND ([RoomFKId] = @Room_Id)) AND ([ScheduleFKId] = @Schedule_Id))"
            );
            
            CreateStoredProcedure(
                "VetOffice_dbo.Appointments_Delete",
                p => new
                    {
                        AppointmentId = p.Guid(),
                        AppointmentType_Id = p.Int(),
                        Doctor_Id = p.Int(),
                        Patient_Id = p.Int(),
                        Room_Id = p.Int(),
                        Schedule_Id = p.Guid(),
                    },
                body:
                    @"DELETE [VetOffice_dbo].[Appointments]
                      WHERE (((((([Id] = @AppointmentId) AND ([AppointmentTypeFKId] = @AppointmentType_Id)) AND (([DoctorFKId] = @Doctor_Id) OR ([DoctorFKId] IS NULL AND @Doctor_Id IS NULL))) AND ([PatientFKId] = @Patient_Id)) AND ([RoomFKId] = @Room_Id)) AND ([ScheduleFKId] = @Schedule_Id))"
            );
            
            CreateStoredProcedure(
                "VetOffice_dbo.AppointmentTypes_Insert",
                p => new
                    {
                        Name = p.String(maxLength: 255),
                        Code = p.String(maxLength: 128),
                        Duration = p.Int(),
                        DateModified = p.DateTime(),
                        DateCreated = p.DateTime(),
                    },
                body:
                    @"INSERT [VetOffice_dbo].[AppointmentTypes]([Name], [Code], [Duration], [DateModified], [DateCreated])
                      VALUES (@Name, @Code, @Duration, @DateModified, @DateCreated)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [VetOffice_dbo].[AppointmentTypes]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [VetOffice_dbo].[AppointmentTypes] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "VetOffice_dbo.AppointmentTypes_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(maxLength: 255),
                        Code = p.String(maxLength: 128),
                        Duration = p.Int(),
                        DateModified = p.DateTime(),
                        DateCreated = p.DateTime(),
                    },
                body:
                    @"UPDATE [VetOffice_dbo].[AppointmentTypes]
                      SET [Name] = @Name, [Code] = @Code, [Duration] = @Duration, [DateModified] = @DateModified, [DateCreated] = @DateCreated
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "VetOffice_dbo.AppointmentTypes_Delete",
                p => new
                    {
                        AppointmentTypeId = p.Int(),
                    },
                body:
                    @"DELETE [VetOffice_dbo].[AppointmentTypes]
                      WHERE ([Id] = @AppointmentTypeId)"
            );
            
            CreateStoredProcedure(
                "VetOffice_dbo.Doctors_Insert",
                p => new
                    {
                        Name = p.String(maxLength: 255),
                        DateModified = p.DateTime(),
                        DateCreated = p.DateTime(),
                        IsDeleted = p.Boolean(),
                    },
                body:
                    @"INSERT [VetOffice_dbo].[Doctors]([Name], [DateModified], [DateCreated], [IsDeleted])
                      VALUES (@Name, @DateModified, @DateCreated, @IsDeleted)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [VetOffice_dbo].[Doctors]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [VetOffice_dbo].[Doctors] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "VetOffice_dbo.Doctors_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(maxLength: 255),
                        DateModified = p.DateTime(),
                        DateCreated = p.DateTime(),
                        IsDeleted = p.Boolean(),
                    },
                body:
                    @"UPDATE [VetOffice_dbo].[Doctors]
                      SET [Name] = @Name, [DateModified] = @DateModified, [DateCreated] = @DateCreated, [IsDeleted] = @IsDeleted
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "VetOffice_dbo.Doctors_Delete",
                p => new
                    {
                        DoctorId = p.Int(),
                    },
                body:
                    @"DELETE [VetOffice_dbo].[Doctors]
                      WHERE ([Id] = @DoctorId)"
            );
            
            CreateStoredProcedure(
                "VetOffice_dbo.Patients_Insert",
                p => new
                    {
                        Name = p.String(maxLength: 255),
                        Gender = p.Int(),
                        DateModified = p.DateTime(),
                        DateCreated = p.DateTime(),
                        AnimalType_Species = p.String(maxLength: 255),
                        AnimalType_Breed = p.String(maxLength: 255),
                        Owner_Id = p.Int(),
                    },
                body:
                    @"INSERT [VetOffice_dbo].[Patients]([Name], [Gender], [DateModified], [DateCreated], [AnimalType_Species], [AnimalType_Breed], [OwnerFKId])
                      VALUES (@Name, @Gender, @DateModified, @DateCreated, @AnimalType_Species, @AnimalType_Breed, @Owner_Id)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [VetOffice_dbo].[Patients]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [VetOffice_dbo].[Patients] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "VetOffice_dbo.Patients_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(maxLength: 255),
                        Gender = p.Int(),
                        DateModified = p.DateTime(),
                        DateCreated = p.DateTime(),
                        AnimalType_Species = p.String(maxLength: 255),
                        AnimalType_Breed = p.String(maxLength: 255),
                        Owner_Id = p.Int(),
                    },
                body:
                    @"UPDATE [VetOffice_dbo].[Patients]
                      SET [Name] = @Name, [Gender] = @Gender, [DateModified] = @DateModified, [DateCreated] = @DateCreated, [AnimalType_Species] = @AnimalType_Species, [AnimalType_Breed] = @AnimalType_Breed, [OwnerFKId] = @Owner_Id
                      WHERE (((([Id] = @Id) AND ([AnimalType_Species] = @AnimalType_Species)) AND ([AnimalType_Breed] = @AnimalType_Breed)) AND ([OwnerFKId] = @Owner_Id))"
            );
            
            CreateStoredProcedure(
                "VetOffice_dbo.Patients_Delete",
                p => new
                    {
                        PatientId = p.Int(),
                        AnimalType_Species = p.String(maxLength: 255),
                        AnimalType_Breed = p.String(maxLength: 255),
                        Owner_Id = p.Int(),
                    },
                body:
                    @"DELETE [VetOffice_dbo].[Patients]
                      WHERE (((([Id] = @PatientId) AND ([AnimalType_Species] = @AnimalType_Species)) AND ([AnimalType_Breed] = @AnimalType_Breed)) AND ([OwnerFKId] = @Owner_Id))"
            );
            
            CreateStoredProcedure(
                "VetOffice_dbo.AnimalTypes_Insert",
                p => new
                    {
                        Species = p.String(maxLength: 255),
                        Breed = p.String(maxLength: 255),
                        DateModified = p.DateTime(),
                        DateCreated = p.DateTime(),
                    },
                body:
                    @"INSERT [VetOffice_dbo].[AnimalTypes]([Species], [Breed], [DateModified], [DateCreated])
                      VALUES (@Species, @Breed, @DateModified, @DateCreated)"
            );
            
            CreateStoredProcedure(
                "VetOffice_dbo.AnimalTypes_Update",
                p => new
                    {
                        Species = p.String(maxLength: 255),
                        Breed = p.String(maxLength: 255),
                        DateModified = p.DateTime(),
                        DateCreated = p.DateTime(),
                    },
                body:
                    @"UPDATE [VetOffice_dbo].[AnimalTypes]
                      SET [DateModified] = @DateModified, [DateCreated] = @DateCreated
                      WHERE (([Species] = @Species) AND ([Breed] = @Breed))"
            );
            
            CreateStoredProcedure(
                "VetOffice_dbo.AnimalTypes_Delete",
                p => new
                    {
                        Species = p.String(maxLength: 255),
                        Breed = p.String(maxLength: 255),
                    },
                body:
                    @"DELETE [VetOffice_dbo].[AnimalTypes]
                      WHERE (([Species] = @Species) AND ([Breed] = @Breed))"
            );
            
            CreateStoredProcedure(
                "VetOffice_dbo.Clients_Insert",
                p => new
                    {
                        FirstName = p.String(maxLength: 255),
                        LastName = p.String(maxLength: 255),
                        CompanyName = p.String(maxLength: 255),
                        Email = p.String(maxLength: 255),
                        PhoneNumbers = p.String(maxLength: 255),
                        PreferredName = p.String(maxLength: 255),
                        BirthDay = p.DateTime(),
                        Salutation = p.String(maxLength: 255),
                        DateModified = p.DateTime(),
                        DateCreated = p.DateTime(),
                        PreferedDoctor_Id = p.Int(),
                    },
                body:
                    @"INSERT [VetOffice_dbo].[Clients]([FirstName], [LastName], [CompanyName], [Email], [PhoneNumbers], [PreferredName], [BirthDay], [Salutation], [DateModified], [DateCreated], [PreferedDoctorFKId])
                      VALUES (@FirstName, @LastName, @CompanyName, @Email, @PhoneNumbers, @PreferredName, @BirthDay, @Salutation, @DateModified, @DateCreated, @PreferedDoctor_Id)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [VetOffice_dbo].[Clients]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [VetOffice_dbo].[Clients] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "VetOffice_dbo.Clients_Update",
                p => new
                    {
                        Id = p.Int(),
                        FirstName = p.String(maxLength: 255),
                        LastName = p.String(maxLength: 255),
                        CompanyName = p.String(maxLength: 255),
                        Email = p.String(maxLength: 255),
                        PhoneNumbers = p.String(maxLength: 255),
                        PreferredName = p.String(maxLength: 255),
                        BirthDay = p.DateTime(),
                        Salutation = p.String(maxLength: 255),
                        DateModified = p.DateTime(),
                        DateCreated = p.DateTime(),
                        PreferedDoctor_Id = p.Int(),
                    },
                body:
                    @"UPDATE [VetOffice_dbo].[Clients]
                      SET [FirstName] = @FirstName, [LastName] = @LastName, [CompanyName] = @CompanyName, [Email] = @Email, [PhoneNumbers] = @PhoneNumbers, [PreferredName] = @PreferredName, [BirthDay] = @BirthDay, [Salutation] = @Salutation, [DateModified] = @DateModified, [DateCreated] = @DateCreated, [PreferedDoctorFKId] = @PreferedDoctor_Id
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "VetOffice_dbo.Clients_Delete",
                p => new
                    {
                        ClientId = p.Int(),
                        PreferedDoctor_Id = p.Int(),
                    },
                body:
                    @"DELETE [VetOffice_dbo].[Clients]
                      WHERE (([Id] = @ClientId) AND (([PreferedDoctorFKId] = @PreferedDoctor_Id) OR ([PreferedDoctorFKId] IS NULL AND @PreferedDoctor_Id IS NULL)))"
            );
            
            CreateStoredProcedure(
                "VetOffice_dbo.Rooms_Insert",
                p => new
                    {
                        Name = p.String(maxLength: 255),
                        DateModified = p.DateTime(),
                        DateCreated = p.DateTime(),
                        IsDeleted = p.Boolean(),
                    },
                body:
                    @"INSERT [VetOffice_dbo].[Rooms]([Name], [DateModified], [DateCreated], [IsDeleted])
                      VALUES (@Name, @DateModified, @DateCreated, @IsDeleted)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [VetOffice_dbo].[Rooms]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [VetOffice_dbo].[Rooms] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "VetOffice_dbo.Rooms_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(maxLength: 255),
                        DateModified = p.DateTime(),
                        DateCreated = p.DateTime(),
                        IsDeleted = p.Boolean(),
                    },
                body:
                    @"UPDATE [VetOffice_dbo].[Rooms]
                      SET [Name] = @Name, [DateModified] = @DateModified, [DateCreated] = @DateCreated, [IsDeleted] = @IsDeleted
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "VetOffice_dbo.Rooms_Delete",
                p => new
                    {
                        RoomId = p.Int(),
                    },
                body:
                    @"DELETE [VetOffice_dbo].[Rooms]
                      WHERE ([Id] = @RoomId)"
            );
            
            CreateStoredProcedure(
                "VetOffice_dbo.Schedules_Insert",
                p => new
                    {
                        Id = p.Guid(),
                        ClinicId = p.Int(),
                        StartDate = p.DateTime(),
                        EndDate = p.DateTime(),
                        DateModified = p.DateTime(),
                        DateCreated = p.DateTime(),
                    },
                body:
                    @"INSERT [VetOffice_dbo].[Schedules]([Id], [ClinicId], [StartDate], [EndDate], [DateModified], [DateCreated])
                      VALUES (@Id, @ClinicId, @StartDate, @EndDate, @DateModified, @DateCreated)"
            );
            
            CreateStoredProcedure(
                "VetOffice_dbo.Schedules_Update",
                p => new
                    {
                        Id = p.Guid(),
                        ClinicId = p.Int(),
                        StartDate = p.DateTime(),
                        EndDate = p.DateTime(),
                        DateModified = p.DateTime(),
                        DateCreated = p.DateTime(),
                    },
                body:
                    @"UPDATE [VetOffice_dbo].[Schedules]
                      SET [ClinicId] = @ClinicId, [StartDate] = @StartDate, [EndDate] = @EndDate, [DateModified] = @DateModified, [DateCreated] = @DateCreated
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "VetOffice_dbo.Schedules_Delete",
                p => new
                    {
                        ScheduleId = p.Guid(),
                    },
                body:
                    @"DELETE [VetOffice_dbo].[Schedules]
                      WHERE ([Id] = @ScheduleId)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("VetOffice_dbo.Schedules_Delete");
            DropStoredProcedure("VetOffice_dbo.Schedules_Update");
            DropStoredProcedure("VetOffice_dbo.Schedules_Insert");
            DropStoredProcedure("VetOffice_dbo.Rooms_Delete");
            DropStoredProcedure("VetOffice_dbo.Rooms_Update");
            DropStoredProcedure("VetOffice_dbo.Rooms_Insert");
            DropStoredProcedure("VetOffice_dbo.Clients_Delete");
            DropStoredProcedure("VetOffice_dbo.Clients_Update");
            DropStoredProcedure("VetOffice_dbo.Clients_Insert");
            DropStoredProcedure("VetOffice_dbo.AnimalTypes_Delete");
            DropStoredProcedure("VetOffice_dbo.AnimalTypes_Update");
            DropStoredProcedure("VetOffice_dbo.AnimalTypes_Insert");
            DropStoredProcedure("VetOffice_dbo.Patients_Delete");
            DropStoredProcedure("VetOffice_dbo.Patients_Update");
            DropStoredProcedure("VetOffice_dbo.Patients_Insert");
            DropStoredProcedure("VetOffice_dbo.Doctors_Delete");
            DropStoredProcedure("VetOffice_dbo.Doctors_Update");
            DropStoredProcedure("VetOffice_dbo.Doctors_Insert");
            DropStoredProcedure("VetOffice_dbo.AppointmentTypes_Delete");
            DropStoredProcedure("VetOffice_dbo.AppointmentTypes_Update");
            DropStoredProcedure("VetOffice_dbo.AppointmentTypes_Insert");
            DropStoredProcedure("VetOffice_dbo.Appointments_Delete");
            DropStoredProcedure("VetOffice_dbo.Appointments_Update");
            DropStoredProcedure("VetOffice_dbo.Appointments_Insert");
            DropForeignKey("VetOffice_dbo.Appointments", "ScheduleFKId", "VetOffice_dbo.Schedules");
            DropForeignKey("VetOffice_dbo.Appointments", "RoomFKId", "VetOffice_dbo.Rooms");
            DropForeignKey("VetOffice_dbo.Appointments", "PatientFKId", "VetOffice_dbo.Patients");
            DropForeignKey("VetOffice_dbo.Patients", "OwnerFKId", "VetOffice_dbo.Clients");
            DropForeignKey("VetOffice_dbo.Clients", "PreferedDoctorFKId", "VetOffice_dbo.Doctors");
            DropForeignKey("VetOffice_dbo.Patients", new[] { "AnimalType_Species", "AnimalType_Breed" }, "VetOffice_dbo.AnimalTypes");
            DropForeignKey("VetOffice_dbo.Appointments", "DoctorFKId", "VetOffice_dbo.Doctors");
            DropForeignKey("VetOffice_dbo.Appointments", "AppointmentTypeFKId", "VetOffice_dbo.AppointmentTypes");
            DropIndex("VetOffice_dbo.Rooms", "RoomName");
            DropIndex("VetOffice_dbo.Clients", "IX_PreferedDoctor_Id");
            DropIndex("VetOffice_dbo.Clients", "ClientEmail");
            DropIndex("VetOffice_dbo.Clients", "ClientCompanyName");
            DropIndex("VetOffice_dbo.Clients", "ClientLastName");
            DropIndex("VetOffice_dbo.Clients", "ClientFirstName");
            DropIndex("VetOffice_dbo.AnimalTypes", "AnimalTypeBreed");
            DropIndex("VetOffice_dbo.AnimalTypes", "AnimalTypeSpecies");
            DropIndex("VetOffice_dbo.Patients", "IX_Owner_Id");
            DropIndex("VetOffice_dbo.Patients", new[] { "AnimalType_Species", "AnimalType_Breed" });
            DropIndex("VetOffice_dbo.Patients", "PatientName");
            DropIndex("VetOffice_dbo.Doctors", "DoctorName");
            DropIndex("VetOffice_dbo.Appointments", "IX_Schedule_Id");
            DropIndex("VetOffice_dbo.Appointments", "IX_Room_Id");
            DropIndex("VetOffice_dbo.Appointments", "IX_Patient_Id");
            DropIndex("VetOffice_dbo.Appointments", "IX_Doctor_Id");
            DropIndex("VetOffice_dbo.Appointments", "IX_AppointmentType_Id");
            DropTable("VetOffice_dbo.Schedules");
            DropTable("VetOffice_dbo.Rooms");
            DropTable("VetOffice_dbo.Clients");
            DropTable("VetOffice_dbo.AnimalTypes");
            DropTable("VetOffice_dbo.Patients");
            DropTable("VetOffice_dbo.Doctors");
            DropTable("VetOffice_dbo.AppointmentTypes");
            DropTable("VetOffice_dbo.Appointments");
        }
    }
}
