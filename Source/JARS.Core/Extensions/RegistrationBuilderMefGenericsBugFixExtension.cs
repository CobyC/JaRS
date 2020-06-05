using System;
using System.ComponentModel.Composition.Primitives;

namespace JARS.Core.Extensions
{
    /// <summary>
    /// When RegistrationBuilder is used, there is problem with Generic constraints - in produced ExportDefinition is generic constraint with descriptor CustomType which is not comparable with Type. 
    /// * so composition failed on Export not found exception.
    /// http://stackoverflow.com/questions/24590096/type-constrained-open-generics-do-not-work-with-registrationbuilder
    /// </summary>
    public static class PatchCatalogForRegistrationBuilderBug
    {
        public static void FixCatalogForRegistrationBuilderBug(this ComposablePartCatalog catalog)
        {
            foreach (var item in catalog)
            {
                object value1;
                if (item.Metadata.TryGetValue("System.ComponentModel.Composition.GenericParameterConstraints", out value1))
                {
                    var items = (object[])value1;
                    foreach (var entry in items)
                    {
                        var types = entry as Type[];
                        if (types != null)
                        {
                            for (int i = 0; i < types.Length; i++)
                            {
                                if (((object)types[i]).GetType().FullName != "System.Reflection.Context.Custom.CustomType") continue; //cast to object is only for due to R# warning
                                types[i] = types[i].UnderlyingSystemType;
                            }
                        }
                    }
                }
            }
        }
    }
}
