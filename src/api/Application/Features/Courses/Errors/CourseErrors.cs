namespace Application.Features.Courses.Errors;

public class CourseErrors
{
    public static DepartmentNotFoundError DepartmentNotFoundError => new();
    public static CourseNotFoundError CourseNotFoundError => new();
    public static ConcurentScheduleError ConcurentScheduleError => new();
}
