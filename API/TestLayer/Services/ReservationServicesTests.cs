using DataLayer;
using DataLayer.Entities.EntertainmentItem;
using DataLayer.Entities.Reservation;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ModelLayer.Dto.Entertainment;
using ModelLayer.Dto.Reservation;
using Moq;
using ServiceLayer.Reservation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLayer.Services
{
    public class ReservationServicesTests
    {
        private readonly ReservationServices _reservationServices;
        private readonly Mock<IReservationRepository> _reservationRepositoryMock;
        private readonly Mock<IEntertainmentRepository> _enentertainmentRepositoryMock;

        public ReservationServicesTests()
        {
            _enentertainmentRepositoryMock = new Mock<IEntertainmentRepository>();
            _reservationRepositoryMock = new Mock<IReservationRepository>();
            _reservationServices = new ReservationServices(_reservationRepositoryMock.Object, _enentertainmentRepositoryMock.Object);
        }

        [Fact]
        public async Task GetReservations_ReturnsExpectedResult()
        {
            // Arrange
            var reservations = new List<GetReservationsDto>
        {
            new GetReservationsDto
            {
                Id = 1,
                EntertainmentId = 1,
                UserId = 1,
                Date = DateTime.Now,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                BreakTime = DateTime.Now.AddMinutes(30),
                MaxCount = 10,
                Period = DateTime.Now.AddHours(2),
                ReservationTime = DateTime.Now.AddHours(3)
            },
            new GetReservationsDto
            {
                Id = 2,
                EntertainmentId = 2,
                UserId = 2,
                Date = DateTime.Now.AddDays(1),
                StartTime = DateTime.Now.AddHours(1),
                EndTime = DateTime.Now.AddHours(2),
                BreakTime = DateTime.Now.AddMinutes(45),
                MaxCount = 20,
                Period = DateTime.Now.AddHours(3),
                ReservationTime = DateTime.Now.AddHours(4)
            }
        };
            _reservationRepositoryMock.Setup(x => x.GetReservations())
     .ReturnsAsync(reservations.Select(r => new ReservationEntity
     {
         ReservationId = r.Id,
         EntertainmentId = r.EntertainmentId,
         UserId = r.UserId,
         Date = r.Date,
         StartTime = r.StartTime,
         EndTime = r.EndTime,
         BreakTime = r.BreakTime,
         MaxCount = (int)r.MaxCount,
         PeriodTime = r.Period,
         ReservationTime = r.ReservationTime
     }).ToList());

            var expectedReservations = new List<GetReservationsDto>
        {
            new GetReservationsDto
            {
                Id = 1,
                EntertainmentId = 1,
                UserId = 1,
                Date = DateTime.Now,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                BreakTime = DateTime.Now.AddMinutes(30),
                MaxCount = 10,
                Period = DateTime.Now.AddHours(2),
                ReservationTime = DateTime.Now.AddHours(3)
            },
            new GetReservationsDto
            {
                Id = 2,
                EntertainmentId = 2,
                UserId = 2,
                Date = DateTime.Now.AddDays(1),
                StartTime = DateTime.Now.AddHours(1),
                EndTime = DateTime.Now.AddHours(2),
                BreakTime = DateTime.Now.AddMinutes(45),
                MaxCount = 20,
                Period = DateTime.Now.AddHours(3),
                ReservationTime = DateTime.Now.AddHours(4)
            }
        };

            // Act
            var result = await _reservationServices.GetReservations();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedReservations.Count, result.Count);
        }

        [Fact]
        public async Task GetEntertainmentReservations_ReturnsCorrectReservations()
        {
            // Arrange
            int entertainmentId = 1;
            string date = "2023-05-13";
            DateTime dateSelected = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var reservations = new List<ReservationEntity>
        {
            new ReservationEntity { Date = dateSelected, ReservationTime = new DateTime(dateSelected.Year, dateSelected.Month, dateSelected.Day, 10, 0, 0) },
            new ReservationEntity { Date = dateSelected, ReservationTime = new DateTime(dateSelected.Year, dateSelected.Month, dateSelected.Day, 12, 0, 0) },
            new ReservationEntity { Date = dateSelected, ReservationTime = new DateTime(dateSelected.Year, dateSelected.Month, dateSelected.Day, 14, 0, 0) },

        };
            _reservationRepositoryMock.Setup(x => x.GetEntertainmentReservations(entertainmentId, dateSelected))
                .ReturnsAsync(reservations);

            // Act
            var result = await _reservationServices.GetEntertainmentReservations(entertainmentId, date);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetEntertainmentReservations_ReturnsEmptyList()
        {
            // Arrange
            int entertainmentId = 1;
            string date = "2023-05-13";
            DateTime dateSelected = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var reservations = new List<ReservationEntity>();
            _reservationRepositoryMock.Setup(x => x.GetEntertainmentReservations(entertainmentId, dateSelected))
                .ReturnsAsync(reservations);

            // Act
            var result = await _reservationServices.GetEntertainmentReservations(entertainmentId, date);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetReservationsForUser_ReturnsCorrectReservations()
        {
            // Arrange
            int userId = 1;
            List<ReservationEntity> reservations = new List<ReservationEntity>
        {
            new ReservationEntity
            {
                ReservationId = 1,
                Entertainment = new EntertainmentItemEntity { EntertainmentName = "Concert", Price = 100.0, Address = "123 Main St" },
                Date = DateTime.Today,
                ReservationTime = DateTime.Now,
                PeriodTime = DateTime.Now.AddHours(1)
            },
            new ReservationEntity
            {
                ReservationId = 2,
                Entertainment = new EntertainmentItemEntity { EntertainmentName = "Movie", Price = 50.0, Address = "456 Elm St" },
                Date = DateTime.Today.AddDays(1),
                ReservationTime = DateTime.Now.AddHours(3),
                PeriodTime = DateTime.Now.AddHours(1)
            }
        };
            _reservationRepositoryMock.Setup(repo => repo.GetReservationsForUser(userId)).ReturnsAsync(reservations);

            // Act
            List<GetReservationInfoForCustomer> result = await _reservationServices.GetReservationsForUser(userId);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetReservationsForUser_ReturnsEmptyList_WhenNoReservationsFound()
        {
            // Arrange
            int userId = 1;
            _reservationRepositoryMock.Setup(repo => repo.GetReservationsForUser(userId)).ReturnsAsync(new List<ReservationEntity>());

            // Act
            List<GetReservationInfoForCustomer> result = await _reservationServices.GetReservationsForUser(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task CreateReservation_ReturnsFalse_WhenCreateReservationDtoIsNull()
        {
            // Arrange
            CreateReservationDto createReservationDto = null;
            int id = 1;

            // Act
            var result = await _reservationServices.CreateReservation(createReservationDto, id);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task CreateReservation_CallsCreateReservationOnRepository_WhenValidInputsAreProvided()
        {
            // Arrange
            CreateReservationDto createReservationDto = new CreateReservationDto
            {
                EntertainmentId = 1,
                UserId = 1,
                StartTime = "10:00",
                EndTime = "12:00",
                BreakTime = "00:15",
                MaxCount = 2,
                Period = "02:00"
            };
            int id = 1;

            _reservationRepositoryMock
                .Setup(x => x.CreateReservation(It.IsAny<ReservationEntity>()))
                .ReturnsAsync(true);

            // Act
            var result = await _reservationServices.CreateReservation(createReservationDto, id);

            // Assert
            Assert.True(result);
            _reservationRepositoryMock.Verify(x => x.CreateReservation(It.IsAny<ReservationEntity>()), Times.Once);
        }

        [Fact]
        public async Task CreateReservation_ReturnsFalse_WhenReservationRepositoryReturnsFalse()
        {
            // Arrange
            CreateReservationDto createReservationDto = new CreateReservationDto
            {
                EntertainmentId = 1,
                UserId = 1,
                StartTime = "10:00",
                EndTime = "12:00",
                BreakTime = "00:15",
                MaxCount = 2,
                Period = "02:00"
            };
            int id = 1;

            _reservationRepositoryMock
                .Setup(x => x.CreateReservation(It.IsAny<ReservationEntity>()))
                .ReturnsAsync(false);

            // Act
            var result = await _reservationServices.CreateReservation(createReservationDto, id);

            // Assert
            Assert.False(result);
            _reservationRepositoryMock.Verify(x => x.CreateReservation(It.IsAny<ReservationEntity>()), Times.Once);
        }

        [Fact]
        public async Task CreateUserReservation_ReturnsTrue_WhenReservationIsCreated()
        {
            // Arrange
            int userId = 1;
            var createUserReservationDto = new CreateUserReservationDto
            {
                EntertainmentId = 1,
                ReservationDate = "2023-05-14",
                ReservationTime = "14:00",
                ReservationPeriod = "02:00"
            };

            _reservationRepositoryMock.Setup(repo => repo.CreateReservation(It.IsAny<ReservationEntity>())).ReturnsAsync(true);

            // Act
            bool result = await _reservationServices.CreateUserReservation(createUserReservationDto, userId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CreateUserReservation_ReturnsFalse_WhenReservationRepositoryReturnsFalse()
        {
            // Arrange
            int userId = 1;
            var createUserReservationDto = new CreateUserReservationDto
            {
                EntertainmentId = 1,
                ReservationDate = "2023-05-14",
                ReservationTime = "14:00",
                ReservationPeriod = "02:00"
            };

            _reservationRepositoryMock.Setup(repo => repo.CreateReservation(It.IsAny<ReservationEntity>())).ReturnsAsync(false);

            // Act
            bool result = await _reservationServices.CreateUserReservation(createUserReservationDto, userId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteUserReservation_ReturnsTrue_WhenReservationExistsAndIsDeleted()
        {
            // Arrange
            int reservationId = 1;
            ReservationEntity reservationEntity = new ReservationEntity { ReservationId = reservationId };
            _reservationRepositoryMock.Setup(repo => repo.GetReservation(reservationId)).ReturnsAsync(reservationEntity);
            _reservationRepositoryMock.Setup(repo => repo.DeleteUserReservation(reservationEntity)).ReturnsAsync(true);

            // Act
            bool result = await _reservationServices.DeleteUserReservation(reservationId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteUserReservation_ReturnsFalse_WhenDeletionFails()
        {
            // Arrange
            int reservationId = 1;
            ReservationEntity reservationEntity = new ReservationEntity { ReservationId = reservationId };
            _reservationRepositoryMock.Setup(repo => repo.GetReservation(reservationId)).ReturnsAsync(reservationEntity);
            _reservationRepositoryMock.Setup(repo => repo.DeleteUserReservation(reservationEntity)).ReturnsAsync(false);

            // Act
            bool result = await _reservationServices.DeleteUserReservation(reservationId);

            // Assert
            Assert.False(result);
        }
    }
}
