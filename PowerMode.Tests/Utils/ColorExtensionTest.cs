namespace BigEgg.Tools.PowerMode.Tests.Utils
{
    using System.Drawing;
    using System.Linq;
    using System.Reflection;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.PowerMode.Utils;

    public class ColorExtensionTest
    {
        [TestClass]
        public class HSVConvert
        {
            [TestMethod]
            public void ConvertTest()
            {
                var colorProperties = typeof(Color).GetProperties(BindingFlags.Static | BindingFlags.Public);
                var colors = colorProperties.ToDictionary(p => p.Name, p => (Color)p.GetValue(null, null));

                foreach (var color in colors)
                {
                    color.Value.ColorToHSV(out double hue, out double saturation, out double value);
                    var newColor = ColorExtensions.ColorFromHSV(hue, saturation, value);

                    Assert.AreEqual(color.Value.R, newColor.R);
                    Assert.AreEqual(color.Value.G, newColor.G);
                    Assert.AreEqual(color.Value.B, newColor.B);
                }
            }
        }
    }
}
