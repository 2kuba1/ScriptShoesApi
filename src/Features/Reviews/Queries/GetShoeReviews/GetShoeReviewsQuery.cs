﻿using MediatR;
using ScriptShoesCQRS.Models.Reviews;

namespace ScriptShoesCQRS.Features.Reviews.Queries.GetShoeReviews;

public class GetShoeReviewsQuery : IRequest<IEnumerable<ReviewsDto>>
{
    public int ShoeId { get; set; }
}