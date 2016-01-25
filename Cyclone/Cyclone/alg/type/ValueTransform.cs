using System;
using System.Collections.Generic;
using System.Text;
using Cyclone;

namespace Cyclone.alg.type
{
    public struct ValueTransform
    {
        public float rotateDegree;
        public float alpha;
        public ValueFloat2 scale;
        public ValueFloat2 anchor;
        public ValueFloat2 pos;

        public void setValue(mod.anim.MFrameUnit unit)
        {
            rotateDegree = unit.rotateDegree;
            alpha = unit.alpha;
            scale.X = unit.scaleX;
            scale.Y = unit.scaleY;
            anchor.X = unit.anchorX;
            anchor.Y = unit.anchorY;
            pos.X = unit.posX;
            pos.Y = unit.posY;
        }
    }
}
