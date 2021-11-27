using System;
using System.Collections.Generic;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ConsultorioContext :DbContext
    {
        public ConsultorioContext(DbContextOptions options):base(options)
        {
                       
        }
           public DbSet<Cita> citas { get; set; }  
           public DbSet<Tratamiento> tratamientos { get; set; } 
           public DbSet<Paciente> pacientes { get; set; }
           public DbSet<Psicologo> psicologos { get; set; }
          public DbSet<HistorialClinico> historiales{get;set;}
          public DbSet<Medicamento> medicamentos{get; set;}
          public DbSet<Persona> personas{get; set;}

          public DbSet<Chat> Chats { get; set; }

          public DbSet<Especialidad> Especialidads { get; set; }  

          public DbSet<Agenda> Agendas { get; set; } 


          public DbSet<UtilidadConsultorio> Utilidades { get; set; }
          public DbSet<Usuario> users { get; set; }

        
       
    }
}
