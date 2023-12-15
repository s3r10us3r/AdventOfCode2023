public class Lens
{
    public int length;
    public string label;

    public override bool Equals(object? obj)
    {
        return label == ((Lens)obj).label;
    }

    public Lens(int length, string label)
    {
        this.length = length;
        this.label = label;
    }
}

public class Box()
{
    public LinkedList<Lens> contents = new();
}