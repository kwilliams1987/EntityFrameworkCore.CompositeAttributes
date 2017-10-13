using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace Microsoft.EntityFrameworkCore
{
    public abstract class CompositeKeyDbContext: DbContext
    {
        public CompositeKeyDbContext()
            : base() { }

        public CompositeKeyDbContext(DbContextOptions options)
            : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in AppDomain.CurrentDomain.GetTypesByPropertyAttribute(typeof(CompositeKeyAttribute)))
            {
                if (modelBuilder.Model.ContainsEntity(entity))
                {
                    var keyProperties = entity.GetPropertiesByAttribute(typeof(CompositeKeyAttribute)).Select(p => p.Name).ToArray();
                    modelBuilder.Entity(entity).HasKey(keyProperties);
                }
            }

            foreach (var entity in AppDomain.CurrentDomain.GetTypesByPropertyAttribute(typeof(CompositeInversePropertyAttribute)))
            {
                if (modelBuilder.Model.ContainsEntity(entity))
                {
                    var inverseProperties = entity.GetPropertiesByAttribute(typeof(CompositeInversePropertyAttribute));

                    foreach (var property in inverseProperties)
                    {
                        var attribute = property.GetAttribute<CompositeInversePropertyAttribute>();
                        var targetType = property.PropertyType;

                        if (targetType.IsGenericType && typeof(IEnumerable).IsAssignableFrom(targetType.GetGenericTypeDefinition()))
                        {
                            var argumentType = targetType.GetGenericArguments()[0];

                            var targetProperties = argumentType.GetProperties().ToList();
                            var targetProperty = targetProperties.FirstOrDefault(p => p.Name == attribute.Property);

                            if (targetProperty != default(PropertyInfo))
                            {
                                var targetAttribute = targetProperty.GetAttribute<CompositeForeignKeyAttribute>();
                                if (targetAttribute != default(CompositeForeignKeyAttribute))
                                {
                                    if (modelBuilder.Model.ContainsEntity(argumentType))
                                    {
                                        modelBuilder.Entity(argumentType)
                                            .HasOne(entity, targetProperty.Name)
                                            .WithMany(property.Name)
                                            .HasForeignKey(targetAttribute.Name.Split(',').Select(c => c.Trim()).ToArray());
                                    }
                                }
                            }
                        }
                        else
                        {
                            var targetProperties = targetType.GetProperties().ToList();
                            var targetProperty = targetProperties.FirstOrDefault(p => p.Name == attribute.Property);

                            if (targetProperty != default(PropertyInfo))
                            {
                                var targetAttribute = targetProperty.GetAttribute<CompositeForeignKeyAttribute>();
                                if (targetAttribute != default(CompositeForeignKeyAttribute))
                                {
                                    if (modelBuilder.Model.ContainsEntity(targetType))
                                    {
                                        modelBuilder.Entity(targetType)
                                            .HasOne(entity, targetProperty.Name)
                                            .WithOne(property.Name)
                                            .HasForeignKey(targetType, targetAttribute.Name.Split(',').Select(c => c.Trim()).ToArray());
                                    }
                                }
                            }
                        }
                    }
                }        
            }

            
            base.OnModelCreating(modelBuilder);
        }
    }
}
