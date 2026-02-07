# StickEmUp!

A pretty straightforward mod: Trees will drop sticks (and seeds) when chopped!
New addition: Now includes an option to drop attached vines. Thanks to SIG-ILL for the contribution!

Developed at the request of a Mr. Pakratt0013 for use on his Vintage Story multiplayer streams!

It might be out of date now, but also check out: https://mods.vintagestory.at/onestick which is a similar mod that was developed before this one!

Enjoy!

VintageStory Mod DB Page: https://mods.vintagestory.at/stickemup

## Config
After seeing OneStick, I did borrow the idea of adding a couple simple configs to this mod as well.
Right now, this includes:
#### UseToolTier
Default Value: True

When enabled, the chance for drops scales with the tier of your tool (axe). Each tier adds 20% to the drop rate with the final 5th tier providing 100% of the maximum configured drop rate (see maximum drop rate modifiers for more info). When disabled, all tiers will provide the maximum configured drop rate.

### Maximum Drop Rate Modifiers
These represent the drop rate penalties incurred when chopping a tree with an axe instead of breaking the leaves (or vines) by hand. Think of these as a percentage of the original drop rate. The default values of 0.8 mean that chopping a tree yields the drops at 80% of the original rate, which is a slight nerf to hand-harvesting. If you wish to prevent any type of item from dropping, set the value to 0. If UseToolTier is true, then this is the rate you get at the highest tier tool, with lower tier tools lowering the chance even more, otherwise, this is the rate for every tool.
#### MaxDropRateModifierSticks
Default Value: 0.8

The drop rate penalty for sticks
#### MaxDropRateModifierSeeds
Default Value: 0.8

The drop rate penalty for seeds
#### MaxDropRateModifierVines
Default Value: 0.8

The drop rate penalty for vines
#### MaxDropRateModifierMisc
Default Value: 0.0

The drop rate penalty for anything that comes out of a leaf block that isn't a stick or vanilla seed. If you want to control modded drop rates from leaves, change this value. The value of 0 should mean modded items won't drop from leaves by default.
