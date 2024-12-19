using Microsoft.EntityFrameworkCore;
using Udemy.Web.Models.Repository.Entities;

namespace Udemy.Web.Models.Repository.CourseRepository
{
    public class CourseRepository(AppDbContext context) : GenericRepository<Course>(context), ICourseRepository
    {
        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            return await _context.Courses.Include(x => x.Category).OrderByDescending(x => x.CreatedAt).ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetCoursesByUserIdAsync(Guid userId)
        {
           return await _context.Courses.Include(x => x.Category).Where(x => x.CreatedBy == userId).OrderByDescending(x => x.CreatedAt).ToListAsync();
        }

        public async Task<Course?> GetCourseByIdAsync(Guid id)
        {
            return await _context.Courses.Include(x => x.Category).Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
