namespace DatingApp.Domain.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        
        public string PublicId { get; set; }

        public string Url { get; set; }
        //What I have to choose? Bytes are very big for database, but in url
        //user cannot upload from the device
        public byte[] Data { get; set; }

        public bool IsMain { get; set; }

        public int AppUserId { get; set; }

        public AppUser Owner { get; set; }
    }
}
