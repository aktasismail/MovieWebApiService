using BusinessLayer.Abstact;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class DataShaper<T> : IDataShaper<T> where T : class
    {
        public PropertyInfo[] properties { get; set; }
        public DataShaper()
        {
            properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
        public IEnumerable<ShapedEntity> ShapeData(IEnumerable<T> shape, string fieldstring)
        {
            var requiredFields = GetRequiredfields(fieldstring);
            return FetchData(shape, requiredFields);
        }

        public ShapedEntity ShapeData(T shape, string fieldstring)
        {
            var requiredProperties = GetRequiredfields(fieldstring);
            return FetchDataForEntity(shape, requiredProperties);
        }
        private IEnumerable<PropertyInfo> GetRequiredfields(string requiredfieldstring)
        {
            var requiredfields = new List<PropertyInfo>();
            if (!string.IsNullOrWhiteSpace(requiredfieldstring))
            {
                var fields = requiredfieldstring.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in fields)
                {
                    var prop  = properties.FirstOrDefault(p=>p.Name.Equals(item.Trim(),
                        StringComparison.InvariantCultureIgnoreCase));
                    if (prop is null)
                        continue;
                    requiredfields.Add(prop);
                }
            }
            else
            {
                requiredfields = properties.ToList();
            }
            return requiredfields;      
        }
        private ShapedEntity FetchDataForEntity(T entity,
           IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedObject = new ShapedEntity();

            foreach (var property in requiredProperties)
            {
                var objectPropertyValue = property.GetValue(entity);
                shapedObject.Entity.TryAdd(property.Name, objectPropertyValue);
            }
            var objectproperty = entity.GetType().GetProperty("Id");
            shapedObject.Id = (int)objectproperty.GetValue(entity);
            return shapedObject;
        }
        private IEnumerable<ShapedEntity> FetchData(IEnumerable<T> entities,
            IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedData = new List<ShapedEntity>();
            foreach (var entity in entities)
            {
                var shapedObject = FetchDataForEntity(entity, requiredProperties);
                shapedData.Add(shapedObject);
            }
            return shapedData;
        }
    }
}
