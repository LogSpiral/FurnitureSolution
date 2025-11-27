using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FurnitureSolution.Solutions.Core;

public struct FurnitureSetData
{
    /// <summary>
    /// 实体块类型
    /// </summary>
    public short SolidTileType { get; set; }

    /// <summary>
    /// 墙壁类型
    /// </summary>
    public short WallType { get; set; }

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

    /// <summary>
    /// 决定Index语义为PlaceStyle还是TileType<br/>
    /// 原版采用PlaceStyle, 模组家具采用TileType<br/>
    /// 虽然模组物块也可以有不同PlaceStyle但是不便于支持还请理解<br/>
    /// <br/>
    /// English:<br/>
    /// <br/>
    /// Determine the semantic of "Index" is "PlaceStyle" or "TileType"<br/>
    /// PlaceStyle for vanilla while TileType for Mod-Furnitures<br/>
    /// Although Mod-Tiles can have different PlaceStyles, but it is hard to support that.
    /// </summary>
    internal bool IsModFurnitureSet { get; set; }

    /// <summary>
    /// 仅对模组门有用<br/>
    /// 因为原版门开关状态共享索引<br/>
    /// <br/>
    /// English:<br/>
    /// <br/>
    /// Only for modded doors<br/>
    /// Since vanilla open or closed door share the same index.
    /// </summary>
    public short OpenDoorType { get; set; }

    internal static FurnitureSetData FromArray(short[] array)
    {
        return new()
        {
            SolidTileType = array[0],
            WallType = array[1],
            PlatformIndex = array[2],
            WorkbenchIndex = array[3],
            TableIndex = array[4],
            ChairIndex = array[5],
            DoorIndex = array[6],
            OpenDoorType = array[7],
            ChestIndex = array[8],
            BedIndex = array[9],
            BookcaseIndex = array[10],
            BathtubIndex = array[11],
            CandelabraIndex = array[12],
            CandleIndex = array[13],
            ChandelierIndex = array[14],
            ClockIndex = array[15],
            DresserIndex = array[16],
            LampIndex = array[17],
            LanternIndex = array[18],
            PianoIndex = array[19],
            SinkIndex = array[20],
            SofaIndex = array[21],
            ToiletIndex = array[22]
        };
    }
}
