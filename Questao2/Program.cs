using Newtonsoft.Json;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = GetTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = GetTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int GetTotalScoredGoals(string team, int year)
    {
        var tasks = new[] { GetTotalScoredGoals(team, year, "team1"), GetTotalScoredGoals(team, year, "team2") };
        var results = Task.WhenAll(tasks);

        var sum = 0;
        foreach (var item in results.Result)
        {
            sum += item;
        }
        return sum;
    }

    public static async Task<int> GetTotalScoredGoals(string team, int year, string filter)
    {
        var sum = 0;

        using var client = new HttpClient();
        var apiUrl = "https://jsonmock.hackerrank.com/api/football_matches";

        var getAll = false;
        for (var i = 0; !getAll; i++)
        {
            var query = FormQueryString(year, filter, team, i);
            var response = await client.GetAsync($"{apiUrl}?{query}");
            response.EnsureSuccessStatusCode();

            var contentString = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<FootballMatchesResponse>(contentString) ?? throw new Exception();

            foreach (var item in content.data)
            {
                if (item.team1 == team)
                {
                    sum += int.Parse(item.team1goals);
                }
                else if (item.team2 == team)
                {
                    sum += int.Parse(item.team2goals);
                }
                else
                {
                    throw new Exception();
                }
            }

            i = content.page;
            if (i == content.total_pages)
            {
                getAll = true;
            }
        }
        return sum;
    }

    public static string FormQueryString(int year, string filter, string team, int page)
    {
        var query = $"year={year}&{filter}={team}";
        if (page > 1)
        {
            query += $"&page={page}";
        }
        return query;
    }

    public record FootballMatchesResponse(int page, int per_page, int total, int total_pages, IList<FootballMatchesData> data);
    public record FootballMatchesData(string team1, string team1goals, string team2, string team2goals);
}