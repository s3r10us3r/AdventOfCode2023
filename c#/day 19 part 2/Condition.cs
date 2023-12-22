public class Condition
{
    List<string> lilConditions;
    //default
    string def;
    public Condition(List<string> lilConditions, string def)
    {
        this.lilConditions = lilConditions;
        foreach(string lilCondition in lilConditions)
        {
            Console.WriteLine($"set lil condition: {lilCondition}");
        }
        this.def = def;
        Console.WriteLine($"set def {def}");
    }

    public List<(string condition, Part part)> Process(Part part)
    {
        List<(string, Part)> proccesedParts = [];
        Part newPart = new(part);
        foreach(var lilCondition in lilConditions)
        {
            string[] split = lilCondition.Split(":");
            string cond =  split[0];
            string ret = split[1];
            string attr = cond[0].ToString();
            int num = int.Parse(cond[3..]);
            if(cond[1] == '<')
            {
                Part passing = new(newPart);
                passing.maximums[attr] = num;

                if(passing.isValid())
                    proccesedParts.Add((ret, passing));

                newPart.minimums[attr] = num;
            }
            else
            {
                Part passing = new(newPart);
                passing.minimums[attr] = num;
                if(passing.isValid())
                    proccesedParts.Add((ret, passing));

                newPart.maximums[attr] = num;
            }
            if(!newPart.isValid())
            {
                return proccesedParts;
            }
        }
        proccesedParts.Add((def, newPart));
        return proccesedParts;
    }
}