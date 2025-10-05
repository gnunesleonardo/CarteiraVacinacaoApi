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
        public int MaxDoses { get; set; }

        protected Vaccine() { }

        public Vaccine(string name, int maxDoses)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Nome da vacina é obrigatório.");

            if (maxDoses <= 0)
                throw new ArgumentException("Número máximo de doses deve ser maior que zero.");

            Name = name;
            MaxDoses = maxDoses;
        }
    }
}
