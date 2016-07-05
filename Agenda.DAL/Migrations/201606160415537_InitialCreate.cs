namespace Agenda.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agendas",
                c => new
                    {
                        AgendaID = c.Int(nullable: false, identity: true),
                        Titulo = c.String(nullable: false, maxLength: 100),
                        Cor = c.String(),
                        Icone = c.String(),
                        UserName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AgendaID);
            
            CreateTable(
                "dbo.Compromissos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Titulo = c.String(nullable: false, maxLength: 100),
                        Assunto = c.String(nullable: false, maxLength: 100),
                        Local = c.String(maxLength: 100),
                        HoraInicio = c.DateTime(nullable: false),
                        HoraFim = c.DateTime(nullable: false),
                        AgendaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Agendas", t => t.AgendaID, cascadeDelete: true)
                .Index(t => t.AgendaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Compromissos", "AgendaID", "dbo.Agendas");
            DropIndex("dbo.Compromissos", new[] { "AgendaID" });
            DropTable("dbo.Compromissos");
            DropTable("dbo.Agendas");
        }
    }
}
