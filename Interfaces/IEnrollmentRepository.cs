using System.Threading.Tasks;
using UOW_101.Models;

namespace UOW_101.Interfaces
{
    public interface IEnrollmentRepository : IGenericRepository<Enrollment>
    {
        Task<bool> Update(Enrollment entity);
    }
}
