using System;
namespace Cyclone.alg.math
{
public class Matrix3
{
	private static Matrix3 m3Temp=new Matrix3();
    public float[] data;
        public const int M00 = 0;
        public const int M01 = 3;
        public const int M02 = 6;
        public const int M10 = 1;
        public const int M11 = 4;
        public const int M12 = 7;
        public const int M20 = 2;
        public const int M21 = 5;
        public const int M22 = 8;

	/**
	 * 构造函数
	 */
	public Matrix3()
	{
		this.data = new float[9];
		identity();
	}

	public Matrix3(float[] b)
        {
            setData(b);
        }

        public Matrix3(Matrix3 t)
        {
            setData(t.data);
        }
        private void setData(float[] b)
        {
            this.data = new float[9];
            for (int i = 0; i < 9; i++)
            {
                this.data[i] = b[i];
            }
        }

	/**
	 * 矩阵重置(变成单位矩阵)
	 */
	public void identity()
	{
		for (int i = 0; i < 9; i++)
		{
			this.data[i] = 0;
		}
		this.data[M00] = this.data[M11] = this.data[M22] = 1;
	}

	/**
	 * 设置数值
	 * 
	 * @param matrix
	 *            指定矩阵
	 */
	public void setValue(Matrix3 matrix)
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

	/**
	 * 返回数值
	 * 
	 * @return 获取数值
	 */
	public float[] getValue()
	{
		return data;
	}

	/**
	 * 3x3矩阵行列式（行列式）的计算
	 * 
	 * @return float 行列式的值
	 */
	public float det()
	{
		float t = data[M00] * data[M11] * data[M22] + data[M01] * data[M12] * data[M20] + data[M02] * data[M21] * data[M10] - data[M20] * data[M11] * data[M02]
				- data[M12] * data[M21] * data[M00] - data[M22] * data[M10] * data[M01];
		return t;
	}
	/**
	 * 3x3矩阵的左乘(当前矩阵左乘M)
	 * 
	 * @param ma
	 *            矩阵m
	 */
	public void multiply(Matrix3 ma)
	{
		multiply(ma,this);
	}
	/**
	 * 3x3矩阵的左乘(当前矩阵左乘ma)，并将结果放入mb
	 * @param ma 左乘矩阵
	 * @param mb 结果矩阵
	 */
	public void multiply(Matrix3 ma,Matrix3 mb)
	{
		float v00 = data[M00] * ma.data[M00] + data[M10] * ma.data[M01] + data[M20] * ma.data[M02];
		float v01 = data[M00] * ma.data[M10] + data[M10] * ma.data[M11] + data[M20] * ma.data[M12];
		float v02 = data[M00] * ma.data[M20] + data[M10] * ma.data[M21] + data[M20] * ma.data[M22];

		float v10 = data[M01] * ma.data[M00] + data[M11] * ma.data[M01] + data[M21] * ma.data[M02];
		float v11 = data[M01] * ma.data[M10] + data[M11] * ma.data[M11] + data[M21] * ma.data[M12];
		float v12 = data[M01] * ma.data[M20] + data[M11] * ma.data[M21] + data[M21] * ma.data[M22];

		float v20 = data[M02] * ma.data[M00] + data[M12] * ma.data[M01] + data[M22] * ma.data[M02];
		float v21 = data[M02] * ma.data[M10] + data[M12] * ma.data[M11] + data[M22] * ma.data[M12];
		float v22 = data[M02] * ma.data[M20] + data[M12] * ma.data[M21] + data[M22] * ma.data[M22];

		mb.data[M00] = v00;
		mb.data[M01] = v01;
		mb.data[M02] = v02;
		mb.data[M10] = v10;
		mb.data[M11] = v11;
		mb.data[M12] = v12;
		mb.data[M20] = v20;
		mb.data[M21] = v21;
		mb.data[M22] = v22;
	}

	/**
	 * 3x3矩阵的逆矩阵计算
	 * 
	 * @return 逆矩阵
	 * @throws Exception
	 */
	public Matrix3 inverse()
	{
		float t = this.det();
		if (t == 0)
		{
			throw new Exception();
		}
		float[] b = new float[9];
		b[M00] = (data[M11] * data[M22] - data[M12] * data[M21]) / t;
		b[M01] = -(data[M01] * data[M22] - data[M21] * data[M02]) / t;
		b[M02] = (data[M01] * data[M12] - data[M02] * data[M11]) / t;
		b[M10] = -(data[M10] * data[M22] - data[M12] * data[M20]) / t;
		b[M11] = (data[M00] * data[M22] - data[M02] * data[M20]) / t;
		b[M12] = -(data[M00] * data[M12] - data[M02] * data[M10]) / t;
		b[M20] = (data[M10] * data[M21] - data[M11] * data[M20]) / t;
		b[M21] = -(data[M00] * data[M21] - data[M01] * data[M20]) / t;
		b[M22] = (data[M00] * data[M11] - data[M01] * data[M10]) / t;
		Matrix3 m = new Matrix3(b);
		return (m);
	}
	/**
	 * 左乘（即后叠加操作）一个旋转指定 弧度的矩阵
	 * 
	 * @param v
	 * @param theta  弧度
	 */

	public void postRotate(float theta)
	{
		m3Temp.setToRotate(theta);
		m3Temp.multiply(this,this);
	}

	/**
	 * 左乘（即后叠加操作）一个位移矩阵
	 * 
	 * @param offX
	 * @param offY
	 * @param offZ
	 */
	public void postTranslate(float offX, float offY)
	{
		m3Temp.setToTranslate(offX,offY);
		m3Temp.multiply(this,this);
	}

	/**
	 * 左乘（即后叠加操作）一个缩放矩阵
	 * 
	 * @param scaleX
	 * @param scaleY
	 * @param scaleZ
	 */
	public void postScale(float scaleX, float scaleY)
	{
		m3Temp.setToScale(scaleX,scaleY);
		m3Temp.multiply(this,this);
	}
	/**
	 * 左乘（即后叠加操作）一个错切矩阵
	 * 
	 * @param shearX
	 * @param shearY
	 */
	public void postShear(float shearX, float shearY)
	{
		m3Temp.setToShear(shearX, shearY);
		m3Temp.multiply(this,this);
	}
	/**
	 * 右乘（即先叠加操作）一个旋转指定 弧度的矩阵
	 * 
	 * @param v
	 * @param theta  弧度
	 */

	public void preRotate(float theta)
	{
		m3Temp.setToRotate(theta);
		multiply(m3Temp);
	}

	/**
	 * 右乘（即先叠加操作）一个位移矩阵
	 * 
	 * @param offX
	 * @param offY
	 * @param offZ
	 */
	public void preTranslate(float offX, float offY)
	{
		m3Temp.setToTranslate(offX,offY);
		multiply(m3Temp);
	}

	/**
	 * 右乘（即先叠加操作）一个缩放矩阵
	 * 
	 * @param scaleX
	 * @param scaleY
	 * @param scaleZ
	 */
	public void preScale(float scaleX, float scaleY)
	{
		m3Temp.setToScale(scaleX,scaleY);
		multiply(m3Temp);
	}
	/**
	 * 右乘（即先叠加操作）一个错切矩阵
	 * 
	 * @param shearX
	 * @param shearY
	 */
	public void preShear(float shearX, float shearY)
	{
		m3Temp.setToShear(shearX, shearY);
		multiply(m3Temp);
	}
	/**
	 * 设置成旋转指定 弧度的矩阵
	 * 
	 * @param v
	 * @param theta  弧度
	 */

	public void setToRotate(float theta)
	{
		float s = (float) Math.Sin(theta);
		float c = (float) Math.Cos(theta);
		this.identity();
		data[M00] = c;
		data[M01] = -s;
		data[M10] = s;
		data[M11] = c;
	}

	/**
	 * 设置成位移矩阵
	 * 
	 * @param offX
	 * @param offY
	 * @param offZ
	 */
	public void setToTranslate(float offX, float offY)
	{
		this.identity();
		data[M02] = offX;
		data[M12] = offY;
	}

	/**
	 * 设置成缩放矩阵
	 * 
	 * @param scaleX
	 * @param scaleY
	 * @param scaleZ
	 */
	public void setToScale(float scaleX, float scaleY)
	{
		this.identity();
		data[M00] = scaleX;
		data[M11] = scaleY;
	}
	/**
	 * 设置成错切矩阵
	 * 
	 * @param shearX
	 * @param shearY
	 */
	public void setToShear(float shearX, float shearY)
	{
		this.identity();
		data[M01] = shearX;
		data[M10] = shearY;
	}
//	/**
//	 * 转换成4*4矩阵
//	 * @return
//	 */
//	public Matrix4 toMatrix4()
//	{
//		m4Temp.setValue(this);
//		return m4Temp;
//	}
	public void show(String txt)
	{
		Console.Write("--"+txt+" --[");
		for (int i = 0; i < data.Length; i++)
		{
            Console.Write(data[i] + ",");
		}
        Console.WriteLine("]");
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
}
}