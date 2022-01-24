using System;
using System.Collections.Generic;

#nullable disable

namespace Practica1Aplicaciones.Domain.Models
{
    public partial class Moovy
    {
        public int IdMoovie { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Gender { get; set; }
        public double Score { get; set; }
        public string Rating { get; set; }
        public int PublicationYear { get; set; }
    }
}
