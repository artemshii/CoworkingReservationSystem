using CoworkingReservation.Data;
using CoworkingReservation.Data.Models;
using CoworkingReservation.Data.Models.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoworkingReservation.Controllers
{
    [Authorize]
    [Route("panel")]
    [ApiController]
    public class AdminPanel : ControllerBase
    {
        private AppDBContext _context { get; set; }
        private ImageCopy _imageCopy { get; set; }
        public AdminPanel(AppDBContext context, ImageCopy imageCopy)
        {
            _context = context;
            _imageCopy = imageCopy;
            
        }

        [HttpPost("addCoworking")]
        [Consumes("multipart/form-data")]

        public async  Task<IActionResult> AddNewCoworkingUnit([FromForm] CoworkingAddModel coworkingAddModel)
        {
            var Entity = _context.CoworkingUnits.Any(q => q.Name == coworkingAddModel.Name);
            if(Entity)
            {
                return BadRequest("Entity with this name already exists");
            }
            else
            {
                CoworkingUnit coworkingUnit = new CoworkingUnit()
                {
                    Name = coworkingAddModel.Name,
                    PricePerHour = coworkingAddModel.PricePerHour,
                    CoworkingFlags = new CoworkingFlags()
                    {
                        MaxNumberOfPeople = coworkingAddModel.MaxNumberOfPeople,
                        IsLanCableAvailable = coworkingAddModel.IsLanCableAvailable,
                        IsSoundProof = coworkingAddModel.IsSoundProof
                    },
                    CoworkingPhotos = new List<CoworkingPhoto>()


                };


                if (coworkingAddModel.Photos != null)
                {
                    foreach (var photo in coworkingAddModel.Photos)
                    {

                        string path = await _imageCopy.SavePhoto(photo);

                        CoworkingPhoto coworkingPhoto = new CoworkingPhoto()
                        {
                            Url = path
                        };
                        coworkingUnit.CoworkingPhotos.Add(coworkingPhoto);
                    }
                }



                await _context.CoworkingUnits.AddAsync(coworkingUnit);
                await _context.SaveChangesAsync();

                return Ok("Successfully Added");
            }

                
        }

        [HttpDelete("deleteCoworking")]

        public IActionResult DeleteExistingCoworking([FromBody] string Name)
        {
            var Entity = _context.CoworkingUnits.Any(q => q.Name == Name);
            if(Entity)
            {
                _context.CoworkingUnits.Where(q => q.Name == Name).ExecuteDelete();
                _context.SaveChanges();
                return Ok("Successfully Deleted");
            }
            else { return BadRequest("Entity doesnt exist"); }

        }

        [HttpDelete("deleteReservation")]

        public IActionResult DeleteExistingReservation([FromBody] ReservationDelitionForAdmins reservationDelition)
        {
            var Entity = _context.Reservations.Any(q => q.CoworkingUnit.Name == reservationDelition.CoworkingName && q.StartTime == reservationDelition.StartTime);
            if(Entity)
            {
                _context.Reservations.Where(q => q.CoworkingUnit.Name == reservationDelition.CoworkingName && q.StartTime == reservationDelition.StartTime).ExecuteDelete();
                _context.SaveChanges();
                return Ok("Successfully Deleted");
            }
            else { return BadRequest("Entity doesnt exist"); }

        }






    }
}
