using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
namespace Cyclone.alg.math
{
    public class MathUtil
    {
        //��ʱ����
        private static Vector2 v2Temp1 = new Vector2();
        private static Vector2 v2Temp2 = new Vector2();
        private static Vector2 v2Temp3 = new Vector2();
        private static Vector2 v2Temp4 = new Vector2();

        /**
        * ��ƽ������.
        *
        * @param x int
        * @return int
        */
        public static long sqrt(long x)
        {
            return (long)Math.Sqrt(x);
        }

	    /**
	     * abs ����ֵ.
	     *
	     * @param number int
	     * @return int
	     */
	    public static long abs(long number)
	    {
		    if (number < 0)
			    number = -number;
		    return number;
	    }

	    /**
	     * Abs.
	     *
	     * @param number the number
	     * @return the int
	     */
	    public static int abs(int number)
	    {
		    if (number < 0)
			    number = -number;
		    return number;
	    }

	    /**
	     * Abs.
	     *
	     * @param number the number
	     * @return the double
	     */
	    public static double abs(double number)
	    {
		    if (number < 0)
			    number = -number;
		    return number;
	    }

	    /**
	     * max ��ϴ�ֵ.
	     *
	     * @param num1 int
	     * @param num2 int
	     * @return int
	     */
	    public static long max(long num1, long num2)
	    {
		    return (num1 > num2) ? num1 : num2;
	    }

	    /**
	     * Max.
	     *
	     * @param num1 the num1
	     * @param num2 the num2
	     * @return the int
	     */
	    public static int max(int num1, int num2)
	    {
		    return (num1 > num2) ? num1 : num2;
	    }

	    /**
	     * min ���Сֵ.
	     *
	     * @param num1 int
	     * @param num2 int
	     * @return int
	     */
	    public static long min(long num1, long num2)
	    {
		    return (num1 < num2) ? num1 : num2;
	    }

	    /**
	     * Min.
	     *
	     * @param num1 the num1
	     * @param num2 the num2
	     * @return the int
	     */
	    public static int min(int num1, int num2)
	    {
		    return (num1 < num2) ? num1 : num2;
	    }

	    // �ж����񹹳��Ƿ�˳ʱ�뷽��(0->1->2,1->2->0,2->0->1Ϊ˳ʱ�룬����Ϊ��ʱ��)
	    /**
	     * Checks if is deasil.
	     *
	     * @param n0 the n0
	     * @param n1 the n1
	     * @param n2 the n2
	     * @return true, if is deasil
	     */
	    public static bool isDeasil(int n0, int n1, int n2)
	    {
            bool res = false;
		    if ((n0 == 0 && n1 == 1 && n2 == 2) || (n0 == 1 && n1 == 2 && n2 == 0) || (n0 == 2 && n1 == 1 && n2 == 0))
		    {
			    res = true;
		    }
		    return res;
	    }

	    /**
	     * �����ȵ�����[0,2PI)����
	     *
	     * @param �� the ��
	     * @return the long
	     */
	    public static double standirdRadians(double ��)
	    {
		    // ���� angle ����׼����[0,360)
		    if (�� < 0)
		    {
			    �� = Math.PI * 2 - ��;
			    �� %= Math.PI * 2;
			    �� = Math.PI * 2 - ��;
		    }
		    else
		    {
			    �� %= Math.PI * 2;
		    }
		    return ��;
	    }
        public static float standirdAngle(float ��)
        {
            return (float)standirdRadians((double)��);
        }
        /**
         * ���Ƕȵ�����[0,360)����
         *
         */
        public static float standirdDegree(float degree)
        {
            // ���� angle ����׼����[0,360)
            if (degree < 0)
            {
                degree = 360 - degree;
                degree %= 360;
                degree = 360 - degree;
            }
            else
            {
                degree %= 360;
            }
            return degree;
        }
	    /**
	     * �����ȵ�����[-PI,PI)����
	     *
	     * @param �� the ��
	     * @return the long
	     */
	    public static double standirdAngle2(double ��)
	    {
		    // ���� angle ����׼����[-PI,PI)
		    if (�� < -Math.PI)
		    {
			    �� += ((-Math.PI - ��) / (Math.PI * 2) + 1) * Math.PI * 2;
		    }
		    else if (�� >= Math.PI)
		    {
			    �� -= ((�� - Math.PI) / (Math.PI * 2) + 1) * Math.PI * 2;
		    }
		    return ��;
	    }

	    /**
	     * ��180�����ڱȽϻ���A��B
	     *
	     * @param ��A the �� a
	     * @param ��B the �� b
	     * @return true, if successful
	     */
	    // TRUE A���|FALSE A�ұ�
        public static bool left_Right(double ��A, double ��B)
	    {
		    ��A = standirdRadians(��A);
		    ��B = standirdRadians(��B);
		    if (MathUtil.abs(��A - ��B) < Math.PI)
		    {
			    return ��A > ��B;
		    }
		    else
		    {
			    return ��A < ��B;
		    }
	    }

	    /**
	     * ��180�����ڼ���Ƕ�A��B�ļн�,����ֵ����ֵ
	     *
	     * @param ��A the �� a
	     * @param ��B the �� b
	     * @return the long
	     */
	    public static double clipAngle(double ��A, double ��B)
	    {
		    ��A = standirdRadians(��A);
		    ��B = standirdRadians(��B);
		    double abs = MathUtil.abs(��A - ��B);
		    if (abs < Math.PI)
		    {
			    return abs;
		    }
		    else
		    {
			    return Math.PI * 2 - abs;
		    }
	    }

	    /**
	     *��180�����ڼ��㻡��A��B�ļн�,����ֵ�����Ҹ�
	     *
	     * @param ��A the �� a
	     * @param ��B the �� b
	     * @return the long
	     */
	    public static double gapAngle(double ��A, double ��B)
	    {
		    if (left_Right(��A, ��B))
		    {
			    return clipAngle(��A, ��B);
		    }
		    else
		    {
			    return -clipAngle(��A, ��B);
		    }
	    }
        /**
         *����Ƕ�A��B���е�С��180�ȵĽ��ڲ����м��
         */
        public static float gapMiddleDegree(float degreeA, float degreeB)
        {
            degreeA = standirdDegree(degreeA);
            degreeB = standirdDegree(degreeB);
            if (degreeA < degreeB)
            {
                degreeA += 360;
            }
            float degree = (degreeA + degreeB) / 2;
            if (degreeA - degreeB > 180)
            {
                degree -= 180;
            }
            return standirdDegree(degree);
        }
	    // �߶ν������
	    // static
	    // {
	    // System.out.println(linCoss1(0, 2, 3, 2, 1, -2, 1, 2));
	    // System.out.println(linCoss(-1, 2, 1, 3, -1, 1, 1, 4));
	    // System.out.println(linCoss(-1, 2, 1, 3, -1, 3, 1, 2));
	    // System.out.println(linCoss(-1, 2, 1, 3, 2, 0, 2, 9));
	    // }

	    // �ж������߶��Ƿ��ཻ(0,1)��(2,3),���һ����������һ���߶���Ҳ�����ཻ
	    /**
	     * Lin coss.
	     *
	     * @param x0 the x0
	     * @param y0 the y0
	     * @param x1 the x1
	     * @param y1 the y1
	     * @param x2 the x2
	     * @param y2 the y2
	     * @param x3 the x3
	     * @param y3 the y3
	     * @return true, if successful
	     */
	    public static bool linCoss(long x0, long y0, long x1, long y1, long x2, long y2, long x3, long y3)
	    {
		    long r1 = (x1 - x0) * (y3 - y0) - (x3 - x0) * (y1 - y0);
		    if (r1 == 0)
		    {
			    if (inRegionClose(x3, x0, x1))
			    {
				    return true;
			    }
			    else
			    {
				    return false;
			    }
		    }
		    long r2 = (x1 - x0) * (y2 - y0) - (x2 - x0) * (y1 - y0);
		    if (r2 == 0)
		    {
			    if (inRegionClose(x2, x0, x1))
			    {
				    return true;
			    }
			    else
			    {
				    return false;
			    }
		    }
		    long r3 = (x3 - x2) * (y0 - y2) - (x0 - x2) * (y3 - y2);
		    if (r3 == 0)
		    {
			    if (inRegionClose(x0, x2, x3))
			    {
				    return true;
			    }
			    else
			    {
				    return false;
			    }
		    }
		    long r4 = (x3 - x2) * (y1 - y2) - (x1 - x2) * (y3 - y2);
		    if (r4 == 0)
		    {
			    if (inRegionClose(x1, x2, x3))
			    {
				    return true;
			    }
			    else
			    {
				    return false;
			    }
		    }
		    long r1T = getCode(r1);
		    long r2T = getCode(r2);
		    long r3T = getCode(r3);
		    long r4T = getCode(r4);
		    if (r1T * r2T < 0 && r3T * r4T < 0)
		    {
			    return true;
		    }
		    return false;
	    }

	    // �ж������߶��Ƿ��ཻ(0,1)��(2,3),�����1���߶�23��Ҳ�����ཻ�����๲�㲻��
	    /**
	     * Lin coss1.
	     *
	     * @param x0 the x0
	     * @param y0 the y0
	     * @param x1 the x1
	     * @param y1 the y1
	     * @param x2 the x2
	     * @param y2 the y2
	     * @param x3 the x3
	     * @param y3 the y3
	     * @return true, if successful
	     */
	    public static bool linCoss1(long x0, long y0, long x1, long y1, long x2, long y2, long x3, long y3)
	    {
		    long r1 = (x1 - x0) * (y3 - y0) - (x3 - x0) * (y1 - y0);
		    long r2 = (x1 - x0) * (y2 - y0) - (x2 - x0) * (y1 - y0);
		    long r3 = (x3 - x2) * (y0 - y2) - (x0 - x2) * (y3 - y2);
		    long r4 = (x3 - x2) * (y1 - y2) - (x1 - x2) * (y3 - y2);
		    if (r4 == 0)
		    {
			    if (inRegionClose(x1, x2, x3))
			    {
				    return true;
			    }
			    else
			    {
				    return false;
			    }
		    }
		    r1 = getCode(r1);
		    r2 = getCode(r2);
		    r3 = getCode(r3);
		    r4 = getCode(r4);
		    if (r1 * r2 < 0 && r3 * r4 < 0)
		    {
			    return true;
		    }
		    return false;
	    }
        // �ж������߶��Ƿ��ཻ(0,1)��(2,3),���һ����������һ���߶���Ҳ�����ཻ
        public static bool linCoss(PointF p0, PointF p1, PointF p2, PointF p3)
        {
            v2Temp1.setValue(p1.X - p0.X, p1.Y - p0.Y);//P01
            v2Temp2.setValue(p2.X - p0.X, p2.Y - p0.Y);//P02
            v2Temp3.setValue(p3.X - p0.X, p3.Y - p0.Y);//P03
            if (v2Temp1.outerProduct(v2Temp2) * v2Temp1.outerProduct(v2Temp3) >= 0)
            {
                return false;
            }
            v2Temp1.setValue(p2.X - p3.X, p2.Y - p3.Y);//P32
            v2Temp2.setValue(p0.X - p3.X, p0.Y - p3.Y);//P30
            v2Temp3.setValue(p1.X - p3.X, p1.Y - p3.Y);//P31
            if (v2Temp1.outerProduct(v2Temp2) * v2Temp1.outerProduct(v2Temp3) >= 0)
            {
                return false;
            }
            return true;
        }
        // �ж��߶�(0,1)��ֱ��(2,3)�Ƿ��ཻ
        public static bool segmentCossLine(PointF p0, PointF p1, PointF p2, PointF p3)
        {
            v2Temp1.setValue(p2.X - p3.X, p2.Y - p3.Y);//P32
            v2Temp2.setValue(p0.X - p3.X, p0.Y - p3.Y);//P30
            v2Temp3.setValue(p1.X - p3.X, p1.Y - p3.Y);//P31
            if (v2Temp1.outerProduct(v2Temp2) * v2Temp1.outerProduct(v2Temp3) >= 0)
            {
                return false;
            }
            return true;
        }
	    // ���������
	    /**
	     * Gets the code.
	     *
	     * @param number the number
	     * @return the code
	     */
        public static long getCode(long number)
	    {
		    if (number > 0)
		    {
			    number = 1;
		    }
		    else if (number < 0)
		    {
			    number = -1;
		    }
		    else
		    {
			    number = 0;
		    }
		    return number;
	    }

        // ���������
        /**
         * Gets the code.
         *
         * @param number the number
         * @return the code
         */
        public static float getCode(float number)
        {
            if (number > 0)
            {
                number = 1;
            }
            else if (number < 0)
            {
                number = -1;
            }
            else
            {
                number = 0;
            }
            return number;
        }
	    // �ж������߶�(2,3)��(0,1)������(��ʱ��)���Ǹ���
	    /**
	     * Left or right.
	     *
	     * @param x0 the x0
	     * @param y0 the y0
	     * @param x1 the x1
	     * @param y1 the y1
	     * @param x2 the x2
	     * @param y2 the y2
	     * @param x3 the x3
	     * @param y3 the y3
	     * @return true, if successful
	     */
	    public static bool leftOrRight(long x0, long y0, long x1, long y1, long x2, long y2, long x3, long y3)
	    {
		    long xa = (x1 - x0);
		    long ya = (y1 - y0);
		    long xb = (x3 - x2);
		    long yb = (y3 - y2);
		    return xa * yb - xb * ya > 0;
	    }

	    // --------------------------------��ѧ����--------------------------------
	    // ��ֵ�Ƿ�λ��ĳ��������
	    /**
	     * In region.
	     *
	     * @param num the num
	     * @param num0 the num0
	     * @param num1 the num1
	     * @return true, if successful
	     */
	    public static bool inRegion(int num, int num0, int num1)
	    {
		    return Math.Min(num0, num1) < num && Math.Max(num0, num1) > num;
	    }
	    /**
	     * In region.
	     *
	     * @param num the num
	     * @param num0 the num0
	     * @param num1 the num1
	     * @return true, if successful
	     */
	    public static  bool inRegion(long num, long num0, long num1)
	    {
		    return Math.Min(num0, num1) < num && Math.Max(num0, num1) > num;
	    }
        /**
         * In region.
         *
         * @param num the num
         * @param num0 the num0
         * @param num1 the num1
         * @return true, if successful
         */
        public static bool inRegion(float num, float num0, float num1)
        {
            return Math.Min(num0, num1) < num && Math.Max(num0, num1) > num;
        }

        /**
         * In region close.
         *
         * @param num the num
         * @param num0 the num0
         * @param num1 the num1
         * @return true, if successful
         */
        public static bool inRegionClose(int num, int num0, int num1)
        {
            return Math.Min(num0, num1) <= num && Math.Max(num0, num1) >= num;
        }
        /**
         * In region close.
         *
         * @param num the num
         * @param num0 the num0
         * @param num1 the num1
         * @return true, if successful
         */
        public static bool inRegionClose(float num, float num0, float num1)
        {
            return Math.Min(num0, num1) <= num && Math.Max(num0, num1) >= num;
        }
	    /**
	     * In region close.
	     *
	     * @param num the num
	     * @param num0 the num0
	     * @param num1 the num1
	     * @return true, if successful
	     */
	    public static  bool inRegionClose(long num, long num0, long num1)
	    {
		    return Math.Min(num0, num1) <= num && Math.Max(num0, num1) >= num;
	    }

	    // ��ײ����
	    /**
	     * Collide with.
	     *
	     * @param x1 the x1
	     * @param y1 the y1
	     * @param w1 the w1
	     * @param h1 the h1
	     * @param x2 the x2
	     * @param y2 the y2
	     * @param w2 the w2
	     * @param h2 the h2
	     * @return true, if successful
	     */
	    public static  bool collideWith(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2)
	    {
		    if (x1 + w1 > x2 && x1 < x2 + w2 && y1 + h1 > y2 && y1 < y2 + h2)
		    {
			    return true;
		    }
		    return false;
	    }

	    /**
	     * Collide with.
	     *
	     * @param x1 the x1
	     * @param y1 the y1
	     * @param w1 the w1
	     * @param h1 the h1
	     * @param x2 the x2
	     * @param y2 the y2
	     * @param w2 the w2
	     * @param h2 the h2
	     * @return true, if successful
	     */
	    public static  bool collideWith(long x1, long y1, long w1, long h1, long x2, long y2, long w2, long h2)
	    {
		    if (x1 + w1 > x2 && x1 < x2 + w2 && y1 + h1 > y2 && y1 < y2 + h2)
		    {
			    return true;
		    }
		    return false;
	    }

	    /**
	     * �ж��������Ƿ���ײ�����㷨 x1,y1 - ����A���Ͻ����� x2,y2 - ����A���½����� x3,y3 - ����B���Ͻ����� x4,y4 -
	     * ����B���½�����.
	     *
	     * @param x1 the x1
	     * @param y1 the y1
	     * @param x2 the x2
	     * @param y2 the y2
	     * @param x3 the x3
	     * @param y3 the y3
	     * @param x4 the x4
	     * @param y4 the y4
	     * @return true, if is rect crossing
	     */
	    public static  bool isRectCrossing(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
	    {
		    if (x1 >= x4)
			    return false;
		    if (x2 <= x3)
			    return false;
		    if (y1 >= y4)
			    return false;
		    if (y2 <= y3)
			    return false;
		    return true;
	    }

	    // ����ֵ������ĳ��������value = [min,max]
	    /**
	     * Limit number.
	     *
	     * @param num the num
	     * @param num0 the num0
	     * @param num1 the num1
	     * @return the int
	     */
	    public static  int limitNumber(int num, int num0, int num1)
	    {
		    if (num < Math.Min(num0, num1))
		    {
			    num = Math.Min(num0, num1);
		    }
		    if (num > Math.Max(num0, num1))
		    {
			    num = Math.Max(num0, num1);
		    }
		    return num;
	    }

	    // ����ֵ������ĳ��������value = [min,max]
	    /**
	     * Limit number.
	     *
	     * @param num the num
	     * @param num0 the num0
	     * @param num1 the num1
	     * @return the long
	     */
	    public static  long limitNumber(long num, long num0, long num1)
	    {
		    if (num < Math.Min(num0, num1))
		    {
			    num = Math.Min(num0, num1);
		    }
		    if (num > Math.Max(num0, num1))
		    {
			    num = Math.Max(num0, num1);
		    }
		    return num;
	    }
        public static float limitNumber(float num, float num0, float num1)
        {
            if (num < Math.Min(num0, num1))
            {
                num = Math.Min(num0, num1);
            }
            if (num > Math.Max(num0, num1))
            {
                num = Math.Max(num0, num1);
            }
            return num;
        }
        public static double limitNumber(double num, double num0, double num1)
        {
            if (num < Math.Min(num0, num1))
            {
                num = Math.Min(num0, num1);
            }
            if (num > Math.Max(num0, num1))
            {
                num = Math.Max(num0, num1);
            }
            return num;
        }
	    /** The Constant myRandom. @todo �����--------------------------------------- */
	    private static  Random myRandom = new Random();

	    // ��������[0,srcNum)
	    /**
	     * Gets the random.
	     *
	     * @param srcNum the src num
	     * @return the random
	     */
	    public static  int getRandom(int srcNum)
	    {
		    if (srcNum <= 0)
		    {
			    return 0;
		    }
		    return Math.Abs(myRandom.Next()) % srcNum;
	    }

	    // ��������[srcNum1,srcNum2)
	    /**
	     * Gets the random.
	     *
	     * @param srcNum1 the src num1
	     * @param srcNum2 the src num2
	     * @return the random
	     */
	    public static  int getRandom(int srcNum1, int srcNum2)
	    {
		    if (srcNum1 >= srcNum2)
		    {
			    return srcNum2;
		    }
		    return srcNum1 + (Math.Abs(myRandom.Next()) % (srcNum2 - srcNum1));
	    }
	    // ��������[srcNum1,srcNum2)
	    /**
	     * Gets the random.
	     *
	     * @param srcNum1 the src num1
	     * @param srcNum2 the src num2
	     * @return the random
	     */
	    public static  float getRandom(float srcNum1, float srcNum2)
	    {
		    if (srcNum1 >= srcNum2)
		    {
			    return srcNum2;
		    }
		    int max=int.MaxValue;
		    return (max*srcNum1 + (Math.Abs(myRandom.Next()) % (max*(srcNum2 - srcNum1))))/max;
	    }
	    // ��������[srcNum1,srcNum2)
	    /**
	     * Gets the random.
	     *
	     * @param srcNum1 the src num1
	     * @param srcNum2 the src num2
	     * @return the random
	     */
	    public static  double getRandom(double srcNum1, double srcNum2)
	    {
		    if (srcNum1 >= srcNum2)
		    {
			    return srcNum2;
		    }
            int max = int.MaxValue;
		    return (max*srcNum1 + (Math.Abs(myRandom.Next()) % (max*(srcNum2 - srcNum1))))/max;
	    }
	    // ������ظ������������[srcNum1,srcNum2]
	    /**
	     * Gets the no repeated random.
	     *
	     * @param srcNum1 the src num1
	     * @param srcNum2 the src num2
	     * @param count the count
	     * @return the no repeated random
	     */
	    public static  int[] getNoRepeatedRandom(int srcNum1, int srcNum2, int count)
	    {
		    int maxCount = Math.Abs(srcNum1 - srcNum2) + 1;
		    int minNumber = Math.Min(srcNum1, srcNum2);
		    if (count > maxCount)
		    {
			    return null;
		    }
            int[] datas = new int[count];
		    // �����������
		    for (int i = 0; i < datas.Length; i++)
		    {
			    int index = getRandom(maxCount - i);
			    // Ѱ���������
			    for (int j = 0; j < maxCount; j++)
			    {
				    bool used = false;
				    // �����ظ�
				    for (int k = 0; k < i; k++)
				    {
					    int data = j + minNumber;
					    if (data == datas[k])
					    {
						    used = true;
						    break;
					    }
				    }
				    if (!used)
				    {
					    if (index == 0)
					    { // �ҵ�
						    index = j;
						    break;
					    }
					    else
					    {
						    index--;
					    }
				    }
			    }
			    // ����
			    datas[i] = index + minNumber;
		    }
		    return datas;
	    }

	    /**
	     * �ж�[x,y]�Ƿ�������[_x,_y,_w,_h]
	     *
	     * @param x the x
	     * @param y the y
	     * @param _x the _x
	     * @param _y the _y
	     * @param _w the _w
	     * @param _h the _h
	     * @return true, if successful
	     */
	    public static  bool inRegion(float x, float y, float _x, float _y, float _w, float _h)
	    {
		    if (x >= _x && x <= _x + _w && y >= _y && y <= _y + _h)
		    {
			    return true;
		    }
		    return false;
	    }
	    /**
	     * �ж�[x,y]�Ƿ�������[_x,_y,_w,_h]
	     *
	     * @param x the x
	     * @param y the y
	     * @param _x the _x
	     * @param _y the _y
	     * @param _w the _w
	     * @param _h the _h
	     * @return true, if successful
	     */
	    public static  bool inRegion(double x, double y, double _x, double _y, double _w, double _h)
	    {
		    if (x >= _x && x <= _x + _w && y >= _y && y <= _y + _h)
		    {
			    return true;
		    }
		    return false;
	    }
	    /**
	     * �ж�[x,y]�Ƿ�������[_x,_y,_w,_h]
	     *
	     * @param x the x
	     * @param y the y
	     * @param _x the _x
	     * @param _y the _y
	     * @param _w the _w
	     * @param _h the _h
	     * @return true, if successful
	     */
	    public static  bool inRegion(int x, int y, int _x, int _y, int _w, int _h)
	    {
		    if (x >= _x && x <= _x + _w && y >= _y && y <= _y + _h)
		    {
			    return true;
		    }
		    return false;
	    }
	    /** The array index. @todo ð�ݷ��Ӵ�С��������,���������������飬���ض�Ӧ�����������е���λ����[30,20,40]����[1,2,0] */
	    private static int[] arrayIndex = null;

	    /**
	     * Order numbers.
	     *
	     * @param arry the arry
	     */
	    public static  void orderNumbers(int[] arry)
	    {
            orderNumbers(arry, arry.Length);
	    }

	    /**
	     * ð�ݷ��Ӵ�С��������,���������������飬���ض�Ӧ�����������е���λ����[30,20,40]����[1,2,0],ֻ����ǰN��
	     *
	     * @param arry the arry
	     * @param nbItem the nb item
	     */
	    public static  void orderNumbers(int[] arry, int nbItem)
	    {
            if (arrayIndex == null || arrayIndex.Length != arry.Length)
		    {
                arrayIndex = new int[arry.Length];
		    }
		    for (int i = 0; i < nbItem; i++)
		    {
			    arrayIndex[i] = i;
		    }
		    int t;
		    for (int i = 0; i < nbItem - 1; i++)
		    {
			    for (int j = i + 1; j < nbItem; j++)
			    {
				    if (arry[j] > arry[i])
				    {
					    // switch number
					    t = arry[j];
					    arry[j] = arry[i];
					    arry[i] = t;
					    // switch index
					    t = arrayIndex[j];
					    arrayIndex[j] = arrayIndex[i];
					    arrayIndex[i] = t;
				    }
			    }
		    }
		    // copy result
		    for (int i = 0; i < nbItem; i++)
		    {
			    arry[arrayIndex[i]] = i;
		    }
	    }

	    // ����ĳһ��Ԫ�Ӵ�С��������
	    /**
	     * Order array.
	     *
	     * @param score the score
	     * @param index the index
	     */
	    public static  void orderArray(int[][] score, int index)
	    {
		    for (int i = 1; i < score.Length; i++)
		    {
			    for (int j = 0; j < i; j++)
			    {
				    if (score[i][index] > score[j][index])
				    {
					    // ����Ǩ��
					    int[] temp = score[i];
					    for (int m = i; m > j; m--)
					    {
						    score[m] = score[m - 1];
					    }
					    score[j] = temp;
					    break;
				    }
			    }
		    }
	    }

	    // ����ĳһ��Ԫ�Ӵ�С��������
	    /**
	     * Order array max.
	     *
	     * @param score the score
	     * @param index the index
	     */
	    public static  void orderArrayMax(long[][] score, int index)
	    {
            for (int i = 1; i < score.Length; i++)
		    {
			    for (int j = 0; j < i; j++)
			    {
				    if (score[i][index] > score[j][index])
				    {
					    // ����Ǩ��
					    long[] temp = score[i];
					    for (int m = i; m > j; m--)
					    {
						    score[m] = score[m - 1];
					    }
					    score[j] = temp;
					    break;
				    }
			    }
		    }
	    }

	    // ����ĳһ��Ԫ��С������������
	    /**
	     * Order array min.
	     *
	     * @param score the score
	     * @param index the index
	     */
	    public static  void orderArrayMin(int[][] score, int index)
	    {
		    for (int i = 1; i < score.Length; i++)
		    {
			    for (int j = 0; j < i; j++)
			    {
				    if (score[i][index] < score[j][index])
				    {
					    // ����Ǩ��
					    int[] temp = score[i];
					    for (int m = i; m > j; m--)
					    {
						    score[m] = score[m - 1];
					    }
					    score[j] = temp;
					    break;
				    }
			    }
		    }
	    }

	    // ����ĳһ��Ԫ��С������������
	    /**
	     * Order array min.
	     *
	     * @param score the score
	     * @param index the index
	     */
	    public static  void orderArrayMin(long[][] score, int index)
	    {
		    for (int i = 1; i < score.Length; i++)
		    {
			    for (int j = 0; j < i; j++)
			    {
				    if (score[i][index] < score[j][index])
				    {
					    // ����Ǩ��
					    long[] temp = score[i];
					    for (int m = i; m > j; m--)
					    {
						    score[m] = score[m - 1];
					    }
					    score[j] = temp;
					    break;
				    }
			    }
		    }
	    }

	    // ����ĳһ��Ԫ��С������������
	    /**
	     * Order array.
	     *
	     * @param sortNumberT the sort number t
	     * @param start the start
	     * @param end the end
	     */
	    public static  void orderArray(long[] sortNumberT, int start, int end)
	    {
		    if (sortNumberT == null || start < 0 || start >= end || end >= sortNumberT.Length)
		    {
			    return;
		    }
		    start = limitNumber(start, 0, sortNumberT.Length - 1);
		    end = limitNumber(end, 0, sortNumberT.Length - 1);
		    quicksort(sortNumberT, start, end);
	    }

	    /**
	     * Order array.
	     *
	     * @param sortNumberT the sort number t
	     * @param start the start
	     * @param end the end
	     */
	    public static  void orderArray(int[][] sortNumberT, int start, int end)
	    {
		    if (sortNumberT == null || start < 0 || start >= end || end >= sortNumberT.Length)
		    {
			    return;
		    }
		    start = limitNumber(start, 0, sortNumberT.Length - 1);
		    end = limitNumber(end, 0, sortNumberT.Length - 1);
		    Sort(sortNumberT, start, end);
	    }

	    /**
	     * Quicksort.
	     *
	     * @param pData the data
	     * @param left the left
	     * @param right the right
	     */
        private static void quicksort(long[] pData, int left, int right)
	    {
		    long middle, strTemp;
		    int i = left;
		    int j = right;
		    middle = pData[(left + right) / 2];
		    do
		    {
			    while (i < right && pData[i] < middle)
			    {
				    i++;
			    }
			    while (j > left && pData[j] > middle)
			    {
				    j--;
			    }
			    if (i <= j)
			    {
				    strTemp = pData[i];
				    pData[i] = pData[j];
				    pData[j] = strTemp;
				    i++;
				    j--;
			    }
		    }
		    while (i < j);// �������ɨ����±꽻�����һ������
		    if (left < j)
		    {
			    quicksort(pData, left, j); // �ݹ����
		    }
		    if (right > i)
		    {
			    quicksort(pData, i, right); // �ݹ����
		    }
	    }

	    /**
	     * Sort.
	     *
	     * @param pData the data
	     * @param left the left
	     * @param right the right
	     */
	    public static  void Sort(int[][] pData, int left, int right)
	    {
            int n, i;
            int[] temp;
		    for (int m = left + 1; m <= right; m++)
		    {
			    n = left;
			    for (; n < m; n++)
			    {
				    if (pData[n][0] < pData[m][0])
				    {
					    continue;
				    }
				    else if (pData[n][0] == pData[m][0])
				    {
					    if (pData[n][1] < pData[m][1])
					    {
						    continue;
					    }
					    else if (pData[n][1] == pData[m][1])
					    {
						    if (pData[n][2] < pData[m][2])
						    {
							    continue;
						    }
						    else if (pData[n][2] == pData[m][2])
						    {
							    if (pData[n][3] < pData[m][3])
							    {
								    continue;
							    }
						    }
					    }
				    }
				    break;
			    }
			    temp = pData[m];
			    for (i = m; i > n; i--)
			    {
				    pData[i] = pData[i - 1];
			    }
			    pData[n] = temp;
		    }

	    }
        //�жϵ��Ƿ�������һ���㼯����ɵĶ����
        public static bool pointInRegion(PointF pHit, PointF[] pFrame)
        {
            if (pFrame == null || pFrame ==null|| pFrame.Length < 3)
            {
                return false;
            }
            float result = 0.0f;
            for (int i = 0; i < pFrame.Length; i++)
            {
                int p0 = i;
                int p1 = (i + 1) % pFrame.Length;
                v2Temp1.x = pFrame[p0].X - pFrame[p1].X;
                v2Temp1.y = pFrame[p0].Y - pFrame[p1].Y;
                v2Temp2.x = pFrame[p0].X - pHit.X;
                v2Temp2.y = pFrame[p0].Y - pHit.Y;
                float resultNew = v2Temp1.outerProduct(v2Temp2);
                if (resultNew == 0.0f)
                {
                    return false;
                }
                if (result == 0.0f)
                {
                    result = resultNew;
                }
                else
                {
                    if (result * resultNew < 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        //�ж������㼯����ɵĶ�����Ƿ��н�������
        public static bool regionCrossed(PointF[] p1, PointF[] p2)
        {
            if (p1 == null || p2 == null)
            {
                return false;
            }
            for (int i = 0; i < p1.Length; i++)
            {
                if (pointInRegion(p1[i], p2))
                {
                    return true;
                }
            }
            for (int i = 0; i < p2.Length; i++)
            {
                if (pointInRegion(p2[i], p1))
                {
                    return true;
                }
            }
            return false;
        }
        //���������֮��ľ����ƽ��
        public static float getDistanceSquare_2Points(PointF p1, PointF p2)
        {
            return (p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y);
        }
        //���������֮��ľ����ƽ��
        public static float getDistance_2Points(PointF p1, PointF p2)
        {
            return (float)Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }
        //�ж�һ����P1�Ƿ�������һ����P2��Χ����r��Χ��
        public static bool pointClose(PointF p1, PointF p2,float r)
        {
            if (getDistanceSquare_2Points(p1, p2) <= r * r)
            {
                return true;
            }
            return false;
        }
        //�ж�һ����Pa���Ƿ�������O��B��C����OΪ��㷢���(С��180�ȵĽ�)��
        public static bool inRayAngle(PointF Pa, PointF Po, PointF Pb, PointF Pc)
        {
            v2Temp1.x = Po.X - Pa.X;
            v2Temp1.y = Po.Y - Pa.Y;//OA
            v2Temp2.x = Po.X - Pb.X;
            v2Temp2.y = Po.Y - Pb.Y;//OB
            v2Temp3.x = Po.X - Pc.X;
            v2Temp3.y = Po.Y - Pc.Y;//OC
            return (v2Temp2.outerProduct(v2Temp3) * v2Temp2.outerProduct(v2Temp1) > 0)
                && (v2Temp3.outerProduct(v2Temp2) * v2Temp3.outerProduct(v2Temp1) > 0);
        }
        //���شӵ�A����B�����߹��ɵĽǶ�ֵ(����)
        public static float getPointAngle(PointF Pa, PointF Pb)
        {
            float x = Pb.X - Pa.X;
            float y = -(Pb.Y - Pa.Y);
            return getVectorAngle(x, y);
        }
        //��ȡ�����ĽǶ�[0-360)
        public static float getVectorAngle(float x, float y)
        {
            double s = x * x + y * y;
            if (s == 0)
            {
                return 0.0f;
            }
            s = Math.Sqrt(s);
            double sin = y / s;
            sin = MathUtil.limitNumber((double)sin, -1.0d, 1.0d);
            double value = Math.Asin(sin);
            if (x > 0 && y == 0)
            {
                value = 0;
            }
            else if (x > 0 && y > 0)
            {

            }
            else if (x == 0 && y > 0)
            {
                value = Math.PI / 2;
            }
            else if (x < 0 && y > 0)
            {
                value = Math.PI - value;
            }
            else if (x < 0 && y == 0)
            {
                value = Math.PI;
            }
            else if (x < 0 && y < 0)
            {
                value = Math.PI - value;
            }
            else if (x == 0 && y < 0)
            {
                value = Math.PI * 3 / 2;
            }
            else if (x > 0 && y < 0)
            {
                value = 2 * Math.PI + value;
            }
            return (float)(value % (2 * Math.PI));
        }
        //�жϵ�Pa�Ƿ����ɵ�Pb�͵�Pc��Ϊ�Խ��߹��ɵľ��ε���չ���η�Χ�ڣ���չ��ֵȡ����S
        public static bool pointInRect(PointF Pa, PointF Pb, PointF Pc, float S)
        {
            float xMin = Math.Min(Pb.X, Pc.X) - S;
            float xMax = Math.Max(Pb.X, Pc.X) + S;
            float yMin = Math.Min(Pb.Y, Pc.Y) - S;
            float yMax = Math.Max(Pb.Y, Pc.Y) + S;
            return inRegion(Pa.X, xMin, xMax) && inRegion(Pa.Y, yMin, yMax);
        }
        //��ȡ��Pa����Pb��Pc���ɵ��߶εľ���
        public static float getDistance_PointToSegment(PointF Pa, PointF Pb, PointF Pc)
        {
            float dis = getDistance_PointToLine(Pa, Pb, Pc);
            float disAB = v2Temp1.size();
            float disAC = v2Temp3.size();
            dis = Math.Min(dis, disAB);
            dis = Math.Min(dis, disAC);
            return dis;
        }
        //��ȡ��Pa����Pb��Pc���ɵ�ֱ�ߵľ���
        public static float getDistance_PointToLine(PointF Pa, PointF Pb, PointF Pc)
        {
            v2Temp1.x = Pa.X - Pb.X;
            v2Temp1.y = Pa.Y - Pb.Y;//BA
            v2Temp2.x = Pc.X - Pb.X;
            v2Temp2.y = Pc.Y - Pb.Y;//BC
            v2Temp3.x = Pc.X - Pa.X;
            v2Temp3.y = Pc.Y - Pa.Y;//AC
            float bc = v2Temp2.size();
            if (bc == 0)
            {
                return v2Temp1.size();
            }
            float dis = v2Temp1.outerProduct(v2Temp2) / bc;
            dis = Math.Abs(dis);
            return dis;
        }
        //��ȡ����Old��New��Center�����ɵĽ�OCN�ĽǶ�(����)�����Ҽ��ϱ仯��������
        public static float getAngle_3Points(PointF POld, PointF PNew, PointF PCenter)
        {
            v2Temp1.x = POld.X - PCenter.X;
            v2Temp1.y = POld.Y - PCenter.Y;//COld
            v2Temp2.x = PNew.X - PCenter.X;
            v2Temp2.y = PNew.Y - PCenter.Y;//CNew
            float sizeOA = v2Temp1.size();
            float sizeOB = v2Temp2.size();
            if (sizeOA == 0 || sizeOB == 0)
            {
                return 0;
            }
            float angle=0;
            double acos = 0;
            try
            {
                acos = v2Temp1.innerProduct(v2Temp2) * (1 / sizeOA) * (1 / sizeOB);
                acos = MathUtil.limitNumber((double)acos, -1.0d, 1.0d);
                angle = (float)(Math.Acos(acos));
                if (v2Temp1.outerProduct(v2Temp2) < 0)
                {
                    angle = -angle;
                }
                //Console.WriteLine("angle:" + angle);
            }
            catch (Exception e)
            {
                Console.WriteLine("error");
                Console.WriteLine(e.Message);
            }
            if (!(angle >= float.MinValue && angle <= float.MaxValue))
            {
                Console.WriteLine("error");
            }
            return radiansToDegrees(angle);
        }
        //��ȡ�ɵ�01���ɵ������ڵ�23���ɵ������ϵ�ͶӰ��������֮��
        public static float getProjection_2Vector(PointF P0, PointF P1, PointF P2, PointF P3)
        {
  
            v2Temp1.setValue(P0, P1);//01
            v2Temp2.setValue(P2, P3);//23
            float l = v2Temp2.size();
            if (l == 0)
            {
                return 0;
            }
            return v2Temp1.innerProduct(v2Temp2) / l;
            
        }
        //��ȡһ�����󣬴���������A�����Ͻ�����S������
        public Matrix getMatrix_Scale(Vector2 vDirct, float S)
        {
            Matrix m = new Matrix();
            float size = vDirct.size();
            if (size != 0)
            {
                m.Scale(vDirct.x / size, vDirct.y / size);
            }
            return m;
        }
        //���ȵ�����ת��
        public static float radiansToDegrees(float radians)
        {
            return (float)(180.0f * radians / Math.PI);
        }
        //����������ת��
        public static float degreesToRadians(float degrees)
        {
            return (float)(Math.PI * degrees / 180);
        }
        //��ȡ����Pa������Pb�ϵ�ͶӰ����
        public static PointF getPointProject(PointF Pa, PointF Pb)
        {
            PointF pc = new PointF();
            double rPb = Math.Sqrt(Pb.X * Pb.X + Pb.Y * Pb.Y);
            if (rPb == 0.0f)
            {
                return pc;
            }
            double rPo = (Pa.X * Pb.X + Pa.Y * Pb.Y) / rPb;
            pc.X = (float)(Pb.X * rPo / rPb);
            pc.Y = (float)(Pb.Y * rPo / rPb);
            return pc;
        }
        //��ȡ����Pa������Pb�ϵ�ͶӰ��ֵ��С(���Ժ�����)
        public static float getPointProjectSize(PointF Pa, PointF Pb)
        {
            double rPb = Math.Sqrt(Pb.X * Pb.X + Pb.Y * Pb.Y);
            if (rPb == 0.0f)
            {
                return float.MaxValue;
            }
            double rPo = (Pa.X * Pb.X + Pa.Y * Pb.Y) / rPb;
            return (float)rPo;
        }
        //��ֵ�Ƿ�λ��ĳ�����ұ�����[num0,num1)
        public static bool inRegionCloseLeft(int num, int num0, int num1)
        {
            return Math.Min(num0, num1) <= num && Math.Max(num0, num1) > num;
        }
        public static bool inRegionCloseLeft(float num, float num0, float num1)
        {
            return Math.Min(num0, num1) <= num && Math.Max(num0, num1) > num;
        }
        //ƽ������
        public static double Sqr(double data)
        {
            return data * data;
        }
        //���ν����ж�
        public static bool rectCross(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2)
        {
            bool crossed = true;
            if (x1 + w1 < x2 || x1 > x2 + w2 || y1 + h1 < y2 || y1 > y2 + h2)
            {
                crossed = false;
            }
            return crossed;
        }
        //��ý�ȡС�������λ��double
        public static double get2AfterDot(double data)
        {
            data *= 100;
            int dataT = (int)(data);
            dataT /= 100;
            data = dataT;
            return data;
        }
        //���С���������λ��float
        public static float get2PlaceFloat(float data)
        {
            data *= 100;
            data = (int)(data);
            data /= 100;
            return data;
        }
        //��intת����16�����ַ�
        public static String convertHexString(int value)
        {
            string hexOutput = String.Format("{0:X}", value);
            return hexOutput;
        }
        //�������ֵ
        public static int MinIntValue(int a, int b, int c)
        {
            return Math.Min(Math.Min(a, b), c);
        }
        //������Сֵ
        public static int MaxIntValue(int a, int b, int c)
        {
            return Math.Max(Math.Max(a, b), c);
        }
        //��ȡ������ֵ��λ��
        public static int getLenOfInt(int vlaue)
        {
            int len = 1;
            while (vlaue >= 10)
            {
                vlaue /= 10;
                len++;
            }
            return len;
        }
        //��ȡ������ֵ���ַ�����ʾ���趨��Сλ����������Сλ����ǰ�油0
        public static String getStringOfInt(int vlaue, int minNum)
        {
            int len = 1;
            int valueT = vlaue;
            while (valueT >= 10)
            {
                valueT /= 10;
                len++;
            }
            String s = "" + vlaue;
            if (len < minNum)
            {
                for (int i = 0; i < minNum - len; i++)
                {
                    s = "0" + s;
                }
            }
            return s;
        }
        //��һϵ�е��ð�Χ���ǵľ������
        public static RectangleF getPointsBox(PointF[] points)
        {
            RectangleF r = new RectangleF();
            if (points != null && points.Length >= 1)
            {
                float xMin = float.MaxValue, xMax = float.MinValue;
                float yMin = float.MaxValue, yMax = float.MinValue;
                for (int i = 0; i < points.Length; i++)
                {
                    if (points[i].X > xMax)
                    {
                        xMax = points[i].X;
                    }
                    if (points[i].X < xMin)
                    {
                        xMin = points[i].X;
                    }
                    if (points[i].Y > yMax)
                    {
                        yMax = points[i].Y;
                    }
                    if (points[i].Y < yMin)
                    {
                        yMin = points[i].Y;
                    }
                }
                r.X = xMin;
                r.Y = yMin;
                r.Width = xMax - xMin;
                r.Height = yMax - yMin;
            }
            return r;
        }
        //��һϵ�о��λ�ð�Χ���ǵľ������
        public static RectangleF getRectsBox(List<RectangleF> rects)
        {
            RectangleF r = new RectangleF();
            if (rects != null && rects.Count >= 1)
            {
                float xMin = float.MaxValue, xMax = float.MinValue;
                float yMin = float.MaxValue, yMax = float.MinValue;
                for (int i = 0; i < rects.Count; i++)
                {
                    if (rects[i].X + rects[i].Width > xMax)
                    {
                        xMax = rects[i].X + rects[i].Width;
                    }
                    if (rects[i].X < xMin)
                    {
                        xMin = rects[i].X;
                    }
                    if (rects[i].Y + rects[i].Height > yMax)
                    {
                        yMax = rects[i].Y + rects[i].Height;
                    }
                    if (rects[i].Y < yMin)
                    {
                        yMin = rects[i].Y;
                    }
                }
                r.X = xMin;
                r.Y = yMin;
                r.Width = xMax - xMin;
                r.Height = yMax - yMin;
            }
            return r;
        }
        public enum BoderType
        {
            BoderLeft,
            BoderRight,
            BoderTop,
            BoderBottom,
        }
        //����ָ�������з�ʽ����С�������о���
        public static void sortRectangles(List<RectangleF> boxs,BoderType boderType)
        {
            List<RectangleF> boxsTemp = new List<RectangleF>();
            for (int i = 0; i < boxs.Count; i++)
            {
                bool find = false;
                for (int j = 0; j < boxsTemp.Count; j++)
                {
                    switch (boderType)
                    {
                        case BoderType.BoderLeft:
                            find = boxs[i].X < boxsTemp[j].X;
                            break;
                        case BoderType.BoderTop:
                            find = boxs[i].Y < boxsTemp[j].Y;
                            break;
                        case BoderType.BoderRight:
                            find = boxs[i].X + boxs[i].Width < boxsTemp[j].X + boxsTemp[j].Width;
                            break;
                        case BoderType.BoderBottom:
                            find = boxs[i].Y + boxs[i].Height < boxsTemp[j].Y + boxsTemp[j].Height;
                            break;
                    }
                    if (find)
                    {
                        boxsTemp.Insert(j, boxs[i]);
                        break;
                    }
                }
                if (!find)
                {
                    boxsTemp.Add(boxs[i]);
                }
            }
            boxs.Clear();
            for (int i = 0; i < boxsTemp.Count; i++)
            {
                boxs.Add(boxsTemp[i]);
            }
            boxsTemp.Clear();
        }
        //����ָ�������з�ʽ����С�������о��Σ�������к�Ľ��˳��
        public static List<int> getRectanglesOrder(List<RectangleF> boxs, BoderType boderType)
        {
            List<RectangleF> boxsTemp = new List<RectangleF>();
            List<int> order = new List<int>();
            for (int i = 0; i < boxs.Count; i++)
            {
                bool find = false;
                for (int j = 0; j < boxsTemp.Count; j++)
                {
                    switch (boderType)
                    {
                        case BoderType.BoderLeft:
                            find = boxs[i].X < boxsTemp[j].X;
                            break;
                        case BoderType.BoderTop:
                            find = boxs[i].Y < boxsTemp[j].Y;
                            break;
                        case BoderType.BoderRight:
                            find = boxs[i].X + boxs[i].Width < boxsTemp[j].X + boxsTemp[j].Width;
                            break;
                        case BoderType.BoderBottom:
                            find = boxs[i].Y + boxs[i].Height < boxsTemp[j].Y + boxsTemp[j].Height;
                            break;
                    }
                    if (find)
                    {
                        boxsTemp.Insert(j, boxs[i]);
                        order.Insert(j, i);
                        break;
                    }
                }
                if (!find)
                {
                    boxsTemp.Add(boxs[i]);
                    order.Add(i);
                }
            }
            boxsTemp.Clear();
            return order;
        }
        //��ȡ���ڵ���ָ��ֵ��2�ı�������ֵ
        public static int getMultipleOfTwo(int number)
        {
            int numberNew = 2;
            while (numberNew < number)
            {
                numberNew *= 2;
            }
            return numberNew;
        }
        //��ȡ���ڵ���ָ��ֵ��2�ı����ĳߴ�
        public static Size getMultipleOfTwo(Size size)
        {
            size.Width = getMultipleOfTwo(size.Width);
            size.Height = getMultipleOfTwo(size.Height);
            return size;
        }
    }

}