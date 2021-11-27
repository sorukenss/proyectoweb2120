using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace BLL
{
    public class UserService
    {
        private ConsultorioContext _consultorioContext;
        public UserService(ConsultorioContext consultorioContext)
        {
            _consultorioContext = consultorioContext;
        }

        public SearchUser BuscarUsuario(string username, string password){
            SearchUser searchUser = new SearchUser();
            try{
                Usuario user;
                var paciente = _consultorioContext.pacientes.Include(p=> p.Persona).Where(p=> p.Persona.Usuario.Equals(username) && p.Persona.password.Equals(password) && p.CodigoPaciente.Equals(p.Persona.Identification)).FirstOrDefault();
                if(paciente!=null){
                    user = PersonaAsignar(paciente.Persona,"PACIENTE");
                    searchUser.Error=false;
                    searchUser.User=user;
                    searchUser.Mensaje="Sesión iniciada";
                } else{
                    var psicologo = _consultorioContext.psicologos.Include(p=> p.Persona).Where(p=> p.Persona.Usuario.Equals(username) && p.Persona.password.Equals(password) && p.CodigoPsicologo.Equals(p.Persona.Identification)).FirstOrDefault();
                    if(psicologo != null){
                        user = PersonaAsignar(psicologo.Persona, "PSICOLOGO");
                        searchUser.Error=false;
                        searchUser.User=user;
                        searchUser.Mensaje="Sesión iniciada";
                    }else{
                        searchUser.Error=true;
                        searchUser.User=null;
                        searchUser.Mensaje="No se encuentra el usuario o contraseña incorrecta";
                    }
                }
            } catch (Exception e){
                searchUser.Error=true;
                searchUser.User=null;
                searchUser.Mensaje=$"Error en la aplicación:{e.Message} ";
            } 
             return searchUser;
        }

        Usuario PersonaAsignar (Persona persona,string Rol){
                Usuario user = new Usuario();
                user.User=persona.Usuario;
                user.Password=persona.password;
                user.Nombre=persona.Nombre;
                user.Apellidos=persona.Apellido;
                user.IdPersona=persona.Identification;
                user.Rol=Rol;
                return user;
        }



        public class SearchUser{
            public string Mensaje { get; set; }
            public bool Error { get; set; }
            public Usuario User { get; set; }
        }
        
    }
}