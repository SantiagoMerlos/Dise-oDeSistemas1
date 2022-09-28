using contrato.entidades;
using contrato.servicios.cliente.solicitudes;
using System.Collections.Generic;
using contrato.servicios.cliente.respuestas;
using System.Linq;
using Newtonsoft.Json;

namespace pobreTito.Services{ 

       public static class pobreTitoServices
       {

            public static List<Asociado> asociados { get; set; }
            public const string DIRECCION_CLIENTE = "db/clientes.json"; 

            //JSON GENERADO PARA GUARDAR LOS USUARIOS.
            private static void GuardarAsociado(Asociado? asociado)
                {
                    if(asociado != null) asociados.Add(asociado);
                    string temp = JsonConvert.SerializeObject(asociados);        
                    System.IO.File.WriteAllText(DIRECCION_CLIENTE, temp);
                } 
            //JSON GENERADO PARA CARGAR LOS ASOCIADOS Y PODER HACER MODIFICACIONES.
            private static void CargarAsociados()
                {
                string json = System.IO.File.ReadAllText(DIRECCION_CLIENTE);
                asociados = JsonConvert.DeserializeObject<List<Asociado>>(json);
            
                if(asociados == null) asociados = new List<Asociado>();
            
                }
            //INCIDENTE
            public static List<Incidente> incidentes { get; set; }
            public const string DIRECCION_INCIDENTE = "db/incidentes.json"; 

            private static void GuardarIncidente(Incidente? incidente)
                {
                if(incidente != null) incidentes.Add(incidente);
                string temp = JsonConvert.SerializeObject(incidentes);        
                System.IO.File.WriteAllText(DIRECCION_INCIDENTE, temp);
                } 
            private static void CargarIncidentes()
                {
                string json = System.IO.File.ReadAllText(DIRECCION_INCIDENTE);
                incidentes = JsonConvert.DeserializeObject<List<Incidente>>(json);
            
                if(incidentes == null) incidentes = new List<Incidente>();
                }   


            //SE OBTIENEN TODOS LOS REGISTROS ASOCIADOS A UNA PERSONA, VAN A TENER EL MISMO ID QUE EL USUARIO.
            public static RespuestaObteneRegistros GetObtenerRegistros(SolicitudObtenerRegistros request)
            {
                CargarIncidentes();
                var user = new List<Incidente> ();
                var respuesta = new RespuestaObteneRegistros();

                    user = incidentes.FindAll(x=> x.Id == request.Id);
                    
                var status = 0;
                    foreach (var i in user)
                    {
                        status = 1;
                    }

                    if(status == 1){
                        respuesta.incidente = user;
                    }else{
                        respuesta.CondicionRegistros = "No tiene registros previos.";
                    }
                return respuesta;
            }

            public static RespuestaInicioSesion ValidarInicioSesion (SolicitudInicioSesion request)
            { 
                CargarAsociados();
                var user = new Asociado();
                var respuesta = new RespuestaInicioSesion();
                    user = asociados.Find(x=> x.Correo == request.correo && x.contrasenia == request.contrasenia);
                    if(user != null){
                        respuesta.Asociado = user;
                    }else{
                        respuesta.ErrorMessage = "Datos erroneos o usuario inexistente";
                    }
                return respuesta;
            }


            public static RespuestaRegistrarUsuario RegistrarUser (SolicitudRegistrarUsuario request)
            {
                CargarAsociados();
                var respuesta = new RespuestaRegistrarUsuario();

                var statusCorreo = 0;
                var max=0;

                foreach (var asoc in asociados)
                {
                    //ASIGNA AUTOMATICAMENTE EL ID DEL USUARIO DE FORMA ASCENDENTE.
                    if (asoc.Id >= max)
                    {
                        max= asoc.Id;
                    }
                    //VALIDA QUE EL CORREO QUE QUIERE REGISTRAR NO SEA IGUAL A UN EXISTENTE.
                    if(asoc.Correo == request.Correo){
                        statusCorreo= 1;
                    }
                }
                
                if (statusCorreo==0){
                var idPerson = max + 1;
                var clienteNuevo = new Asociado();
                
                clienteNuevo.Id = idPerson;
                clienteNuevo.Nombre = request.Nombre;
                clienteNuevo.Apellido = request.Apellido;
                clienteNuevo.contrasenia = request.contrasenia;
                clienteNuevo.Correo = request.Correo;
                clienteNuevo.Telefono = request.Telefono;
                
                respuesta.Asociado = clienteNuevo;
                GuardarAsociado(clienteNuevo);
                return respuesta;

                }else{
                    respuesta.errorMessage = "El correo ya pertenece a un usuario existente";
                    return respuesta;
                }
                return respuesta;

            }
        
            public static RespuestaRegistrarIncidente RegistrarIncidente (SolicitudRegistrarIncidente request)
            {
                CargarIncidentes();

                var respuesta = new RespuestaRegistrarIncidente();

                var RegistroIncidentes = new Incidente();

                RegistroIncidentes.Id = request.Id;
                RegistroIncidentes.Nombre = request.Nombre;
                RegistroIncidentes.Apellido = request.Apellido;
                RegistroIncidentes.Telefono = request.Telefono;
                RegistroIncidentes.fechaSuccess = request.fechaSuccess;
                RegistroIncidentes.barrio = request.barrio;
                RegistroIncidentes.direccionIncident = request.direccionIncident;
                RegistroIncidentes.detalleIncident = request.detalleIncident;

                GuardarIncidente(RegistroIncidentes);

                respuesta.incidente = RegistroIncidentes;
                return respuesta;
            }
       }


}