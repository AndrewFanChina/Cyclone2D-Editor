using System;
using System.Collections.Generic;
using System.Text;
using Cyclone.alg;
using System.IO;
using System.Collections;
using Cyclone.alg.type;
using Cyclone.alg.util;

namespace Cyclone.mod.script
{
    class ScriptFileElement : ObjectElement, SerializeAble
    {
        public ScriptFileElement(ObjectVector parentT)
        {
            parent = parentT;
        }
        public override string getValueToLenString()
        {
            return (String)value;
        }
        public override int getUsedTime()
        {
            return 0;
        }
        public override ObjectElement clone()
        {
            ScriptFileElement newInstance = new ScriptFileElement(parent);
            if (value != null)
            {
                newInstance.value = ((String)value) + "";
            }
            return newInstance;
        }
        public ObjectElement cloneForExceport(ObjectVector parent)
        {
            ScriptFileElement newInstance = new ScriptFileElement(parent);
            if (value != null)
            {
                newInstance.value = ((String)value) + "";
            }
            return newInstance;
        }
        #region SerializeAble 成员

        public void ReadObject(Stream s)
        {
            value = IOUtil.readString(s);
        }

        public void WriteObject(Stream s)
        {
            IOUtil.writeString(s, (String)value);
        }

        public void ExportObject(Stream fs_bin)
        {
            //..go on
        }

        #endregion
    }
}
