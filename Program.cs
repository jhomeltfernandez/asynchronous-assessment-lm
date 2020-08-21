using System;
using System.Collections.Generic;
using System.Threading.Tasks;

/**
 * INSTRUCTIONS:
 *  1. Modify the codes below and make it asynchronous
 *  2. After your modification, explain what makes it asynchronous
**/


namespace asynchronous_assessment_lm
{
    class Program
    {
        static async Task Main(string[] args)
        {

            Coffee cup = await PourCoffeeAsync();
            Console.WriteLine("Coffee is ready");

            var fryEggTask = FryEggsAsync(2);
            var fryBaconTask = FryBaconAsync(3);
            var toastTask = PrepareToastBreadAsync(2);

            List<Task> concurrentTasks = new List<Task> 
            { 
                fryEggTask, 
                fryBaconTask, 
                toastTask 
            };

            foreach (var task in concurrentTasks)
            {
                Task currentTask = await Task.WhenAny(task);
                if(currentTask == fryEggTask) Console.WriteLine("Eggs are ready");
                if (currentTask == fryEggTask) Console.WriteLine("Bacon is ready");
                if (currentTask == fryEggTask) Console.WriteLine("toast is ready");
            }

            Juice orange = await PourOJAsync();
            Console.WriteLine("Orange juice is ready");

            Console.WriteLine("Breakfast is ready!");
        }

        private static async Task<Juice> PourOJAsync()
        {
            await Task.Run(() => Console.WriteLine("Pouring orange juice"));
            return new Juice();
        }

        private static async Task ApplyJamAsync(Toast toast) =>
            await Task.Run(()=> Console.WriteLine("Putting jam on the toast"));

        private static async Task ApplyButterAsync(Toast toast) =>
            await Task.Run(() => Console.WriteLine("Putting butter on the toast"));

        private static async Task<Toast> ToastBreadAsync(int slices)
        {
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster");
            }
            Console.WriteLine("Start toasting...");
            await Task.Delay(3000);
            Console.WriteLine("Remove toast from toaster");

            return new Toast();
        }

        private static async Task<Toast> PrepareToastBreadAsync(int slices)
        {
            Toast toast = await ToastBreadAsync(slices);
            await ApplyButterAsync(toast);
            await ApplyJamAsync(toast);

            return toast;
        }

        private static async Task<Bacon> FryBaconAsync(int slices)
        {
            Console.WriteLine($"putting {slices} slices of bacon in the pan");
            Console.WriteLine("cooking first side of bacon...");
            await Task.Delay(3000);
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("flipping a slice of bacon");
            }
            Console.WriteLine("cooking the second side of bacon...");
            await Task.Delay(3000);


            return new Bacon();
        }

        private static async Task<Egg> FryEggsAsync(int count)
        {
            Console.WriteLine("Warming the egg pan...");
            await Task.Delay(3000);
            Console.WriteLine($"cracking {count} eggs");
            Console.WriteLine("cooking the eggs ...");
            await Task.Delay(3000);
            Console.WriteLine("Put eggs on plate");

            return new Egg();
        }

        private static async Task<Coffee> PourCoffeeAsync()
        {
            await Task.Run(()=> Console.WriteLine("Pouring coffee"));
            return new Coffee();
        }

        private class Coffee
        {
        }

        private class Egg
        {
        }

        private class Bacon
        {
        }

        private class Toast
        {
        }

        private class Juice
        {
        }
    }
}
