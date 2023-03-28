//matrix 3x3
//метод квадратних коренів
//метод Зейделя

internal class Program
{
    //Метод квадратичних коренів
    private static double[,] TransposeMatrix(double[,] matrix)
    {
        int size = matrix.GetLength(0);
        double[,] x = new double[size, size];
        for(int i = 0; i < size; i++) 
        {
            for(int j = 0; j < size; j++) 
            {
                x[i,j] = matrix[j,i];
            }
        }
        return x;
    }
    private static double[,] MultipleMatrix(double[,] a, double[,] b) 
    {
        int size = a.GetLength(0);
        double temp = 0;
        double[,] m = new double[size,size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                temp = 0;
                for (int k = 0; k < size; k++)
                {
                    temp += a[i, k] * b[k, j];
                }
                m[i, j] = temp;
            }
        }
        return m;
    }
    private static bool IsSymmetrical(double[,] matrix)
    {
        int size = matrix.GetLength(0);
        for(int i = 0; i < size; i++) 
        {
            for(int j = 0; j < size; j++) 
            {
                if (matrix[i, j] != matrix[j,i]) return false;
            }
        }
        return true;
    }
    private static double[,] ReadMatrix(int n, int m)
    {
        double[,] matrix = new double[n, m];
        for(int i = 0; i < n; i++)
        {
            string[] numbers = Console.ReadLine().Split(" ");
            for(int j = 0; j < m; j++)
            {
                matrix[i,j] = double.Parse(numbers[j]);
            }
        }
        return matrix;
    }
    private static void PrintMatrix(double[] m, char c)
    {
        for (int i = 0; i < m.Length; i++)
        {
            Console.Write($"{c}{i + 1}={m[i]} ");
        }
        Console.WriteLine();
        
    }
    private static void PrintMatrix(double[,] m)
    {
        for(int i = 0; i < m.GetLength(0); i++)
        {
            for(int j = 0; j < m.GetLength(1); j++)
            {
                Console.Write($"{m[i, j]} ");
            }
            Console.WriteLine();
        }
    }
    private static double Det(double [,] S, double [,] D)
    {
        int size = S.GetLength(0);
        double det = 1;
        for(int i = 0; i < size; i++)
        {
            det *= S[i,i]*S[i,i]*D[i, i];
        }
        return det;
    }
    private static void CalculateSnD(double[,] S, double[,] D, double[,] a)
    {
        int n = a.GetLength(0);
        for(int i = 0; i < n; i++)
        {
            double temp1 = 0;
            double temp2 = 0;
            for (int p = 0; p <= i - 1; p++)
            {
                temp1 += S[p, i] * S[p, i] * D[p, p];
            }
            D[i, i] = Math.Sign(a[i,i] - temp1);
            S[i, i] = Math.Sqrt(Math.Abs(a[i, i] - temp1));
            for(int j = i + 1; j < n; j++)
            {
                double temp3 = 0;
                for(int p = 0; p <= i - 1;p++)
                {
                    temp3 += S[p, i] * D[p, p] * S[p, j];
                }
                S[i, j] = (a[i, j] - temp3) / (D[i, i] * S[i, i]);
            }
        }
    }
    private static double[] CalculateY(double[,] S, double[,] D, double[,] matrix)
    {
        int size = S.GetLength(0);
        double[] y = new double[size];
        double[,] StD = MultipleMatrix(TransposeMatrix(S), D);
        for(int i = 0; i < size; i++)
        {
            double temp = 0;
            for (int k = 0; k < size - 1; k++)
            {
                temp += StD[i,k]*y[k];
            }
            y[i] = (matrix[i,3] - temp) / StD[i, i];
        }
        return y;
    }
    private static double[] CalculateX(double[,] S, double[] y)
    {
        int size = S.GetLength(0);
        double[] x = new double[size];
        for (int i = size - 1 ; i >= 0; i--) 
        {
            double temp = 0;
            for(int k = size - 1; k > i; k--)
            {
                temp += S[i, k] * y[k];
            }
            x[i] = (y[i] - temp) / S[i,i];
        }
        return x;
    }
    // Метод Зейделя
    private static double CalculateOdds(double[] x1, double[] x2)
    {
        int size = x1.GetLength(0);
        double[] odd = new double[size];
        for(int i = 0; i < size; i++)
        {
            odd[i] = Math.Abs(x1[i] - x2[i]);
        }
        return odd.Max();
    }
    private static double[] ZeidelMethod(double[,] m, double[] Xn, double e)
    {
        Console.WriteLine("Перша норма:");
        PrintMatrix(Xn, 'x');
        int size = m.GetLength(0);  
        double[] Xn_1 = new double[size];
        do
        {
            for (int k = 0; k < size; k++)
            {
                Xn_1[k] = Xn[k];
            }
            for (int i = 0; i < size; i++)
            {
                double temp = 0;
                for (int j = 0; j < size; j++)
                {
                    if (i == j) continue;
                    temp += m[i, j] * Xn[j];
                }
                Xn[i] = (m[i, 3] - temp) / m[i, i]; 
            }
            if (CalculateOdds(Xn, Xn_1) < e)
            {
                Console.WriteLine("Остання норма");
                PrintMatrix(Xn_1, 'x');
                return Xn; 
            }
        }
        while (true);
    }
    private static void Main(string[] args)
    {
        Console.WriteLine("Виберіть метод:");
        Console.WriteLine("1 - Метод квадратичних коренів");
        Console.WriteLine("2 - Метод Зейделя");
        Console.Write("Вибір:");
        int choice = int.Parse(Console.ReadLine());
        int n;
        switch (choice) 
        {
            case 1:
                Console.WriteLine("Метод квадратичних коренів");
                Console.Write("Введіть розміри матриці:");
                n = int.Parse(Console.ReadLine());
                double[,] m = ReadMatrix(n, n + 1);
                if (IsSymmetrical(m))
                {
                    double[,] S = new double[n, n];
                    double[,] D = new double[n, n];
                    CalculateSnD(S, D, m);
                    Console.WriteLine("S:");
                    PrintMatrix(S);
                    Console.WriteLine("D:");
                    PrintMatrix(D);
                    Console.WriteLine($"Визначник матриці = {Det(S, D)}");
                    double[] y = CalculateY(S, D, m);
                    PrintMatrix(y, 'y');
                    double[] x = CalculateX(S, y);
                    PrintMatrix(x, 'x');
                }
                else
                {
                    Console.WriteLine("Невірні данні!");
                }
                break;
            case 2:
                Console.WriteLine("Метод Зейделя");
                Console.Write("Введіть розмір матриці:");
                n = int.Parse(Console.ReadLine());
                double[,] matrix = ReadMatrix(n, n + 1);
                if (IsSymmetrical(matrix)) Console.WriteLine("Перша достатня умова збіжності виконується");
                else Console.WriteLine("Перша достатня умова збіжності не виконується");
                double[] startX = new double[n];
                Console.Write("Введіть початкове наближення:");
                string[] start = Console.ReadLine().Split(' ');
                for (int i = 0; i < n; i++)
                {
                    startX[i] = double.Parse(start[i]);
                }
                Console.Write("Введіть точність обчислення:");
                double e = double.Parse(Console.ReadLine());
                double[] result = ZeidelMethod(matrix, startX, e);
                Console.WriteLine("Результат");
                PrintMatrix(result, 'x');
                break;
            default:
                break;
        }
    }
}