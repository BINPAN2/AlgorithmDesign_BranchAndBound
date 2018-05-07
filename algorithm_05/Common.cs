using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

public class Common
{

    /**
     * 读取txt数据文件，转化为字符串
     * */
    public static string readDat(String filepath)
    {
        string content = "";
        StreamReader sr = new StreamReader(filepath, Encoding.Default);
        string line = null;
        while ((line = sr.ReadLine()) != null)
        {
            content += "\n" + line;
        }
        sr.Close();
        if (content.StartsWith("\n"))
        {
            content = content.Substring(1);
        }

        return content;
    }

    /**
 * 将文件中的内容转换为矩阵的形式。
 * @param str
 * @return
 */
    public static int[,] getMatrix(String str)
    {
        string[] people = str.Split('\n');
        int[,] result = new int[people.Length, people.Length];

        for (int i = 0; i < people.Length; i++)
        {
            string[] works = people[i].Split(',');
            for (int j = 0; j < works.Length; j++)
            {
                result[i, j] = int.Parse(works[j]);
            }
        }

        return result;
    }


}
