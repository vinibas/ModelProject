using System.Text.RegularExpressions;

namespace ProjetoSimples.Presentation.Utils
{
    public static class StringUtil
    {
        public static string RemoveExtraSpaces(this string value, bool withTrim)
            => new Regex(@"\s{2,}").Replace(value, " ");

        public static string FirstCharToUpper(this string value)
        {
            var res = value.ToCharArray();

            for (var i = 0; i < res.Length; i++)
            {
                if (res[i] != ' ')
                {
                    res[i] = char.ToUpper(res[i]);
                    return new string(res);
                }
            }

            return value;
        }
    }
}
