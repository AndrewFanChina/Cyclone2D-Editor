using System;
using System.Drawing;
namespace Cyclone.alg.math
{
    public class Vector2
    {
        public float x, y;

        private static Vector3 vector3Temp = new Vector3();// 临时三维向量

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2()
        {
            x = y = 0;
        }

        public Vector2(Vector2 v)
        {
            this.x = v.x;
            this.y = v.y;
        }
        /**
         * 设置数值
         * @param x
         * @param y
         */
        public void setValue(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        /**
         * 设置成P0->P1的向量
         * @param P0
         * @param P1
         */
        public void setValue(PointF P0, PointF P1)
        {
            this.x = P1.X - P0.X;
            this.y = P1.Y - P0.Y;
        }

        /**
         * 内积计算
         * 
         * @return int
         */
        public float innerProduct(Vector2 v)
        {
            return (x * v.x + y * v.y);
        }

        /**
         * 内积计算
         * 
         * @param v1
         *            向量1
         * @param v2
         *            向量2
         * @return 两个向量的内积
         */
        public static float innerProduct(Vector2 v1, Vector2 v2)
        {
            return (v1.x * v2.x + v1.y * v2.y);
        }

        /**
         * 外积计算
         * 
         * @param v
         *            Vector2
         * @return float 外积结果，从三维来看，实际上是一个向量
         */
        public float outerProduct(Vector2 v)
        {
            return (x * v.y - v.x * y);
        }

        /**
         * 外积计算
         * 
         * @param v1
         *            二维向量1
         * @param v2
         *            二维向量2
         * @return 两个向量的外积结果，从三维来看，实际上是一个向量
         */
        public static float outerProduct(Vector2 v1, Vector2 v2)
        {
            float t = (v1.x * v2.y - v2.x * v1.y);
            return t;
        }

        /**
         * 长度计算
         * 
         * @return float 长度
         */
        public float size()
        {
            float t = x * x + y * y;
            t = (float)Math.Sqrt(t);
            return t;
        }

        /**
         * 长度的平方
         * 
         * @return float 长度的平方
         */
        public float size2()
        {
            float t = (x * x + y * y);
            return t;
        }

        /**
         * 旋转指定弧度(从三维来看围绕Z轴)
         * 
         * @param theta
         *            弧度
         */
        public void rotate(float theta)
        {
            vector3Temp.setValue(x, 1, -y);
            vector3Temp.rotateY(theta);
            this.setValue(vector3Temp.x, -vector3Temp.z);
        }
    }
}
