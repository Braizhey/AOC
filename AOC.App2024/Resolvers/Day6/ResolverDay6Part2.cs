using AOC.Common.Models;
using AOC.Common.Services;

namespace AOC.App2024.Resolvers.Day6
{
    public class ResolverDay6Part2 : IAdventServiceResolver
    {
        public int Resolve(List<string> data)
        {
            var field = new Field(data[0].Length, data.Count);

            var y = 0;
            foreach (var line in data)
            {
                var x = 0;
                var cols = line.ToList();
                foreach (var pos in cols)
                {
                    var position = new Position(x, y);
                    field.AnalysePosition(position, pos);
                    x++;
                }
                y++;
            }
            if (field.Guard == null) throw new InvalidDataException("Guard is not set");

            while (field.IsGuardInside())
            {
                field.MoveGuard(null);
            }
            field.Guard.CheckCurrentPosition();

            var nbLoops = 0;
            var options = field.Guard.UniqueHistory.Where(p => !p.Equals(field.GuardInitPosition)).ToList();
            foreach (var option in options)
            {
                field.ResetGuard();
                field.Obstacles.Add(option);
                while (field.IsGuardInside() && !field.IsGuardLooping())
                {
                    field.MoveGuard(option);
                }
                field.Guard.CheckCurrentPosition();
                if (field.IsGuardLooping())
                {
                    nbLoops++;
                }
                field.Obstacles.Remove(option);
            }
            return nbLoops;
        }

        private class Field(int width, int height)
        {
            private const char _obstacle = '#';
            private const char _guardEast = '>';
            private const char _guardWest = '<';
            private const char _guardNorth = '^';
            private const char _guardSouth = 'v';
            private const char _default = '.';

            public int Width { get; private set; } = width;
            public int Height { get; private set; } = height;

            public List<Position> Obstacles { get; private set; } = [];
            public Guard? Guard {  get; private set; }
            public Position? GuardInitPosition { get; private set; }
            public Direction? GuardInitDirection { get; private set; }

            public void AnalysePosition(Position position, char content)
            {
                if (_obstacle.Equals(content))
                {
                    Obstacles.Add(position);
                }
                else if (!_default.Equals(content))
                {
                    // Guard
                    Direction guardDirection;
                    switch (content)
                    {
                        case _guardNorth:
                            guardDirection = Direction.North;
                            break;
                        case _guardSouth:
                            guardDirection = Direction.South;
                            break;
                        case _guardWest:
                            guardDirection = Direction.West;
                            break;
                        case _guardEast:
                            guardDirection = Direction.East;
                            break;
                        default:
                            throw new ArgumentException("Invalid direction content: " + content);
                    }
                    Guard = new Guard(position, guardDirection);
                    GuardInitPosition = new Position(position);
                    GuardInitDirection = guardDirection;
                }
            }

            public void MoveGuard(Position? specialObstacle)
            {
                if (Guard == null) throw new InvalidDataException("Guard is not set");

                Position nextPosition;
                switch (Guard.Direction)
                {
                    case Direction.North:
                        nextPosition = new Position(Guard.X, Guard.Y - 1);
                        break;
                    case Direction.East:
                        nextPosition = new Position(Guard.X + 1, Guard.Y);
                        break;
                    case Direction.South:
                        nextPosition = new Position(Guard.X, Guard.Y + 1);
                        break;
                    case Direction.West:
                        nextPosition = new Position(Guard.X - 1, Guard.Y);
                        break;
                    default:
                        throw new ArgumentException("Invalid guard direction: " + Guard.Direction);
                }

                if (Obstacles.Exists(o => o.Equals(nextPosition)))
                {
                    Guard.Rotate();
                }
                else
                {
                    Guard.Move(nextPosition);
                }
            }

            public bool IsGuardInside()
            {
                if (Guard == null) throw new InvalidDataException("Guard is not set"); 

                return Guard.X > 0 && Guard.X < Width - 1
                    && Guard.Y > 0 && Guard.Y < Height - 1;
            }

            public bool IsGuardLooping()
            {
                if (Guard == null) throw new InvalidDataException("Guard is not set");

                //return Guard.DejaVuCounter > 1 && Guard.DejaVuCounter == Guard.HistoryCount.Values.Max();

                var currentPosition = new Position(Guard.X, Guard.Y);
                return Guard.HistoryCount.ContainsKey(currentPosition.ToString()) && Guard.HistoryCount[currentPosition.ToString()] > 3;
            }

            public void ResetGuard()
            {
                if (Guard == null || GuardInitPosition == null || !GuardInitDirection.HasValue) throw new InvalidDataException("Guard is not set");

                Guard.History = [];
                Guard.UniqueHistory = [];
                Guard.HistoryCount = [];
                Guard.SetPosition(GuardInitPosition, GuardInitDirection.Value);
            }
        }

        private class Guard(Position initPos, Direction dir) : Position(initPos.X, initPos.Y)
        {
            public Direction Direction { get; private set; } = dir;

            public List<Position> UniqueHistory { get; set; } = [];
            public List<Position> History { get; set; } = [];
            public Dictionary<string, int> HistoryCount { get; set; } = [];
            public int DejaVuCounter = 0;

            public void Rotate()
            {
                switch (Direction)
                {
                    case Direction.North:
                        Direction = Direction.East;
                        break;
                    case Direction.East:
                        Direction = Direction.South;
                        break;
                    case Direction.South:
                        Direction = Direction.West;
                        break;
                    case Direction.West:
                        Direction = Direction.North;
                        break;
                }
            }

            public void CheckCurrentPosition()
            {
                var currentPosition = new Position(X, Y);
                if (HistoryCount.ContainsKey(currentPosition.ToString()))
                {
                    HistoryCount[currentPosition.ToString()]++;
                }
                else
                {
                    HistoryCount.Add(currentPosition.ToString(), 1);
                }
                if (!UniqueHistory.Exists(p => p.Equals(this)))
                {
                    UniqueHistory.Add(currentPosition);
                }
                History.Add(currentPosition);
            }

            public void Move(Position newPos)
            {
                var currentPosition = new Position(X, Y);
                if (HistoryCount.ContainsKey(currentPosition.ToString()))
                {
                    HistoryCount[currentPosition.ToString()]++;
                    DejaVuCounter++;
                }
                else
                {
                    DejaVuCounter = 0;
                    HistoryCount.Add(currentPosition.ToString(), 1);
                }

                // Unique history
                if (!UniqueHistory.Exists(p => p.Equals(this)))
                {
                    UniqueHistory.Add(currentPosition);
                }
                History.Add(currentPosition);

                X = newPos.X;
                Y = newPos.Y;
            }

            public void SetPosition(Position pos, Direction dir)
            {
                X = pos.X; 
                Y = pos.Y;
                Direction = dir;
            }

            public override string ToString()
            {
                return $"<{Direction}> {base.ToString()}";
            }
        }
    }
}
