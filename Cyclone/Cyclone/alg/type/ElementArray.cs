using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Cyclone.mod;

namespace Cyclone.alg.type
{
    public interface ElementArray
    {
        bool addElement(Object element);
        bool insertElement(Object element, int index);
        Object getElement(int index);
        int getElementID(Object element);
        int getElementCount();
        bool removeElement(int index);
    }
}
