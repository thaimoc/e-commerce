namespace eCommerce.SharedKernel
{
    public class ParameterInfo<T>
    {
        public T Value { get; }
        public string Name { get; }

        public ParameterInfo(T value, string name)
        {
            Value = value;
            Name = name;
        }
    }
}