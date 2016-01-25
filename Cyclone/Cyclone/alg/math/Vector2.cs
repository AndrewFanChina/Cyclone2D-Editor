using System;
using System.Drawing;
namespace Cyclone.alg.math
{
    public class Vector2
    {
        public float x, y;

        private static Vector3 vector3Temp = new Vector3();// ��ʱ��ά����

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
         * ������ֵ
         * @param x
         * @param y
         */
        public void setValue(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        /**
         * ���ó�P0->P1������
         * @param P0
         * @param P1
         */
        public void setValue(PointF P0, PointF P1)
        {
            this.x = P1.X - P0.X;
            this.y = P1.Y - P0.Y;
        }

        /**
         * �ڻ�����
         * 
         * @return int
         */
        public float innerProduct(Vector2 v)
        {
            return (x * v.x + y * v.y);
        }

        /**
         * �ڻ�����
         * 
         * @param v1
         *            ����1
         * @param v2
         *            ����2
         * @return �����������ڻ�
         */
        public static float innerProduct(Vector2 v1, Vector2 v2)
        {
            return (v1.x * v2.x + v1.y * v2.y);
        }

        /**
         * �������
         * 
         * @param v
         *            Vector2
         * @return float ������������ά������ʵ������һ������
         */
        public float outerProduct(Vector2 v)
        {
            return (x * v.y - v.x * y);
        }

        /**
         * �������
         * 
         * @param v1
         *            ��ά����1
         * @param v2
         *            ��ά����2
         * @return ����������������������ά������ʵ������һ������
         */
        public static float outerProduct(Vector2 v1, Vector2 v2)
        {
            float t = (v1.x * v2.y - v2.x * v1.y);
            return t;
        }

        /**
         * ���ȼ���
         * 
         * @return float ����
         */
        public float size()
        {
            float t = x * x + y * y;
            t = (float)Math.Sqrt(t);
            return t;
        }

        /**
         * ���ȵ�ƽ��
         * 
         * @return float ���ȵ�ƽ��
         */
        public float size2()
        {
            float t = (x * x + y * y);
            return t;
        }

        /**
         * ��תָ������(����ά����Χ��Z��)
         * 
         * @param theta
         *            ����
         */
        public void rotate(float theta)
        {
            vector3Temp.setValue(x, 1, -y);
            vector3Temp.rotateY(theta);
            this.setValue(vector3Temp.x, -vector3Temp.z);
        }
    }
}
