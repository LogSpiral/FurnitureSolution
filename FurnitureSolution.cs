using FurnitureSolution.Solutions.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Terraria.ModLoader;

namespace FurnitureSolution;

// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
public class FurnitureSolution : Mod
{
    public static short[,] FurnitureTable { get; private set; }
    public static FurnitureSetData[] FurnitureSets { get; private set; }


    public override void Load()
    {
        FurnitureTable = new short[43, 22];
        FurnitureSets = new FurnitureSetData[43];
        string furnitureTableText = Encoding.UTF8.GetString(GetFileBytes("FurnitureTable.csv"));
        string[] lines = furnitureTableText.Split('\n');
        int counter = 0;
        static void AddIfVaild(HashSet<short> set, short value)
        {
            if (value != -1) set.Add(value);
        }
        foreach (string line in lines)
        {
            ref FurnitureSetData furniture = ref FurnitureSets[counter];
            string[] contents = line.Replace(new string([(char)65279]),"").Split(','); // ¹µ²ÛµÄBOM
            for (int n = 0; n < 22; n++)
            {
                string current = contents[n];
                if (n == 21)
                    current = current.Replace("\r", "");
                
                var value = (short)int.Parse(current);

                FurnitureTable[counter,n] = value;

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
        base.Load();
    }

    public static HashSet<short> TileInSet { get; } = [];
    public static HashSet<short> WallInSet { get; } = [];
    public static HashSet<short> PlatformInSet { get; } = [];
    public static HashSet<short> WorkbenchInSet { get; } = [];
    public static HashSet<short> TableInSet { get; } = [];
    public static HashSet<short> ChairInSet { get; } = [];
    public static HashSet<short> DoorInSet { get; } = [];
    public static HashSet<short> ChestInSet { get; } = [];
    public static HashSet<short> BedInSet { get; } = [];
    public static HashSet<short> BookcaseInSet { get; } = [];
    public static HashSet<short> BathtubInSet { get; } = [];
    public static HashSet<short> CandelabraInSet { get; } = [];
    public static HashSet<short> CandleInSet { get; } = [];
    public static HashSet<short> ChandelierInSet { get; } = [];
    public static HashSet<short> ClockInSet { get; } = [];
    public static HashSet<short> DresserInSet { get; } = [];
    public static HashSet<short> LampInSet { get; } = [];
    public static HashSet<short> LanternInSet { get; } = [];
    public static HashSet<short> PianoInSet { get; } = [];
    public static HashSet<short> SinkInSet { get; } = [];
    public static HashSet<short> SofaInSet { get; } = [];
    public static HashSet<short> ToiletInSet { get; } = [];

}



