namespace BigEgg.Tools.PowerMode.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class RandomUtils
    {
        public static Random Random { get; } = new Random(DateTime.Now.Millisecond);


        public static string NextString(IList<string> stringList)
        {
            return stringList[Random.Next(0, stringList.Count)];
        }

        public static int NextSignal()
        {
            return Random.Next(0, 2) == 1 ? 1 : -1;
        }

        public static Color NextColor()
        {
            var bytes = new byte[3];
            Random.NextBytes(bytes);

            return Color.FromArgb(bytes[0], bytes[1], bytes[2]);
        }
    }
}
