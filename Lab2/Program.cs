using System;

class Program
{
    static double f(double x)
    {
        return x * x * x - 5 * x + 3;
    }

    static double fp(double x) => 3 * x * x - 5; 
    static double f2p(double x) => 6 * x;        
    static double Newton(double a, double b, double eps, int Kmax)
    {
        double x = b;

        if (f(x) * f2p(x) < 0) x = a;
        else if (f(x) * f2p(x) == 0)
        {
            Console.WriteLine("Збіжність методу Ньютона не гарантується");
            return double.NaN;
        }

        for (int i = 1; i <= Kmax; i++)
        {
            double Dx = f(x) / fp(x);
            x = x - Dx;
            Console.WriteLine($"[Ньютон] Ітерація {i}: x = {x}, Dx = {Dx}");
            if (Math.Abs(Dx) < eps) return x;
        }

        Console.WriteLine("За Kmax ітерацій корінь не знайдено.");
        return double.NaN;
    }

    static double Secant(double a, double b, double eps, int Kmax)
    {
        double x0 = a, x1 = b;

        for (int i = 1; i <= Kmax; i++)
        {
            double fx0 = f(x0), fx1 = f(x1);
            if (fx1 == fx0)
            {
                Console.WriteLine("Ділення на нуль у МДН!");
                return double.NaN;
            }

            double x2 = x1 - fx1 * (x1 - x0) / (fx1 - fx0);
            Console.WriteLine($"[Дотичних] Ітерація {i}: x = {x2}");

            if (Math.Abs(x2 - x1) < eps) return x2;

            x0 = x1;
            x1 = x2;
        }

        Console.WriteLine("За Kmax ітерацій корінь не знайдено.");
        return double.NaN;
    }

    static void FindInterval(double start, double end, double step)
    {
        Console.WriteLine("\nТабулювання f(x) для пошуку інтервалів:");
        double prevX = start;
        double prevF = f(prevX);

        for (double x = start + step; x <= end; x += step)
        {
            double fx = f(x);
            Console.WriteLine($"x = {x,6:F2}, f(x) = {fx,10:F4}");

            if (prevF * fx < 0)
                Console.WriteLine($"→ Зміна знаку між [{prevX}, {x}]");

            prevX = x;
            prevF = fx;
        }
    }

    static void Main()
    {
        Console.Write("Введіть a: ");
        double a = double.Parse(Console.ReadLine());

        Console.Write("Введіть b: ");
        double b = double.Parse(Console.ReadLine());

        Console.Write("Введіть точність eps: ");
        double eps = double.Parse(Console.ReadLine());

        Console.Write("Введіть Kmax: ");
        int Kmax = int.Parse(Console.ReadLine());

        Console.WriteLine("\nОберіть метод:");
        Console.WriteLine("1 – Метод Ньютона");
        Console.WriteLine("2 – Метод дотичних (секущих)");
        int choice = int.Parse(Console.ReadLine());

        double root = double.NaN;

        if (choice == 1)
        {
            root = Newton(a, b, eps, Kmax);
        }
        else if (choice == 2)
        {
            Console.WriteLine("\nПошук інтервалів локалізації...");
            FindInterval(a, b, 0.5); 
            root = Secant(a, b, eps, Kmax);
        }

        if (!double.IsNaN(root))
            Console.WriteLine($"\nЗнайдений корінь: x ≈ {root:F6}");
        else
            Console.WriteLine("\nРозв'язок не знайдено.");

        Console.ReadLine();
    }
}
