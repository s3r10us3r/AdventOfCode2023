List<Brick> allBricks = [];

using(StreamReader sr = new("input.txt"))
{
    string line;
    while((line = sr.ReadLine()) != null)
    {
        Brick brick = new(line);
        bool inserted = false;
        for(int i = 0; i < allBricks.Count; i++)
        {
            if(allBricks[i].start.z > brick.start.z)
            {
                allBricks.Insert(i, brick);
                inserted = true;
                break;
            }
        }

        if(!inserted)
        {
            allBricks.Add(brick);
        }
    }
}

foreach(Brick brick in allBricks)
{
    Console.WriteLine(brick);
}