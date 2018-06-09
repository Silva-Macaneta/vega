using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace vega.Migrations
{
    public partial class addVehicles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vehicles_v",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContactName = table.Column<string>(maxLength: 255, nullable: false),
                    ContactPhone = table.Column<string>(maxLength: 255, nullable: false),
                    ContactEmail = table.Column<string>(maxLength: 255, nullable: true),
                    isRegistered = table.Column<bool>(nullable: false),
                    ModelId = table.Column<int>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicles_v", x => x.id);
                    table.ForeignKey(
                        name: "FK_vehicles_v_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleFeatures_v",
                columns: table => new
                {
                    FeatureId = table.Column<int>(nullable: false),
                    VehicleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleFeatures_v", x => new { x.FeatureId, x.VehicleId });
                    table.ForeignKey(
                        name: "FK_VehicleFeatures_v_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleFeatures_v_vehicles_v_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "vehicles_v",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleFeatures_v_VehicleId",
                table: "VehicleFeatures_v",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_v_ModelId",
                table: "vehicles_v",
                column: "ModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleFeatures_v");

            migrationBuilder.DropTable(
                name: "vehicles_v");
        }
    }
}
