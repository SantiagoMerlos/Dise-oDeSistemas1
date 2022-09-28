using System.Threading.Tasks;
using contrato.entidades;
using contrato.servicios.cliente.solicitudes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using contrato.servicios.cliente.respuestas;
using pobreTito.Services;
using System.Linq;
using Newtonsoft.Json;

namespace pobreTito.Controllers
{
    [ApiController]
    [Route("/api/[Controller]")]


    public class AsociadoController : Controller
    {

//ASOCIADO

    //Metodo para validar las credenciales del inicio de sesión
        [HttpGet]
        public async Task<RespuestaInicioSesion> Get([FromQuery] SolicitudInicioSesion request) => pobreTitoServices.ValidarInicioSesion(request);
    

    //Metodo para la registración de un nuevo usuario
        [HttpPost]
        public async Task<RespuestaRegistrarUsuario> Post([FromBody] SolicitudRegistrarUsuario request) => pobreTitoServices.RegistrarUser(request);

//INCIDENTE

    //Metodo para registrar incidentes
        [HttpPost]
        [Route("RegisterIncident")]
        public async Task<RespuestaRegistrarIncidente> Post([FromBody] SolicitudRegistrarIncidente request) => pobreTitoServices.RegistrarIncidente(request);


    //Metodo para obtener los incidentes registrados 
        [HttpGet]
        [Route("ObtenerRegistros")]
        public async Task<RespuestaObteneRegistros> Get([FromQuery] SolicitudObtenerRegistros request) => pobreTitoServices.GetObtenerRegistros(request);


} }