namespace WebAppComposite.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; } //foreign key oldugunu isimlendirmeden dolayi anliyor EF
                                              //diger turlu foreign key attribute kullanmak gerekiyordu
    }
}
