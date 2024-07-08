using System;
using System.Collections.Generic;

namespace ComercioComputadora.Models;

public partial class Computadora
{
    public int Id { get; set; }

    public int Categoria { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string Imagen { get; set; } = null!;

    public double Precio { get; set; }
}
