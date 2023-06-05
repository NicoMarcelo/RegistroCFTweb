using System;
using System.Collections.Generic;

namespace RegistroCFTweb.Models;

public partial class Asignaturasestudiante
{
    public int Id { get; set; }

    public int EstudianteId { get; set; }

    public int AsignaturaId { get; set; }

    public DateOnly? FechaRegistro { get; set; }

    public virtual Asignatura Asignatura { get; set; } = null!;

    public virtual Estudiante Estudiante { get; set; } = null!;
}
