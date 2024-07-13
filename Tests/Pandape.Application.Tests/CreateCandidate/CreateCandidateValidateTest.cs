using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Pandape.Application.CreateCandidate
{
    public class CreateCandidateValidateTest
    {
        private CreateCandidateValidate? _validator;
        [SetUp]
        public void SetUp() 
        {
            _validator = new CreateCandidateValidate();
        }

        [Test]
        public void ErrorWhenNameIsEmpty()
        {
            var candidate = new CreateCandidateCommand
            {
                Name = string.Empty,
                Surname = "Doe",
                Email = "john.doe@example.com",
                Birthdate = new DateTime(2000, 1, 1)
            };
            var result = _validator.TestValidate(candidate);
            var err = result.ShouldHaveValidationErrorFor(x => x.Name).FirstOrDefault();
            Assert.That(err.ErrorMessage, Is.EqualTo("Name is requiered."));
        }
    }
}
