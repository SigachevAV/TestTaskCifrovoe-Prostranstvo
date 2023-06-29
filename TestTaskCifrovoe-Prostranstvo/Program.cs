using System;

namespace TestTaskCifrovoe_Prostranstvo
{
    class Program
    {
        static void Main(string[] args)
        {
            Task task = new Task();
            try
            {
                task.PersonsGenerator(10000);
                task.Serialize();
                task.ResolvePath();
                task.Write();
                task.Dispose();
                task.Deserialize();
                task.PrintCount();
            }
            catch(Exception ex) 
            {
                task.Log(ex);
                return;
            }
         }
    }
}
