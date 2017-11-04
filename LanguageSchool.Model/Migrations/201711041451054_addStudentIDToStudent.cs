namespace LanguageSchool.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addStudentIDToStudent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StudentToClasses", "StudentRefID", "dbo.Students");
            DropIndex("dbo.StudentToClasses", new[] { "StudentRefID" });
            DropPrimaryKey("dbo.StudentToClasses");
            DropPrimaryKey("dbo.Students");
            AddColumn("dbo.Students", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.StudentToClasses", "StudentRefID", c => c.Int(nullable: false));
            AlterColumn("dbo.Students", "Email", c => c.String(nullable: false));
            AddPrimaryKey("dbo.StudentToClasses", new[] { "StudentRefID", "ClassRefID" });
            AddPrimaryKey("dbo.Students", "ID");
            CreateIndex("dbo.StudentToClasses", "StudentRefID");
            AddForeignKey("dbo.StudentToClasses", "StudentRefID", "dbo.Students", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentToClasses", "StudentRefID", "dbo.Students");
            DropIndex("dbo.StudentToClasses", new[] { "StudentRefID" });
            DropPrimaryKey("dbo.Students");
            DropPrimaryKey("dbo.StudentToClasses");
            AlterColumn("dbo.Students", "Email", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.StudentToClasses", "StudentRefID", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Students", "ID");
            AddPrimaryKey("dbo.Students", "Email");
            AddPrimaryKey("dbo.StudentToClasses", new[] { "StudentRefID", "ClassRefID" });
            CreateIndex("dbo.StudentToClasses", "StudentRefID");
            AddForeignKey("dbo.StudentToClasses", "StudentRefID", "dbo.Students", "Email", cascadeDelete: true);
        }
    }
}
