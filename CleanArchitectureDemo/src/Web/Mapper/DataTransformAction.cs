using AutoMapper;
using Shared.Attributes;
using System.Reflection;

namespace Web.Mapper
{
    public class DataTransformAction<Source, Target> : IMappingAction<Source, Target>
        where Source : class
        where Target : class
    {
        public void Process(Source source, Target destination, ResolutionContext context)
        {
            throw new NotImplementedException();

            var props = typeof(Target).GetProperties();
            foreach(var prop in props)
            {
                ToCaseSensitive(prop, destination);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="obj"></param>
        private void ToCaseSensitive(PropertyInfo prop, Target obj)
        {
            var attr = prop.GetCustomAttributes(typeof(CaseSensitiveAttribute), true).FirstOrDefault();
            if(attr == null)
            {
                return;
            }
            var value = prop.GetValue(obj, null);
            if (value == null || string.IsNullOrEmpty(value.ToString())) return;

            // to do: convert word to the first upper letter case 
            var output = value.ToString().ToUpper();
            prop.SetValue(obj, output);
        }
    }
}
