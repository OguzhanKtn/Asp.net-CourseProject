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

        public async Task<List<Course>> SearchCourseAsync(string query)
        {
            return await _context.Courses
                .Include(x => x.Category)
                .Where(c => EF.Functions.Like(c.Title, $"%{query}%"))
                .ToListAsync();
        }

        public IQueryable<Course> GetFilteredCourses(string? searchTerm, int? categoryId, decimal? minPrice, decimal? maxPrice, string? sortBy)
        {
            var query = _context.Courses.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(c => EF.Functions.Like(c.Title, $"%{searchTerm}%"));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(c => c.CategoryId == categoryId.Value);
            }

            if (minPrice.HasValue)
            {
                query = query.Where(c => c.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(c => c.Price <= maxPrice.Value);
            }

            query = sortBy switch
            {
                "price_asc" => query.OrderBy(c => c.Price),
                "price_desc" => query.OrderByDescending(c => c.Price),
                "newest" => query.OrderByDescending(c => c.CreatedAt),
                _ => query.OrderByDescending(c => c.CreatedAt)
            };

            return query;
        }


    }
}
