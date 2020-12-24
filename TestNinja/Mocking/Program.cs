namespace TestNinja.Mocking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var videoservice = new VideoService();
            var title = videoservice.ReadVideoTitle();
        }
    }
}