using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using QFramework;
using UnityEngine;

namespace Model
{
    public interface IDicePropertyDataConfigurationModel : IModel
    {
        public List<DicePropertyData> DataList { get; }
    }
    public class DicePropertyDataConfigurationModel : AbstractModel, IDicePropertyDataConfigurationModel
    {
        private const string PATH = "/Data/PropertyData/DicePropertyData.xlsx";

        public List<DicePropertyData> DataList { get; private set; } = new List<DicePropertyData>(20);
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
                    DicePropertyData data = new DicePropertyData();

                    data.name = cells[row, 1].Value.ToString();
                    data.level = int.Parse(cells[row, 2].Value.ToString());
                    data.description = cells[row, 3].Value.ToString();
                    data.purchasePrice = int.Parse(cells[row, 4].Value.ToString());
                    data.purchaseTimes = int.Parse(cells[row, 5].Value.ToString());
                    data.spritePath = cells[row, 6].Value.ToString();
                    data.excuteName = cells[row, 7].Value.ToString();
                    data.sellingPrice = int.Parse(cells[row, 8].Value.ToString());

                    DataList.Add(data);
                    Debug.Log(DataList.Count);
                    ++row;
                }
            }
        }
    }
    public class DicePropertyData
    {
        public string name;
        public int level;
        public string description;
        public int purchasePrice;
        public int purchaseTimes;
        public string spritePath;
        public string excuteName;
        public int sellingPrice;
    }
}