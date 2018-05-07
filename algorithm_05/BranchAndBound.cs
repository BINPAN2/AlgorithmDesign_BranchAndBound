using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * 分支限界法
 * 
 * */

public class BranchAndBound
{
    private int[,] table = null;//[人员][任务]
    private int[] member = null;//人员分配表
    private int minTime = 10000;//最小用时
    private Node result;//计算结果
    private int upper = 0;//计算上界


    /**
 * 分支限界计算最短时间
 */
    public void branchAndBound()
    {
        List<int> mission = new List<int>();
        List<int> member = new List<int>();
        List<Node> queue = new List<Node>();

        //初始化任务列表
        for (int i = 0; i < table.GetLength(0); i++)
        {
            mission.Add(i);
            member.Add(i);
        }

        upper = getUpper(mission, member);
        Node root = new Node(0, "", 0, 0, member);
        queue.Add(root);
        result = back(queue);
    }

    /**
	 * 利用分支限界获得结果
	 */
    private Node back(List<Node> queue)
    {
        Node head = queue[0];
        if (head.mission == table.GetLength(1))
        {
            head.result = head.result.Substring(1);
            return head;
        }

        foreach (int i in head.otherMembers)
        {
            //计算下界
            List<int> members = new List<int>();
            members.AddRange(head.otherMembers);
            members.Remove(i);
            int currTime = 0;
            if (head.mission < 0)
            {
                currTime = head.currTime;
            }
            else
            {
                currTime = head.currTime + table[i, head.mission];
            }
            minTime = getMinTime(head.mission + 1, members) + currTime;
            if (minTime <= upper)
            {//剪枝，去除超出上界的节点
                Node node = new Node(head.mission + 1, head.result + "-" + i, currTime, minTime, members);
                queue.Add(node);
            }
        }
        queue.RemoveAt(0);
        queue.Sort((i, j) => { return i.lower - j.lower; });
        return back(queue);
    }


    /**
 * 获取当前状态最短时间总和
 * @param mission 剩余任务
 * @param member  剩余人员
 * @return
 */
    private int getMinTime(int startMission, List<int> member)
    {
        int result = 0;

        for (int i = startMission; i < table.GetLength(1); i++)
        {
            int min = 10000;

            foreach (int j in member)
            {
                if (min > table[j, i])
                {
                    min = table[j, i];
                }
            }
            result += min;
        }

        return result;
    }


/**
 * 通过贪心算法获取一个节点的上界
 */
    private int getUpper(List<int> mission, List<int> member)
    {
        List<int> mem = new List<int>();
        mem.AddRange(member);
        List<int> mis = new List<int>();
        mis.AddRange(mission);
        return getUpperMinTime(mis, mem, 0);
    }

    /**
	 * 贪心算法获取最短时间总和
	 * @param mission 剩余任务
	 * @param member  剩余人员
	 * @param result  最短时间总和
	 * @return
	 */
    private int getUpperMinTime(List<int> mission, List<int> member, int result)
    {
        if (mission.Count == 0)
        {
            return result;
        }

        int minTime = 10000;
        int minMission = 0;
        int minMember = 0;


        foreach (int i in member)
        {//找到整个表中最小的
            foreach (int j in mission)
            {//找到每个人员在任务上最少的用时
                if (minTime > table[i, j])
                {
                    minTime = table[i, j];
                    minMission = j;
                    minMember = i;
                }
            }
        }

        mission.Remove(minMission);
        member.Remove(minMember);
        return getUpperMinTime(mission, member, result + minTime);
    }

    /**
      * 获取table
      */
    public void setTable(int[,] table)
    {
        this.table = table;
        member = new int[table.GetLength(0)];
        for (int i = 0; i < table.GetLength(0); i++)
        {
            member[i] = i;
        }
    }

    /**
	 * 格式化输出数据
	 */
    public void display()
    {

        Console.WriteLine("最短用时：" + result.currTime);
        string[] members = result.result.Split('-');
        for (int j = 0; j < table.GetLength(1); j++)
        {
            Console.WriteLine("任务" + (j + 1) + "分配给人员" + (int.Parse(members[j]) + 1) + ",用时" + table[int.Parse(members[j]), j]);
        }
    }




}

public class Node
{
    public int currTime;//当前已用时间
    public int mission;//当前进行的任务
    public string result;//当前的已经分配的结果
    public int lower;//当前节点可能的最短时间，用于排序剪枝
    public List<int> otherMembers;//当前节点还没有被分配任务的人员

    public Node(int mission, String result, int currTime, int lower, List<int> otherMembers)
    {
        this.result = result;
        this.mission = mission;
        this.currTime = currTime;
        this.lower = lower;
        this.otherMembers = new List<int>();
        this.otherMembers.AddRange(otherMembers);
    }
}