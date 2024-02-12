using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SwordData;

namespace Sword
{
    public class ConfigurationManager
    {
        public void Initialize()
        {
            var roles= ReadConfigDatas();
        }
        public List<IRole> ReadConfigDatas()
        {
            List<IRole> roles = new List<IRole>();
            string filePath = "Resources/config/charicters.xlsx"; // 指定要读取的Excel文件路径

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                XSSFWorkbook workbook = new XSSFWorkbook(fs); // 创建工作簿对象

                foreach (var sheet in workbook) // 遍历每个Sheet页
                {
                    Enum.TryParse(sheet.SheetName,out Camp camp);

                    Console.WriteLine("Sheet Name: " + sheet.SheetName);

                    for (int i = 0; i <= sheet.LastRowNum; i++) // 遍历每一行数据
                    {
                        var row = sheet.GetRow(i);

                        if (row != null) // 判断该行是否为有效行（非空）
                        {
                            for (int j = 0; j < row.LastCellNum; j++) // 遍历当前行的所有单元格
                            {
                                var cellValue = GetCellValue(row.GetCell(j)); // 获取单元格值并转换成字符串类型
                                Console.Write($"Column[{j}]: {cellValue}\t");
                            }
                            if(GetCellValue(row.GetCell(0))!=null&& GetCellValue(row.GetCell(0)) != string.Empty)
                            {
                                Role role = new Role(GetCellValue(row.GetCell(0)));
                                role.Strength =Convert.ToInt32(GetCellValue(row.GetCell(1)));
                                role.Intelligence = Convert.ToInt32(GetCellValue(row.GetCell(2)));
                                role.Defence = Convert.ToInt32(GetCellValue(row.GetCell(3)));
                                role.Skill = Convert.ToInt32(GetCellValue(row.GetCell(4)));
                                role.Critical = Convert.ToInt32(GetCellValue(row.GetCell(5)));
                                Enum.TryParse(GetCellValue(row.GetCell(5)), out Rank rank);
                                role.RoleRank = rank;
                                roles.Add(role);
                            }
                        }
                    }
                }
            }
            return roles;
        }
        private static string GetCellValue(ICell cell)
        {
            switch (cell?.CellType ?? CellType.Blank)
            {
                case CellType.String: return cell.StringCellValue;
                case CellType.Boolean: return Convert.ToString(cell.BooleanCellValue);
                case CellType.Numeric: return Convert.ToString(cell.NumericCellValue);
                default: return "";
            }
        }
    }
}
