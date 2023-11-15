using Application.Persistence;
using Application.Persistence.Base;
using Domain.Entities;
using Domain.ValueObjects;
using FluentResults;
using MediatR;

namespace Application.Features.Courses;

public class AddCourseDefinition
    : IRequestHandler<AddCourseDefinition.AddCourseDefinitionCommand, Result<AddCourseDefinition.AddCourseDefinitionResponse>>
{
    private readonly ICoursesRepository _coursesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddCourseDefinition(
        ICoursesRepository coursesRepository,
        IUnitOfWork unitOfWork)
    {
        _coursesRepository = coursesRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AddCourseDefinitionResponse>> Handle(AddCourseDefinitionCommand request, CancellationToken cancellationToken)
    {
        // Sanitize data
        var startTime = new TimeOnly(request.Request.StartTime.Hour, request.Request.StartTime.Minute);
        var duration = new TimeSpan(request.Request.Duration.Hours, request.Request.Duration.Minutes, 0);

        // Get the specified course
        var courseId = new CourseId(request.Id);
        var course = await _coursesRepository.GetByIdIncludingDefinitionsAsync(courseId, cancellationToken);
        if(course is null)
            return null!;

        // Check for date and time coherence
        if(!IsDateCoherent(course, request.Request.DayOfWeek, startTime))
            return null!;

        // Create the course defiition and add it to the course
        if(course.Planning is null)
        {
            var defiition = Definition.CreateUnique();
            var definitionItem = DefinitionItem.CreateUnique(defiition, request.Request.DayOfWeek, startTime, duration);
            defiition.Items.Add(definitionItem);
            var planning = Planning.CreateUnique(course, defiition);
            course.SetPlanning(planning);
        }
        else
        {
            var definitionItem = DefinitionItem.CreateUnique(course.Planning.Definition, request.Request.DayOfWeek, startTime, duration);
            course.Planning.Definition.Items.Add(definitionItem);
        }

        // Save the changes
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Return
        return new AddCourseDefinitionResponse(
            course.Planning!.Definition.Items
                .Select(i => new Item(
                    i.DayOfWeek,
                    new Time(i.StartTime.Hour, i.StartTime.Minute),
                    new Duration(i.Duration.Hours, i.Duration.Minutes)
                ))
                .ToArray()
        );
    }

    private static bool IsDateCoherent(Course course, DayOfWeek dayOfWeek, TimeOnly startTime)
    {
        return
            ! course
                .Planning?
                .Definition
                .Items
                .Any(i => i.DayOfWeek == dayOfWeek && i.StartTime.Add(i.Duration) > startTime)

            ?? true;
    }

    public record AddCourseDefinitionRequest(
        DayOfWeek DayOfWeek,
        Time StartTime,
        Duration Duration
    );

    public record AddCourseDefinitionCommand(
        Guid Id,
        AddCourseDefinitionRequest Request
    ) : IRequest<Result<AddCourseDefinitionResponse>>;
    public record AddCourseDefinitionResponse(
        Item[] Items
    );

    public record Item(
        DayOfWeek DayOfWeek,
        Time StartTime,
        Duration Duration
    );

    public record struct Time(
        int Hour,
        int Minute
    );

    public record struct Duration(
        int Hours,
        int Minutes
    );
}
