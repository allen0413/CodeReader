using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;

namespace WatsonCodeReader
{
    public partial class Form1 : Form
    {
        int numberOfFocus = 8;
        float bright = 1.5f;
        public int waitTime = 5000;
        int radius = 3;

        Bitmap bitmap;
        Graphics gra;
        List<Image> imgList = new List<Image>();
        int imgCount = 0;
        bool continueCapture = false;
        MVS mvs;
        public bool updating = false;
        public bool m_bGrabbing = false;
        public int collectIdx = 0;

        public Form1()
        {
            InitializeComponent();
            mvs = new MVS(this, pictureBox1.Width, pictureBox1.Height);
            mvs.DeviceListAcq();
            Control.CheckForIllegalCrossThreadCalls = false;
            mvs.eventFinished += new MVS.DelegateFinish(UpdateImg);
            buttonFindDevice_Click(null, null);
            dataGridView.ScrollBars = ScrollBars.None;
            dataGridView.MouseWheel += new MouseEventHandler(mousewheel);
            buttonSeeImgList.Enabled = false;
            pictureBox1.Show();
        }

        private void mousewheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0 && dataGridView.FirstDisplayedScrollingRowIndex > 0)
            {
                dataGridView.FirstDisplayedScrollingRowIndex--;
            }
            else if (e.Delta < 0)
            {
                dataGridView.FirstDisplayedScrollingRowIndex++;
            }
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
            for (int i = 0; i < numberOfFocus; i++)
                checkedListBox.Items.Add(string.Format("   辨識位置 {0}", i + 1));

            pictureBox1.Image = null;

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
            imgList.Clear();
            buttonSeeImgList.Enabled = false;
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
            continueCapture = false;
            int ret = mvs.StopGrab();
            // ch:停止采集 | en:Stop Grabbing
            if (ret != 0)
            {
                ShowErrorMsg("Stop Grabbing Fail!", ret);
            }

            // ch:控件操作 | en:Control Operation
            SetCtrlWhenStopGrab();
            collectIdx = 0;
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
            mvs.GetFocus();
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
            if (continueCapture)
            {
                bitmap = new Bitmap(mvs.collectedImg);
                gra = Graphics.FromImage(bitmap);
                foreach (BarData bar in barList)
                {
                    DrawCenterPoint(gra, bar);
                    DrawContour(gra, bar);
                    if (DecideSameBar(bar))
                    {
                        dataGridView.Rows.Insert(0, WriteRow(bar));
                    }
                }
                barList.Clear();
                pictureBox1.Image = AdjustBrightness(bitmap, bright);
                pictureBox1.Refresh();
                updating = false;
            }
            else
            {
                if (imgCount == 0 && collectIdx == 0)
                {
                    bitmap = new Bitmap(mvs.collectedImg);
                    gra = Graphics.FromImage(bitmap);
                }

                if (imgCount == 0)
                {
                    imgList.Add(AdjustBrightness(mvs.collectedImg, bright));
                }

                foreach (BarData bar in barList)
                {
                    if (DecideSameBar(bar))
                    {
                        DrawCenterPoint(gra, bar);
                        DrawContour(gra, bar);
                        dataGridView.Rows.Insert(0, WriteRow(bar));
                        DrawWord(gra, bar, dataGridView.Rows.Count);
                        Application.DoEvents();
                    }
                }
                barList.Clear();

                if (imgCount < 10)
                {
                    imgCount += 1;
                }
                else
                {
                    pictureBox1.Image = AdjustBrightness(bitmap, bright);
                    pictureBox1.Refresh();
                    checkedListBox.SetItemChecked(collectIdx, true);
                    if (collectIdx < numberOfFocus - 1)
                    {
                        collectIdx += 1;
                        imgCount = 0;
                    }
                    else
                    {
                        m_bGrabbing = false;
                        buttonSeeImgList.Enabled = true;
                        buttonEndGrab_Click(null, null);
                    }
                }
                updating = false;
            }
        }


        public bool DecideSameBar(BarData bar)
        {
            if (dataGridView.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    double distX = Math.Pow(Convert.ToDouble(bar.centerPoint[0]) - Convert.ToDouble(row.Cells[5].Value), 2);
                    double distY = Math.Pow(Convert.ToDouble(bar.centerPoint[1]) - Convert.ToDouble(row.Cells[6].Value), 2);
                    double dist = Math.Pow(distX + distY, 0.5);
                    if (dist < 50)
                        return false;
                    if (bar.barCode.ToString() == row.Cells[3].Value.ToString())
                        return false;
                }
            }
            return true;
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

        private void DrawWord(Graphics gra, BarData bar, int idx)
        {
            Pen pen = new Pen(Color.Blue, 3);
            var fontFamily = new FontFamily("Javanese Text");
            var font = new Font(fontFamily, 100, FontStyle.Regular, GraphicsUnit.Pixel);
            var solidBrush = new SolidBrush(Color.FromArgb(255, 0, 0, 255));

            gra.TextRenderingHint = TextRenderingHint.AntiAlias;
            gra.DrawString(idx.ToString(), font, solidBrush, new PointF(bar.centerPoint[0] + 20, bar.centerPoint[1]));
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
            cDataRow.Cells[0].Value = this.dataGridView.Rows.Count + 1;
            cDataRow.Cells[1].Value = DateTime.Now.ToString("MM'-'dd' 'HH':'mm':'ss");
            cDataRow.Cells[2].Value = bar.barType;
            cDataRow.Cells[3].Value = bar.barCode;
            cDataRow.Cells[4].Value = bar.centerPoint[0];
            cDataRow.Cells[5].Value = bar.centerPoint[1];
            cDataRow.Cells[6].Value = bar.score;
            return cDataRow;
        }

        private void buttonSeeImgList_Click(object sender, EventArgs e)
        {
            if (imgList.Count > 0 && !m_bGrabbing)
            {
                ImgListWindow imgListWindow = new ImgListWindow(imgList);
                imgListWindow.Show();
            }
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            // 清空读码信息
            while (dataGridView.Rows.Count != 0)
            {
                dataGridView.Rows.RemoveAt(0);
            }
            checkedListBox.Items.Clear();
            pictureBox1.Image = null;

            // ch:标志位置位true | en:Set position bit true
            m_bGrabbing = true;
            updating = false;
            continueCapture = true;
            collectIdx = 2;
            int ret = mvs.StartGrab();
            if (ret != 0)
            {
                m_bGrabbing = false;
                ShowErrorMsg("Start Grabbing Fail!", ret);
                return;
            }

            // ch:控件操作 | en:Control Operation
            SetCtrlWhenStartGrab();
            buttonSeeImgList.Enabled = false;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            buttonCloseDevice_Click(sender, e);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            buttonCloseDevice_Click(sender, e);
        }
    }
}
