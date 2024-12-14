using System;
using System.Collections.Generic;

namespace Movie.Models;

public partial class Mufaj
{
    public int Id { get; set; }

    public string Nev { get; set; } = null!;

    public string RovidNev { get; set; } = null!;

    public virtual ICollection<Film> Films { get; set; } = new List<Film>();
}
