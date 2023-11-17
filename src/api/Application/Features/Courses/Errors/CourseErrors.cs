namespace Application.Features.Courses.Errors;

public class CourseErrors
{
    public static DepartmentNotFoundError DepartmentNotFoundError => new();
    public static CourseNotFoundError CourseNotFoundError => new();
    public static ConcurrentScheduleError ConcurrentScheduleError => new();
    public static SessionNotFoundError SessionNotFoundError => new();
    public static PlanningNotSetError PlanningNotSetError => new();
}
