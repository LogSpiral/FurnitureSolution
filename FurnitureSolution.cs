using FurnitureSolution.Solutions.Core;
using System.Collections.Generic;
using System.Text;
using Terraria.ID;
using Terraria.ModLoader;

namespace FurnitureSolution;

public partial class FurnitureSolution : Mod
{
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

    public override void Unload() => CleanUp();

    private void InitializeFurnitureData()
    {
        // FurnitureTable = new short[43, 22];
        var vanillaFurnitureSets = new FurnitureSetData[43];
        string furnitureTableText = Encoding.UTF8.GetString(GetFileBytes("FurnitureTable.csv"));
        string[] lines = furnitureTableText.Split('\n');
        int counter = 0;
        static void AddIfValid(HashSet<ushort> set, short value)
        {
            if (value != -1) set.Add((ushort)value);
        }
        static void RegisterIfValid(Dictionary<ushort, HashSet<short>> dictionary, ushort key, short style)
        {
            if (style == -1) return;
            if (!dictionary.TryGetValue(key, out var set))
            {
                set = [];
                dictionary.Add(key, set);
            }
            set.Add(style);
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

                // FurnitureTable[counter, n] = value;

                switch (n)
                {
                    case 0:
                        furniture.SolidTileType = value < 0 ? ushort.MaxValue : (ushort)value;
                        AddIfValid(TileInSet, value);
                        break;
                    case 1:
                        furniture.WallType = value < 0 ? ushort.MaxValue : (ushort)value;
                        AddIfValid(WallInSet, value);
                        break;
                    case 2:
                        furniture.PlatformIndex = value;
                        furniture.PlatformType = TileID.Platforms;
                        RegisterIfValid(PlatformDictionary, TileID.Platforms, value);
                        break;
                    case 3:
                        furniture.WorkbenchIndex = value;
                        furniture.WorkbenchType = TileID.WorkBenches;
                        RegisterIfValid(WorkbenchDictionary, TileID.WorkBenches, value);
                        break;
                    case 4:
                        ushort type = TileID.Tables;
                        if (value >= 100)
                        {
                            value -= 100;
                            type = TileID.Tables2;
                        }
                        furniture.TableIndex = value;
                        furniture.TableType = type;
                        RegisterIfValid(TableDictionary, type, value);
                        break;
                    case 5:
                        furniture.ChairIndex = value;
                        furniture.ChairType = TileID.Chairs;
                        RegisterIfValid(ChairDictionary, TileID.Chairs, value);
                        break;
                    case 6:
                        furniture.DoorIndex = value;
                        furniture.ClosedDoorType = TileID.ClosedDoor;
                        furniture.OpenDoorType = TileID.OpenDoor;
                        RegisterIfValid(DoorClosedDictionary, TileID.ClosedDoor, value);
                        RegisterIfValid(DoorOpenDictionary, TileID.OpenDoor, value);
                        break;
                    case 7:
                        type = TileID.Containers;
                        if (value >= 100)
                        {
                            value -= 100;
                            type = TileID.Containers2;
                        }
                        furniture.ChestIndex = value;
                        furniture.ChestType = type;
                        RegisterIfValid(ChestDictionary, type, value);
                        break;
                    case 8:
                        furniture.BedIndex = value;
                        furniture.BedType = TileID.Beds;
                        RegisterIfValid(BedDictionary, TileID.Beds, value);
                        break;
                    case 9:
                        furniture.BookcaseIndex = value;
                        furniture.BookcaseType = TileID.Bookcases;
                        RegisterIfValid(BookcaseDictionary, TileID.Bookcases, value);
                        break;
                    case 10:
                        furniture.BathtubIndex = value;
                        furniture.BathtubType = TileID.Bathtubs;
                        RegisterIfValid(BathtubDictionary, TileID.Bathtubs, value);
                        break;
                    case 11:
                        furniture.CandelabraIndex = value;
                        furniture.CandelabraType = TileID.Candelabras;
                        RegisterIfValid(CandelabraDictionary, TileID.Candelabras, value);
                        break;
                    case 12:
                        furniture.CandleIndex = value;
                        furniture.CandleType = TileID.Candles;
                        RegisterIfValid(CandleDictionary, TileID.Candles, value);
                        break;
                    case 13:
                        furniture.ChandelierIndex = value;
                        furniture.ChandelierType = TileID.Chandeliers;
                        RegisterIfValid(ChandelierDictionary,TileID.Chandeliers, value);
                        break;
                    case 14:
                        furniture.ClockIndex = value;
                        furniture.ClockType = TileID.GrandfatherClocks;
                        RegisterIfValid(ClockDictionary, TileID.GrandfatherClocks, value);
                        break;
                    case 15:
                        furniture.DresserIndex = value;
                        furniture.DresserType = TileID.Dressers;
                        RegisterIfValid(DresserDictionary, TileID.Dressers, value);
                        break;
                    case 16:
                        furniture.LampIndex = value;
                        furniture.LampType = TileID.Lamps;
                        RegisterIfValid(LampDictionary, TileID.Lamps, value);
                        break;
                    case 17:
                        furniture.LanternIndex = value;
                        furniture.LanternType = TileID.HangingLanterns;
                        RegisterIfValid(LanternDictionary, TileID.HangingLanterns, value);
                        break;
                    case 18:
                        furniture.PianoIndex = value;
                        furniture.PianoType = TileID.Pianos;
                        RegisterIfValid(PianoDictionary, TileID.Pianos, value);
                        break;
                    case 19:
                        furniture.SinkIndex = value;
                        furniture.SinkType = TileID.Sinks;
                        RegisterIfValid(SinkDictionary, TileID.Sinks, value);
                        break;
                    case 20:
                        furniture.SofaIndex = value;
                        furniture.SofaType = TileID.Benches;
                        RegisterIfValid(SofaDictionary, TileID.Benches, value);
                        break;
                    case 21:
                        type = TileID.Toilets;
                        if (value >= 100)
                        {
                            value -= 100;
                            type = TileID.Chairs;
                        }
                        furniture.ToiletIndex = value;
                        furniture.ToiletType = type;
                        RegisterIfValid(ToiletDictionary, type, value);
                        break;
                }
            }
            counter++;
        }

        FurnitureSets = [.. vanillaFurnitureSets];
    }

    private static void CleanUp()
    {
        FurnitureSets.Clear();
        FurnitureSetIndexDictionary.Clear();
        Instance = null;

        TileInSet.Clear();
        WallInSet.Clear();
        PlatformDictionary.Clear();
        WorkbenchDictionary.Clear();
        TableDictionary.Clear();
        ChairDictionary.Clear();
        DoorClosedDictionary.Clear();
        DoorOpenDictionary.Clear();
        ChestDictionary.Clear();
        BedDictionary.Clear();
        BookcaseDictionary.Clear();
        BathtubDictionary.Clear();
        CandelabraDictionary.Clear();
        CandleDictionary.Clear();
        ChandelierDictionary.Clear();
        ClockDictionary.Clear();
        DresserDictionary.Clear();
        LampDictionary.Clear();
        LanternDictionary.Clear();
        PianoDictionary.Clear();
        SinkDictionary.Clear();
        ToiletDictionary.Clear();
    }

    internal static HashSet<ushort> TileInSet { get; } = [];
    internal static HashSet<ushort> WallInSet { get; } = [];
    internal static Dictionary<ushort, HashSet<short>> PlatformDictionary { get; } = [];
    internal static Dictionary<ushort, HashSet<short>> WorkbenchDictionary { get; } = [];
    internal static Dictionary<ushort, HashSet<short>> TableDictionary { get; } = [];
    internal static Dictionary<ushort, HashSet<short>> ChairDictionary { get; } = [];
    internal static Dictionary<ushort, HashSet<short>> DoorClosedDictionary { get; } = [];
    internal static Dictionary<ushort, HashSet<short>> DoorOpenDictionary { get; } = [];
    internal static Dictionary<ushort, HashSet<short>> ChestDictionary { get; } = [];
    internal static Dictionary<ushort, HashSet<short>> BedDictionary { get; } = [];
    internal static Dictionary<ushort, HashSet<short>> BookcaseDictionary { get; } = [];
    internal static Dictionary<ushort, HashSet<short>> BathtubDictionary { get; } = [];
    internal static Dictionary<ushort, HashSet<short>> CandelabraDictionary { get; } = [];
    internal static Dictionary<ushort, HashSet<short>> CandleDictionary { get; } = [];
    internal static Dictionary<ushort, HashSet<short>> ChandelierDictionary { get; } = [];
    internal static Dictionary<ushort, HashSet<short>> ClockDictionary { get; } = [];
    internal static Dictionary<ushort, HashSet<short>> DresserDictionary { get; } = [];
    internal static Dictionary<ushort, HashSet<short>> LampDictionary { get; } = [];
    internal static Dictionary<ushort, HashSet<short>> LanternDictionary { get; } = [];
    internal static Dictionary<ushort, HashSet<short>> PianoDictionary { get; } = [];
    internal static Dictionary<ushort, HashSet<short>> SinkDictionary { get; } = [];
    internal static Dictionary<ushort, HashSet<short>> SofaDictionary { get; } = [];
    internal static Dictionary<ushort, HashSet<short>> ToiletDictionary { get; } = [];
    internal static Dictionary<ushort, FurnitureFrameData> FrameDataDictionary { get; } = [];
}