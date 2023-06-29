using TestTaskCifrovoe_Prostranstvo.Models;

namespace TestTaskCifrovoe_Prostranstvo.Interfaces
{
    interface IPersonBuilder : IChildBuilder
    {
        
        public new Person GetResult();
        public new void BuildBurthDate();
        public void BuildTransportId();
        public void BuildSequenceId();
        public void BuildCreditCardNumbers();
        public void BuildAge();
        public void BuildPhones();
        public void BuildSalary();
        public void BuildIsMarred();
        public void BuildChildren();
    }
}
