using ElevatorManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElevatorManagementSystem.Data.Repositories
{
    public interface IElevatorRepository
    {
        Task<IList<Elevator>> GetAll();

        Task<Elevator> GetById(int id);

        Task<Elevator> Get(Func<Elevator, bool> filter);

        Task Save(Elevator elevator);
    }
}