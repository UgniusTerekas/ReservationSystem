using API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Dto.Entertainment;
using ModelLayer.Dto.Reservation;
using Moq;
using ServiceLayer.Interfaces;
using ServiceLayer.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace TestLayer.Controllers
{
    public class ReservationControllerTests
    {
        private readonly Mock<IReservationServices> _reservationServicesMock;
        private readonly ReservationController _controller;

        public ReservationControllerTests()
        {
            _reservationServicesMock = new Mock<IReservationServices>();
            _controller = new ReservationController(_reservationServicesMock.Object);
        }

        [Fact]
        public async Task CreateReservation_ValidUser_ReturnsOk()
        {
            // Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", "1"),
            }, "test"));
            var createReservationDto = new CreateReservationDto { EntertainmentId = 4, UserId = 3,
                StartTime = "10:00",
                EndTime = "21:00",
                BreakTime = "01:00",
                MaxCount = 4,
                Period = "01:00"
            };
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            _reservationServicesMock.Setup(x => x.CreateReservation(createReservationDto, 1)).ReturnsAsync(true);
            // Act
            var result = await _controller.CreateReservation(createReservationDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetReservations_ShouldReturnOkObjectResultWithReservations_WhenReservationsExist()
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
                EndTime = DateTime.Now,
                BreakTime = DateTime.Now,
                MaxCount = 10,
                Period = DateTime.Now,
                ReservationTime = DateTime.Now
            },
            new GetReservationsDto
            {
                Id = 2,
                EntertainmentId = 2,
                UserId = 2,
                Date = DateTime.Now,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                BreakTime = DateTime.Now,
                MaxCount = 5,
                Period = DateTime.Now,
                ReservationTime = DateTime.Now
            }
        };

            _reservationServicesMock.Setup(x => x.GetReservations()).ReturnsAsync(reservations);

            // Act
            var result = await _controller.GetReservations();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var resultValue = Assert.IsAssignableFrom<List<GetReservationsDto>>(okObjectResult.Value);
            Assert.Equal(2, resultValue.Count);
            Assert.Equal(1, resultValue[0].Id);
            Assert.Equal(2, resultValue[1].Id);
        }

        [Fact]
        public async Task GetReservations_ShouldReturnOkObjectResultWithEmptyList_WhenNoReservationsExist()
        {
            // Arrange
            var reservations = new List<GetReservationsDto>();

            _reservationServicesMock.Setup(x => x.GetReservations()).ReturnsAsync(reservations);

            // Act
            var result = await _controller.GetReservations();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var resultValue = Assert.IsAssignableFrom<List<GetReservationsDto>>(okObjectResult.Value);
            Assert.Empty(resultValue);
        }

        [Fact]
        public async Task CreateReservation_InvalidUser_ReturnsUnauthorized()
        {
            // Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            var createReservationDto = new CreateReservationDto { EntertainmentId = 1, UserId = -1, StartTime = "10:00", EndTime = "12:00", BreakTime = "0", MaxCount = 10, Period = "Daily" };
            var reservationServicesMock = new Mock<IReservationServices>();
            var controller = new ReservationController(reservationServicesMock.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            // Act
            var result = await controller.CreateReservation(createReservationDto);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task CreateReservation_NullInput_ReturnsBadRequest()
        {
            // Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim("UserId", "1"),
            }, "test"));
            var controller = new ReservationController(_reservationServicesMock.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            // Act
            var result = await controller.CreateReservation(null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task GetUserReservations_UnauthorizedUser_ReturnsUnauthorizedResult()
        {
            // Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            // Act
            var result = await _controller.GetUserReservations();

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task GetUserReservations_ValidUser_ReturnsOkResultWithReservationInfo()
        {
            // Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim("UserId", "1"),
            }, "test"));
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            var reservationInfoList = new List<GetReservationInfoForCustomer>()
        {
            new GetReservationInfoForCustomer()
            {
                ReservationId = 1,
                EntertainmentName = "Entertainment 1",
                Date = "2023-05-20",
                Time = "10:00",
                Price = 10.00,
                Duration = "2 hours",
                Address = "123 Main St"
            },
            new GetReservationInfoForCustomer()
            {
                ReservationId = 2,
                EntertainmentName = "Entertainment 2",
                Date = "2023-05-21",
                Time = "11:00",
                Price = 20.00,
                Duration = "1 hour",
                Address = "456 Main St"
            }
        };

            _reservationServicesMock.Setup(x => x.GetReservationsForUser(It.IsAny<int>()))
                .ReturnsAsync(reservationInfoList);

            // Act
            var result = await _controller.GetUserReservations() as OkObjectResult;
            var reservationInfoResult = result.Value as List<GetReservationInfoForCustomer>;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.NotNull(reservationInfoResult);
            Assert.Equal(reservationInfoList.Count, reservationInfoResult.Count);
            Assert.Equal(reservationInfoList[0].ReservationId, reservationInfoResult[0].ReservationId);
            Assert.Equal(reservationInfoList[1].EntertainmentName, reservationInfoResult[1].EntertainmentName);
        }

        [Fact]
        public async Task GetReservationFillData_WithValidEntertainmentId_ReturnsOkObjectResult()
        {
            // Arrange
            int entertainmentId = 1;
            var expectedResult = new GetReservationFillDto { ReservationId = 1, EntertainmentId = 1, Date = "2023-05-14", StartTime = "10:00", EndTime = "21:00", BreakTime = "01:00", PeriodTime = "01:00", MaxCount = 10 };
            _reservationServicesMock.Setup(x => x.GetReservationFillData(entertainmentId)).ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.GetReservationFillData(entertainmentId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(expectedResult, okResult.Value);
        }

        [Fact]
        public async Task GetReservationFillData_WithInvalidEntertainmentId_ReturnsNotFoundResult()
        {
            // Arrange
            int entertainmentId = -1;
            _reservationServicesMock.Setup(x => x.GetReservationFillData(entertainmentId)).ReturnsAsync(() => null);

            // Act
            var result = await _controller.GetReservationFillData(entertainmentId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateUserReservation_ValidData_ReturnsCreated()
        {
            // Arrange
            var createUserReservationDto = new CreateUserReservationDto
            {
                EntertainmentId = 1,
                ReservationDate = "2023-05-14",
                ReservationTime = "10:00",
                ReservationPeriod = "01:00"
            };
            var userId = "1";
            var expectedCreatedResult = new CreatedResult("", true);
            _reservationServicesMock.Setup(s => s.CreateUserReservation(createUserReservationDto, It.IsAny<int>())).ReturnsAsync(true);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("UserId", userId) })); // mocking HttpContext.User.FindFirstValue("UserId")

            // Act
            var result = await _controller.CreateUserReservation(createUserReservationDto);

            // Assert
            Assert.IsType<CreatedResult>(result);
            Assert.Equal(expectedCreatedResult.StatusCode, (result as CreatedResult).StatusCode);
        }


        [Fact]
        public async Task CreateUserReservation_InvalidData_ReturnsBadRequest()
        {
            // Arrange
            var createUserReservationDto = new CreateUserReservationDto
            {
                EntertainmentId = 0,
                ReservationDate = "",
                ReservationTime = "",
                ReservationPeriod = ""
            };
            var userId = "1";
            var expectedBadRequestResult = new BadRequestResult(); // change to BadRequestResult
            _controller.ModelState.AddModelError("EntertainmentId", "Entertainment Id is required");
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("UserId", userId) })); // mocking HttpContext.User.FindFirstValue("UserId")

            // Act
            var result = await _controller.CreateUserReservation(createUserReservationDto);

            // Assert
            Assert.IsType<BadRequestResult>(result); // change to BadRequestResult
            Assert.Equal(expectedBadRequestResult.StatusCode, (result as BadRequestResult).StatusCode);
        }

        [Fact]
        public async Task GetEntertainmentReservations_ValidData_ReturnsOk()
        {
            // Arrange
            var entertainmentId = 4;
            var date = "2023-05-14";
            var expectedOkResult = new OkObjectResult(new List<EntertainmentReservationDto>());

            _reservationServicesMock
                .Setup(s => s.GetEntertainmentReservations(entertainmentId, date))
                .ReturnsAsync(new List<EntertainmentReservationDto>());

            // Act
            var result = await _controller.GetEntertainmentReservations(entertainmentId, date);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedOkResult.StatusCode, (result as OkObjectResult).StatusCode);
        }

        [Fact]
        public async Task GetEntertainmentReservations_InvalidData_ReturnsBadRequest()
        {
            // Arrange
            var entertainmentId = 0;
            var date = "";
            var expectedBadRequestResult = new BadRequestResult();
            _controller.ModelState.AddModelError("EntertainmentId", "Entertainment Id is required");

            // Act
            var result = await _controller.GetEntertainmentReservations(entertainmentId, date);

            // Assert
            Assert.IsType<BadRequestResult>(result);
            Assert.Equal(expectedBadRequestResult.StatusCode, (result as BadRequestResult).StatusCode);
        }

        [Fact]
        public async Task GetAdminDashboardReservations_ValidData_ReturnsOk()
        {
            // Arrange
            var expectedReservations = new List<GetAdminReservationsDto>
        {
            new GetAdminReservationsDto
            {
                Id = 1,
                EntertainmentName = "Test Entertainment",
                EntertainmentId = 4,
                Username = "testuser",
                Email = "testuser@test.com",
                UserId = 1,
                ReservationTime = "10:00",
                Price = 10.00
            }
        };
            _reservationServicesMock.Setup(s => s.GetAdminReservations(1)).ReturnsAsync(expectedReservations);
            var userId = "1";
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("UserId", userId) })); // mocking HttpContext.User.FindFirstValue("UserId")
            // Act
            var result = await _controller.GetAdminDashboardReservations();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualReservations = Assert.IsType<List<GetAdminReservationsDto>>(okResult.Value);
            Assert.Equal(expectedReservations.Count, actualReservations.Count);
            for (int i = 0; i < expectedReservations.Count; i++)
            {
                Assert.Equal(expectedReservations[i].Id, actualReservations[i].Id);
                Assert.Equal(expectedReservations[i].EntertainmentName, actualReservations[i].EntertainmentName);
                Assert.Equal(expectedReservations[i].EntertainmentId, actualReservations[i].EntertainmentId);
                Assert.Equal(expectedReservations[i].Username, actualReservations[i].Username);
                Assert.Equal(expectedReservations[i].Email, actualReservations[i].Email);
                Assert.Equal(expectedReservations[i].UserId, actualReservations[i].UserId);
                Assert.Equal(expectedReservations[i].ReservationTime, actualReservations[i].ReservationTime);
                Assert.Equal(expectedReservations[i].Price, actualReservations[i].Price);
            }
        }

        [Fact]
        public async Task GetAdminDashboardReservations_Unauthorized_ReturnsUnauthorized()
        {
            // Arrange
            var userId = "0";
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("UserId", userId) }));

            // Act
            var result = await _controller.GetAdminDashboardReservations();

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task DeleteUserReservation_ValidReservationId_ReturnsOk()
        {
            // Arrange
            var reservationId = 1;
            var expectedResult = true;
            _reservationServicesMock.Setup(s => s.DeleteUserReservation(reservationId)).ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.DeleteUserReservation(reservationId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResult = Assert.IsType<bool>(okResult.Value);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task DeleteUserReservation_InvalidReservationId_ReturnsBadRequest()
        {
            // Arrange
            var reservationId = 0;
            var expectedBadRequestResult = new BadRequestResult();

            // Act
            var result = await _controller.DeleteUserReservation(reservationId);

            // Assert
            Assert.IsType<BadRequestResult>(result);
            Assert.Equal(expectedBadRequestResult.StatusCode, (result as BadRequestResult).StatusCode);
        }

    }
}
