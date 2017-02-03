using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DB.Contexts;

namespace DB.Migrations
{
    [DbContext(typeof(HomegearDevicesContext))]
    partial class HomegearDevicesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("DB.Models.LightSwitch", b =>
                {
                    b.Property<int>("LightSwitchId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ExecutionDateTime");

                    b.Property<bool>("State");

                    b.HasKey("LightSwitchId");

                    b.ToTable("LightSwitches");
                });
        }
    }
}
