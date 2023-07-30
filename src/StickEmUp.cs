using System.Collections.Generic;
using System.Diagnostics;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;
using Vintagestory.GameContent;

namespace stickemup.src
{
    public class StickEmUp : ModSystem
    {

        ICoreServerAPI api;

        public override bool ShouldLoad(EnumAppSide side)
        {
            return side == EnumAppSide.Server;
        }

        public override void StartPre(ICoreAPI api)
        {
            var configFileName = "stickemup_config.json";
            try
            {
                ModConfig diskConfig;
                if ((diskConfig = api.LoadModConfig<ModConfig>(configFileName)) == null)
                {
                    api.StoreModConfig<ModConfig>(ModConfig.Loaded, configFileName);
                } else
                {
                    ModConfig.Loaded = diskConfig;
                }
            } catch
            {
                api.StoreModConfig<ModConfig>(ModConfig.Loaded, configFileName);
            }
            base.StartPre(api);
        }

        public override void StartServerSide(ICoreServerAPI api)
        {
            this.api = api;
            api.Event.BreakBlock += OnBreakBlock;
        }

        private void OnBreakBlock(IServerPlayer byPlayer, BlockSelection blockSel, ref float dropQuantityMultiplier, ref EnumHandling handling)
        {
            if(byPlayer.InventoryManager.ActiveTool != null && byPlayer.InventoryManager.ActiveTool == EnumTool.Axe)
            {
                ItemAxe axe = (ItemAxe)byPlayer.InventoryManager.ActiveHotbarSlot.Itemstack.Item;
                if (blockSel.Block != null && blockSel.Block.BlockMaterial == EnumBlockMaterial.Wood)
                {
                    int resistance;
                    int woodTier;
                    Stack<BlockPos> stack = axe.FindTree(byPlayer.Entity.World, blockSel.Position, out resistance, out woodTier);
                    while (stack.Count > 0)
                    {
                        BlockPos pos = stack.Pop();
                        Block block = byPlayer.Entity.World.BlockAccessor.GetBlock(pos);
                        if(block.BlockMaterial == EnumBlockMaterial.Leaves)
                        {                                     
                            foreach (BlockDropItemStack drop in block.Drops)
                            {
                                ItemStack nextDrop = drop.GetNextItemStack(1f);
                                if(nextDrop != null)
                                {
                                    float modifier = (ModConfig.Loaded.MaxDropRateModifier > 1.0f) ? 1.0f : ModConfig.Loaded.MaxDropRateModifier;
                                    double chance = ModConfig.Loaded.UseToolTier ? (axe.ToolTier * modifier / 5.0f) : modifier;
                                    if(byPlayer.Entity.World.Rand.NextDouble() < chance)
                                    {
                                        byPlayer.Entity.World.SpawnItemEntity(nextDrop, new Vec3d((double)pos.X + 0.5, (double)pos.Y + 0.5, (double)pos.Z + 0.5));
                                    }
                                }

                            }                    
                        }
                    }
                }
                foreach (BlockDropItemStack stack in blockSel.Block.Drops)
                {
                    
                }
                
            }
        }

    }
}
