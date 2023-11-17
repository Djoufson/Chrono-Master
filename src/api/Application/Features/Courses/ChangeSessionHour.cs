using Application.Features.Courses.Common;
using Application.Features.Courses.Errors;
using Application.Persistence;
using Domain.ValueObjects;
using FluentResults;
using MediatR;

namespace Application.Features.Courses;

public class ChangeSessionHour
    : IRequestHandler<ChangeSessionHour.ChangeSessionHourCommand, Result<ChangeSessionHour.ChangeSessionHourResponse>>
{
    private readonly ICoursesRepository _coursesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeSessionHour(
        ICoursesRepository coursesRepository,
        IUnitOfWork unitOfWork)
    {
        _coursesRepository = coursesRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ChangeSessionHourResponse>> Handle(ChangeSessionHourCommand request, CancellationToken cancellationToken)
    {
        // Get the course
        var courseId = new CourseId(request.CourseId);
        var course = await _coursesRepository.GetByIdIncludingAllsAsync(courseId, cancellationToken);
        if(course is null)
            return Result.Fail(CourseErrors.CourseNotFoundError);

        if(course.Planning is null)
            return Result.Fail(CourseErrors.PlanningNotSetError);

        // Get the specific Session
        var sessionId = new SessionId(request.SessionId);
        var session = course.Planning.Sessions.FirstOrDefault(s => s.Id == sessionId);
        if(session is null)
            return Result.Fail(CourseErrors.SessionNotFoundError);

        // Change the hour of this session
        var duration = new TimeSpan(request.Request.Duration.Hours, request.Request.Duration.Minutes, 0);
        var startDateTime = request.Request.StartDateTime;
        var @override = request.Request.Override;

        // Override for all related sessions
        await course.Planning.ChangeSessionHourAsync(
            session,
            startDateTime,
            duration,
            @override);

        // Save changes
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Return
        return new ChangeSessionHourResponse(true);
    }

    public record ChangeSessionHourCommand(
        Guid CourseId,
        Guid SessionId,
        ChangeSessionHourRequest Request
    ) : IRequest<Result<ChangeSessionHourResponse>>;

    public record ChangeSessionHourResponse(
        bool Success
    );

    public record ChangeSessionHourRequest(
        DateTime StartDateTime,
        Duration Duration,
        bool Override = false
    );
}
