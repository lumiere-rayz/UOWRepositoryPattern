using System;

namespace UOW_101.Models
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string InstructorName { get; set; }
        public string CourseUnit { get; set; }

    }
}
