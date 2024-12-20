﻿using Udemy.Web.Models.Repository.Entities;

namespace Udemy.Web.Models.Repository.CourseRepository
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
       Task<IEnumerable<Course>> GetCoursesByUserIdAsync(Guid userId);
       Task<IEnumerable<Course>> GetCoursesAsync();
       Task<Course?> GetCourseByIdAsync(Guid id);
    }
}
