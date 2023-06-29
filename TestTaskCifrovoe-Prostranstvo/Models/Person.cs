using System;


namespace TestTaskCifrovoe_Prostranstvo.Models
{
    public class Person : Child
    {
		public Guid TransportId { get; set; }
		public Int32 SequenceId { get; set; }
		public String[] CreditCardNumbers { get; set; }
		public Int32 Age { get; set; }
		public String[] Phones { get; set; }
		public Double Salary { get; set; }
		public Boolean IsMarred { get; set; }
		public Child[] Children { get; set; }
	}
}
