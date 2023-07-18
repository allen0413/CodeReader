
namespace WatsonCodeReader
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.comboBoxDeviceList = new System.Windows.Forms.ComboBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonApplySetting = new System.Windows.Forms.Button();
            this.buttonGetSetting = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbFrameRate = new System.Windows.Forms.TextBox();
            this.tbGain = new System.Windows.Forms.TextBox();
            this.tbExposure = new System.Windows.Forms.TextBox();
            this.buttonCloseDevice = new System.Windows.Forms.Button();
            this.buttonOpenDevice = new System.Windows.Forms.Button();
            this.buttonFindDevice = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkedListBox = new System.Windows.Forms.CheckedListBox();
            this.buttonEndGrab = new System.Windows.Forms.Button();
            this.buttonStartGrab = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReadTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.APPCostTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BarType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BarcodeContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.X座標 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y座標 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodeScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(25, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(587, 440);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // comboBoxDeviceList
            // 
            this.comboBoxDeviceList.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBoxDeviceList.FormattingEnabled = true;
            this.comboBoxDeviceList.Location = new System.Drawing.Point(128, 35);
            this.comboBoxDeviceList.Name = "comboBoxDeviceList";
            this.comboBoxDeviceList.Size = new System.Drawing.Size(364, 32);
            this.comboBoxDeviceList.TabIndex = 3;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox2.Location = new System.Drawing.Point(639, 27);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(592, 440);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox3.Location = new System.Drawing.Point(1253, 27);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(592, 440);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.buttonApplySetting);
            this.groupBox1.Controls.Add(this.buttonGetSetting);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbFrameRate);
            this.groupBox1.Controls.Add(this.tbGain);
            this.groupBox1.Controls.Add(this.tbExposure);
            this.groupBox1.Controls.Add(this.buttonCloseDevice);
            this.groupBox1.Controls.Add(this.buttonOpenDevice);
            this.groupBox1.Controls.Add(this.buttonFindDevice);
            this.groupBox1.Controls.Add(this.comboBoxDeviceList);
            this.groupBox1.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(44, 488);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(514, 251);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "相機設定";
            // 
            // buttonApplySetting
            // 
            this.buttonApplySetting.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonApplySetting.Location = new System.Drawing.Point(341, 205);
            this.buttonApplySetting.Name = "buttonApplySetting";
            this.buttonApplySetting.Size = new System.Drawing.Size(120, 30);
            this.buttonApplySetting.TabIndex = 14;
            this.buttonApplySetting.Text = "應用數據";
            this.buttonApplySetting.UseVisualStyleBackColor = true;
            this.buttonApplySetting.Click += new System.EventHandler(this.buttonApplySetting_Click);
            // 
            // buttonGetSetting
            // 
            this.buttonGetSetting.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonGetSetting.Location = new System.Drawing.Point(215, 205);
            this.buttonGetSetting.Name = "buttonGetSetting";
            this.buttonGetSetting.Size = new System.Drawing.Size(120, 30);
            this.buttonGetSetting.TabIndex = 13;
            this.buttonGetSetting.Text = "載入數據";
            this.buttonGetSetting.UseVisualStyleBackColor = true;
            this.buttonGetSetting.Click += new System.EventHandler(this.buttonGetSetting_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(211, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 24);
            this.label3.TabIndex = 12;
            this.label3.Text = "幀率";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(211, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 24);
            this.label2.TabIndex = 11;
            this.label2.Text = "增益";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(211, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 24);
            this.label1.TabIndex = 10;
            this.label1.Text = "曝光";
            // 
            // tbFrameRate
            // 
            this.tbFrameRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbFrameRate.Enabled = false;
            this.tbFrameRate.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbFrameRate.Location = new System.Drawing.Point(267, 170);
            this.tbFrameRate.Name = "tbFrameRate";
            this.tbFrameRate.Size = new System.Drawing.Size(194, 29);
            this.tbFrameRate.TabIndex = 9;
            // 
            // tbGain
            // 
            this.tbGain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbGain.Enabled = false;
            this.tbGain.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbGain.Location = new System.Drawing.Point(267, 129);
            this.tbGain.Name = "tbGain";
            this.tbGain.Size = new System.Drawing.Size(194, 29);
            this.tbGain.TabIndex = 8;
            // 
            // tbExposure
            // 
            this.tbExposure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbExposure.Enabled = false;
            this.tbExposure.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbExposure.Location = new System.Drawing.Point(267, 88);
            this.tbExposure.Name = "tbExposure";
            this.tbExposure.Size = new System.Drawing.Size(194, 29);
            this.tbExposure.TabIndex = 7;
            // 
            // buttonCloseDevice
            // 
            this.buttonCloseDevice.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonCloseDevice.Location = new System.Drawing.Point(34, 171);
            this.buttonCloseDevice.Name = "buttonCloseDevice";
            this.buttonCloseDevice.Size = new System.Drawing.Size(150, 55);
            this.buttonCloseDevice.TabIndex = 6;
            this.buttonCloseDevice.Text = "關閉相機";
            this.buttonCloseDevice.UseVisualStyleBackColor = true;
            this.buttonCloseDevice.Click += new System.EventHandler(this.buttonCloseDevice_Click);
            // 
            // buttonOpenDevice
            // 
            this.buttonOpenDevice.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonOpenDevice.Location = new System.Drawing.Point(34, 98);
            this.buttonOpenDevice.Name = "buttonOpenDevice";
            this.buttonOpenDevice.Size = new System.Drawing.Size(150, 55);
            this.buttonOpenDevice.TabIndex = 5;
            this.buttonOpenDevice.Text = "開啟相機";
            this.buttonOpenDevice.UseVisualStyleBackColor = true;
            this.buttonOpenDevice.Click += new System.EventHandler(this.buttonOpenDevice_Click);
            // 
            // buttonFindDevice
            // 
            this.buttonFindDevice.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonFindDevice.Location = new System.Drawing.Point(25, 34);
            this.buttonFindDevice.Name = "buttonFindDevice";
            this.buttonFindDevice.Size = new System.Drawing.Size(87, 33);
            this.buttonFindDevice.TabIndex = 4;
            this.buttonFindDevice.Text = "尋找相機";
            this.buttonFindDevice.UseVisualStyleBackColor = true;
            this.buttonFindDevice.Click += new System.EventHandler(this.buttonFindDevice_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.checkedListBox);
            this.groupBox2.Controls.Add(this.buttonEndGrab);
            this.groupBox2.Controls.Add(this.buttonStartGrab);
            this.groupBox2.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox2.Location = new System.Drawing.Point(44, 759);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(514, 190);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "條碼辨識";
            // 
            // checkedListBox
            // 
            this.checkedListBox.BackColor = System.Drawing.SystemColors.Control;
            this.checkedListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBox.CausesValidation = false;
            this.checkedListBox.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBox.FormattingEnabled = true;
            this.checkedListBox.Location = new System.Drawing.Point(290, 56);
            this.checkedListBox.Name = "checkedListBox";
            this.checkedListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.checkedListBox.Size = new System.Drawing.Size(191, 102);
            this.checkedListBox.TabIndex = 7;
            this.checkedListBox.UseCompatibleTextRendering = true;
            // 
            // buttonEndGrab
            // 
            this.buttonEndGrab.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonEndGrab.Location = new System.Drawing.Point(25, 112);
            this.buttonEndGrab.Name = "buttonEndGrab";
            this.buttonEndGrab.Size = new System.Drawing.Size(241, 50);
            this.buttonEndGrab.TabIndex = 6;
            this.buttonEndGrab.Text = "結束辨識";
            this.buttonEndGrab.UseVisualStyleBackColor = true;
            this.buttonEndGrab.Click += new System.EventHandler(this.buttonEndGrab_Click);
            // 
            // buttonStartGrab
            // 
            this.buttonStartGrab.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonStartGrab.Location = new System.Drawing.Point(25, 47);
            this.buttonStartGrab.Name = "buttonStartGrab";
            this.buttonStartGrab.Size = new System.Drawing.Size(241, 50);
            this.buttonStartGrab.TabIndex = 5;
            this.buttonStartGrab.Text = "開始辨識";
            this.buttonStartGrab.UseVisualStyleBackColor = true;
            this.buttonStartGrab.Click += new System.EventHandler(this.buttonStartGrab_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView.CausesValidation = false;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dataGridView.ColumnHeadersHeight = 50;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Number,
            this.ReadTime,
            this.APPCostTime,
            this.BarType,
            this.BarcodeContent,
            this.X座標,
            this.Y座標,
            this.CodeScore});
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle19;
            this.dataGridView.Enabled = false;
            this.dataGridView.EnableHeadersVisualStyles = false;
            this.dataGridView.Location = new System.Drawing.Point(587, 503);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowHeadersWidth = 50;
            this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle21;
            this.dataGridView.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dataGridView.RowTemplate.Height = 100;
            this.dataGridView.RowTemplate.ReadOnly = true;
            this.dataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(1243, 414);
            this.dataGridView.TabIndex = 8;
            // 
            // Number
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
            this.Number.DefaultCellStyle = dataGridViewCellStyle16;
            this.Number.FillWeight = 40F;
            this.Number.Frozen = true;
            this.Number.HeaderText = "編號";
            this.Number.Name = "Number";
            this.Number.ReadOnly = true;
            this.Number.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Number.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ReadTime
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ReadTime.DefaultCellStyle = dataGridViewCellStyle17;
            this.ReadTime.FillWeight = 200F;
            this.ReadTime.Frozen = true;
            this.ReadTime.HeaderText = "辨識時間";
            this.ReadTime.MinimumWidth = 10;
            this.ReadTime.Name = "ReadTime";
            this.ReadTime.ReadOnly = true;
            this.ReadTime.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ReadTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ReadTime.Width = 300;
            // 
            // APPCostTime
            // 
            this.APPCostTime.Frozen = true;
            this.APPCostTime.HeaderText = "耗時(ms)";
            this.APPCostTime.MinimumWidth = 10;
            this.APPCostTime.Name = "APPCostTime";
            this.APPCostTime.ReadOnly = true;
            this.APPCostTime.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.APPCostTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.APPCostTime.Width = 150;
            // 
            // BarType
            // 
            this.BarType.Frozen = true;
            this.BarType.HeaderText = "碼制";
            this.BarType.MinimumWidth = 10;
            this.BarType.Name = "BarType";
            this.BarType.ReadOnly = true;
            this.BarType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.BarType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BarType.Width = 120;
            // 
            // BarcodeContent
            // 
            this.BarcodeContent.FillWeight = 300F;
            this.BarcodeContent.Frozen = true;
            this.BarcodeContent.HeaderText = "碼內容";
            this.BarcodeContent.MinimumWidth = 50;
            this.BarcodeContent.Name = "BarcodeContent";
            this.BarcodeContent.ReadOnly = true;
            this.BarcodeContent.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.BarcodeContent.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BarcodeContent.Width = 250;
            // 
            // X座標
            // 
            this.X座標.Frozen = true;
            this.X座標.HeaderText = "X座標";
            this.X座標.Name = "X座標";
            this.X座標.ReadOnly = true;
            // 
            // Y座標
            // 
            this.Y座標.Frozen = true;
            this.Y座標.HeaderText = "Y座標";
            this.Y座標.Name = "Y座標";
            this.Y座標.ReadOnly = true;
            // 
            // CodeScore
            // 
            this.CodeScore.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CodeScore.DefaultCellStyle = dataGridViewCellStyle18;
            this.CodeScore.Frozen = true;
            this.CodeScore.HeaderText = "讀碼評分";
            this.CodeScore.MinimumWidth = 10;
            this.CodeScore.Name = "CodeScore";
            this.CodeScore.ReadOnly = true;
            this.CodeScore.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CodeScore.Width = 200;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1871, 968);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBoxDeviceList;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonCloseDevice;
        private System.Windows.Forms.Button buttonOpenDevice;
        private System.Windows.Forms.Button buttonFindDevice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbFrameRate;
        private System.Windows.Forms.TextBox tbGain;
        private System.Windows.Forms.TextBox tbExposure;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonEndGrab;
        private System.Windows.Forms.Button buttonStartGrab;
        private System.Windows.Forms.CheckedListBox checkedListBox;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReadTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn APPCostTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn BarType;
        private System.Windows.Forms.DataGridViewTextBoxColumn BarcodeContent;
        private System.Windows.Forms.DataGridViewTextBoxColumn X座標;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y座標;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodeScore;
        private System.Windows.Forms.Button buttonApplySetting;
        private System.Windows.Forms.Button buttonGetSetting;
    }
}

