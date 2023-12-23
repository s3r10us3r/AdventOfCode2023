public abstract class Module
{
    public abstract List<(string sender, string receiver, int pulse)> Process(int pulse, string sender);
}

public class Flip_flop : Module
{
    bool isOn = false;
    List<string> receivers;
    string name;
    public Flip_flop(List<string> receivers, string name)
    {
        this.receivers = receivers;
        this.name = name;
    }

    public override List<(string sender, string receiver, int pulse)> Process(int pulse, string sender)
    {
        if(pulse == 1)
            return null;
        
        List<(string sender, string receiver, int pulse)> sentPulses = [];
        isOn = !isOn;
        int newPulse = isOn ? 1 : 0;
        foreach(string receiver in receivers)
        {
            sentPulses.Add((name, receiver, newPulse));
        }
        return sentPulses;
    }
}

public class Conjunction : Module
{
    Dictionary<string, int> memory;
    List<string> receivers;
    string name;

    public Conjunction(List<string> senders, List<string> receivers, string name)
    {
        this.receivers = receivers;
        this.name = name;
        memory = [];
        foreach(string sender in senders)
        {
            memory.Add(sender, 0);
        }
    }

    public override List<(string sender, string receiver, int pulse)> Process(int pulse, string sender)
    {
        memory[sender] = pulse;
        int sum = memory.Values.Sum();
        int newPulse = 1;
        if(sum == memory.Count)
        {
            newPulse = 0;
        }

        List<(string sender, string receiver, int pulse)> sentPulses = [];
        foreach(string receiver in receivers)
        {
            sentPulses.Add((name, receiver, newPulse));
        }

        return sentPulses;
    }
}