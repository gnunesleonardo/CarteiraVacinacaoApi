using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Domain.Entities
{
    public class Vaccine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? DosesRequired { get; set; }

        protected Vaccine() { }

        public Vaccine(string name, int requiredDoses)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Nome da vacina é obrigatório.");

            Name = name;
            DosesRequired = requiredDoses;
        }
    }
}
