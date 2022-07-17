using System.Threading.Tasks;
using UOW_101.Interfaces;

namespace UOW_101.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IStudentRepository Student { get; }
        ICourseRepository Course { get; }

        IEnrollmentRepository Enrollment { get; }

        Task CompleteAsync();

        void Dispose();
    }
}
