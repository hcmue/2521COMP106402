namespace BaiTapBuoi03.Models
{
    public class Demo
    {
        public static string FuncA()
        {
            Thread.Sleep(2000);
            return "Hello World";
        }

        public static int FuncB()
        {
            Thread.Sleep(5000);
            return new Random().Next();
        }

        public static void FuncC()
        {
            Thread.Sleep(3000);
        }


        public static async Task<string> FuncAAsync()
        {
            await Task.Delay(2000);
            return "Hello World";
        }

        public static async Task<int> FuncBAsync()
        {
            await Task.Delay(5000);
            return new Random().Next();
        }

        public static async Task FuncCAsync()
        {
            await Task.Delay(3000);
        }
    }
}
