namespace AdventOfCode.Year2025.Day12.Commons;

internal class PresentBox
{
    public int Id;
    public bool[,] OrginalShape = new bool[3, 3];
    public bool[,] CurrentShape = new bool[3, 3];
    public int Area;

    public PresentBox(int id, bool[,] shape)
    {
        Id = id;
        OrginalShape = shape;
        ResetShape();
        Area = CalculateArea(shape);
    }

    private int CalculateArea(bool[,] shape)
    {
        var area = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (shape[i, j])
                    area++;
            }
        }
        return area;
    }

    public void RotateClockWise()
    {
        var newShape = new bool[3, 3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                newShape[i, j] = CurrentShape[2 - j, i];
            }
        }
        CurrentShape = newShape;

    }

    public void Mirror()
    {
        var newShape = new bool[3, 3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                newShape[i, 2 - j] = CurrentShape[i, j];
            }
        }
        CurrentShape = newShape;
    }

    public void ResetShape()
    {
        CurrentShape = (bool[,])OrginalShape.Clone();
    }

    public bool TryFit(bool[,] area)
    {
        ResetShape();

        if (TryFitInRotations(area))
            return true;

        Mirror();

        if (TryFitInRotations(area))
            return true;

        return false;
    }

    public bool TryFitInRotations(bool[,] area)
    {
        for (int i = 0; i < 4; i++)
        {
            if (TryFitCurrent(area))
                return true;
            RotateClockWise();
        }
        return false;
    }

    public bool TryFitCurrent(bool[,] area)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (CurrentShape[i, j] && area[i, j])
                {
                    return false;
                }
            }
        }
        return true;
    }
}
