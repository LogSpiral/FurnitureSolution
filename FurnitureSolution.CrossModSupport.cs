using FurnitureSolution.Solutions.Core;
using FurnitureSolution.Solutions.WoodSolutions;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria.ID;
using Terraria.ModLoader;

namespace FurnitureSolution;

public partial class FurnitureSolution
{

    public override object Call(params object[] args)
    {
        if (args[0] is not string methodName)
        {
            Logger.Error("First parameter should be methodName");
            return false;
        }
        switch (methodName)
        {
            case nameof(RegisterModFurnitureSolution):
                {
                    if (args[1] is not Mod mod
                        || args[2] is not string setName
                        || args[3] is not string TexturePath
                        || args[4] is not int dustType
                        || args[5] is not short[] array)
                    {
                        Logger.Error("parameter type wrong.");
                        return false;
                    }
                    if (array.Length != 23)
                    {
                        Logger.Error("furniture set data should be 23 elements");
                        return false;
                    }
                    var data = FurnitureSetData.FromArray(array);
                    data.IsModFurnitureSet = true;
                    return RegisterModFurnitureSolution(mod, setName, TexturePath, dustType, data);
                }
            default:
                Logger.Error("Unknown Method");
                return false;
        }
    }


    public static int RegisterModFurnitureSolution(
        Mod mod,
        string setName,
        string TexturePath,
        int dustType,
        in FurnitureSetData data
        )
    {
        FurnitureSets.Add(data);

        #region RegisterToHashSet
        static void AddIfVaild(HashSet<short> set, short value)
        {
            if (value != -1) set.Add(value);
        }
        AddIfVaild(ModTileInSet, data.SolidTileType);
        AddIfVaild(ModWallInSet, data.WallType);
        AddIfVaild(ModPlatformInSet, data.PlatformIndex);
        AddIfVaild(ModWorkbenchInSet, data.WorkbenchIndex);
        AddIfVaild(ModTableInSet, data.TableIndex);
        AddIfVaild(ModChairInSet, data.ChairIndex);
        AddIfVaild(ModClosedDoorInSet, data.DoorIndex);
        AddIfVaild(ModOpenDoorInSet, data.OpenDoorType);
        AddIfVaild(ModChestInSet, data.ChestIndex);
        AddIfVaild(ModBedInSet, data.BedIndex);
        AddIfVaild(ModBookcaseInSet, data.BookcaseIndex);
        AddIfVaild(ModBathtubInSet, data.BathtubIndex);
        AddIfVaild(ModCandelabraInSet, data.CandelabraIndex);
        AddIfVaild(ModCandleInSet, data.CandleIndex);
        AddIfVaild(ModChandelierInSet, data.ChandelierIndex);
        AddIfVaild(ModClockInSet, data.ClockIndex);
        AddIfVaild(ModDresserInSet, data.DresserIndex);
        AddIfVaild(ModLampInSet, data.LampIndex);
        AddIfVaild(ModLanternInSet, data.LanternIndex);
        AddIfVaild(ModPianoInSet, data.PianoIndex);
        AddIfVaild(ModSinkInSet, data.SinkIndex);
        AddIfVaild(ModSofaInSet, data.SofaIndex);
        AddIfVaild(ModToiletInSet, data.ToiletIndex);
        #endregion

        int rowIndex = FurnitureSets.Count - 1;
        var crossModSolutionProjectile = new SolutionProjectileCrossMod(setName, rowIndex, dustType);
        mod.AddContent(crossModSolutionProjectile);
        var crossModSolutionItem = new SolutionItemCrossMod(mod, setName, TexturePath);
        mod.AddContent(crossModSolutionItem);
        if (ModLoader.TryGetMod("TouhouPets", out var touhouPets))
            touhouPets.Call(
                "YukaSolutionInfo",
                Instance,
                mod.Find<ModItem>($"{setName}Solution").Type,
                mod.Find<ModProjectile>($"{setName}SolutionProjectile").Type,
                dustType);

        return rowIndex;
    }


    public static FurnitureSetData GetFurnitureSetData(int index) => FurnitureSets[index];

    public static FurnitureSetData GetFurnitureSetDataFromName(string name)
    {
        if (FurnitureSetIndexDictionary.TryGetValue(name, out var index))
            return FurnitureSets[index];
        return default;
    }

    public static int GetFurnitureSetIndexFromName(string name)
    {
        if (FurnitureSetIndexDictionary.TryGetValue(name, out var index))
            return index;
        return -1;
    }


    internal static HashSet<short> ModTileInSet { get; } = [];
    internal static HashSet<short> ModWallInSet { get; } = [];
    internal static HashSet<short> ModPlatformInSet { get; } = [];
    internal static HashSet<short> ModWorkbenchInSet { get; } = [];
    internal static HashSet<short> ModTableInSet { get; } = [];
    internal static HashSet<short> ModChairInSet { get; } = [];
    internal static HashSet<short> ModClosedDoorInSet { get; } = [];
    internal static HashSet<short> ModOpenDoorInSet { get; } = [];
    internal static HashSet<short> ModChestInSet { get; } = [];
    internal static HashSet<short> ModBedInSet { get; } = [];
    internal static HashSet<short> ModBookcaseInSet { get; } = [];
    internal static HashSet<short> ModBathtubInSet { get; } = [];
    internal static HashSet<short> ModCandelabraInSet { get; } = [];
    internal static HashSet<short> ModCandleInSet { get; } = [];
    internal static HashSet<short> ModChandelierInSet { get; } = [];
    internal static HashSet<short> ModClockInSet { get; } = [];
    internal static HashSet<short> ModDresserInSet { get; } = [];
    internal static HashSet<short> ModLampInSet { get; } = [];
    internal static HashSet<short> ModLanternInSet { get; } = [];
    internal static HashSet<short> ModPianoInSet { get; } = [];
    internal static HashSet<short> ModSinkInSet { get; } = [];
    internal static HashSet<short> ModSofaInSet { get; } = [];
    internal static HashSet<short> ModToiletInSet { get; } = [];
}
