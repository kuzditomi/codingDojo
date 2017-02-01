namespace Library.Contracts.Models
{
    public class Reader
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Reader(string name)
        {
            Name = name;
        }
    }
}
