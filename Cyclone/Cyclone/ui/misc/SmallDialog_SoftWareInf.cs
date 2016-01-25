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
            String welcome = "欢迎使用Cyclone2D（智能机版）游戏设计软件。软件发行日期：2013年3月14日\n";
            String version = "当前版本: "
                + ((Consts.softWareVersion % 100000) / 10000) + "."
                + ((Consts.softWareVersion % 10000) / 1000) + "."
                + ((Consts.softWareVersion % 1000) / 100) + "."
                + ((Consts.softWareVersion % 100))+" ";
            String copyRight = "版权所有：Andrew Fan\n";
            String endDate = "本软件使用日期截止：" + Lisence.timeToYear + "年" + Lisence.timeToMonth + "月" + Lisence.timeToDay + "日\n";
            String url = "请访问官方网站http://www.cyclone2d.cn以获得更多信息";
            textBoxDescription.Text = welcome + version + copyRight + endDate + url;

        }
    }
}