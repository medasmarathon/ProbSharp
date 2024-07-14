using System.ComponentModel.DataAnnotations;
using App.Entities;
using App.Operations.Interfaces;
using FluentValidation;

namespace App.Operations.AddPEvent;

public class AddPEventRequestValidator : AbstractValidator<AddPEventRequest>
{
    public AddPEventRequestValidator()
    {
        RuleFor(r => r.Outcome).Must((request, outcome) =>
        {
            if (request.EventType == PEventType.Atomic)
                return outcome is not null;
            return true;
        });
        RuleFor(r => r.SubEvents).Must((request, subEvents) =>
        {
            if (request.EventType == PEventType.And || request.EventType == PEventType.Or)
                return subEvents is not null;
            return true;
        });
        RuleFor(r => r.ConditionEvent).Must((request, conditionEv) =>
        {
            if (request.EventType == PEventType.Conditional)
                return conditionEv is not null;
            return true;
        });
        RuleFor(r => r.SubjectEvent).Must((request, subjectEv) =>
        {
            if (request.EventType == PEventType.Conditional)
                return subjectEv is not null;
            return true;
        });
    }
}
