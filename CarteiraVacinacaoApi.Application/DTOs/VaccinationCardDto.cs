using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Application.DTOs
{
    public class VaccinationCardDto
    {
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public List<VaccineRecordDto> VaccineRecords { get; set; } = new();
    }
}
