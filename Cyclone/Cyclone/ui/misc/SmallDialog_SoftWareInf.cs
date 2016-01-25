using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Cyclone.alg.util;

namespace Cyclone.mod.misc
{
    public partial class SmallDialog_SoftWareInf : Form
    {
        public SmallDialog_SoftWareInf()
        {
            InitializeComponent();
        }

        private void SmallDialog_SoftWareInf_Load(object sender, EventArgs e)
        {
            String welcome = "��ӭʹ��Cyclone2D�����ܻ��棩��Ϸ������������������ڣ�2013��3��14��\n";
            String version = "��ǰ�汾: "
                + ((Consts.softWareVersion % 100000) / 10000) + "."
                + ((Consts.softWareVersion % 10000) / 1000) + "."
                + ((Consts.softWareVersion % 1000) / 100) + "."
                + ((Consts.softWareVersion % 100))+" ";
            String copyRight = "��Ȩ���У�Andrew Fan\n";
            String endDate = "�����ʹ�����ڽ�ֹ��" + Lisence.timeToYear + "��" + Lisence.timeToMonth + "��" + Lisence.timeToDay + "��\n";
            String url = "����ʹٷ���վhttp://www.cyclone2d.cn�Ի�ø�����Ϣ";
            textBoxDescription.Text = welcome + version + copyRight + endDate + url;

        }
    }
}