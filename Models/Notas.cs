using System;
using System.Collections.Generic;

namespace RegistroCFTweb.Models;

public partial class Notas
{
    public int Id { get; set; }

    public int AsignaturaId { get; set; }

    public int EstudianteId { get; set; }

    public float Calificacion { get; set; }

    public float Ponderacion { get; set; }

    public virtual Asignatura Asignatura { get; set; } = null!;

    public virtual Estudiante Estudiante { get; set; } = null!;
}
