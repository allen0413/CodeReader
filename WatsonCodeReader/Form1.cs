using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;

namespace WatsonCodeReader
{
    public partial class Form1 : Form
    {
        MVS mvs;
        int radius = 3;
        int imgCount = 0;
        Bitmap bitmap;
        Graphics gra;
        public bool updating = false;
        public bool m_bGrabbing = false;
        public int tripleCollectIdx = 0;

        public Form1()
        {
            InitializeComponent();
            mvs = new MVS(this, pictureBox1.Width, pictureBox1.Height);
            mvs.DeviceListAcq();
            Control.CheckForIllegalCrossThreadCalls = false;

            pictureBox1.Show();
            pictureBox2.Show();
            pictureBox3.Show();

            mvs.eventFinished += new MVS.DelegateFinish(UpdateImg);
            buttonFindDevice_Click(null, null);
        }

        private void buttonFindDevice_Click(object sender, EventArgs e)
        {
            System.GC.Collect();
            comboBoxDeviceList.Items.Clear();
            List<string> DeviceList = mvs.DeviceListAcq();
            foreach (string device in DeviceList)
            {
                comboBoxDeviceList.Items.Add(device);
            }
            // ch:选择第一项 | en:Select the first item
            if (comboBoxDeviceList.Items.Count > 0)
            {
                comboBoxDeviceList.SelectedIndex = 0;
            }
        }

        // ch:显示错误信息 | en:Show error message
        public void ShowErrorMsg(string csMessage, int nErrorNum)
        {
            string errorMsg = "";
            if (nErrorNum == 0)
            {
                errorMsg = csMessage;
            }
            else
            {
                errorMsg = csMessage + ": Error =" + String.Format("{0:X}", nErrorNum);
            }

            errorMsg += mvs.ErrorMsgDecode(nErrorNum);
            MessageBox.Show(errorMsg, "PROMPT");
        }

        private void buttonOpenDevice_Click(object sender, EventArgs e)
        {
            if (comboBoxDeviceList.Items.Count == 0 || comboBoxDeviceList.SelectedIndex == -1)
            {
                ShowErrorMsg("No stDevInfo, please select", 0);
                return;
            }
            mvs.OpenDevice(comboBoxDeviceList.SelectedIndex);
            buttonGetSetting_Click(null, null);// ch:获取参数 | en:Get parameters
            // ch:控件操作 | en:Control operation
            SetCtrlWhenOpen();
        }

        private void SetCtrlWhenOpen()
        {
            buttonOpenDevice.Enabled = false;
            buttonCloseDevice.Enabled = true;
            buttonStartGrab.Enabled = true;
            buttonEndGrab.Enabled = false;
            tbExposure.Enabled = true;
            tbGain.Enabled = true;
            tbFrameRate.Enabled = true;
            buttonGetSetting.Enabled = true;
            buttonApplySetting.Enabled = true;
        }

        private void buttonCloseDevice_Click(object sender, EventArgs e)
        {
            // ch:取流标志位清零 | en:Reset flow flag bit
            if (m_bGrabbing == true)
            {
                m_bGrabbing = false;
                mvs.StopGrab();
            }

            mvs.CloseDevice();

            // ch:控件操作 | en:Control Operation
            SetCtrlWhenClose();
        }

        private void SetCtrlWhenClose()
        {
            buttonOpenDevice.Enabled = true;
            buttonCloseDevice.Enabled = false;
            buttonStartGrab.Enabled = false;
            buttonEndGrab.Enabled = false;
            tbExposure.Enabled = false;
            tbGain.Enabled = false;
            tbFrameRate.Enabled = false;
            buttonGetSetting.Enabled = false;
            buttonApplySetting.Enabled = false;
            tbExposure.Clear();
            tbGain.Clear();
            tbFrameRate.Clear();
        }

        private void buttonStartGrab_Click(object sender, EventArgs e)
        {
            // 清空读码信息
            while (dataGridView.Rows.Count != 0)
            {
                dataGridView.Rows.RemoveAt(0);
            }
            checkedListBox.Items.Clear();
            checkedListBox.Items.Add("   辨識位置 一");
            checkedListBox.Items.Add("   辨識位置 二");
            checkedListBox.Items.Add("   辨識位置 三");

            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;

            // ch:标志位置位true | en:Set position bit true
            m_bGrabbing = true;
            int ret = mvs.StartGrab();
            if (ret != 0)
            {
                m_bGrabbing = false;
                //m_hReceiveThread.Join();
                ShowErrorMsg("Start Grabbing Fail!", ret);
                return;
            }

            // ch:控件操作 | en:Control Operation
            SetCtrlWhenStartGrab();
            updating = false;
            imgCount = 0;
        }

        private void SetCtrlWhenStartGrab()
        {
            buttonStartGrab.Enabled = false;
            buttonEndGrab.Enabled = true;
        }

        private void buttonEndGrab_Click(object sender, EventArgs e)
        {
            // ch:标志位设为false | en:Set flag bit false
            m_bGrabbing = false;
            int ret = mvs.StopGrab();
            // ch:停止采集 | en:Stop Grabbing
            if (ret != 0)
            {
                ShowErrorMsg("Stop Grabbing Fail!", ret);
            }

            // ch:控件操作 | en:Control Operation
            SetCtrlWhenStopGrab();
            tripleCollectIdx = 0;
        }

        private void SetCtrlWhenStopGrab()
        {
            buttonStartGrab.Enabled = true;
            buttonEndGrab.Enabled = false;
        }

        private void buttonGetSetting_Click(object sender, EventArgs e)
        {
            Dictionary<string, float> para = mvs.GetParameters();
            if (para.Count == 3)
            {
                tbExposure.Text = para["ExposureTime"].ToString("F1");
                tbGain.Text = para["Gain"].ToString("F1");
                tbFrameRate.Text = para["AcquisitionFrameRate"].ToString("F1");
            }
        }
        private void buttonApplySetting_Click(object sender, EventArgs e)
        {
            try
            {
                if (mvs.SetParameters(float.Parse(tbExposure.Text), float.Parse(tbGain.Text), float.Parse(tbFrameRate.Text)))
                    MessageBox.Show("Set Param Succeed!");
            }
            catch
            {
                ShowErrorMsg("Please enter correct type!", 0);
                return;
            }
        }

        private void UpdateImg(int idx)
        {
            List<BarData> barList = mvs.barDataList;
            //Graphics gra = this.CreateGraphics();

            if (imgCount == 0)
            {
                bitmap = new Bitmap(mvs.collectedImg);
                gra = Graphics.FromImage(bitmap);
            }

            foreach (BarData bar in barList)
            {
                if (DecideSameBar(bar))
                {
                    DrawCenterPoint(gra, bar);
                    DrawContour(gra, bar);
                    dataGridView.Rows.Insert(0, WriteRow(bar));
                }
            }
            barList.Clear();

            if (imgCount < 10)
            {
                imgCount += 1;
            }
            else
            {
                switch (idx)
                {
                    case 0:
                        pictureBox1.Image = AdjustBrightness(bitmap, 2);
                        pictureBox1.Refresh();
                        break;
                    case 1:
                        pictureBox2.Image = AdjustBrightness(bitmap, 2);
                        pictureBox2.Refresh();
                        break;
                    case 2:
                        pictureBox3.Image = AdjustBrightness(bitmap, 2);
                        pictureBox3.Refresh();
                        break;
                }
                
                if (tripleCollectIdx < 2)
                {
                    tripleCollectIdx += 1;
                    imgCount = 0;
                }
                else
                {
                    m_bGrabbing = false;
                    buttonEndGrab_Click(null, null);
                }
            }
            updating = false;
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            buttonCloseDevice_Click(sender, e);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            buttonCloseDevice_Click(sender, e);
        }

        public bool DecideSameBar(BarData bar)
        {
            if (dataGridView.Rows.Count > 1)
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    //double distX = Math.Pow(Convert.ToDouble(bar.centerPoint[0]) - Convert.ToDouble(row.Cells[5].Value), 2);
                    //double distY = Math.Pow(Convert.ToDouble(bar.centerPoint[1]) - Convert.ToDouble(row.Cells[6].Value), 2);
                    //double dist = Math.Pow(distX + distY, 0.5);
                    //if (dist < 60)
                    //    return false;
                    if (bar.barCode.ToString() == row.Cells[4].Value.ToString())
                        return false;
                }
            }
            return true;
        }

        private Graphics DrawImg(PictureBox picBox, Image img)
        {
            picBox.Image = AdjustBrightness(img, 2);
            //picBox.Image = img;
            picBox.Refresh();
            return picBox.CreateGraphics();
        }

        private void DrawCenterPoint(Graphics gra, BarData bar)
        {
            Pen pen = new Pen(Color.Blue, 3);
            gra.DrawEllipse(pen, bar.centerPoint[0] - radius, bar.centerPoint[1] - radius, radius * 2, radius * 2);
        }

        private void DrawContour(Graphics gra, BarData bar)
        {
            Pen pen = new Pen(Color.Blue, 3);
            gra.DrawPolygon(pen, bar.pointList);
        }

        private Bitmap AdjustBrightness(Image image, float brightness)
        {
            // Make the ColorMatrix.
            float b = brightness;
            ColorMatrix cm = new ColorMatrix(new float[][]
                {
                    new float[] {b, 0, 0, 0, 0},
                    new float[] {0, b, 0, 0, 0},
                    new float[] {0, 0, b, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1},
                });
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(cm);

            // Draw the image onto the new bitmap while applying the new ColorMatrix.
            Point[] points =
            {
                new Point(0, 0),
                new Point(image.Width, 0),
                new Point(0, image.Height),
            };
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

            // Make the result bitmap.
            Bitmap bm = new Bitmap(image.Width, image.Height);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.DrawImage(image, points, rect, GraphicsUnit.Pixel, attributes);
            }

            // Return the result.
            return bm;
        }

        private DataGridViewRow WriteRow(BarData bar)
        {
            DataGridViewRow cDataRow = new DataGridViewRow();
            cDataRow.CreateCells(dataGridView);
            cDataRow.Cells[0].Value = this.dataGridView.Rows.Count;
            DateTime cDateTime = DateTime.Now;
            cDataRow.Cells[1].Value = cDateTime.ToString();
            cDataRow.Cells[2].Value = bar.timeCost.ToString();
            cDataRow.Cells[3].Value = bar.barType;
            cDataRow.Cells[4].Value = bar.barCode;
            cDataRow.Cells[5].Value = bar.centerPoint[0];//PointXSum / 4;
            cDataRow.Cells[6].Value = bar.centerPoint[1]; //PointYSum / 4;
            cDataRow.Cells[7].Value = bar.score; //stBcrResultEx2.stBcrInfoEx2[i].nIDRScore.ToString();
            return cDataRow;
        }
    }
}
