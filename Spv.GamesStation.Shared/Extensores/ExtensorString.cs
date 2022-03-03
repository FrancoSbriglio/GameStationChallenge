using System.Text.RegularExpressions;

namespace Spv.GamesStation.Shared.Extensores
{
    public static class ExtensorString
    {
        public static string ToSnakeCase(this string str)
        {
            Regex pattern = new Regex(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");
            return string.Join("_", pattern.Matches(str)).ToLower();
        }
    }
}
