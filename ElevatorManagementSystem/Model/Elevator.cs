using ElevatorManagementSystem.Domain.Enums;

namespace ElevatorManagementSystem.Model
{
    public class Elevator
    {
        public int Id { get; set; }

        public int CurrentFloor { get; set; }

        public ElevatorAction Action { get; set; }

        public ElevatorDirection Direction { get; set; }
    }
}
