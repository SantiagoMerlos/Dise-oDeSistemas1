using System.ComponentModel.DataAnnotations;

namespace contrato.entidades
{
    public class Asociado{ 
    public int Id { get; set; }
    public string  Nombre { get; set; }
    public string  Apellido { get; set; }

    public string Correo { get; set; }

    public string contrasenia { get; set; }

    public string Telefono { get; set; }
    }
    
}