# StickEmUp!

A pretty straightforward mod: Trees will drop sticks (and saplings) when chopped!

Developed at the request of a Mr. Pakratt0013 for use on his Vintage Story multiplayer streams!

To my dismay, after getting about 90% of the development done on this mod, I learned about the existence of: https://mods.vintagestory.at/onestick, which basically does the same thing, albeit having a different implementation.
So please check out that mod and see which one you prefer!

Enjoy!

VintageStory Mod DB Page: https://mods.vintagestory.at/stickemup

## Config
After seeing OneStick, I did borrow the idea of adding a couple simple configs to this mod as well.
Right now, this includes:
#### UseToolTier
Default Value: True
When enabled, the chance for drops scales with the tier of your tool (axe). Each tier adds 20% to the drop rate with the final 5th tier providing 100% of the maximum configured drop rate. When disabled, all tiers will provide the maximum configured drop rate.
#### MaxDropRateModifier
Default Value: 0.8
This is not the chance for a leaf to drop a stick or sapling upon chopping a tree. Instead, think of this as an additional tax on the stick/sapling drop rates. At 1.0, or 100%, the leaves should drop sticks/saplings at the same rate as if you broke them by hand. So by default, 0.8 or 80% results in a slight nerf to chopping trees instead of breaking leaves by hand. If UseToolTier is true, then this is the rate you get at the highest tier tool, otherwise, this is the rate for every tool.