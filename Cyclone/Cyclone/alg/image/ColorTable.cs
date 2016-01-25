using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using Cyclone.alg;
using System.IO;
using Cyclone.mod;
using Cyclone.alg.math;
using Cyclone.alg.util;

namespace Cyclone.alg.image
{
    public class ColorTable
    {
        private ArrayList colorList = new ArrayList();//颜色表
        PictureBox picBox = null;
        private const int FRAME_SIZE=12;
        //注册图片框
        public void registerPicBox(PictureBox picBoxT)
        {
            this.picBox = picBoxT;
        }
        //从PNG调色板添加颜色
        public void updateFromPal(byte[] palData,int transID)
        {
            colorList.Clear();
            if(palData==null)
            {
                return;
            }
            int colorCount=(palData.Length - 4)/3;
            int colorData=0;
            for (int i = 0; i < colorCount; i++)
            {
                if (i == transID)
                {
                    colorData = 0xFFFFFF;
                }
                else
                {
                    colorData = (0xFF << 24) | (palData[i * 3] << 16) | (palData[i * 3 + 1] << 8) | (palData[i * 3 + 2]);
                }
                colorList.Add(colorData);
            }
        }
        //添加颜色
        public void addColor(int color)
        {
            if (searchColor(color).Length > 0)
            {
                return;
            }
            colorList.Add(color);
        }
        //返回颜色
        public int getColor(int id)
        {
            if (id < 0 || id >= colorList.Count)
            {
                return 0xFFFFFF;
            }
            else
            {
                int color = (int)colorList[id];
                return color;
            }

        }
        //拷贝
        public void copyTo(ColorTable otherTable)
        {
            if (otherTable != null)
            {
                otherTable.colorList.Clear();
                for (int i = 0; i < colorList.Count; i++)
                {
                    int color = (int)colorList[i];
                    otherTable.colorList.Add(color);
                }
            }
        }
        //返回颜色长度
        public int getColorCount()
        {
            return colorList.Count;
        }
        //设置颜色
        public void setColor(int id,int color)
        {
            if (id < 0 || id >= colorList.Count)
            {
                return;
            }
            colorList[id] = color;
        }
        //寻找颜色
        public int[] searchColor(int color)
        {
            ArrayList idsArray = new ArrayList();
            for (int i = 0; i < colorList.Count; i++)
            {
                int colorI = (int)colorList[i];
                if (colorI == color)
                {
                    idsArray.Add(i);
                }
            }
            int[] ids = new int[idsArray.Count];
            for(int i=0;i<idsArray.Count;i++)
            {
                ids[i] = (int)idsArray[i];
            }
            return ids;
        }
        //删除所有颜色
        public void removeAllColors()
        {
            colorList.Clear();
        }
        //刷新UI
        public Bitmap bitmap = null;
        public void updateUI(ArrayList selectedIDs)
        {
            if (picBox == null)
            {
                return;
            }
            if (bitmap == null || bitmap.Width != picBox.Width)
            {
                bitmap = new Bitmap(picBox.Width, picBox.Height);
            }
            //绘制
            Graphics g = Graphics.FromImage(bitmap);
            //背景
            GraphicsUtil.fillRect(g, 0, 0, picBox.Width, picBox.Height, 0xECE9D8);
            //颜色数据
            int sizePerLine = picBox.Width / FRAME_SIZE;
            int x = 0;
            int y = 0;
            int count = 0;
            for (int i = 0; i < colorList.Count; i++)
            {
                GraphicsUtil.drawRect(g, x, y, FRAME_SIZE+1, FRAME_SIZE+1, 0x0);
                int alpha = ((int)colorList[i] >> 24) & 0xFF;
                if (alpha == 0)
                {
                    GraphicsUtil.fillRect(g, x + 1, y + 1, FRAME_SIZE - 1, FRAME_SIZE - 1, 0xFFFFFF);
                    int TinySize = 3;
                    for (int j = 0; j < 3; j++)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            GraphicsUtil.fillRect(g, x + 2 + k * TinySize, y + 2 + j * TinySize, TinySize, TinySize, (j * 3 + k) % 2 == 0 ? 0xA0A0A3 : 0xFFFFFF);
                        }
                    }
                }
                else
                {
                    GraphicsUtil.fillRect(g, x+1, y+1, FRAME_SIZE - 1, FRAME_SIZE - 1, (int)colorList[i]);
                }
                //焦点
                if (selectedIDs != null && selectedIDs.Contains(i))
                {
                    GraphicsUtil.drawRect(g, x + 1, y + 1, FRAME_SIZE - 1, FRAME_SIZE - 1, 0xFFFFFF);
                    GraphicsUtil.drawRect(g, x + 2, y + 2, FRAME_SIZE - 3, FRAME_SIZE - 3, 0);
                }
                //转移到下一个颜色单元
                x += FRAME_SIZE;
                count++;
                if (count >= sizePerLine)
                {
                    count %= sizePerLine;
                    x = 0;
                    y += FRAME_SIZE;
                }
            }
            //边框
            GraphicsUtil.drawLine(g, 0, 0, 0, picBox.Height - 1, 0, 1);
            GraphicsUtil.drawLine(g, 0, 0, picBox.Width - 1, 0, 0, 1);
            GraphicsUtil.drawLine(g, 0, picBox.Height - 1, picBox.Width - 1, picBox.Height - 1, 0, 1);
            GraphicsUtil.drawLine(g, picBox.Width - 1, 0, picBox.Width - 1, picBox.Height - 1, 0, 1);
            g.Dispose();
            g = null;
            //刷上屏幕
            if (picBox.Image == null || !picBox.Image.Equals(bitmap))
            {
                picBox.Image = bitmap;
            }
            else
            {
                picBox.Refresh();
            }
        }
        //点击事件
        public int getIDByPosition(int x, int y)
        {
            int sizePerLine = picBox.Width / FRAME_SIZE;
            int id = (y / FRAME_SIZE) * sizePerLine + x / FRAME_SIZE;
            if (id >= this.colorList.Count)
            {
                id = -1;
            }
            return id;
        }

        public void readObject(FileStream s,bool lastTrans)
        {
            colorList.Clear();
            short len = IOUtil.readShort(s);
            int color = 0;
            for (int i = 0; i < len; i++)
            {
                color = IOUtil.readInt(s);
                colorList.Add(color);
            }
            clearTransColor(colorList, lastTrans);
        }

        public void writeObject(FileStream s)
        {
            ArrayList expColorList = new ArrayList();
            MiscUtil.copyArrayList(colorList, expColorList);
            clearTransColor(expColorList, false);
            short len = (short)expColorList.Count;
            IOUtil.writeShort(s, len);
            Console.WriteLine("--------------------------"+len);
            for (int i = 0; i < len; i++)
            {
                IOUtil.writeInt(s, (int)expColorList[i]);
                Console.WriteLine(MathUtil.convertHexString((int)expColorList[i]) + ",");
            }
        }
        public void clearTransColor(ArrayList colorList , bool lastTrans)
        {
            //去除透明色
            for (int i = 0; i < colorList.Count; i++)
            {
                int color = (int)colorList[i];
                int alpha = ((color >> 24)&0xFF);
                if (alpha == 0)
                {
                    colorList.RemoveAt(i);
                    Console.WriteLine("删除透明色:"+i);
                    break;
                }
                else if (lastTrans && i == colorList.Count - 1)
                {
                    if (color == 0xFFFFFF || color == 0)
                    {
                        colorList.RemoveAt(i);
                        break;
                    }
                }
            }
        }
    }
}
