using System;

class Program
{
    static void Main(string[] args)
    {

        List<int> numbers = new List<int>();
        Console.WriteLine("Enter a list of numbers, type 0 when you finished.");

        int number = -1;
        while (number != 0)
        {
            Console.Write("Enter a list of number : ");

            string response = Console.ReadLine();
            number = int.Parse(response);


            if (number != 0)
            {
                numbers.Add(number);
            }
        }


        int sum = 0;
        foreach (int i in numbers)
        {
            sum += i;
        }

        Console.WriteLine($"The sum is: {sum}");


        float average = ((float)sum) / numbers.Count;
        Console.WriteLine($"The average is: {average}");



        int max = numbers[0];

        foreach (int i in numbers)
        {
            if (i > max)
            {

                max = i;
            }
        }

        Console.WriteLine($"The max is: {max}");

        int smallest = numbers[0];

        foreach (int i in numbers)
        {
            if (i < smallest)
            {
                smallest = i;
            }
        }

        Console.WriteLine($"The smallest is: {smallest}");

        int stoted = numbers[0];

        for (int i = 1; i < numbers.Count; i++)
        {
            if (numbers[i] > stoted)
            {
                stoted = numbers[i];
            }
        }
        Console.WriteLine($"The stoted list is: {stoted}"); 
        Console.ReadLine();

    }

}


