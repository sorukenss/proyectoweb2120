using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using Entity;

namespace BLL{

    public class UtilidadConsultorioService{
        
        private ConsultorioContext _consultorioContext;
        public UtilidadConsultorioService(ConsultorioContext context)
        {
            _consultorioContext=context;
        }
       
       public SaveResponseUtilidad GuardarUtilidad(double valorcita,double ganancia){
           UtilidadConsultorio utilidad=new UtilidadConsultorio();
           try{
               utilidad.ValorCita=valorcita;
               utilidad.Ganancia=ganancia;
                _consultorioContext.Add(utilidad);
                _consultorioContext.SaveChanges();
                 return new SaveResponseUtilidad(utilidad);
           }catch (Exception e){
                 return new SaveResponseUtilidad($"Error:{e.Message}");
           }
       }

    }

    public class SaveResponseUtilidad{
           
           public SaveResponseUtilidad(string mensaje){
               Error=true;
               Mensaje=mensaje;
           }
           public SaveResponseUtilidad(UtilidadConsultorio utilidad){
               Error=false;
               Utilidad=utilidad;
           }
        public bool Error { get; set; }
        public string Mensaje { get; set; }

        public UtilidadConsultorio Utilidad { get; set; }
    }
}