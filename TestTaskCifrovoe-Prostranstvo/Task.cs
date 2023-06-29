using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security;
using log4net;
using log4net.Layout;
using log4net.Appender;
using log4net.Config;
using System.IO;

namespace TestTaskCifrovoe_Prostranstvo
{
    internal class Task
    {
        private List<Models.Person> persones;
        private ILog loger;
        private string jsonData;
        private string path;

        public Task()
        {
            this.SetUpLogger();
        }

        private void SetUpLogger()
        {
            PatternLayout layout = new PatternLayout() { ConversionPattern = "[%level] [%class] [%method] - %message%newline" };
            layout.ActivateOptions();
            ConsoleAppender appender = new ConsoleAppender()
            {
                Layout = layout,
                Threshold = log4net.Core.Level.All,
                Name = "ConsoleAppender"
            };
            FileAppender fileAppender = new FileAppender()
            { 
                Layout= layout,
                Threshold = log4net.Core.Level.All,
                Name = "FileAppender",
                AppendToFile = true,
                File = "task.log"
            };
            appender.ActivateOptions();
            fileAppender.ActivateOptions();
            BasicConfigurator.Configure(appender, fileAppender);
            this.loger = LogManager.GetLogger(typeof(Task));
        }

        public void PersonsGenerator(int _n)
        {
            this.loger.Info("Создаю данные");
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
                    this.loger.Error(ex.Message);
                    throw ex;
                }

            }
            this.loger.Info("Данные созданы");
            this.persones =  persons;
        }
        public void Serialize()
        {
            this.loger.Info("Провожу сериализацию");
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            this.jsonData = JsonConvert.SerializeObject(this.persones, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });
            this.loger.Info("Сериализация завершена");
        }

        public void ResolvePath() 
        {
            this.loger.Info("Определяю путь");
            try
            {
                this.path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Persons.json";
            }
            catch (PlatformNotSupportedException ex)
            {
                this.loger.Error(ex.Message); 
                throw ex;
            }
            this.loger.Info("Путь определён");
        }

        public void Write()
        {
            this.loger.Info("Записываю");
            try
            {
                File.WriteAllText(path, jsonData);
            }
            catch (Exception ex) when (ex is SecurityException || ex is UnauthorizedAccessException)
            {
                this.loger.Error(ex.Message);
                throw ex;
            }
            this.loger.Info("Запись завершена");
        }

        public void Dispose() 
        {
            this.loger.Info("Сбрасываю память");
            this.persones= null;
        }

        public void Deserialize() 
        {
            this.loger.Info("Чтение и десериализация");
            try
            {
                this.persones = JsonConvert.DeserializeObject<List<Models.Person>>(File.ReadAllText(path));
            }
            catch (Exception ex) when (ex is SecurityException || ex is UnauthorizedAccessException)
            {
                this.loger.Error(ex.Message);
                throw ex;
            }
            this.loger.Info("Чтение и десериализация завершена");
        }

        public void PrintCount()
        {
            this.loger.Info("Подсчёт статистики");
            this.loger.Info("Всего людей: " + this.persones.Count);
            int cardCount = 0;
            int childCount = 0;
            int childAvv = 0;
            for (int i = 0; i < this.persones.Count; i++)
            {
                cardCount += this.persones[i].CreditCardNumbers.Length;
                for (int j = 0; j < this.persones[i].Children.Length; j++)
                {
                    childCount++;
                    childAvv += (int)(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - this.persones[i].Children[j].BirthDate) / (int)(365 * 24 * 60 * 60);
                }
            }
            this.loger.Info("Банковских карт: " + cardCount);
            this.loger.Info("Средний возраст детей: " + childAvv / childCount);
        }
        public void Log(Exception exception)
        {
            this.loger.Fatal(exception.Message);
        }
    }
}