namespace Cat.API.Request
{
    public class PutCatRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double Price { get; set; }
    }
}
