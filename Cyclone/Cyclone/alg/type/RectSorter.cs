// TypeSet.cpp: implementation of the CTypeset class.
//
//////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Drawing;
using Cyclone.alg;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Cyclone.alg.math;
using Cyclone.alg.util;
namespace Cyclone.alg.type
{
    public class RectSorter
    {
        public List<RectMaterial> m_MaterialList = new List<RectMaterial>();
        public int m_iCurIndex;
        public RectSorter()
        {
            m_iCurIndex = 0;
        }
        public int getUsedSize()
        {
            int size = 0;
            for (int j = 0; j < this.m_MaterialList.Count; j++)
            {
                RectMaterial material = (RectMaterial)m_MaterialList[j];
                int imgW = (int)(material.m_dWidth);
                int imgH = (int)(material.m_dHeight);
                size += imgW * imgH;
            }
            return size;
        }
        private Size getSortedSize()
        {
            int imgW = 1;
            int imgH = 1;
            for (int j = 0; j < this.m_MaterialList.Count; j++)
            {
                RectMaterial material = (RectMaterial)m_MaterialList[j];
                if (imgW < material.m_dX + material.m_dWidth)
                {
                    imgW = (int)(material.m_dX + material.m_dWidth);
                }
                if (imgH < material.m_dY + material.m_dHeight)
                {
                    imgH = (int)(material.m_dY + material.m_dHeight);
                }
            }
            return new Size(imgW, imgH);
        }
        public Size getNeedSize()
        {
            Size realSize = getSortedSize();
            return MathUtil.getMultipleOfTwo(realSize);
        }
        public void Add(RectMaterial pMaterial)
        {
            m_MaterialList.Add(pMaterial);
            m_iCurIndex++;
        }

        //�����㷨(����ת)
        public const int INFINITY = 30000;
        //flag ���ڱ�ʶ�����޵Ļ������޵�. flag=0,��ʾ�߶�������,1��ʾ�и߶�����
        private void sort(int flag, int n, int width, int height, int xleft, int yleft)
        {
            int index = 0;
            if (m_MaterialList.Count <= 0)
            {
                return;
            }
            RectMaterial rectI = ((RectMaterial)m_MaterialList[index]); ;
            if (flag == 0)
            {
                while (index < n)
                {
                    rectI = ((RectMaterial)m_MaterialList[index]);
                    if (rectI.m_bUsed == false && rectI.m_dWidth <= width)
                    {
                        break;
                    }
                    index++;
                }
                if (index >= n)
                {
                    return;
                }
                rectI.m_bUsed = true;	//������Ѿ��Ž�ȥ��.
                rectI.m_dX = xleft;
                rectI.m_dY = yleft;

                sort(1, n, (int)(width - rectI.m_dWidth), (int)(rectI.m_dHeight), (int)(xleft + rectI.m_dWidth), yleft);
                sort(0, n, width, INFINITY, xleft, (int)(yleft + rectI.m_dHeight));
            }
            else if (flag == 1)
            {
                while (index < n)
                {
                    if (index >= m_MaterialList.Count)
                    {
                        Console.WriteLine(index + "/" + m_MaterialList.Count);
                    }
                    rectI = ((RectMaterial)m_MaterialList[index]);
                    if (rectI.m_bUsed == false && rectI.m_dWidth <= width && rectI.m_dHeight <= height)
                    {
                        break;
                    }
                    index++;
                }
                if (index >= n)
                {
                    return;
                }

                rectI.m_bUsed = true;
                rectI.m_dX = xleft;
                rectI.m_dY = yleft;

                sort(1, n, (int)(width - rectI.m_dWidth), (int)(rectI.m_dHeight), (int)(xleft + rectI.m_dWidth), yleft);
                sort(1, n, width, (int)(height - rectI.m_dHeight), xleft, (int)(yleft + rectI.m_dHeight));
            }

        }
        private List<RectMaterial> back_MaterialList = new List<RectMaterial>();
        //������������
        private void GenerateMaterial()
        {
            RectMaterial iter2 = null;
            RectMaterial iter3 = null;
            //��m_MaterialList�е�Ԫ�شӴ�С����
            while (m_MaterialList.Count != 0)
            {
                double dMaxHeight = 0;//��ѡ���߶�
                double dMaxHeightW = 0;//���߶ȵ�Ԫ�صĿ��
                iter2 = m_MaterialList[0];
                RectMaterial p = null;
                iter3 = iter2;
                while (iter2 != null)
                {
                    if (iter2.m_dHeight > dMaxHeight)
                    {
                        p = iter2;
                        iter3 = iter2;
                        dMaxHeight = iter2.m_dHeight;
                        dMaxHeightW = iter2.m_dWidth;
                    }
                    else if (iter2.m_dHeight == dMaxHeight)
                    {
                        if (iter2.m_dWidth > dMaxHeightW)
                        {
                            p = iter2;
                            iter3 = iter2;
                            dMaxHeight = iter2.m_dHeight;
                            dMaxHeightW = iter2.m_dWidth;
                        }
                    }
                    if (m_MaterialList.IndexOf(iter2) >= m_MaterialList.Count - 1)
                    {
                        iter2 = null;
                    }
                    else
                    {
                        iter2 = m_MaterialList[m_MaterialList.IndexOf(iter2) + 1];
                    }
                }

                if (p != null)
                {
                    back_MaterialList.Add(p);
                    m_MaterialList.Remove(iter3);
                }
            }
        }
        public void StartSort()
        {
            int keepWidth = 0;//������ȣ�������Ƭ�����Ŀ��
            //���㱣���ߴ�
            for (int i = 0; i < m_MaterialList.Count; i++)
            {
                OptmizeClip baseClipElement = m_MaterialList[i].optmizeClip;
                if (keepWidth < baseClipElement.clipElement.clipRect.Width)
                {
                    keepWidth = baseClipElement.clipElement.clipRect.Width;
                }
            }
            keepWidth = MathUtil.getMultipleOfTwo(keepWidth);
            GenerateMaterial();
            Size minSize = new Size(short.MaxValue, short.MaxValue);
            int keepWidthBak = keepWidth;
            //int id = 0;
            while (true)
            {
                copyMaterial();
                //����һ������
                sort(0, m_MaterialList.Count, keepWidth, INFINITY, 0, 0);
                //��ȡ�����������
                Size newSize = getNeedSize();
                //�Ƚ����ν��
                bool replace = false;
                if (newSize.Width * newSize.Height < minSize.Width * minSize.Height)
                {
                    replace = true;
                }
                if (newSize.Width * newSize.Height == minSize.Width * minSize.Height)
                {
                    if (Math.Max(newSize.Width, newSize.Height) < Math.Max(minSize.Width, minSize.Height))
                    {
                        replace = true;
                    }
                }
                if (replace)
                {
                    minSize = newSize;
                    keepWidthBak = keepWidth;
                }
                //�������ɵ�ͼƬ
                ////����ͼƬ
                //Image imgExort = new Bitmap(newSize.Width, newSize.Height);
                //Graphics g = Graphics.FromImage(imgExort);
                //for (int j = 0; j < m_MaterialList.Count; j++)
                //{
                //    RectMaterial material = m_MaterialList[j];
                //    GraphicsUtil.drawClip(g, material.optmizeClip.clipElement.imageElement.image, (int)material.m_dX, (int)material.m_dY,
                //        material.optmizeClip.clipElement.clipRect.X, material.optmizeClip.clipElement.clipRect.Y, material.optmizeClip.clipElement.clipRect.Width,
                //        material.optmizeClip.clipElement.clipRect.Height, Consts.TRANS_NONE);
                //}
                //imgExort.Save(@"E:\project\EngineProject\CycloneEditor\test\test\testGen" + id + ".png");
                //id++;
                if (keepWidth >= 2048)
                {
                    break;
                }
                keepWidth *= 2;
            }
            //ʹ�����һ����������һ��
            if (keepWidthBak != keepWidth)
            {
                copyMaterial();
                //����һ������
                sort(0, m_MaterialList.Count, keepWidthBak, INFINITY, 0, 0);
            }
        }
        //��������
        private void copyMaterial()
        {
            m_MaterialList.Clear();
            for (int i = 0; i < back_MaterialList.Count; i++)
            {
                RectMaterial rect = (RectMaterial)back_MaterialList[i];
                rect.m_bUsed = false;
                m_MaterialList.Add(rect);
            }
        }


    }
}






