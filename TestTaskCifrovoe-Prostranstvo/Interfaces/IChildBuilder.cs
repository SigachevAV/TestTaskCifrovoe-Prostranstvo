using TestTaskCifrovoe_Prostranstvo.Models;

namespace TestTaskCifrovoe_Prostranstvo.Interfaces
{
    public interface IChildBuilder
    {
        Child GetResult();
        void BuildFirstName();
        void BuildLastName();
        void BuildGender();
        void BuildBurthDate();
        void BuildId();
    }
}
