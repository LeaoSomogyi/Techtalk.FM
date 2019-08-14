using DTO = Techtalk.FM.Domain.DTOs;

namespace Techtalk.FM.Domain.Entities
{
    public class User : BaseModel
    {
        public virtual string Name { get; set; }

        public virtual string Email { get; set; }

        public virtual string Password { get; set; }

        public User() : base() { }

        public User(DTO.User user)
        {
            Email = user.Email;
            Password = user.Password;
        }
    }
}
