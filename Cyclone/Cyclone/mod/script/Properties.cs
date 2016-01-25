using System;
using System.Collections.Generic;
using System.Text;
using Cyclone.alg;
using System.Collections;
using System.IO;
using Cyclone.mod.util;
using Cyclone.alg.type;
using Cyclone.alg.util;
using MSExcel = Microsoft.Office.Interop.Excel;
using System.Reflection;
namespace Cyclone.mod.script
{
    /****************************** �������͹����� ******************************/
    public class PropertyTypesManager : ObjectVector, SerializeAble
    {
        public Form_Main form_main = null;
        public PropertyTypesManager(Form_Main form_mainT)
        {
            form_main = form_mainT;
        }
        //�ϲ�
        public void combine(PropertyTypesManager srcPropertyTypesManager, ArrayList ids)
        {
            for (int i = 0; i < srcPropertyTypesManager.getElementCount(); i++)
            {
                if (ids == null || !ids.Contains(i))
                {
                    continue;
                }
                PropertyTypeElement srcPropertyTypeElement = (PropertyTypeElement)srcPropertyTypesManager.getElement(i);
                //Ѱ����ͬ���������͵�Ԫ
                PropertyTypeElement localPropertyTypeElement = null;
                for (int j = 0; j < getElementCount(); j++)
                {
                    PropertyTypeElement tempPropertyTypeElement = (PropertyTypeElement)getElement(j);
                    if (tempPropertyTypeElement.name.Equals(srcPropertyTypeElement.name))
                    {
                        localPropertyTypeElement = tempPropertyTypeElement;
                        break;
                    }
                }
                //�ҵ���ͬ���Ƶı��ص�Ԫ
                if (localPropertyTypeElement != null)
                {
                    localPropertyTypeElement.combine(srcPropertyTypeElement);
                }
                else//���뱾����
                {
                    srcPropertyTypeElement.combineTo(this);
                }
            }
        }
        //��д
        #region SerializeAble Members

        public void ReadObject(System.IO.Stream s)
        {
            short len = 0;
            len = IOUtil.readShort(s);

            for (int i = 0; i < len; i++)
            {
                PropertyTypeElement element = new PropertyTypeElement(this);
                addElement(element);
            }
            for (int i = 0; i < len; i++)
            {
                PropertyTypeElement element = (PropertyTypeElement)getElement(i);
                element.readObjectInit(s);
            }
            for (int i = 0; i < len; i++)
            {
                PropertyTypeElement element = (PropertyTypeElement)getElement(i);
                element.ReadObject(s);
            }
        }

        public void WriteObject(System.IO.Stream s)
        {
            short len = (short)getElementCount();
            IOUtil.writeShort(s, len);
            for (int i = 0; i < len; i++)
            {
                PropertyTypeElement element = (PropertyTypeElement)getElement(i);
                element.writeObjectInit(s);
            }
            for (int i = 0; i < len; i++)
            {
                PropertyTypeElement element = (PropertyTypeElement)getElement(i);
                element.WriteObject(s);
            }
        }

        public void ExportObject(System.IO.Stream fs_bin)
        {
            short len = (short)getElementCount();
            IOUtil.writeShort(fs_bin, len);
            //��һ����������ʼ����Ϣ
            for (int i = 0; i < len; i++)
            {
                PropertyTypeElement element = (PropertyTypeElement)getElement(i);
                element.exportObjectInit(fs_bin);
            }
            //�ڶ������������ֵ��Ϣ
            for (int i = 0; i < len; i++)
            {
                PropertyTypeElement element = (PropertyTypeElement)getElement(i);
                element.ExportObject(fs_bin);
            }
        }
        #endregion

        //��ȡ����ʹ�õ����ı�
        public String getAllTxt()
        {
            String s = "";
            short len = (short)getElementCount();
            for (int i = 0; i < len; i++)
            {
                PropertyTypeElement element = (PropertyTypeElement)getElement(i);
                s += element.name;
                for (int j = 0; j < element.propertiesManager.getElementCount(); j++)
                {
                    PropertyElement property = (PropertyElement)element.propertiesManager.getElement(j);
                    s += property.name;
                }
                for (int j = 0; j < element.instancesManager.getElementCount(); j++)
                {
                    InstanceElement instance = (InstanceElement)element.instancesManager.getElement(j);
                    s += instance.name;
                    for (int k = 0; k < instance.propertyValueManager.getElementCount(); k++)
                    {
                        PropertyValueElement propertyValueElement = (PropertyValueElement)instance.propertyValueManager.getElement(k);
                        s += propertyValueElement.name;
                        if (propertyValueElement.ValueType == Consts.PARAM_STR)
                        {
                            Object value = propertyValueElement.getValue();
                            if (value != null && value is String)
                            {
                                s += value;
                            }
                        }
                    }

                }
            }
            return s;
        }
        //���������õ������ݳ��ı���ʽ
        public void exportAllToTxt(Stream fs)
        {
            short len = (short)getElementCount();
            String expAll = "";
            String lineEnd = "\r\n";
            for (int i = 0; i < len; i++)
            {
                PropertyTypeElement element = (PropertyTypeElement)getElement(i);
                expAll += element.name + lineEnd;
                for (int j = 0; j < element.propertiesManager.getElementCount(); j++)
                {
                    PropertyElement property = (PropertyElement)element.propertiesManager.getElement(j);
                    expAll += property.name + lineEnd;
                }
                for (int j = 0; j < element.instancesManager.getElementCount(); j++)
                {
                    InstanceElement instance = (InstanceElement)element.instancesManager.getElement(j);
                    expAll += instance.name + lineEnd;
                    for (int k = 0; k < instance.propertyValueManager.getElementCount(); k++)
                    {
                        PropertyValueElement propertyValueElement = (PropertyValueElement)instance.propertyValueManager.getElement(k);
                        if (propertyValueElement.ValueType == Consts.PARAM_STR || propertyValueElement.ValueType == Consts.PARAM_INT)
                        {
                            Object value = propertyValueElement.getValue();
                            if (value != null && (value is String||value is int))
                            {
                                if (value is String)
                                {
                                    expAll += (String)value + lineEnd;
                                }
                                else
                                {
                                    expAll += "" + Convert.ToInt32(value) + lineEnd;
                                }
                            }
                            else
                            {
                                expAll += "---" + lineEnd;
                            }
                        }
                        else
                        {
                            expAll += "---" + lineEnd;
                        }
                    }

                }
            }
            IOUtil.writeText(fs, expAll);
        }
        //���ı��ļ���������
        public void importAllFromTxt(Stream fs)
        {
            short len = (short)getElementCount();
            iText=-1;
            for (int i = 0; i < len; i++)
            {
                PropertyTypeElement element = (PropertyTypeElement)getElement(i);
                String content = getNextLine(fs);
                if (content != null)
                {
                    element.name = content;
                }
                for (int j = 0; j < element.propertiesManager.getElementCount(); j++)
                {
                    PropertyElement property = (PropertyElement)element.propertiesManager.getElement(j);
                    content = getNextLine(fs);
                    if (content != null)
                    {
                        property.name = content;
                    }
                }
                for (int j = 0; j < element.instancesManager.getElementCount(); j++)
                {
                    InstanceElement instance = (InstanceElement)element.instancesManager.getElement(j);
                    content = getNextLine(fs);
                    if (content != null)
                    {
                        instance.name = content;
                    }
                    for (int k = 0; k < instance.propertyValueManager.getElementCount(); k++)
                    {
                        PropertyValueElement propertyValueElement = (PropertyValueElement)instance.propertyValueManager.getElement(k);
                        content = getNextLine(fs);
                        if (content == null)
                        {
                            continue;
                        }
                        if (propertyValueElement.ValueType == Consts.PARAM_STR || propertyValueElement.ValueType == Consts.PARAM_INT)
                        {
                            Object value = propertyValueElement.getValue();
                            if (value != null && (value is String || value is int))
                            {
                                if (value is String)
                                {
                                    propertyValueElement.setValue(content);
                                }
                                else
                                {
                                    propertyValueElement.setValue(Convert.ToInt32(content));
                                }
                            }
                        }
                    }

                }
            }
        }
        //���������õ������ݳ�Excel��ʽ
        public void exportAllToExcel(String path)
        {
            short len = (short)getElementCount();
            MSExcel.Application excelApp=null;  //ExcelӦ�ó��� 
            MSExcel.Workbook excelWorkbook;  //Excel�ĵ� 
            try
            {
                excelApp = new MSExcel.Application();
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                Object nothing = Missing.Value;
                excelWorkbook = excelApp.Workbooks.Add(nothing);
                Object format = MSExcel.XlFileFormat.xlWorkbookNormal;
                for (int i = 0; i < len; i++)
                {
                    PropertyTypeElement element = (PropertyTypeElement)getElement(i);
                    MSExcel.Worksheet xSheet = (MSExcel.Worksheet)excelWorkbook.Sheets[i+1];
                    xSheet.Name = element.name;
                    xSheet.Cells[1, 1] = "��������";
                    xSheet.Cells[1, 2] = "��������";
                    xSheet.Cells[1, 3] = "���Գ�ֵ";
                    for (int j = 0; j < element.propertiesManager.getElementCount(); j++)
                    {
                        PropertyElement property = (PropertyElement)element.propertiesManager.getElement(j);
                        xSheet.Cells[j+2, 1] = property.name;
                        xSheet.Cells[j + 2, 2] = Consts.getParamType(property.ValueType);
                        xSheet.Cells[j + 2, 3] = property.getDefaultValue();
                    }

                    for (int j = 0; j < element.instancesManager.getElementCount(); j++)
                    {
                        InstanceElement instance = (InstanceElement)element.instancesManager.getElement(j);
                        xSheet.Cells[1, 4 + j] = instance.name;
                        for (int k = 0; k < instance.propertyValueManager.getElementCount(); k++)
                        {
                            PropertyValueElement propertyValueElement = (PropertyValueElement)instance.propertyValueManager.getElement(k);
                            String valueDest = "";
                            Object value = propertyValueElement.getValue();
                            if (value != null)
                            {
                                if (value is String)
                                {
                                    valueDest = (String)value;
                                }
                                else if (value is int)
                                {
                                    valueDest = "" + Convert.ToInt32(value);
                                }
                                else
                                {
                                    valueDest = "" + ((InstanceElement)(value)).name;
                                }
                            }
                            else
                            {
                                valueDest = "";
                            }
                            xSheet.Cells[2 + k, 4 + j] = valueDest;
                        }

                    }
                }
                excelWorkbook.SaveAs(path, nothing, nothing, nothing, nothing, nothing, MSExcel.XlSaveAsAccessMode.xlShared, nothing, nothing, nothing, nothing, nothing);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (excelApp!=null)
                {
                    excelApp.Quit();
                }
                excelWorkbook = null;
                excelApp = null;
            }
          

        }
        //��Excel�ļ�����
        public void importAllFromExcel(string filePath_excel)
        {
            short lenCrrent = (short)getElementCount();
            iText = -1;
            MSExcel.Application excelApp = null;  //ExcelӦ�ó��� 
            MSExcel.Workbook excelWorkbook;  //Excel�ĵ� 
            try
            {
                excelApp = new MSExcel.Application();
                Object nothing = Missing.Value;
                excelWorkbook = excelApp.Workbooks._Open(filePath_excel, nothing, nothing, nothing,
                    nothing, nothing, nothing, nothing, nothing, nothing, nothing, nothing, nothing);
                for (int i = 0; i < lenCrrent; i++)
                {
                    PropertyTypeElement element = (PropertyTypeElement)getElement(i);
                    if(i>=excelWorkbook.Sheets.Count)
                    {
                        break;
                    }
                    MSExcel.Worksheet xSheet = (MSExcel.Worksheet)excelWorkbook.Sheets[i + 1];
                    if (xSheet == null)
                    {
                        break;
                    }
                    String content = xSheet.Name;
                    if (content != null)
                    {
                        element.name = content;
                    }
                    for (int j = 0; j < element.propertiesManager.getElementCount(); j++)
                    {
                        PropertyElement property = (PropertyElement)element.propertiesManager.getElement(j);
                        //��������
                        content = getCellContent(xSheet.Cells[2 + j, 1]);
                        if (content != null)
                        {
                            property.name = content;
                        }
                        //���Գ�ֵ
                        content = getCellContent(xSheet.Cells[2 + j, 3]);
                        if (content != null)
                        {
                            property.setDefaultValue(content);
                        }
                    }
                    //��ֵ
                    for (int j = 0; j < element.instancesManager.getElementCount(); j++)
                    {
                        InstanceElement instance = (InstanceElement)element.instancesManager.getElement(j);
                        content = getCellContent(xSheet.Cells[1, 4 + j]);
                        if (content != null)
                        {
                            instance.name = content;
                        }
                        for (int k = 0; k < instance.propertyValueManager.getElementCount(); k++)
                        {
                            PropertyValueElement propertyValueElement = (PropertyValueElement)instance.propertyValueManager.getElement(k);
                            content = getCellContent(xSheet.Cells[2 + k, 4 + j]);
                            if (content == null)
                            {
                                continue;
                            }
                            propertyValueElement.setValueFromObj(content);
                        }

                    }
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (excelApp != null)
                {
                    excelApp.Quit();
                }
                excelWorkbook = null;
                excelApp = null;
            }
        }
        private static String getCellContent(object o)
        {
            String content = "";
            if (o!=null && o is MSExcel.Range)
            {
                try
                {
                    o = ((MSExcel.Range)o).Value2;
                    content = Convert.ToString(o);
                }
                catch (System.Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return content;
        }
        //��һ�仰�з����ָ����ǰ���ַ������ҷ��ؽ�������ǰ���ַ�����ȷ�������ؿ�
        private static ArrayList arrayText = new ArrayList();
        private static int iText=-1;
        private String getNextLine(Stream fs)
        {
            if (iText < 0 || iText >= arrayText.Count)
            {
                arrayText = IOUtil.readTextLinesGBK(fs);
                iText = 0;
            }
            String text = (String)arrayText[iText];
            iText++;
            return text;
        }
    }
    /****************************** �������͵�Ԫ ******************************/
    public class PropertyTypeElement : ObjectElement, SerializeAble
    {
        public PropertiesManager propertiesManager = null;//���Թ���
        public InstancesManager instancesManager = null;  //ʵ������
        public PropertyTypeElement(PropertyTypesManager parentT)
            : base(parentT)
        {
            propertiesManager = new PropertiesManager(this,((PropertyTypesManager)parent).form_main);
            instancesManager = new InstancesManager(this);
        }
        //�ϲ�
        public void combine(PropertyTypeElement srcPropertyTypeElement)
        {
            if (srcPropertyTypeElement == null)
            {
                return;
            }
            propertiesManager.combine(srcPropertyTypeElement.propertiesManager);
            instancesManager.combine(srcPropertyTypeElement.instancesManager);
        }
        //�ϲ���
        public void combineTo(PropertyTypesManager destPropertyTypesManager)
        {
            this.parent = destPropertyTypesManager;
            //ȥ����������
            for (int i = 0; i < propertiesManager.getElementCount(); i++)
            {
                PropertyElement propertyElement = (PropertyElement)propertiesManager.getElement(i);
                propertyElement.propertyTypeElementUsed = null;
            }
            for (int i = 0; i < instancesManager.getElementCount(); i++)
            {
                InstanceElement instanceElement = (InstanceElement)instancesManager.getElement(i);
                instanceElement.propertyValueManager.clearDependProperty();
            }
            destPropertyTypesManager.addElement(this);
        }
        public void setName(String name)
        {
            this.name = name;
            ((PropertyTypesManager)parent).refreshUI_Element(this.getID());
        }
        public override String getValueToLenString()
        {
            return name;
        }
        public override int getUsedTime()
        {
            int time = 0;
            PropertyTypesManager propertyTypesManager = (PropertyTypesManager)(parent);
            for (int i = 0; i < propertyTypesManager.getElementCount(); i++)
            {
                PropertyTypeElement propertyTypeElement = (PropertyTypeElement)propertyTypesManager.getElement(i);
                PropertiesManager propertiesManager = (PropertiesManager)propertyTypeElement.propertiesManager;
                for (int j = 0; j < propertiesManager.getElementCount(); j++)
                {
                    PropertyElement propertyElement = (PropertyElement)propertiesManager.getElement(j);
                    if (propertyElement.ValueType == Consts.PARAM_PROP)
                    {
                        if (propertyElement.propertyTypeElementUsed != null && propertyElement.propertyTypeElementUsed.Equals(this))
                        {
                            time++;
                        }
                    }
                }
            }
            return time;
        }
        public String getUsedTimeInf()
        {
            int time = 0;
            String inf = "";
            PropertyTypesManager propertyTypesManager = (PropertyTypesManager)(parent);
            for (int i = 0; i < propertyTypesManager.getElementCount(); i++)
            {
                PropertyTypeElement propertyTypeElement = (PropertyTypeElement)propertyTypesManager.getElement(i);
                PropertiesManager propertiesManager = (PropertiesManager)propertyTypeElement.propertiesManager;
                for (int j = 0; j < propertiesManager.getElementCount(); j++)
                {
                    PropertyElement propertyElement = (PropertyElement)propertiesManager.getElement(j);
                    if (propertyElement.ValueType == Consts.PARAM_PROP)
                    {
                        if (propertyElement.propertyTypeElementUsed != null && propertyElement.propertyTypeElementUsed.Equals(this))
                        {
                            inf += "�ڡ�" + propertyTypeElement.name + "��->��" + propertyElement.name + "����������\n";
                            time++;
                        }
                    }
                }
            }
            inf += "====���������ˡ�" + time + "����====\n";
            return inf;
        }
        #region SerializeAble Members
        public void readObjectInit(System.IO.Stream s)
        {
            name = IOUtil.readString(s);
            propertiesManager.readObjectInit(s);
            instancesManager.readObjectInit(s);
        }
        public void ReadObject(System.IO.Stream s)
        {
            propertiesManager.ReadObject(s);
            instancesManager.ReadObject(s);
        }
        public void writeObjectInit(System.IO.Stream s)
        {
            IOUtil.writeString(s, name);
            propertiesManager.writeObjectInit(s);
            instancesManager.writeObjectInit(s);
        }
        public void WriteObject(System.IO.Stream s)
        {
            propertiesManager.WriteObject(s);
            instancesManager.WriteObject(s);
        }

        public void exportObjectInit(System.IO.Stream fs_bin)
        {
            propertiesManager.exportObjectInit(fs_bin);
            instancesManager.exportObjectInit(fs_bin);
        }
        public void ExportObject(System.IO.Stream fs_bin)
        {
            propertiesManager.ExportObject(fs_bin);
            instancesManager.ExportObject(fs_bin);
        }
        #endregion

        public override ObjectElement clone()
        {
            PropertyTypeElement newInstance = new PropertyTypeElement((PropertyTypesManager)parent);
            baseCloneTo(newInstance);
            propertiesManager.cloneTo(newInstance.propertiesManager);
            newInstance.propertiesManager.resetElementParent();
            instancesManager.cloneTo(newInstance.instancesManager);
            newInstance.instancesManager.resetElementParent();
            //����propertiesManager
            for (int i = 0; i < newInstance.instancesManager.getElementCount(); i++)
            {
                InstanceElement inst = (InstanceElement)newInstance.instancesManager.getElement(i);
                inst.propertyValueManager.propertiesManager = newInstance.propertiesManager;
            }
            return newInstance;
        }
    }
    /****************************** �������Թ����� ******************************/
    public class PropertiesManager : ObjectVector, SerializeAble
    {
        public Form_Main form_main = null;
        public PropertyTypeElement parent = null;
        public PropertiesManager(PropertyTypeElement parentT, Form_Main form_mainT)
        {
            parent = parentT;
            form_main = form_mainT;
        }
        //�ϲ�
        public void combine(PropertiesManager srcPropertiesManager)
        {
            for (int i = 0; i < srcPropertiesManager.getElementCount(); i++)
            {
                PropertyElement srcPropertyElement = (PropertyElement)srcPropertiesManager.getElement(i);
                bool findSame=false;
                //Ѱ����ͬ
                for (int j = 0; j < getElementCount(); j++)
                {
                    PropertyElement tempPropertyElement = (PropertyElement)getElement(j);
                    if (tempPropertyElement.equalsPropertyElement(srcPropertyElement))
                    {
                        findSame=true;
                        break;
                    }
                }
                if (findSame)//����ԭ����
                {
                    continue;
                }
                else
                {
                    PropertyElement newPropertyElement = srcPropertyElement.getCopy(this);
                    addElement(newPropertyElement);
                    parent.instancesManager.addProperty(newPropertyElement);
                }
            }
        }
        #region SerializeAble Members

        public void readObjectInit(System.IO.Stream s)
        {
            short len = 0;
            len = IOUtil.readShort(s);

            for (int i = 0; i < len; i++)
            {
                PropertyElement element = new PropertyElement(this);
                addElement(element);
            }

        }
        public void ReadObject(System.IO.Stream s)
        {
            for (int i = 0; i < getElementCount(); i++)
            {
                PropertyElement element = (PropertyElement)this.getElement(i);
                element.ReadObject(s);
            }
        }
        public void writeObjectInit(System.IO.Stream s)
        {
            short len = (short)getElementCount();
            IOUtil.writeShort(s, len);
        }
        public void WriteObject(System.IO.Stream s)
        {
            short len = (short)getElementCount();
            for (int i = 0; i < len; i++)
            {
                PropertyElement element = (PropertyElement)getElement(i);
                element.WriteObject(s);
            }
        }
        public void exportObjectInit(System.IO.Stream fs_bin)
        {
            short len = (short)getElementCount();
            IOUtil.writeShort(fs_bin, len);
            for (int i = 0; i < len; i++)
            {
                PropertyElement element = (PropertyElement)getElement(i);
                element.ExportObject(fs_bin);
            }
        }
        public void ExportObject(System.IO.Stream fs_bin)
        {

        }
        #endregion
    }
    /****************************** �������Ե�Ԫ ******************************/
    public class PropertyElement : ObjectElement, SerializeAble
    {
        public byte ValueType = Consts.PARAM_INT;//��ֵ����
        public PropertyTypeElement propertyTypeElementUsed = null;
        public Object defaultValue = null;
        public PropertyElement(PropertiesManager parentT)
            : base(parentT)
        {

        }
        public Object getDefaultValue()
        {
            switch (ValueType)
            {
                case Consts.PARAM_INT:
                    if (defaultValue != null)
                    {
                        return defaultValue;
                    }
                    return 0;
                case Consts.PARAM_STR:
                    if (defaultValue != null)
                    {
                        return defaultValue;
                    }
                    return "δ����";
                case Consts.PARAM_INT_VAR:
                case Consts.PARAM_STR_VAR:
                    return null;
                case Consts.PARAM_PROP:
                    if (defaultValue != null && propertyTypeElementUsed!=null)
                    {
                        int id = Convert.ToInt32(defaultValue);
                        return propertyTypeElementUsed.instancesManager.getElement(id);
                    }
                    return null;
                case Consts.PARAM_INT_ID:
                    if (defaultValue != null)
                    {
                        int id=Convert.ToInt32(defaultValue);
                        return ((PropertiesManager)parent).form_main.iDsManager.getElement(id);
                    }
                    return null;
            }
            return null;
        }
        public void setDefaultValue(String defValue)
        {
            if (defValue == null)
            {
                return;
            }
            Object destValue = null;
            try
            {
                switch (ValueType)
                {
                    case Consts.PARAM_INT:
                        destValue = Convert.ToInt32(defValue);
                        break;
                    case Consts.PARAM_STR:
                        destValue = Convert.ToString(defValue);
                        break;
                    case Consts.PARAM_INT_VAR:
                    case Consts.PARAM_STR_VAR:
                        destValue =  null;
                        break;
                    case Consts.PARAM_PROP:
                        if (propertyTypeElementUsed != null)
                        {
                            String name = Convert.ToString(defValue);
                            if(name!=null)
                            {
                                for (int i = 0; i < propertyTypeElementUsed.instancesManager.getElementCount(); i++)
                                {
                                    InstanceElement instance= (InstanceElement)propertyTypeElementUsed.instancesManager.getElement(i);
                                    if (instance.name.Equals(name))
                                    {
                                        destValue = i;
                                        break;
                                    }
                                }
                            }
                        }
                        break;
                     case Consts.PARAM_INT_ID:
                        int id = Convert.ToInt32(defaultValue);
                        destValue =((PropertiesManager)parent).form_main.iDsManager.getElement(id);
                        break;
                }
                if (destValue!=null)
                {
                    defaultValue = destValue;
                }
            }
            catch (Exception e)
            { 
            }
        }
        //����(����¡��������)
        public PropertyElement getCopy(PropertiesManager parentT)
        {
            PropertyElement newInstance = new PropertyElement(parentT);
            newInstance.ValueType = ValueType;
            return newInstance;
        }
        //��¡
        public override ObjectElement clone()
        {
            PropertyElement newInstance = new PropertyElement((PropertiesManager)parent);
            baseCloneTo(newInstance);
            newInstance.ValueType = ValueType;
            newInstance.propertyTypeElementUsed = propertyTypeElementUsed;
            newInstance.defaultValue = defaultValue;
            return newInstance;
        }
        //�ж���ͬ
        public bool equalsPropertyElement(PropertyElement destPropertyElement)
        {
            if (destPropertyElement == null || destPropertyElement.ValueType != ValueType || !destPropertyElement.name.Equals(name))
            {
                return false;
            }
            return true;
        }
        public void setPropertyType(Object obj)
        {
            if (obj == null || !(obj is PropertyTypeElement))
            {
                propertyTypeElementUsed = null;
            }
            else 
            {
                propertyTypeElementUsed = (PropertyTypeElement)obj;
            }

        }
        public override String getValueToLenString()
        {
            return (ValueType == Consts.PARAM_INT ? "[INT]" : ValueType == Consts.PARAM_STR ? "[STR]" :
                ValueType == Consts.PARAM_INT_ID ? "[ID]" : "[POP_" 
                + (propertyTypeElementUsed==null?"null":propertyTypeElementUsed.name) + "]") + "   [" + name + "]";
        }
        public override int getUsedTime()
        {
            //....go on
            return 0;
        }
        //�����������ͣ��������õĶ���ID
        public int getPropertyTypeElementUsedID()
        {
            if (propertyTypeElementUsed == null)
            {
                return -1;
            }
            return propertyTypeElementUsed.getID();
        }
        public String getUsedInf()
        {
            String s = null;
            //....go on
            return s;
        }
        #region SerializeAble Members
        public void ReadObject(System.IO.Stream s)
        {
            name = IOUtil.readString(s);
            ValueType = IOUtil.readByte(s);
            if (ValueType == Consts.PARAM_PROP)
            {
                int id = IOUtil.readInt(s);
                propertyTypeElementUsed = (PropertyTypeElement)((PropertiesManager)parent).form_main.propertyTypesManager.getElement(id);
            }
            if (ValueType == Consts.PARAM_STR)
            {
                defaultValue = IOUtil.readString(s);
            }
            else
            {
                defaultValue = IOUtil.readInt(s);
            }

        }

        public void WriteObject(System.IO.Stream s)
        {
            IOUtil.writeString(s, name);
            IOUtil.writeByte(s, ValueType);
            if (ValueType == Consts.PARAM_PROP)
            {
                int id = -1;
                if (propertyTypeElementUsed != null)
                {
                    id = propertyTypeElementUsed.getID();
                }
                IOUtil.writeInt(s, id);
            }
            if (ValueType == Consts.PARAM_STR)
            {
                IOUtil.writeString(s, (String)defaultValue);
            }
            else
            {
                IOUtil.writeInt(s, Convert.ToInt32(defaultValue));
            }
        }

        public void ExportObject(System.IO.Stream fs_bin)
        {
            if (ValueType == Consts.PARAM_PROP)
            {
                byte id = Consts.PARAM_PROP;
                if (propertyTypeElementUsed != null)
                {
                    id = (byte)(Consts.PARAM_PROP + 1 + propertyTypeElementUsed.getID());
                }
                IOUtil.writeByte(fs_bin, id);
            }
            else
            {
                IOUtil.writeByte(fs_bin, ValueType);
            }
        }

        #endregion


    }
    /****************************** ����ʵ�������� ******************************/
    public class InstancesManager : ObjectVector, SerializeAble
    {
        public PropertyTypeElement parent = null;
        public InstancesManager(PropertyTypeElement parentT)
        {
            parent = parentT;
        }
        //�ϲ�
        public void combine(InstancesManager srcInstancesManager)
        {
            if (srcInstancesManager == null)
            {
                return;
            }
            for (int i = 0; i < srcInstancesManager.getElementCount(); i++)
            {
                InstanceElement srcInstanceElement = (InstanceElement)srcInstancesManager.getElement(i);
                bool findSame = false;
                //Ѱ����ͬ
                for (int j = 0; j < getElementCount(); j++)
                {
                    InstanceElement tempInstanceElement = (InstanceElement)getElement(j);
                    if (tempInstanceElement.equalsInstanceElement(srcInstanceElement))
                    {
                        findSame = true;
                        break;
                    }
                }
                if (findSame)//����ԭʵ��
                {
                    continue;
                }
                else//����µ�ʵ��
                {
                    InstanceElement newInstanceElement = new InstanceElement(this);
                    newInstanceElement.propertyValueManager.refreshProperty();
                    newInstanceElement.setName(srcInstanceElement.name);
                    addElement(newInstanceElement);
                }
            }

        }
        //��������
        public void addProperty(PropertyElement property)
        {
            for (int i = 0; i < getElementCount(); i++)
            {
                InstanceElement element = (InstanceElement)getElement(i);
                element.addProperty(property);
            }
        }
        //��������
        public void inseartProperty(PropertyElement property,int index)
        {
            for (int i = 0; i < getElementCount(); i++)
            {
                InstanceElement element = (InstanceElement)getElement(i);
                element.inseartProperty(property, index);
            }
        }
        //��������
        public void moveUpProperty(int index)
        {
            for (int i = 0; i < getElementCount(); i++)
            {
                InstanceElement element = (InstanceElement)getElement(i);
                element.moveUpProperty(index);
            }
        }
        //��������
        public void moveDownProperty(int index)
        {
            for (int i = 0; i < getElementCount(); i++)
            {
                InstanceElement element = (InstanceElement)getElement(i);
                element.moveDownProperty(index);
            }
        }
        //��������
        public void configProperty(PropertyElement property)
        {
            for (int i = 0; i < getElementCount(); i++)
            {
                InstanceElement element = (InstanceElement)getElement(i);
                element.configProperty(property);
            }
        }
        //��������
        public void configPropertyName(PropertyElement property)
        {
            for (int i = 0; i < getElementCount(); i++)
            {
                InstanceElement element = (InstanceElement)getElement(i);
                element.configPropertyName(property);
            }
        }
        //ɾ������
        public bool  removeProperty(PropertyElement property)
        {
            String s=property.getUsedInf();
            if (s!=null)
            {
                SmallDialog_ShowParagraph.showString(s, "����ɾ����ʹ�õ�����");
                return false;
            }
            for (int i = 0; i < getElementCount(); i++)
            {
                InstanceElement element = (InstanceElement)getElement(i);
                element.removeProperty(property);
            }
            return true;
        }
        #region SerializeAble Members
        public void readObjectInit(System.IO.Stream s)
        {
            short len = 0;
            len = IOUtil.readShort(s);
            for (int i = 0; i < len; i++)
            {
                InstanceElement element = new InstanceElement(this);
                addElement(element);
            }
        }
        public void ReadObject(System.IO.Stream s)
        {
            for (int i = 0; i < getElementCount(); i++)
            {
                InstanceElement element = (InstanceElement)this.getElement(i);
                element.ReadObject(s);
            }
        }
        public void writeObjectInit(System.IO.Stream s)
        {
            short len = (short)getElementCount();
            IOUtil.writeShort(s, len);
        }
        public void WriteObject(System.IO.Stream s)
        {
            short len = (short)getElementCount();
            for (int i = 0; i < len; i++)
            {
                InstanceElement element = (InstanceElement)getElement(i);
                element.WriteObject(s);
            }
        }
        public void exportObjectInit(System.IO.Stream fs_bin)
        {
            short len = (short)getElementCount();
            IOUtil.writeShort(fs_bin, len);
        }
        public void ExportObject(System.IO.Stream fs_bin)
        {
            short len = (short)getElementCount();
            for (int i = 0; i < len; i++)
            {
                InstanceElement element = (InstanceElement)getElement(i);
                element.ExportObject(fs_bin);
            }
        }
        #endregion
    }
    /****************************** �������Ե�Ԫ ******************************/
    public class InstanceElement : ObjectElement, SerializeAble
    {
        public PropertyValueManager propertyValueManager;
        public InstanceElement(InstancesManager parentT)
            : base(parentT)
        {
            propertyValueManager = new PropertyValueManager(this);
        }
        //�ж���ͬ
        public bool equalsInstanceElement(InstanceElement destInstanceElement)
        {
            if (destInstanceElement == null)
            {
                return false;
            }
            if (!destInstanceElement.name.Equals(name))
            {
                return false;
            }
            return true;
        }
        public override String getValueToLenString()
        {
            return name;
        }
        public void setName(String name)
        {
            this.name = name+"";
            ((InstancesManager)parent).refreshUI_Element(this.getID());
        }
        public override int getUsedTime()
        {
            int time = 0;
            PropertyTypesManager propertyTypesManager=(PropertyTypesManager)(((InstancesManager)parent).parent.parent);
            for (int i = 0; i < propertyTypesManager.getElementCount(); i++)
            {
                PropertyTypeElement propertyTypeElement = (PropertyTypeElement)propertyTypesManager.getElement(i);
                for (int j = 0; j < propertyTypeElement.instancesManager.getElementCount(); j++)
                {
                    InstanceElement instanceElement = (InstanceElement)propertyTypeElement.instancesManager.getElement(j);
                    for (int k = 0; k < instanceElement.propertyValueManager.getElementCount(); k++)
                    {
                        PropertyValueElement propertyValueElement = (PropertyValueElement)instanceElement.propertyValueManager.getElement(k);
                        if (propertyValueElement.ValueType == Consts.PARAM_PROP)
                        {
                            Object value=propertyValueElement.getValue();
                            if (value != null && value.Equals(this))
                            {
                                time++;
                            }
                        }
                    }
                }
            }
            return time;
        }
        public String getUsedTimeInf()
        {
            int time = 0;
            String inf = "";
            PropertyTypesManager propertyTypesManager = (PropertyTypesManager)(((InstancesManager)parent).parent.parent);
            for (int i = 0; i < propertyTypesManager.getElementCount(); i++)
            {
                PropertyTypeElement propertyTypeElement = (PropertyTypeElement)propertyTypesManager.getElement(i);
                for (int j = 0; j < propertyTypeElement.instancesManager.getElementCount(); j++)
                {
                    InstanceElement instanceElement = (InstanceElement)propertyTypeElement.instancesManager.getElement(j);
                    for (int k = 0; k < instanceElement.propertyValueManager.getElementCount(); k++)
                    {
                        PropertyValueElement propertyValueElement = (PropertyValueElement)instanceElement.propertyValueManager.getElement(k);
                        if (propertyValueElement.ValueType == Consts.PARAM_PROP)
                        {
                            Object value = propertyValueElement.getValue();
                            if (value != null && value.Equals(this))
                            {
                                inf += "�ڡ�" + propertyTypeElement.name + "��->��" + instanceElement.name + "��->��" + propertyValueElement.name + "����������\n";
                                time++;
                            }
                        }
                    }
                }
            }
            inf += "====���������ˡ�" + time + "����====\n";
            return inf;
        }
        //��������
        public void addProperty(PropertyElement property)
        {
            PropertyValueElement element = new PropertyValueElement(propertyValueManager);
            element.name = property.name + "";
            element.ValueType = property.ValueType;
            element.setValue(property.getDefaultValue());
            propertyValueManager.addElement(element);
            propertyValueManager.refreshUI();
        }
        //��������
        public void inseartProperty(PropertyElement property,int index)
        {
            PropertyValueElement element = new PropertyValueElement(propertyValueManager);
            element.name = property.name + "";
            element.ValueType = property.ValueType;
            element.setValue(property.getDefaultValue());
            propertyValueManager.insertElement(element, index);
            propertyValueManager.refreshUI();
        }
        //��������
        public void moveUpProperty(int index)
        {
            propertyValueManager.moveUpElement(index);
            propertyValueManager.refreshUI();
        }
        //��������
        public void moveDownProperty(int index)
        {
            propertyValueManager.moveDownElement(index);
            propertyValueManager.refreshUI();
        }
        //��������
        public void configProperty(PropertyElement property)
        {
            if (property == null)
            {
                return;
            }
            PropertyValueElement element = (PropertyValueElement)propertyValueManager.getElement(property.getID());
            element.name = property.name + "";
            element.ValueType = property.ValueType;
            element.setValue(property.getDefaultValue());
            propertyValueManager.refreshUI_Element(property.getID());
        }
        public void configPropertyName(PropertyElement property)
        {
            if (property == null)
            {
                return;
            }
            PropertyValueElement element = (PropertyValueElement)propertyValueManager.getElement(property.getID());
            element.name = property.name + "";
            propertyValueManager.refreshUI_Element(property.getID());
        }
        //ɾ������
        public void removeProperty(PropertyElement property)
        {
            propertyValueManager.removeElement(property.getID());
        }
        #region SerializeAble Members
        public void ReadObject(System.IO.Stream s)
        {
            name = IOUtil.readString(s);
            propertyValueManager.ReadObject(s);
        }

        public void WriteObject(System.IO.Stream s)
        {
            IOUtil.writeString(s, name);
            propertyValueManager.WriteObject(s);
        }

        public void ExportObject(System.IO.Stream fs_bin)
        {
            String nameT = name;
            if (nameT == null)
            {
                nameT = "";
            }
            IOUtil.writeString(fs_bin, nameT);
            propertyValueManager.ExportObject(fs_bin);
        }

        #endregion

        public override ObjectElement clone()
        {
            InstanceElement newInstance = new InstanceElement((InstancesManager)parent);
            baseCloneTo(newInstance);
            propertyValueManager.cloneTo(newInstance.propertyValueManager);
            return newInstance;
        }
    }
    /****************************** ��������ֵ������ ******************************/
    public class PropertyValueManager : ObjectVector, SerializeAble
    {
        public InstanceElement parentInstance = null;
        public PropertiesManager propertiesManager = null;
        public PropertyValueManager(InstanceElement parentT)
        {
            parentInstance = parentT;
            propertiesManager = ((InstancesManager)parentInstance.parent).parent.propertiesManager;
        }
        #region SerializeAble Members
        //ˢ������(ȫ����дĬ������)
        public void refreshProperty()
        {
            this.removeAll();
            for (int i = 0; i < propertiesManager.getElementCount(); i++)
            {
                PropertyValueElement element = new PropertyValueElement(this);
                PropertyElement property = (PropertyElement)(propertiesManager.getElement(i));
                element.name = property.name + "";
                element.ValueType = property.ValueType;
                element.setValue(property.getDefaultValue());
                addElement(element);
            }
        }
        //ȥ����������
        public void clearDependProperty()
        {
            for (int i = 0; i < propertiesManager.getElementCount(); i++)
            {
                PropertyValueElement element = (PropertyValueElement)getElement(i);
                if (Consts.PARAM_PROP == element.ValueType)
                {
                    element.setValue(getDefaultParams(element.ValueType));
                }
            }
        }
        //��ȡĬ�������������͵�Ĭ�ϲ���
        public Object getDefaultParams(byte type)
        {
            switch (type)
            {
                case Consts.PARAM_INT:
                    return 0;
                case Consts.PARAM_STR:
                    return "δ����";
                case Consts.PARAM_INT_VAR:
                case Consts.PARAM_STR_VAR:
                case Consts.PARAM_PROP:
                case Consts.PARAM_INT_ID:
                    return null;
            }
            return null;
        }
        public void ReadObject(System.IO.Stream s)
        {
            short len = 0;
            len = IOUtil.readShort(s);
            for (int i = 0; i < len; i++)
            {
                PropertyValueElement element = new PropertyValueElement(this);
                addElement(element);
            }
            for (int i = 0; i < len; i++)
            {
                PropertyValueElement element = (PropertyValueElement)this.getElement(i);
                element.ReadObject(s);
            }
        }

        public void WriteObject(System.IO.Stream s)
        {
            short len = (short)getElementCount();
            IOUtil.writeShort(s, len);
            for (int i = 0; i < len; i++)
            {
                PropertyValueElement element = (PropertyValueElement)getElement(i);
                element.WriteObject(s);
            }
        }

        public void ExportObject(System.IO.Stream fs_bin)
        {
            short len = (short)getElementCount();
            for (int i = 0; i < len; i++)
            {
                PropertyValueElement element = (PropertyValueElement)getElement(i);
                element.ExportObject(fs_bin);
            }
        }
        #endregion
    }
    /****************************** ��������ֵ��Ԫ ******************************/
    public class PropertyValueElement : ObjectElement, SerializeAble
    {
        public byte ValueType = Consts.PARAM_INT;//
        public PropertyValueElement(PropertyValueManager parentT)
            : base(parentT)
        {
        }

        public void setValueFromObj(Object valueT)
        {
            if (valueT == null)
            {
                return;
            }
            Object destValue = null;
            try
            {
                switch (ValueType)
                {
                    case Consts.PARAM_INT:
                        destValue = Convert.ToInt32(valueT);
                        break;
                    case Consts.PARAM_STR:
                        destValue = Convert.ToString(valueT);
                        break;
                    case Consts.PARAM_INT_VAR:
                    case Consts.PARAM_STR_VAR:
                        destValue = null;
                        break;
                    case Consts.PARAM_PROP:
                        PropertyValueManager pvm = (PropertyValueManager)parent;
                        do 
                        {
                            if (pvm == null)
                            {
                                break;
                            }
                            int id = this.getID();
                            PropertyElement pElement = (PropertyElement)pvm.propertiesManager.getElement(id);
                            if(pElement==null||pElement.propertyTypeElementUsed==null||pElement.propertyTypeElementUsed.instancesManager==null)
                            {
                                break;
                            }
                            if(pElement.ValueType!=Consts.PARAM_PROP)
                            {
                                break;
                            }
                            InstancesManager im = pElement.propertyTypeElementUsed.instancesManager;
                            String name = null;
                            try
                            {
                                name = Convert.ToString(valueT);
                            }
                            catch (System.Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            if (name == null)
                            {
                                break;
                            }
                            for (int i = 0; i < im.getElementCount(); i++)
                            {
                                InstanceElement instance = (InstanceElement)im.getElement(i);
                                if (instance.name.Equals(name))
                                {
                                    destValue = i;
                                    break;
                                }
                            }
                        } while (false);
                        break;
                    case Consts.PARAM_INT_ID:
                        int intID = Convert.ToInt32(valueT);
                        destValue = ((PropertiesManager)parent).form_main.iDsManager.getElement(intID);
                        break;
                }
                if (destValue != null)
                {
                    value = destValue;
                }
            }
            catch (Exception e)
            {
            }
        }
        public override String getValueToLenString()
        {
            String text = null;
            if (value != null)
            {
                if (value is ObjectElement)
                {
                    text = ((ObjectElement)value).name;
                }
                else
                {
                    text = value.ToString();
                }
            }
            else
            {
                text = "";
            }
            //Console.WriteLine("[" + name + "] == " + text);
            return "[" + name + "] == " + text;
        }
        public override int getUsedTime()
        {
            //....go on
            return 0;
        }
        //��ö�Ӧ������
        public PropertyElement getMatchPropertyElement()
        {
            int id = this.getID();
            return (PropertyElement)((((PropertyValueManager)parent).propertiesManager.getElement(id)));
        }
        #region SerializeAble Members
        public void ReadObject(System.IO.Stream s)
        {
            name = IOUtil.readString(s);
            ValueType = IOUtil.readByte(s);
            if (ValueType == Consts.PARAM_INT)
            {
                this.value = IOUtil.readInt(s);
            }
            else if (ValueType == Consts.PARAM_STR)
            {
                this.value = IOUtil.readString(s);
            }
            else if (ValueType == Consts.PARAM_PROP)
            {
                int id = IOUtil.readInt(s);
                PropertyElement property = this.getMatchPropertyElement();
                this.value = property.propertyTypeElementUsed.instancesManager.getElement(id);
            }
            else if (ValueType == Consts.PARAM_INT_ID)
            {
                int id = IOUtil.readInt(s);
                this.value = ((PropertyTypesManager)(((InstancesManager)(((PropertyValueManager)
                    parent).parentInstance.parent)).parent.parent)).form_main.iDsManager.getElement(id);
            }
        }

        public void WriteObject(System.IO.Stream s)
        {
            IOUtil.writeString(s, name);
            IOUtil.writeByte(s, ValueType);
            if (ValueType == Consts.PARAM_INT)
            {
                IOUtil.writeInt(s, Convert.ToInt32(value));
            }
            else if (ValueType == Consts.PARAM_STR)
            {
                IOUtil.writeString(s, (String)value);
            }
            else if (ValueType == Consts.PARAM_PROP)
            {
                int id = -1;
                if (value != null)
                {
                    id=((InstanceElement)value).getID();
                }
                IOUtil.writeInt(s, id);
            }
            else if (ValueType == Consts.PARAM_INT_ID)
            {
                int id = -1;
                if (value != null)
                {
                    id = ((VarElement)value).getID();
                }
                IOUtil.writeInt(s, id);
            }
        }

        public void ExportObject(System.IO.Stream fs_bin)
        {
            if (ValueType == Consts.PARAM_INT)
            {
                IOUtil.writeInt(fs_bin, Convert.ToInt32(value));
            }
            else if (ValueType == Consts.PARAM_STR)
            {
                IOUtil.writeString(fs_bin, (String)value);
            }
            else if (ValueType == Consts.PARAM_PROP)
            {
                int id = -1;
                if (value != null)
                {
                    id = ((InstanceElement)value).getID();
                }
                IOUtil.writeInt(fs_bin, id);
            }
            else if (ValueType == Consts.PARAM_INT_ID)
            {
                int id = -1;
                if (value != null)
                {
                    id = Convert.ToInt32(((VarElement)value).getValue());
                }
                IOUtil.writeInt(fs_bin, id);
            }
        }

        #endregion

        public override ObjectElement clone()
        {
            PropertyValueElement newInstance = new PropertyValueElement((PropertyValueManager)parent);
            baseCloneTo(newInstance);
            newInstance.ValueType = ValueType;
            if (ValueType == Consts.PARAM_INT)
            {
                newInstance.value = Convert.ToInt32(value) + 0;
            }
            else if (ValueType == Consts.PARAM_STR)
            {
                newInstance.value = Convert.ToString(value)+ "";
            }
            else
            {
                newInstance.value = value;
            }
            return newInstance;
        }
    }
}
