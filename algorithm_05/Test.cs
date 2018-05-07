using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * 测试类
 */

public class Test
{
    static void Main(string[] args)
    {
        for (int i = 1; i < 6; i++)
        {
            BranchAndBound BranchAndBound = new BranchAndBound();
            int[,] table = Common.getMatrix(Common.readDat("data/input_assign05_0" + i + ".txt"));
            Console.WriteLine("第" + i + "组数据结果为：");
            BranchAndBound.setTable(table);
            BranchAndBound.branchAndBound();
            BranchAndBound.display();
            Console.WriteLine("\n");
        }
        Console.ReadKey();
    }
}
