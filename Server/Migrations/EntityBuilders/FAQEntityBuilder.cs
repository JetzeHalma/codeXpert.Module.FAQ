using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace codeXpert.Module.FAQ.Migrations.EntityBuilders
{
    public class FAQEntityBuilder : AuditableBaseEntityBuilder<FAQEntityBuilder>
    {
        private const string _entityTableName = "FAQ";
        private readonly PrimaryKey<FAQEntityBuilder> _primaryKey = new("PK_FAQ", x => x.FAQId);
        private readonly ForeignKey<FAQEntityBuilder> _moduleForeignKey = new("FK_FAQ_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public FAQEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override FAQEntityBuilder BuildTable(ColumnsBuilder table)
        {
            FAQId = AddAutoIncrementColumn(table,"FAQId");
            ModuleId = AddIntegerColumn(table,"ModuleId");
            Question = AddMaxStringColumn(table,"Question");
            Answer = AddMaxStringColumn(table, "Answer");
            Order = AddIntegerColumn(table, "Order");
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> FAQId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Question { get; set; }
        public OperationBuilder<AddColumnOperation> Answer { get; set; }
        public OperationBuilder<AddColumnOperation> Order { get; set; }
    }
}
