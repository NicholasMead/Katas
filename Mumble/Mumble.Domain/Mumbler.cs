using System.Text.RegularExpressions;

namespace Mumble.Domain;

public class Mumbler
{
    public string Mumble(string input)
    {
        var accept = @"^\w*$";

        if (!Regex.IsMatch(input, accept))
            throw new ArgumentException("Invalid characters", nameof(input));

        IEnumerable<string> sections = input.Select((char c, int i) =>
        {
            var section = "";
            for (int j = 0; j <= i; j++)
                section += j == 0 ? char.ToUpper(c) : char.ToLower(c);
            return section;
        });

        return string.Join('-', sections);
    }
}
