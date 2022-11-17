using AutoMapper;
using ScriptShoesCQRS.Features.Reviews.Commands.UpdateReview;
using ScriptShoesCQRS.Models.Reviews;

namespace ScriptShoesCQRS.MapProfiles;

public class ReviewsMappingProfile : Profile
{
    public ReviewsMappingProfile()
    {
        CreateMap<UpdateReviewCommand, ReviewsDto>();
    }
}