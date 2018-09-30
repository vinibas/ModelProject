using System;

namespace Vini.ModelProject.Domain
{
    public class Usuário
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime CriadoEm { get; set; }
    }
}
