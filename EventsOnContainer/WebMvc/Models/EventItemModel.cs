using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models
{
    public class EventItemModel
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string EventImageUrl { get; set; }
        public DateTime EventStartTime { get; set; }
        public DateTime EventEndTime { get; set; }
        public string OrganizerName { get; set; }
        public string OrganzierPhoneNumber { get; set; }


        public int AddressId { get; set; }
        public int TypeId { get; set; }
        public int CatagoryId { get; set; }

        public EventAddressModel Address { get; set; }
        public EventCategoryModel EventCategory { get; set; }
        public EventTypeModel EventType { get; set; }
    }

}
