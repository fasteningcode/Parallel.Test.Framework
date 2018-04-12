using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Parallel.Test.Framework.Lib.DotNet
{
    public static class CustomStringLib
    {
        /// <summary>  
        /// Remove space from the String  
        /// </summary>  
        /// <param name="entity">String</param>  
        /// <returns></returns>  
        public static string RemoveSpace(this string entity)
        {
            return ReplaceParticularCharacter(entity, " ", string.Empty);
        }

        /// <summary>  
        /// Remove last character from the String  
        /// </summary>  
        /// <param name="entity">String</param>  
        /// <returns></returns>  
        public static string RemoveLastCharacter(this string entity)
        {
            return string.IsNullOrWhiteSpace(entity) ? string.Empty : entity.Remove((entity.Length - 1), 1);
        }


        /// <summary>  
        /// Remove first character from String  
        /// </summary>  
        /// <param name="entity">String</param>  
        /// <returns></returns>  
        public static string RemoveFirstCharacter(this string entity)
        {
            return string.IsNullOrWhiteSpace(entity) ? string.Empty : entity.Remove(0, 1);
        }


        /// <summary>  
        /// Create a string from an array of string using StringBuilder class and specified separator  
        /// </summary>  
        /// <param name="entities">List of string</param>  
        /// <param name="separator">Separator</param>  
        /// <param name="trim"></param>  
        /// <returns></returns>  
        public static string ToString(this string[] entities, string separator, bool trim = false)
        {
            StringBuilder str = new StringBuilder();
            foreach (var item in entities)
            {
                str.Append(trim ? (item.Trim() + separator) : (item + separator));
            }
            return str.ToString().RemoveLastCharacter();
        }


        /// <summary>  
        /// Replace old character with new character in a string  
        /// </summary>  
        /// <param name="entity">String</param>  
        /// <param name="oldCharacter">Old character</param>  
        /// <param name="newCharacter">New Character</param>  
        /// <returns></returns>  
        public static string ReplaceParticularCharacter(this string entity, string oldCharacter, string newCharacter)
        {
            return entity.Replace(oldCharacter, newCharacter);
        }

        /// <summary>  
        /// Create MD5 Hash of a String  
        /// </summary>  
        /// <param name="entity">String</param>  
        /// <returns></returns>  
        public static string CreateMd5Hash(this string entity)
        {
            var md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(entity));

            StringBuilder sBuilder = new StringBuilder();
            foreach (byte t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }
            return sBuilder.ToString();
        }

        /// <summary>  
        /// Convert the string to SEO friendly slug  
        /// </summary>  
        /// <param name="entity">String</param>  
        /// <returns></returns>  
        public static string ToSeoSlug(this string entity)
        {
            var str = entity.ReplaceParticularCharacter("&", "and");
            // Remove non word characters  
            Regex rgx = new Regex("\\W");

            // Remove multiple dash characters from the string  
            Regex rgx1 = new Regex("[-]+");

            var removeMultipleDash = rgx1.Replace(rgx.Replace(str, "-"), "-");

            var removeFirstDash = removeMultipleDash.StartsWith("-") ? removeMultipleDash.RemoveFirstCharacter() : removeMultipleDash;
            var removeLastDash = removeFirstDash.EndsWith("-") ? removeFirstDash.RemoveLastCharacter() : removeFirstDash;
            return removeLastDash.ToLower();
        }
    }
}
