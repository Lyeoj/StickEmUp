namespace StickEmUp
{
    public class ModConfig
    {
        private static ModConfig _Loaded = new ModConfig();
        public static ModConfig Loaded { get { return _Loaded; } set { _Loaded = value; } }

        private bool _UseToolTier = true;
        public bool UseToolTier { get { return _UseToolTier; } set { _UseToolTier = value; } }

        private float _MaxDropRateModifierSticks = 0.8f;
        public float MaxDropRateModifierSticks { get { return _MaxDropRateModifierSticks; } set { _MaxDropRateModifierSticks = value >= 0 ? value : 0; } }

        private float _MaxDropRateModifierSeeds = 0.8f;
        public float MaxDropRateModifierSeeds { get { return _MaxDropRateModifierSeeds; } set { _MaxDropRateModifierSeeds = value >= 0 ? value : 0; } }

        private float _MaxDropRateModifierVines = 0.8f;
        public float MaxDropRateModifierVines { get { return _MaxDropRateModifierVines; } set { _MaxDropRateModifierVines = value >= 0 ? value : 0; } }

        private float _MaxDropRateModifierMisc = 0;
        public float MaxDropRateModifierMisc { get { return _MaxDropRateModifierMisc; } set { _MaxDropRateModifierMisc = value >= 0 ? value : 0; } }

    }
}
