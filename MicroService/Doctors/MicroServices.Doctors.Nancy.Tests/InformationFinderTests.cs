using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using MicroServices.Common.Tests;
using MicroServices.DataAccess.DoctorsSlots.Interfaces;
using MicroServices.Doctors.Nancy.Interfaces;
using NSubstitute;
using NSubstitute.Core;
using Xunit;
using Xunit.Extensions;

namespace MicroServices.Doctors.Nancy.Tests
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class InformationFinderTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void FindById_ReturnsDoctor_ForExistingId([NotNull] IDoctor doctor)
        {
            // Arrange
            var repository = Substitute.For <IDoctorsRepository>();
            repository.FindById(doctor.Id).Returns(doctor);
            InformationFinder sut = CreateSut(repository);

            // Act
            IDoctorForResponse actual = sut.FindById(doctor.Id);

            // Assert
            Assert.Equal(doctor.Id,
                         actual.Id);
        }

        [Theory]
        [AutoNSubstituteData]
        public void FindByLastName_ReturnsDoctors_WhenCalled([NotNull] IDoctor doctor)
        {
            // Arrange
            var repository = Substitute.For <IDoctorsRepository>();
            repository.FindByLastName(Arg.Any <string>()).Returns(CreateList);
            InformationFinder sut = CreateSut(repository);

            // Act
            IEnumerable <IDoctorForResponse> actual = sut.FindByLastName("any name");

            // Assert
            Assert.Equal(2,
                         actual.Count());
        }

        [Fact]
        public void FindById_ReturnsNull_ForNotExistingId()
        {
            // Arrange
            var repository = Substitute.For <IDoctorsRepository>();
            repository.FindById(-1).Returns(( IDoctor ) null);
            InformationFinder sut = CreateSut(repository);

            // Act
            IDoctorForResponse actual = sut.FindById(-1);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void List_ReturnsDoctors_WhenCalled()
        {
            // Arrange
            var repository = Substitute.For <IDoctorsRepository>();
            repository.All.Returns(CreateList);
            InformationFinder sut = CreateSut(repository);

            // Act
            IEnumerable <IDoctorForResponse> actual = sut.List();

            // Assert
            Assert.Equal(2,
                         actual.Count());
        }

        private IQueryable <IDoctor> CreateList(CallInfo arg)
        {
            var list = new Collection <IDoctor>
                       {
                           Substitute.For <IDoctor>(),
                           Substitute.For <IDoctor>()
                       };


            return list.AsQueryable();
        }

        private InformationFinder CreateSut(IDoctorsRepository repository)
        {
            return new InformationFinder(repository);
        }
    }
}