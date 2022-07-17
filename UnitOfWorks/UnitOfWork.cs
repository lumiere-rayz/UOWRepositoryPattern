using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using UOW_101.Data;
using UOW_101.Interfaces;
using UOW_101.Repositories;

namespace UOW_101.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DBContext _context;
        private readonly ILogger _logger;

        public IStudentRepository Student { get; private set; }
        public ICourseRepository Course { get; private set; }
        public IEnrollmentRepository Enrollment { get; private set; }


        public UnitOfWork(DBContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            Student = new StudentRepository(context, _logger);
            Course = new CourseRepository(context, _logger);
            Enrollment = new EnrollmentRepository(context, _logger);



        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
