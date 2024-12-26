using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace Hexstor.Module.Template.Migrations.EntityBuilders
{
    public class TemplateEntityBuilder : AuditableBaseEntityBuilder<TemplateEntityBuilder>
    {
        private const string _entityTableName = "HexstorTemplate";
        private readonly PrimaryKey<TemplateEntityBuilder> _primaryKey = new("PK_HexstorTemplate", x => x.TemplateId);
        private readonly ForeignKey<TemplateEntityBuilder> _moduleForeignKey = new("FK_HexstorTemplate_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public TemplateEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override TemplateEntityBuilder BuildTable(ColumnsBuilder table)
        {
            TemplateId = AddAutoIncrementColumn(table,"TemplateId");
            ModuleId = AddIntegerColumn(table,"ModuleId");
            Name = AddMaxStringColumn(table,"Name");
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> TemplateId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Name { get; set; }
    }
}
