using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FurnitureSolution.Solutions.Core;

public abstract class SolutionProjectileBase(int furnitureTableRowIndex) : ModProjectile
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
    private void Convert(int i, int j, int size = 4)
    {
        for (int k = i - size; k <= i + size; k++)
        {
            for (int l = j - size; l <= j + size; l++)
            {
                if (!WorldGen.InWorld(k, l, 1) || Math.Abs(k - i) + Math.Abs(l - j) >= 6)
                    continue;
                if (ConvertFurniture(k, l, FurnitureSolution.FurnitureSets[FurnitureTableRowIndex]))
                {
                    WorldGen.SquareWallFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }

            }
        }
    }

    private static bool ConvertFurniture(int i, int j, in FurnitureSetData furnitureSetData)
    {
        var tile = Framing.GetTileSafely(i, j);
        bool wallReplaced = false;
        var wallIdx = furnitureSetData.WallType;
        if (wallIdx != -1
            && wallIdx != (short)tile.WallType
            && FurnitureSolution.WallInSet.Contains((short)tile.WallType))
        {
            tile.WallType = (ushort)furnitureSetData.WallType;
            wallReplaced = true;
        }
        var tileIdx = furnitureSetData.SolidTileType;
        if (tileIdx != -1
            && tileIdx != (short)tile.TileType
            && FurnitureSolution.TileInSet.Contains((short)tile.TileType))
        {
            tile.TileType = (ushort)furnitureSetData.SolidTileType;
            return true;
        }
        switch (tile.TileType)
        {
            case TileID.Platforms:
                {
                    var idx = furnitureSetData.PlatformIndex;
                    if (idx == -1) break;
                    int currentIndex = tile.TileFrameY / 18;
                    if (FurnitureSolution.PlatformInSet.Contains((short)currentIndex)
                        && currentIndex != idx)
                    {
                        tile.TileFrameY = (short)(idx * 18);
                        return true;
                    }
                    break;
                }

            case TileID.WorkBenches:
                {
                    var idx = furnitureSetData.WorkbenchIndex;
                    if (idx == -1) break;
                    int currentIndex = tile.TileFrameX / 18;
                    if (currentIndex % 2 == 1) break;
                    currentIndex /= 2;
                    if (FurnitureSolution.WorkbenchInSet.Contains((short)currentIndex)
                        && currentIndex != idx)
                    {
                        tile.TileFrameX = (short)(idx * 36);
                        Framing.GetTileSafely(i + 1, j).TileFrameX = (short)(idx * 36 + 18);
                        return true;
                    }
                    break;
                }
            case TileID.Tables:
            case TileID.Tables2:
                {
                    int idx = furnitureSetData.TableIndex;
                    if (idx == -1) break;
                    int currentIndex = tile.TileFrameX / 18;
                    if (currentIndex % 3 != 1) break;
                    if (tile.TileFrameY != 0) break;
                    currentIndex /= 3;
                    if (tile.TileType == TileID.Tables2)
                        currentIndex += 100;
                    if (FurnitureSolution.TableInSet.Contains((short)currentIndex)
                        && currentIndex != idx)
                    {
                        bool isTable2 = idx >= 100;
                        var tileType = isTable2 ? TileID.Tables2 : TileID.Tables;
                        if (isTable2) idx -= 100;
                        for (int x = -1; x < 2; x++)
                            for (int y = 0; y < 2; y++)
                            {
                                var currentTile = Framing.GetTileSafely(i + x, j + y);
                                currentTile.TileType = tileType;
                                currentTile.TileFrameX = (short)(idx * 54 + 18 * (x + 1));
                            }
                        return true;
                    }
                    break;
                }
            case TileID.Chairs:
                {
                    var idx = furnitureSetData.ChairIndex;
                    if (idx == -1) break;
                    int currentIndex = tile.TileFrameY / 40;
                    if (tile.TileFrameY % 40 == 18) break;
                    if (FurnitureSolution.ChairInSet.Contains((short)currentIndex)
                        && currentIndex != idx)
                    {
                        tile.TileFrameY = (short)(idx * 40);
                        Framing.GetTileSafely(i, j + 1).TileFrameY = (short)(idx * 40 + 18);
                        return true;
                    }
                    else if (FurnitureSolution.ToiletInSet.Contains((short)(currentIndex + 100))
                        && furnitureSetData.ToiletIndex is { } tolietIdx && tolietIdx != -1 && tolietIdx != currentIndex + 100)
                    {
                        for (int y = 0; y < 2; y++)
                        {
                            var currentTile = Framing.GetTileSafely(i, j + y);
                            currentTile.TileFrameY = (short)(tolietIdx * 40 + 18 * y);
                            currentTile.TileType = TileID.Toilets;
                        }
                    }
                    break;
                }
            case TileID.ClosedDoor:
                {
                    int idx = furnitureSetData.DoorIndex;
                    if (idx == -1) break;
                    int currentIndex = tile.TileFrameY / 18;
                    if (currentIndex % 3 != 1) break;
                    currentIndex /= 3;
                    currentIndex += tile.TileFrameX / 54 * 36;
                    if (FurnitureSolution.DoorInSet.Contains((short)currentIndex)
                        && currentIndex != idx)
                    {
                        int frameX = idx >= 36 ? 54 : 0;
                        frameX += Main.rand.Next(3) * 18;
                        if (idx >= 36) idx -= 36;
                        for (int y = -1; y < 2; y++)
                        {
                            var currentTile = Framing.GetTileSafely(i, j + y);
                            currentTile.TileFrameX = (short)frameX;
                            currentTile.TileFrameY = (short)(idx * 54 + 18 * (y + 1));
                        }
                        return true;
                    }
                    break;
                }
            case TileID.OpenDoor:
                {
                    var idx = furnitureSetData.DoorIndex;
                    if (idx == -1) break;
                    int currentIndex = tile.TileFrameY / 18;
                    if (currentIndex % 3 != 1) break;
                    int tFrX = tile.TileFrameX % 72;
                    if (tFrX is 18 or 36) break;
                    currentIndex /= 3;
                    currentIndex += tile.TileFrameX / 72 * 36;
                    if (FurnitureSolution.DoorInSet.Contains((short)currentIndex)
                        && currentIndex != idx)
                    {
                        int offX = tFrX is 0 ? 0 : -1;
                        int frameX = idx >= 36 ? 72 : 0;
                        if (idx >= 36) idx -= 36;
                        for (int x = 0; x < 2; x++)
                            for (int y = -1; y < 2; y++)
                            {
                                var currentTile = Framing.GetTileSafely(i + x + offX, j + y);

                                currentTile.TileFrameX = (short)(frameX + currentTile.TileFrameX % 72);
                                currentTile.TileFrameY = (short)(idx * 54 + 18 * (y + 1));
                            }
                        return true;
                    }
                    break;
                }
            case TileID.Containers:
            case TileID.Containers2:
                {
                    int idx = furnitureSetData.ChestIndex;
                    if (idx == -1) break;
                    int currentIndex = tile.TileFrameX / 18;
                    if (currentIndex % 2 == 1) break;
                    if (tile.TileFrameY != 0) break;
                    currentIndex /= 2;
                    if (tile.TileType == TileID.Containers2)
                        currentIndex += 100;
                    if (FurnitureSolution.ChestInSet.Contains((short)currentIndex)
                        && currentIndex != idx)
                    {
                        bool isTable2 = idx >= 100;
                        var tileType = isTable2 ? TileID.Containers2 : TileID.Containers;
                        if (isTable2) idx -= 100;
                        for (int x = 0; x < 2; x++)
                            for (int y = 0; y < 2; y++)
                            {
                                var currentTile = Framing.GetTileSafely(i + x, j + y);
                                currentTile.TileType = tileType;
                                currentTile.TileFrameX = (short)(idx * 36 + 18 * x);
                            }
                        return true;
                    }
                    break;
                }
            case TileID.Beds:
                {
                    var idx = furnitureSetData.BedIndex;
                    if (idx == -1) break;
                    int currentIndex = tile.TileFrameY / 18;
                    if (currentIndex % 2 == 1) break;
                    int tFrX = tile.TileFrameX % 72;
                    if (tFrX is not 18) break;
                    currentIndex /= 2;
                    if (FurnitureSolution.BedInSet.Contains((short)currentIndex)
                        && currentIndex != idx)
                    {
                        for (int x = -1; x < 3; x++)
                            for (int y = 0; y < 2; y++)
                            {
                                var currentTile = Framing.GetTileSafely(i + x, j + y);
                                currentTile.TileFrameY = (short)(idx * 36 + 18 * y);
                            }
                        return true;
                    }
                    break;
                }
            case TileID.Bookcases:
                {
                    int idx = furnitureSetData.BookcaseIndex;
                    if (idx == -1) break;
                    int currentIndex = tile.TileFrameX / 18;
                    if (currentIndex % 3 != 1) break;
                    if (tile.TileFrameY != 36) break;
                    currentIndex /= 3;
                    if (FurnitureSolution.BookcaseInSet.Contains((short)currentIndex)
                        && currentIndex != idx)
                    {
                        for (int x = -1; x < 2; x++)
                            for (int y = -2; y < 2; y++)
                            {
                                var currentTile = Framing.GetTileSafely(i + x, j + y);
                                currentTile.TileFrameX = (short)(idx * 54 + 18 * (x + 1));
                            }
                        return true;
                    }
                    break;
                }
            case TileID.Bathtubs:
                {
                    var idx = furnitureSetData.BathtubIndex;
                    if (idx == -1) break;
                    int currentIndex = tile.TileFrameY / 18;
                    if (currentIndex % 2 == 1) break;
                    int tFrX = tile.TileFrameX % 72;
                    if (tFrX is not 18) break;
                    currentIndex /= 2;
                    if (FurnitureSolution.BathtubInSet.Contains((short)currentIndex)
                        && currentIndex != idx)
                    {
                        for (int x = -1; x < 3; x++)
                            for (int y = 0; y < 2; y++)
                            {
                                var currentTile = Framing.GetTileSafely(i + x, j + y);
                                currentTile.TileFrameY = (short)(idx * 36 + 18 * y);
                            }
                        return true;
                    }
                    break;
                }
            case TileID.Candelabras:
                {
                    var idx = furnitureSetData.CandelabraIndex;
                    if (idx == -1) break;
                    int currentIndex = tile.TileFrameY / 18;
                    if (currentIndex % 2 == 1) break;
                    int tFrX = tile.TileFrameX % 36;
                    if (tFrX is not 0) break;
                    currentIndex /= 2;
                    if (FurnitureSolution.CandelabraInSet.Contains((short)currentIndex)
                        && currentIndex != idx)
                    {
                        for (int x = 0; x < 2; x++)
                            for (int y = 0; y < 2; y++)
                            {
                                var currentTile = Framing.GetTileSafely(i + x, j + y);
                                currentTile.TileFrameY = (short)(idx * 36 + 18 * y);
                            }
                        return true;
                    }
                    break;
                }
            case TileID.Candles:
                {
                    var idx = furnitureSetData.CandleIndex;
                    if (idx == -1) break;
                    int currentIndex = tile.TileFrameY / 22;
                    if (FurnitureSolution.CandleInSet.Contains((short)currentIndex)
                        && currentIndex != idx)
                    {
                        tile.TileFrameY = (short)(22 * idx);
                        return true;
                    }
                    break;
                }
            case TileID.Chandeliers:
                {
                    int idx = furnitureSetData.ChandelierIndex;
                    if (idx == -1) break;
                    int currentIndex = tile.TileFrameY / 18;
                    if (currentIndex % 3 != 1) break;
                    if (tile.TileFrameX % 108 != 18) break;
                    currentIndex /= 3;

                    currentIndex += tile.TileFrameX / 108 * 37;
                    if (FurnitureSolution.ChandelierInSet.Contains((short)currentIndex)
                        && currentIndex != idx)
                    {
                        int frameX = idx >= 37 ? 108 : 0;
                        if (idx >= 37) idx -= 37;
                        for (int x = -1; x < 2; x++)
                            for (int y = -1; y < 2; y++)
                            {
                                var currentTile = Framing.GetTileSafely(i + x, j + y);
                                currentTile.TileFrameX = (short)(frameX + currentTile.TileFrameX % 108);
                                currentTile.TileFrameY = (short)(idx * 54 + 18 * (y + 1));
                            }
                        return true;
                    }
                    break;
                }
            case TileID.GrandfatherClocks:
                {
                    var idx = furnitureSetData.ClockIndex;
                    if (idx == -1) break;
                    int currentIndex = tile.TileFrameX / 18;
                    if (currentIndex % 2 == 1) break;
                    if (tile.TileFrameY is not 36) break;
                    currentIndex /= 2;
                    if (FurnitureSolution.ClockInSet.Contains((short)currentIndex)
                        && currentIndex != idx)
                    {
                        for (int x = 0; x < 2; x++)
                            for (int y = -2; y < 3; y++)
                            {
                                var currentTile = Framing.GetTileSafely(i + x, j + y);
                                currentTile.TileFrameX = (short)(idx * 36 + 18 * x);
                            }
                        return true;
                    }
                    break;
                }
            case TileID.Dressers:
                {
                    int idx = furnitureSetData.DresserIndex;
                    if (idx == -1) break;
                    int currentIndex = tile.TileFrameX / 18;
                    if (currentIndex % 3 != 1) break;
                    if (tile.TileFrameY != 0) break;
                    currentIndex /= 3;
                    if (FurnitureSolution.DresserInSet.Contains((short)currentIndex)
                        && currentIndex != idx)
                    {
                        for (int x = -1; x < 2; x++)
                            for (int y = 0; y < 2; y++)
                            {
                                var currentTile = Framing.GetTileSafely(i + x, j + y);
                                currentTile.TileFrameX = (short)(idx * 54 + 18 * (x + 1));
                            }
                        return true;
                    }
                    break;
                }
            case TileID.Lamps:
                {
                    int idx = furnitureSetData.LampIndex;
                    if (idx == -1) break;
                    int currentIndex = tile.TileFrameY / 18;
                    if (currentIndex % 3 != 1) break;
                    currentIndex /= 3;
                    if (FurnitureSolution.LampInSet.Contains((short)currentIndex)
                        && currentIndex != idx)
                    {
                        for (int y = -1; y < 2; y++)
                        {
                            var currentTile = Framing.GetTileSafely(i, j + y);
                            currentTile.TileFrameY = (short)(idx * 54 + 18 * (y + 1));
                        }
                        return true;
                    }
                    break;
                }
            case TileID.HangingLanterns:
                {
                    int idx = furnitureSetData.LanternIndex;
                    if (idx == -1) break;
                    int currentIndex = tile.TileFrameY / 18;
                    if (currentIndex % 2 == 1) break;
                    currentIndex /= 2;
                    if (FurnitureSolution.LanternInSet.Contains((short)currentIndex)
                        && currentIndex != idx)
                    {
                        for (int y = 0; y < 2; y++)
                        {
                            var currentTile = Framing.GetTileSafely(i, j + y);
                            currentTile.TileFrameY = (short)(idx * 36 + 18 * y);
                        }
                        return true;
                    }
                    break;
                }
            case TileID.Pianos:
                {
                    int idx = furnitureSetData.PianoIndex;
                    if (idx == -1) break;
                    int currentIndex = tile.TileFrameX / 18;
                    if (currentIndex % 3 != 1) break;
                    if (tile.TileFrameY != 0) break;
                    currentIndex /= 3;
                    if (FurnitureSolution.PianoInSet.Contains((short)currentIndex)
                        && currentIndex != idx)
                    {
                        for (int x = -1; x < 2; x++)
                            for (int y = 0; y < 2; y++)
                            {
                                var currentTile = Framing.GetTileSafely(i + x, j + y);
                                currentTile.TileFrameX = (short)(idx * 54 + 18 * (x + 1));
                            }
                        return true;
                    }
                    break;
                }
            case TileID.Sinks:
                {
                    var idx = furnitureSetData.SinkIndex;
                    if (idx == -1) break;
                    int currentIndex = tile.TileFrameY / 38;
                    if (tile.TileFrameY % 38 == 18) break;
                    if (tile.TileFrameX == 18) break;
                    if (FurnitureSolution.SinkInSet.Contains((short)currentIndex)
                        && currentIndex != idx)
                    {
                        for (int x = 0; x < 2; x++)
                            for (int y = 0; y < 2; y++)
                            {
                                var currentTile = Framing.GetTileSafely(i + x, j + y);
                                currentTile.TileFrameY = (short)(idx * 38 + 18 * y);
                            }
                        return true;
                    }
                    break;
                }
            case TileID.Benches:
                {
                    int idx = furnitureSetData.SofaIndex;
                    if (idx == -1) break;
                    int currentIndex = tile.TileFrameX / 18;
                    if (currentIndex % 3 != 1) break;
                    if (tile.TileFrameY != 0) break;
                    currentIndex /= 3;
                    if (FurnitureSolution.SofaInSet.Contains((short)currentIndex)
                        && currentIndex != idx)
                    {
                        for (int x = -1; x < 2; x++)
                            for (int y = 0; y < 2; y++)
                            {
                                var currentTile = Framing.GetTileSafely(i + x, j + y);
                                currentTile.TileFrameX = (short)(idx * 54 + 18 * (x + 1));
                            }
                        return true;
                    }
                    break;
                }
            case TileID.Toilets:
                {
                    var idx = furnitureSetData.ToiletIndex;
                    int currentIndex = tile.TileFrameY / 40;
                    if (tile.TileFrameY % 40 == 18) break;
                    if (FurnitureSolution.ToiletInSet.Contains((short)currentIndex)
                        && currentIndex != idx)
                    {
                        ushort tileType = TileID.Toilets;
                        if (idx >= 100)
                        {
                            idx -= 100;
                            tileType = TileID.Chairs;
                        }
                        for (int y = 0; y < 2; y++)
                        {
                            var currentTile = Framing.GetTileSafely(i, j + y);
                            currentTile.TileFrameY = (short)(idx * 40 + 18 * y);
                            currentTile.TileType = tileType;
                        }
                        return true;
                    }
                    break;
                }

        }
        return wallReplaced;
    }
}
