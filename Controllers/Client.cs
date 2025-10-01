using CoworkingReservation.Data;
using CoworkingReservation.Data.Models;
using CoworkingReservation.Data.Models.DbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CoworkingReservation.Controllers
{
    [Route("client")]
    [ApiController]
    public class Client : ControllerBase
    {
        private AppDBContext _context { get; set; }
        public Client(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet("search")]

        public IActionResult GetByFilter([FromQuery] SearchModel searchModel)
        {
            var result = _context.CoworkingUnits
                                 .Where(q =>
                                       (searchModel.IsLanCableAvailable == null || q.CoworkingFlags.IsLanCableAvailable == searchModel.IsLanCableAvailable) &&
                                       (searchModel.IsSoundProof == null || q.CoworkingFlags.IsSoundProof == searchModel.IsSoundProof) &&
                                       (string.IsNullOrEmpty(searchModel.Name) || q.Name == searchModel.Name) &&
                                       (searchModel.StartTime == null || searchModel.EndTime == null ||!q.Reservations.Any(r =>r.StartTime < searchModel.EndTime &&r.EndTime > searchModel.StartTime)) &&
                                       (searchModel.PricePerHour == null || q.PricePerHour == searchModel.PricePerHour ) &&
                                       (searchModel.MaxNumberOfPeople == null || q.CoworkingFlags.MaxNumberOfPeople == searchModel.MaxNumberOfPeople))
                                 .Include(q => q.CoworkingPhotos)
                                 .Include(q => q.CoworkingFlags)
                                 .Include(q => q.Reservations)
                                 .ToList();



            if (result != null)
            {
                return Ok(result);
            }
            else { return BadRequest("No coworking found"); }
        }

        [HttpPost("addreservation")]

        public IActionResult ReservUnit([FromBody] ReservationAdd reservationAdd)
        {
            var unit = _context.CoworkingUnits.Where(q => q.Name == reservationAdd.CoworkingName).FirstOrDefault();
            if (unit != null)
            {
                bool hasReservation = _context.CoworkingUnits
                                          .Where(u => u.Name == reservationAdd.CoworkingName)
                                          .SelectMany(u => u.Reservations)
                                          .Any(r => r.StartTime < reservationAdd.EndTime && r.EndTime > reservationAdd.StartTime);

                if (hasReservation)
                {
                    return NotFound("Time slot is already reserved");
                }
                else
                {
                    var reservation = new Reservation
                    {
                        CoworkingUnitId = _context.CoworkingUnits.First(q => q.Name == reservationAdd.CoworkingName).Id,
                        StartTime = reservationAdd.StartTime,
                        EndTime = reservationAdd.EndTime,
                        Guid = Guid.NewGuid()
                    };
                    _context.Reservations.Add(reservation);
                    _context.SaveChanges();
                    return Ok(reservation.Guid);
                }
            }
            else
            {
                return NotFound("Coworking with this name doesnt exist");
            }
            

                
        }


        [HttpDelete("deletereservation")]
        public IActionResult DeleteReservation([FromBody] ReservationDelition reservationDelition)
        {
            var unit = _context.CoworkingUnits.Where(q => q.Name == reservationDelition.CoworkingName).FirstOrDefault();
            if(unit != null)
            {
                var reservation = _context.CoworkingUnits.Where(q => q.Name == reservationDelition.CoworkingName).SelectMany(q => q.Reservations).FirstOrDefault(q => q.Guid == reservationDelition.Guid);
                if(reservation != null)
                {
                    _context.Remove(reservation);
                    _context.SaveChanges();
                    return Ok("Reservation was deleted");
                }
                else
                {
                    return BadRequest("Guid doesnt match");
                }
            }
            else
            {
                return NotFound("Name doesnt exist");
            }
                
        }
    }
}

