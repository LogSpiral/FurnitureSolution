using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FurnitureSolution.Solutions.Core;

public abstract partial class SolutionProjectileBase(int furnitureTableRowIndex) : ModProjectile
{

    public override string Texture => $"{nameof(FurnitureSolution)}/Solutions/Core/SolutionSpray";
    public override void SetDefaults()
    {
        Projectile.width = 6;
        Projectile.height = 6;
        Projectile.aiStyle = 0;
        Projectile.friendly = true;
        Projectile.alpha = 255;
        Projectile.penetrate = -1;
        Projectile.extraUpdates = 2;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        base.SetDefaults();
    }
    private int FurnitureTableRowIndex { get; } = furnitureTableRowIndex;
    public override bool? CanCutTiles() => false;
    public override void AI()
    {

        bool flag11 = Projectile.ai[1] == 1f;

        if (Projectile.owner == Main.myPlayer)
        {
            int size = 2;
            if (flag11)
                size = 3;

            Point point = Projectile.Center.ToTileCoordinates();
            Convert(point.X, point.Y, size);
        }

        if (Projectile.timeLeft > 133)
            Projectile.timeLeft = 133;

        int num274 = 7;
        if (flag11)
            num274 = 3;

        if (Projectile.ai[0] > (float)num274)
        {
            float extraScale = 1f;
            if (Projectile.ai[0] == (float)(num274 + 1))
                extraScale = 0.2f;
            else if (Projectile.ai[0] == (float)(num274 + 2))
                extraScale = 0.4f;
            else if (Projectile.ai[0] == (float)(num274 + 3))
                extraScale = 0.6f;
            else if (Projectile.ai[0] == (float)(num274 + 4))
                extraScale = 0.8f;

            int extraWidth = 0;
            if (flag11)
            {
                extraScale *= 1.2f;
                extraWidth = (int)(12f * extraScale);
            }

            Projectile.ai[0]++;
            int dustType = DustID.GreenTorch;
            for (int num277 = 0; num277 < 1; num277++)
            {
                Vector2 velocity = Projectile.velocity * .2f;
                Color color = Color.White;
                Vector2 position = Projectile.position;
                int alpha = 100;
                ModifyNewDust(
                    ref dustType,
                    ref position,
                    ref velocity,
                    ref extraWidth,
                    ref extraScale,
                    ref alpha,
                    ref color);

                if (!PreNewDust(
                    dustType,
                    position,
                    velocity,
                    extraWidth,
                    extraScale,
                    alpha,
                    color)) continue;

                int num278 = Dust.NewDust(
                    position - new Vector2(extraWidth),
                    Projectile.width + extraWidth * 2,
                    Projectile.height + extraWidth * 2,
                    dustType,
                    velocity.X,
                    velocity.Y,
                    alpha,
                    color);
                Main.dust[num278].noGravity = true;
                Dust dust2 = Main.dust[num278];
                dust2.scale *= 1.75f;
                Main.dust[num278].velocity.X *= 2f;
                Main.dust[num278].velocity.Y *= 2f;
                dust2 = Main.dust[num278];
                dust2.scale *= extraScale;
            }
        }
        else
        {
            Projectile.ai[0]++;
        }

        Projectile.rotation += 0.3f * (float)Projectile.direction;
        base.AI();
    }
    public virtual void ModifyNewDust(
        ref int dustType,
        ref Vector2 position,
        ref Vector2 velocity,
        ref int extraWidth,
        ref float extraScale,
        ref int alpha,
        ref Color color)
    {

    }
    public virtual bool PreNewDust(
        int dustType,
        Vector2 position,
        Vector2 velocity,
        int extraWidth,
        float extraScale,
        int alpha,
        Color color)
    {
        return true;
    }
}
