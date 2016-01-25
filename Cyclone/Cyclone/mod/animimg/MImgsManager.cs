using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Cyclone.alg;
using Cyclone.ui_script;
using Cyclone.alg.util;
using Cyclone.mod.map;
using Cyclone.alg.type;
using Cyclone.mod.anim;

namespace Cyclone.mod.animimg
{
    /// <summary>
    /// 图片管理器，拥有一个存放所有图片的列表，并管理图片增减等操作
    /// </summary>
    public class MImgsManager : MNode<MImgElement>
    {
        public Form_Main form_Main = null;          //可以是地图或者动画图片
        public MImgsManager()
        {
        }
        public MImgsManager(Form_Main form)
        {
            form_Main = form;
        }
        public void setUser(Form_Main form)
        {
            form_Main = form;
        }
        public MImgsManager CloneReference()
        {
            return CloneReference(form_Main);
        }
        public MImgsManager CloneReference(Form_Main form_MainT)
        {
            MImgsManager newInstance = new MImgsManager(form_MainT);
            for (int i = 0; i < Count(); i++)
            {
                newInstance.Add(this[i],false);
            }
            newInstance.ui = ui;
            return newInstance;
        }
        public MImgsManager Clone()
        {
            return Clone(form_Main);
        }
        public MImgsManager Clone(Form_Main form_MainT)
        {
            MImgsManager newInstance = new MImgsManager(form_MainT);
            for (int i = 0; i < Count(); i++)
            {
                newInstance.Add(this[i].Clone(newInstance), false);
            }
            newInstance.ui = ui;
            return newInstance;
        }
        //重新绑定所有贴图
        public void rebindTextures()
        {
            for (int i = 0; i < this.Count(); i++)
            {
                MImgElement imgElement = this[i];
                imgElement.rebindTexture();
            }
        }
        //合并图片资源
        public void combine(MImgsManager src_ImgsManager)
        {
            //检查子文件夹
            if (!Directory.Exists(Consts.PATH_PROJECT_FOLDER + Consts.SUBPARH_IMG))
            {
                Directory.CreateDirectory(Consts.PATH_PROJECT_FOLDER + Consts.SUBPARH_IMG);
            }
            int res = 0;
            //合并
            for (int i = 0; i < src_ImgsManager.Count(); i++)
            {
                MImgElement srcElement = src_ImgsManager[i];
                String nameSrc = srcElement.name;
                MImgElement newElement = null;
                bool findSameName = false;
                MImgElement localElement = null;
                //同名检查
                for (int j = 0; j < this.Count(); j++)
                {
                    localElement = this[j];
                    if (localElement.equalsOnName(srcElement))//发现相同的文件名
                    {
                        findSameName = true;
                        break;
                    }
                }
                //同名处理
                if (findSameName)
                {
                    if (res < 2)
                    {
                        res = SmallDialog_MessageBox.getResult("覆盖", "忽略", "全部覆盖", "全部忽略", "发现相同的图片资源“" + localElement.name + "”");
                    }
                    if (res == 0 || res == 2)//覆盖
                    {
                        if (!srcElement.getFullName().Equals(localElement.getFullName()))
                        {
                            IOUtil.Copy(srcElement.getFullName(), localElement.getFullName(), true);
                            if (!srcElement.strAlphaImage.Equals(""))
                            {
                                IOUtil.Copy(srcElement.getFullAlphaName(), localElement.getFullAlphaName(srcElement.strAlphaImage), true);
                            }
                            if (!srcElement.strPmt.Equals(""))
                            {
                                IOUtil.Copy(srcElement.getFullPmtName(), localElement.getFullPmtName(srcElement.strPmt), true);
                            }
                        }
                        localElement.loadImage();
                        newElement = localElement;
                    }
                    else if (res == 1 || res == 3)//忽略新资源
                    {
                        newElement = localElement;
                    }
                }
                else
                {
                    newElement = srcElement;//.Clone(this);
                    this.Add(newElement);
                    IOUtil.Copy(srcElement.getFullName(), newElement.getFullName(srcElement.name), true);
                    if (!srcElement.strAlphaImage.Equals(""))
                    {
                        IOUtil.Copy(srcElement.getFullAlphaName(), localElement.getFullAlphaName(srcElement.strAlphaImage), true);
                    }
                    if (!srcElement.strPmt.Equals(""))
                    {
                        IOUtil.Copy(srcElement.getFullPmtName(), localElement.getFullPmtName(srcElement.strPmt), true);
                    }

                    newElement.loadImage();
                }
                if (this.Equals(form_Main.mapImagesManager))//地图图片
                {
                    TileGfxManager tileGfxManager = form_Main.mapsManager.tileGfxManager;
                    for (int iCM = 0; iCM < tileGfxManager.Count(); iCM++)
                    {
                        MClipsManager clipsManagerElement = tileGfxManager[iCM];
                        for (int k = 0; k < clipsManagerElement.Count(); k++)
                        {
                            MClipElement element = clipsManagerElement[k];
                            if (element.imageElement != null && element.imageElement.Equals(srcElement))
                            {
                                element.imageElement = newElement;
                            }
                        }
                    }
                }
                else if (this.Equals(form_Main.form_MAnimation.form_MImgsList.mImgsManager))//动画图片
                {
                    MClipsManager mClipsManager = form_Main.form_MAnimation.form_MImgsList.MClipsManager;
                    MClipsManager clipsManagerElement = mClipsManager;
                    for (int k = 0; k < clipsManagerElement.Count(); k++)
                    {
                        MClipElement element = clipsManagerElement[k];
                        if (element.imageElement != null && element.imageElement.Equals(srcElement))
                        {
                            element.imageElement = newElement;
                        }
                    }
                }
            }
        }
        //找到图片列表中的最小的没有被使用过的链接ID
        public int getMinUnunsedLinkID()
        {
            int id = int.MaxValue;
            List<int> usedLinkID = new List<int>();
            foreach (MImgElement element in m_sonList)
            {
                if (element.linkID >= 0)
                {
                    if (!usedLinkID.Contains(element.linkID))
                    {
                        usedLinkID.Add(element.linkID);
                    }
                    if (element.linkID < id)
                    {
                        id = element.linkID;
                    }
                }
            }
            if (id == int.MaxValue)
            {
                id = 0;
            }
            else if (usedLinkID.Count>0)//寻找最小ID
            {
                usedLinkID.Sort();
                if (usedLinkID[0] > 0)
                {
                    id = usedLinkID[0] - 1;
                }
                else
                {
                    bool find = false;
                    for (int i = usedLinkID[0]; i < usedLinkID[0] + usedLinkID.Count; i++)
                    {
                        if (!usedLinkID.Contains(i))
                        {
                            id = i;
                            find=true;
                            break;
                        }
                    }
                    if (!find)
                    {
                        id = usedLinkID[usedLinkID.Count - 1] + 1;
                    }
                }
            }
            return id;
        }
        //搜索所有图片，并将使用过的图片分组返回(先是分组列表，最后一个分组是禁止优化的图片组)
        public List<List<MImgElement>> getAllUsedGroups()
        {
            List<int> linkIDs = new List<int>();
            Hashtable linkedTable = new Hashtable();//被链接过的图片组
            List<MImgElement> imgsNotLinked = new List<MImgElement>();//未必链接，允许优化的图片组
            List<MImgElement> imgsForbidOpt = new List<MImgElement>();//禁止优化的图片组
            foreach (MImgElement element in m_sonList)
            {
                if (element.getUsedTime() <= 0)
                {
                    continue;
                }
                if (element.forbidOptimize)
                {
                    imgsForbidOpt.Add(element);
                }
                else if (element.linkID < 0)
                {
                    imgsNotLinked.Add(element);
                }
                else
                {
                    List<MImgElement> linkedGroup;
                    if (linkedTable.ContainsKey(element.linkID))
                    {
                        linkedGroup = (List<MImgElement>)linkedTable[element.linkID];
                    }
                    else
                    {
                        linkedGroup = new List<MImgElement>();
                        linkedTable.Add(element.linkID, linkedGroup);
                    }
                    linkedGroup.Add(element);
                }
            }
            //整理图片组
            List<List<MImgElement>> groups = new List<List<MImgElement>>();
            //加入链接图片组
            foreach (DictionaryEntry de in linkedTable)
            {
                List<MImgElement> linkedGroup = (List<MImgElement>)linkedTable[de.Key];
                groups.Add(linkedGroup);
            }
            //加入单个图片组
            foreach (MImgElement img in imgsNotLinked)
            {
                List<MImgElement> singleGroup = new List<MImgElement>();
                singleGroup.Add(img);
                groups.Add(singleGroup);
            }
            //加入禁止优化组
            groups.Add(imgsForbidOpt);
            //返回
            return groups;
        }
        //串行化输入与输出===================================================================
        //串行读入
        public override void ReadObject(Stream s)
        {
            int imgsNum = IOUtil.readInt(s);
            this.Clear();
            for (int i = 0; i < imgsNum; i++)
            {
                MImgElement imgElement = new MImgElement(this);
                imgElement.ReadObject(s);
                this.Add(imgElement);//增加到列表
                //Consts.loadingDialog.setStep(Consts.loadingProcess + i * 10 / imgsNum, "载入图片:" + imgElement.name);
                //Console.WriteLine("imgElement.name:" + imgElement.name);
            }

        }
        //串行输出
        public override void WriteObject(Stream s)
        {
            int imgsNum = this.Count();
            IOUtil.writeInt(s, imgsNum);
            for (int i = 0; i < imgsNum; i++)
            {
                MImgElement imgElement = (MImgElement)this[i];
                imgElement.WriteObject(s);
            }
        }
        //串行输出
        public override void ExportObject(Stream fs_bin)
        {
            short len = (short)Count();
            IOUtil.writeShort(fs_bin, len);
            for (short i = 0; i < len; i++)
            {
                this[i].ExportObject(fs_bin);
            }
        }
        //图片单元操作=====================================================================
        //导入图片
        public void importImage(String srcFileName, bool fixWidth)
        {
            if (!File.Exists(srcFileName))
            {
                return;
            }
            int lastIndex = srcFileName.LastIndexOf('\\') + 1;
            String newFileName = srcFileName.Substring(lastIndex, srcFileName.Length - lastIndex);
            String sunFolderName = Consts.PATH_PROJECT_FOLDER + Consts.SUBPARH_IMG;
            String newFileFullPath = sunFolderName + newFileName;
            //if (existImage(newFileName))
            //{
            //    MessageBox.Show("图片已经存在，请将文件复制到子目录后更新！", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            Image img = IOUtil.createImage(srcFileName);
            if (img == null)
            {
                return;
            }
            if (fixWidth)
            {
                if (img.Width != Consts.MapImgFixWidth)
                {
                    MessageBox.Show("地图图片宽度不是" + Consts.MapImgFixWidth + "！", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            //if (img.Width * img.Height > Consts.MaxImgSize)
            //{
            //    MessageBox.Show("图片面积不能超过" + (Consts.MaxImgSize) + "！", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            img.Dispose();
            img = null;
            try
            {
                //检查子文件夹
                if (!Directory.Exists(sunFolderName))
                {
                    Directory.CreateDirectory(sunFolderName);
                }
                //拷贝图片
                if (!srcFileName.Equals(newFileFullPath))
                {
                    IOUtil.Copy(srcFileName, newFileFullPath, true);
                }
                //加入列表
                this.Add(new MImgElement(newFileName, this));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        //导入Alpha图片
        public String importAlphaImage(Image imgSrc, String srcFileName)
        {
            if (!File.Exists(srcFileName))
            {
                return null;
            }
            Image imgAlpha = Image.FromFile(srcFileName);
            if (imgAlpha == null)
            {
                return null;
            }
            if (imgAlpha.Width != imgSrc.Width || imgAlpha.Height != imgSrc.Height)
            {
                MessageBox.Show("图片尺寸与原图不一致！", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            imgAlpha.Dispose();
            imgAlpha = null;
            int lastIndex = srcFileName.LastIndexOf('\\') + 1;
            String newFileName = srcFileName.Substring(lastIndex, srcFileName.Length - lastIndex);
            String sunFolderName = Consts.PATH_PROJECT_FOLDER + Consts.SUBPARH_IMG;
            String newFileFullPath = sunFolderName + newFileName;
            if (existImage(newFileName))
            {
                MessageBox.Show("图片已经存在，请将文件复制到子目录后更新！", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            try
            {
                //检查子文件夹
                if (!Directory.Exists(sunFolderName))
                {
                    Directory.CreateDirectory(sunFolderName);
                }
                //拷贝图片
                if (!srcFileName.Equals(newFileFullPath))
                {
                    IOUtil.Copy(srcFileName, newFileFullPath, true);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return newFileName;
        }
        //检查列表中是否存在相同名称的图片单元
        private bool existImage(String name)
        {
            foreach (MImgElement imgElem in m_sonList)
            {
                if (imgElem.name.Equals(name))
                {
                    return true;
                }
            }
            return false;
        }
        //更新列表中的图片单元中的图片
        public void reloadImageElements()
        {
            foreach (MImgElement imgElem in m_sonList)
            {
                imgElem.loadImage();
            }
            if (this.Equals(form_Main.mapImagesManager))//地图图片
            {
                TileGfxManager tileGfxManager = form_Main.mapsManager.tileGfxManager;
                tileGfxManager.resetImgClips();
            }
            else if (this.Equals(form_Main.form_MAnimation.form_MImgsList.mImgsManager))//动画图片
            {
                MClipsManager mClipsManager = form_Main.form_MAnimation.form_MImgsList.MClipsManager;
                mClipsManager.resetImgClips();
            }
        }
        //删除列表中的图片单元
        public bool removeImageElementAt(int index)
        {
            if (index < 0 || index >= this.Count())
            {
                return false;
            }
            MImgElement imgElem = (MImgElement)this[index];
            int usedTime = imgElem.getUsedTime();
            if (usedTime > 0)//提醒有切片在使用
            {
                MessageBox.Show("该图片被" + usedTime + "处引用，不能删除。", "错误操作", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                RemoveAt(index);
                return true;
            }
        }
        //根据索引返回图片
        public Image getImage(int index)
        {
            if (index >= this.Count() || index < 0)
            {
                return null;
            }
            return this[index].image;
        }
        //优化指定图片组内所有图片上切块，并返回新生成的图片和大小信息，可以设置其是否将排样数据反射到原始切块
        public static MImgElement optimizeClips(List<MImgElement> imgElements, out Size needSize, out int usedSpace, out Image imgGenerate, bool castData)
        {
            List<OptmizeClip> clipsForOptimize = new List<OptmizeClip>();
            //先搜索组内所有切块，检查重叠后存放在待优化列表中
            for (int imgIndex = 0; imgIndex < imgElements.Count; imgIndex++)
            {
                MImgElement imgElement = imgElements[imgIndex];
                //获取当前图片的切片列表，加入到总表
                List<MClipElement> clipsList = imgElement.getAllClipsUsingMe();
                int imgCheckIndex = clipsForOptimize.Count;
                foreach (MClipElement clip in clipsList)
                {
                    OptmizeClip opClip = new OptmizeClip(clip);
                    clipsForOptimize.Add(opClip);
                }
                //在当前图片的切片列表中检查并去掉重叠裁剪区域
                for (int i = imgCheckIndex; i < clipsForOptimize.Count; i++)
                {
                    OptmizeClip clip = clipsForOptimize[i];
                    for (int j = imgCheckIndex; j < clipsForOptimize.Count; j++)
                    {
                        OptmizeClip clipParent = (OptmizeClip)clipsForOptimize[j];
                        if (clipParent.Equals(clip))
                        {
                            continue;
                        }
                        //找到同一张图片下的被包含区域
                        if (clipParent.Contains(clip))
                        {
                            clipParent.addSubClip(clip);//将被包含区域索引储存在其父列表中
                            clipsForOptimize.Remove(clip);
                            i--;
                            break;
                        }
                    }
                }

            }
            //开始优化------------------------------------------------------------------------------
            //进入优化算法
            RectSorter sorter = new RectSorter();
            //添加切片单元
            for (int i = 0; i < clipsForOptimize.Count; i++)//查找当前图片上的切片
            {
                OptmizeClip opClip = clipsForOptimize[i];
                RectMaterial material = new RectMaterial(opClip);
                sorter.Add(material);
            }
            //进行排样
            sorter.StartSort();
            //计算本次排样后面积大小
            needSize = sorter.getNeedSize();
            usedSpace = sorter.getUsedSize();
            int imgW = needSize.Width;
            int imgH = needSize.Height;
            double sNew = imgW * imgH;
            //生成图片
            if (sNew > 0)
            {
                imgGenerate = new Bitmap(imgW, imgH);
                Graphics g = Graphics.FromImage(imgGenerate);
                for (int j = 0; j < sorter.m_MaterialList.Count; j++)
                {
                    RectMaterial material = sorter.m_MaterialList[j];
                    GraphicsUtil.drawClip(g, material.optmizeClip.clipElement.imageElement.image, (int)material.m_dX, (int)material.m_dY,
                        material.optmizeClip.clipElement.clipRect.X, material.optmizeClip.clipElement.clipRect.Y, material.optmizeClip.clipElement.clipRect.Width,
                        material.optmizeClip.clipElement.clipRect.Height, Consts.TRANS_NONE);
                }
            }
            else
            {
                imgGenerate = null;
            }
            MImgElement imgElementNew = null;
            //将结果映射到切片单元
            if (castData)
            {
                //生成新的图片对象
                imgElementNew = new MImgElement(null);//使用组内第一张图片的名称
                imgElementNew.name = imgElements[0].name;
                imgElementNew.image = imgGenerate;
                //映射数据和图片对象
                for (int i = 0; i < sorter.m_MaterialList.Count; i++)
                {
                    RectMaterial material = (RectMaterial)sorter.m_MaterialList[i];
                    material.CastBaseClip(imgElementNew);
                }
            }
            return imgElementNew;

        }

    }
}
