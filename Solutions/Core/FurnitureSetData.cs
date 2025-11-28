using System.Runtime.CompilerServices;

namespace FurnitureSolution.Solutions.Core;

public struct FurnitureSetData
{
    /// <summary>
    /// 实体块类型
    /// </summary>
    public ushort SolidTileType { get; set; }

    /// <summary>
    /// 墙壁类型
    /// </summary>
    public ushort WallType { get; set; }

    /// <summary>
    /// 平台索引
    /// </summary>
    public short PlatformIndex { get; set; }

    /// <summary>
    /// 工作台索引
    /// </summary>
    public short WorkbenchIndex { get; set; }

    /// <summary>
    /// 桌子索引
    /// </summary>
    public short TableIndex { get; set; }

    /// <summary>
    /// 椅子索引
    /// </summary>
    public short ChairIndex { get; set; }

    /// <summary>
    /// 门索引
    /// </summary>
    public short DoorIndex { get; set; }

    /// <summary>
    /// 箱子索引
    /// </summary>
    public short ChestIndex { get; set; }

    /// <summary>
    /// 床索引
    /// </summary>
    public short BedIndex { get; set; }

    /// <summary>
    /// 书架索引
    /// </summary>
    public short BookcaseIndex { get; set; }

    /// <summary>
    /// 浴缸索引
    /// </summary>
    public short BathtubIndex { get; set; }

    /// <summary>
    /// 烛台索引
    /// </summary>
    public short CandelabraIndex { get; set; }

    /// <summary>
    /// 蜡烛索引
    /// </summary>
    public short CandleIndex { get; set; }

    /// <summary>
    /// 吊灯索引
    /// </summary>
    public short ChandelierIndex { get; set; }

    /// <summary>
    /// 钟索引
    /// </summary>
    public short ClockIndex { get; set; }

    /// <summary>
    /// 梳妆台索引
    /// </summary>
    public short DresserIndex { get; set; }

    /// <summary>
    /// 灯索引
    /// </summary>
    public short LampIndex { get; set; }

    /// <summary>
    /// 灯笼索引
    /// </summary>
    public short LanternIndex { get; set; }

    /// <summary>
    /// 钢琴索引
    /// </summary>
    public short PianoIndex { get; set; }

    /// <summary>
    /// 水槽索引
    /// </summary>
    public short SinkIndex { get; set; }

    /// <summary>
    /// 沙发索引
    /// </summary>
    public short SofaIndex { get; set; }

    /// <summary>
    /// 马桶索引
    /// </summary>
    public short ToiletIndex { get; set; }

    public ushort PlatformType { get; set; }

    public ushort WorkbenchType { get; set; }

    public ushort TableType { get; set; }

    public ushort ChairType { get; set; }

    public ushort ClosedDoorType { get; set; }

    public ushort OpenDoorType { get; set; }

    public ushort ChestType { get; set; }

    public ushort BedType { get; set; }

    public ushort BookcaseType { get; set; }

    public ushort BathtubType { get; set; }

    public ushort CandelabraType { get; set; }

    public ushort CandleType { get; set; }

    public ushort ChandelierType { get; set; }

    public ushort ClockType { get; set; }

    public ushort DresserType { get; set; }

    public ushort LampType { get; set; }

    public ushort LanternType { get; set; }

    public ushort PianoType { get; set; }

    public ushort SinkType { get; set; }

    public ushort SofaType { get; set; }

    public ushort ToiletType { get; set; }

    public static FurnitureSetData FromArray(object[] array)
    {
        if (array.Length != 22)
            throw new System.ArgumentException($"Length of array should be 22.");



        var data = new FurnitureSetData();

        {
            if (array[0] is int type)
                data.SolidTileType = type == -1 ? ushort.MaxValue : (ushort)type;
            else
                throw new System.ArgumentException($"Element in index of 0 should be int, but the type is {array[0].GetType()}");
        }
        {
            if (array[1] is int type)
                data.WallType = type == -1 ? ushort.MaxValue : (ushort)type;
            else
                throw new System.ArgumentException($"Element in index of 1 should be int, but the type is {array[0].GetType()}");
        }
        {
            if (array[2] is ITuple tuple && tuple.Length == 2 && tuple[0] is int type && tuple[1] is int style)
            {
                data.PlatformType = type == -1 ? ushort.MaxValue : (ushort)type;
                data.PlatformIndex = (short)style;
            }
            else if (array[2] is int type2)
                data.PlatformType = type2 == -1 ? ushort.MaxValue : (ushort)type2;
            else
                throw new System.ArgumentException($"Element in index of 2 should be int or (int,int), but the type is {array[2].GetType()}");
        }
        {
            if (array[3] is ITuple tuple && tuple.Length == 2 && tuple[0] is int type && tuple[1] is int style)
            {
                data.WorkbenchType = type == -1 ? ushort.MaxValue : (ushort)type;
                data.WorkbenchIndex = (short)style;
            }
            else if (array[3] is int type2)
                data.WorkbenchType = type2 == -1 ? ushort.MaxValue : (ushort)type2;
            else
                throw new System.ArgumentException($"Element in index of 3 should be int or (int,int), but the type is {array[3].GetType()}");
        }
        {
            if (array[4] is ITuple tuple && tuple.Length == 2 && tuple[0] is int type && tuple[1] is int style)
            {
                data.TableType = type == -1 ? ushort.MaxValue : (ushort)type;
                data.TableIndex = (short)style;
            }
            else if (array[4] is int type2)
                data.TableType = type2 == -1 ? ushort.MaxValue : (ushort)type2;
            else
                throw new System.ArgumentException($"Element in index of 4 should be int or (int,int), but the type is {array[4].GetType()}");
        }
        {
            if (array[5] is ITuple tuple && tuple.Length == 2 && tuple[0] is int type && tuple[1] is int style)
            {
                data.ChairType = type == -1 ? ushort.MaxValue : (ushort)type;
                data.ChairIndex = (short)style;
            }
            else if (array[5] is int type2)
                data.ChairType = type2 == -1 ? ushort.MaxValue : (ushort)type2;
            else
                throw new System.ArgumentException($"Element in index of 5 should be int or (int,int), but the type is {array[5].GetType()}");
        }
        {
            if (array[6] is ITuple tuple && tuple.Length is 2 or 3)
            {
                if (tuple[0] is int typeClosed && tuple[1] is int typeOpen)
                {
                    data.ClosedDoorType = typeClosed == -1 ? ushort.MaxValue : (ushort)typeClosed;
                    data.OpenDoorType = typeOpen == -1 ? ushort.MaxValue : (ushort)typeOpen;
                }
                else
                    throw new System.ArgumentException($"Element in index of 6 should be (int,int) or (int,int,int), but the type is {array[6].GetType()}");
                if (tuple.Length == 3) 
                {
                    if (tuple[2] is int style)
                        data.DoorIndex = (short)style;
                    else
                        throw new System.ArgumentException($"Element in index of 6 should be (int,int) or (int,int,int), but the type is {array[6].GetType()}");

                }
            }
            else if (array[6] is int type2)
                data.ClosedDoorType = type2 == -1 ? ushort.MaxValue : (ushort)type2;
            else
                throw new System.ArgumentException($"Element in index of 6 should be (int,int) or (int,int,int), but the type is {array[6].GetType()}");
        }
        {
            if (array[7] is ITuple tuple && tuple.Length == 2 && tuple[0] is int type && tuple[1] is int style)
            {
                data.ChestType = type == -1 ? ushort.MaxValue : (ushort)type;
                data.ChestIndex = (short)style;
            }
            else if (array[7] is int type2)
                data.ChestType = type2 == -1 ? ushort.MaxValue : (ushort)type2;
            else
                throw new System.ArgumentException($"Element in index of 7 should be int or (int,int), but the type is {array[7].GetType()}");
        }
        {
            if (array[8] is ITuple tuple && tuple.Length == 2 && tuple[0] is int type && tuple[1] is int style)
            {
                data.BedType = type == -1 ? ushort.MaxValue : (ushort)type;
                data.BedIndex = (short)style;
            }
            else if (array[8] is int type2)
                data.BedType = type2 == -1 ? ushort.MaxValue : (ushort)type2;
            else
                throw new System.ArgumentException($"Element in index of 8 should be int or (int,int), but the type is {array[8].GetType()}");
        }
        {
            if (array[9] is ITuple tuple && tuple.Length == 2 && tuple[0] is int type && tuple[1] is int style)
            {
                data.BookcaseType = type == -1 ? ushort.MaxValue : (ushort)type;
                data.BookcaseIndex = (short)style;
            }
            else if (array[9] is int type2)
                data.BookcaseType = type2 == -1 ? ushort.MaxValue : (ushort)type2;
            else
                throw new System.ArgumentException($"Element in index of 9 should be int or (int,int), but the type is {array[9].GetType()}");
        }
        {
            if (array[10] is ITuple tuple && tuple.Length == 2 && tuple[0] is int type && tuple[1] is int style)
            {
                data.BathtubType = type == -1 ? ushort.MaxValue : (ushort)type;
                data.BathtubIndex = (short)style;
            }
            else if (array[10] is int type2)
                data.BathtubType = type2 == -1 ? ushort.MaxValue : (ushort)type2;
            else
                throw new System.ArgumentException($"Element in index of 10 should be int or (int,int), but the type is {array[10].GetType()}");
        }
        {
            if (array[11] is ITuple tuple && tuple.Length == 2 && tuple[0] is int type && tuple[1] is int style)
            {
                data.CandelabraType = type == -1 ? ushort.MaxValue : (ushort)type;
                data.CandelabraIndex = (short)style;
            }
            else if (array[11] is int type2)
                data.CandelabraType = type2 == -1 ? ushort.MaxValue : (ushort)type2;
            else
                throw new System.ArgumentException($"Element in index of 11 should be int or (int,int), but the type is {array[11].GetType()}");
        }
        {
            if (array[12] is ITuple tuple && tuple.Length == 2 && tuple[0] is int type && tuple[1] is int style)
            {
                data.CandleType = type == -1 ? ushort.MaxValue : (ushort)type;
                data.CandleIndex = (short)style;
            }
            else if (array[12] is int type2)
                data.CandleType = type2 == -1 ? ushort.MaxValue : (ushort)type2;
            else
                throw new System.ArgumentException($"Element in index of 12 should be int or (int,int), but the type is {array[12].GetType()}");
        }
        {
            if (array[13] is ITuple tuple && tuple.Length == 2 && tuple[0] is int type && tuple[1] is int style)
            {
                data.ChandelierType = type == -1 ? ushort.MaxValue : (ushort)type;
                data.ChandelierIndex = (short)style;
            }
            else if (array[13] is int type2)
                data.ChandelierType = type2 == -1 ? ushort.MaxValue : (ushort)type2;
            else
                throw new System.ArgumentException($"Element in index of 13 should be int or (int,int), but the type is {array[13].GetType()}");
        }
        {
            if (array[14] is ITuple tuple && tuple.Length == 2 && tuple[0] is int type && tuple[1] is int style)
            {
                data.ClockType = type == -1 ? ushort.MaxValue : (ushort)type;
                data.ClockIndex = (short)style;
            }
            else if (array[14] is int type2)
                data.ClockType = type2 == -1 ? ushort.MaxValue : (ushort)type2;
            else
                throw new System.ArgumentException($"Element in index of 14 should be int or (int,int), but the type is {array[14].GetType()}");
        }
        {
            if (array[15] is ITuple tuple && tuple.Length == 2 && tuple[0] is int type && tuple[1] is int style)
            {
                data.DresserType = type == -1 ? ushort.MaxValue : (ushort)type;
                data.DresserIndex = (short)style;
            }
            else if (array[15] is int type2)
                data.DresserType = type2 == -1 ? ushort.MaxValue : (ushort)type2;
            else
                throw new System.ArgumentException($"Element in index of 15 should be int or (int,int), but the type is {array[15].GetType()}");
        }
        {
            if (array[16] is ITuple tuple && tuple.Length == 2 && tuple[0] is int type && tuple[1] is int style)
            {
                data.LampType = type == -1 ? ushort.MaxValue : (ushort)type;
                data.LampIndex = (short)style;
            }
            else if (array[16] is int type2)
                data.LampType = type2 == -1 ? ushort.MaxValue : (ushort)type2;
            else
                throw new System.ArgumentException($"Element in index of 16 should be int or (int,int), but the type is {array[16].GetType()}");
        }
        {
            if (array[17] is ITuple tuple && tuple.Length == 2 && tuple[0] is int type && tuple[1] is int style)
            {
                data.LanternType = type == -1 ? ushort.MaxValue : (ushort)type;
                data.LanternIndex = (short)style;
            }
            else if (array[17] is int type2)
                data.LanternType = type2 == -1 ? ushort.MaxValue : (ushort)type2;
            else
                throw new System.ArgumentException($"Element in index of 17 should be int or (int,int), but the type is {array[17].GetType()}");
        }
        {
            if (array[18] is ITuple tuple && tuple.Length == 2 && tuple[0] is int type && tuple[1] is int style)
            {
                data.PianoType = type == -1 ? ushort.MaxValue : (ushort)type;
                data.PianoIndex = (short)style;
            }
            else if (array[18] is int type2)
                data.PianoType = type2 == -1 ? ushort.MaxValue : (ushort)type2;
            else
                throw new System.ArgumentException($"Element in index of 18 should be int or (int,int), but the type is {array[18].GetType()}");
        }
        {
            if (array[19] is ITuple tuple && tuple.Length == 2 && tuple[0] is int type && tuple[1] is int style)
            {
                data.SinkType = type == -1 ? ushort.MaxValue : (ushort)type;
                data.SinkIndex = (short)style;
            }
            else if (array[19] is int type2)
                data.SinkType = type2 == -1 ? ushort.MaxValue : (ushort)type2;
            else
                throw new System.ArgumentException($"Element in index of 19 should be int or (int,int), but the type is {array[19].GetType()}");
        }
        {
            if (array[20] is ITuple tuple && tuple.Length == 2 && tuple[0] is int type && tuple[1] is int style)
            {
                data.SofaType = type == -1 ? ushort.MaxValue : (ushort)type;
                data.SofaIndex = (short)style;
            }
            else if (array[20] is int type2)
                data.SofaType = type2 == -1 ? ushort.MaxValue : (ushort)type2;
            else
                throw new System.ArgumentException($"Element in index of 20 should be int or (int,int), but the type is {array[20].GetType()}");
        }
        {
            if (array[21] is ITuple tuple && tuple.Length == 2 && tuple[0] is int type && tuple[1] is int style)
            {
                data.ToiletType = type == -1 ? ushort.MaxValue : (ushort)type;
                data.ToiletIndex = (short)style;
            }
            else if (array[21] is int type2)
                data.ToiletType = type2 == -1 ? ushort.MaxValue : (ushort)type2;
            else
                throw new System.ArgumentException($"Element in index of 21 should be int or (int,int), but the type is {array[21].GetType()}");
        }

        return data;

    }
}
