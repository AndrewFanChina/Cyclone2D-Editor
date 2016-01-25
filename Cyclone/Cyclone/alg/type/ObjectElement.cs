using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Cyclone.alg.type
{
    public abstract class ObjectElement:Element
    {
        public ObjectVector parent = null;
        public String name = "Î´ÃüÃû";
        protected Object value=null;
        public ObjectElement()
        {
 
        }
        public ObjectElement(ObjectVector parentT)
        {
            parent = parentT;
        }
        public void baseCloneTo(ObjectElement newInstance)
        {
            newInstance.parent = parent;
            newInstance.name = name + "";
        }
        #region Element Members
        public int getID()
        {
            return parent.getElementID(this);
        }
        public void setValue(object obj)
        {
            value = obj;
        }
        public Object getValue()
        {
            return value;
        }
        public abstract String getValueToLenString();
        public abstract int getUsedTime();
        public abstract ObjectElement clone();

        #endregion
    }
}
