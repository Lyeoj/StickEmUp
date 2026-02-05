using System.Collections.Generic;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;
using Vintagestory.GameContent;

namespace StickEmUp
{
    public class StickEmUpModSystem : ModSystem
    {
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
                }
                else
                {
                    ModConfig.Loaded = diskConfig;
                }
            }
            catch
            {
                api.StoreModConfig<ModConfig>(ModConfig.Loaded, configFileName);
            }
            base.StartPre(api);
        }

        public override void StartServerSide(ICoreServerAPI api)
        {
            api.Event.BreakBlock += OnBreakBlock;
        }

        private void OnBreakBlock(IServerPlayer byPlayer, BlockSelection blockSel, ref float dropQuantityMultiplier, ref EnumHandling handling)
        {
            if (byPlayer.InventoryManager.ActiveTool == null || byPlayer.InventoryManager.ActiveTool != EnumTool.Axe || blockSel.Block == null || blockSel.Block.BlockMaterial != EnumBlockMaterial.Wood) { return; }

            ItemAxe axe = (ItemAxe)byPlayer.InventoryManager.ActiveHotbarSlot.Itemstack.Item;
            int resistance;
            int woodTier;
            Stack<BlockPos> treePositions = axe.FindTree(byPlayer.Entity.World, blockSel.Position, out resistance, out woodTier);
            List<BlockPos> handledVinesPositions = new();
            while (treePositions.Count > 0)
            {
                BlockPos pos = treePositions.Pop();
                Block block = byPlayer.Entity.World.BlockAccessor.GetBlock(pos);

                if(ModConfig.Loaded.DropVines && (block.BlockMaterial == EnumBlockMaterial.Wood || block.BlockMaterial == EnumBlockMaterial.Leaves))
                {
                    DropAttachedVines(pos, byPlayer, handledVinesPositions);
                }

                if (block.BlockMaterial != EnumBlockMaterial.Leaves) { continue; }

                foreach (BlockDropItemStack drop in block.Drops)
                {
                    ItemStack nextDrop = drop.GetNextItemStack(1f);
                    
                    if (nextDrop == null) { continue; }

                    float modifier = (ModConfig.Loaded.MaxDropRateModifier > 1.0f) ? 1.0f : ModConfig.Loaded.MaxDropRateModifier;
                    double chance = ModConfig.Loaded.UseToolTier ? (axe.ToolTier * modifier / 5.0f) : modifier;
                    if (byPlayer.Entity.World.Rand.NextDouble() < chance)
                    {
                        byPlayer.Entity.World.SpawnItemEntity(nextDrop, new Vec3d((double)pos.X + 0.5, (double)pos.Y + 0.5, (double)pos.Z + 0.5));
                    }
                }
            }
        }

        private void DropAttachedVines(BlockPos treeBlockPos, IServerPlayer byPlayer, List<BlockPos> handledVinesPositions)
        {
            IWorldAccessor world = byPlayer.Entity.World;
            IBlockAccessor blockAccessor = world.BlockAccessor;
			foreach(BlockFacing facing in BlockFacing.HORIZONTALS)  // Look for vines on all four sides of the block.
			{
				BlockPos vinesPosition = treeBlockPos.AddCopy(facing);
				BlockVines? vinesBlock = blockAccessor.GetBlock(vinesPosition) as BlockVines;
				if(vinesBlock == null || vinesBlock.LastCodePart() != facing.Code) { continue; }

                BlockPos vinesTop = FindVinesTop(vinesPosition, blockAccessor);
                if(handledVinesPositions.Contains(vinesTop)) { continue; }

                handledVinesPositions.Add(vinesTop);
                SpawnVinesDrops(vinesTop, byPlayer);
			}
		}

        private BlockPos FindVinesTop(BlockPos vinesPosition, IBlockAccessor blockAccessor)
        {
            BlockPos vinesTop = vinesPosition.Copy();
            vinesTop.Up();
			while(blockAccessor.GetBlock(vinesTop) is BlockVines) {
				vinesTop.Up();
			}
			vinesTop.Down();    // Previous position was top, so go back to that position.
            return vinesTop;
		}

        private void SpawnVinesDrops(BlockPos vinesTop, IServerPlayer byPlayer)
        {
			BlockPos vinesPosition = vinesTop.Copy();
			IWorldAccessor world = byPlayer.Entity.World;
            BlockVines? vinesBlock;
			while((vinesBlock = world.BlockAccessor.GetBlock(vinesPosition) as BlockVines) != null)
            {
				foreach(ItemStack drop in vinesBlock.GetDrops(world, vinesPosition, byPlayer))
                {
					if(drop == null) { continue; }
					world.SpawnItemEntity(drop, new Vec3d(vinesPosition.X + 0.5, vinesPosition.Y + 0.5, vinesPosition.Z + 0.5));
				}

				vinesPosition.Down();
			}
		}
    }
}
