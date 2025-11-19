using Terraria.ID;
using Terraria.ModLoader;

namespace FurnitureSolution.Solutions.Core;

public abstract class SolutionItemBase(int projectileType) : ModItem
{
    // TODO 每个物品都画上相应贴图
    public override string Texture => $"Terraria/Images/Item_{ItemID.GreenSolution}";
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Item.DefaultToSolution(projectileType);
        Item.rare = ItemRarityID.Orange;
    }

    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.Solutions;
    }
}

public abstract class SolutionItemBase<T>() : SolutionItemBase(ModContent.ProjectileType<T>()) where T : SolutionProjectileBase;