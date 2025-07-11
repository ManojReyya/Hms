using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthCareManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    DoctorId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Department = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    WorkStart = table.Column<TimeSpan>(type: "time", nullable: false),
                    WorkEnd = table.Column<TimeSpan>(type: "time", nullable: false),
                    Qualification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactNum = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    ExperienceYears = table.Column<int>(type: "int", nullable: false),
                    AppointmentsDone = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.DoctorId);
                    table.ForeignKey(
                        name: "FK_Doctors_Users_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactNum = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Age = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_Patients_Users_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PatientId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SlotStart = table.Column<TimeSpan>(type: "time", nullable: false),
                    SlotEnd = table.Column<TimeSpan>(type: "time", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorId1 = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    PatientId1 = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_DoctorId1",
                        column: x => x.DoctorId1,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId");
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId1",
                        column: x => x.PatientId1,
                        principalTable: "Patients",
                        principalColumn: "PatientId");
                });

            migrationBuilder.CreateTable(
                name: "MedicalRecords",
                columns: table => new
                {
                    MedicalRecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DoctorId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoctorId1 = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    PatientId1 = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecords", x => x.MedicalRecordId);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "AppointmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Doctors_DoctorId1",
                        column: x => x.DoctorId1,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId");
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Patients_PatientId1",
                        column: x => x.PatientId1,
                        principalTable: "Patients",
                        principalColumn: "PatientId");
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "DoctorId", "AppointmentsDone", "Available", "Bio", "ContactNum", "Department", "Email", "ExperienceYears", "FullName", "IsApproved", "Qualification", "WorkEnd", "WorkStart" },
                values: new object[] { "D001", 300, true, "Cardiologist with 8 years of experience", "9876543210", "Cardiology", "ananya@hospital.com", 8, "Dr. Ananya Sharma", true, "MBBS, MD", new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientId", "Age", "ContactNum", "Email", "FullName", "Gender" },
                values: new object[] { "P001", new DateTime(1990, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "9123456780", "amit.patel@example.com", "Amit Patel", "Male" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "Password", "Role" },
                values: new object[] { "admin001", new DateTime(2025, 7, 11, 16, 38, 7, 970, DateTimeKind.Utc).AddTicks(9380), "admin@123", "Admin" });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "AppointmentId", "AppointmentDate", "DoctorId", "DoctorId1", "PatientId", "PatientId1", "SlotEnd", "SlotStart", "Status" },
                values: new object[] { 1, new DateTime(2025, 7, 11, 0, 0, 0, 0, DateTimeKind.Utc), "D001", null, "P001", null, new TimeSpan(0, 10, 30, 0, 0), new TimeSpan(0, 10, 0, 0, 0), "Scheduled" });

            migrationBuilder.InsertData(
                table: "MedicalRecords",
                columns: new[] { "MedicalRecordId", "AppointmentId", "Diagnosis", "DoctorId", "DoctorId1", "Notes", "PatientId", "PatientId1", "Prescription", "RecordDate" },
                values: new object[] { 1, 1, "High Blood Pressure", "D001", null, "Monitor BP daily", "P001", null, "Amlodipine 5mg", new DateTime(2025, 7, 11, 16, 38, 7, 971, DateTimeKind.Utc).AddTicks(4050) });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId1",
                table: "Appointments",
                column: "DoctorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId1",
                table: "Appointments",
                column: "PatientId1");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_AppointmentId",
                table: "MedicalRecords",
                column: "AppointmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_DoctorId",
                table: "MedicalRecords",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_DoctorId1",
                table: "MedicalRecords",
                column: "DoctorId1");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_PatientId",
                table: "MedicalRecords",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_PatientId1",
                table: "MedicalRecords",
                column: "PatientId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalRecords");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
