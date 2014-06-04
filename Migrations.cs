using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace MainBit.MarkupTags
{
    public class Migrations : DataMigrationImpl {

        public int Create() {

            SchemaBuilder.CreateTable("MarkupTagRecord", table => table
                .Column("Id", DbType.Int32, column => column.PrimaryKey().Identity())
                .Column("Title", DbType.String, column => column.NotNull())
                .Column("Content", DbType.String, column => column.NotNull().Unlimited())
                .Column("Position", DbType.String, column => column.NotNull())
                .Column("Enable", DbType.Boolean, column => column.NotNull())
            );

            return 1;
        }

        public int UpdateFrom1()
        {

            SchemaBuilder.AlterTable("MarkupTagRecord", table => table
                .AddColumn("Zone", DbType.String));

            return 2;
        }
    }
}