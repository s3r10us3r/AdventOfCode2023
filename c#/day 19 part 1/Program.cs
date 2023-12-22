Dictionary<string, Func<(int x, int m, int a, int s), string>> conditions = [];
List<(int x, int m, int a, int s)> accepted = [];
List<(int x, int m, int a, int s)> parts = [];
using (StreamReader sr = new("input.txt"))
{
    string line;
    while( (line = sr.ReadLine()) != "")
    {
        string[] inputs = line.Split("{");
        string name = inputs[0];
        string conditionsString = inputs[1].Trim('}');
        string[] conditionsStrings = conditionsString.Split(",");
        List<Func<(int x, int m, int a, int s), bool>> littleConditions = [];
        List<string> returns = [];
        for(int i = 0; i < conditionsStrings.Length - 1; i++)
        {
            string[] conditionAndreturn = conditionsStrings[i].Split(":");
            string lilConditionString = conditionAndreturn[0];
            returns.Add(conditionAndreturn[1]);
            if(lilConditionString[1] == '>')
            {
                string[] lilConditionStringSplit = lilConditionString.Split(">");
                string arg = lilConditionStringSplit[0];
                string num = lilConditionStringSplit[1];

                Func<(int x, int m, int a, int s), bool> lilCondition = null;
                switch(arg)
                {
                    case "x":
                        lilCondition = ((int x, int m, int a, int s) part ) => part.x > int.Parse(num);
                        break;
                    case "m":
                        lilCondition = ((int x, int m, int a, int s) part ) => part.m > int.Parse(num);
                        break;
                    case "a":
                        lilCondition = ((int x, int m, int a, int s) part ) => part.a > int.Parse(num);
                        break;
                    case "s":
                        lilCondition = ((int x, int m, int a, int s) part ) => part.s > int.Parse(num);
                        break;
                }
                littleConditions.Add(lilCondition);
            }

            else if(lilConditionString[1] == '<')
            {
                string[] lilConditionStringSplit = lilConditionString.Split("<");
                string arg = lilConditionStringSplit[0];
            
                string num = lilConditionStringSplit[1];
                Func<(int x, int m, int a, int s), bool> lilCondition = null;
                switch(arg)
                {
                    case "x":
                        lilCondition = ((int x, int m, int a, int s) part ) => part.x < int.Parse(num);
                        break;
                    case "m":
                        lilCondition = ((int x, int m, int a, int s) part ) => part.m < int.Parse(num);
                        break;
                    case "a":
                        lilCondition = ((int x, int m, int a, int s) part ) => part.a < int.Parse(num);
                        break;
                    case "s":
                        lilCondition = ((int x, int m, int a, int s) part ) => part.s < int.Parse(num);
                        break;
                }
                littleConditions.Add(lilCondition);
            }
        }

        string condition((int x, int m, int a, int s) part)
        {
            for (int i = 0; i < littleConditions.Count; i++)
            {
                if (littleConditions[i](part))
                {
                    return returns[i];
                }
            }

            return conditionsStrings[^1];
        }

        conditions.Add(name, condition);
    }

    while( (line = sr.ReadLine()) != null)
    {
        line = line.Trim('{');
        line = line.Trim('}');
        string[] partString = line.Split(",");
        int x,m,a,s;
    
        string[] xString = partString[0].Split("=");
        x = int.Parse(xString[1]);

        string[] mString = partString[1].Split("=");
        m = int.Parse(mString[1]);

        string[] aString = partString[2].Split("=");
        a = int.Parse(aString[1]);

        string[] sString = partString[3].Split("=");
        s = int.Parse(sString[1]);

        parts.Add((x,m,a,s));
    }

    foreach(var part in parts)
    {
        string condName = "in";
        while(condName != "R" && condName != "A")
        {
            condName = conditions[condName](part);
        }
        if(condName == "A")
        {
            accepted.Add(part);
        }
    }

    int result = 0;
    foreach(var part in accepted)
    {
        result += part.x + part.m + part.a + part.s;
    }

    Console.WriteLine(result);
}