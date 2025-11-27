using FurnitureSolution.Solutions.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FurnitureSolution;

public partial class FurnitureSolution : Mod
{
    internal static short[,] FurnitureTable { get; private set; }
    internal static List<FurnitureSetData> FurnitureSets { get; private set; }
    internal static Dictionary<string, int> FurnitureSetIndexDictionary { get; private set; } = [];
    public static FurnitureSolution Instance { get; private set; }
    public override void Load()
    {
        InitializeFurnitureData();
        if (ModLoader.TryGetMod("TouhouPets", out var touhouPets))
            RegisterYukaSolution(touhouPets);

        Instance = this;
        base.Load();
    }

    private void InitializeFurnitureData()
    {
        FurnitureTable = new short[43, 22];
        var vanillaFurnitureSets = new FurnitureSetData[43];
        string furnitureTableText = Encoding.UTF8.GetString(GetFileBytes("FurnitureTable.csv"));
        string[] lines = furnitureTableText.Split('\n');
        int counter = 0;
        static void AddIfVaild(HashSet<short> set, short value)
        {
            if (value != -1) set.Add(value);
        }
        foreach (string line in lines)
        {
            ref FurnitureSetData furniture = ref vanillaFurnitureSets[counter];
            string[] contents = line.Replace(new string([(char)65279]), "").Split(','); // ¹µ²ÛµÄBOM
            for (int n = 0; n < 22; n++)
            {
                string current = contents[n];
                if (n == 21)
                    current = current.Replace("\r", "");

                var value = (short)int.Parse(current);

                FurnitureTable[counter, n] = value;

                switch (n)
                {
                    case 0:
                        furniture.SolidTileType = value;
                        AddIfVaild(TileInSet, value);
                        break;
                    case 1:
                        furniture.WallType = value;
                        AddIfVaild(WallInSet, value);
                        break;
                    case 2:
                        furniture.PlatformIndex = value;
                        AddIfVaild(PlatformInSet, value);
                        break;
                    case 3:
                        furniture.WorkbenchIndex = value;
                        AddIfVaild(WorkbenchInSet, value);
                        break;
                    case 4:
                        furniture.TableIndex = value;
                        AddIfVaild(TableInSet, value);
                        break;
                    case 5:
                        furniture.ChairIndex = value;
                        AddIfVaild(ChairInSet, value);
                        break;
                    case 6:
                        furniture.DoorIndex = value;
                        AddIfVaild(DoorInSet, value);
                        break;
                    case 7:
                        furniture.ChestIndex = value;
                        AddIfVaild(ChestInSet, value);
                        break;
                    case 8:
                        furniture.BedIndex = value;
                        AddIfVaild(BedInSet, value);
                        break;
                    case 9:
                        furniture.BookcaseIndex = value;
                        AddIfVaild(BookcaseInSet, value);
                        break;
                    case 10:
                        furniture.BathtubIndex = value;
                        AddIfVaild(BathtubInSet, value);
                        break;
                    case 11:
                        furniture.CandelabraIndex = value;
                        AddIfVaild(CandelabraInSet, value);
                        break;
                    case 12:
                        furniture.CandleIndex = value;
                        AddIfVaild(CandleInSet, value);
                        break;
                    case 13:
                        furniture.ChandelierIndex = value;
                        AddIfVaild(ChandelierInSet, value);
                        break;
                    case 14:
                        furniture.ClockIndex = value;
                        AddIfVaild(ClockInSet, value);
                        break;
                    case 15:
                        furniture.DresserIndex = value;
                        AddIfVaild(DresserInSet, value);
                        break;
                    case 16:
                        furniture.LampIndex = value;
                        AddIfVaild(LampInSet, value);
                        break;
                    case 17:
                        furniture.LanternIndex = value;
                        AddIfVaild(LanternInSet, value);
                        break;
                    case 18:
                        furniture.PianoIndex = value;
                        AddIfVaild(PianoInSet, value);
                        break;
                    case 19:
                        furniture.SinkIndex = value;
                        AddIfVaild(SinkInSet, value);
                        break;
                    case 20:
                        furniture.SofaIndex = value;
                        AddIfVaild(SofaInSet, value);
                        break;
                    case 21:
                        furniture.ToiletIndex = value;
                        AddIfVaild(ToiletInSet, value);
                        break;
                }
            }
            counter++;
        }

        FurnitureSets = [.. vanillaFurnitureSets];
    }

    internal static HashSet<short> TileInSet { get; } = [];
    internal static HashSet<short> WallInSet { get; } = [];
    internal static HashSet<short> PlatformInSet { get; } = [];
    internal static HashSet<short> WorkbenchInSet { get; } = [];
    internal static HashSet<short> TableInSet { get; } = [];
    internal static HashSet<short> ChairInSet { get; } = [];
    internal static HashSet<short> DoorInSet { get; } = [];
    internal static HashSet<short> ChestInSet { get; } = [];
    internal static HashSet<short> BedInSet { get; } = [];
    internal static HashSet<short> BookcaseInSet { get; } = [];
    internal static HashSet<short> BathtubInSet { get; } = [];
    internal static HashSet<short> CandelabraInSet { get; } = [];
    internal static HashSet<short> CandleInSet { get; } = [];
    internal static HashSet<short> ChandelierInSet { get; } = [];
    internal static HashSet<short> ClockInSet { get; } = [];
    internal static HashSet<short> DresserInSet { get; } = [];
    internal static HashSet<short> LampInSet { get; } = [];
    internal static HashSet<short> LanternInSet { get; } = [];
    internal static HashSet<short> PianoInSet { get; } = [];
    internal static HashSet<short> SinkInSet { get; } = [];
    internal static HashSet<short> SofaInSet { get; } = [];
    internal static HashSet<short> ToiletInSet { get; } = [];

}

#if DEBUG
public class ViewPlayer : ModPlayer 
{
    public override void ResetEffects()
    {
        
        if (Player.HeldItem.type == ItemID.None) 
        {
            var tile = Framing.GetTileSafely(Main.MouseWorld.ToTileCoordinates());
            Main.NewText(tile);
        }
    }
}
#endif
