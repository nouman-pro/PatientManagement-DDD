using Microsoft.EntityFrameworkCore;
using Patients.Domain.Abstractions;
using Patients.Domain.Aggregates;

namespace Patients.Infrastructure.Persistence.Repositories;

internal sealed class PatientRepository : IPatientRepository
{
    private readonly PatientsDbContext _context;

    public PatientRepository(PatientsDbContext context) => _context = context;

    public void Add(Patient patient) => _context.Patients.Add(patient);

    public Task<Patient?> GetByIdAsync(PatientId id, CancellationToken cancellationToken = default) =>
        _context.Patients.FirstOrDefaultAsync(patient => patient.Id == id, cancellationToken);
}
