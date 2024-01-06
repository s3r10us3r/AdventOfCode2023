using System.Numerics;
using System.Security.Cryptography;

class Hail
{
    public Vector p;
    public Vector v;

    public Hail(string hailString)
    {
        string[] hailArr = hailString.Split(" @ ");
        string[] start = hailArr[0].Split(", ");
        string[] vels = hailArr[1].Split(", ");

        double x = double.Parse(start[0]);
        double y = double.Parse(start[1]);
        double z = double.Parse(start[2]);

        p = new Vector(x, y, z);

        double vx = double.Parse(vels[0]);
        double vy = double.Parse(vels[1]);
        double vz = double.Parse(vels[2]);

        v = new Vector(vx, vy, vz);
    }
}

class Vector(double x, double y, double z)
{
    public readonly double x = x;
    public readonly double y = y;
    public readonly double z = z;

    public static double DotProduct(Vector v1, Vector v2)
    {
        return (v1.x * v2.x) + (v1.y * v2.y) + (v1.z * v2.z);
    }

    public static Vector CrossProduct(Vector v1, Vector v2)
    {
        double i = v1.y * v2.z - v1.z * v2.y;
        double j = v1.z * v2.x - v1.x * v2.z;
        double k = v1.x * v2.y - v1.y * v2.x;

        return new Vector(i,j,k);
    }

    public static Vector operator -(Vector a, Vector b)
    {
        return new Vector(a.x - b.x, a.y - b.y, a.z - b.z);
    }
}

class Matrix
{
    int rows;
    int cols;
    double[,] nums;
    public Matrix Augment {get; set;}

    public Matrix(double[,] nums)
    {
        this.nums = nums;
        this.rows = nums.GetLength(0);
        this.cols = nums.GetLength(1);
    }
    public Matrix(Vector v1, Vector v2, Vector v3)
    {
        double[,] newNums = 
        {
            {v1.x, v1.y, v1.z},
            {v2.x, v2.y, v2.z},
            {v3.x, v3.y, v3.z},
        };

        nums = newNums;
        rows = 3;
        cols = 3;
    }

    public void Gauss()
    {
        for(int i = 0; i < rows - 1; i++)
        {
            if(nums[i,i] == 0)
            {
                for(int j = i + 1; j < rows; j++)
                {
                    if(nums[j,i] != 0)
                    {
                        SwapRows(i,j);
                        break;
                    }                  
                }
            }
        }
        //triangulating
        for(int i = 0; i < rows - 1; i++)
        {
            for(int j = i + 1; j < rows; j++)
            {
                if(nums[j,i] == 0)
                {
                    continue;
                }

                double t = nums[j,i] / nums[i,i];
                SubstractRows(j,i,t);
                Augment.SubstractRows(j,i,t);
                nums[j,i] = 0;
            }
        }


        //reducing every number other than those from the diagonall
        for(int i = rows - 1; i >= 0; i--)
        {
            Augment.MultiplyRow(i, 1/nums[i,i]);
            MultiplyRow(i, 1/nums[i,i]);
            nums[i,i] = 1;

            for(int j = i - 1; j >= 0; j--)
            {
                Augment.SubstractRows(j,i,nums[j,i]);
                SubstractRows(j,i,nums[j,i]);
            }
            
        }
    }

    public double ComputeDet()
    {
        double det = 1;
        for(int i = 0; i < rows; i++)
        {
            det *= nums[i,i];
        }
        return det;
    }

    //we substract src row by t and substract it from dest row
    private void SubstractRows(int dest, int src, double t)
    {
        for(int i = 0; i < cols; i++)
        {
            nums[dest, i] -= nums[src, i] * t;
        }
    }

    private void MultiplyRow(int dest, double t)
    {
        for(int i = 0; i < cols; i++)
        {
            nums[dest, i] *= t;
        }
    }

    private void SwapRows(int row1, int row2)
    {
        for(int col = 0; col < cols; col++)
        {
            (nums[row1, col], nums[row2, col]) = (nums[row2, col], nums[row1, col]);
        }
    }

    public override string ToString()
    {
        string res = "";
        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < cols; j++)
            {
                res += nums[i,j];
                if(j < cols - 1)
                    res += ", ";
            }
            res += "\n";
        }
        return res;
    }

    //this only works given that the matrix is 3x3
    public Vector TimesVector(Vector v)
    {
        double x = nums[0,0] * v.x + nums[0,1] * v.y + nums[0,2] * v.z;
        double y = nums[1,0] * v.x + nums[1,1] * v.y + nums[1,2] * v.z;
        double z = nums[2,0] * v.x + nums[2,1] * v.y + nums[2,2] * v.z;

        return new Vector(x,y,z);
    }
}