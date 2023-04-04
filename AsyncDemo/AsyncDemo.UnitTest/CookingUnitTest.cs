namespace AsyncDemo.UnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(2,3,2,7)]
        public void test_sync_cooking_process(int fryEggSecond, int fryBaconSecond, int toastSecond, int expected)
        {
            var cooking = new Cooking();

            DateTime startTime = DateTime.Now;

            //start cooking
            Egg eggs = cooking.FryEggs(fryEggSecond);
            Console.WriteLine("eggs are ready");

            Bacon bacon = cooking.FryBacon(fryBaconSecond);
            Console.WriteLine("bacon is ready");

            Toast toast = cooking.ToastBread(toastSecond);
            Console.WriteLine("toast is ready");

            Console.WriteLine("Breakfast is ready!");
            //end of cooking

            DateTime endTime = DateTime.Now;
            int timespan = (int)(endTime - startTime).TotalSeconds;
            Assert.That(timespan, Is.EqualTo(expected));
        }

        [TestCase(2,3,2,7)]
        public async Task test_async_cooking_process_sequencial(int fryEggSecond, int fryBaconSecond, int toastSecond, int expected)
        {
            var asyncCooking = new Cooking();

            // start cooking 
            var startTime = DateTime.Now;
            var eggs = await asyncCooking.FryEggsAsync(fryEggSecond);
            Console.WriteLine("eggs are ready");

            var bacon = await asyncCooking.FryBaconAsync(fryBaconSecond);
            Console.WriteLine("bacon are ready");

            var toast = await asyncCooking.ToastBreadAsync(toastSecond);
            Console.WriteLine("toasts are ready");

            Console.WriteLine("Breakfast is ready!");
             
            var endTime = DateTime.Now;
            // end of cooking

            var timespan = (int)(endTime - startTime).TotalSeconds;
            Assert.That(timespan, Is.EqualTo(expected));
        }

        [TestCase(2,3,2,3)]
        public async Task test_async_cooking_process_parallel(int fryEggSecond, int fryBaconSecond, int toastSecond, int expected)
        {
            var asyncCooking = new Cooking();

            // start cooking
            var startTime = DateTime.Now;
            Task<Egg> eggsTask = asyncCooking.FryEggsAsync(fryEggSecond);
            Task<Bacon> baconTask = asyncCooking.FryBaconAsync(fryBaconSecond);
            Task<Toast> toastTask = asyncCooking.ToastBreadAsync(toastSecond);

            var eggs = await eggsTask;
            Console.WriteLine("eggs are ready");

            var bacon = await baconTask;
            Console.WriteLine("bacon are ready");

            var toast = await toastTask;
            Console.WriteLine("toasts are ready");

            Console.WriteLine("Breakfast is ready!");

            var endTime = DateTime.Now;
            // end of cooking

            var timespan = (int)(endTime - startTime).TotalSeconds;
            Assert.That(timespan, Is.EqualTo(expected));

        }

        [TestCase(2,3,2,3)]
        public async Task test_async_cooking_process_await_all(int fryEggSecond, int fryBaconSecond, int toastSecond, int expected)
        {
            var asyncCooking = new Cooking();

            // start cooking
            var startTime = DateTime.Now;

            Task<Egg> eggsTask = asyncCooking.FryEggsAsync(fryEggSecond);
            Task<Bacon> baconTask = asyncCooking.FryBaconAsync(fryBaconSecond);
            Task<Toast> toastTask = asyncCooking.ToastBreadAsync(toastSecond);

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

            var endTime = DateTime.Now;
            // end of cooking

            var timespan = (int)(endTime - startTime).TotalSeconds;
            Assert.That(timespan, Is.EqualTo(expected));
        }

    }
}