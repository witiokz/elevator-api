using System.ComponentModel.DataAnnotations;

namespace ElevatorManagementSystem.Domain.ViewModel
{
    public class ElevatorStatusViewModel
    {
        [Required]
        public int ElevatorId { get; set; }
    }
}
