using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WatsonCodeReader
{
    public partial class ImgListWindow : Form
    {
        int idx = 0;
        List<Image> imgList;
        public ImgListWindow(List<Image> imgListInput)
        {
            InitializeComponent();
            imgList = imgListInput;
            pictureBox1.Image = imgList[idx];
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            ShowIdx();
        }

        private void buttonPevious_Click(object sender, EventArgs e)
        {
            if (idx > 0)
            {
                idx -= 1;
                pictureBox1.Image = imgList[idx];
                pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            }
            ShowIdx();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (idx < imgList.Count - 1)
            {
                idx += 1;
                pictureBox1.Image = imgList[idx];
                pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            }
            ShowIdx();
        }

        private void ShowIdx()
        {
            label1.Text = string.Format("{0} / {1}", idx + 1, imgList.Count);
        }
    }
}
