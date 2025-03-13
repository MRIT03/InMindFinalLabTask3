using System;
using System.Linq;
using System.Reflection;

/* This is an object mapper between two types, a source type (Tsource) and a destination type (TDestination)
 *
 * The mapper works by first creating a new instance of the destination type
 *
 * It then collects all the properties of the source type and destination type
 *
 * Then for each property within the source and the destination, if the properties name match (case-insensitive)
 * then that property in the destination object will be set to the value inside the source object.
 * this will also check if the property inside the destination object is null/writeable as to avoid any potential errors
 * properties that do not match between source and destination will be set to null.
 *
 * This service highlights the power of reflection and generics as the mapper does not need to know the type of the
 * source and destination objects at compile time. It will deduce the types at runtime, enhancing its capability in
 * mapping different types.
 *
 * There is an imposed constraint on this mapper, as the TDestination type needs to have a parameterless constructor
 * 
 */



namespace InMindLab1.Services
{
    public class ObjectMapperService
    {
        // Generics are used here so we don't need to hardcode the types.
        public TDestination Map<TSource, TDestination>(TSource source) where TDestination : new()
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            TDestination destination = new TDestination();

            // Reflection is used here to determine the type of the objects and allows us to read their properties.
            PropertyInfo[] sourceProperties = typeof(TSource).GetProperties();
            PropertyInfo[] destinationProperties = typeof(TDestination).GetProperties();

            foreach (var sourceProp in sourceProperties)
            {
                var destProp = destinationProperties.FirstOrDefault(p =>
                    p.Name.Equals(sourceProp.Name, StringComparison.OrdinalIgnoreCase) &&
                    p.PropertyType == sourceProp.PropertyType);

                if (destProp != null && destProp.CanWrite)
                {
                    destProp.SetValue(destination, sourceProp.GetValue(source));
                }
            }

            return destination;
        }
    }
}