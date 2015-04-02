using System;

namespace Stackoverflow_CGVM.Domain.Entities
{
    public class Account : IEntity
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string LastName{ get; set; }

        public bool confirmed { get; set; }
        public string Email { get; set; }
        public string Passw { get; set; }

        public string RegistrationDate { get; set; }
        public int Vistas { get; set; }
        public string LastSeen { get; set; }
        public Account()
        {
            Id = Guid.NewGuid();
        }
    }
}