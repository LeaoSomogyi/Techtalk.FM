using System;

namespace Techtalk.FM.Domain.DTOs
{
    public class Book
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Author { get; set; }

        public DateTime PublishDate { get; set; }

        public int Version { get; set; }

        public int PageNumber { get; set; }

        public string PublishingHouse { get; set; }

        public string ISBN { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Book() { }

        public Book(Entities.Book book)
        {
            Id = book.Id;
            Title = book.Title;
            Subtitle = book.Subtitle;
            Author = book.Author;
            PublishDate = book.PublishDate;
            Version = book.Version;
            PageNumber = book.PageNumber;
            PublishingHouse = book.PublishingHouse;
            ISBN = book.ISBN;
            CreatedAt = book.CreatedAt;
            UpdatedAt = book.UpdatedAt;
        }
    }
}
