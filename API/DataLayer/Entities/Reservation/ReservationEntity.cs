using DataLayer.Entities.EntertainmentItem;
using DataLayer.Entities.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Reservation
{
    public class ReservationEntity
    {
        [Key]
        public int ReservationId { get; set; }

        [ForeignKey("Entertainment")]
        public int EntertainmentId { get; set; }

        public virtual EntertainmentItemEntity Entertainment { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual UserEntity User { get; set; }

        public DateTime Date { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int BreakTime { get; set; }

        public int MaxCount { get; set; }
    }
}
