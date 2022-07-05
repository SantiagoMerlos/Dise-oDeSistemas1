namespace contrato.servicios.cliente.respuestas
{
    public class RespuestaRegistrarUsuario
    {
        public contrato.entidades.Asociado Asociado { get; set; }
        public string errorMessage { get; set; }
    }
}