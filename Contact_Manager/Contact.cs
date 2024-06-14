namespace ContactManager
{
    public class Contact
    {
        public string Name { get; set; }
        public string Cat { get; set; }

        public Contact(string name, string category)
        {
            Name = name;
            Cat = category;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Category: {Cat}";
        }
    }
}