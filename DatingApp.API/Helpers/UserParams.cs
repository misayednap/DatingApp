namespace DatingApp.API.Helpers
{
    public class UserParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize)? MaxPageSize: value; }
        }

// We will filter out the person that's logged in from the Member List
// Also filter out the same gender
        public int UserId { get; set; }
        public string Gender { get; set; }

        // To filter out people under 18
        public int MinAge { get; set; } = 18;
        public int MaxAge { get; set; } = 99;
        public string OrderBy { get; set; }
        public bool Likees { get; set; }
        public bool Likers { get; set; }
    }
}