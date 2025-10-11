using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StickEmUp
{
    public class ModConfig
    {
        private static ModConfig _Loaded = new ModConfig();
        public static ModConfig Loaded { get { return _Loaded; } set { _Loaded = value; } }

        private bool _UseToolTier = true;
        public bool UseToolTier { get { return _UseToolTier; } set { _UseToolTier = value; } }

        private float _MaxDropRateModifier = 0.8f;
        public float MaxDropRateModifier { get { return _MaxDropRateModifier; } set { _MaxDropRateModifier = value >= 0 ? value : 0; } }

    }
}
