using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace contrato.servicios.cliente.solicitudes
{
    public class SolicitudRegistrarUsuario
    {
        [Required]
    public int Id { get; set; }
    public string  Nombre { get; set; }
    public string  Apellido { get; set; }

    public string Correo { get; set; }

    public string contrasenia { get; set; }

    public string Telefono { get; set; }
    }
}
