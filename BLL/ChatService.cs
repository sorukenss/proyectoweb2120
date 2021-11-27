using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using Entity;




namespace BLL
{
    public class ChatService
    {

        private ConsultorioContext _consultorioContext;

        public ChatService(ConsultorioContext context)
        {
            _consultorioContext = context;
        }

        public SaveResponseChat GuardarChat(Chat chat)
        {

            try
            {
                var paciente = _consultorioContext.pacientes.Find(chat.CodigoPaciente);
                var psicologo = _consultorioContext.psicologos.Find(chat.CodigoPsicologo);
                if (paciente != null && psicologo != null)
                {
                    _consultorioContext.Add(chat);
                    _consultorioContext.SaveChanges();
                    return new SaveResponseChat(chat);
                }
                else if (paciente == null)
                {
                    return new SaveResponseChat($"No se Encontro el paciente");
                }
                else return new SaveResponseChat($"No se Encontro el Psicologo");



            }
            catch (Exception e)
            {
                return new SaveResponseChat($"Error : {e.Message}");
            }
        }

        public ConsultarResponseChat ConsultarChats(){

          try{
              var chats= _consultorioContext.Chats.ToList();
              if(chats!= null){
                  return new ConsultarResponseChat(chats);
              } else return new ConsultarResponseChat("No se Encontro");
              
          }catch (Exception e){
                return new ConsultarResponseChat($"Error{e.Message}");
          }
        }
       


    }
     public class ConsultarResponseChat{
            
            public ConsultarResponseChat(List<Chat> chat)
            {
                Error = false;
                Chat = chat;
            }

            public ConsultarResponseChat(string mensaje)
            {
                Error = true;
                Mensaje = mensaje;
            }

            public bool Error { get; set; }

            public string Mensaje { get; set; }

            public List<Chat> Chat { get; set; }
        }


        public class SaveResponseChat
        {


            public SaveResponseChat(Chat chat)
            {
                Error = false;
                Chat = chat;
            }

            public SaveResponseChat(string mensaje)
            {
                Error = true;
                Mensaje = mensaje;
            }

            public bool Error { get; set; }

            public string Mensaje { get; set; }

            public Chat Chat { get; set; }
        }


}