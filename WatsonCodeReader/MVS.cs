using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using MvCodeReaderSDKNet;
using System.Runtime.InteropServices;

namespace WatsonCodeReader
{
    class MVS
    {
        Form1 form;
        MvCodeReader.MV_CODEREADER_DEVICE_INFO_LIST m_stDeviceList = new MvCodeReader.MV_CODEREADER_DEVICE_INFO_LIST();
        MvCodeReader m_cMyDevice = new MvCodeReader();
        MvCodeReader.MV_CODEREADER_ENUMVALUE stParamEnum = new MvCodeReader.MV_CODEREADER_ENUMVALUE();
        Thread m_hReceiveThread = null;
        // ch:用于从驱动获取图像的缓存 | en:Buffer for getting image from driver
        byte[] m_BufForDriver = new byte[1024 * 1024 * 20];
        // 显示
        Point[] stPointList = new Point[4];                 // 条码位置的4个点坐标
        List<int> pictureBoxSize = new List<int>(2);
        int previousIdx = -1;
        public delegate void DelegateFinish(int idx);
        public event DelegateFinish eventFinished;
        public Image collectedImg;
        public List<BarData> barDataList = new List<BarData>();

        public MVS(Form1 formInput, int picSizeX, int picSizeY)
        {
            form = formInput;
            pictureBoxSize.Add(picSizeX);
            pictureBoxSize.Add(picSizeY);
        }

        public List<string> DeviceListAcq()
        {
            List<string> DeviceList = new List<string>();
            // ch:创建设备列表 | en:Create Device List
            m_stDeviceList.nDeviceNum = 0;
            int nRet = MvCodeReader.MV_CODEREADER_EnumDevices_NET(ref m_stDeviceList, MvCodeReader.MV_CODEREADER_GIGE_DEVICE);
            if (0 != nRet)
            {
                form.ShowErrorMsg("Enumerate devices fail!", nRet);
                return DeviceList;
            }

            if (0 == m_stDeviceList.nDeviceNum)
            {
                form.ShowErrorMsg("None Device!", 0);
                return DeviceList;
            }
            string strUserDefinedName = "";
            // ch:在窗体列表中显示设备名 | en:Display stDevInfo name in the form list
            for (int i = 0; i < m_stDeviceList.nDeviceNum; i++)
            {
                MvCodeReader.MV_CODEREADER_DEVICE_INFO stDevInfo = (MvCodeReader.MV_CODEREADER_DEVICE_INFO)Marshal.PtrToStructure(m_stDeviceList.pDeviceInfo[i], typeof(MvCodeReader.MV_CODEREADER_DEVICE_INFO));
                if (stDevInfo.nTLayerType == MvCodeReader.MV_CODEREADER_GIGE_DEVICE)
                {
                    IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(stDevInfo.SpecialInfo.stGigEInfo, 0);
                    MvCodeReader.MV_CODEREADER_GIGE_DEVICE_INFO stGigEDeviceInfo = (MvCodeReader.MV_CODEREADER_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MvCodeReader.MV_CODEREADER_GIGE_DEVICE_INFO));
                    DeviceList.Add("GEV: " + stGigEDeviceInfo.chManufacturerName + " " + stGigEDeviceInfo.chModelName + " (" + stGigEDeviceInfo.chSerialNumber + ")");
                }
            }
            return DeviceList;
        }

        public void OpenDevice(int index)
        {
            // ch:获取选择的设备信息 | en:Get selected stDevInfo information
            MvCodeReader.MV_CODEREADER_DEVICE_INFO stDevInfo = (MvCodeReader.MV_CODEREADER_DEVICE_INFO)Marshal.
                PtrToStructure(m_stDeviceList.pDeviceInfo[index], typeof(MvCodeReader.MV_CODEREADER_DEVICE_INFO));

            // ch:打开设备 | en:Open stDevInfo
            if (null == m_cMyDevice)
            {
                m_cMyDevice = new MvCodeReader();
            }

            int nRet = m_cMyDevice.MV_CODEREADER_CreateHandle_NET(ref stDevInfo);
            if (MvCodeReader.MV_CODEREADER_OK != nRet)
            {
                form.ShowErrorMsg("MV_CODEREADER_CreateHandle_NET fail!", nRet);
                return;
            }
            nRet = m_cMyDevice.MV_CODEREADER_OpenDevice_NET();
            if (MvCodeReader.MV_CODEREADER_OK != nRet)
            {
                m_cMyDevice.MV_CODEREADER_DestroyHandle_NET();
                form.ShowErrorMsg("Device open fail!", nRet);
                return;
            }

            // ch:设置采集连续模式 | en:Set Continues Aquisition Mode
            m_cMyDevice.MV_CODEREADER_SetEnumValue_NET("TriggerMode", (uint)MvCodeReader.MV_CODEREADER_TRIGGER_MODE.MV_CODEREADER_TRIGGER_MODE_OFF);

        }

        public void CloseDevice()
        {
            if (m_hReceiveThread != null)
                m_hReceiveThread.Join();
            // ch:关闭设备 | en:Close Device
            m_cMyDevice.MV_CODEREADER_CloseDevice_NET();
            m_cMyDevice.MV_CODEREADER_DestroyHandle_NET();
        }

        public int StartGrab()
        {
            // ch:开始采集 | en:Start Grabbing
            m_hReceiveThread = new Thread(ReceiveThreadProcess);
            m_hReceiveThread.Start();
            return m_cMyDevice.MV_CODEREADER_StartGrabbing_NET();
        }

        public int StopGrab()
        {
            // ch:停止采集 | en:Stop Grabbing
            m_cMyDevice.MV_CODEREADER_StopGrabbing_NET();
            /*
            if (null != m_hReceiveThread)
            {
                m_hReceiveThread.Join();
            }*/
            return 0;
        }

        private List<int> SetFocus(int focusIndex)
        {
            List<int> results = new List<int>();
            int SetFocusReturn1 = m_cMyDevice.MV_CODEREADER_SetEnumValue_NET("FocusPosition", (uint)(focusIndex + 1));
            Thread.Sleep(100);
            int SetFocusReturn2 = m_cMyDevice.MV_CODEREADER_SetCommandValue_NET("FocusPositionLoad");
            results.Add(SetFocusReturn1);
            results.Add(SetFocusReturn2);
            bool test;
            if (results[0] != 0 || results[1] != 0)
                test = true;
            return results;
        }

        public void GetFocus()
        {
            MvCodeReader.MV_CODEREADER_INTVALUE_EX intParam = new MvCodeReader.MV_CODEREADER_INTVALUE_EX();
            MvCodeReader.MV_CODEREADER_ENUMVALUE enumParam = new MvCodeReader.MV_CODEREADER_ENUMVALUE();
            int ret = m_cMyDevice.MV_CODEREADER_GetEnumValue_NET("FocusPosition", ref enumParam);
            ret = m_cMyDevice.MV_CODEREADER_GetIntValue_NET("Focus", ref intParam);
        }

        public Dictionary<string, float> GetParameters()
        {
            Dictionary<string, float> para = new Dictionary<string, float>();
            MvCodeReader.MV_CODEREADER_FLOATVALUE stParam = new MvCodeReader.MV_CODEREADER_FLOATVALUE();
            int nRet = m_cMyDevice.MV_CODEREADER_GetFloatValue_NET("ExposureTime", ref stParam);
            if (MvCodeReader.MV_CODEREADER_OK == nRet)
            {
                para.Add("ExposureTime", stParam.fCurValue);
            }
            else
            {
                form.ShowErrorMsg("Get ExposureTime Fail!", nRet);
            }

            nRet = m_cMyDevice.MV_CODEREADER_GetFloatValue_NET("Gain", ref stParam);
            if (MvCodeReader.MV_CODEREADER_OK == nRet)
            {
                para.Add("Gain", stParam.fCurValue);
            }
            else
            {
                form.ShowErrorMsg("Get Gain Fail!", nRet);
            }

            nRet = m_cMyDevice.MV_CODEREADER_GetFloatValue_NET("AcquisitionFrameRate", ref stParam);
            if (MvCodeReader.MV_CODEREADER_OK == nRet)
            {
                para.Add("AcquisitionFrameRate", stParam.fCurValue);
            }
            else
            {
                form.ShowErrorMsg("Get FrameRate Fail!", nRet);
            }
            return para;
        }

        public bool SetParameters(float Exposure, float Gain, float FrameRate)
        {
            bool bIsSetted = true;

            m_cMyDevice.MV_CODEREADER_SetEnumValue_NET("ExposureAuto", 0);
            int nRet = m_cMyDevice.MV_CODEREADER_SetFloatValue_NET("ExposureTime", Exposure);
            if (nRet != MvCodeReader.MV_CODEREADER_OK)
            {
                bIsSetted = false;
                form.ShowErrorMsg("Set Exposure Time Fail!", nRet);
            }

            m_cMyDevice.MV_CODEREADER_SetEnumValue_NET("GainAuto", 0);
            nRet = m_cMyDevice.MV_CODEREADER_SetFloatValue_NET("Gain", Gain);
            if (nRet != MvCodeReader.MV_CODEREADER_OK)
            {
                bIsSetted = false;
                form.ShowErrorMsg("Set Gain Fail!", nRet);
            }

            nRet = m_cMyDevice.MV_CODEREADER_SetFloatValue_NET("AcquisitionFrameRate", FrameRate);
            if (nRet != MvCodeReader.MV_CODEREADER_OK)
            {
                bIsSetted = false;
                form.ShowErrorMsg("Set Frame Rate Fail!", nRet);
            }
            return bIsSetted;
        }

        public string ErrorMsgDecode(int nErrorNum)
        {
            string error = string.Format("Unknown error number: {0}", nErrorNum);
            switch (nErrorNum)
            {
                case MvCodeReader.MV_CODEREADER_E_HANDLE: return " Error or invalid handle ";
                case MvCodeReader.MV_CODEREADER_E_SUPPORT: return " Not supported function ";
                case MvCodeReader.MV_CODEREADER_E_BUFOVER: return " Cache is full ";
                case MvCodeReader.MV_CODEREADER_E_CALLORDER: return " Function calling order error ";
                case MvCodeReader.MV_CODEREADER_E_PARAMETER: return " Incorrect parameter ";
                case MvCodeReader.MV_CODEREADER_E_RESOURCE: return " Applying resource failed ";
                case MvCodeReader.MV_CODEREADER_E_NODATA: return " No data ";
                case MvCodeReader.MV_CODEREADER_E_PRECONDITION: return " Precondition error, or running environment changed ";
                case MvCodeReader.MV_CODEREADER_E_VERSION: return " Version mismatches ";
                case MvCodeReader.MV_CODEREADER_E_NOENOUGH_BUF: return " Insufficient memory ";
                case MvCodeReader.MV_CODEREADER_E_UNKNOW: return " Unknown error ";
                case MvCodeReader.MV_CODEREADER_E_GC_GENERIC: return " General error ";
                case MvCodeReader.MV_CODEREADER_E_GC_ACCESS: return " Node accessing condition error ";
                case MvCodeReader.MV_CODEREADER_E_ACCESS_DENIED: return " No permission ";
                case MvCodeReader.MV_CODEREADER_E_BUSY: return " Device is busy, or network disconnected ";
                case MvCodeReader.MV_CODEREADER_E_NETER: return " Network error ";
            }
            return error;
        }

        public void ReceiveThreadProcess()
        {
            int nRet = MvCodeReader.MV_CODEREADER_OK;
            IntPtr pData = IntPtr.Zero;
            MvCodeReader.MV_CODEREADER_IMAGE_OUT_INFO_EX2 stFrameInfoEx2 = new MvCodeReader.MV_CODEREADER_IMAGE_OUT_INFO_EX2();
            IntPtr pstFrameInfoEx2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MvCodeReader.MV_CODEREADER_IMAGE_OUT_INFO_EX2)));
            Marshal.StructureToPtr(stFrameInfoEx2, pstFrameInfoEx2, false);

            while (form.m_bGrabbing)
            {
                if (!form.updating)
                {
                    if (previousIdx != form.collectIdx)
                    {
                        SetFocus(form.collectIdx);
                        Thread.Sleep(form.waitTime);
                    }
                    previousIdx = form.collectIdx;

                    nRet = m_cMyDevice.MV_CODEREADER_GetOneFrameTimeoutEx2_NET(ref pData, pstFrameInfoEx2, 1000);
                    if (nRet == MvCodeReader.MV_CODEREADER_OK)
                    {
                        stFrameInfoEx2 = (MvCodeReader.MV_CODEREADER_IMAGE_OUT_INFO_EX2)Marshal.PtrToStructure(pstFrameInfoEx2, typeof(MvCodeReader.MV_CODEREADER_IMAGE_OUT_INFO_EX2));
                    }

                    if (nRet == MvCodeReader.MV_CODEREADER_OK)
                    {
                        if (0 >= stFrameInfoEx2.nFrameLen)
                        {
                            continue;
                        }

                        // 绘制图像
                        Marshal.Copy(pData, m_BufForDriver, 0, (int)stFrameInfoEx2.nFrameLen);
                        if (stFrameInfoEx2.enPixelType == MvCodeReader.MvCodeReaderGvspPixelType.PixelType_CodeReader_Gvsp_Jpeg)
                        {
                            GC.Collect();
                            MemoryStream ms = new MemoryStream();
                            ms.Write(m_BufForDriver, 0, (int)stFrameInfoEx2.nFrameLen);
                            collectedImg = Image.FromStream(ms);
                        }

                        MvCodeReader.MV_CODEREADER_RESULT_BCR_EX2 stBcrResultEx2 = (MvCodeReader.MV_CODEREADER_RESULT_BCR_EX2)Marshal.PtrToStructure(stFrameInfoEx2.UnparsedBcrList.pstCodeListEx2, typeof(MvCodeReader.MV_CODEREADER_RESULT_BCR_EX2));

                        for (int i = 0; i < stBcrResultEx2.nCodeNum; ++i)
                        {
                            for (int j = 0; j < 4; ++j)
                            {
                                stPointList[j].X = (int)(stBcrResultEx2.stBcrInfoEx2[i].pt[j].x);// * (float)(pictureBoxSize[0])/ stFrameInfoEx2.nWidth);
                                stPointList[j].Y = (int)(stBcrResultEx2.stBcrInfoEx2[i].pt[j].y);// * (float)(pictureBoxSize[1])/ stFrameInfoEx2.nHeight);
                            }
                            BarData bar = new BarData();
                            bar.pointList = (Point[])stPointList.Clone();
                            bar.timeCost = (int)stBcrResultEx2.stBcrInfoEx2[i].nTotalProcCost + stBcrResultEx2.stBcrInfoEx2[i].sAlgoCost;
                            bar.barType = BarData.GetBarType((MvCodeReader.MV_CODEREADER_CODE_TYPE)stBcrResultEx2.stBcrInfoEx2[i].nBarType);
                            bar.barCode = bar.TrimBytes(System.Text.Encoding.Default.GetString(stBcrResultEx2.stBcrInfoEx2[i].chCode));
                            bar.score = (int)stBcrResultEx2.stBcrInfoEx2[i].nIDRScore;
                            bar.centerPoint = bar.CalculteCenterPoint(stPointList);
                            barDataList.Add(bar);
                        }
                        form.updating = true;
                        eventFinished(form.collectIdx);
                    }
                    Thread.Sleep(5);
                }
            }
            SetFocus(0);
        }
    }

    public class BarData
    {
        public int timeCost = 0;
        public string barType = "";
        public string barCode = "";
        public int score;
        public Point[] pointList = new Point[4];
        public GraphicsPath WayShapePath = new GraphicsPath();
        public GraphicsPath OcrShapePath = new GraphicsPath();
        public List<int> fourPointsX = new List<int>(4);
        public List<int> fourPointsY = new List<int>(4);
        public List<int> centerPoint = new List<int>(2);

        public static String GetBarType(MvCodeReader.MV_CODEREADER_CODE_TYPE nBarType)
        {
            switch (nBarType)
            {
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_TDCR_DM:
                    return "DM碼";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_TDCR_QR:
                    return "QR碼";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_EAN8:
                    return "EAN8碼";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_UPCE:
                    return "UPCE碼";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_UPCA:
                    return "UPCA碼";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_EAN13:
                    return "EAN13碼";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_ISBN13:
                    return "ISBN13碼";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_CODABAR:
                    return "库德巴碼";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_ITF25:
                    return "交叉25碼";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_CODE39:
                    return " Code 39碼";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_CODE93:
                    return "Code 93碼";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_CODE128:
                    return "Code 128碼";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_TDCR_PDF417:
                    return "PDF417碼";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_MATRIX25:
                    return "MATRIX25碼";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_MSI:
                    return "MSI碼";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_CODE11:
                    return "Code 11碼";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_INDUSTRIAL25:
                    return "industria125碼";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_CHINAPOST:
                    return "中国邮政碼";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_ITF14:
                    return "交叉14碼";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_TDCR_ECC140:
                    return "ECC140碼";
                default:
                    return "/";
            }
        }
        
        public List<int> CalculteCenterPoint(Point[] pointList)
        {
            List<int> centerPoint = new List<int>();
            int pxSum = 0;
            int pySum = 0;
            foreach(Point p in pointList)
            {
                pxSum += p.X;
                pySum += p.Y;
            }
            centerPoint.Add(pxSum / 4);
            centerPoint.Add(pySum / 4);
            return centerPoint;
        }
        
        public string TrimBytes(string str)
        {
            return str.Replace("\0", "");
        }
    }
}
