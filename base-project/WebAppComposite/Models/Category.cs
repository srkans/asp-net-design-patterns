namespace WebAppComposite.Models
{
    public class Category
    {
        //Id    Name   UserId    ReferenceId
        //1  kitaplar    1          0
        //2  kitaplar1   1          1

        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public ICollection<Book> Books { get; set; }    
        public int ReferenceId { get; set; } // kategori baglantilarini takip etmek icin

    }
}
