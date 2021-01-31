using Hahn.ApplicatonProcess.December2020.Domain.Entities;
using Hahn.ApplicatonProcess.December2020.Domain.Interfaces;
using Hahn.ApplicatonProcess.December2020.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Services
{
    public class ApplicantService : IApplicantService
    {
        readonly IRepository<Applicant> _applicantsRepository;
        public ApplicantService(IRepository<Applicant> repository)
        {
            _applicantsRepository = repository;
        }

        public async Task<Applicant> AddApplicant(ApplicantModel applicantModel)
        {
            try
            {

                var applicant = BuildApplicantEntity(applicantModel);
                return await _applicantsRepository.AddItem(applicant);
            }
            catch (Exception exception)
            {
                throw;
            }
        }


        public async Task<bool> DeleteApplicant(int applicantId)
        {
            try
            {
                return await _applicantsRepository.RemoveItem(applicantId);
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public async Task<Applicant> GetApplicant(int applicantId)
        {
            try
            {
                return await _applicantsRepository.GetItem(applicantId);
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Applicant>> GetApplicants()
        {
            try
            {
                return await _applicantsRepository.GetItems();
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Applicant>> GetApplicants(Func<Applicant, bool> predicate)
        {
            try
            {
                return await _applicantsRepository.GetItems(predicate);
            }
            catch (Exception exception)
            {
                return Enumerable.Empty<Applicant>();
            }
        }

        public async Task<bool> UpdateApplicant(int id, Applicant updatedInfo)
        {
            try
            {
                var applicantToUpdate = await GetApplicant(id);
                if (applicantToUpdate == null)
                    return false;
                applicantToUpdate.Address = updatedInfo.Address;
                applicantToUpdate.Age = updatedInfo.Age;
                applicantToUpdate.CountryofOrigin = updatedInfo.CountryofOrigin;
                applicantToUpdate.EmailAddress = updatedInfo.EmailAddress;
                applicantToUpdate.FamilyName = updatedInfo.FamilyName;
                applicantToUpdate.Hired = updatedInfo.Hired;
                applicantToUpdate.Name = updatedInfo.Name;
                return (await _applicantsRepository.UpdateItem(id, applicantToUpdate)) != null;
            }
            catch (Exception exception)
            {
                throw;
            }
        }






        #region privates


        private Applicant BuildApplicantEntity(ApplicantModel applicantModel)
        {
            return new Applicant
            {
                Address = applicantModel.Address,
                EmailAddress = applicantModel.EmailAddress,
                Age = applicantModel.Age,
                CountryofOrigin = applicantModel.CountryofOrigin,
                FamilyName = applicantModel.FamilyName,
                Hired = applicantModel.Hired,
                Name = applicantModel.Name
            };
        }
        #endregion



    }
}
