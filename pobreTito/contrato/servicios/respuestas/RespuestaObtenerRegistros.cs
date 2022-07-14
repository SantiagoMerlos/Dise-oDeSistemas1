using System.Collections.Generic;
namespace contrato.servicios.cliente.respuestas
{
    public class RespuestaObteneRegistros
    {
        public List<contrato.entidades.Incidente> incidente { get; set; }
        public string CondicionRegistros { get; set; }
    }
}