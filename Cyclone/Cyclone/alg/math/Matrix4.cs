using System;
using System.Drawing;
namespace Cyclone.alg.math
{
    public class Matrix4
    {
	    public float[] data; // 数值
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
	    private static  Vector3 vector3Temp = new Vector3();// 临时三维向量
	    private static Matrix4 matrix4Temp = new Matrix4();// 临时四维矩阵
	    private static float[] f4_Temp0 = new float[4];// 临时浮点数组

        /**
         * 构造函数
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
	     * 矩阵重置(变成单位矩阵)
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
	     * 从4*4矩阵设置数值
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
	    // * 从3*3矩阵设置数值
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
	     * 返回数值
	     * 
	     * @return
	     */
	    public float[] getValue()
	    {
		    return data;
	    }

	    /**
	     * 两个4×4矩阵的乘积，并将结果赋给当前矩阵（当前矩阵左乘M。）
	     * 
	     * @param m
	     */
	    public void multiply(Matrix4 m)
	    {
		    multiply(this.getValue(), m.getValue(), this.getValue());
	    }

	    /**
	     * 两个4×4矩阵的乘积（当前矩阵左乘M，并将结果赋值给传入的矩阵。）
	     * 
	     * @param m
	     * @param res
	     */
	    public void multiply(Matrix4 m, Matrix4 res)
	    {
		    multiply(this.getValue(), m.getValue(), res.getValue());
	    }

	    /**
	     * 两个4×4矩阵的乘积（m1*m2->m3）
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

	    // 转置矩阵索引
	    private static  int[] TR = { 0, 4, 8, 12, 1, 5, 9, 13, 2, 6, 10, 14, 3, 7, 11, 15 };

	    /**
	     * 4x4矩阵转置（矩阵的行和列互换。）
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
	     * 逆反矩阵，将结果赋值给自己
	     */
	    public bool inverse()
	    {
		    return inverse(this.getValue(), this.getValue());
	    }

	    /**
	     * 逆反矩阵，将结果赋值给指定结果
	     */
	    public bool inverse(Matrix4 res)
	    {
		    return inverse(this.getValue(), res.getValue());
	    }

        /**
         * 逆反矩阵（inverse(m1)->m2）
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
	     * 矩阵行列式（行列式）的计算
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
	     * 左乘（即后叠加操作）一个围绕任意轴的旋转矩阵
	     * 
	     * @param v
	     * @param theta
	     *            弧度
	     */
	    public void postRotate(Vector3 v, float theta)
	    {
		    matrix4Temp.setToRotate(v, theta);
		    matrix4Temp.multiply(this, this);
	    }

	    /**
	     * 左乘（即后叠加操作）一个围绕Z轴的旋转矩阵
	     * 
	     * @param theta
	     *            弧度
	     */
	    public void postRotate(float theta)
	    {
		    matrix4Temp.setToRotate(theta);
		    matrix4Temp.multiply(this, this);
	    }
	    /**
	     * 左乘（即后叠加操作）一个Z==0平面的位移矩阵
	     * 
	     * @param offX
	     * @param offY
	     */
	    public void postTranslate(float offX, float offY)
	    {
		    postTranslate(offX,offY,0.0f);
	    }
	    /**
	     * 左乘（即后叠加操作）一个位移矩阵
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
	     * 左乘（即后叠加操作）一个XY方向的缩放矩阵(Z缩放==1)
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
	     * 左乘（即后叠加操作）一个缩放矩阵
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
	     * 左乘（即后叠加操作）一个Z==0平面错切矩阵
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
	     * 右乘（即先叠加操作）一个围绕任意轴的旋转矩阵
	     * 
	     * @param v
	     * @param theta
	     *            弧度
	     */
	    public void preRotate(Vector3 v, float theta)
	    {
		    matrix4Temp.setToRotate(v, theta);
		    multiply(matrix4Temp);
	    }

	    /**
	     * 右乘（即先叠加操作）一个围绕Z轴的旋转矩阵
	     * 
	     * @param theta
	     *            弧度
	     */
	    public void preRotate(float theta)
	    {
		    matrix4Temp.setToRotate(theta);
		    multiply(matrix4Temp);
	    }
	    /**
	     * 右乘（即先叠加操作）一个Z==0平面位移矩阵
	     * 
	     * @param offX
	     * @param offY
	     */
	    public void preTranslate(float offX, float offY)
	    {
		    preTranslate(offX,offY,0.0f);
	    }
	    /**
	     * 右乘（即先叠加操作）一个位移矩阵
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
	     * 右乘（即先叠加操作）一个XY方向的缩放矩阵(Z缩放==1)
	     * 
	     * @param scaleX
	     * @param scaleY
	     */
	    public void preScale(float scaleX, float scaleY)
	    {
		    preScale(scaleX, scaleY, 1.0f);
	    }
	    /**
	     * 右乘（即先叠加操作）一个缩放矩阵
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
	     * 右乘（即先叠加操作）一个Z==0平面错切矩阵
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
	     * 设置成围绕任意轴的旋转矩阵
	     * 
	     * @param v
	     * @param theta
	     *            弧度
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
	     * 设置成围绕Z轴的旋转矩阵
	     * 
	     * @param theta
	     *            弧度
	     */
	    public void setToRotate(float theta)
	    {
		    vector3Temp.setValue(0, 0, 1);
		    setToRotate(vector3Temp, theta);
	    }

	    /**
	     * 设置成Z=0平面的位移矩阵
	     * 
	     * @param offX
	     * @param offY
	     */
	    public void setToTranslate(float offX, float offY)
	    {
		    setToTranslate(offX, offY, 0.0f);
	    }

	    /**
	     * 设置成位移矩阵
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
	     * 设置成Z==0平面的缩放矩阵
	     * 
	     * @param scaleX
	     * @param scaleY
	     */
	    public void setToScale(float scaleX, float scaleY)
	    {
		    setToScale(scaleX, scaleY,0.0f);
	    }
	    /**
	     * 设置成缩放矩阵
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
	     * 设置成Z==0平面错切矩阵
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
	     * 释放资源
	     */
	    public void releaseRes()
	    {
		    if (data != null)
		    {
			    data = null;
		    }
	    }

	    /**
	     * 将矩阵应用到矢量(左乘矢量)，并将结果存放回此矢量
	     * 
	     * @param vector
	     */
	    public void mapVector(Vector3 vector)
	    {
		    mapVector(vector, vector);
	    }

	    /**
	     * 将矩阵应用到矢量A(左乘矢量A)，并将结果存放回矢量B
	     * 
	     * @param vectorA
	     *            矢量A
	     * @param vectorB
	     *            矢量B
	     */
	    public void mapVector(Vector3 vectorA, Vector3 vectorB)
	    {
		    vectorA.getValue(f4_Temp0);
		    mapVector(this.getValue(), f4_Temp0, f4_Temp0);
		    vectorB.setValue(f4_Temp0);
	    }

	    /**
	     * 将矩阵m1应用到矢量A(左乘矢量A)，并将结果存放回矢量B
	     * 
	     * @param m1
	     *            矩阵m1
	     * @param va
	     *            矢量A
	     * @param vb
	     *            矢量B
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
         * 逆反矩阵（inverse(m1)->m2）
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
	     * 显示此矩阵内容
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
         * 将映射信息应用到每个顶点
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