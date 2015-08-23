namespace SelfHosting
{
    using System;
    using Nancy.Hosting.Self;

    class Program
    {
        static void Main()
        {
            var uri =
                new Uri("http://localhost:63579");

            using (var host = new NancyHost(uri))
            {
                host.Start();

                Console.WriteLine("Your application is running on " + uri);
                Console.WriteLine("Press any [Enter] to close the host.");
                Console.ReadLine();
            }
        }
    }
}
