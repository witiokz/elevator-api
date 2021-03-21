using ElevatorManagementSystem.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElevatorManagementSystem.Model
{
    public class ElevatorLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ElevatorId { get; set; }

        public DateTime Updated { get; set; }

        public ElevatorAction Action { get; set; }
    }
}
