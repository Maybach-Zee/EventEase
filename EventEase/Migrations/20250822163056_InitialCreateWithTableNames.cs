using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventEase.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateWithTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Users_ClientId",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Clients");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Clients",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Clients",
                newName: "ClientId");

            migrationBuilder.AddColumn<string>(
                name: "ClientName",
                table: "Clients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "ClientId");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 1,
                columns: new[] { "ClientName", "Email", "PhoneNumber" },
                values: new object[] { "John Smith", "john.smith@email.com", "555-0123" });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 2,
                columns: new[] { "ClientName", "Email", "PhoneNumber" },
                values: new object[] { "Sarah Johnson", "sarah.j@email.com", "555-0456" });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 3,
                columns: new[] { "ClientName", "Email", "PhoneNumber" },
                values: new object[] { "Mike Wilson", "mike.w@email.com", "555-0789" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1,
                columns: new[] { "Description", "EventDate", "EventName" },
                values: new object[] { "Annual technology conference", new DateTime(2025, 9, 21, 18, 30, 56, 125, DateTimeKind.Local).AddTicks(190), "Tech Conference" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 2,
                columns: new[] { "Description", "EventDate" },
                values: new object[] { "Wedding services showcase", new DateTime(2025, 10, 6, 18, 30, 56, 127, DateTimeKind.Local).AddTicks(2609) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 3,
                columns: new[] { "Description", "EventDate" },
                values: new object[] { "Year-end corporate celebration", new DateTime(2025, 10, 21, 18, 30, 56, 127, DateTimeKind.Local).AddTicks(2661) });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "VenueId",
                keyValue: 1,
                column: "Location",
                value: "123 Main St");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "VenueId",
                keyValue: 2,
                column: "Location",
                value: "456 Oak Ave");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "VenueId",
                keyValue: 3,
                column: "Location",
                value: "789 Park Rd");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Clients_ClientId",
                table: "Bookings",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Clients_ClientId",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ClientName",
                table: "Clients");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "Users");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Users",
                newName: "UserId");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1,
                columns: new[] { "Description", "EventDate", "EventName" },
                values: new object[] { "Annual technology conference featuring latest innovations", new DateTime(2023, 12, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), "Tech Conference 2023" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 2,
                columns: new[] { "Description", "EventDate" },
                values: new object[] { "Showcase of wedding services and vendors", new DateTime(2023, 12, 20, 10, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 3,
                columns: new[] { "Description", "EventDate" },
                values: new object[] { "Year-end celebration for corporate partners", new DateTime(2023, 12, 31, 19, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "Email", "Password", "Role", "Username" },
                values: new object[] { "admin@eventease.com", "admin123", "Admin", "admin" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "Email", "Password", "Role", "Username" },
                values: new object[] { "john.smith@eventease.com", "password123", "Specialist", "john.smith" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                columns: new[] { "Email", "Password", "Role", "Username" },
                values: new object[] { "sara.jones@eventease.com", "password123", "Specialist", "sara.jones" });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "VenueId",
                keyValue: 1,
                column: "Location",
                value: "123 Main St, City Center");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "VenueId",
                keyValue: 2,
                column: "Location",
                value: "456 Oak Ave, Business District");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "VenueId",
                keyValue: 3,
                column: "Location",
                value: "789 Park Rd, Green Area");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Users_ClientId",
                table: "Bookings",
                column: "ClientId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
