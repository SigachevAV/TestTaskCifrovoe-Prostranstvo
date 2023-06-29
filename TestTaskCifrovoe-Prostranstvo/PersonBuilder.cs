using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskCifrovoe_Prostranstvo.Interfaces;
using TestTaskCifrovoe_Prostranstvo.Models;

namespace TestTaskCifrovoe_Prostranstvo
{
    public class PersonBuilder : ChildBuilder, IPersonBuilder
    {
        private Person person = new Person();
        private String[] GetRandomCards()
        {
            int count = new Random().Next() % 3;
            String[] result = new String[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = GetRandomNumericString(16);
            }
            return result;
        }
        private String GetRandomNumericString(int _n)
        {
            String random = String.Empty;
            for (int i = 0; i < _n; i++)
            {
                random += Convert.ToString(new Random().Next() % 10);
            }
            return random;
        }
        private String GetRandomPhone()
        {
            String result = "+7";
            result += GetRandomNumericString(10);
            return result;
        }
        private String[] GetRandomPhones()
        {
            int count = new Random().Next() % 3;
            String[] result = new String[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = GetRandomPhone();
            }
            return result;
        }
        private Child[] GetRandomChildren(int _n)
        {
            Child[] result = new Child[_n];
            ChildBuilder builder = new ChildBuilder();
            for (int i = 0; i < _n; i++)
            {
                builder.BuildGender();
                builder.BuildBurthDate();
                builder.BuildFirstName();
                builder.BuildLastName();
                builder.BuildId();
                result[i] = builder.GetResult();
            }
            return result;
        }

        public new void BuildId()
        {
            this.person.Id = Faker.RandomNumber.Next(1, 10000);
        }
        public new void BuildBurthDate()
        {
            this.person.BirthDate = BurthDateGeneration(18, 50);
        }

        public void BuildAge()
        {
            if(this.person.BirthDate == null)
            {
                throw new Exception("Неверный порядок");
            }
            this.person.Age = (int)((Int64)(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - this.person.BirthDate) / (Int64)(365 * 24 * 60 * 60));
        }

        public void BuildChildren()
        {
            this.person.Children = GetRandomChildren(Faker.RandomNumber.Next(0, 3));
        }

        public void BuildCreditCardNumbers()
        {
            this.person.CreditCardNumbers = GetRandomCards();
        }

        public void BuildIsMarred()
        {
            this.person.IsMarred = Faker.Boolean.Random();
        }

        public void BuildPhones()
        {
            this.person.Phones = GetRandomPhones();
        }

        public void BuildSalary()
        {
            this.person.Salary = (Double)Faker.RandomNumber.Next(1000, 100000);
        }

        public void BuildSequenceId()
        {
            if (this.person.Id == null)
            {
                throw new Exception("Неверный порядок");
            }
            this.person.SequenceId = this.person.Id;
        }

        public void BuildTransportId()
        {
            this.person.TransportId = Guid.NewGuid();
        }

        public new Person GetResult()
        {
            this.person.FirstName = this.child.FirstName;
            this.person.LastName = this.child.LastName;
            this.person.Gender = this.child.Gender;
            Person result = this.person;
            this.person = new Person();
            return result;
        }
    }
}
