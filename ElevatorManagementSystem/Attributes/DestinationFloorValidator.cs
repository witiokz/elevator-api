using System.ComponentModel.DataAnnotations;

namespace ElevatorManagementSystem.Attributes
{
    public class DestinationFloorValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int.TryParse(Startup.StaticConfig["ElevatorConfiguration:FloorCount"], out int elevatorCount);

            return (int)value <= elevatorCount;
        }
    }
}
