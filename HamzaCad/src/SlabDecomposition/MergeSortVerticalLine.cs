using System;
using System.Collections.Generic;
using HamzaCad.Utils;
using HamzaCad.SlabDrawing;


namespace HamzaCad.SlabDecomposition
{
    public class MergeSortVerticalLine
    {
        public static void Sort(List<Line2D> lines)
        {
            SortMerge(lines, 0,lines.Count-1);
        }
        static void SortMerge(List<Line2D> lines, int left, int right)
        {
            int mid;
            if (right > left)
            {
                mid = (right + left) / 2;
                SortMerge(lines, left, mid);
                SortMerge(lines, (mid + 1), right);
                MainMerge(lines, left, (mid + 1), right);
            }
        }
        static void MainMerge(List<Line2D> lines, int left, int mid, int right)
        {
            Line2D[] temp = new Line2D[lines.Count+1];
            int i, eol, num, pos;
            eol = (mid - 1);
            pos = left;
            num = (right - left + 1);

            while ((left <= eol) && (mid <= right))
            {
                if (lines[left].StartPoint.X <= lines[mid].StartPoint.X)
                    temp[pos++] = lines[left++];
                else
                    temp[pos++] = lines[mid++];
            }
            while (left <= eol)
                temp[pos++] = lines[left++];
            while (mid <= right)
                temp[pos++] = lines[mid++];
            for (i = 0; i < num; i++)
            {
                lines[right] = temp[right];
                right--;
            }
        }


    }
}
