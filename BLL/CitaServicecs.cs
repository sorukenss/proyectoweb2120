using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace BLL
{
    public class CitaService
    {
        private ConsultorioContext _consultorioContext;
        private UtilidadConsultorioService _utilidadService;
        private AgendaService _agendaService;

        public CitaService(ConsultorioContext context)
        {
            _consultorioContext = context;
            _agendaService  = new AgendaService(context);
        }

        public SaveResponse GuardarCita(Cita cita)
        {
            try
            {
                var pacienteResponse = _consultorioContext.pacientes.Find(cita.IdPaciente);

                if (pacienteResponse != null)
                {
                    cita.Estado="PENDIENTE";
                    cita.IdPsicologo=_consultorioContext.Agendas.Find(Convert.ToInt32(cita.CodigoAgenda)).IdPsicologo;
                    _consultorioContext.citas.Add(cita);
                    _consultorioContext.SaveChanges();
                    return new SaveResponse(cita);
                }else
                 return new SaveResponse("El paciente no existe", "ERROR");


            }
            catch (Exception e)
            {
                return new SaveResponse($"Error aplicación: {e.Message}", "ERROR");
            }
        }

        public AtenderCitaResponse AtenderCita (Cita cita){
            try{
                var response = _consultorioContext.personas.Find(cita.IdPaciente);
                if(response != null){
                    response =  _consultorioContext.personas.Find(cita.IdPsicologo);
                    if(response != null){
                        if(cita.Tratamiento!=null){
                            _consultorioContext.tratamientos.Add(cita.Tratamiento);
                            _consultorioContext.SaveChanges();
                            cita.IdTratamiento=cita.Tratamiento.CodigoTratamiento;
                        }
                        if(cita.Duracion>20){
                            if((cita.Duracion-20)<10){
                                cita.ValorCita=50000+(50000*0.1);
                                
                            } else if ((cita.Duracion-20)>10 && (cita.Duracion-20)<20 ){
                                cita.ValorCita=50000+(50000*0.2);
                            } else if ((cita.Duracion-20)>30){
                                cita.ValorCita=50000+(50000*0.3);
                            }
                        } else{
                            cita.ValorCita=50000;
                        }
                        
                        double ganancia = cita.ValorCita*0.3;
                        cita.Estado="ATENDIDA";
                        _consultorioContext.citas.Update(cita);
                        _consultorioContext.SaveChanges();
                        _utilidadService.GuardarUtilidad(cita.ValorCita,ganancia);
                        return new AtenderCitaResponse(cita);
                    } else return new AtenderCitaResponse("No existe el psicologo");
                } else return new AtenderCitaResponse("No existe el paciente");
            } catch (Exception ex){
                return new AtenderCitaResponse($"Error en la aplicación, {ex.Message}");
            }
        }


        public ConsultarResponse ConsultarCitas()
        {
            ConsultarResponse consultarResponse = new ConsultarResponse();
            try
            {
                consultarResponse.Error = false;
                consultarResponse.Mensaje = "Consultado correctamente";
                var citas = _consultorioContext.citas.ToList();
                foreach (var c in citas)
                {
                    c.Paciente = _consultorioContext.pacientes.Include(p => p.Persona).Where(p => p.CodigoPaciente.Equals(c.IdPaciente)).FirstOrDefault();

                }
                consultarResponse.Citas = citas;
            }
            catch (Exception e)
            {
                consultarResponse.Error = true;
                consultarResponse.Mensaje = $"Hubo un error al momento de consultar,{e.Message} ";
                consultarResponse.Citas = null;
            }
            return consultarResponse;
        }

        public ConsultarResponse ConsultarCitasPaciente(string id)
        {
            ConsultarResponse consultarResponse = new ConsultarResponse();
            try
            {
                consultarResponse.Error = false;
                consultarResponse.Mensaje = "Consultado correctamente";
                var citas = _consultorioContext.citas.Where(c => c.IdPaciente.Equals(id)).ToList();
                consultarResponse.Citas = citas;
            }
            catch (Exception e)
            {
                consultarResponse.Error = true;
                consultarResponse.Mensaje = $"Hubo un error al momento de consultar,{e.Message} ";
                consultarResponse.Citas = null;
            }
            return consultarResponse;
        }

        public ConsultarResponse ConsultarCitaId(string id)
        {
            ConsultarResponse consultarResponse = new ConsultarResponse();
            try
            {
                consultarResponse.Error = false;
                consultarResponse.Mensaje = "Consulta Correctamente";
                var cita = _consultorioContext.citas.Where(c => c.CodigoCita.Equals(id)).FirstOrDefault();
                consultarResponse.Cita = cita;

            }
            catch (Exception e)
            {
                consultarResponse.Error = true;
                consultarResponse.Mensaje = $"Hubo Un Error: {e.Message}";
                consultarResponse.Cita = null;
            }
            return consultarResponse;
        }



        public class ConsultarResponse
        {
            public bool Error { get; set; }
            public String Mensaje { get; set; }
            public List<Cita> Citas { get; set; }
            public Cita Cita { get; set; }
        }

        public class AtenderCitaResponse
        {
            public AtenderCitaResponse(Cita cita)
            {
                Error = false;
                Mensaje="Atendida correctamente";
                Cita = cita;
            }

            public AtenderCitaResponse(string mensaje)
            {
                Error = true;
                Mensaje=mensaje;
                Cita = null;
            }


            public bool Error { get; set; }
            public String Mensaje { get; set; }
            public Cita Cita { get; set; }
        }


        public class SaveResponse
        {

            public SaveResponse(Cita cita)
            {
                Error = false;
                Cita = cita;
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
            public Cita Cita { get; set; }
            public string Estado { get; set; }
        }
    }
}