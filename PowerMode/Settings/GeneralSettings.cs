﻿namespace BigEgg.Tools.PowerMode.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GeneralSettings : Model
    {
        private bool isEnablePowerMode = true;
        private bool isEnableComboMode = true;
        private bool isEnableParticles = true;
        private bool isEnableScreenShake = true;
        private bool isEnableAudio = false;
        private string excludedFileTypesString = ".xml;.json";
        private List<string> excludedFileTypesList = new List<string>() { ".xml", ".json" };


        public bool IsEnablePowerMode
        {
            get { return isEnablePowerMode; }
            set { SetProperty(ref isEnablePowerMode, value); }
        }

        public bool IsEnableComboMode
        {
            get { return isEnableComboMode; }
            set { SetProperty(ref isEnableComboMode, value); }
        }

        public bool IsEnableParticles
        {
            get { return isEnableParticles; }
            set { SetProperty(ref isEnableParticles, value); }
        }

        public bool IsEnableScreenShake
        {
            get { return isEnableScreenShake; }
            set { SetProperty(ref isEnableScreenShake, value); }
        }

        public bool IsEnableAudio
        {
            get { return isEnableAudio; }
            set { SetProperty(ref isEnableAudio, value); }
        }

        public string ExcludedFileTypesString
        {
            get { return excludedFileTypesString; }
            set
            {
                excludedFileTypesList.Clear();
                excludedFileTypesList.AddRange(
                    value.Split(new char[] { ';', ',', '|' }, StringSplitOptions.RemoveEmptyEntries)
                         .Select(str =>
                         {
                             str = str.Trim();
                             return str.StartsWith(".") ? str : $".{str}";
                         }));

                SetProperty(ref excludedFileTypesString, value);
            }
        }

        public IList<string> ExcludedFileTypesList
        {
            get { return excludedFileTypesList; }
        }


        public void CloneFrom(GeneralSettings other)
        {
            this.IsEnablePowerMode = other.IsEnablePowerMode;
            this.IsEnableComboMode = other.IsEnableComboMode;
            this.IsEnableParticles = other.IsEnableParticles;
            this.IsEnableScreenShake = other.IsEnableScreenShake;
            this.IsEnableAudio = other.IsEnableAudio;
            this.ExcludedFileTypesString = other.ExcludedFileTypesString;
        }
    }
}
