using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Reviews;

public static class ReviewErrors
{
    public static readonly Error NotEligable = new("Review.NotEligable", 
        "The review is not eligible because the booking is not yet completed");
}
