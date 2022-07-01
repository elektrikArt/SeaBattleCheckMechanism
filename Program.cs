using Ship = System.Collections.Generic.List<Point>;


int kitsCount = int.Parse(Console.ReadLine() ?? "_");
bool _turnIsMade = false;
string _currDirection = "_";

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
    var shipLengths = new List<int>();
    try
    {
        FindShips();
        CheckShips();
        Console.WriteLine("YES");

        var orderedShipLengths = shipLengths.OrderBy(l => l);
        Console.Write($"{orderedShipLengths.First()}");
        for (int i = 1; i < orderedShipLengths.Count(); i++)
            Console.Write($" {orderedShipLengths.ElementAt(i)}");
        Console.WriteLine();
    }
    catch //(Exception ex)
    {
        //Console.Write("cause: {0}: ", ex.Message);
        Console.WriteLine("NO");
    }



    void FindShips()
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
                    _turnIsMade = false;
                    _currDirection = "_";
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

        seekP = new Point { Y = endP.Y, X = endP.X - 1 };  // left
        if (IsDeck(seekP) && ship.Contains(seekP) == false)
        {
            if (_currDirection != "left" || GetDistance(seekP, ship.Last()) > 1)
            {
                if (_turnIsMade)
                    throw new Exception();
                if (_currDirection != "_")
                    _turnIsMade = true;
                _currDirection = "left";
            }
            ship.Add(seekP);
            DiscoverShip(ship);
        }
        seekP = new Point { Y = endP.Y - 1, X = endP.X };  // up
        if (IsDeck(seekP) && ship.Contains(seekP) == false)
        {
            if (_currDirection != "up" || GetDistance(seekP, ship.Last()) > 1)
            {
                if (_turnIsMade)
                    throw new Exception();
                if (_currDirection != "_")
                    _turnIsMade = true;
                _currDirection = "up";
            }
            ship.Add(seekP);
            DiscoverShip(ship);
        }
        seekP = new Point { Y = endP.Y, X = endP.X + 1 };  // right
        if (IsDeck(seekP) && ship.Contains(seekP) == false)
        {
            if (_currDirection != "right" || GetDistance(seekP, ship.Last()) > 1)
            {
                if (_turnIsMade)
                    throw new Exception();
                if (_currDirection != "_")
                    _turnIsMade = true;
                _currDirection = "right";
            }
            ship.Add(seekP);
            DiscoverShip(ship);
        }
        seekP = new Point { Y = endP.Y + 1, X = endP.X };  // down
        if (IsDeck(seekP) && ship.Contains(seekP) == false)
        {
            if (_currDirection != "down" || GetDistance(seekP, ship.Last()) > 1)
            {
                if (_turnIsMade)
                    throw new Exception();
                if (_currDirection != "_")
                    _turnIsMade = true;
                _currDirection = "down";
            }
            ship.Add(seekP);
            DiscoverShip(ship);
        }

        //_turnIsMade = true;
        //_currDirection = "^";
    }
    void CheckShips()
    {
        foreach (var ship in ships)
        {
            foreach (var p in ship)
            {
                Point seekP;
                seekP = new Point { Y = p.Y - 1, X = p.X - 1 };
                if (IsDeck(seekP) && ship.Contains(seekP) == false)
                    throw new Exception($"CheckShips:coorX,Y:{seekP.Y},{seekP.X}");
                seekP = new Point { Y = p.Y - 1, X = p.X + 1 };
                if (IsDeck(seekP) && ship.Contains(seekP) == false)
                    throw new Exception($"CheckShips:coorX,Y:{seekP.Y},{seekP.X}");
                seekP = new Point { Y = p.Y + 1, X = p.X + 1 };
                if (IsDeck(seekP) && ship.Contains(seekP) == false)
                    throw new Exception($"CheckShips:coorX,Y:{seekP.Y},{seekP.X}");
                seekP = new Point { Y = p.Y + 1, X = p.X - 1 };
                if (IsDeck(seekP) && ship.Contains(seekP) == false)
                    throw new Exception($"CheckShips:coorX,Y:{seekP.Y},{seekP.X}");
            }

            var endP = ship.Last();
            var cuttedShip = ship.Where(p => p.Y != endP.Y).Where(p => p.X != endP.X);
            int deckStraightLen = ship.Count() - cuttedShip.Count();
            if (deckStraightLen != cuttedShip.Count() + 1)
                throw new Exception($"CheckShips:deckXLen,deckYLen:{deckStraightLen}");
            shipLengths.Add(deckStraightLen * 2 - 1);

            //Console.Write(".");
        }
    }
    int GetDistance(Point p1, Point p2)
    {
        return Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);
    }
}



struct Point
{
    public int X { get; set; }
    public int Y { get; set; }
}