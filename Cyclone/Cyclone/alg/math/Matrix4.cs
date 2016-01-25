using System;
using System.Drawing;
namespace Cyclone.alg.math
{
    public class Matrix4
    {
	    public float[] data; // ��ֵ
        public const int M00 = 0;// 0;
        public const int M01 = 4;// 1;
        public const int M02 = 8;// 2;
        public const int M03 = 12;// 3;
        public const int M10 = 1;// 4;
        public const int M11 = 5;// 5;
        public const int M12 = 9;// 6;
        public const int M13 = 13;// 7;
        public const int M20 = 2;// 8;
        public const int M21 = 6;// 9;
        public const int M22 = 10;// 10;
        public const int M23 = 14;// 11;
        public const int M30 = 3;// 12;
        public const int M31 = 7;// 13;
        public const int M32 = 11;// 14;
        public const int M33 = 15;// 15;
	    private static  Vector3 vector3Temp = new Vector3();// ��ʱ��ά����
	    private static Matrix4 matrix4Temp = new Matrix4();// ��ʱ��ά����
	    private static float[] f4_Temp0 = new float[4];// ��ʱ��������

        /**
         * ���캯��
         * 
         * @param b
         */
        public Matrix4(float[] b)
        {
            setData(b);
        }

        public Matrix4()
        {
            this.data = new float[16];
            identity();
        }

        public Matrix4(Matrix4 t)
        {
            setData(t.data);
        }
        private void setData(float[] b)
        {
            this.data = new float[16];
            for (int i = 0; i < 16; i++)
            {
                this.data[i] = b[i];
            }
        }

	    /**
	     * ��������(��ɵ�λ����)
	     */
	    public void identity()
	    {
		    if (data == null)
		    {
                throw new Exception();
		    }
		    for (int i = 0; i < 16; i++)
		    {
			    this.data[i] = 0;
		    }
		    this.data[M00] = this.data[M11] = this.data[M22] = this.data[M33] = 1;
	    }

	    /**
	     * ��4*4����������ֵ
	     * 
	     * @param matrix
	     */
	    public void setValue(Matrix4 matrix)
	    {
		    if (matrix == null)
		    {
			    return;
		    }
		    for (int i = 0; i < data.Length; i++)
		    {
			    this.data[i] = matrix.data[i];
		    }
	    }

	    // /**
	    // * ��3*3����������ֵ
	    // *
	    // * @param matrix
	    // */
	    // public void setValue(Matrix3 matrix)
	    // {
	    // if (matrix == null)
	    // {
	    // return;
	    // }
	    // this.identity();
	    // data[M00] = matrix.data[Matrix3.M00];
	    // data[M01] = matrix.data[Matrix3.M01];
	    // data[M03] = matrix.data[Matrix3.M02];
	    // data[M10] = matrix.data[Matrix3.M10];
	    // data[M11] = matrix.data[Matrix3.M11];
	    // data[M13] = matrix.data[Matrix3.M12];
	    // data[M20] = matrix.data[Matrix3.M20];
	    // data[M21] = matrix.data[Matrix3.M21];
	    // data[M33] = matrix.data[Matrix3.M22];
	    // }

	    /**
	     * ������ֵ
	     * 
	     * @return
	     */
	    public float[] getValue()
	    {
		    return data;
	    }

	    /**
	     * ����4��4����ĳ˻����������������ǰ���󣨵�ǰ�������M����
	     * 
	     * @param m
	     */
	    public void multiply(Matrix4 m)
	    {
		    multiply(this.getValue(), m.getValue(), this.getValue());
	    }

	    /**
	     * ����4��4����ĳ˻�����ǰ�������M�����������ֵ������ľ��󡣣�
	     * 
	     * @param m
	     * @param res
	     */
	    public void multiply(Matrix4 m, Matrix4 res)
	    {
		    multiply(this.getValue(), m.getValue(), res.getValue());
	    }

	    /**
	     * ����4��4����ĳ˻���m1*m2->m3��
	     * 
	     * @param m
	     * @param res
	     */
	    private static void multiply(float[] mata, float[] matb, float[] matc)
        {
	        float[] tmp=new float[16];
	        tmp[M00] = mata[M00] * matb[M00] + mata[M01] * matb[M10] + mata[M02] * matb[M20] + mata[M03] * matb[M30];
	        tmp[M01] = mata[M00] * matb[M01] + mata[M01] * matb[M11] + mata[M02] * matb[M21] + mata[M03] * matb[M31];
	        tmp[M02] = mata[M00] * matb[M02] + mata[M01] * matb[M12] + mata[M02] * matb[M22] + mata[M03] * matb[M32];
	        tmp[M03] = mata[M00] * matb[M03] + mata[M01] * matb[M13] + mata[M02] * matb[M23] + mata[M03] * matb[M33];
	        tmp[M10] = mata[M10] * matb[M00] + mata[M11] * matb[M10] + mata[M12] * matb[M20] + mata[M13] * matb[M30];
	        tmp[M11] = mata[M10] * matb[M01] + mata[M11] * matb[M11] + mata[M12] * matb[M21] + mata[M13] * matb[M31];
	        tmp[M12] = mata[M10] * matb[M02] + mata[M11] * matb[M12] + mata[M12] * matb[M22] + mata[M13] * matb[M32];
	        tmp[M13] = mata[M10] * matb[M03] + mata[M11] * matb[M13] + mata[M12] * matb[M23] + mata[M13] * matb[M33];
	        tmp[M20] = mata[M20] * matb[M00] + mata[M21] * matb[M10] + mata[M22] * matb[M20] + mata[M23] * matb[M30];
	        tmp[M21] = mata[M20] * matb[M01] + mata[M21] * matb[M11] + mata[M22] * matb[M21] + mata[M23] * matb[M31];
	        tmp[M22] = mata[M20] * matb[M02] + mata[M21] * matb[M12] + mata[M22] * matb[M22] + mata[M23] * matb[M32];
	        tmp[M23] = mata[M20] * matb[M03] + mata[M21] * matb[M13] + mata[M22] * matb[M23] + mata[M23] * matb[M33];
	        tmp[M30] = mata[M30] * matb[M00] + mata[M31] * matb[M10] + mata[M32] * matb[M20] + mata[M33] * matb[M30];
	        tmp[M31] = mata[M30] * matb[M01] + mata[M31] * matb[M11] + mata[M32] * matb[M21] + mata[M33] * matb[M31];
	        tmp[M32] = mata[M30] * matb[M02] + mata[M31] * matb[M12] + mata[M32] * matb[M22] + mata[M33] * matb[M32];
	        tmp[M33] = mata[M30] * matb[M03] + mata[M31] * matb[M13] + mata[M32] * matb[M23] + mata[M33] * matb[M33];
            tmp.CopyTo(matc, 0);

        }

	    // ת�þ�������
	    private static  int[] TR = { 0, 4, 8, 12, 1, 5, 9, 13, 2, 6, 10, 14, 3, 7, 11, 15 };

	    /**
	     * 4x4����ת�ã�������к��л�������
	     */
	    public Matrix4 transpose()
	    {
		    Matrix4 m = new Matrix4();
		    for (int i = 0; i < 16; i++)
		    {
			    m.data[i] = data[TR[i]];
		    }
		    return m;
	    }

	    /**
	     * �淴���󣬽������ֵ���Լ�
	     */
	    public bool inverse()
	    {
		    return inverse(this.getValue(), this.getValue());
	    }

	    /**
	     * �淴���󣬽������ֵ��ָ�����
	     */
	    public bool inverse(Matrix4 res)
	    {
		    return inverse(this.getValue(), res.getValue());
	    }

        /**
         * �淴����inverse(m1)->m2��
         * 
         * @param m
         * @param res
         */
        private static bool inverse(float[] matA, float[] matB)
        {
            bool result = matrix4_inv(matA, matB);
            return result;
        }

	    /**
	     * ��������ʽ������ʽ���ļ���
	     * 
	     * @return
	     */
	    public float det()
	    {
		    return det(this.getValue());
	    }

	    private static float det(float[] val)
        {
	        return val[M30] * val[M21] * val[M12] * val[M03] - val[M20] * val[M31] * val[M12] * val[M03] - val[M30] * val[M11]
			        * val[M22] * val[M03] + val[M10] * val[M31] * val[M22] * val[M03] + val[M20] * val[M11] * val[M32] * val[M03] - val[M10]
			        * val[M21] * val[M32] * val[M03] - val[M30] * val[M21] * val[M02] * val[M13] + val[M20] * val[M31] * val[M02] * val[M13]
			        + val[M30] * val[M01] * val[M22] * val[M13] - val[M00] * val[M31] * val[M22] * val[M13] - val[M20] * val[M01] * val[M32]
			        * val[M13] + val[M00] * val[M21] * val[M32] * val[M13] + val[M30] * val[M11] * val[M02] * val[M23] - val[M10] * val[M31]
			        * val[M02] * val[M23] - val[M30] * val[M01] * val[M12] * val[M23] + val[M00] * val[M31] * val[M12] * val[M23] + val[M10]
			        * val[M01] * val[M32] * val[M23] - val[M00] * val[M11] * val[M32] * val[M23] - val[M20] * val[M11] * val[M02] * val[M33]
			        + val[M10] * val[M21] * val[M02] * val[M33] + val[M20] * val[M01] * val[M12] * val[M33] - val[M00] * val[M21] * val[M12]
			        * val[M33] - val[M10] * val[M01] * val[M22] * val[M33] + val[M00] * val[M11] * val[M22] * val[M33];
        }

	    /**
	     * ��ˣ�������Ӳ�����һ��Χ�����������ת����
	     * 
	     * @param v
	     * @param theta
	     *            ����
	     */
	    public void postRotate(Vector3 v, float theta)
	    {
		    matrix4Temp.setToRotate(v, theta);
		    matrix4Temp.multiply(this, this);
	    }

	    /**
	     * ��ˣ�������Ӳ�����һ��Χ��Z�����ת����
	     * 
	     * @param theta
	     *            ����
	     */
	    public void postRotate(float theta)
	    {
		    matrix4Temp.setToRotate(theta);
		    matrix4Temp.multiply(this, this);
	    }
	    /**
	     * ��ˣ�������Ӳ�����һ��Z==0ƽ���λ�ƾ���
	     * 
	     * @param offX
	     * @param offY
	     */
	    public void postTranslate(float offX, float offY)
	    {
		    postTranslate(offX,offY,0.0f);
	    }
	    /**
	     * ��ˣ�������Ӳ�����һ��λ�ƾ���
	     * 
	     * @param offX
	     * @param offY
	     * @param offZ
	     */
	    public void postTranslate(float offX, float offY, float offZ)
	    {
		    matrix4Temp.setToTranslate(offX, offY, offZ);
		    matrix4Temp.multiply(this, this);
	    }
	    /**
	     * ��ˣ�������Ӳ�����һ��XY��������ž���(Z����==1)
	     * 
	     * @param scaleX
	     * @param scaleY
	     * @param scaleZ
	     */
	    public void postScale(float scaleX, float scaleY)
	    {
		    postScale(scaleX,scaleY,1.0f);
	    }
	    /**
	     * ��ˣ�������Ӳ�����һ�����ž���
	     * 
	     * @param scaleX
	     * @param scaleY
	     * @param scaleZ
	     */
	    public void postScale(float scaleX, float scaleY, float scaleZ)
	    {
		    matrix4Temp.setToScale(scaleX, scaleY, scaleZ);
		    matrix4Temp.multiply(this, this);
	    }

	    /**
	     * ��ˣ�������Ӳ�����һ��Z==0ƽ����о���
	     * 
	     * @param shearX
	     * @param shearY
	     */
	    public void postShear(float shearX, float shearY)
	    {
		    matrix4Temp.setToWarp(shearX, shearY);
		    matrix4Temp.multiply(this, this);
	    }

	    /**
	     * �ҳˣ����ȵ��Ӳ�����һ��Χ�����������ת����
	     * 
	     * @param v
	     * @param theta
	     *            ����
	     */
	    public void preRotate(Vector3 v, float theta)
	    {
		    matrix4Temp.setToRotate(v, theta);
		    multiply(matrix4Temp);
	    }

	    /**
	     * �ҳˣ����ȵ��Ӳ�����һ��Χ��Z�����ת����
	     * 
	     * @param theta
	     *            ����
	     */
	    public void preRotate(float theta)
	    {
		    matrix4Temp.setToRotate(theta);
		    multiply(matrix4Temp);
	    }
	    /**
	     * �ҳˣ����ȵ��Ӳ�����һ��Z==0ƽ��λ�ƾ���
	     * 
	     * @param offX
	     * @param offY
	     */
	    public void preTranslate(float offX, float offY)
	    {
		    preTranslate(offX,offY,0.0f);
	    }
	    /**
	     * �ҳˣ����ȵ��Ӳ�����һ��λ�ƾ���
	     * 
	     * @param offX
	     * @param offY
	     * @param offZ
	     */
	    public void preTranslate(float offX, float offY, float offZ)
	    {
		    matrix4Temp.setToTranslate(offX, offY, offZ);
		    multiply(matrix4Temp);
	    }
	    /**
	     * �ҳˣ����ȵ��Ӳ�����һ��XY��������ž���(Z����==1)
	     * 
	     * @param scaleX
	     * @param scaleY
	     */
	    public void preScale(float scaleX, float scaleY)
	    {
		    preScale(scaleX, scaleY, 1.0f);
	    }
	    /**
	     * �ҳˣ����ȵ��Ӳ�����һ�����ž���
	     * 
	     * @param scaleX
	     * @param scaleY
	     * @param scaleZ
	     */
	    public void preScale(float scaleX, float scaleY, float scaleZ)
	    {
		    matrix4Temp.setToScale(scaleX, scaleY, scaleZ);
		    multiply(matrix4Temp);
	    }

	    /**
	     * �ҳˣ����ȵ��Ӳ�����һ��Z==0ƽ����о���
	     * 
	     * @param shearX
	     * @param shearY
	     */
	    public void preShear(float shearX, float shearY)
	    {
		    matrix4Temp.setToWarp(shearX, shearY);
		    multiply(matrix4Temp);
	    }

	    /**
	     * ���ó�Χ�����������ת����
	     * 
	     * @param v
	     * @param theta
	     *            ����
	     */
	    public void setToRotate(Vector3 v, float theta)
	    {
		    float size2 = v.size2();
		    float size = v.size();
		    float s = (float) Math.Sin(theta);
		    float c = (float) Math.Cos(theta);
		    float t = (1) - c;
		    this.identity();
		    data[M00] = t * v.x * v.x / size2 + c;
		    data[M01] = t * v.x * v.y / size2 - (v.z * s / size);
		    data[M02] = t * v.z * v.x / size2 + (v.y * s / size);
		    data[M10] = t * v.x * v.y / size2 + (v.z * s / size);
		    data[M11] = t * v.y * v.y / size2 + c;
		    data[M12] = t * v.y * v.z / size2 - (v.x * s / size);
		    data[M20] = t * v.z * v.x / size2 - (v.y * s / size);
		    data[M21] = t * v.y * v.z / size2 + (v.x * s / size);
		    data[M22] = t * v.z * v.z / size2 + c;
	    }

	    /**
	     * ���ó�Χ��Z�����ת����
	     * 
	     * @param theta
	     *            ����
	     */
	    public void setToRotate(float theta)
	    {
		    vector3Temp.setValue(0, 0, 1);
		    setToRotate(vector3Temp, theta);
	    }

	    /**
	     * ���ó�Z=0ƽ���λ�ƾ���
	     * 
	     * @param offX
	     * @param offY
	     */
	    public void setToTranslate(float offX, float offY)
	    {
		    setToTranslate(offX, offY, 0.0f);
	    }

	    /**
	     * ���ó�λ�ƾ���
	     * 
	     * @param offX
	     * @param offY
	     * @param offZ
	     */
	    public void setToTranslate(float offX, float offY, float offZ)
	    {
		    this.identity();
		    data[M03] = offX;
		    data[M13] = offY;
		    data[M23] = offZ;
	    }
	    /**
	     * ���ó�Z==0ƽ������ž���
	     * 
	     * @param scaleX
	     * @param scaleY
	     */
	    public void setToScale(float scaleX, float scaleY)
	    {
		    setToScale(scaleX, scaleY,0.0f);
	    }
	    /**
	     * ���ó����ž���
	     * 
	     * @param scaleX
	     * @param scaleY
	     * @param scaleZ
	     */
	    public void setToScale(float scaleX, float scaleY, float scaleZ)
	    {
		    this.identity();
		    data[M00] = scaleX;
		    data[M11] = scaleY;
		    data[M22] = scaleZ;
	    }

	    /**
	     * ���ó�Z==0ƽ����о���
	     * 
	     * @param shearX
	     * @param shearY
	     */
	    public void setToWarp(float shearX, float shearY)
	    {
		    this.identity();
		    data[M01] = shearX;
		    data[M10] = shearY;
	    }

	    /**
	     * �ͷ���Դ
	     */
	    public void releaseRes()
	    {
		    if (data != null)
		    {
			    data = null;
		    }
	    }

	    /**
	     * ������Ӧ�õ�ʸ��(���ʸ��)�����������Żش�ʸ��
	     * 
	     * @param vector
	     */
	    public void mapVector(Vector3 vector)
	    {
		    mapVector(vector, vector);
	    }

	    /**
	     * ������Ӧ�õ�ʸ��A(���ʸ��A)�����������Ż�ʸ��B
	     * 
	     * @param vectorA
	     *            ʸ��A
	     * @param vectorB
	     *            ʸ��B
	     */
	    public void mapVector(Vector3 vectorA, Vector3 vectorB)
	    {
		    vectorA.getValue(f4_Temp0);
		    mapVector(this.getValue(), f4_Temp0, f4_Temp0);
		    vectorB.setValue(f4_Temp0);
	    }

	    /**
	     * ������m1Ӧ�õ�ʸ��A(���ʸ��A)�����������Ż�ʸ��B
	     * 
	     * @param m1
	     *            ����m1
	     * @param va
	     *            ʸ��A
	     * @param vb
	     *            ʸ��B
	     */
        private static void mapVector(float[] matrix, float[] va, float[] vb)
        {
	        float []tmp=new float[4];
	        tmp[0] = matrix[M00] * va[0] + matrix[M01] * va[1] + matrix[M02] * va[2] + matrix[M03] * va[3];
	        tmp[1] = matrix[M10] * va[0] + matrix[M11] * va[1] + matrix[M12] * va[2] + matrix[M13] * va[3];
	        tmp[2] = matrix[M20] * va[0] + matrix[M21] * va[1] + matrix[M22] * va[2] + matrix[M23] * va[3];
	        tmp[3] = matrix[M30] * va[0] + matrix[M31] * va[1] + matrix[M32] * va[2] + matrix[M33] * va[3];
            tmp.CopyTo(vb, 0);
        }
        /**
         * �淴����inverse(m1)->m2��
         * 
         * @param m
         * @param res
         */
        private static bool matrix4_inv(float[] val, float[] res)
        {
            float[] tmp = new float[16];
            float l_det = det(val);
            if (l_det == 0) return false;
            tmp[M00] = val[M12] * val[M23] * val[M31] - val[M13] * val[M22] * val[M31] + val[M13] * val[M21] * val[M32] - val[M11]
                * val[M23] * val[M32] - val[M12] * val[M21] * val[M33] + val[M11] * val[M22] * val[M33];
            tmp[M01] = val[M03] * val[M22] * val[M31] - val[M02] * val[M23] * val[M31] - val[M03] * val[M21] * val[M32] + val[M01]
                * val[M23] * val[M32] + val[M02] * val[M21] * val[M33] - val[M01] * val[M22] * val[M33];
            tmp[M02] = val[M02] * val[M13] * val[M31] - val[M03] * val[M12] * val[M31] + val[M03] * val[M11] * val[M32] - val[M01]
                * val[M13] * val[M32] - val[M02] * val[M11] * val[M33] + val[M01] * val[M12] * val[M33];
            tmp[M03] = val[M03] * val[M12] * val[M21] - val[M02] * val[M13] * val[M21] - val[M03] * val[M11] * val[M22] + val[M01]
                * val[M13] * val[M22] + val[M02] * val[M11] * val[M23] - val[M01] * val[M12] * val[M23];
            tmp[M10] = val[M13] * val[M22] * val[M30] - val[M12] * val[M23] * val[M30] - val[M13] * val[M20] * val[M32] + val[M10]
                * val[M23] * val[M32] + val[M12] * val[M20] * val[M33] - val[M10] * val[M22] * val[M33];
            tmp[M11] = val[M02] * val[M23] * val[M30] - val[M03] * val[M22] * val[M30] + val[M03] * val[M20] * val[M32] - val[M00]
                * val[M23] * val[M32] - val[M02] * val[M20] * val[M33] + val[M00] * val[M22] * val[M33];
            tmp[M12] = val[M03] * val[M12] * val[M30] - val[M02] * val[M13] * val[M30] - val[M03] * val[M10] * val[M32] + val[M00]
                * val[M13] * val[M32] + val[M02] * val[M10] * val[M33] - val[M00] * val[M12] * val[M33];
            tmp[M13] = val[M02] * val[M13] * val[M20] - val[M03] * val[M12] * val[M20] + val[M03] * val[M10] * val[M22] - val[M00]
                * val[M13] * val[M22] - val[M02] * val[M10] * val[M23] + val[M00] * val[M12] * val[M23];
            tmp[M20] = val[M11] * val[M23] * val[M30] - val[M13] * val[M21] * val[M30] + val[M13] * val[M20] * val[M31] - val[M10]
                * val[M23] * val[M31] - val[M11] * val[M20] * val[M33] + val[M10] * val[M21] * val[M33];
            tmp[M21] = val[M03] * val[M21] * val[M30] - val[M01] * val[M23] * val[M30] - val[M03] * val[M20] * val[M31] + val[M00]
                * val[M23] * val[M31] + val[M01] * val[M20] * val[M33] - val[M00] * val[M21] * val[M33];
            tmp[M22] = val[M01] * val[M13] * val[M30] - val[M03] * val[M11] * val[M30] + val[M03] * val[M10] * val[M31] - val[M00]
                * val[M13] * val[M31] - val[M01] * val[M10] * val[M33] + val[M00] * val[M11] * val[M33];
            tmp[M23] = val[M03] * val[M11] * val[M20] - val[M01] * val[M13] * val[M20] - val[M03] * val[M10] * val[M21] + val[M00]
                * val[M13] * val[M21] + val[M01] * val[M10] * val[M23] - val[M00] * val[M11] * val[M23];
            tmp[M30] = val[M12] * val[M21] * val[M30] - val[M11] * val[M22] * val[M30] - val[M12] * val[M20] * val[M31] + val[M10]
                * val[M22] * val[M31] + val[M11] * val[M20] * val[M32] - val[M10] * val[M21] * val[M32];
            tmp[M31] = val[M01] * val[M22] * val[M30] - val[M02] * val[M21] * val[M30] + val[M02] * val[M20] * val[M31] - val[M00]
                * val[M22] * val[M31] - val[M01] * val[M20] * val[M32] + val[M00] * val[M21] * val[M32];
            tmp[M32] = val[M02] * val[M11] * val[M30] - val[M01] * val[M12] * val[M30] - val[M02] * val[M10] * val[M31] + val[M00]
                * val[M12] * val[M31] + val[M01] * val[M10] * val[M32] - val[M00] * val[M11] * val[M32];
            tmp[M33] = val[M01] * val[M12] * val[M20] - val[M02] * val[M11] * val[M20] + val[M02] * val[M10] * val[M21] - val[M00]
                * val[M12] * val[M21] - val[M01] * val[M10] * val[M22] + val[M00] * val[M11] * val[M22];

            float inv_det = 1.0f / l_det;
            res[M00] = tmp[M00] * inv_det;
            res[M01] = tmp[M01] * inv_det;
            res[M02] = tmp[M02] * inv_det;
            res[M03] = tmp[M03] * inv_det;
            res[M10] = tmp[M10] * inv_det;
            res[M11] = tmp[M11] * inv_det;
            res[M12] = tmp[M12] * inv_det;
            res[M13] = tmp[M13] * inv_det;
            res[M20] = tmp[M20] * inv_det;
            res[M21] = tmp[M21] * inv_det;
            res[M22] = tmp[M22] * inv_det;
            res[M23] = tmp[M23] * inv_det;
            res[M30] = tmp[M30] * inv_det;
            res[M31] = tmp[M31] * inv_det;
            res[M32] = tmp[M32] * inv_det;
            res[M33] = tmp[M33] * inv_det;
            return true;
        }
	    /**
	     * ��ʾ�˾�������
	     * 
	     * @param txt
	     */
	    public void show(String txt)
	    {
		    Console.Write("--" + txt + " --[");
		    for (int i = 0; i < data.Length; i++)
		    {
			    Console.Write(data[i] + ",");
		    }
            Console.WriteLine("]");
	    }
        /**
         * ��ӳ����ϢӦ�õ�ÿ������
         */
        public void TransformPoints(PointF[] points)
        {
            if (points == null)
            {
                return;
            }
            for (int i = 0; i < points.Length; i++)
            {
                f4_Temp0[0] = points[i].X;
                f4_Temp0[1] = points[i].Y;
                f4_Temp0[2] = 0;
                f4_Temp0[3] = 1;
                mapVector(this.getValue(), f4_Temp0, f4_Temp0);
                points[i].X = f4_Temp0[0];
                points[i].Y = f4_Temp0[1];
            }
        }
    }
}