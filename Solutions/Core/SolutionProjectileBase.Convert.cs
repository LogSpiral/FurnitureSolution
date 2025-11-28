using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace FurnitureSolution.Solutions.Core;

partial class SolutionProjectileBase
{
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
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
            }
        }
    }

    private static bool ConvertFurniture(int i, int j, in FurnitureSetData furnitureSetData)
    {
        var tile = Framing.GetTileSafely(i, j);
        bool wallReplaced = false;

        // 墙壁转换
        var wallIdx = furnitureSetData.WallType;
        if (wallIdx != ushort.MaxValue
            && wallIdx != (short)tile.WallType
            && FurnitureSolution.WallInSet.Contains(tile.WallType))
        {
            tile.WallType = furnitureSetData.WallType;
            wallReplaced = true;
        }

        // 物块转换
        var tileIdx = furnitureSetData.SolidTileType;
        if (tileIdx != ushort.MaxValue
            && tileIdx != (short)tile.TileType
            && FurnitureSolution.TileInSet.Contains(tile.TileType))
        {
            tile.TileType = furnitureSetData.SolidTileType;
            return true;
        }

        // 根据家具分类进行转换
        static bool TryConvertByCategory(
            Dictionary<ushort, HashSet<short>> categoryDictionary,
            Tile tile,
            int i,
            int j,
            ushort tileType,
            short style,
            in FurnitureFrameData frameData,
            out bool succeed
            )
        {
            succeed = false;
            if (style == -1) return false;
            if (tileType == ushort.MaxValue) return false;
            if (!tile.HasTile) return true;
            // 如果当前分类字典的物块类型里面没找到当前物块所属的就直接开溜
            if (!categoryDictionary.TryGetValue(tile.TileType, out var set)) return false;

            var data = frameData;
            var prevData = frameData;

            if (FurnitureSolution.FrameDataDictionary.TryGetValue(tile.TileType, out var frameDataPrevNew))
                prevData = frameDataPrevNew;

            if (FurnitureSolution.FrameDataDictionary.TryGetValue(tileType, out var frameDataNew))
                data = frameDataNew;

            // 检测是否是锚点物块，仅在锚点上发生统一转换
            if (!prevData.CheckAnchor(tile)) return true;

            // 计算当前物块的style
            int currentIndex = prevData.CalculateStyleIndex(tile);

            // 检测是否需要发生转换
            if (!set.Contains((short)currentIndex)
                || (currentIndex == style
                    && tile.TileType == tileType))
                return false;

            // 转换
            succeed = true;
            data.Convert(i, j, tileType, style, prevData);
            return true;
        }

        #region 别展开，有古法源生

        // C#没有宏太邪恶了
        // 古法SourceGenerator启动

        if (TryConvertByCategory(
                FurnitureSolution.PlatformDictionary,
                tile, i, j,
                furnitureSetData.PlatformType,
                furnitureSetData.PlatformIndex,
                FurnitureFrameData.PlatformData,
                out var succeed))
            return succeed || wallReplaced;

        if (TryConvertByCategory(
                FurnitureSolution.WorkbenchDictionary,
                tile, i, j,
                furnitureSetData.WorkbenchType,
                furnitureSetData.WorkbenchIndex,
                FurnitureFrameData.WorkbenchData,
                out succeed))
            return succeed || wallReplaced;

        if (TryConvertByCategory(
                FurnitureSolution.TableDictionary,
                tile, i, j,
                furnitureSetData.TableType,
                furnitureSetData.TableIndex,
                FurnitureFrameData.TableData,
                out succeed))
            return succeed || wallReplaced;

        if (TryConvertByCategory(
                FurnitureSolution.ChairDictionary,
                tile, i, j,
                furnitureSetData.ChairType,
                furnitureSetData.ChairIndex,
                FurnitureFrameData.ChairData,
                out succeed))
            return succeed || wallReplaced;

        if (TryConvertByCategory(
                FurnitureSolution.DoorClosedDictionary,
                tile, i, j,
                furnitureSetData.ClosedDoorType,
                furnitureSetData.DoorIndex,
                FurnitureFrameData.ClosedDoorData,
                out succeed))
            return succeed || wallReplaced;

        if (TryConvertByCategory(
                FurnitureSolution.DoorOpenDictionary,
                tile, i, j,
                furnitureSetData.OpenDoorType,
                furnitureSetData.DoorIndex,
                FurnitureFrameData.OpenDoorData,
                out succeed))
            return succeed || wallReplaced;

        if (TryConvertByCategory(
                FurnitureSolution.ChestDictionary,
                tile, i, j,
                furnitureSetData.ChestType,
                furnitureSetData.ChestIndex,
                FurnitureFrameData.ChestData,
                out succeed))
            return succeed || wallReplaced;

        if (TryConvertByCategory(
                FurnitureSolution.BedDictionary,
                tile, i, j,
                furnitureSetData.BedType,
                furnitureSetData.BedIndex,
                FurnitureFrameData.BedData,
                out succeed))
            return succeed || wallReplaced;

        if (TryConvertByCategory(
                FurnitureSolution.BookcaseDictionary,
                tile, i, j,
                furnitureSetData.BookcaseType,
                furnitureSetData.BookcaseIndex,
                FurnitureFrameData.BookcaseData,
                out succeed))
            return succeed || wallReplaced;

        if (TryConvertByCategory(
                FurnitureSolution.BathtubDictionary,
                tile, i, j,
                furnitureSetData.BathtubType,
                furnitureSetData.BathtubIndex,
                FurnitureFrameData.BathtubData,
                out succeed))
            return succeed || wallReplaced;

        if (TryConvertByCategory(
                FurnitureSolution.CandelabraDictionary,
                tile, i, j,
                furnitureSetData.CandelabraType,
                furnitureSetData.CandelabraIndex,
                FurnitureFrameData.CandelabraData,
                out succeed))
            return succeed || wallReplaced;

        if (TryConvertByCategory(
                FurnitureSolution.CandleDictionary,
                tile, i, j,
                furnitureSetData.CandleType,
                furnitureSetData.CandleIndex,
                FurnitureFrameData.CandleData,
                out succeed))
            return succeed || wallReplaced;

        if (TryConvertByCategory(
                FurnitureSolution.ChandelierDictionary,
                tile, i, j,
                furnitureSetData.ChandelierType,
                furnitureSetData.ChandelierIndex,
                FurnitureFrameData.ChandelierData,
                out succeed))
            return succeed || wallReplaced;

        if (TryConvertByCategory(
                FurnitureSolution.ClockDictionary,
                tile, i, j,
                furnitureSetData.ClockType,
                furnitureSetData.ClockIndex,
                FurnitureFrameData.ClockData,
                out succeed))
            return succeed || wallReplaced;

        if (TryConvertByCategory(
                FurnitureSolution.DresserDictionary,
                tile, i, j,
                furnitureSetData.DresserType,
                furnitureSetData.DresserIndex,
                FurnitureFrameData.DresserData,
                out succeed))
            return succeed || wallReplaced;

        if (TryConvertByCategory(
                FurnitureSolution.LampDictionary,
                tile, i, j,
                furnitureSetData.LampType,
                furnitureSetData.LampIndex,
                FurnitureFrameData.LampData,
                out succeed))
            return succeed || wallReplaced;

        if (TryConvertByCategory(
                FurnitureSolution.LanternDictionary,
                tile, i, j,
                furnitureSetData.LanternType,
                furnitureSetData.LanternIndex,
                FurnitureFrameData.LanternData,
                out succeed))
            return succeed || wallReplaced;

        if (TryConvertByCategory(
                FurnitureSolution.PianoDictionary,
                tile, i, j,
                furnitureSetData.PianoType,
                furnitureSetData.PianoIndex,
                FurnitureFrameData.PianoData,
                out succeed))
            return succeed || wallReplaced;

        if (TryConvertByCategory(
                FurnitureSolution.SinkDictionary,
                tile, i, j,
                furnitureSetData.SinkType,
                furnitureSetData.SinkIndex,
                FurnitureFrameData.SinkData,
                out succeed))
            return succeed || wallReplaced;

        if (TryConvertByCategory(
                FurnitureSolution.SofaDictionary,
                tile, i, j,
                furnitureSetData.SofaType,
                furnitureSetData.SofaIndex,
                FurnitureFrameData.SofaData,
                out succeed))
            return succeed || wallReplaced;

        if (TryConvertByCategory(
                FurnitureSolution.ToiletDictionary,
                tile, i, j,
                furnitureSetData.ToiletType,
                furnitureSetData.ToiletIndex,
                FurnitureFrameData.ToiletData,
                out succeed))
            return succeed || wallReplaced;


        #endregion

        return wallReplaced;
    }
}
