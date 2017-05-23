namespace BigEgg.Tools.PowerMode.Utils
{
    using System;
    using System.Collections.Generic;

    public class RandomUtils
    {
        private static Random random = new Random(DateTime.Now.Millisecond);

        public static string NextString(IList<string> stringList)
        {
            return stringList[random.Next(0, stringList.Count)];
        }
    }
}
