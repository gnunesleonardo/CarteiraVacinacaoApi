using CarteiraVacinacaoApi.Application.Interfaces;
using CarteiraVacinacaoApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Application.Commands.DeleteVaccineRecord
{
    public class DeleteVaccineRecordHandler : IRequestHandler<DeleteVaccineRecordCommand>
    {
        private readonly IVaccineRecordRepository _vaccineRecordRepository;

        public DeleteVaccineRecordHandler(IVaccineRecordRepository vaccineRecordRepository)
        {
            _vaccineRecordRepository = vaccineRecordRepository;
        }

        public async Task Handle(DeleteVaccineRecordCommand request, CancellationToken cancellationToken)
        {
            var vaccineRecord = await _vaccineRecordRepository.GetById(request.Id);
            if (vaccineRecord == null)
                throw new KeyNotFoundException("Registro de vacinação não encontrado.");

            var lastDoseApplied = await _vaccineRecordRepository.GetLastDoseApplied(vaccineRecord.PersonId, vaccineRecord.VaccineId);
            if (vaccineRecord.DoseNumber != lastDoseApplied)
                throw new ArgumentException("Só é permitida excluir a última dose aplicada.");

            await _vaccineRecordRepository.Remove(vaccineRecord);
        }
    }
}
