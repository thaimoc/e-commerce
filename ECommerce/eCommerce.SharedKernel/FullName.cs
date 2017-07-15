namespace eCommerce.SharedKernel
{
    //no ID because it's a value object
    //EF will recognized this as a complex type
    //some methods borrowed from Vaughn Vernon IDDD.NET sample
    public class FullName : ValueObject<FullName>
    {
        public FullName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public FullName(FullName fullName)
          : this(fullName.FirstName, fullName.LastName)
        {
        }

        public FullName()
        {
        }


        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AsFormattedName()
        {
            return this.FirstName + " " + LastName;
        }

        public FullName WithChangedFirstName(string firstName)
        {
            return new FullName(firstName, LastName);
        }

        public FullName WithChangedLastName(string lastName)
        {
            return new FullName(FirstName, lastName);
        }
        public override string ToString()
        {
            return "FullName [firstName=" + FirstName + ", lastName=" + LastName + "]";
        }
    }
}