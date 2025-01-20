using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace Devin.Module.Targeting.Migrations.EntityBuilders
{
    public class TargetingEntityBuilder : AuditableBaseEntityBuilder<TargetingEntityBuilder>
    {
        private const string _entityTableName = "DevinTargeting";
        private readonly PrimaryKey<TargetingEntityBuilder> _primaryKey = new("PK_DevinTargeting", x => x.TargetingId);
        private readonly ForeignKey<TargetingEntityBuilder> _moduleForeignKey = new("FK_DevinTargeting_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public TargetingEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override TargetingEntityBuilder BuildTable(ColumnsBuilder table)
        {
            TargetingId = AddAutoIncrementColumn(table,"TargetingId");
            ModuleId = AddIntegerColumn(table,"ModuleId");
            Name = AddMaxStringColumn(table,"Name");
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> TargetingId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Name { get; set; }
    }
}
