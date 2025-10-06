using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Application.DTOs
{
    public class VaccineRecordDto
    {
        public int RecordId { get; set; }
        public int DoseNumber { get; set; }
        public DateTime AppliedDate { get; set; }
        public VaccineDto Vaccine { get; set; }
    }
}
