public class Condition
{
    List<string> lilConditions;
    //default
    string def;
    public Condition(List<string> lilConditions, string def)
    {
        this.lilConditions = lilConditions;
        this.def = def;
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
            
            ulong num = ulong.Parse(cond[2..]);
            if(cond[1] == '<')
            {
                Part passing = new(newPart);
                passing.maximums[attr] = Math.Min(num - 1, passing.maximums[attr]);

                if(passing.isValid())
                    proccesedParts.Add((ret, passing));

                newPart.minimums[attr] = Math.Max(num, newPart.minimums[attr]);
            }
            else
            {
                Part passing = new(newPart);
                passing.minimums[attr] = Math.Max(num + 1, passing.minimums[attr]);
                if(passing.isValid())
                    proccesedParts.Add((ret, passing));

                newPart.maximums[attr] = Math.Min(num, newPart.maximums[attr]);
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