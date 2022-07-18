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
    public class EnrollmentRepository : GenericRepository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(DBContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<IEnumerable<Enrollment>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(EnrollmentRepository));
                return new List<Enrollment>();
            }
        }
        public override async Task<bool> Upsert(Enrollment entity)
        {
            try
            {
                int countresult = await dbSet.Where(x => x.StudentId == entity.StudentId)
                                                    .CountAsync();

                var existingUser = await dbSet.Where(x => x.CourseId == entity.CourseId && x.StudentId == entity.StudentId)
                                                        .FirstOrDefaultAsync();

                if (existingUser == null && countresult < 3)
                {
                    return await Add(entity);

                    //existingUser.StudentId = entity.StudentId;
                    //existingUser.CourseId = entity.CourseId;
                    //return true;
                }
                else
                {
                    //return false;
                    throw new Exception("not allowed");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("not allowed(cant enroll for a course twice and more than three course)");

                //_logger.LogError(ex, "{Repo} Upsert function error", typeof(EnrollmentRepository));
                //return false;
            }
        }

        public async Task<bool> Update(Enrollment entity)
        {
            try
            {
                int countresult = await dbSet.Where(x => x.StudentId == entity.StudentId)
                                                    .CountAsync();

                var existingUser = await dbSet.Where(x => x.Id == entity.Id)
                                                        .FirstOrDefaultAsync();

                if (existingUser != null && countresult < 3)
                {

                    existingUser.StudentId = entity.StudentId;
                    existingUser.CourseId = entity.CourseId;
                    return await Add(existingUser);
                    //return true;
                }
                else
                {
                    throw new Exception("not allowed");
                    //return false;

                }

            }
            catch (Exception ex)
            {
                throw new Exception("not allowed");
                //_logger.LogError(ex, "{Repo} Upsert function error", typeof(EnrollmentRepository));
                //return false;
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(EnrollmentRepository));
                return false;
            }
        }
    }
}
