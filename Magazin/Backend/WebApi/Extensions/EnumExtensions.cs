using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace WebApi.Extensions
{
    /// <summary>
    /// Расширения для ENUM
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Получить описание enum
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static Dictionary<int, string> GetEnumDesciption<TEnum>()
        {
            var returnDict = new Dictionary<int, string>();
            foreach (var item in Enum.GetValues(typeof(TEnum)))
            {
                returnDict.Add((int)item, ((Enum)item).GetDisplayName());
            }

            return returnDict;
        }

        /// <summary>
        /// Get display name
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static string GetDisplayName(this Enum @enum)
        {
            var attr = @enum.GetType().GetMember(@enum.ToString()).FirstOrDefault()?.GetCustomAttribute<DisplayAttribute>();
            if(attr != null)
            {
                return attr.Name;
            }
            else
            {
                return @enum.ToString();
            }
        }
    }
}
