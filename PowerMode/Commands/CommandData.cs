namespace BigEgg.Tools.PowerMode.Commands
{
    using System;

    public static class CommandData
    {
        public static readonly Guid CommandSet = new Guid("ed6f1616-73f4-4149-8738-7bd5ae7b13a9");
        public static readonly int TogglePowerModeCommandId = 0x1100;
        public static readonly int ToggleComboModeCommandId = 0x1200;
        public static readonly int ToggleParticlesCommandId = 0x1300;
        public static readonly int ToggleScreenShakeCommandId = 0x1400;
        public static readonly int ToggleAudioCommandId = 0x1500;
    }
}
