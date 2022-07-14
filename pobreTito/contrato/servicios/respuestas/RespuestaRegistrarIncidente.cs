namespace contrato.servicios.cliente.respuestas
{
    public class RespuestaRegistrarIncidente
    {
        public contrato.entidades.Incidente incidente { get; set; }
        public string errorMessage { get; set; }
    }
}