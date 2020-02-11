namespace ADONetTest
{
    using System;

    public class SomeClass : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("Dispose!");
        }
    }
}
