namespace System.ComponentModel.DataAnnotations.Schema
{
    //
    // Summary:
    //     Denotes the properties used as foreign keys in a relationship. The annotation may
    //     be placed on the foreign key property and specify the associated navigation property
    //     name, or placed on a navigation property and specify the associated foreign key
    //     name.
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
