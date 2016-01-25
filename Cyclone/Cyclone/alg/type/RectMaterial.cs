using System;
using System.Collections.Generic;
using System.Text;
using Cyclone.mod;
using System.Collections;
using System.Drawing;
using Cyclone.mod.anim;
using Cyclone.mod.animimg;

namespace Cyclone.alg.type
{
    public class RectMaterial
    {
        public double m_dHeight, m_dWidth;
        public double m_dX, m_dY;
        public bool m_bUsed;
        public OptmizeClip optmizeClip = null;
        public RectMaterial(OptmizeClip baseClipP)
        {
         m_dX=0;
         m_dY=0;
         m_bUsed=false;
         SetBaseClip(baseClipP);
        }
        private RectMaterial(RectMaterial pMaterial)
        {
         m_dX=pMaterial.m_dX;
         m_dY=pMaterial.m_dY;
         m_dHeight=pMaterial.m_dHeight;
         m_dWidth=pMaterial.m_dWidth;
         m_bUsed=pMaterial.m_bUsed;
         optmizeClip = pMaterial.optmizeClip;
        }
        public void SetBaseClip(OptmizeClip baseClipP)
        {
            optmizeClip = baseClipP;
            m_dHeight = optmizeClip.clipElement.clipRect.Height;
            m_dWidth = optmizeClip.clipElement.clipRect.Width;
        }
        public void CastBaseClip(MImgElement imgElementNew)
        {
            if (optmizeClip != null)
            {
                optmizeClip.clipElement.clipRect.X = (int)m_dX;
                optmizeClip.clipElement.clipRect.Y = (int)m_dY;
                optmizeClip.clipElement.clipRect.Width = (int)m_dWidth;
                optmizeClip.clipElement.clipRect.Height = (int)m_dHeight;
                if (imgElementNew != null)
                {
                    optmizeClip.clipElement.imageElement = imgElementNew;
                }
                //映射到子矩形
                optmizeClip.CastSubClipList(imgElementNew);
            }
        }

        public void SetUsed(bool bUsed)
        {
         m_bUsed=bUsed;
        }
        public double GetArea()
        {
	        return m_dHeight*m_dWidth; 
        }


        public RectMaterial Copy()
        {
         RectMaterial p=new RectMaterial(this);
         return p;
        }
    }
    public class OptmizeClip
    {
        public MClipElement clipElement = null;
        public ArrayList subClips = new ArrayList();
        public Rectangle clipRectOld = new Rectangle();
        public OptmizeClip(MClipElement clipT)
        {
            clipElement = clipT;
            clipRectOld = clipElement.clipRect;
        }
        public void addSubClip(OptmizeClip subClipT)
        {
            subClips.Add(subClipT);
        }
        public bool Contains(OptmizeClip subClip)
        {
            if (subClip == null || subClip.clipElement == null)
            {
                return false;
            }
            return clipElement.clipRect.Contains(subClip.clipElement.clipRect);
        }
        public void CastSubClipList(MImgElement imgElementNew)
        {
            foreach (OptmizeClip subClip in subClips)
            {
                //将CLIP根据父剪切区的翻转而翻转------------------------
                subClip.clipElement.clipRect.X = clipElement.clipRect.X + subClip.clipElement.clipRect.X - clipRectOld.X;
                subClip.clipElement.clipRect.Y = clipElement.clipRect.Y + subClip.clipElement.clipRect.Y - clipRectOld.Y;
                if (imgElementNew != null)
                {
                    subClip.clipElement.imageElement = imgElementNew;
                }
                subClip.CastSubClipList(imgElementNew);
            }
        }
    }
}
