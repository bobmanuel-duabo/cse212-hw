using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// Find symmetric pairs of two-character words using a set.
    /// Words with identical letters are excluded.
    /// </summary>
    public static string[] FindPairs(string[] words)
    {
        var seen = new HashSet<string>();
        var pairs = new List<string>();

        foreach (var word in words)
        {
            if (word[0] == word[1])
            {
                continue;
            }

            var reversed = $"{word[1]}{word[0]}";

            if (seen.Contains(reversed))
            {
                pairs.Add($"{reversed} & {word}");
            }

            seen.Add(word);
        }

        return pairs.ToArray();
    }

    /// <summary>
    /// Count the degrees found in the fourth column of the census file.
    /// The file has no header row.
    /// </summary>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();

        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            var degree = fields[3].Trim();

            if (degrees.ContainsKey(degree))
            {
                degrees[degree]++;
            }
            else
            {
                degrees[degree] = 1;
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine whether two words are anagrams.
    /// Ignore spaces and letter case. Use a dictionary.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        var counts = new Dictionary<char, int>();

        // Count letters in the first word.
        foreach (var letter in word1.ToUpperInvariant())
        {
            if (letter == ' ')
            {
                continue;
            }

            if (counts.ContainsKey(letter))
            {
                counts[letter]++;
            }
            else
            {
                counts[letter] = 1;
            }
        }

        // Subtract letters found in the second word.
        foreach (var letter in word2.ToUpperInvariant())
        {
            if (letter == ' ')
            {
                continue;
            }

            if (!counts.ContainsKey(letter))
            {
                return false;
            }

            counts[letter]--;

            if (counts[letter] == 0)
            {
                counts.Remove(letter);
            }
        }

        return counts.Count == 0;
    }

    /// <summary>
    /// Read earthquake JSON data and return locations and magnitudes.
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri =
            "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";

        using var client = new HttpClient();
        using var response = client.GetAsync(uri).GetAwaiter().GetResult();
        response.EnsureSuccessStatusCode();

        var json = response.Content.ReadAsStringAsync()
            .GetAwaiter().GetResult();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var featureCollection =
            JsonSerializer.Deserialize<FeatureCollection>(json, options)
            ?? throw new InvalidOperationException(
                "Unable to read earthquake data.");

        var summaries = new List<string>();

        foreach (var feature in featureCollection.Features)
        {
            var properties = feature.Properties;

            var magnitude = properties.Mag?.ToString(
                System.Globalization.CultureInfo.InvariantCulture)
                ?? "Unknown";

            summaries.Add($"{properties.Place} - Mag {magnitude}");
        }

        return summaries.ToArray();
    }
}