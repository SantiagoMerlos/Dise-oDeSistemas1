namespace contrato.servicios.cliente.respuestas
{
    public class RespuestaInicioSesion
    {
        public contrato.entidades.Asociado Asociado { get; set; }

        public string ErrorMessage { get; set; }
    }
}