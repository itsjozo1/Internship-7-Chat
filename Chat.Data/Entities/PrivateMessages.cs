namespace Chat.Data.Entities;

public class PrivateMessage
{
        public int PrivateMessageId { get; set; }
        public DateTime SentTime { get; set; }
        public string Content { get; set; }
    
        public int SentUserId { get; set; }
        public User? SentUser { get; set; }
        public int RecievedUserId { get; set; }
        public User? RecievedUser { get; set; }
}