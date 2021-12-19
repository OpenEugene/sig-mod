using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace OE.SigMod.Migrations.EntityBuilders
{
    public class SigModEntityBuilder : AuditableBaseEntityBuilder<SigModEntityBuilder>
    {
        private const string _entityTableName = "OESigMod";
        private readonly PrimaryKey<SigModEntityBuilder> _primaryKey = new("PK_OESigMod", x => x.SigModId);
        private readonly ForeignKey<SigModEntityBuilder> _moduleForeignKey = new("FK_OESigMod_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public SigModEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override SigModEntityBuilder BuildTable(ColumnsBuilder table)
        {
            SigModId = AddAutoIncrementColumn(table,"SigModId");
            ModuleId = AddIntegerColumn(table,"ModuleId");
            Name = AddMaxStringColumn(table,"Name");
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> SigModId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Name { get; set; }
    }
}
