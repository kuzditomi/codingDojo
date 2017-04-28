namespace Library.Web.Models
{
    public class BorrowViewModel
    {
        public int Id { get; set; }
        public string Reader { get; set; }
        public int Days { get; set; }
        public string Title { get; set; }
    }
}