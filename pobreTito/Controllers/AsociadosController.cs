using System.Threading.Tasks;
using contrato.entidades;
using contrato.servicios.cliente.solicitudes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using contrato.servicios.cliente.respuestas;
using System.Linq;
using Newtonsoft.Json;

namespace capacitacion.Controllers
{
    [ApiController]
    [Route("/api/[Controller]")]

    //ASOCIADO
    public class AsociadoController : Controller
    {
        public List<Asociado> asociados { get; set; }
        public const string DIRECCION_CLIENTE = "db/clientes.json"; 
        public AsociadoController()
        {

        }

        private void GuardarAsociado(Asociado? asociado)
    {
        if(asociado != null) asociados.Add(asociado);
        string temp = JsonConvert.SerializeObject(asociados);        
        System.IO.File.WriteAllText(DIRECCION_CLIENTE, temp);
    } 
     private void CargarAsociados()
    {
        string json = System.IO.File.ReadAllText(DIRECCION_CLIENTE);
        asociados = JsonConvert.DeserializeObject<List<Asociado>>(json);
    
        if(asociados == null) asociados = new List<Asociado>();
    
    }
//INCIDENTE
        public List<Incidente> incidentes { get; set; }
        public const string DIRECCION_INCIDENTE = "db/incidentes.json"; 

        private void GuardarIncidente(Incidente? incidente)
    {
        if(incidente != null) incidentes.Add(incidente);
        string temp = JsonConvert.SerializeObject(incidentes);        
        System.IO.File.WriteAllText(DIRECCION_INCIDENTE, temp);
    } 
     private void CargarIncidentes()
    {
        string json = System.IO.File.ReadAllText(DIRECCION_INCIDENTE);
        incidentes = JsonConvert.DeserializeObject<List<Incidente>>(json);
    
        if(incidentes == null) incidentes = new List<Incidente>();
    
    }





//ASOCIADO

        [HttpGet]
        public async Task<RespuestaInicioSesion> Get([FromQuery] SolicitudInicioSesion solicitud)
        {
            CargarAsociados();
            var user = new Asociado();
            var respuesta = new RespuestaInicioSesion();
                user = asociados.Find(x=> x.Correo == solicitud.correo && x.contrasenia == solicitud.contrasenia);
                if(user != null){
                    respuesta.Asociado = user;
                }else{
                    respuesta.ErrorMessage = "Datos erroneos o usuario inexistente";
                }
            return respuesta;
        }


     [HttpPost]

        public async Task<RespuestaRegistrarUsuario> Post([FromBody] SolicitudRegistrarUsuario solicitud)
        {
            CargarAsociados();
            var respuesta = new RespuestaRegistrarUsuario();

            var statusCorreo = 0;
            var max=0;

            foreach (var asoc in asociados)
            {
                if (asoc.Id >= max)
                {
                    max= asoc.Id;
                }
                if(asoc.Correo == solicitud.Correo){
                    statusCorreo= 1;
                }
            }
            
            if (statusCorreo==0){
            var idPerson = max + 1;
            var clienteNuevo = new Asociado();
            
            clienteNuevo.Id = idPerson;
            clienteNuevo.Nombre = solicitud.Nombre;
            clienteNuevo.Apellido = solicitud.Apellido;
            clienteNuevo.contrasenia = solicitud.contrasenia;
            clienteNuevo.Correo = solicitud.Correo;
            clienteNuevo.Telefono = solicitud.Telefono;
            
            respuesta.Asociado = clienteNuevo;
            GuardarAsociado(clienteNuevo);
            return respuesta;

            }else{
                respuesta.errorMessage = "El correo ya pertenece a un usuario existente";
                return respuesta;
            }
            return respuesta;

        }  
//INCIDENTE+

    [HttpPost]
    [Route("RegisterIncident")]
    public async Task<RespuestaRegistrarIncidente> Post([FromBody] SolicitudRegistrarIncidente solicitud)
    { 
        CargarIncidentes();

        var respuesta = new RespuestaRegistrarIncidente();

        var RegistroIncidentes = new Incidente();

        RegistroIncidentes.Id = solicitud.Id;
        RegistroIncidentes.Nombre = solicitud.Nombre;
        RegistroIncidentes.Apellido = solicitud.Apellido;
        RegistroIncidentes.Telefono = solicitud.Telefono;
        RegistroIncidentes.fechaSuccess = solicitud.fechaSuccess;
        RegistroIncidentes.barrio = solicitud.barrio;
        RegistroIncidentes.direccionIncident = solicitud.direccionIncident;
        RegistroIncidentes.detalleIncident = solicitud.detalleIncident;

        GuardarIncidente(RegistroIncidentes);

        respuesta.incidente = RegistroIncidentes;
        return respuesta;
    }

    [HttpGet]
    [Route("ObtenerRegistros")]
    public async Task<RespuestaObteneRegistros> GetObtenerRegistros([FromQuery] SolicitudObtenerRegistros solicitud)
        {
            CargarIncidentes();
            var user = new List<Incidente> ();
            var respuesta = new RespuestaObteneRegistros();

                user = incidentes.FindAll(x=> x.Id == solicitud.Id);

                if(user != null){
                    respuesta.incidente = user;
                }else{
                    respuesta.CondicionRegistros = "No tiene registros previos.";
                }
            return respuesta;
        }
/*
        [HttpPut]
        public async Task<RespuestaActualizarAsociado> Put([FromBody] SolicitudActualizarAsociado solicitud)
        {
            CargarAsociados();
        
            var respuesta = new RespuestaActualizarAsociado();

            var cliente= this.asociados;

            bool existeCliente = cliente.Exists(x=>x.Id == solicitud.Id);

            if(existeCliente)
            {
                cliente.Remove(cliente.Find(x => x.Id == solicitud.Id));

                var clienteNuevo = new Asociado();

                if( solicitud.medicamentos != true && solicitud.enfermedades != true ){
                
                    clienteNuevo.esDonante = true;

                }else{
                    clienteNuevo.esDonante = false;
                }

                
                clienteNuevo.Id = solicitud.Id;
                clienteNuevo.Apellido = solicitud.Apellido;
                clienteNuevo.Nombre = solicitud.Nombre;
                clienteNuevo.Correo = solicitud.Correo;
                clienteNuevo.medicamentos = solicitud.medicamentos;
                clienteNuevo.enfermedades = solicitud.enfermedades;
                clienteNuevo.tipoSangre = solicitud.tipoSangre;
                
                
                respuesta.Asociados = clienteNuevo;
                GuardarAsociado(clienteNuevo);
                return respuesta;
            }else{

                respuesta.ErrorMensaje = "El usuario no esta en la base de datos.";
                return respuesta;
            }

        }

        [HttpDelete]
        public async Task<RespuestaEliminarAsociado> Delete ([FromBody] SolicitudEliminarAsociado solicitud)
        {
            CargarAsociados();
        
            var respuesta = new RespuestaEliminarAsociado();

            var cliente= this.asociados;

            bool existeCliente = cliente.Exists(x=>x.Id == solicitud.Id);

            if(existeCliente)
            {
                cliente.Remove(cliente.Find(x => x.Id == solicitud.Id));

                
                respuesta.MensajeExito = "Usuario eliminado con exito!!" ;
                GuardarAsociado(null);

                return respuesta;

            }else{

                respuesta.MensajeError = "El usuario no esta en la base de datos.";
                return respuesta;
            }
        } 
    }   */
} }