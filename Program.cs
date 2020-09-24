namespace P2
{
    class Program
    {
        static void Main(string[] args)
        {
            string salt = args[0];
            CollisionLooper.Run(salt, 1000000);
        }
    }
}