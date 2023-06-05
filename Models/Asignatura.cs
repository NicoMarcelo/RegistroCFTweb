using System;
using System.Collections.Generic;

namespace RegistroCFTweb.Models;

public partial class Asignatura
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string Codigo { get; set; } = null!;

    public DateOnly? FechaActualizacion { get; set; }

    public virtual ICollection<Asignaturasestudiante> Asignaturasestudiantes { get; set; } = new List<Asignaturasestudiante>();

    public virtual ICollection<Notas> Nota { get; set; } = new List<Notas>();
}
