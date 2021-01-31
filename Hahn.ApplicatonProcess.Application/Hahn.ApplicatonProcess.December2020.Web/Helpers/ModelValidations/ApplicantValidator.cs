using FluentValidation;
using Hahn.ApplicatonProcess.December2020.Domain.Models;
using Hahn.ApplicatonProcess.December2020.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Helpers.ModelValidations
{
    public class ApplicantValidator : AbstractValidator<ApplicantModel>
    {
        public ApplicantValidator()
        {

            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Applicant Name is required")
                .MinimumLength(5).WithMessage("Name length must be at least 5 Character long.");
            RuleFor(x => x.FamilyName).NotEmpty()
                .WithMessage("Applicant FamilyName is required")
                .MinimumLength(5).WithMessage("FamilyName length must be at least 5 Character long.");
            RuleFor(x => x.Address).NotEmpty()
                .WithMessage("Applicant Address is required")
                .MinimumLength(10).WithMessage("Address length must be at least 10 Character long.");
            RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("Email address is required")
                     .EmailAddress().WithMessage("A valid email is required");
            RuleFor(x => x.Age).InclusiveBetween(20, 60)
                .WithMessage("Applicant Age range must be between 20 and 60.");
            RuleFor(x => x.Hired).NotNull()
               .WithMessage("Applicant Hired status is required");
            RuleFor(x => x.CountryofOrigin).MustAsync(async (country, cancellation) =>
            {
                (bool valid, string name) = await new CountryValidatorService().ValidateCountryName(country);
                return valid;
            }).WithMessage($"CountryOfOrigin must be a valid country name");
        }
    }
}
