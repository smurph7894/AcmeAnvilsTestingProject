namespace AcmeAnvils
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                OrderSystem orderSystem = new OrderSystem();
                orderSystem.CollectOrder();
                Console.ReadKey();
                Environment.Exit(0);
            }
            catch (ExitAppException exception)
            {
                Environment.Exit(1);
            }
        }
    }
}
