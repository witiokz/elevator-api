using ElevatorManagementSystem.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ElevatorManagementSystem.Domain.ViewModel
{
    public class CallElevatorDto
    {
        [Required]
        [DestinationFloorValidator]
        public int DestinationFloor { get; set; }
    }
}
