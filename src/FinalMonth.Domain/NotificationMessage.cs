namespace FinalMonth.Domain
{
    public class NotificationMessage{
        public string NotificationId { get; set; }
        public string Message { get; set; }
        public string From { get; set; }

        public override string ToString()
        {
            return $"{From}:{Message}";
        }

        public static NotificationMessage Create(string user, string message)
        {
            return new NotificationMessage
            {
                From = user,
                Message = message
            };
        }
    }
}
