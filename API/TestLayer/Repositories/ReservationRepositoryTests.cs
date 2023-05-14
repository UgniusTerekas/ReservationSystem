using DataLayer;
using DataLayer.Entities.EntertainmentItem;
using DataLayer.Entities.Reservation;
using DataLayer.Entities.User;
using DataLayer.Interfaces;
using DataLayer.Repositories.Reservation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLayer.Repositories
{
    public class ReservationRepositoryTests
    {
        private readonly DbContextOptions<DataBaseContext> _options;
        private ReservationRepository _repository;
        private readonly DataBaseContext _dbContext;

        public ReservationRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            _repository = new ReservationRepository(new DataBaseContext(_options));
            _dbContext = new DataBaseContext();
        }

        [Fact]
        public async Task CreateReservation_Should_Add_Reservation_To_DbContext()
        {
            // Arrange
            using var dbContext = new DataBaseContext(_options);
            dbContext.Database.EnsureDeleted();
            var reservationRepository = new ReservationRepository(dbContext);
            var entertainment = new EntertainmentItemEntity { EntertainmentId = 1 };
            var user = new UserEntity { UserId = 1 };
            var reservation = new ReservationEntity
            {
                EntertainmentId = 1,
                UserId = 1,
                Date = new DateTime(2023, 05, 13),
                StartTime = new DateTime(2023, 05, 13, 10, 0, 0),
                EndTime = new DateTime(2023, 05, 13, 11, 0, 0),
                MaxCount = 10
            };

            // Act
            await reservationRepository.CreateReservation(reservation);

            // Assert
            var reservations = await dbContext.Reservations.ToListAsync();
            Assert.Single(reservations);
            var createdReservation = reservations[0];
            Assert.Equal(reservation.EntertainmentId, createdReservation.EntertainmentId);
            Assert.Equal(reservation.UserId, createdReservation.UserId);
            Assert.Equal(reservation.Date, createdReservation.Date);
            Assert.Equal(reservation.StartTime, createdReservation.StartTime);
            Assert.Equal(reservation.EndTime, createdReservation.EndTime);
            Assert.Equal(reservation.MaxCount, createdReservation.MaxCount);
        }

        [Fact]
        public async Task CreateReservation_Should_Return_True_When_Successful()
        {
            // Arrange
            using var dbContext = new DataBaseContext(_options);
            var reservationRepository = new ReservationRepository(dbContext);
            var entertainment = new EntertainmentItemEntity { EntertainmentId = 1 };
            var user = new UserEntity { UserId = 1 };
            var reservation = new ReservationEntity
            {
                EntertainmentId = 1,
                UserId = 1,
                Date = new DateTime(2023, 05, 13),
                StartTime = new DateTime(2023, 05, 13, 10, 0, 0),
                EndTime = new DateTime(2023, 05, 13, 11, 0, 0),
                MaxCount = 10
            };

            // Act
            var result = await reservationRepository.CreateReservation(reservation);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetReservations_ShouldReturnAllReservations()
        {
            // Arrange
            var reservations = new List<ReservationEntity>
        {
            new ReservationEntity { ReservationId = 1, UserId = 1, EntertainmentId = 1, Date = DateTime.Now.Date, StartTime = DateTime.Now.AddHours(1), EndTime = DateTime.Now.AddHours(2), MaxCount = 10 },
            new ReservationEntity { ReservationId = 2, UserId = 2, EntertainmentId = 2, Date = DateTime.Now.Date, StartTime = DateTime.Now.AddHours(3), EndTime = DateTime.Now.AddHours(4), MaxCount = 5 },
            new ReservationEntity { ReservationId = 3, UserId = 3, EntertainmentId = 3, Date = DateTime.Now.Date.AddDays(1), StartTime = DateTime.Now.AddHours(5), EndTime = DateTime.Now.AddHours(6), MaxCount = 15 }
        };
            using (var context = new DataBaseContext(_options))
            {
                await context.AddRangeAsync(reservations);
                await context.SaveChangesAsync();
            }

            // Act
            var result = await _repository.GetReservations();

            // Assert
            Assert.Equal(reservations.Count, result.Count);
            foreach (var reservation in reservations)
            {
                Assert.Contains(result, r => r.ReservationId == reservation.ReservationId
                    && r.UserId == reservation.UserId
                    && r.EntertainmentId == reservation.EntertainmentId
                    && r.Date == reservation.Date
                    && r.StartTime == reservation.StartTime
                    && r.EndTime == reservation.EndTime
                    && r.MaxCount == reservation.MaxCount);
            }
        }

        [Fact]
        public async Task GetReservations_ShouldReturnEmptyListWhenNoReservationsExist()
        {
            // Arrange
            using var dbContext = new DataBaseContext(_options);
            dbContext.Database.EnsureDeleted();
            // Act
            var result = await _repository.GetReservations();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetReservationsForUser_ReturnsCorrectReservations()
        {
            // Arrange
            using var dbContext = new DataBaseContext(_options);
            dbContext.Database.EnsureDeleted();
            var user1 = new UserEntity { UserId = 1,
                UserName = "admin",
                UserEmail = "admin@example.com",
                PasswordSalt = Encoding.UTF8.GetBytes("salt"),
                Password = Encoding.UTF8.GetBytes("password")
            };
            var user2 = new UserEntity { UserId = 2,
                UserName = "sdfsdf",
                UserEmail = "sdfsfd@example.com",
                PasswordSalt = Encoding.UTF8.GetBytes("salt"),
                Password = Encoding.UTF8.GetBytes("password")
            };
            var entertainment = new EntertainmentItemEntity { EntertainmentId = 1,
                Address = "fdsf",
                Email = "email@gmail.com",
                PhoneNumber = "12345",
                EntertainmentDescription = "sfdds",
                EntertainmentName = "dsgsdgf"
            };
            var reservation1 = new ReservationEntity { ReservationId = 1, UserId = 1, EntertainmentId = 1, Date = DateTime.Now, ReservationTime = DateTime.Now, Entertainment = entertainment };
            var reservation2 = new ReservationEntity { ReservationId = 2, UserId = 1, EntertainmentId = 1, Date = DateTime.Now.AddDays(1), ReservationTime = DateTime.Now.AddDays(1), Entertainment = entertainment };
            var reservation3 = new ReservationEntity { ReservationId = 3, UserId = 2, EntertainmentId = 1, Date = DateTime.Now, ReservationTime = DateTime.Now, Entertainment = entertainment };
            await dbContext.AddRangeAsync(user1, user2, entertainment, reservation1, reservation2, reservation3);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await _repository.GetReservationsForUser(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ReservationEntity>>(result);
        }

        [Fact]
        public async Task GetReservationsForUser_ReturnsEmptyList_WhenNoReservationsFound()
        {
            using var dbContext = new DataBaseContext(_options);
            dbContext.Database.EnsureDeleted();
            // Arrange
            var user = new UserEntity { UserId = 1,
                UserName = "sdfsdf",
                UserEmail = "sdfsfd@example.com",
                PasswordSalt = Encoding.UTF8.GetBytes("salt"),
                Password = Encoding.UTF8.GetBytes("password")
            };
            var entertainment = new EntertainmentItemEntity { EntertainmentId = 1,
                Address = "fdsf",
                Email = "email@gmail.com",
                PhoneNumber = "12345",
                EntertainmentDescription = "sfdds",
                EntertainmentName = "dsgsdgf"
            };
            await dbContext.AddRangeAsync(user, entertainment);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await _repository.GetReservationsForUser(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ReservationEntity>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetEntertainmentReservations_ReturnsEmptyList_WhenNoReservationsFound()
        {
            // Arrange
            using var dbContext = new DataBaseContext(_options);
            dbContext.Database.EnsureDeleted();
            var entertainment = new EntertainmentItemEntity
            {
                EntertainmentId = 1,
                Address = "fdsf",
                Email = "email@gmail.com",
                PhoneNumber = "12345",
                EntertainmentDescription = "sfdds",
                EntertainmentName = "dsgsdgf"
            };
            await dbContext.AddAsync(entertainment);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await _repository.GetEntertainmentReservations(1, DateTime.Now);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ReservationEntity>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetEntertainmentReservations_ReturnsListWithReservations_WhenReservationsFound()
        {
            // Arrange
            using var dbContext = new DataBaseContext(_options);
            dbContext.Database.EnsureDeleted();
            var entertainment = new EntertainmentItemEntity
            {
                EntertainmentId = 1,
                Address = "fdsf",
                Email = "email@gmail.com",
                PhoneNumber = "12345",
                EntertainmentDescription = "sfdds",
                EntertainmentName = "dsgsdgf"
            };
            var reservation1 = new ReservationEntity
            {
                ReservationId = 1,
                EntertainmentId = 1,
                Date = DateTime.Now.Date,
                ReservationTime = DateTime.Now.AddHours(1)
            };
            var reservation2 = new ReservationEntity
            {
                ReservationId = 2,
                EntertainmentId = 1,
                Date = DateTime.Now.Date,
                ReservationTime = DateTime.Now.AddHours(2)
            };
            await dbContext.AddRangeAsync(entertainment, reservation1, reservation2);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await _repository.GetEntertainmentReservations(1, DateTime.Now.Date);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ReservationEntity>>(result);
        }

        [Fact]
        public async Task GetReservation_ReturnsNull_WhenReservationNotFound()
        {
            using var dbContext = new DataBaseContext(_options);
            dbContext.Database.EnsureDeleted();
            // Arrange

            // Act
            var result = await _repository.GetReservation(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetReservation_ReturnsReservation_WhenReservationFound()
        {
            // Arrange
            var reservation = new ReservationEntity
            {
                ReservationId = 1,
                EntertainmentId = 1,
                UserId = 1,
                Date = DateTime.Now,
                ReservationTime = DateTime.Parse("10:00:00")
            };

            using var dbContext = new DataBaseContext(_options);
            dbContext.Database.EnsureDeleted();
            await dbContext.Reservations.AddAsync(reservation);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await _repository.GetReservation(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ReservationEntity>(result);
            Assert.Equal(reservation.ReservationId, result.ReservationId);
            Assert.Equal(reservation.EntertainmentId, result.EntertainmentId);
            Assert.Equal(reservation.UserId, result.UserId);
            Assert.Equal(reservation.Date, result.Date);
            Assert.Equal(reservation.ReservationTime, result.ReservationTime);
        }

        [Fact]
        public async Task DeleteUserReservation_ShouldRemoveReservationFromDb()
        {
            // Arrange
            var reservation = new ReservationEntity
            {
                ReservationId = 1,
                UserId = 1,
                EntertainmentId = 1,
                Date = DateTime.Today,
                ReservationTime = DateTime.Now
            };
            using var dbContext = new DataBaseContext(_options);
            dbContext.Database.EnsureDeleted();
            await dbContext.Reservations.AddAsync(reservation);
            await dbContext.SaveChangesAsync();

            var repository = new ReservationRepository(dbContext);

            // Act
            var result = await repository.DeleteUserReservation(reservation);

            // Assert
            Assert.True(result);
            Assert.DoesNotContain(reservation, dbContext.Reservations);
        }

        [Fact]
        public async Task DeleteUserReservation_ShouldReturnTrue_WhenReservationExists()
        {
            // Arrange
            var reservation = new ReservationEntity
            {
                ReservationId = 1,
                UserId = 1,
                EntertainmentId = 1,
                Date = DateTime.Today,
                ReservationTime = DateTime.Now
            };
            using var dbContext = new DataBaseContext(_options);
            dbContext.Database.EnsureDeleted();
            var repository = new ReservationRepository(dbContext);
            await dbContext.Reservations.AddAsync(reservation);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await repository.DeleteUserReservation(reservation);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetAdminReservations_ShouldReturnEmptyList_WhenAdminHasNoReservations()
        {
            // Arrange
            var adminId = 1;
            using var dbContext = new DataBaseContext(_options);
            var repository = new ReservationRepository(dbContext);

            // Act
            var result = await repository.GetAdminReservations(adminId);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAdminReservations_ShouldReturnNonEmptyList_WhenAdminHasReservations()
        {
            // Arrange
            var adminId = 1;
            var entertainment = new EntertainmentItemEntity
            {
                EntertainmentId = 1,
                Address = "fdsf",
                Email = "email@gmail.com",
                PhoneNumber = "12345",
                EntertainmentDescription = "sfdds",
                EntertainmentName = "Test Entertainment"
            };
            var user = new UserEntity
            {
                UserId = 1,
                UserName = "Test User",
                UserEmail = "sdfsfd@example.com",
                PasswordSalt = Encoding.UTF8.GetBytes("salt"),
                Password = Encoding.UTF8.GetBytes("password")
            };
            var reservation = new ReservationEntity
            {
                ReservationId = 1,
                UserId = adminId,
                EntertainmentId = entertainment.EntertainmentId,
                Date = DateTime.Today,
                ReservationTime = DateTime.Now,
                Entertainment = entertainment,
                User = user
            };
            using var dbContext = new DataBaseContext(_options);
            dbContext.Database.EnsureDeleted();
            dbContext.AddRange(entertainment, user, reservation);
            await dbContext.SaveChangesAsync();
            var repository = new ReservationRepository(dbContext);

            // Act
            var result = await repository.GetAdminReservations(adminId);

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, r => r.ReservationId == reservation.ReservationId);
        }
    }
}
