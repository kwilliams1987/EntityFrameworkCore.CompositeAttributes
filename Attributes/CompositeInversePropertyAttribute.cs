namespace System.ComponentModel.DataAnnotations.Schema
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class CompositeInversePropertyAttribute: Attribute
    {
        public String Property { get; }

        public CompositeInversePropertyAttribute(String name)
        {
            Property = name;
        }
    }
}
