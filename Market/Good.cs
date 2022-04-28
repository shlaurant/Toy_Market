namespace Market
{
    public class Good
    {
        public readonly string Name;

        public Good(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}";
        }
    }
}