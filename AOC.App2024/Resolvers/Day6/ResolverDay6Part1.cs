using AOC.Common.Models;
using AOC.Common.Services;

namespace AOC.App2024.Resolvers.Day6
{
    public class ResolverDay6Part1 : IAdventServiceResolver
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
                field.MoveGuard();
            }
            field.Guard.CheckCurrentPosition();

            return field.Guard.UniqueHistory.Count;
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
                }
            }

            public void MoveGuard()
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
        }

        private class Guard(Position initPos, Direction dir) : Position(initPos.X, initPos.Y)
        {
            public Direction Direction { get; private set; } = dir;

            public List<Position> UniqueHistory { get; set; } = [];

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
                if (!UniqueHistory.Exists(p => p.Equals(this)))
                {
                    UniqueHistory.Add(new Position(X, Y));
                }
            }

            public void Move(Position newPos)
            {
                if (!UniqueHistory.Exists(p => p.Equals(this)))
                {
                    UniqueHistory.Add(new Position(X, Y));
                }

                X = newPos.X;
                Y = newPos.Y;
            }

            public override string ToString()
            {
                return $"<{Direction}> {base.ToString()}";
            }
        }
    }
}
