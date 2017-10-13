using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.EntityFrameworkCore
{
    static class IMutableModelExtensions
    {
        public static Boolean ContainsEntity(this IMutableModel model, Type type)
        {
            return model.FindEntityType(type) != null;
        }
    }
}
