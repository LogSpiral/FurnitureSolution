using Terraria.ID;
using Terraria.ModLoader;

namespace FurnitureSolution.Solutions.Core;

public abstract class SolutionItemBase(int projectileType) : ModItem
{
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