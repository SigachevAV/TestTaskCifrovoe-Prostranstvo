using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
