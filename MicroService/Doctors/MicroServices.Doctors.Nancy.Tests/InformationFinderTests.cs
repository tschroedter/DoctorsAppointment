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
        private const int DoesNotMatter = -1;

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

        [Theory]
        [AutoNSubstituteData]
        public void Create_ReturnsNewDoctor_WhenCalled([NotNull] IDoctor doctor)
        {
            // Arrange
            var repository = Substitute.For <IDoctorsRepository>();
            repository.Create().Returns(doctor);
            InformationFinder sut = CreateSut(repository);

            // Act
            IDoctorForResponse actual = sut.Create();

            // Assert
            Assert.Equal(doctor.Id,
                         actual.Id);
        }

        [Fact]
        public void Create_ReturnsNull_ForCanNotAdd()
        {
            // Arrange
            var repository = Substitute.For <IDoctorsRepository>();
            repository.Create().Returns(( IDoctor ) null);
            InformationFinder sut = CreateSut(repository);

            // Act
            IDoctorForResponse actual = sut.Create();

            // Assert
            Assert.Null(actual);
        }


        [Theory]
        [AutoNSubstituteData]
        public void Delete_ReturnsNewDoctor_WhenCalled([NotNull] IDoctor doctor)
        {
            // Arrange
            var repository = Substitute.For<IDoctorsRepository>();
            repository.Delete(Arg.Any<int>()).Returns(doctor);
            InformationFinder sut = CreateSut(repository);

            // Act
            IDoctorForResponse actual = sut.Delete(DoesNotMatter);

            // Assert
            Assert.Equal(doctor.Id,
                         actual.Id);
        }

        [Fact]
        public void Delete_ReturnsNull_ForCanNotAdd()
        {
            // Arrange
            var repository = Substitute.For<IDoctorsRepository>();
            repository.Delete(Arg.Any<int>()).Returns((IDoctor)null);
            InformationFinder sut = CreateSut(repository);

            // Act
            IDoctorForResponse actual = sut.Delete(DoesNotMatter);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void FindById_ReturnsNull_ForNotExistingId()
        {
            // Arrange
            var repository = Substitute.For <IDoctorsRepository>();
            repository.FindById(DoesNotMatter).Returns((IDoctor)null);
            InformationFinder sut = CreateSut(repository);

            // Act
            IDoctorForResponse actual = sut.FindById(DoesNotMatter);

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