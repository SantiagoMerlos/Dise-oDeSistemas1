using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace contrato.servicios.cliente.solicitudes
{
    public class SolicitudRegistrarIncidente
    {
        [Required]
    public int Id { get; set; }
    public string  Nombre { get; set; }
    public string  Apellido { get; set; }
    public string Telefono { get; set; }

    public string fechaSuccess { get; set; }
    public string barrio { get; set; }

    public string direccionIncident { get; set; }

    public string detalleIncident { get; set; } 
    }
}