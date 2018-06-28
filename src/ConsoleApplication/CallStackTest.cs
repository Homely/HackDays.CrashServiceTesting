using System;

namespace ConsoleApplication
{
    public static class CallStackTest
    {
        public static void CallSomething()
        {
            throw new Exception("Testing call stack exception!");
        }
    }
}
