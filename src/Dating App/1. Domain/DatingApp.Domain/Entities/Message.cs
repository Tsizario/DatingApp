﻿namespace DatingApp.Domain.Entities
{
    public class Message
    {
        public int Id { get; set; }

        public int SenderId { get; set; }

        public int RecipientId { get; set; }

        public string SenderUserName { get; set; }

        public string RecipientUserName { get; set; }

        public AppUser Sender { get; set; }

        public AppUser Recipient { get; set; }

        public string Content { get; set; }

        public bool IsSenderDeleted { get; set; }

        public bool IsRecipientDeleted { get; set; }

        public DateTime? DateOfChecked { get; set; }

        public DateTime? MessageSent { get; set; } = DateTime.UtcNow;
    }
}
