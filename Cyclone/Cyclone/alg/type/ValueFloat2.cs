using System.Drawing;
namespace Cyclone.alg.type
{
    public struct ValueFloat2
    {
       public float X, Y;

        public ValueFloat2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public ValueFloat2(ValueFloat2 v)
        {
            this.X = v.X;
            this.Y = v.Y;
        }
        /**
         * ������ֵ
         * @param x
         * @param y
         */
        public void setValue(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
        /**
         * ���ó�P0->P1������
         * @param P0
         * @param P1
         */
        public void setValue(PointF P0, PointF P1)
        {
            this.X = P1.X - P0.X;
            this.Y = P1.Y - P0.Y;
        }
    }
}
