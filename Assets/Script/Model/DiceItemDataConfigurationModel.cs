using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using QFramework;
using UnityEngine;

namespace Model
{
    public interface IDiceItemDataConfigurationModel : IModel
    {
        public List<DiceItemData> DataList { get; }
    }
    /// <summary>
    /// 装载在骰子每面上的道具相关的配置信息
    /// </summary> <summary>
    /// 
    /// </summary>
    public class DiceItemDataConfigurationModel : AbstractModel, IDiceItemDataConfigurationModel
    {
        private const string PATH = "/Data/PropertyData/DicePropertyData.xlsx";

        public List<DiceItemData> DataList { get; private set; } = new List<DiceItemData>(20);
        protected override void OnInit()
        {
            FileInfo fileInfo = new FileInfo(Application.dataPath + PATH);
            using (ExcelPackage excel = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet sheet = excel.Workbook.Worksheets[1];
                int row = 2;
                var cells = sheet.Cells;
                while (cells[row, 1].Value != null)
                {
                    DiceItemData data = new DiceItemData();

                    data.name = cells[row, 1].Value.ToString();
                    data.level = int.Parse(cells[row, 2].Value.ToString());
                    data.description = cells[row, 3].Value.ToString();
                    data.purchasePrice = int.Parse(cells[row, 4].Value.ToString());
                    data.purchaseTimes = int.Parse(cells[row, 5].Value.ToString());
                    data.spriteName = cells[row, 6].Value.ToString();
                    data.excuteName = cells[row, 7].Value.ToString();
                    data.sellingPrice = int.Parse(cells[row, 8].Value.ToString());

                    DataList.Add(data);
                    ++row;
                }
            }
        }
    }
    public class DiceItemData
    {
        public string name;
        public int level; //品阶
        public string description;
        public int purchasePrice; //购买价格
        public int purchaseTimes; //可购买次数
        public string spriteName; //AddressableGroup中的名字
        public string excuteName; //触发道具效果时所要执行的代码的所在类的名字
        public int sellingPrice; //出售价格
    }
}