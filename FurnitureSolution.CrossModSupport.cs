using FurnitureSolution.Solutions.Core;
using System;
using System.Collections.Generic;
using Terraria;
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
                        || args[5] is not Action<Recipe> setRecipeContent
                        || args[6] is not object[] array)
                    {
                        Logger.Error("parameter type wrong.");
                        return false;
                    }
                    if (array.Length != 22)
                    {
                        Logger.Error("furniture set data should be 22 elements");
                        return false;
                    }
                    var data = FurnitureSetData.FromArray(array);
                    return RegisterModFurnitureSolution(mod, setName, TexturePath, dustType, setRecipeContent, data);
                }
            case nameof(SetModFurnitureFrameData):
                {
                    if (args[1] is not int tileType
                        || args[2] is not object[] array)
                    {
                        Logger.Error("parameter type wrong.");
                        return false;
                    }
                    if (array.Length != 9)
                    {
                        Logger.Error("furniture frame data should be 9 elements");
                        return false;
                    }
                    if (tileType < 0)
                        return false;
                    SetModFurnitureFrameData((ushort)tileType, FurnitureFrameData.FromArray(array));
                    return true;
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
        Action<Recipe> setRecipeContent,
        in FurnitureSetData data
        )
    {
        FurnitureSets.Add(data);

        #region RegisterToHashSet
        static void AddIfValid(HashSet<ushort> set, ushort value)
        {
            if (value != ushort.MaxValue) set.Add(value);
        }
        static void RegisterIfValid(Dictionary<ushort, HashSet<short>> dictionary, ushort key, short style)
        {
            if (key == ushort.MaxValue) return;
            if (style == -1) return;
            if (!dictionary.TryGetValue(key, out var set))
            {
                set = [];
                dictionary.Add(key, set);
            }
            set.Add(style);
        }

        AddIfValid(TileInSet, data.SolidTileType);
        AddIfValid(WallInSet, data.WallType);
        RegisterIfValid(PlatformDictionary, data.PlatformType, data.PlatformIndex);
        RegisterIfValid(WorkbenchDictionary, data.WorkbenchType, data.WorkbenchIndex);
        RegisterIfValid(TableDictionary, data.TableType, data.TableIndex);
        RegisterIfValid(ChairDictionary, data.ChairType, data.ChairIndex);
        RegisterIfValid(DoorClosedDictionary, data.ClosedDoorType, data.DoorIndex);
        RegisterIfValid(DoorOpenDictionary, data.OpenDoorType, data.DoorIndex);
        RegisterIfValid(ChestDictionary, data.ChestType, data.ChestIndex);
        RegisterIfValid(BedDictionary, data.BedType, data.BedIndex);
        RegisterIfValid(BookcaseDictionary, data.BookcaseType, data.BookcaseIndex);
        RegisterIfValid(BathtubDictionary, data.BathtubType, data.BathtubIndex);
        RegisterIfValid(CandelabraDictionary, data.CandelabraType, data.CandelabraIndex);
        RegisterIfValid(CandleDictionary, data.CandleType, data.CandleIndex);
        RegisterIfValid(ChandelierDictionary, data.ChandelierType, data.ChandelierIndex);
        RegisterIfValid(ClockDictionary, data.ClockType, data.ClockIndex);
        RegisterIfValid(DresserDictionary, data.DresserType, data.DresserIndex);
        RegisterIfValid(LampDictionary, data.LampType, data.LampIndex);
        RegisterIfValid(LanternDictionary, data.LanternType, data.LanternIndex);
        RegisterIfValid(PianoDictionary, data.PianoType, data.PianoIndex);
        RegisterIfValid(SinkDictionary, data.SinkType, data.SinkIndex);
        RegisterIfValid(SofaDictionary, data.SofaType, data.SofaIndex);
        RegisterIfValid(ToiletDictionary, data.ToiletType, data.ToiletIndex);
        #endregion

        int rowIndex = FurnitureSets.Count - 1;
        var crossModSolutionProjectile = new SolutionProjectileCrossMod(setName, rowIndex, dustType);
        mod.AddContent(crossModSolutionProjectile);
        var crossModSolutionItem = new SolutionItemCrossMod(mod, setName, TexturePath, setRecipeContent);
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

    public static void SetModFurnitureFrameData(
        ushort tileType,
        FurnitureFrameData data)
    {
        FrameDataDictionary[tileType] = data;
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
}
