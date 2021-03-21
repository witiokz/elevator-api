using ElevatorManagementSystem.Domain.Enums;
using System;
using System.Text.Json.Serialization;

namespace ElevatorManagementSystem.Domain.Dto
{
    public class ElevatorLogDto
    {
        public int ElevatorId { get; set; }

        public DateTime Updated { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ElevatorAction Action { get; set; }
    }
}
