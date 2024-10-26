using System.Collections.Generic;
using System.IO;
using QFramework;
using UnityEngine;
using System;
using OfficeOpenXml;
namespace Model
{
    public interface IAllWaveInformationModel : IModel
    {
        public BindableProperty<int> WaveCount { get; }
        public WaveConfigurationInformation GetWaveInformation();
    }
    public class WaveConfigurationInformation
    {
        public int waveCount;
        public int duration;
        public string waveEnemy;
        public string slowlyRefreshWaveEnemy;
        public int waveWeight;
    }
    public class AllWaveInformationModel : AbstractModel, IAllWaveInformationModel
    {
        public BindableProperty<int> WaveCount { get; private set; } = new BindableProperty<int>();
        private Dictionary<int, WaveConfigurationInformation> waveConfigurationInformationDic = new Dictionary<int, WaveConfigurationInformation>();

        private const string PATH = "/Data/Enemy/EnemyWaveInformation.xlsx";
        protected override void OnInit()
        {
            FileInfo fileInfo = new FileInfo(Application.dataPath + PATH);
            using (ExcelPackage excel = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet sheet = excel.Workbook.Worksheets[1];
                var cells = sheet.Cells;
                int row = 2;
                while(cells[row, 1].Value != null)
                {
                    WaveConfigurationInformation info = new WaveConfigurationInformation();
                    info.waveCount = int.Parse(cells[row, 1].Value.ToString());
                    info.duration = int.Parse(cells[row, 2].Value.ToString());
                    info.waveEnemy = cells[row, 3].Value.ToString();
                    info.slowlyRefreshWaveEnemy = cells[row, 4].Value.ToString();
                    info.waveWeight = int.Parse(cells[row, 5].Value.ToString());

                    waveConfigurationInformationDic.Add(info.waveCount, info);
                    ++row;
                }
            }
        }

        public WaveConfigurationInformation GetWaveInformation()
        {
            if(waveConfigurationInformationDic.TryGetValue(WaveCount, out var info))
            {
                return info;
            }
            return waveConfigurationInformationDic[waveConfigurationInformationDic.Count - 1];
        }
    }
}