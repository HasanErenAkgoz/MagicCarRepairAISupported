namespace MagicCarRepairAISupported.Application.Features.Ratings.Commands.ReplyToRating
{
    public class ReplyToRatingResponse
    {
        public int Id { get; set; }
        public string ServiceReply { get; set; } = string.Empty;
        public DateTime ServiceReplyDate { get; set; }
    }
}
