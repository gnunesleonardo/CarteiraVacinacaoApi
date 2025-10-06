using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Application.DTOs
{
    public class VaccineDto
    {
        public int VaccineId { get; set; }
        public string VaccineName { get; set; }
        public int DosesRequired { get; set; }
    }
}
