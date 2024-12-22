using AoCHelper;

namespace AdventOfCode2024.Solutions;

public class Day14 : BaseDay
{
    private readonly string _input;
    private readonly int _dimensionX = 101;
    private readonly int _dimensionY = 103;

    public Day14()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var robots = new List<Robot>();

        foreach (var line in _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            var robotInitialiser = line.Split(' ');
            var position = robotInitialiser[0].Replace("p=", string.Empty).Split(',').Select(int.Parse).ToArray();
            var velocity = robotInitialiser[1].Replace("v=", string.Empty).Split(',').Select(int.Parse).ToArray();

            robots.Add(new Robot(position[0], position[1]).Move(velocity[0], velocity[1], _dimensionX, _dimensionY, 100));
        }

        var quadrants = new int[4];
        foreach (var robot in robots)
        {
            if (robot.X < _dimensionX / 2 && robot.Y < _dimensionY / 2)
            {
                quadrants[0]++;
            }
            else if (robot.X > _dimensionX / 2 && robot.Y < _dimensionY / 2)
            {
                quadrants[1]++;
            }
            else if (robot.X < _dimensionX / 2 && robot.Y > _dimensionY / 2)
            {
                quadrants[2]++;
            }
            else if (robot.X > _dimensionX / 2 && robot.Y > _dimensionY / 2)
            {
                quadrants[3]++;
            }
        }    

        var answer = quadrants.Aggregate((a, b) => a * b);

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 = '{answer}'");
    }

    public override ValueTask<string> Solve_2()
    {
        var answer = string.Empty;

        // SOLVE.

        answer = "TODO";

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 = '{answer}'");
    }

    private readonly record struct Robot(int X, int Y)
    {
        public Robot Move(int velocityX, int velocityY, int dimensionX, int dimensionY, int steps)
        {
            var positionX = X + (velocityX * steps) % dimensionX;
            var positionY = Y + (velocityY * steps) % dimensionY;

            if (positionX < 0)
            {
                positionX += dimensionX;
            }
            if (positionX >= dimensionX)
            {
                positionX -= dimensionX;
            }

            if (positionY < 0)
            {
                positionY += dimensionY;
            }
            if (positionY >= dimensionY)
            {
                positionY -= dimensionY;
            }

            return new Robot(positionX, positionY);
        }
    }
}
