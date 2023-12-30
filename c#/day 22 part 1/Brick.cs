class Brick
{
    static int count = 0;
    public readonly int id;
    public (int x, int y, int z) start;
    public (int x, int y, int z) finish;

    public List<Brick> below = [];
    public List<Brick> above = [];

    public Brick(string line)
    {
        string[] splits = line.Split("~");
        string[] startCords = splits[0].Split(",");
        string[] finishCords = splits[1].Split(",");

        start = (int.Parse(startCords[0]), int.Parse(startCords[1]), int.Parse(startCords[2]));
        finish = (int.Parse(finishCords[0]), int.Parse(finishCords[1]), int.Parse(finishCords[2]));
        id = count;
        count++;
    }

    public bool CheckIfIntersect(Brick brick)
    {
        bool intersectInX = (start.x >= brick.start.x && start.x <= brick.finish.x) || (finish.x >= brick.start.x && finish.x <= brick.finish.x) || (brick.start.x >= start.x && brick.start.x <= finish.x) || (brick.finish.x >= start.x && brick.finish.x <= finish.x);
        bool intersectInY = (start.y >= brick.start.y && start.y <= brick.finish.y) || (finish.y >= brick.start.y && finish.y <= brick.finish.y) || (brick.start.y >= start.y && brick.start.y <= finish.y) || (brick.finish.y >= start.y && brick.finish.y <= finish.y);
        return intersectInX && intersectInY;
    }

    public void ConnectAbove(Brick brick)
    {
        above.Add(brick);
    }

    public void ConnectBelow(Brick brick)
    {
        below.Add(brick);
    }

    public void Fall()
    {
        start = (start.x, start.y, start.z - 1);
        finish = (finish.x, finish.y, finish.z - 1);
    }

    public override string ToString()
    {
        return $"{start.x},{start.y},{start.z}~{finish.x},{finish.y},{finish.z}";
    }
}