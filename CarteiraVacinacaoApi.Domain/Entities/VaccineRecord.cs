using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Domain.Entities
{
    public class VaccineRecord
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int VaccineId { get; set; }
        public int DoseNumber { get; set; }
        public DateTime AppliedDate { get; set; }

        public Person Person { get; set; }
        public Vaccine Vaccine { get; set; }

        protected VaccineRecord() { }

        public VaccineRecord(int personId, int vaccineId, int doseNumber, DateTime appliedDate)
        {
            if (doseNumber <= 0)
            {
                throw new ArgumentException("Número da dose aplicada deve ser maior que zero.");
            }
            
            PersonId = personId;
            VaccineId = vaccineId;
            DoseNumber = doseNumber;
            AppliedDate = appliedDate;
        }
    }
}
