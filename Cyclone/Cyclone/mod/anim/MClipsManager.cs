using System;
using System.Collections.Generic;
using System.Text;
using Cyclone.alg;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using Cyclone.mod.anim;
using Cyclone.alg.util;
using Cyclone.mod.animimg;
using System.Xml;
using Cyclone.Cyclone.alg.util;

namespace Cyclone.mod.anim
{
    /// <summary>
    /// ������Ƭ��������ӵ��һ��ԭʼ��Ƭ�б����ڹ����ɫ�������������õ�ԭʼ��Ƭ
    /// </summary>
    public class MClipsManager : MNode<MClipElement>
    {
        public Form_Main form_Main = null;
        public MClipsManager()
        {
        }
        public MClipsManager( Form_Main form_MAT)
        {
            form_Main = form_MAT;
        }
        public MClipsManager Clone()
        {
            return Clone( form_Main);
        }
        public MClipsManager Clone( Form_Main form_MAT)
        {
            MClipsManager newInstance = new MClipsManager(form_MAT);
            for (int i = 0; i < Count(); i++)
            {
                MClipElement clipElement = (MClipElement)this[i];
                MClipElement newClip = clipElement.Clone(newInstance);
                newInstance.Add(newClip);
            }
            return newInstance;
        }
        public MClipsManager CloneReference()
        {
            return CloneReference(form_Main);
        }
        public MClipsManager CloneReference(Form_Main form_MAT)
        {
            MClipsManager newInstance = new MClipsManager(form_MAT);
            for (int i = 0; i < Count(); i++)
            {
                newInstance.Add(this[i].Clone());
            }
            return newInstance;
        }
        //��¡�����õ���Ƭ������(ֻ������ʹ�ù�����Ƭ)
        public MClipsManager CloneForExport()
        {
            MClipsManager newInstance = new MClipsManager(form_Main);
            for (int i = 0; i < Count(); i++)
            {
                MClipElement clipElement = (MClipElement)this[i];
                MClipElement newClip = clipElement.Clone(newInstance);
                newInstance.Add(newClip);
            }
            return newInstance;
        }
        public virtual MImgsManager ImgsManager
        {
            get
            {
                return form_Main.form_MAnimation.form_MImgsList.mImgsManager;
            }
        }
        //��������ʹ����ָ��ͼƬ��Ԫ����Ƭ��Ԫ
        private static ArrayList usingImgClipsList = new ArrayList();
        public ArrayList getAllClipsUsingImg(MImgElement p_imgElement)
        {
            if (usingImgClipsList == null)
            {
                usingImgClipsList = new ArrayList();
            }
            else
            {
                usingImgClipsList.Clear();
            }
            if (this.Count() > 0)
            {
                foreach (MClipElement clip in m_sonList)
                {
                    if (clip != null && clip.imageElement != null && clip.imageElement.Equals(p_imgElement))
                    {
                        usingImgClipsList.Add(clip);
                    }
                }
            }
            return usingImgClipsList;
        }
        //����ͼƬ����Դ������Ƭ˳�򣬾�����������
        //public void orderByImg()
        //{
            //if (ImgsManager.Size() <= 0)
            //{
            //    return;
            //}
            ////�Ѽ�����
            //List<MClipElement>[] orderLists = new List<MClipElement>[ImgsManager.Size()];
            //for (int i = 0; i < ImgsManager.Size(); i++)
            //{
            //    MImgElement imgElement = ImgsManager[i];
            //    orderLists[i] = new List<MClipElement>();
            //    for (int j = 0; j < this.Size(); j++)
            //    {
            //        MClipElement baseclip = (MClipElement)this[j];
            //        if (baseclip == null)
            //        {
            //            break;
            //        }
            //        if (baseclip.imageElement != null && baseclip.imageElement.Equals(imgElement))
            //        {
            //            orderLists[i].Add(baseclip);
            //            this.Remove(baseclip);
            //            j--;
            //        }
            //    }
            //}
            ////����������
            //List<MClipElement> baseClipsListNew = new List<MClipElement>();
            //for (int i = 0; i < orderLists.Length; i++)
            //{
            //    for (int j = 0; j < orderLists[i].Count; j++)
            //    {
            //        baseClipsListNew.Add(orderLists[i][j]);
            //    }
            //}
            //for (int i = 0; i < this.Size(); i++)
            //{
            //    baseClipsListNew.Add(this[i]);
            //}
            //this.sonList.Clear();//���ԭ������
            //sonList = baseClipsListNew;//����������
            ////�������
            //for (int i = 0; i < orderLists.Length; i++)
            //{
            //    orderLists[i].Clear();
            //    orderLists[i] = null;
            //}
            //orderLists = null;
        //}
        //���δʹ�õĵ�Ԫ���ظ��ĵ�Ԫ(mantainUnusedָ���Ƿ���δʹ�õĵ�Ԫ)
        public void ClearSpilth(bool mantainUnused)
        {
            //���δʹ�õĵ�Ԫ
            if (!mantainUnused)
            {
                for (int i = 0; i < this.Count(); i++)
                {
                    MClipElement clipElement = (MClipElement)this[i];
                    if (clipElement.getUsedTime() <= 0)
                    {
                        Remove(clipElement);
                        i--;
                    }
                }
            }
            //����ظ��ĵ�Ԫ
            for (int i = 0; i < Count(); i++)
            {
                MClipElement baseclip = this[i];
                for (int j = i + 1; j < Count(); j++)
                {
                    MClipElement baseclipT = this[j];
                    if (baseclipT.Equals(baseclip))
                    {
                        continue;
                    }
                    if (baseclip.equalsClip(baseclipT))
                    {
                        replaceHandler(baseclip, baseclipT);
                        //ɾ���ظ���Ƭ
                        this.RemoveAt(baseclipT.GetID());
                        j--;
                    }
                }
            }
        }
        //�滻����
        public void replaceHandler(MClipElement toReplace, MClipElement toCompare)
        {
            MActorsManager MAM = form_Main.form_MAnimation.form_MActorList.actorsManager;
            //ת������      
            for (int i1 = 0; i1 < MAM.Count(); i1++)
            {
                for (int i2 = 0; i2 < MAM[i1].Count(); i2++)
                {
                    for (int i3 = 0; i3 < MAM[i1][i2].Count(); i3++)
                    {
                        for (int i4 = 0; i4 < MAM[i1][i2][i3].Count(); i4++)
                        {
                            for (int i5 = 0; i5 < MAM[i1][i2][i3][i4].Count(); i5++)
                            {
                                for (int i6 = 0; i6 < MAM[i1][i2][i3][i4][i5].Count(); i6++)
                                {
                                    MFrameUnit unit = MAM[i1][i2][i3][i4][i5][i6];
                                    if (unit is MFrameUnit_Bitmap)
                                    {
                                        MFrameUnit_Bitmap unitBitmap = (MFrameUnit_Bitmap)unit;
                                        if (unitBitmap.clipElement.Equals(toCompare))
                                        {
                                            unitBitmap.clipElement = toReplace;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        //�ϲ���Ƭ��Դ
        public void combine(MClipsManager src_Manager)
        {
            for (int i = 0; i < src_Manager.Count(); i++)
            {
                MClipElement srcElement = src_Manager[i];
                MClipElement newElement = srcElement.Clone(this);
                //����ظ�
                for (int j = 0; j < this.Count(); j++)
                {
                    MClipElement localClip = this[j];
                    if (localClip.equalsClip(newElement))
                    {
                        newElement = localClip;
                        break;
                    }
                }
                if (!this.Contains(newElement))
                {
                    this.Add(newElement);
                }
                //ת������
                src_Manager.replaceHandler(newElement, srcElement);
            }
        }

        public override void ReadObject(System.IO.Stream s)
        {
            this.m_sonList.Clear();
            short len = IOUtil.readShort(s);
            for (short i = 0; i < len; i++)
            {
                MClipElement clipElem = new MClipElement(this);
                clipElem.ReadObject(s);
                Add(clipElem);
            }
        }

        public override void WriteObject(System.IO.Stream s)
        {
            short len = (short)this.Count();
            IOUtil.writeShort(s, len);
            for (short i = 0; i < len; i++)
            {
                MClipElement clipElem = (MClipElement)this[i];
                clipElem.WriteObject(s);
            }
        }
        public override void ExportObject(System.IO.Stream fs_bin)
        {
            short len = (short)this.Count();
            UserDoc.ArrayTxts_Head.Add("//Generated " + len + " texture clips");
            UserDoc.ArrayTxts_Java.Add("//Generated " + len + " texture clips");
            IOUtil.writeShort(fs_bin, len);
            for (short i = 0; i < len; i++)
            {
                MClipElement clipElem = (MClipElement)this[i];
                clipElem.ExportObject(fs_bin);
            }

        }


        //ˢ���ӵ�Ԫ��������ʱ��Ƭ
        public void resetImgClips()
        {
            for (int i = 0; i < Count(); i++)
            {
                MClipElement clip = this[i];
                clip.resetImgClip();
            }
        }


        #region MImagesUser ��Ա

        public List<MClipsManager> GetClipsManagers()
        {
            List<MClipsManager> managers = new List<MClipsManager>();
            managers.Add(this);
            return managers;
        }

        #endregion
    }
}