namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var d = float.TryParse("10,", out var a);
        }
    }
    public interface I
    {
        public bool B { get; set; }
        public void Foo()
        {
            B = true;
        }
    }

    public class C : I
    {
        public bool B { get; set; }
        public void Foo()
        {
            
        }
    }
}