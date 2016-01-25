using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Cyclone.alg;
using DockingUI.WinFormsUI.Docking;
using Cyclone.mod;
using Cyclone.alg.type;
using Cyclone.mod.anim;
using Cyclone.alg.math;
using Cyclone.alg.util;

namespace Cyclone.mod.anim
{
    public partial class Form_MConfig : DockContent
    {
        private static bool noEvent = false;
        Form_MAnimation form_MA;
        public Form_MConfig(Form_MAnimation form_MAT)
        {
            InitializeComponent();
            form_MA = form_MAT;
            if (checkBox_ShowGrid != null)
            {
                checkBox_ShowGrid.Checked = Consts.showBgGrid;
            }
            if (comboBox_screen != null&&comboBox_screen.Items.Count>0)
            {
                comboBox_screen.Text = (String)comboBox_screen.Items[0];
            }
        }
        protected override string GetPersistString()
        {
            return "Form_MConfig";
        }
        private void button_BgColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new System.Windows.Forms.ColorDialog();
            colorDialog.Color = button_bgNetColor.BackColor;
            colorDialog.FullOpen = true;
            DialogResult dr = colorDialog.ShowDialog();
            if (dr.Equals(DialogResult.OK))
            {
                button_bgNetColor.BackColor = colorDialog.Color;
                Consts.colorAnimBG = button_bgNetColor.BackColor.ToArgb();
                if (!Consts.showBgGrid)
                {
                    Consts.colorAnimSelect = GraphicsUtil.getOpposingColor(Consts.colorAnimBG);
                }
                form_MA.form_MFrameEdit.UpdateRegion_EditFrame();
            }
        }

        private void comboBox_screen_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Form_MAnimation.noCheckEvent)
            {
                return;
            }
            String s = (String)comboBox_screen.Items[comboBox_screen.SelectedIndex];
            String[] contents = s.Split(new char[] { '×' });
            if (contents == null || contents.Length != 2)
            {
                return;
            }
            Consts.screenWidth = int.Parse(contents[0]);
            Consts.screenHeight = int.Parse(contents[1]);
            form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
        }

        private void checkBox_showScreen_CheckedChanged(object sender, EventArgs e)
        {
            if (Form_MAnimation.noCheckEvent)
            {
                return;
            }
            Consts.showScreenFrame = this.checkBox_showScreen.Checked;
            form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
        }
        //编辑区 网格多选框
        public void checkBox_ShowGrid_CheckedChanged(object sender, EventArgs e)
        {
            if (Form_MAnimation.noCheckEvent)
            {
                return;
            }
            Consts.showBgGrid = checkBox_ShowGrid.Checked;
            if (Consts.showBgGrid)
            {
                Consts.colorAnimSelect = 0;
            }
            else
            {
                Consts.colorAnimSelect = GraphicsUtil.getOpposingColor(Consts.colorAnimBG);
            }
            form_MA.form_MFrameEdit.UpdateRegion_EditFrame();
        }
        public MFrameUnit Unit
        {
            get 
            {
                if (CurrentUnits.Count == 0)
                {
                    return null;
                }
                else if (CurrentUnits.Count == 1)
                {
                    return CurrentUnits[0];
                }
                else
                {
                    if (form_MA.form_MFrameEdit.TransformNew != null)
                    {
                        return form_MA.form_MFrameEdit.TransformNew;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        private Nullable<ValueTransform> UnitVary
        {
            get
            {
                if (CurrentUnits.Count == 0)
                {
                    return null;
                }
                else if (CurrentUnits.Count == 1)
                {
                    return CurrentUnits[0].getValueTransform(Form_MTimeLine.timePosition);
                }
                else
                {
                    if (form_MA.form_MFrameEdit.TransformNew != null)
                    {
                        ValueTransform transform = new ValueTransform();
                        transform.setValue(form_MA.form_MFrameEdit.TransformNew);
                        return transform;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public List<MFrameUnit> CurrentUnits
        {
            get { return form_MA.form_MFrameEdit.currentUnits; }
        }
        public void checkUnitProperty()
        {
            if (CurrentUnits.Count ==0)
            {
                panel_unitInfor.Enabled = false;
            }
            else if (CurrentUnits.Count == 1)
            {
                panel_unitInfor.Enabled = true;
                button_TransReset.Enabled = true;
                setEnableNUD(true);
                noEvent = true;
                inputValues();
                noEvent = false;
                NUD_Alpha.Enabled = true;
            }
            else if (CurrentUnits.Count != 1)
            {
                panel_unitInfor.Enabled = true;
                if (form_MA.form_MFrameEdit.TransformNew != null)
                {
                    button_TransReset.Enabled = false;
                    NUD_Alpha.Enabled = false;
                    noEvent = true;
                    inputValues();
                    noEvent = false;
                    setEnableNUD(false);
                }
                else
                {
                    button_TransReset.Enabled = true;
                    setEnableNUD(false);
                    NUD_Alpha.Enabled = true;
                    inputAlphaValue();
                }

            }
            setNUDFresh();
        }

        private void setEnableNUD(bool enable)
        {
            NUD_AnchorX.Enabled = enable;
            NUD_AnchorY.Enabled = enable;
            NUD_PosX.Enabled = enable;
            NUD_PosY.Enabled = enable;
            NUD_Rotate.Enabled = enable;
            NUD_ScaleX.Enabled = enable;
            NUD_ScaleY.Enabled = enable;
            NUD_SizeH.Enabled = enable;
            NUD_SizeW.Enabled = enable;
        }
        private void setNUDFresh()
        {
            NUD_AnchorX.Refresh();
            NUD_AnchorY.Refresh();
            NUD_PosX.Refresh();
            NUD_PosY.Refresh();
            NUD_Rotate.Refresh();
            NUD_ScaleX.Refresh();
            NUD_ScaleY.Refresh();
            NUD_SizeH.Refresh();
            NUD_SizeW.Refresh();
        }
        //从单元输入数值
        private void inputValues()
        {
            try
            {
                ValueTransform valueT = UnitVary.Value;
                NUD_ScaleX.Value = (decimal)valueT.scale.X;
                NUD_ScaleY.Value = (decimal)valueT.scale.Y;
                NUD_Rotate.Value = (decimal)valueT.rotateDegree;
                //NUD_ShearX.Value = (decimal)unit.shearX;
                //NUD_ShearY.Value = (decimal)unit.shearY;
                NUD_PosX.Value = (decimal)valueT.pos.X;
                NUD_PosY.Value = (decimal)valueT.pos.Y;
                NUD_AnchorX.Value = (decimal)valueT.anchor.X;
                NUD_AnchorY.Value = (decimal)valueT.anchor.Y;
                NUD_SizeW.Value = (decimal)Math.Abs(valueT.scale.X * Unit.getSize().Width);
                NUD_SizeH.Value = (decimal)Math.Abs(valueT.scale.Y * Unit.getSize().Height);
                NUD_Alpha.Value = (decimal)valueT.alpha;
                RectangleF box = Unit.getTransformBox();
                textBox_RoomW.Text = "" + MathUtil.get2PlaceFloat(Math.Abs(box.Width));
                textBox_RoomH.Text = "" + MathUtil.get2PlaceFloat(Math.Abs(box.Height));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private void inputAlphaValue()
        {
            noEvent = true;
            //如果Alpha值相同，显示统一值
            float alpha = -1;
            foreach (MFrameUnit unitI in CurrentUnits)
            {
                if (alpha < 0)
                {
                    alpha = unitI.alpha;
                }
                else
                {
                    if (alpha != unitI.alpha)
                    {
                        alpha = 1.0f;
                        break;
                    }
                }
            }
            if (alpha < 0)
            {
                alpha = 1.0f;
            }
            NUD_Alpha.Value = (decimal)alpha;
            noEvent = false;
        }

        private void NUD_ValueChanged()
        {
            if (noEvent)
            {
                return;
            }
            if (Unit != null)
            {
                form_MA.historyManager.ReadyHistory(HistoryType.Action);
                form_MA.form_MFrameEdit.checkTransitFrame();
                outputValues();
                form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
                form_MA.historyManager.AddHistory(HistoryType.Action);
            }

        }
        //输出数值
        private void outputValues()
        {
            Unit.scaleX = (float)NUD_ScaleX.Value;
            Unit.scaleY = (float)NUD_ScaleY.Value;
            Unit.rotateDegree = (float)NUD_Rotate.Value;
            //unit.shearX = (float)NUD_ShearX.Value;
            //unit.shearY = (float)NUD_ShearY.Value;
            Unit.posX = (float)NUD_PosX.Value;
            Unit.posY = (float)NUD_PosY.Value;
            Unit.anchorX = (float)NUD_AnchorX.Value;
            Unit.anchorY = (float)NUD_AnchorY.Value;
            Unit.alpha = (float)NUD_Alpha.Value;
            form_MA.form_MFrameEdit.checkSingleTransform();
        }

        private void button_TransReset_Click(object sender, EventArgs e)
        {
            noEvent = true;
            form_MA.form_MFrameEdit.resetTransform();
            noEvent = false;
        }

        private void button_TransCopy_Click(object sender, EventArgs e)
        {

        }

        private void NUD_SizeW_ValueChanged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            if (Unit != null)
            {
                form_MA.historyManager.ReadyHistory(HistoryType.Action);
                noEvent = true;
                form_MA.form_MFrameEdit.checkTransitFrame();
                float newWidth = (float)NUD_SizeW.Value;
                float newScaleX = (newWidth / Unit.getSize().Width) * (UnitVary.Value.scale.X >= 0 ? 1 : -1);
                Unit.scaleX = newScaleX;
                inputValues();
                noEvent = false;
                outputValues();
                form_MA.historyManager.AddHistory(HistoryType.Action);
                form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
            }
        }

        private void NUD_SizeH_ValueChanged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            if (Unit != null)
            {
                form_MA.historyManager.ReadyHistory(HistoryType.Action);
                noEvent = true;
                form_MA.form_MFrameEdit.checkTransitFrame();
                float newHeight = (float)NUD_SizeH.Value;
                float newScaleY = (newHeight / Unit.getSize().Height) * (UnitVary.Value.scale.Y >= 0 ? 1 : -1);
                Unit.scaleY = newScaleY;
                inputValues();
                noEvent = false;
                outputValues();
                form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
                form_MA.historyManager.AddHistory(HistoryType.Action);
            }
        }
        private void NUD_Rotate_ValueChanged(object sender, EventArgs e)
        {
            NUD_ValueChanged();
        }

        private void NUD_ScaleX_ValueChanged(object sender, EventArgs e)
        {
            NUD_ValueChanged();
        }

        private void NUD_ScaleY_ValueChanged(object sender, EventArgs e)
        {
            NUD_ValueChanged();
        }

        private void NUD_PosX_ValueChanged(object sender, EventArgs e)
        {
            NUD_ValueChanged();
        }

        private void NUD_PosY_ValueChanged(object sender, EventArgs e)
        {
            NUD_ValueChanged();
        }

        private void NUD_AnchorX_ValueChanged(object sender, EventArgs e)
        {
            NUD_ValueChanged();
        }

        private void NUD_AnchorY_ValueChanged(object sender, EventArgs e)
        {
            NUD_ValueChanged();
        }

        private void button_AxisColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new System.Windows.Forms.ColorDialog();
            colorDialog.Color = button_AxisColor.BackColor;
            colorDialog.FullOpen = true;
            DialogResult dr = colorDialog.ShowDialog();
            if (dr.Equals(DialogResult.OK))
            {
                button_AxisColor.BackColor = colorDialog.Color;
                Consts.colorMFrameEdit_Axis = button_AxisColor.BackColor.ToArgb();
                form_MA.form_MFrameEdit.UpdateRegion_EditFrame();
            }
        }

        private void checkBox_ShowAxis_CheckedChanged(object sender, EventArgs e)
        {
            if (Form_MAnimation.noCheckEvent)
            {
                return;
            }
            Consts.showMFrameEdit_Axis = this.checkBox_ShowAxis.Checked;
            form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
        }

        private void NUD_Alpha_ValueChanged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            if (CurrentUnits == null)
            {
                return;
            }
            form_MA.historyManager.ReadyHistory(HistoryType.Action);
            form_MA.form_MFrameEdit.checkTransitFrame();
            //设置统一值
            foreach (MFrameUnit unitI in CurrentUnits)
            {
                unitI.alpha = (float)NUD_Alpha.Value;
            }
            form_MA.historyManager.AddHistory(HistoryType.Action);
            form_MA.form_MFrameEdit.UpdateRegion_EditAndFrameLevel();
        }
        //#####################################布局面板操作#######################################
        private void button_AlignLeft_Click(object sender, EventArgs e)
        {
            form_MA.form_MFrameEdit.LayoutOperations(Form_MFrameEdit.LayOuts.Align_Left, 0, 0, checkBox_relateCanvas.Checked);
        }

        private void button_AlignHCenter_Click(object sender, EventArgs e)
        {
            form_MA.form_MFrameEdit.LayoutOperations(Form_MFrameEdit.LayOuts.Align_HCenter, 0, 0, checkBox_relateCanvas.Checked);
        }

        private void button_AlignRight_Click(object sender, EventArgs e)
        {
            form_MA.form_MFrameEdit.LayoutOperations(Form_MFrameEdit.LayOuts.Align_Right, 0, 0, checkBox_relateCanvas.Checked);
        }

        private void button_AlignTop_Click(object sender, EventArgs e)
        {
            form_MA.form_MFrameEdit.LayoutOperations(Form_MFrameEdit.LayOuts.Align_Top, 0, 0, checkBox_relateCanvas.Checked);
        }

        private void button_AlignVCenter_Click(object sender, EventArgs e)
        {
            form_MA.form_MFrameEdit.LayoutOperations(Form_MFrameEdit.LayOuts.Align_VCenter, 0, 0, checkBox_relateCanvas.Checked);
        }

        private void button_AlignBottom_Click(object sender, EventArgs e)
        {
            form_MA.form_MFrameEdit.LayoutOperations(Form_MFrameEdit.LayOuts.Align_Bottom, 0, 0, checkBox_relateCanvas.Checked);
        }

        private void button_scatterV_Click(object sender, EventArgs e)
        {
            form_MA.form_MFrameEdit.LayoutOperations(Form_MFrameEdit.LayOuts.Scatter_V, 0, 0, checkBox_relateCanvas.Checked);
        }

        private void button_scatterH_Click(object sender, EventArgs e)
        {
            form_MA.form_MFrameEdit.LayoutOperations(Form_MFrameEdit.LayOuts.Scatter_H, 0, 0, checkBox_relateCanvas.Checked);
        }

        private void button_matchV_Click(object sender, EventArgs e)
        {
            form_MA.form_MFrameEdit.LayoutOperations(Form_MFrameEdit.LayOuts.Match_V, 0, 0, checkBox_relateCanvas.Checked);
        }

        private void button_matchH_Click(object sender, EventArgs e)
        {
            form_MA.form_MFrameEdit.LayoutOperations(Form_MFrameEdit.LayOuts.Match_H, 0, 0, checkBox_relateCanvas.Checked);
        }

        private void button_MirrorH_Click(object sender, EventArgs e)
        {
            form_MA.form_MFrameEdit.LayoutOperations(Form_MFrameEdit.LayOuts.Mirror_H, 0, 0, checkBox_relateCanvas.Checked);
        }

        private void button_MirrorV_Click(object sender, EventArgs e)
        {
            form_MA.form_MFrameEdit.LayoutOperations(Form_MFrameEdit.LayOuts.Mirror_V, 0, 0, checkBox_relateCanvas.Checked);
        }

        private void button_MirrorTL_Click(object sender, EventArgs e)
        {
            form_MA.form_MFrameEdit.LayoutOperations(Form_MFrameEdit.LayOuts.Mirror_TL, 0, 0, checkBox_relateCanvas.Checked);
        }

        private void button_MirrorTR_Click(object sender, EventArgs e)
        {
            form_MA.form_MFrameEdit.LayoutOperations(Form_MFrameEdit.LayOuts.Mirror_TR, 0, 0, checkBox_relateCanvas.Checked);
        }

        private void button_Anchor00_Click(object sender, EventArgs e)
        {
            form_MA.form_MFrameEdit.LayoutOperations(Form_MFrameEdit.LayOuts.AnchorSet, 0.0f, 1.0f, checkBox_relateCanvas.Checked);
        }

        private void button_Anchor01_Click(object sender, EventArgs e)
        {
            form_MA.form_MFrameEdit.LayoutOperations(Form_MFrameEdit.LayOuts.AnchorSet, 0.5f, 1.0f, checkBox_relateCanvas.Checked);
        }

        private void button_Anchor02_Click(object sender, EventArgs e)
        {
            form_MA.form_MFrameEdit.LayoutOperations(Form_MFrameEdit.LayOuts.AnchorSet, 1.0f, 1.0f, checkBox_relateCanvas.Checked);
        }

        private void button_Anchor10_Click(object sender, EventArgs e)
        {
            form_MA.form_MFrameEdit.LayoutOperations(Form_MFrameEdit.LayOuts.AnchorSet, 0.0f, 0.5f, checkBox_relateCanvas.Checked);
        }

        private void button_Anchor11_Click(object sender, EventArgs e)
        {
            form_MA.form_MFrameEdit.LayoutOperations(Form_MFrameEdit.LayOuts.AnchorSet, 0.5f, 0.5f, checkBox_relateCanvas.Checked);
        }

        private void button_Anchor12_Click(object sender, EventArgs e)
        {
            form_MA.form_MFrameEdit.LayoutOperations(Form_MFrameEdit.LayOuts.AnchorSet, 1.0f, 0.5f, checkBox_relateCanvas.Checked);
        }

        private void button_Anchor20_Click(object sender, EventArgs e)
        {
            form_MA.form_MFrameEdit.LayoutOperations(Form_MFrameEdit.LayOuts.AnchorSet, 0.0f, 0.0f, checkBox_relateCanvas.Checked);
        }

        private void button_Anchor21_Click(object sender, EventArgs e)
        {
            form_MA.form_MFrameEdit.LayoutOperations(Form_MFrameEdit.LayOuts.AnchorSet, 0.5f, 0.0f, checkBox_relateCanvas.Checked);
        }

        private void button_Anchor22_Click(object sender, EventArgs e)
        {
            form_MA.form_MFrameEdit.LayoutOperations(Form_MFrameEdit.LayOuts.AnchorSet, 1.0f, 0.0f, checkBox_relateCanvas.Checked);
        }

        private void button_AlignSetHCenter_Click(object sender, EventArgs e)
        {
            form_MA.form_MFrameEdit.LayoutOperations(Form_MFrameEdit.LayOuts.Align_SetH, 0.0f, 0.0f, checkBox_relateCanvas.Checked);
        }

        private void button_AlignSetVCenter_Click(object sender, EventArgs e)
        {
            form_MA.form_MFrameEdit.LayoutOperations(Form_MFrameEdit.LayOuts.Align_SetV, 0.0f, 0.0f, checkBox_relateCanvas.Checked);
        }

        private void checkBox_AlignPixel_CheckedChanged(object sender, EventArgs e)
        {
            if (noEvent)
            {
                return;
            }
            Consts.moveAlignPixel = !Consts.moveAlignPixel;
            noEvent = true;
            checkBox_AlignPixel.Checked = Consts.moveAlignPixel;
            noEvent = false;
        }
        public void showInfor(String text)
        {
            this.richTextBox_infor.Text = text;
        }

        private void button_changeLevelUp_Click(object sender, EventArgs e)
        {
            form_MA.form_MFrameEdit.moveUpCurrentClip();
        }

        private void button_changeLevelDown_Click(object sender, EventArgs e)
        {
            form_MA.form_MFrameEdit.moveDownCurrentClip();
        }
    }
}