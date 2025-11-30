using System;
using Terraria;
namespace FurnitureSolution.Solutions.Core;

public class FurnitureFrameData
{
    public int UnitWidth { get; set; }
    public int UnitHeight { get; set; }
    public bool RowMode { get; set; }
    public int WrapCount { get; set; }
    public int WidthTileCount { get; set; }
    public int HeightTileCount { get; set; }
    public int AnchorX { get; set; }
    public int AnchorY { get; set; }
    public int SubSylesX { get; set; }

    public bool CheckAnchor(in Tile tile)
    {
        if (!tile.HasTile) return false;

        if (WidthTileCount != 1 && AnchorX != -1 && tile.TileFrameX % (SubSylesX == 0 ? UnitWidth : UnitWidth / SubSylesX) != AnchorX * 18) return false;
        if (HeightTileCount != 1 && AnchorY != -1 && tile.TileFrameY % UnitHeight != AnchorY * 18) return false;
        return true;
    }

    public int CalculateStyleIndex(in Tile tile)
    {
        int currentIndex;
        if (RowMode)
        {
            currentIndex = tile.TileFrameX / UnitWidth;
            if (UnitHeight != -1 && WrapCount != -1)
                currentIndex += tile.TileFrameY / UnitHeight * WrapCount;
        }
        else
        {
            currentIndex = tile.TileFrameY / UnitHeight;
            if (UnitWidth != -1 && WrapCount != -1)
                currentIndex += tile.TileFrameX / UnitWidth * WrapCount;
        }
        return currentIndex;
    }

    public void Convert(int i, int j, ushort tileType, short style, FurnitureFrameData previousData)
    {
        for (int x = 0; x < WidthTileCount; x++)
        {
            for (int y = 0; y < HeightTileCount; y++)
            {
                int coordX = i + x;
                int coordY = j + y;
                if (AnchorX > 0)
                    coordX -= AnchorX;
                if (AnchorY > 0)
                    coordY -= AnchorY;
                var currentTile = Framing.GetTileSafely(coordX, coordY);
                if (!currentTile.HasTile) continue;

                currentTile.TileType = tileType;
                int frameX = style;
                int frameY = 0;
                if (WrapCount is not 0 and not -1)
                {
                    frameX = style % WrapCount;
                    frameY = style / WrapCount;
                }
                if (!RowMode)
                    (frameX, frameY) = (frameY, frameX);

                if (currentTile.TileFrameX >= previousData.UnitWidth)
                    currentTile.TileFrameX %= (short)previousData.UnitWidth;
                currentTile.TileFrameX += (short)(UnitWidth * frameX);

                if (currentTile.TileFrameY >= previousData.UnitHeight)
                    currentTile.TileFrameY %= (short)previousData.UnitHeight;
                currentTile.TileFrameY += (short)(UnitHeight * frameY);
            }
        }
        int x0 = i;
        int y0 = j;
        if (AnchorX > 0)
            x0 -= AnchorX;
        if (AnchorY > 0)
            y0 -= AnchorY;
        NetMessage.SendTileSquare(-1, x0, y0, WidthTileCount, HeightTileCount);

    }

    public static FurnitureFrameData FromArray(object[] array)
    {
        FurnitureFrameData data = new();

        if (array[0] is not int width)
            throw new ArgumentException($"Element in index of 0 should be int, but the type is {array[0].GetType()}");
        data.UnitWidth = width;

        if (array[1] is not int height)
            throw new ArgumentException($"Element in index of 1 should be int, but the type is {array[1].GetType()}");
        data.UnitHeight = height;

        if (array[2] is not bool rowMode)
            throw new ArgumentException($"Element in index of 2 should be bool, but the type is {array[2].GetType()}");
        data.RowMode = rowMode;

        if (array[3] is not int wrapCount)
            throw new ArgumentException($"Element in index of 3 should be int, but the type is {array[3].GetType()}");
        data.WrapCount = wrapCount;

        if (array[4] is not int widthTileCount)
            throw new ArgumentException($"Element in index of 4 should be int, but the type is {array[4].GetType()}");
        data.WidthTileCount = widthTileCount;

        if (array[5] is not int heightTileCount)
            throw new ArgumentException($"Element in index of 5 should be int, but the type is {array[5].GetType()}");
        data.HeightTileCount = heightTileCount;

        if (array[6] is not int anchorX)
            throw new ArgumentException($"Element in index of 6 should be int, but the type is {array[6].GetType()}");
        data.AnchorX = anchorX;

        if (array[7] is not int anchorY)
            throw new ArgumentException($"Element in index of 7 should be int, but the type is {array[7].GetType()}");
        data.AnchorY = anchorY;

        if (array[8] is not int subStyleX)
            throw new ArgumentException($"Element in index of 8 should be int, but the type is {array[8].GetType()}");
        data.SubSylesX = subStyleX;

        return data;
    }

    public static FurnitureFrameData PlatformData { get; } = new()
    {
        UnitHeight = 18,
        UnitWidth = 486,
        RowMode = false,
        WrapCount = -1,
        WidthTileCount = 1,
        HeightTileCount = 1,
        AnchorX = 0,
        AnchorY = 0
    };
    public static FurnitureFrameData WorkbenchData { get; } = new()
    {
        UnitHeight = 18,
        UnitWidth = 36,
        RowMode = true,
        WrapCount = -1,
        WidthTileCount = 2,
        HeightTileCount = 1,
        AnchorX = 0,
        AnchorY = 0
    };
    public static FurnitureFrameData TableData { get; } = new()
    {
        UnitHeight = 38,
        UnitWidth = 54,
        RowMode = true,
        WrapCount = -1,
        WidthTileCount = 3,
        HeightTileCount = 2,
        AnchorX = 1,
        AnchorY = 0
    };
    public static FurnitureFrameData ChairData { get; } = new()
    {
        UnitHeight = 40,
        UnitWidth = 36,
        RowMode = false,
        WrapCount = -1,
        WidthTileCount = 1,
        HeightTileCount = 2,
        AnchorX = -1,
        AnchorY = 0
    };
    public static FurnitureFrameData ClosedDoorData { get; } = new()
    {
        UnitHeight = 54,
        UnitWidth = 54,
        RowMode = false,
        WrapCount = 36,
        WidthTileCount = 1,
        HeightTileCount = 3,
        AnchorX = -1,
        AnchorY = 1
    };
    public static FurnitureFrameData OpenDoorData { get; } = new()
    {
        UnitHeight = 54,
        UnitWidth = 72,
        RowMode = false,
        WrapCount = 36,
        WidthTileCount = 2,
        HeightTileCount = 3,
        AnchorX = 0,
        AnchorY = 1,
        SubSylesX = 2
    };
    public static FurnitureFrameData ChestData { get; } = new()
    {
        UnitHeight = 114,
        UnitWidth = 36,
        RowMode = true,
        WrapCount = -1,
        WidthTileCount = 2,
        HeightTileCount = 2,
        AnchorX = 0,
        AnchorY = 0
    };
    public static FurnitureFrameData BedData { get; } = new()
    {
        UnitHeight = 36,
        UnitWidth = 144,
        RowMode = false,
        WrapCount = -1,
        WidthTileCount = 4,
        HeightTileCount = 2,
        AnchorX = 1,
        AnchorY = 0,
        SubSylesX = 2
    };
    public static FurnitureFrameData BookcaseData { get; } = new()
    {
        UnitHeight = 72,
        UnitWidth = 54,
        RowMode = true,
        WrapCount = -1,
        WidthTileCount = 3,
        HeightTileCount = 4,
        AnchorX = 1,
        AnchorY = 1
    };
    public static FurnitureFrameData BathtubData { get; } = new()
    {
        UnitHeight = 36,
        UnitWidth = 144,
        RowMode = false,
        WrapCount = -1,
        WidthTileCount = 4,
        HeightTileCount = 2,
        AnchorX = 1,
        AnchorY = 0,
        SubSylesX = 2
    };
    public static FurnitureFrameData CandelabraData { get; } = new()
    {
        UnitHeight = 36,
        UnitWidth = 72,
        RowMode = false,
        WrapCount = -1,
        WidthTileCount = 2,
        HeightTileCount = 2,
        AnchorX = 0,
        AnchorY = 0
    };
    public static FurnitureFrameData CandleData { get; } = new()
    {
        UnitHeight = 22,
        UnitWidth = 36,
        RowMode = false,
        WrapCount = -1,
        WidthTileCount = 1,
        HeightTileCount = 1,
        AnchorX = 0,
        AnchorY = 0
    };
    public static FurnitureFrameData ChandelierData { get; } = new()
    {
        UnitHeight = 54,
        UnitWidth = 108,
        RowMode = false,
        WrapCount = 37,
        WidthTileCount = 3,
        HeightTileCount = 3,
        AnchorX = 1,
        AnchorY = 1
    };
    public static FurnitureFrameData ClockData { get; } = new()
    {
        UnitHeight = 90,
        UnitWidth = 36,
        RowMode = true,
        WrapCount = -1,
        WidthTileCount = 2,
        HeightTileCount = 5,
        AnchorX = 0,
        AnchorY = 2
    };
    public static FurnitureFrameData DresserData { get; } = new()
    {
        UnitHeight = 36,
        UnitWidth = 54,
        RowMode = true,
        WrapCount = -1,
        WidthTileCount = 3,
        HeightTileCount = 2,
        AnchorX = 1,
        AnchorY = 0
    };
    public static FurnitureFrameData LampData { get; } = new()
    {
        UnitHeight = 54,
        UnitWidth = 36,
        RowMode = false,
        WrapCount = -1,
        WidthTileCount = 1,
        HeightTileCount = 3,
        AnchorX = 0,
        AnchorY = 1
    };
    public static FurnitureFrameData LanternData { get; } = new()
    {
        UnitHeight = 36,
        UnitWidth = 36,
        RowMode = false,
        WrapCount = -1,
        WidthTileCount = 1,
        HeightTileCount = 2,
        AnchorX = 0,
        AnchorY = 0
    };
    public static FurnitureFrameData PianoData { get; } = new()
    {
        UnitHeight = 36,
        UnitWidth = 54,
        RowMode = true,
        WrapCount = -1,
        WidthTileCount = 3,
        HeightTileCount = 2,
        AnchorX = 1,
        AnchorY = 0
    };
    public static FurnitureFrameData SinkData { get; } = new()
    {
        UnitHeight = 38,
        UnitWidth = 36,
        RowMode = false,
        WrapCount = -1,
        WidthTileCount = 2,
        HeightTileCount = 2,
        AnchorX = 0,
        AnchorY = 0
    };
    public static FurnitureFrameData SofaData { get; } = new()
    {
        UnitHeight = 36,
        UnitWidth = 54,
        RowMode = true,
        WrapCount = -1,
        WidthTileCount = 3,
        HeightTileCount = 2,
        AnchorX = 1,
        AnchorY = 0
    };
    public static FurnitureFrameData ToiletData { get; } = new()
    {
        UnitHeight = 40,
        UnitWidth = 36,
        RowMode = false,
        WrapCount = -1,
        WidthTileCount = 1,
        HeightTileCount = 2,
        AnchorX = -1,
        AnchorY = 0
    };
}
