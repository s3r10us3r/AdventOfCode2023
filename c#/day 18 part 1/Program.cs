List<(char direction, int dist)> directions = [];

using( StreamReader sr = new("input.txt"))
{
    string line;
    while((line = sr.ReadLine()) != null)
    {
        var split = line.Split(' ');
        char direction = split[0][0];
        int dist = int.Parse(split[1]);
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