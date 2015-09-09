using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using MicroServices.Common.Tests;
using MicroServices.Doctors.Nancy.Interfaces;
using Nancy;
using NSubstitute;
using NSubstitute.Core;
using Xunit;
using Xunit.Extensions;

namespace MicroServices.Doctors.Nancy.Tests
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class RequestHandlerTests
    {
        // todo don't know how to get content from Response object, 
        // but integration test cover this
        private const int DoesNotMatter = -1;

        [Fact]
        public void List_ReturnsResponse_WhenCalled()
        {
            // Arrange
            var finder = Substitute.For <IInformationFinder>();
            finder.List().Returns(CreateList);
            RequestHandler sut = CreateSut(finder);

            // Act
            Response actual = sut.List();

            // Assert
            Assert.Equal(HttpStatusCode.OK,
                         actual.StatusCode);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Save_ReturnsStatusOK_WhenCalled([NotNull] IDoctorForResponse toBeCreated,
                                                    [NotNull] IDoctorForResponse created)
        {
            // Arrange
            var finder = Substitute.For <IInformationFinder>();
            finder.Save(toBeCreated).Returns(created);
            RequestHandler sut = CreateSut(finder);

            // Act
            Response actual = sut.Save(toBeCreated);

            // Assert
            Assert.Equal(HttpStatusCode.OK,
                         actual.StatusCode);
        }

        [Theory]
        [AutoNSubstituteData]
        public void DeleteById_ReturnsResponse_WhenCalled([NotNull] IDoctorForResponse doctor)
        {
            // Arrange
            var finder = Substitute.For <IInformationFinder>();
            finder.Delete(doctor.Id).Returns(doctor);
            RequestHandler sut = CreateSut(finder);

            // Act
            Response actual = sut.DeleteById(doctor.Id);

            // Assert
            Assert.Equal(HttpStatusCode.OK,
                         actual.StatusCode);
        }

        [Fact]
        public void DeleteById_ReturnsResponse_ForAddFailed()
        {
            // Arrange
            var finder = Substitute.For <IInformationFinder>();
            finder.Delete(DoesNotMatter).Returns(( IDoctorForResponse ) null);
            RequestHandler sut = CreateSut(finder);

            // Act
            Response actual = sut.DeleteById(DoesNotMatter);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound,
                         actual.StatusCode);
        }

        [Theory]
        [AutoNSubstituteData]
        public void FindById_ReturnsResponse_ForKnownId([NotNull] IDoctorForResponse doctor)
        {
            // Arrange
            var finder = Substitute.For <IInformationFinder>();
            finder.FindById(doctor.Id).Returns(doctor);
            RequestHandler sut = CreateSut(finder);

            // Act
            Response actual = sut.FindById(doctor.Id);

            // Assert
            Assert.Equal(HttpStatusCode.OK,
                         actual.StatusCode);
        }

        [Theory]
        [AutoNSubstituteData]
        public void FindById_ReturnsResponse_ForUnknownId()
        {
            // Arrange
            var finder = Substitute.For <IInformationFinder>();
            finder.FindById(DoesNotMatter).Returns(( IDoctorForResponse ) null);
            RequestHandler sut = CreateSut(finder);

            // Act
            Response actual = sut.FindById(DoesNotMatter);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound,
                         actual.StatusCode);
        }

        [Theory]
        [AutoNSubstituteData]
        public void FindByLastName_ReturnsResponse_ForUnknownId()
        {
            // Arrange
            var finder = Substitute.For <IInformationFinder>();
            finder.FindById(DoesNotMatter).Returns(( IDoctorForResponse ) null);
            RequestHandler sut = CreateSut(finder);

            // Act
            Response actual = sut.FindByLastName("Unknown");

            // Assert
            Assert.Equal(HttpStatusCode.OK,
                         actual.StatusCode);
        }

        [Theory]
        [AutoNSubstituteData]
        public void FindByLastName_ReturnsResponseOK_ForMoreThanOneDoctor([NotNull] IDoctorForResponse doctor)
        {
            // Arrange
            IEnumerable <IDoctorForResponse> list = CreateListOneDoctor(doctor);
            var finder = Substitute.For <IInformationFinder>();
            finder.FindByLastName(doctor.LastName).Returns(list);
            RequestHandler sut = CreateSut(finder);

            // Act
            Response actual = sut.FindByLastName(doctor.LastName);

            // Assert
            Assert.Equal(HttpStatusCode.OK,
                         actual.StatusCode);
        }

        private IEnumerable <IDoctorForResponse> CreateListOneDoctor(IDoctorForResponse doctor)
        {
            var list = new Collection <IDoctorForResponse>
                       {
                           doctor
                       };


            return list.AsQueryable();
        }

        private IQueryable <IDoctorForResponse> CreateList(CallInfo arg)
        {
            var list = new Collection <IDoctorForResponse>
                       {
                           Substitute.For <IDoctorForResponse>(),
                           Substitute.For <IDoctorForResponse>()
                       };


            return list.AsQueryable();
        }

        private RequestHandler CreateSut(IInformationFinder finder)
        {
            return new RequestHandler(finder);
        }
    }
}