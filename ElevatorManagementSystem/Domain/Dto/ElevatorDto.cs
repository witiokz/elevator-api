using ElevatorManagementSystem.Domain.Enums;
using System.Text.Json.Serialization;

namespace ElevatorManagementSystem.Domain.Dto
{
    public class ElevatorDto
    {
        public int Id { get; set; }

        public int CurrentFloor { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ElevatorAction Action { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ElevatorDirection Direction { get; set; }
    }
}
