List<string> input = [];

//this was done partialy by hand you have to find all of the Conjunctions that lead to one cnojunction that lead to rx and find after how many button presses
//each of them pops out a high pulse, then find LCM of these numbers

using(StreamReader sr = new("input.txt"))
{
    string line;
    while((line = sr.ReadLine()) != null)
    {
        input.Add(line);
    }
}

Dictionary<string, List<string>> senderDict = [];
foreach(string line in input)
{
    string[] lineSplit = line.Split(" -> ");
    string sender = lineSplit[0];
    if(sender[0] != 'b')
    {
        sender = sender[1..];
    }
    string[] receivers = lineSplit[1].Split(", ");
    foreach(string receiver in receivers)
    {
        if(senderDict.ContainsKey(receiver))
        {
            senderDict[receiver].Add(sender);
        }
        else
        {
            senderDict[receiver] = [sender];
        }
    }
}

List<string> broadcasterOutputs = [];

Dictionary<string, Module> modules = [];

foreach(string line in input)
{
    string[] lineSplit = line.Split(" -> ");
    string sender = lineSplit[0];
    string[] receivers = lineSplit[1].Split(", ");
    if(sender == "broadcaster")
    {
        broadcasterOutputs = new List<string>(receivers);
    }
    else
    {
        char type = sender[0];
        sender = sender[1..];
        Module mod;
        if(type == '%')
        {
            mod = new Flip_flop(new(receivers), sender);
        }
        else
        {
            mod = new Conjunction(senderDict[sender], new(receivers), sender);
        }
        modules.Add(sender, mod);
    }
}

Queue<(string sender, string receiver, int pulse)> pulses = [];

int buttonPressesLeft = 10000;
int lowPulses = 0;
int highPulses = 0;
int pressCount = 0;
while(buttonPressesLeft > 0)
{
    //Console.WriteLine();
    buttonPressesLeft--;
    pressCount++;
    lowPulses++;
    foreach(string receiver in broadcasterOutputs)
    {
        pulses.Enqueue(("broadcaster", receiver, 0));
    }

    while(pulses.Count > 0)
    {
        (string sender, string receiver, int pulse) = pulses.Dequeue();
        if(pulse == 0)
        {
            lowPulses++;
        }
        else
        {
            highPulses++;
        }

        if(receiver == "jm" && pulse == 1)
        {
            Console.WriteLine($"{sender} {pulse} {pressCount}");
        }

        if(modules.ContainsKey(receiver))
        {
            var newPulses = modules[receiver].Process(pulse, sender);
            if(newPulses != null)
            {
                foreach(var newPulse in newPulses)
                {
                    pulses.Enqueue(newPulse);
                }
            }
        }
    }
}

