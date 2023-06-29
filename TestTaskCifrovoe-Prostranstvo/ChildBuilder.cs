using System;
using TestTaskCifrovoe_Prostranstvo.Interfaces;
using TestTaskCifrovoe_Prostranstvo.Models;

namespace TestTaskCifrovoe_Prostranstvo
{
    public class ChildBuilder : IChildBuilder
    {
        protected Child child = new Child();
        protected long BurthDateGeneration(int minAge, int maxAge)
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds() - (60 * 60 * 24 * 365 * Faker.RandomNumber.Next(minAge, maxAge));
        }
        public Child GetResult()
        {
            Child result = this.child;
            this.child = new Child();
            return result;
        }

        public void BuildFirstName()
        {
            this.child.FirstName=Faker.Name.First();
        }

        public void BuildLastName()
        {
            this.child.LastName = Faker.Name.Last();
        }

        public void BuildGender()
        {
            this.child.Gender = Faker.Enum.Random<Gender>();
        }

        public void BuildBurthDate()
        {
            this.child.BirthDate = BurthDateGeneration(1, 17);
        }

        public void BuildId()
        {
            this.child.Id=Faker.RandomNumber.Next(10000, 20000);
        }
    }
}
