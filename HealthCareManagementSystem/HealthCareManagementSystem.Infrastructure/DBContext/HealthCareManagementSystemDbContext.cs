using System;
using Microsoft.EntityFrameworkCore;
using HealthCareManagementSystem.Domain.Entities;
using HealthCareManagementSystem.Domain.Enums;

// using hashing algo ==> bcrypt

namespace HealthCareManagementSystem.Infrastructure
{
    public class HealthCareManagementSystemDbContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ELIZA\\SQLEXPRESS;Database=HealthCareDB;Trusted_Connection=True;TrustServerCertificate=True;");
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Enum conversions
            modelBuilder.Entity<Appointment>()
                .Property(a => a.Status)
                .HasConversion<string>();

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>();

            // Primary keys
            modelBuilder.Entity<Doctor>().HasKey(d => d.DoctorId);
            modelBuilder.Entity<Patient>().HasKey(p => p.PatientId);
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<Appointment>().HasKey(a => a.AppointmentId);
            modelBuilder.Entity<MedicalRecord>().HasKey(m => m.MedicalRecordId);

            // Relationships
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany()
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany()
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MedicalRecord>()
                .HasOne(m => m.Doctor)
                .WithMany()
                .HasForeignKey(m => m.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MedicalRecord>()
                .HasOne(m => m.Patient)
                .WithMany()
                .HasForeignKey(m => m.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MedicalRecord>()
                .HasOne(m => m.Appointment)
                .WithOne(a => a.MedicalRecord)
                .HasForeignKey<MedicalRecord>(m => m.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Static seed data
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    DoctorId = "D001",
                    FullName = "Dr. Ananya Sharma",
                    Bio = "Cardiologist with 8 years of experience",
                    Department = "Cardiology",
                    Available = true,
                    IsApproved = true,
                    WorkStart = new TimeSpan(9, 0, 0),
                    WorkEnd = new TimeSpan(17, 0, 0),
                    Qualification = "MBBS, MD",
                    Email = "ananya@hospital.com",
                    ContactNum = "9876543210",
                    ExperienceYears = 8,
                    AppointmentsDone = 300
                }
            );

            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    PatientId = "P001",
                    FullName = "Amit Patel",
                    Gender = "Male",
                    Email = "amit.patel@example.com",
                    ContactNum = "9123456780",
                    Age = 34
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = "admin001",
                    // actual password => admin@123
                    Password = "$2a$12$bQx1RmyUCP.E8vNe6RFk5ugrLHHxQiee/yau.xfBKHXsn9huz7U6C",
                    Role = UserRole.Admin,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 1, 1, 10, 0, 0)
                },
                new User
                {
                    UserId = "D001", // This is required for Doctor FK
                    // actual password => doctor@123
                    Password = "$2a$12$D/ys3Mu9NoInYBOtNoEDP.Dh4IfbUS1Ihuhj8GlvHZCwN2YpdkqwW",
                    Role = UserRole.Doctor,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 1, 1, 9, 0, 0)
                },
                new User
                {
                    UserId = "P001", // Needed to satisfy FK constraint
                    // actual password => patient@123
                    Password = "$2a$12$pIR23T7gg7saSRoh2g.y8eFAefYJSM2ueAp4NnHQmEGB/3BRwJYHW",
                    Role = UserRole.Patient,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 1, 1, 8, 0, 0)
                }

            );

            modelBuilder.Entity<Appointment>().HasData(
                new Appointment
                {
                    AppointmentId = 1,
                    DoctorId = "D001",
                    PatientId = "P001",
                    AppointmentDate = new DateTime(2024, 1, 2),
                    SlotStart = new TimeSpan(10, 0, 0),
                    SlotEnd = new TimeSpan(10, 30, 0),
                    Status = AppointmentStatus.Scheduled
                }
            );

            modelBuilder.Entity<MedicalRecord>().HasData(
                new MedicalRecord
                {
                    MedicalRecordId = 1,
                    DoctorId = "D001",
                    PatientId = "P001",
                    AppointmentId = 1,
                    Diagnosis = "High Blood Pressure",
                    Prescription = "Amlodipine 5mg",
                    Notes = "Monitor BP daily",
                    RecordDate = new DateTime(2024, 1, 3)
                }
            );
        }
    }
}
