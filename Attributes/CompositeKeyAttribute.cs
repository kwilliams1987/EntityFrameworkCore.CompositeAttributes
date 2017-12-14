namespace System.ComponentModel.DataAnnotations.Schema
{
    //
    // Summary:
    //     Denotes one or more properties that uniquely identify an entity.
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class CompositeKeyAttribute : Attribute
    {
        public CompositeKeyAttribute() { }
    }
}
