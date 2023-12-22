public class Part
{
    public Dictionary<string, int> minimums = [];
    public Dictionary<string, int> maximums = [];

    public Part()
    {
        string[] attrs = ["x", "m", "a", "s"];
        foreach(string attr in attrs)
        {
            minimums.Add(attr, 0);
            maximums.Add(attr, 4001);
        }
    }

    public Part(Part part)
    {
        minimums = part.minimums.ToDictionary(entry => entry.Key, entry => entry.Value);
        maximums = part.maximums.ToDictionary(entry => entry.Key, entry => entry.Value);
    }

    public bool isValid()
    {
        foreach(string key in minimums.Keys)
        {
            if(minimums[key] >= maximums[key])
            {
                return false;
            }
        }

        return true;
    }
}