static (int, char) hexCodeTodistAndDir(string hexCode)
{
    int dist = 0;
    int power = 4;
    for(int i = 2; i < 7; i++)
    {
        int digit = 0;
        if(hexCode[i] >= '0' && hexCode[i] <= '9')
        {
            digit = hexCode[i] - '0';
        }
        else
        {
            digit = hexCode[i] -'a' + 10;
        }
        dist += digit * (int)Math.Pow(16, power--);
    }
    char dir = '0';

    switch(hexCode[7])
    {
        case '0':
            dir = 'R';
            break;
        case '1':
            dir = 'D';
            break;
        case '2':
            dir = 'L';
            break;
        case '3':
            dir = 'U';
            break;
    }
    return(dist, dir);
}

List<(char direction, int dist)> directions = [];

using( StreamReader sr = new("input.txt"))
{
    string line;
    while((line = sr.ReadLine()) != null)
    {
        var split = line.Split(' ');
        (int dist, char direction) = hexCodeTodistAndDir(split[2]);
        directions.Add((direction, dist));
    }
}

List<(double x, double y)> points = [(0,0)];

(double x, double y) lastPoint = (0,0);

foreach((char direction, int dist) in directions)
{
    (double x, double y) = lastPoint;
    
    int rdist = dist;
    switch(direction)
    {
        case 'R':
            lastPoint = (x + rdist, y);
            points.Add(lastPoint);
            break;
        case 'L':
            lastPoint = (x - rdist, y);
            points.Add(lastPoint);
            break;
        case 'U':
            lastPoint = (x, y + rdist);
            points.Add(lastPoint);
            break;
        case 'D':
            lastPoint = (x, y - rdist);
            points.Add(lastPoint);
            break;
    }
}

double area = 0;
double missedArea = 0;
for(int i = 0; i < points.Count - 1; i++)
{
    (double x1, double y1) = points[i];
    (double x2, double y2) = points[i + 1];
    area += x1 * y2 - y1 * x2;
    missedArea += (Math.Abs(x1 - x2) + Math.Abs(y1 - y2) - 1) / 2 ;
}

area = Math.Abs(area)/2 + missedArea + (points.Count + 1) / 2;

Console.WriteLine(area);