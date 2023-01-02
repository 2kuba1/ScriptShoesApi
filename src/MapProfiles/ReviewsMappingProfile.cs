using AutoMapper;
using ScriptShoesAPI.Database.Entities;
using ScriptShoesAPI.Features.Reviews.Commands.UpdateReview;
using ScriptShoesAPI.Models.Reviews;

namespace ScriptShoesAPI.MapProfiles;

public class ReviewsMappingProfile : Profile
{
    public ReviewsMappingProfile()
    {
        CreateMap<UpdateReviewCommand, ReviewsDto>();
        CreateMap<Reviews, ReviewsDto>();
    }
}