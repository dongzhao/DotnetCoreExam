using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDemo
{
    public class Cooking
    {
        private readonly int _timeUnit = 1000;

        public Cooking()
        {

        }

        #region Sync call
        
        public void StartCooking()
        {
            Egg eggs = FryEggs(2);
            Console.WriteLine("eggs are ready");

            Bacon bacon = FryBacon(3);
            Console.WriteLine("bacon is ready");

            Toast toast = ToastBread(2);
            Console.WriteLine("toast is ready");

            Console.WriteLine("Breakfast is ready!");
        }

        public Toast ToastBread(int mins)
        {
            DateTime startTime = DateTime.Now;
            Task.Delay(_timeUnit*mins).Wait();
            DateTime endTime = DateTime.Now;
            return new Toast() { timespan = (int)(endTime - startTime).TotalSeconds };
        }

        public Bacon FryBacon(int mins)
        {
            DateTime startTime = DateTime.Now;
            Task.Delay(_timeUnit*mins).Wait();
            DateTime endTime = DateTime.Now;
            return new Bacon() { timespan = (int)(endTime - startTime).TotalSeconds };
        }

        public Egg FryEggs(int mins)
        {
            DateTime startTime = DateTime.Now;
            Task.Delay(_timeUnit * mins).Wait();
            DateTime endTime = DateTime.Now;
            return new Egg() { timespan = (int)(endTime - startTime).TotalSeconds };
        }
        #endregion

        #region Async call
        public async Task StartCookingAsync1()
        {
            var eggs = await FryEggsAsync(2);
            Console.WriteLine("eggs are ready");

            var bacon = await FryBaconAsync(3);
            Console.WriteLine("bacon are ready");

            var toast = await ToastBreadAsync(2);
            Console.WriteLine("toasts are ready");

            Console.WriteLine("Breakfast is ready!");
        }

        public async Task StartCookingAsync2()
        {
            Task<Egg> eggsTask = FryEggsAsync(2);
            Task<Bacon> baconTask = FryBaconAsync(3);
            Task<Toast> toastTask = ToastBreadAsync(2);

            var eggs = await eggsTask;
            Console.WriteLine("eggs are ready");

            var bacon = await baconTask;
            Console.WriteLine("bacon are ready");

            var toast = await toastTask;
            Console.WriteLine("toasts are ready");

            Console.WriteLine("Breakfast is ready!");
        }

        public async Task StartCookingAsync3()
        {
            Task<Egg> eggsTask = FryEggsAsync(2);
            Task<Bacon> baconTask = FryBaconAsync(3);
            Task<Toast> toastTask = ToastBreadAsync(2);

            var cookingTasks = new List<Task> { eggsTask, baconTask, toastTask };
            while (cookingTasks.Count > 0)
            {
                var completed = await Task.WhenAny(cookingTasks);
                if (completed == eggsTask)
                {
                    Console.WriteLine("eggs are ready");
                }
                else if (completed == baconTask)
                {
                    Console.WriteLine("bacon is ready");
                }
                else if (completed == toastTask)
                {
                    Console.WriteLine("toast is ready");
                }
                await completed;
                cookingTasks.Remove(completed);
            }
            Console.WriteLine("Breakfast is ready!");
        }

        public async Task<Toast> ToastBreadAsync(int mins)
        {
            DateTime startTime = DateTime.Now;
            await Task.Delay(_timeUnit * mins);
            DateTime endTime = DateTime.Now;

            return new Toast() { timespan = (int)(endTime - startTime).TotalSeconds };
        }

        public async Task<Bacon> FryBaconAsync(int mins)
        {
            DateTime startTime = DateTime.Now;
            await Task.Delay(_timeUnit * mins);
            DateTime endTime = DateTime.Now;
            return new Bacon() { timespan = (int)(endTime - startTime).TotalSeconds };
        }

        public async Task<Egg> FryEggsAsync(int mins)
        {
            DateTime startTime = DateTime.Now;
            await Task.Delay(_timeUnit * mins);
            DateTime endTime = DateTime.Now;
            return new Egg() { timespan = (int)(endTime - startTime).TotalSeconds };
        }
        #endregion
    }
}
