using System;
using System.Text.Json;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TestTaskCifrovoe_Prostranstvo
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Создаю данные");
            List<Models.Person> people = new Utils().PersonsGenerator(10000);
            Console.WriteLine("Провожу сериализацию");
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            string jsonData = JsonConvert.SerializeObject(people, new JsonSerializerSettings 
            {
                ContractResolver = contractResolver, 
                Formatting = Formatting.Indented
            });
            Console.WriteLine("Записываю в файл");
            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Persons.json";
            File.WriteAllText(path, jsonData);
            Console.WriteLine("Сбрасываю память");
            people = null;
            Console.WriteLine("Провожу десериализацию");
            people = JsonConvert.DeserializeObject<List<Models.Person>>(File.ReadAllText(path));
            Console.WriteLine("Всего людей: " + people.Count);
            int cardCount = 0;
            int childCount = 0;
            int childAvv = 0;
            for(int i =0; i < people.Count; i++)
            {
                cardCount += people[i].CreditCardNumbers.Length;
                for(int j = 0; j<people[i].Children.Length; j++)
                {
                    childCount++;
                    childAvv += (int)(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - people[i].Children[j].BirthDate) / (int)(365 * 24 * 60 * 60);
                }
            }
            Console.WriteLine("Банковских карт: " + cardCount);
            Console.WriteLine("Средний возраст детей: " + childAvv / childCount);
        }
    }
}
