using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace contrato.servicios.cliente.solicitudes
{
    public class SolicitudInicioSesion
    {
        [Required]
        //public int Id { get; set; }

        public string correo { get; set; }

        public string contrasenia { get; set; }
        
    }
}
