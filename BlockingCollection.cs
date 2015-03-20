class MultiThread_Sync
    {
        static void Main(string[] args)
        {

            UseBlockingCollection();
            Console.ReadLine();
        }

        static void UseBlockingCollection()
        {
            var count = 0;
            const int countMax = 10;
            var blockingCollection = new BlockingCollection<string>();

            var producer1 = Task.Factory.StartNew(() =>
            {
                while (count <= countMax)
                {
                    blockingCollection.Add("value" + count);
                    count++;
                }
                blockingCollection.CompleteAdding();
            });

            var producer2 = Task.Factory.StartNew(() =>
            {
                while (count <= countMax)
                {
                    blockingCollection.Add("value" + count);
                    count++;
                }
                blockingCollection.CompleteAdding();
            });

            var consumer1 = Task.Factory.StartNew(() =>
            {
                foreach (var value in blockingCollection.GetConsumingEnumerable())
                {
                    Console.WriteLine("Worker 1: " + value);
                    Thread.Sleep(1000);
                }
            });
            

            var consumer2 = Task.Factory.StartNew(() =>
            {
                foreach (var value in blockingCollection.GetConsumingEnumerable())
                {
                    Console.WriteLine("Worker 1: " + value);
                    Thread.Sleep(1000);
                }
            });

            Task.WaitAll(producer1, consumer1, consumer2);
        }

    }
