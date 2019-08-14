using System;

namespace Techtalk.FM.Domain.Entities
{
    public class Book : BaseModel
    {
        public virtual string Title { get; set; }

        public virtual string Subtitle { get; set; }

        public virtual string Author { get; set; }

        public virtual DateTime PublishDate { get; set; }

        public virtual int Version { get; set; }

        public virtual int PageNumber { get; set; }

        public virtual string PublishingHouse { get; set; }

        public virtual string ISBN { get; set; }

        public Book() : base() { }

        public Book(DTOs.Book book) : this()
        {
            Title = book.Title;
            Subtitle = book.Subtitle;
            Author = book.Author;
            PublishDate = book.PublishDate;
            Version = book.Version;
            PageNumber = book.PageNumber;
            PublishingHouse = book.PublishingHouse;
            ISBN = book.ISBN;
        }
    }
}
