using System;
namespace Cyclone.alg.math
{
    public class Vector3
    {

	    private static Matrix4 martix4Temp = new Matrix4();// ��ʱ����
	    private static Vector3 vector3Temp = new Vector3();// ��ʱʸ��
	    public float x, y, z;

	    // ���캯��
	    public Vector3(float x, float y, float z)
	    {
		    this.x = x;
		    this.y = y;
		    this.z = z;
	    }

	    public Vector3()
	    {
		    x = y = z = 0;
	    }

	    public Vector3(Vector3 v)
	    {
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
	    }

	    /**
	     * �ж��Ƿ�Ϊ������
	     * 
	     * @return bool
	     */
	    public bool isZeroVector()
	    {
		    if (Math.Abs(x) == 0 && Math.Abs(y) == 0 && Math.Abs(z) == 0)
		    {
			    return true;
		    }
		    return false;
	    }

	    /**
	     * ���ȼ���
	     * 
	     * @return float ����
	     */
	    public float size()
	    {
		    float t = x * x + y * y + z * z;
		    t = (float) Math.Sqrt(t);
		    return t;
	    }

	    /**
	     * ���ȵ�ƽ��
	     * 
	     * @return float
	     */
	    public float size2()
	    {
		    float t = (x * x + y * y + z * z);
		    return t;
	    }

	    /**
	     * ��ֵ
	     * 
	     * @param x
	     * @param y
	     * @param z
	     */
	    public void setValue(float x, float y, float z)
	    {
		    this.x = x;
		    this.y = y;
		    this.z = z;
	    }

	    /**
	     * ��ֵ
	     * 
	     * @param v
	     */
	    public void setValue(Vector3 v)
	    {
		    this.x = v.x;
		    this.y = v.y;
		    this.z = v.z;
	    }
	    /**
	     * ��ȡ��ֵ�����buffer����>=4������ĸ���Ԫ������Ϊ1
	     * @param buffer[]
	     */
	    public void getValue(float[] buffer)
	    {
		    if(buffer==null||buffer.Length<3)
		    {
			    return;
		    }
		    buffer[0]=x;
		    buffer[1]=y;
		    buffer[2]=z;
		    if(buffer.Length>=4)
		    {
			    buffer[3]=1;
		    }
	    }
	    /**
	     * ������ֵ�����buffer����>=4���ҵ��ĸ���Ԫ��Ϊ1����ô������ʱ����Ե��ĸ���Ԫ
	     * @param buffer[]
	     */
        public void setValue(float[] buffer)
	    {
		    if(buffer==null||buffer.Length<3)
		    {
			    return;
		    }
		    if(buffer.Length>=4&&buffer[3]!=1)
		    {
			    x=buffer[0]/buffer[3];
			    y=buffer[1]/buffer[3];
			    z=buffer[2]/buffer[3];
		    }
		    else
		    {
			    x=buffer[0];
			    y=buffer[1];
			    z=buffer[2];
		    }
	    }
	    /**
	     * ʸ���Ĳ��
	     * 
	     * @param a
	     *            ��1��ʸ��
	     * @param b
	     *            ��2��ʸ��
	     * @return Vector3 ����ʸ����˲�������ʸ��
	     */
	    public static Vector3 crossProduct(Vector3 a, Vector3 b)
	    {
		    Vector3 c = new Vector3();
		    c.x = (a.y * b.z - b.y * a.z);
		    c.y = (a.z * b.x - b.z * a.x);
		    c.z = (a.x * b.y - b.x * a.y);
		    return c;
	    }

	    /**
	     * ʸ���Ĳ��
	     * 
	     * @param b
	     *            ʸ��b
	     * @return Vector3 ��ǰʸ����ʸ��b����ʸ����˲�������ʸ��
	     */
	    public Vector3 crossProduct(Vector3 b)
	    {
		    Vector3 c = new Vector3();
		    c.x = (this.y * b.z - b.y * this.z);
		    c.y = (this.z * b.x - b.z * this.x);
		    c.z = (this.x * b.y - b.x * this.y);
		    return c;
	    }

	    /**
	     * ʸ�����
	     * 
	     * @param a
	     *            ʸ��a
	     * @return float ��ǰʸ����ʸ��a����ʸ����˵Ľ��
	     */
	    public float innerProduct(Vector3 a)
	    {
		    float t = (this.x * a.x + this.y * a.y + this.z * a.z);
		    return t;
	    }

	    /**
	     * ʸ�����
	     * 
	     * @param x
	     *            ָ��ʸ��X����
	     * @param y
	     *            ָ��ʸ��Y����
	     * @param z
	     *            ָ��ʸ��Z����
	     * @return float ��ǰʸ����ָ��ʸ������ʸ����˵Ľ��
	     */
	    public float innerProduct(float x, float y, float z)
	    {
		    float t = (this.x * x + this.y * y + this.z * z);
		    return t;
	    }

	    /**
	     * ʸ�����
	     * 
	     * @param a
	     *            ָ��ʸ��a
	     * @param b
	     *            ָ��ʸ��b
	     * @return ָ��ʸ��a��ָ��ʸ��b����ʸ����˵Ľ��
	     */
	    public static float innerProduct(Vector3 a, Vector3 b)
	    {
		    float t = (a.x * b.x + a.y * b.y + a.z * b.z);
		    return t;
	    }

	    /**
	     * ʸ����������(����)
	     * 
	     * @param t
	     */
	    public void scale(float t)
	    {
		    this.x = (this.x * t);
		    this.y = (this.y * t);
		    this.z = (this.z * t);
	    }

	    /**
	     * �����ӷ�
	     * 
	     * @param v
	     */
	    public void add(Vector3 v)
	    {
		    this.x += v.x;
		    this.y += v.y;
		    this.z += v.z;
	    }

	    /**
	     * ��������
	     * 
	     * @param v
	     */
	    public void subtract(Vector3 v)
	    {
		    this.x -= v.x;
		    this.y -= v.y;
		    this.z -= v.z;
	    }

	    /**
	     * Χ��X����ת
	     * 
	     * @param theta
	     *            ����
	     */
	    public void rotateX(float theta)
	    {
		    vector3Temp.setValue(1, 0, 0);
		    rotate(vector3Temp, theta);
	    }

	    /**
	     * Χ��Y����ת
	     * 
	     * @param theta
	     *            ����
	     */
	    public void rotateY(float theta)
	    {
		    vector3Temp.setValue(0, 1, 0);
		    rotate(vector3Temp, theta);
	    }

	    /**
	     * Χ��Z����ת
	     * 
	     * @param theta
	     *            ����
	     */
	    public void rotateZ(float theta)
	    {
		    vector3Temp.setValue(0, 0, 1);
		    rotate(vector3Temp, theta);
	    }

	    /**
	     * Χ����������ת
	     * 
	     * @param theta
	     *            ����
	     */
	    public void rotate(Vector3 v, float theta)
	    {
		    martix4Temp.setToRotate(v, theta);
		    multiplyBy(martix4Temp);
	    }

	    /**
	     * 4x4����ͱ������ĳ˻�(����ữ��w=1)
	     * 
	     * @param m
	     *            ����
	     */
	    public void multiplyBy(Matrix4 m)
	    {
		    float wT = 1.0f / (m.data[Matrix4.M30] * x + m.data[Matrix4.M31] * y + m.data[Matrix4.M32] * z + m.data[Matrix4.M33]);
		    x = (m.data[Matrix4.M00] * x + m.data[Matrix4.M01] * y + m.data[Matrix4.M02] * z + m.data[Matrix4.M03]) * wT;
		    y = (m.data[Matrix4.M10] * x + m.data[Matrix4.M11] * y + m.data[Matrix4.M12] * z + m.data[Matrix4.M13]) * wT;
		    z = (m.data[Matrix4.M20] * x + m.data[Matrix4.M21] * y + m.data[Matrix4.M22] * z + m.data[Matrix4.M23]) * wT;
	    }

	    /**
	     * 3x3����������m�ĳ˻�(����W)
	     * 
	     * @param m
	     *            ����
	     */
	    public void multiplyBy(Matrix3 m)
	    {
		    x = (m.data[0] * x + m.data[1] * y + m.data[2] * z);
		    y = (m.data[3] * x + m.data[4] * y + m.data[5] * z);
		    z = (m.data[6] * x + m.data[7] * y + m.data[8] * z);
	    }

	    // ��ӡ��Ϣ

	    public void show(String s)
	    {
		    Console.WriteLine("--*v* "+s+"  (x,y,z)=(" + x + "," + y + "," + z + ")");
	    }
    }
}