using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Domain.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<VaccineRecord> VaccineRecords { get; set; }

        public Person() { }

        public Person(int id)
        {
            Id = id;
        }

        public Person(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Nome da pessoa é obrigatório.");

            Name = name;
        }
    }
}
