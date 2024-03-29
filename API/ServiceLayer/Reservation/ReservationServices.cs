﻿using DataLayer.Entities.Reservation;
using DataLayer.Interfaces;
using ModelLayer.Dto.Entertainment;
using ModelLayer.Dto.Reservation;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Reservation
{
    public class ReservationServices : IReservationServices
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IEntertainmentRepository _entertainmentRepository;

        public ReservationServices(
            IReservationRepository reservationRepository,
            IEntertainmentRepository entertainmentRepository)
        {
            _reservationRepository = reservationRepository;
            _entertainmentRepository = entertainmentRepository;
        }

        public async Task<List<GetReservationsDto>> GetReservations()
        {
            var reservations = await _reservationRepository.GetReservations();

            return reservations
                .Select(x => new GetReservationsDto
                {
                    Id = x.ReservationId,
                    EntertainmentId = x.EntertainmentId,
                    UserId = x.UserId,
                    Date = x.Date,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    BreakTime = x.BreakTime,
                    MaxCount = x.MaxCount,
                    Period = x.PeriodTime,
                    ReservationTime = x.ReservationTime,
                })
                .ToList();
        }

        public async Task<List<EntertainmentReservationDto>> GetEntertainmentReservations(
            int entertainmentId,
            string date)
        {
            DateTime dateSelected = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var reservations = await _reservationRepository.GetEntertainmentReservations(entertainmentId, dateSelected);

            return reservations
                .Select(x => new EntertainmentReservationDto
                {
                    Date = x.Date.ToString(),
                    Time = x.ReservationTime.ToString(),
                })
                .ToList();
        }

        public async Task<List<GetReservationInfoForCustomer>> GetReservationsForUser(int userId)
        {
            var reservationEntity = await _reservationRepository.GetReservationsForUser(userId);

            return reservationEntity
                .Select(x => new GetReservationInfoForCustomer
                {
                    ReservationId = x.ReservationId,
                    EntertainmentName = x.Entertainment.EntertainmentName,
                    Date = x.Date.ToString(),
                    Time = x.ReservationTime.ToString(),
                    Price = x.Entertainment.Price,
                    Duration = x.PeriodTime.ToString(),
                    Address = x.Entertainment.Address
                })
                .OrderByDescending(x => x.Time)
                .ToList();
        }

        public async Task<bool> CreateReservation(
            CreateReservationDto createReservation,
            int id)
        {
            if (createReservation == null)
            {
                return false;
            }

            DateTime startTime = DateTime.ParseExact(createReservation.StartTime, "HH:mm", CultureInfo.InvariantCulture);
            DateTime endTime = DateTime.ParseExact(createReservation.EndTime, "HH:mm", CultureInfo.InvariantCulture);
            DateTime period = DateTime.ParseExact(createReservation.Period, "HH:mm", CultureInfo.InvariantCulture);
            DateTime breakTime = DateTime.ParseExact(createReservation.BreakTime, "HH:mm", CultureInfo.InvariantCulture);

            var reservationEntity = new ReservationEntity
            {
                EntertainmentId = createReservation.EntertainmentId,
                UserId = id,
                StartTime = startTime,
                EndTime = endTime,
                BreakTime = breakTime,
                MaxCount = createReservation.MaxCount,
                PeriodTime = period
            };

            return await _reservationRepository.CreateReservation(reservationEntity);
        }

        public async Task<GetReservationFillDto> GetReservationFillData(int entertainmentId)
        {
            var entertainmentEntity = await _entertainmentRepository.GetEntertainment(entertainmentId);
            int reservationId = -1;

            var reservation = entertainmentEntity
                .Reservations
                .Where(x => x.EntertainmentId == entertainmentId)
                .Select(x => new
                {
                    x.ReservationId
                })
                .FirstOrDefault();

            if (reservation != null)
            {
                reservationId = reservation.ReservationId;
            }

            var reservationEntity = await _reservationRepository.GetReservation(reservationId);

            return new GetReservationFillDto
            {
                Date = reservationEntity.Date.ToString(),
                ReservationId = reservationId,
                StartTime = reservationEntity.StartTime.ToString(),
                EndTime = reservationEntity.EndTime.ToString(),
                EntertainmentId = reservationEntity.EntertainmentId,
                BreakTime = reservationEntity.BreakTime.ToString(),
                PeriodTime = reservationEntity.PeriodTime.ToString(),
                MaxCount = reservationEntity.MaxCount,
            };
        }

        public async Task<bool> CreateUserReservation(
            CreateUserReservationDto createUserReservation,
            int userId)
        {
            DateTime date = DateTime.ParseExact(createUserReservation.ReservationDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime reservationTime = DateTime.ParseExact(createUserReservation.ReservationTime, "HH:mm", CultureInfo.InvariantCulture);
            DateTime reservationPeriod = DateTime.ParseExact(createUserReservation.ReservationPeriod, "HH:mm", CultureInfo.InvariantCulture);

            var reservationEntity = new ReservationEntity
            {
                EntertainmentId = createUserReservation.EntertainmentId,
                UserId = userId,
                Date = date,
                ReservationTime = reservationTime,
                PeriodTime = reservationPeriod,
            };

            return await _reservationRepository.CreateReservation(reservationEntity);
        }

        public async Task<bool> DeleteUserReservation(int reservationId)
        {
            var reservationEntity = await _reservationRepository.GetReservation(reservationId);

            return await _reservationRepository.DeleteUserReservation(reservationEntity);
        }

        public async Task<List<GetAdminReservationsDto>> GetAdminReservations(int adminId)
        {
            var reservations = await _reservationRepository.GetAdminReservations(adminId);

            return reservations
                .Select(r => new GetAdminReservationsDto
                {
                    Id = r.ReservationId,
                    EntertainmentName = r.Entertainment.EntertainmentName,
                    EntertainmentId = r.EntertainmentId,
                    Username = r.User.UserName,
                    Email = r.User.UserEmail,
                    UserId = adminId,
                    ReservationTime = r.ReservationTime.ToString(),
                    Price = r.Entertainment.Price
                })
                .OrderByDescending(r => r.Id)
                .ToList();
        }
    }
}
