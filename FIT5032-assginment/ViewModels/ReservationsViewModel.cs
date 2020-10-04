using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace FIT5032_assginment.ViewModels
{
    public class ReservationsViewModel
    {
        public int Id { get; set; }
        public System.DateTime StartTime { get; set; }
        public System.DateTime EndTime { get; set; }
        public string UserId { get; set; }
        public int CoachId { get; set; }
        public string ReservationDesc { get; set; }

    }
}