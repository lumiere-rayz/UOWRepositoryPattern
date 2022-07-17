using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UOW_101.Data;
using UOW_101.Interfaces;
using UOW_101.Models;

namespace UOW_101.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        IStudentRepository studentRepository;
        public CourseRepository(DBContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<IEnumerable<Course>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(StudentRepository));
                return new List<Course>();
            }
        }
        public override async Task<bool> Upsert(Course entity)
        {
            try
            {

                var existingUser = await dbSet.Where(x => x.Id == entity.Id)
                                                    .FirstOrDefaultAsync();

                if (entity == null)
 

                    existingUser.Title = entity.Title;
                    existingUser.InstructorName = entity.InstructorName;
                    existingUser.CourseUnit = entity.CourseUnit;
                    return await Add(existingUser);


                //return true;



            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(CourseRepository));
                return false;
            }
        }
        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var exist = await dbSet.Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

                dbSet.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", typeof(CourseRepository));
                return false;
            }
        }
    }
}
