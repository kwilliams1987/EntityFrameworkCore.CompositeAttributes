namespace System.ComponentModel.DataAnnotations.Schema
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class CompositeForeignKeyAttribute: ForeignKeyAttribute
    {
        public CompositeForeignKeyAttribute(params String[] names)
            : base(String.Join(", ", names)) { }
    }
}
