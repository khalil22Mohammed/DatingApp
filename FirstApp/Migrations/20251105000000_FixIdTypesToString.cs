using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstApp.Migrations
{
    /// <inheritdoc />
    public partial class FixIdTypesToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key constraints first
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Members_Users_Id')
                    ALTER TABLE [Members] DROP CONSTRAINT [FK_Members_Users_Id];
                
                IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Photos_Members_MemberId')
                    ALTER TABLE [Photos] DROP CONSTRAINT [FK_Photos_Members_MemberId];
            ");

            // Drop primary keys
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT * FROM sys.key_constraints WHERE name = 'PK_Members')
                    ALTER TABLE [Members] DROP CONSTRAINT [PK_Members];
                
                IF EXISTS (SELECT * FROM sys.key_constraints WHERE name = 'PK_Users')
                    ALTER TABLE [Users] DROP CONSTRAINT [PK_Users];
            ");

            // Add temporary string columns
            migrationBuilder.Sql(@"
                ALTER TABLE [Users] ADD [IDTemp] nvarchar(450) NULL;
                ALTER TABLE [Members] ADD [IdTemp] nvarchar(450) NULL;
                ALTER TABLE [Photos] ADD [MemberIdTemp] nvarchar(450) NULL;
            ");

            // Copy and convert data from int to string
            migrationBuilder.Sql(@"
                UPDATE [Users] SET [IDTemp] = CAST([ID] AS NVARCHAR(450));
                UPDATE [Members] SET [IdTemp] = CAST([Id] AS NVARCHAR(450));
                UPDATE [Photos] SET [MemberIdTemp] = CAST([MemberId] AS NVARCHAR(450));
            ");

            // Drop old int columns
            migrationBuilder.Sql(@"
                ALTER TABLE [Photos] DROP COLUMN [MemberId];
                ALTER TABLE [Members] DROP COLUMN [Id];
                ALTER TABLE [Users] DROP COLUMN [ID];
            ");

            // Rename temp columns
            migrationBuilder.Sql(@"
                EXEC sp_rename '[Users].[IDTemp]', 'ID', 'COLUMN';
                EXEC sp_rename '[Members].[IdTemp]', 'Id', 'COLUMN';
                EXEC sp_rename '[Photos].[MemberIdTemp]', 'MemberId', 'COLUMN';
            ");

            // Make columns NOT NULL
            migrationBuilder.Sql(@"
                ALTER TABLE [Users] ALTER COLUMN [ID] nvarchar(450) NOT NULL;
                ALTER TABLE [Members] ALTER COLUMN [Id] nvarchar(450) NOT NULL;
                ALTER TABLE [Photos] ALTER COLUMN [MemberId] nvarchar(450) NOT NULL;
            ");

            // Recreate primary keys
            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Members",
                table: "Members",
                column: "Id");

            // Recreate foreign keys
            migrationBuilder.AddForeignKey(
                name: "FK_Members_Users_Id",
                table: "Members",
                column: "Id",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Members_MemberId",
                table: "Photos",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // This migration is not easily reversible
            // Converting string back to int would require data conversion
            throw new NotImplementedException("Rolling back this migration requires data conversion and is not supported automatically.");
        }
    }
}

