using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace FurnitureSolution.Solutions.Core;

internal sealed class SolutionItemCrossMod(Mod mod, string name, string texturePath,Action<Recipe> setRecipeContent) : SolutionItemBase(mod.Find<ModProjectile>($"{name}SolutionProjectile").Type)
{
    protected override bool CloneNewInstances => true;
    public override string Name => $"{name}Solution";

    public override string Texture => texturePath;

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        setRecipeContent?.Invoke(recipe);
        recipe.Register();
    }
}

internal sealed class SolutionProjectileCrossMod(string name, int furnitureTableRowIndex, int dust) : SolutionProjectileBase(furnitureTableRowIndex)
{
    protected override bool CloneNewInstances => true;
    public override string Name => $"{name}SolutionProjectile";
    public override void ModifyNewDust(ref int dustType, ref Vector2 position, ref Vector2 velocity, ref int extraWidth, ref float extraScale, ref int alpha, ref Color color)
    {
        dustType = dust;
    }
}