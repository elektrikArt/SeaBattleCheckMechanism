using Ship = System.Collections.Generic.List<Point>;


int kitsCount = int.Parse(Console.ReadLine() ?? "_");

for (int _ = 0; _ < kitsCount; _++)
{
    string[] dimsInput = (Console.ReadLine() ?? "_") .Split(' ');
    int n = int.Parse(dimsInput[0]);
    int m = int.Parse(dimsInput[1]);

    string[] field = new string[n];
    for (int y = 0; y < n; y++)
    {
        field[y] = Console.ReadLine() ?? "_";
    }

    var ships = new List<Ship>();
    FindShips(ships);
    if (ships.All(s => IsValid(s)))
    {
        Console.WriteLine("YES");
        Console.WriteLine(ships.Count);
    }
    else
    {
        Console.WriteLine("NO");
    }



    void FindShips(List<Ship> ships)
    {
        for (int y = 0; y < n; y++)
        {
            for (int x = 0; x < m; x++)
            {
                var p = new Point { Y = y, X = x };
                if (IsDeck(p) && IsContained(p) == false)
                {
                    //Console.WriteLine("New ship found with p.Y: {0}, p.X: {1}", p.Y, p.X);
                    var ship = new Ship();
                    ship.Add(p);
                    DiscoverShip(ship);
                    ships.Add(ship);
                }
            }
        }
    }
    bool IsContained(Point p)
    {
        return ships.Any(s => s.Contains(p));
    }
    bool IsDeck(Point p)
    {
        if (p.Y >= 0 && p.Y < n && p.X >= 0 && p.X < m)
            return field[p.Y][p.X] == '*';
        return false;
    }
    void DiscoverShip(Ship ship)
    {
        var endP = ship.Last();
        Point seekP;
        //Console.WriteLine("endP.Y: {0}, enpP.X: {1}", endP.Y, endP.X);

        seekP = new Point { Y = endP.Y, X = endP.X - 1 };
        if (IsDeck(seekP) && ship.Contains(seekP) == false)
        {
            ship.Add(seekP);
            DiscoverShip(ship);
        }
        seekP = new Point { Y = endP.Y - 1, X = endP.X };
        if (IsDeck(seekP) && ship.Contains(seekP) == false)
        {
            ship.Add(seekP);
            DiscoverShip(ship);
        }
        seekP = new Point { Y = endP.Y, X = endP.X + 1 };
        if (IsDeck(seekP) && ship.Contains(seekP) == false)
        {
            ship.Add(seekP);
            DiscoverShip(ship);
        }
        seekP = new Point { Y = endP.Y + 1, X = endP.X };
        if (IsDeck(seekP) && ship.Contains(seekP) == false)
        {
            ship.Add(seekP);
            DiscoverShip(ship);
        }
    }
    bool IsValid(Ship ship)
    {
        var firstDicsoveredP = ship.First();
        var sameLinePs = ship.Where(p => p.Y == firstDicsoveredP.Y);  // horizontal line
        var lineStartP = sameLinePs.MinBy(p => p.Y);
        var lineEndP = sameLinePs.MaxBy(p => p.X);
        if (lineEndP.X - lineStartP.X != sameLinePs.Count())
            return false;

        var otherLinePs = ship.Where(p => p.Y != firstDicsoveredP.Y);  // vertical line
        lineStartP = otherLinePs.MinBy(p => p.Y);
        lineEndP = otherLinePs.MaxBy(p => p.Y);
        if (lineEndP.Y - lineStartP.Y != sameLinePs.Count())
            return false;

        return true;
}



struct Point
{
    public int X { get; set; }
    public int Y { get; set; }
}