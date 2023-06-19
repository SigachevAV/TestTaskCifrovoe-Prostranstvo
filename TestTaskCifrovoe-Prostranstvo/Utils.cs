using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskCifrovoe_Prostranstvo
{
    class Utils
    {
        private String[] GetRandomCards()
        {
            int count = new Random().Next() % 3;
            String[] result = new String[count];
            for(int i =0; i<count; i++)
            {
                result[i] = GetRandomNumericString(16);
            }
            return result;
        }
        private String GetRandomNumericString(int _n)
        {
            String random = String.Empty;
            for(int i = 0; i< _n; i++)
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
        public Models.Child[] GetRandomChild(int _n)
        {
            Models.Child[] result = new Models.Child[_n];
            for (int i = 0; i < _n; i++)
            {
                result[i] = new Models.Child
                {
                    Id = Faker.RandomNumber.Next(10000, 1000000),
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last(),
                    BirthDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - (60*60*24*365*Faker.RandomNumber.Next(1, 17)),
                    Gender = Faker.Enum.Random<Models.Gender>()
                };
            }
            return result;
        }
        public List<Models.Person> PersonsGenerator(int _n)
        {
            List<Models.Person> persons = new List<Models.Person>();
            for (int i = 0; i < _n; i++)
            {
                persons.Add(new Models.Person
                {
                    Id = i,
                    TransportId = Guid.NewGuid(),
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last(),
                    SequenceId = i,
                    CreditCardNumbers = GetRandomCards(),
                    Age = Faker.RandomNumber.Next(18, 100),
                    Phones = GetRandomPhones(),
                    BirthDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    Salary = (Double)Faker.RandomNumber.Next(1000, 100000),
                    IsMarred = Faker.Boolean.Random(),
                    Gender = Faker.Enum.Random<Models.Gender>(),
                    Children = GetRandomChild(Faker.RandomNumber.Next(0, 3))
                });
            }
            return persons;
        }
    }
}
