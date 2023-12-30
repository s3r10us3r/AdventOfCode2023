void ConnectBricks(Brick above, Brick below)
{
    below.ConnectAbove(above);
    above.ConnectBelow(below);
}


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
            if(allBricks[i].start.z >= brick.start.z)
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

//this is sorted ascendingly by finish z
List<Brick> fallenBricks = [];


void PutIntofallenBricks(Brick brick)
{
    if(fallenBricks.Count == 0 || fallenBricks.Last().finish.z <= brick.finish.z)
    {
        fallenBricks.Add(brick);
        return;
    }


    for(int i = fallenBricks.Count - 1; i >= 1; i--)
    {
        if(fallenBricks[i].finish.z > brick.finish.z && fallenBricks[i-1].finish.z <= brick.finish.z)
        {
            fallenBricks.Insert(i, brick);
            return;
        }
    }
    
    fallenBricks.Insert(0, brick);
}

//we find where the brick falls and THEN we insert it into the fallen bricks array by its finish z
foreach(var fallingBrick in allBricks)
{
    int i = fallenBricks.Count - 1;
    while(fallingBrick.start.z > 1)
    {
        bool hasStopped = false;
        while(i >= 0 && fallenBricks[i].finish.z + 1 >= fallingBrick.start.z)
        {
            if(fallenBricks[i].finish.z + 1 > fallingBrick.start.z)
            {
                i--;
                continue;
            }
            if(fallingBrick.CheckIfIntersect(fallenBricks[i]))
            {
                ConnectBricks(fallingBrick, fallenBricks[i]);
                hasStopped = true;
            }
            i--;
        }
        if(hasStopped)
        {
            break;
        }
        fallingBrick.Fall();
    }

    PutIntofallenBricks(fallingBrick);
}

int result = 0;
foreach(var brick in fallenBricks)
{
    result++;
    foreach(var aboveBrick in brick.above)
    {
        if(aboveBrick.below.Count == 1)
        {
            result--;
            break;
        }
    }
}

Console.WriteLine(result);