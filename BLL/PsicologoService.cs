using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace BLL
{
    public class PsicologoService
    {
        private ConsultorioContext _consultorioContext;

        public PsicologoService(ConsultorioContext context)
        {
            _consultorioContext = context;
        }

        public SaveResponse GuardarPsicologo(Psicologo psicologo)
        {
            try
            {
                var response = _consultorioContext.personas.Find(psicologo.Persona.Identification);
                if (response == null)
                {
                    response = _consultorioContext.personas.Where(p => p.Usuario.Equals(psicologo.Persona.Usuario)).FirstOrDefault();
                    if (response == null)
                    {
                        foreach(var p in psicologo.Agenda){
                            p.IdPsicologo=psicologo.Persona.Identification;
                        }
                        psicologo.Persona.Estado=true;
                        psicologo.CodigoPsicologo=psicologo.Persona.Identification;
                        _consultorioContext.Add(psicologo);
                        _consultorioContext.SaveChanges();
                        return new SaveResponse(psicologo);
                    } else return new SaveResponse("Ya se encuentraa un psicologo con este usuario", "EXISTE");

                }
                else return new SaveResponse("Ya se encuentra un psicologo con esta identificación", "EXISTE");
            }
            catch (Exception e)
            {
                return new SaveResponse($"Error aplicación: {e.Message}", "ERROR");
            }
        }

        public LoginResponse LoginPaciente(string username, string password){
            var response = _consultorioContext.pacientes.Where(p=>p.Persona.Usuario.Equals(username) && p.Persona.password.Equals(password)).FirstOrDefault();
            if(response == null){
                var responsep = _consultorioContext.psicologos.Where(p=>p.Persona.Usuario.Equals(username) && p.Persona.password.Equals(password)).FirstOrDefault();
                if(responsep == null){
                    return new LoginResponse(false, "null");
                }else return new LoginResponse(true, "psicologo");
            }  else return new LoginResponse(true, "paciente");
        }
  public ConsultarResponse ConsultarPsicologos(){
            ConsultarResponse consultarResponse = new ConsultarResponse();
            try{
                consultarResponse.Error= false;
                consultarResponse.Mensaje="Consulta Correctamente";
                consultarResponse.psicologos=_consultorioContext.psicologos.Include(p=>p.Persona).Include(p=>p.Especialidad).Include(p=>p.Agenda).Where(p=>p.Persona.Estado==true).ToList();
                

            }catch (Exception e){
               consultarResponse.Error =true;
               consultarResponse.Mensaje=$"Hubo Un Error:{e.Message}";
               consultarResponse.psicologos=null;
            }

            return consultarResponse;
        }


        public ConsultarResponse ConsultarPsicologoId(string id){
            ConsultarResponse consultarResponse = new ConsultarResponse();
            try{
                consultarResponse.Error =false;
                consultarResponse.Mensaje ="Consulta Correctamente";
                var psicologo= _consultorioContext.psicologos.Include(p=> p.Especialidad).Include(p=>p.Persona).Include(a=>a.Agenda).Where(p=> p.CodigoPsicologo.Equals(id)&&p.Persona.Estado==true).FirstOrDefault();
                var especialidad= _consultorioContext.Especialidads.Where(p=> p.CodigoEspecialidad==psicologo.Especialidad.CodigoEspecialidad ).FirstOrDefault();
                
                consultarResponse.Psicologo=psicologo;

            }catch (Exception e){
                      consultarResponse.Error =true;
                consultarResponse.Mensaje =$"Hubo Un Error: {e.Message}";
                consultarResponse.Psicologo=null;
            }
            return consultarResponse;
        }

       
          public DeleteResponseP CambiarEstadoPsicologo(string id){
              DeleteResponseP deletepsicologo = new DeleteResponseP();
              try{
                  var psicologo= _consultorioContext.psicologos.Include(p=>p.Persona).Where(p=> p.CodigoPsicologo==id).FirstOrDefault();
                  
                  if(psicologo == null){
                      deletepsicologo.Error=true;
                      deletepsicologo.Mensaje="No se encontro";
                  }else{
                    deletepsicologo.Error=false;
                    
                    psicologo.Persona.Estado=!psicologo.Persona.Estado;
                    _consultorioContext.SaveChanges();
                    deletepsicologo.Psicologo= psicologo;
                    deletepsicologo.Mensaje="se borro con exito";
                  }
                 
                   
              }catch (Exception e){
                            deletepsicologo.Error=true;
                            deletepsicologo.Mensaje =$"Hubo un error:{e.Message}";
                            deletepsicologo.Psicologo= null;
              }
               return deletepsicologo;
          }

          public EditPsicologo EditarPsicologo(Psicologo psicologo){
                EditPsicologo editPsicologo=new EditPsicologo();
                try{
                    var psicologoConsultado= _consultorioContext.psicologos.Include(p=>p.Persona).Where(p=> p.CodigoPsicologo==psicologo.Persona.Identification).FirstOrDefault();
                    if(psicologoConsultado!=null){
                        editPsicologo.Error=false;
                       psicologoConsultado.Persona.Nombre=psicologo.Persona.Nombre;
                       psicologoConsultado.Persona.Apellido=psicologo.Persona.Apellido;
                       psicologoConsultado.Persona.Edad=psicologo.Persona.Edad;
                       psicologoConsultado.Persona.Correo=psicologo.Persona.Correo;
                       psicologoConsultado.Persona.Direccion=psicologo.Persona.Direccion;
                       psicologoConsultado.Persona.Sexo=psicologo.Persona.Sexo;
                       psicologoConsultado.Persona.Foto=psicologo.Persona.Foto;
                       psicologoConsultado.Persona.Telefono=psicologo.Persona.Telefono;
                       psicologoConsultado.Persona.Usuario=psicologo.Persona.Usuario;
                       psicologoConsultado.Persona.password=psicologo.Persona.password;
                       psicologoConsultado.Especialidad=psicologo.Especialidad;
                       
                       _consultorioContext.psicologos.Update(psicologoConsultado);
                       _consultorioContext.SaveChanges();
                       editPsicologo.Mensaje="Se Edito Con exito";
                    }
                }catch (Exception e){
                     editPsicologo.Error =true;
                     editPsicologo.Mensaje =$"No se encontro{e.Message}";
                }
                return editPsicologo;
          }

         


        public class DeleteResponseP{
            public bool Error { get; set; }
            public string Mensaje { get; set; }
            public Psicologo Psicologo { get; set; }
        }


        public class ConsultarResponse{

           
            public string Mensaje { get; set; }
             public bool Error { get; set; }
              public List<Psicologo> psicologos { get; set; }

              public Psicologo Psicologo{ get; set; }
            
        }



public class SaveResponse
    {

        public SaveResponse(Psicologo psicologo)
        {
            Error = false;
            Psicologo = psicologo;
            Estado = "Guardado";
        }

        public SaveResponse(string message, string estate)
        {
            Error = true;
            Mensaje = message;
            Estado = estate;
        }

        public bool Error { get; set; }
        public string Mensaje { get; set; }
        public Psicologo Psicologo { get; set; }
        public string Estado { get; set; }
    }

    public class LoginResponse{
        public LoginResponse(bool state, string role)
        {
         Login=state;
         Rol=role;   
        }
        public bool Login{get; set;}
        public string Rol{get; set;}
    }
        public class EditPsicologo{
            public string Mensaje { get; set; }
            public bool Error { get; set; }

            public Psicologo Psicologo { get; set; }
        }
    }

    

    
    }