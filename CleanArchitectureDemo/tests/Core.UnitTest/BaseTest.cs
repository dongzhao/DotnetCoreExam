using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Core.UnitTest
{
    public abstract class BaseTest
    {
        protected string ToJson(object result)
        {
            return JsonSerializer.Serialize(result, new JsonSerializerOptions()
            {
                // optional setting
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });
        }

        protected T? FromJsonFile<T>(string filename)
        {
            var json = File.ReadAllText(filename);
            return FromJson<T>(json);
        }

        protected T? FromJson<T>(string json)
        {
            var result = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions() {
                // optional setting
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });

            if(result == null)
            {
                return default(T);
            }
            else
            {
                return (T)result;
            }
        }

        protected void Copy<T>(T from, T to, string[]? excludedFields = null)
        {
            var type = typeof(T);
            var primitiveTypeArr = new Type[]
            {
                typeof(string),
            };
            var props = type.GetProperties().Where(c =>
                    c.PropertyType.IsPrimitive ||
                    primitiveTypeArr.Contains(c.PropertyType));
            foreach (var prop in props)
            {
                try
                {
                    if (prop.GetCustomAttributes(typeof(KeyAttribute)) != null) continue; // skip key 
                    if (prop.GetCustomAttributes(typeof(ForeignKeyAttribute)) != null) continue; // skip foreign key
                    if (excludedFields != null && excludedFields.Length > 0 && excludedFields.Contains(prop.Name)) continue; // skip excluded fields
                    var actural = prop.GetValue(from, null);
                    prop.SetValue(to, actural, null);

                }
                catch (Exception ex)
                {
                    Assert.Fail($"Copy error: '{prop.Name}' field failed to copy!");
                }
            }
        }

        protected void Verify<T>(T from, T to, string[]? excludedFields = null)
        {
            var type = typeof(T);
            var primitiveTypeArr = new Type[]
            {
                typeof(string),
            };
            var props = type.GetProperties().Where(c => 
                    c.PropertyType.IsPrimitive || 
                    primitiveTypeArr.Contains(c.PropertyType));
            foreach(var prop in props)
            {
                try
                {
                    if (prop.GetCustomAttributes(typeof(KeyAttribute)) != null) continue; // skip key 
                    if (prop.GetCustomAttributes(typeof(ForeignKeyAttribute)) != null) continue; // skip foreign key
                    if (excludedFields != null && excludedFields.Length > 0 && excludedFields.Contains(prop.Name)) continue; // skip excluded fields
                    var actural = prop.GetValue(from, null);
                    var expected = prop.GetValue(to, null);
                    Assert.That(actural, Is.EqualTo(expected), $"{prop.Name} of {type.FullName}: value doesnt match");
                }
                catch(Exception ex)
                {
                    Assert.Fail($"Verify Error: '{prop.Name}' field");
                }
            }
        }

        protected void Verify<T>(List<T> from, List<T> to)
        {
            Assert.That(from.Count, Is.EqualTo(to.Count), $"length doesn't match");
            Assert.Multiple(() => { 
                for(var idx = 0; idx<from.Count; idx++)
                {
                    Verify<T>(from[idx], to[idx]);
                }
            });
        }
    }

}
