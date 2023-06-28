using System;
using System.Text.Json;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Security;

namespace TestTaskCifrovoe_Prostranstvo
{
    class Program
    {
        static List<Models.Person> PersonsGenerator(int _n)
        {
            PersonBuilder builder = new PersonBuilder();
            List<Models.Person> persons = new List<Models.Person>();
            for (int i = 0; i < _n; i++)
            {
                try
                {
                    builder.BuildGender();
                    builder.BuildBurthDate();
                    builder.BuildAge();
                    builder.BuildChildren();
                    builder.BuildPhones();
                    builder.BuildFirstName();
                    builder.BuildLastName();
                    builder.BuildCreditCardNumbers();
                    builder.BuildSalary();
                    builder.BuildId();
                    builder.BuildIsMarred();
                    builder.BuildTransportId();
                    builder.BuildSequenceId();
                    persons.Add(builder.GetResult());
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                
            }
            return persons;
        }
        static void LogAcsessEror(Exception ex) 
        {
            Console.WriteLine("Отсутвует разрешение на доступ к файлу");
            Console.WriteLine(ex.Message);
        }
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Создаю данные");
                List<Models.Person> people = PersonsGenerator(10000);
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
                string path;
                try
                {
                    path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Persons.json";
                }
                catch (PlatformNotSupportedException ex)
                {
                    Console.WriteLine("платформа не потдерживаеться");
                    Console.WriteLine(ex.Message); ;
                    return;
                }
                try
                {
                    File.WriteAllText(path, jsonData);
                }
                catch (Exception ex) when(ex is SecurityException || ex is UnauthorizedAccessException)
                {
                    LogAcsessEror(ex);
                    return;
                }
                Console.WriteLine("Сбрасываю память");
                people = null;
                Console.WriteLine("Провожу десериализацию");
                try
                {
                    people = JsonConvert.DeserializeObject<List<Models.Person>>(File.ReadAllText(path));
                }
                catch (Exception ex) when (ex is SecurityException || ex is UnauthorizedAccessException)
                {
                    LogAcsessEror(ex);
                    return;
                }
                Console.WriteLine("Всего людей: " + people.Count);
                int cardCount = 0;
                int childCount = 0;
                int childAvv = 0;
                for (int i = 0; i < people.Count; i++)
                {
                    cardCount += people[i].CreditCardNumbers.Length;
                    for (int j = 0; j < people[i].Children.Length; j++)
                    {
                        childCount++;
                        childAvv += (int)(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - people[i].Children[j].BirthDate) / (int)(365 * 24 * 60 * 60);
                    }
                }
                Console.WriteLine("Банковских карт: " + cardCount);
                Console.WriteLine("Средний возраст детей: " + childAvv / childCount);
            }
            catch(Exception ex) 
            { 
                Console.WriteLine("Неожидаемое исключение");
                Console.WriteLine(ex.GetType().Name);  
                Console.WriteLine(ex.Message); 
                return; 
            }
         }
    }
}
