using Application.Features.Courses.Errors;
using Application.Persistence;
using Domain.ValueObjects;
using FluentResults;
using MediatR;

namespace Application.Features.Courses;

public class ComputeSessionsGeneration
    : IRequestHandler<ComputeSessionsGeneration.ComputeSessionCommand, Result<ComputeSessionsGeneration.ComputeSessionResponse>>
{
    private readonly ICoursesRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ComputeSessionsGeneration(
        ICoursesRepository courseRepository,
        IUnitOfWork unitOfWork)
    {
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ComputeSessionResponse>> Handle(ComputeSessionCommand request, CancellationToken cancellationToken)
    {
        // Get the course
        var courseId = new CourseId(request.Id);
        var course = await _courseRepository.GetByIdIncludingAllsAsync(courseId, cancellationToken);

        // Compute
        if(course is null)
            return Result.Fail(CourseErrors.CourseNotFoundError);

        var (RemainingHours, Sessions) = await course.Planning!.GenerateSessionsAsync(request.StartDay, request.EndDay);

        // Save to the database
        if(request.Persist)
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Return
        return new ComputeSessionResponse(
            RemainingHours,
            Sessions
                .Select(s => new Session(
                    s.StartDateTime,
                    s.Duration
                ))
                .ToArray()
        );
    }

    public record ComputeSessionRequest(
        DateOnly StartDay,
        DateOnly EndDay,
        bool Persist = false);

    public record ComputeSessionCommand(
        Guid Id,
        DateOnly StartDay,
        DateOnly EndDay,
        bool Persist = false
    ) : IRequest<Result<ComputeSessionResponse>>;

    public record ComputeSessionResponse(
        int RemainingHours,
        Session[] Sessions
    );

    public record Session(
        DateTime StartDateTime,
        TimeSpan Duration);
}
