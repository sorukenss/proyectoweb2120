using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using Entity;
using Microsoft.EntityFrameworkCore;


namespace BLL
{
    public class AgendaService
    {
        private ConsultorioContext _consultorioContext;

        public AgendaService(ConsultorioContext context)
        {
            _consultorioContext=context;
        }


          public ConsultarResponse ConsultarCitas(string id){
              try{
                  var psicologo = _consultorioContext.psicologos.Where(p=> p.CodigoPsicologo.Equals(id));
                  if(psicologo==null){
                      return new ConsultarResponse("No se encuentra al psicólogo");
                  } else{
                      var Fecha = DateTime.Now.ToShortDateString();
                      var citasPsicologo = _consultorioContext.citas.Where(c=>c.Fecha.Equals(Fecha) && c.IdPsicologo.Equals(id)).ToList();
                      return new ConsultarResponse(citasPsicologo);
                  }

              }catch(Exception e){
                  return new ConsultarResponse($"Error en la aplicación,{e.Message}");
              }
          }

          public ConsultarAgendaResponse ConsultarAgenda(string dia, string fecha ){
                try{
                      List<Agenda> agendasDisponibles = new List<Agenda>();
                      var citasPsicologo = _consultorioContext.Agendas.Where(a => a.Dia.Equals(dia) && a.Estado==false).ToList();
                      var citas = _consultorioContext.citas.ToList();
                      if(citas.Count>0){
                          foreach (var item in citasPsicologo)
                            {
                          foreach (var c in citas)
                          {
                              if(c.CodigoAgenda.Equals(item.CodigoAgenda) && c.Fecha.Equals(fecha) && c.Estado.Equals("ATENDIDA")){
                                  agendasDisponibles.Add(item);
                              } else if(!c.CodigoAgenda.Equals(item.CodigoAgenda+"")){
                                  agendasDisponibles.Add(item);
                              }
                          }
                      }
                      } else{
                          agendasDisponibles=citasPsicologo;
                      }
                      
                      return new ConsultarAgendaResponse(agendasDisponibles);
                  

              }catch(Exception e){
                  return new ConsultarAgendaResponse($"Error en la aplicación,{e.Message}");
              }
          }

        public bool CambiarEstadoAgenda(string codigo){
            try{
                var response =_consultorioContext.Agendas.Find(codigo);
                response.Estado=!response.Estado;
                _consultorioContext.Agendas.Update(response);
                _consultorioContext.SaveChanges();
                return true;
            } catch(Exception e){
                return false;
            }

        }   



         public class ConsultarResponse{
             public ConsultarResponse(string mensaje)
             {
                 Error = true;
                 Mensaje = mensaje;
             }
             public ConsultarResponse(List<Cita> citas)
             {
                 Mensaje = "Consultado Correctamente";
                 Error =false;
                 Citas= citas;
             }
             public string Mensaje  { get; set; }
             public bool Error { get; set; }
             public List<Cita> Citas   { get; set; }
         }

         public class ConsultarAgendaResponse{
             public ConsultarAgendaResponse(string mensaje)
             {
                 Error = true;
                 Mensaje = mensaje;
             }
             public ConsultarAgendaResponse(List<Agenda> agendas)
             {
                 Mensaje = "Consultado Correctamente";
                 Error =false;
                 Agendas= agendas;
             }
             public string Mensaje  { get; set; }
             public bool Error { get; set; }
             public List<Agenda> Agendas   { get; set; }
         }

    }


    
}
