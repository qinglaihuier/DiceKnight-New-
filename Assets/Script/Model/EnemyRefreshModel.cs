using System.Collections.Generic;
using System.IO;
using QFramework;
using UnityEngine;
using System;
using OfficeOpenXml;
namespace Model
{
    public interface IEnemyRefreshModel : IModel
    {
        public BindableProperty<int> WaveCount { get; }
        public EnemyWaveConfigurationInformation GetEnemyWaveInformation();
    }
    public class EnemyWaveConfigurationInformation
    {
        public int waveCount; //第几波
        public int duration; //该敌人刷新段所持续时间间隔
        public string waveBeginningEnemy; //该刷新段一开始所要刷新的敌人
        public string slowlyRefreshWaveEnemy; //该刷新段缓慢刷新的敌人
        public int waveWeight; //该刷新段所对应的敌人权重
    }
    /// <summary>
    /// 敌人一波一波刷新相关的数据
    /// </summary>
    public class EnemyRefreshModel : AbstractModel, IEnemyRefreshModel  
    {
        public BindableProperty<int> WaveCount { get; private set; } = new BindableProperty<int>(); //当前第几波
        private Dictionary<int, EnemyWaveConfigurationInformation> enemyWaveConfigurationInformationDic = new Dictionary<int, EnemyWaveConfigurationInformation>();

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
                    EnemyWaveConfigurationInformation info = new EnemyWaveConfigurationInformation();
                    info.waveCount = int.Parse(cells[row, 1].Value.ToString());
                    info.duration = int.Parse(cells[row, 2].Value.ToString());
                    info.waveBeginningEnemy = cells[row, 3].Value.ToString();
                    info.slowlyRefreshWaveEnemy = cells[row, 4].Value.ToString();
                    info.waveWeight = int.Parse(cells[row, 5].Value.ToString());

                    enemyWaveConfigurationInformationDic.Add(info.waveCount, info);
                    ++row;
                }
            }
        }

        public EnemyWaveConfigurationInformation GetEnemyWaveInformation()
        {
            if(enemyWaveConfigurationInformationDic.TryGetValue(WaveCount, out var info))
            {
                return info;
            }
            return enemyWaveConfigurationInformationDic[enemyWaveConfigurationInformationDic.Count - 1];
        }
    }
}