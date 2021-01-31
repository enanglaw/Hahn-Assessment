using Hahn.ApplicatonProcess.December2020.Domain.Entities;
using Hahn.ApplicatonProcess.December2020.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Interfaces
{
    public interface IApplicantService
    {
        Task<Applicant> AddApplicant(ApplicantModel applicant);
        Task<Applicant> GetApplicant(int applicantId);
        Task<bool> UpdateApplicant(int id, Applicant updatedInfo);
        Task<bool> DeleteApplicant(int applicantId);
        Task<IEnumerable<Applicant>> GetApplicants();
        Task<IEnumerable<Applicant>> GetApplicants(Func<Applicant, bool> predicate);
    }
}
