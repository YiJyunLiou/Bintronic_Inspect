namespace Bintronic_Inspect
{
    partial class formMain
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
            this.components = new System.ComponentModel.Container();
            this.mns1 = new System.Windows.Forms.MenuStrip();
            this.mnsSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.grbArea2 = new System.Windows.Forms.GroupBox();
            this.tbxInspector = new System.Windows.Forms.TextBox();
            this.lblNowDateTime = new System.Windows.Forms.Label();
            this.cboModel = new System.Windows.Forms.ComboBox();
            this.tbxSerialNumber = new System.Windows.Forms.TextBox();
            this.lblModelTitle = new System.Windows.Forms.Label();
            this.lblInspectorTitle = new System.Windows.Forms.Label();
            this.lblSerialNumberTitle = new System.Windows.Forms.Label();
            this.lblNowTimeTitle = new System.Windows.Forms.Label();
            this.grbArea3 = new System.Windows.Forms.GroupBox();
            this.btnSkipToNextTest = new System.Windows.Forms.Button();
            this.btnDetection = new System.Windows.Forms.Button();
            this.grbArea4 = new System.Windows.Forms.GroupBox();
            this.btnLedNG = new System.Windows.Forms.Button();
            this.btnLedOK = new System.Windows.Forms.Button();
            this.lblLedTest = new System.Windows.Forms.Label();
            this.grbArea5 = new System.Windows.Forms.GroupBox();
            this.lblReciprocal = new System.Windows.Forms.Label();
            this.lblDetectionProcessDescription = new System.Windows.Forms.Label();
            this.lblDetectionProcessDescriptionTitle = new System.Windows.Forms.Label();
            this.grbArea6 = new System.Windows.Forms.GroupBox();
            this.lblTestLedResult = new System.Windows.Forms.Label();
            this.lblTestTxRxP3Result = new System.Windows.Forms.Label();
            this.lblTestControlBP10Result = new System.Windows.Forms.Label();
            this.lblTestControlAP9Result = new System.Windows.Forms.Label();
            this.lblTest485CommunicationP5Result = new System.Windows.Forms.Label();
            this.lblTest485CommunicationP4Result = new System.Windows.Forms.Label();
            this.lblTestModuleFirmwareVersionResult = new System.Windows.Forms.Label();
            this.lblTestSW2Result = new System.Windows.Forms.Label();
            this.lblTestSW1Result = new System.Windows.Forms.Label();
            this.lblTestSW2_2V = new System.Windows.Forms.Label();
            this.lblTestSW2_2 = new System.Windows.Forms.Label();
            this.lblTestSW2_1V = new System.Windows.Forms.Label();
            this.lblTestSW2_1 = new System.Windows.Forms.Label();
            this.btnLine8 = new System.Windows.Forms.Button();
            this.btnLine7 = new System.Windows.Forms.Button();
            this.btnLine6 = new System.Windows.Forms.Button();
            this.btnLine5 = new System.Windows.Forms.Button();
            this.btnLine4 = new System.Windows.Forms.Button();
            this.btnLine3 = new System.Windows.Forms.Button();
            this.btnLine2 = new System.Windows.Forms.Button();
            this.lblTestSW1_6V = new System.Windows.Forms.Label();
            this.lblTestSW1_5V = new System.Windows.Forms.Label();
            this.lblTestSW1_4V = new System.Windows.Forms.Label();
            this.lblTestSW1_3V = new System.Windows.Forms.Label();
            this.lblTestSW1_2V = new System.Windows.Forms.Label();
            this.lblTestSW1_6 = new System.Windows.Forms.Label();
            this.lblTestSW1_5 = new System.Windows.Forms.Label();
            this.lblTestSW1_4 = new System.Windows.Forms.Label();
            this.lblTestSW1_3 = new System.Windows.Forms.Label();
            this.lblTestSW1_2 = new System.Windows.Forms.Label();
            this.lblTestSW1_1V = new System.Windows.Forms.Label();
            this.lblTestSW1_1 = new System.Windows.Forms.Label();
            this.btnLine1 = new System.Windows.Forms.Button();
            this.lblTestLed = new System.Windows.Forms.Label();
            this.lblTestTxRxP3 = new System.Windows.Forms.Label();
            this.lblTestControlBP10 = new System.Windows.Forms.Label();
            this.lblTestControlAP9 = new System.Windows.Forms.Label();
            this.lblTest485CommunicationP5 = new System.Windows.Forms.Label();
            this.lblTest485CommunicationP4 = new System.Windows.Forms.Label();
            this.lblTestModuleFirmwareVersion = new System.Windows.Forms.Label();
            this.lblTestResultSW2 = new System.Windows.Forms.Label();
            this.lblTestSW1 = new System.Windows.Forms.Label();
            this.lblTestResultTitle = new System.Windows.Forms.Label();
            this.tmrNowDateTime = new System.Windows.Forms.Timer(this.components);
            this.tmrReciproca = new System.Windows.Forms.Timer(this.components);
            this.tmrLedState = new System.Windows.Forms.Timer(this.components);
            this.tmrDetection = new System.Windows.Forms.Timer(this.components);
            this.tmrLedStateFailDelay = new System.Windows.Forms.Timer(this.components);
            this.mns1.SuspendLayout();
            this.grbArea2.SuspendLayout();
            this.grbArea3.SuspendLayout();
            this.grbArea4.SuspendLayout();
            this.grbArea5.SuspendLayout();
            this.grbArea6.SuspendLayout();
            this.SuspendLayout();
            // 
            // mns1
            // 
            this.mns1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnsSetting});
            this.mns1.Location = new System.Drawing.Point(0, 0);
            this.mns1.Name = "mns1";
            this.mns1.Size = new System.Drawing.Size(1248, 34);
            this.mns1.TabIndex = 0;
            this.mns1.Text = "menuStrip1";
            // 
            // mnsSetting
            // 
            this.mnsSetting.Font = new System.Drawing.Font("Microsoft JhengHei UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.mnsSetting.Name = "mnsSetting";
            this.mnsSetting.Size = new System.Drawing.Size(66, 30);
            this.mnsSetting.Text = "設定";
            // 
            // grbArea2
            // 
            this.grbArea2.Controls.Add(this.tbxInspector);
            this.grbArea2.Controls.Add(this.lblNowDateTime);
            this.grbArea2.Controls.Add(this.cboModel);
            this.grbArea2.Controls.Add(this.tbxSerialNumber);
            this.grbArea2.Controls.Add(this.lblModelTitle);
            this.grbArea2.Controls.Add(this.lblInspectorTitle);
            this.grbArea2.Controls.Add(this.lblSerialNumberTitle);
            this.grbArea2.Controls.Add(this.lblNowTimeTitle);
            this.grbArea2.Location = new System.Drawing.Point(32, 37);
            this.grbArea2.Name = "grbArea2";
            this.grbArea2.Size = new System.Drawing.Size(408, 258);
            this.grbArea2.TabIndex = 1;
            this.grbArea2.TabStop = false;
            // 
            // tbxInspector
            // 
            this.tbxInspector.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbxInspector.Location = new System.Drawing.Point(170, 135);
            this.tbxInspector.Name = "tbxInspector";
            this.tbxInspector.Size = new System.Drawing.Size(209, 39);
            this.tbxInspector.TabIndex = 8;
            // 
            // lblNowDateTime
            // 
            this.lblNowDateTime.AutoSize = true;
            this.lblNowDateTime.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblNowDateTime.Location = new System.Drawing.Point(168, 39);
            this.lblNowDateTime.Name = "lblNowDateTime";
            this.lblNowDateTime.Size = new System.Drawing.Size(213, 27);
            this.lblNowDateTime.TabIndex = 7;
            this.lblNowDateTime.Text = "2023/05/11 14:13:22";
            // 
            // cboModel
            // 
            this.cboModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModel.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cboModel.FormattingEnabled = true;
            this.cboModel.Location = new System.Drawing.Point(170, 187);
            this.cboModel.Name = "cboModel";
            this.cboModel.Size = new System.Drawing.Size(209, 38);
            this.cboModel.TabIndex = 6;
            // 
            // tbxSerialNumber
            // 
            this.tbxSerialNumber.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbxSerialNumber.Location = new System.Drawing.Point(170, 83);
            this.tbxSerialNumber.Name = "tbxSerialNumber";
            this.tbxSerialNumber.Size = new System.Drawing.Size(209, 39);
            this.tbxSerialNumber.TabIndex = 5;
            // 
            // lblModelTitle
            // 
            this.lblModelTitle.AutoSize = true;
            this.lblModelTitle.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblModelTitle.Location = new System.Drawing.Point(78, 187);
            this.lblModelTitle.Name = "lblModelTitle";
            this.lblModelTitle.Size = new System.Drawing.Size(85, 30);
            this.lblModelTitle.TabIndex = 4;
            this.lblModelTitle.Text = "型號：";
            // 
            // lblInspectorTitle
            // 
            this.lblInspectorTitle.AutoSize = true;
            this.lblInspectorTitle.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblInspectorTitle.Location = new System.Drawing.Point(30, 138);
            this.lblInspectorTitle.Name = "lblInspectorTitle";
            this.lblInspectorTitle.Size = new System.Drawing.Size(133, 30);
            this.lblInspectorTitle.TabIndex = 3;
            this.lblInspectorTitle.Text = "檢測人員：";
            // 
            // lblSerialNumberTitle
            // 
            this.lblSerialNumberTitle.AutoSize = true;
            this.lblSerialNumberTitle.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblSerialNumberTitle.Location = new System.Drawing.Point(30, 86);
            this.lblSerialNumberTitle.Name = "lblSerialNumberTitle";
            this.lblSerialNumberTitle.Size = new System.Drawing.Size(133, 30);
            this.lblSerialNumberTitle.TabIndex = 2;
            this.lblSerialNumberTitle.Text = "生產序號：";
            // 
            // lblNowTimeTitle
            // 
            this.lblNowTimeTitle.AutoSize = true;
            this.lblNowTimeTitle.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblNowTimeTitle.Location = new System.Drawing.Point(30, 35);
            this.lblNowTimeTitle.Name = "lblNowTimeTitle";
            this.lblNowTimeTitle.Size = new System.Drawing.Size(109, 30);
            this.lblNowTimeTitle.TabIndex = 1;
            this.lblNowTimeTitle.Text = "目前時間";
            // 
            // grbArea3
            // 
            this.grbArea3.Controls.Add(this.btnSkipToNextTest);
            this.grbArea3.Controls.Add(this.btnDetection);
            this.grbArea3.Location = new System.Drawing.Point(32, 311);
            this.grbArea3.Name = "grbArea3";
            this.grbArea3.Size = new System.Drawing.Size(408, 215);
            this.grbArea3.TabIndex = 2;
            this.grbArea3.TabStop = false;
            // 
            // btnSkipToNextTest
            // 
            this.btnSkipToNextTest.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSkipToNextTest.Location = new System.Drawing.Point(106, 124);
            this.btnSkipToNextTest.Name = "btnSkipToNextTest";
            this.btnSkipToNextTest.Size = new System.Drawing.Size(196, 39);
            this.btnSkipToNextTest.TabIndex = 1;
            this.btnSkipToNextTest.Text = "跳至下項檢測";
            this.btnSkipToNextTest.UseVisualStyleBackColor = true;
            // 
            // btnDetection
            // 
            this.btnDetection.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnDetection.Location = new System.Drawing.Point(106, 47);
            this.btnDetection.Name = "btnDetection";
            this.btnDetection.Size = new System.Drawing.Size(196, 39);
            this.btnDetection.TabIndex = 0;
            this.btnDetection.Text = "檢測";
            this.btnDetection.UseVisualStyleBackColor = true;
            // 
            // grbArea4
            // 
            this.grbArea4.Controls.Add(this.btnLedNG);
            this.grbArea4.Controls.Add(this.btnLedOK);
            this.grbArea4.Controls.Add(this.lblLedTest);
            this.grbArea4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.grbArea4.Location = new System.Drawing.Point(32, 542);
            this.grbArea4.Name = "grbArea4";
            this.grbArea4.Size = new System.Drawing.Size(408, 185);
            this.grbArea4.TabIndex = 2;
            this.grbArea4.TabStop = false;
            // 
            // btnLedNG
            // 
            this.btnLedNG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(60)))), ((int)(((byte)(0)))));
            this.btnLedNG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLedNG.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLedNG.ForeColor = System.Drawing.Color.White;
            this.btnLedNG.Location = new System.Drawing.Point(227, 101);
            this.btnLedNG.Name = "btnLedNG";
            this.btnLedNG.Size = new System.Drawing.Size(128, 39);
            this.btnLedNG.TabIndex = 3;
            this.btnLedNG.Text = "NG";
            this.btnLedNG.UseVisualStyleBackColor = false;
            // 
            // btnLedOK
            // 
            this.btnLedOK.BackColor = System.Drawing.Color.Green;
            this.btnLedOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLedOK.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLedOK.ForeColor = System.Drawing.Color.White;
            this.btnLedOK.Location = new System.Drawing.Point(54, 101);
            this.btnLedOK.Name = "btnLedOK";
            this.btnLedOK.Size = new System.Drawing.Size(128, 39);
            this.btnLedOK.TabIndex = 2;
            this.btnLedOK.Text = "OK";
            this.btnLedOK.UseVisualStyleBackColor = true;
            // 
            // lblLedTest
            // 
            this.lblLedTest.AutoSize = true;
            this.lblLedTest.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblLedTest.Location = new System.Drawing.Point(127, 35);
            this.lblLedTest.Name = "lblLedTest";
            this.lblLedTest.Size = new System.Drawing.Size(152, 30);
            this.lblLedTest.TabIndex = 0;
            this.lblLedTest.Text = "LED檢測結果";
            // 
            // grbArea5
            // 
            this.grbArea5.Controls.Add(this.lblReciprocal);
            this.grbArea5.Controls.Add(this.lblDetectionProcessDescription);
            this.grbArea5.Controls.Add(this.lblDetectionProcessDescriptionTitle);
            this.grbArea5.Location = new System.Drawing.Point(464, 37);
            this.grbArea5.Name = "grbArea5";
            this.grbArea5.Size = new System.Drawing.Size(753, 175);
            this.grbArea5.TabIndex = 2;
            this.grbArea5.TabStop = false;
            // 
            // lblReciprocal
            // 
            this.lblReciprocal.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblReciprocal.Location = new System.Drawing.Point(300, 132);
            this.lblReciprocal.Name = "lblReciprocal";
            this.lblReciprocal.Size = new System.Drawing.Size(157, 30);
            this.lblReciprocal.TabIndex = 4;
            this.lblReciprocal.Text = "5";
            this.lblReciprocal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDetectionProcessDescription
            // 
            this.lblDetectionProcessDescription.AllowDrop = true;
            this.lblDetectionProcessDescription.Font = new System.Drawing.Font("微軟正黑體", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblDetectionProcessDescription.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblDetectionProcessDescription.Location = new System.Drawing.Point(6, 52);
            this.lblDetectionProcessDescription.Name = "lblDetectionProcessDescription";
            this.lblDetectionProcessDescription.Size = new System.Drawing.Size(741, 97);
            this.lblDetectionProcessDescription.TabIndex = 3;
            this.lblDetectionProcessDescription.Text = "請將SW1 全撥到OFF";
            this.lblDetectionProcessDescription.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblDetectionProcessDescriptionTitle
            // 
            this.lblDetectionProcessDescriptionTitle.AutoSize = true;
            this.lblDetectionProcessDescriptionTitle.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblDetectionProcessDescriptionTitle.Location = new System.Drawing.Point(299, 18);
            this.lblDetectionProcessDescriptionTitle.Name = "lblDetectionProcessDescriptionTitle";
            this.lblDetectionProcessDescriptionTitle.Size = new System.Drawing.Size(157, 30);
            this.lblDetectionProcessDescriptionTitle.TabIndex = 2;
            this.lblDetectionProcessDescriptionTitle.Text = "檢測流程說明";
            // 
            // grbArea6
            // 
            this.grbArea6.Controls.Add(this.lblTestLedResult);
            this.grbArea6.Controls.Add(this.lblTestTxRxP3Result);
            this.grbArea6.Controls.Add(this.lblTestControlBP10Result);
            this.grbArea6.Controls.Add(this.lblTestControlAP9Result);
            this.grbArea6.Controls.Add(this.lblTest485CommunicationP5Result);
            this.grbArea6.Controls.Add(this.lblTest485CommunicationP4Result);
            this.grbArea6.Controls.Add(this.lblTestModuleFirmwareVersionResult);
            this.grbArea6.Controls.Add(this.lblTestSW2Result);
            this.grbArea6.Controls.Add(this.lblTestSW1Result);
            this.grbArea6.Controls.Add(this.lblTestSW2_2V);
            this.grbArea6.Controls.Add(this.lblTestSW2_2);
            this.grbArea6.Controls.Add(this.lblTestSW2_1V);
            this.grbArea6.Controls.Add(this.lblTestSW2_1);
            this.grbArea6.Controls.Add(this.btnLine8);
            this.grbArea6.Controls.Add(this.btnLine7);
            this.grbArea6.Controls.Add(this.btnLine6);
            this.grbArea6.Controls.Add(this.btnLine5);
            this.grbArea6.Controls.Add(this.btnLine4);
            this.grbArea6.Controls.Add(this.btnLine3);
            this.grbArea6.Controls.Add(this.btnLine2);
            this.grbArea6.Controls.Add(this.lblTestSW1_6V);
            this.grbArea6.Controls.Add(this.lblTestSW1_5V);
            this.grbArea6.Controls.Add(this.lblTestSW1_4V);
            this.grbArea6.Controls.Add(this.lblTestSW1_3V);
            this.grbArea6.Controls.Add(this.lblTestSW1_2V);
            this.grbArea6.Controls.Add(this.lblTestSW1_6);
            this.grbArea6.Controls.Add(this.lblTestSW1_5);
            this.grbArea6.Controls.Add(this.lblTestSW1_4);
            this.grbArea6.Controls.Add(this.lblTestSW1_3);
            this.grbArea6.Controls.Add(this.lblTestSW1_2);
            this.grbArea6.Controls.Add(this.lblTestSW1_1V);
            this.grbArea6.Controls.Add(this.lblTestSW1_1);
            this.grbArea6.Controls.Add(this.btnLine1);
            this.grbArea6.Controls.Add(this.lblTestLed);
            this.grbArea6.Controls.Add(this.lblTestTxRxP3);
            this.grbArea6.Controls.Add(this.lblTestControlBP10);
            this.grbArea6.Controls.Add(this.lblTestControlAP9);
            this.grbArea6.Controls.Add(this.lblTest485CommunicationP5);
            this.grbArea6.Controls.Add(this.lblTest485CommunicationP4);
            this.grbArea6.Controls.Add(this.lblTestModuleFirmwareVersion);
            this.grbArea6.Controls.Add(this.lblTestResultSW2);
            this.grbArea6.Controls.Add(this.lblTestSW1);
            this.grbArea6.Controls.Add(this.lblTestResultTitle);
            this.grbArea6.Location = new System.Drawing.Point(464, 227);
            this.grbArea6.Name = "grbArea6";
            this.grbArea6.Size = new System.Drawing.Size(753, 500);
            this.grbArea6.TabIndex = 2;
            this.grbArea6.TabStop = false;
            // 
            // lblTestLedResult
            // 
            this.lblTestLedResult.AutoSize = true;
            this.lblTestLedResult.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestLedResult.Location = new System.Drawing.Point(600, 455);
            this.lblTestLedResult.Name = "lblTestLedResult";
            this.lblTestLedResult.Size = new System.Drawing.Size(61, 30);
            this.lblTestLedResult.TabIndex = 47;
            this.lblTestLedResult.Text = "待測";
            // 
            // lblTestTxRxP3Result
            // 
            this.lblTestTxRxP3Result.AutoSize = true;
            this.lblTestTxRxP3Result.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestTxRxP3Result.Location = new System.Drawing.Point(600, 416);
            this.lblTestTxRxP3Result.Name = "lblTestTxRxP3Result";
            this.lblTestTxRxP3Result.Size = new System.Drawing.Size(61, 30);
            this.lblTestTxRxP3Result.TabIndex = 46;
            this.lblTestTxRxP3Result.Text = "待測";
            // 
            // lblTestControlBP10Result
            // 
            this.lblTestControlBP10Result.AutoSize = true;
            this.lblTestControlBP10Result.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestControlBP10Result.Location = new System.Drawing.Point(600, 377);
            this.lblTestControlBP10Result.Name = "lblTestControlBP10Result";
            this.lblTestControlBP10Result.Size = new System.Drawing.Size(61, 30);
            this.lblTestControlBP10Result.TabIndex = 45;
            this.lblTestControlBP10Result.Text = "待測";
            // 
            // lblTestControlAP9Result
            // 
            this.lblTestControlAP9Result.AutoSize = true;
            this.lblTestControlAP9Result.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestControlAP9Result.Location = new System.Drawing.Point(600, 335);
            this.lblTestControlAP9Result.Name = "lblTestControlAP9Result";
            this.lblTestControlAP9Result.Size = new System.Drawing.Size(85, 30);
            this.lblTestControlAP9Result.TabIndex = 44;
            this.lblTestControlAP9Result.Text = "測試中";
            // 
            // lblTest485CommunicationP5Result
            // 
            this.lblTest485CommunicationP5Result.AutoSize = true;
            this.lblTest485CommunicationP5Result.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTest485CommunicationP5Result.Location = new System.Drawing.Point(600, 294);
            this.lblTest485CommunicationP5Result.Name = "lblTest485CommunicationP5Result";
            this.lblTest485CommunicationP5Result.Size = new System.Drawing.Size(93, 30);
            this.lblTest485CommunicationP5Result.TabIndex = 43;
            this.lblTest485CommunicationP5Result.Text = "FAILED";
            // 
            // lblTest485CommunicationP4Result
            // 
            this.lblTest485CommunicationP4Result.AutoSize = true;
            this.lblTest485CommunicationP4Result.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTest485CommunicationP4Result.Location = new System.Drawing.Point(600, 252);
            this.lblTest485CommunicationP4Result.Name = "lblTest485CommunicationP4Result";
            this.lblTest485CommunicationP4Result.Size = new System.Drawing.Size(72, 30);
            this.lblTest485CommunicationP4Result.TabIndex = 42;
            this.lblTest485CommunicationP4Result.Text = "PASS";
            // 
            // lblTestModuleFirmwareVersionResult
            // 
            this.lblTestModuleFirmwareVersionResult.AutoSize = true;
            this.lblTestModuleFirmwareVersionResult.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestModuleFirmwareVersionResult.Location = new System.Drawing.Point(600, 211);
            this.lblTestModuleFirmwareVersionResult.Name = "lblTestModuleFirmwareVersionResult";
            this.lblTestModuleFirmwareVersionResult.Size = new System.Drawing.Size(61, 30);
            this.lblTestModuleFirmwareVersionResult.TabIndex = 41;
            this.lblTestModuleFirmwareVersionResult.Text = "12.1";
            // 
            // lblTestSW2Result
            // 
            this.lblTestSW2Result.AutoSize = true;
            this.lblTestSW2Result.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestSW2Result.Location = new System.Drawing.Point(600, 168);
            this.lblTestSW2Result.Name = "lblTestSW2Result";
            this.lblTestSW2Result.Size = new System.Drawing.Size(72, 30);
            this.lblTestSW2Result.TabIndex = 40;
            this.lblTestSW2Result.Text = "PASS";
            // 
            // lblTestSW1Result
            // 
            this.lblTestSW1Result.AutoSize = true;
            this.lblTestSW1Result.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestSW1Result.Location = new System.Drawing.Point(600, 94);
            this.lblTestSW1Result.Name = "lblTestSW1Result";
            this.lblTestSW1Result.Size = new System.Drawing.Size(72, 30);
            this.lblTestSW1Result.TabIndex = 39;
            this.lblTestSW1Result.Text = "PASS";
            // 
            // lblTestSW2_2V
            // 
            this.lblTestSW2_2V.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestSW2_2V.Location = new System.Drawing.Point(165, 171);
            this.lblTestSW2_2V.Name = "lblTestSW2_2V";
            this.lblTestSW2_2V.Size = new System.Drawing.Size(70, 27);
            this.lblTestSW2_2V.TabIndex = 38;
            this.lblTestSW2_2V.Text = "ON";
            this.lblTestSW2_2V.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTestSW2_2
            // 
            this.lblTestSW2_2.AutoSize = true;
            this.lblTestSW2_2.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestSW2_2.Location = new System.Drawing.Point(189, 139);
            this.lblTestSW2_2.Name = "lblTestSW2_2";
            this.lblTestSW2_2.Size = new System.Drawing.Size(24, 27);
            this.lblTestSW2_2.TabIndex = 37;
            this.lblTestSW2_2.Text = "2";
            // 
            // lblTestSW2_1V
            // 
            this.lblTestSW2_1V.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestSW2_1V.Location = new System.Drawing.Point(89, 171);
            this.lblTestSW2_1V.Name = "lblTestSW2_1V";
            this.lblTestSW2_1V.Size = new System.Drawing.Size(70, 27);
            this.lblTestSW2_1V.TabIndex = 36;
            this.lblTestSW2_1V.Text = "ON";
            this.lblTestSW2_1V.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTestSW2_1
            // 
            this.lblTestSW2_1.AutoSize = true;
            this.lblTestSW2_1.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestSW2_1.Location = new System.Drawing.Point(113, 139);
            this.lblTestSW2_1.Name = "lblTestSW2_1";
            this.lblTestSW2_1.Size = new System.Drawing.Size(24, 27);
            this.lblTestSW2_1.TabIndex = 35;
            this.lblTestSW2_1.Text = "1";
            // 
            // btnLine8
            // 
            this.btnLine8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnLine8.FlatAppearance.BorderSize = 0;
            this.btnLine8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLine8.Location = new System.Drawing.Point(15, 449);
            this.btnLine8.Name = "btnLine8";
            this.btnLine8.Size = new System.Drawing.Size(720, 2);
            this.btnLine8.TabIndex = 34;
            this.btnLine8.UseVisualStyleBackColor = false;
            // 
            // btnLine7
            // 
            this.btnLine7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnLine7.FlatAppearance.BorderSize = 0;
            this.btnLine7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLine7.Location = new System.Drawing.Point(15, 410);
            this.btnLine7.Name = "btnLine7";
            this.btnLine7.Size = new System.Drawing.Size(720, 2);
            this.btnLine7.TabIndex = 33;
            this.btnLine7.UseVisualStyleBackColor = false;
            // 
            // btnLine6
            // 
            this.btnLine6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnLine6.FlatAppearance.BorderSize = 0;
            this.btnLine6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLine6.Location = new System.Drawing.Point(15, 370);
            this.btnLine6.Name = "btnLine6";
            this.btnLine6.Size = new System.Drawing.Size(720, 2);
            this.btnLine6.TabIndex = 32;
            this.btnLine6.UseVisualStyleBackColor = false;
            // 
            // btnLine5
            // 
            this.btnLine5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnLine5.FlatAppearance.BorderSize = 0;
            this.btnLine5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLine5.Location = new System.Drawing.Point(15, 329);
            this.btnLine5.Name = "btnLine5";
            this.btnLine5.Size = new System.Drawing.Size(720, 2);
            this.btnLine5.TabIndex = 31;
            this.btnLine5.UseVisualStyleBackColor = false;
            // 
            // btnLine4
            // 
            this.btnLine4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnLine4.FlatAppearance.BorderSize = 0;
            this.btnLine4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLine4.Location = new System.Drawing.Point(15, 288);
            this.btnLine4.Name = "btnLine4";
            this.btnLine4.Size = new System.Drawing.Size(720, 2);
            this.btnLine4.TabIndex = 30;
            this.btnLine4.UseVisualStyleBackColor = false;
            // 
            // btnLine3
            // 
            this.btnLine3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnLine3.FlatAppearance.BorderSize = 0;
            this.btnLine3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLine3.Location = new System.Drawing.Point(15, 245);
            this.btnLine3.Name = "btnLine3";
            this.btnLine3.Size = new System.Drawing.Size(720, 2);
            this.btnLine3.TabIndex = 29;
            this.btnLine3.UseVisualStyleBackColor = false;
            // 
            // btnLine2
            // 
            this.btnLine2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnLine2.FlatAppearance.BorderSize = 0;
            this.btnLine2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLine2.Location = new System.Drawing.Point(15, 204);
            this.btnLine2.Name = "btnLine2";
            this.btnLine2.Size = new System.Drawing.Size(720, 2);
            this.btnLine2.TabIndex = 28;
            this.btnLine2.UseVisualStyleBackColor = false;
            // 
            // lblTestSW1_6V
            // 
            this.lblTestSW1_6V.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestSW1_6V.Location = new System.Drawing.Point(469, 97);
            this.lblTestSW1_6V.Name = "lblTestSW1_6V";
            this.lblTestSW1_6V.Size = new System.Drawing.Size(70, 27);
            this.lblTestSW1_6V.TabIndex = 27;
            this.lblTestSW1_6V.Text = "ON";
            this.lblTestSW1_6V.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTestSW1_5V
            // 
            this.lblTestSW1_5V.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestSW1_5V.Location = new System.Drawing.Point(393, 97);
            this.lblTestSW1_5V.Name = "lblTestSW1_5V";
            this.lblTestSW1_5V.Size = new System.Drawing.Size(70, 27);
            this.lblTestSW1_5V.TabIndex = 26;
            this.lblTestSW1_5V.Text = "ON";
            this.lblTestSW1_5V.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTestSW1_4V
            // 
            this.lblTestSW1_4V.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestSW1_4V.Location = new System.Drawing.Point(317, 97);
            this.lblTestSW1_4V.Name = "lblTestSW1_4V";
            this.lblTestSW1_4V.Size = new System.Drawing.Size(70, 27);
            this.lblTestSW1_4V.TabIndex = 25;
            this.lblTestSW1_4V.Text = "ON";
            this.lblTestSW1_4V.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTestSW1_3V
            // 
            this.lblTestSW1_3V.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestSW1_3V.Location = new System.Drawing.Point(241, 97);
            this.lblTestSW1_3V.Name = "lblTestSW1_3V";
            this.lblTestSW1_3V.Size = new System.Drawing.Size(70, 27);
            this.lblTestSW1_3V.TabIndex = 24;
            this.lblTestSW1_3V.Text = "ON";
            this.lblTestSW1_3V.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTestSW1_2V
            // 
            this.lblTestSW1_2V.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestSW1_2V.Location = new System.Drawing.Point(165, 97);
            this.lblTestSW1_2V.Name = "lblTestSW1_2V";
            this.lblTestSW1_2V.Size = new System.Drawing.Size(70, 27);
            this.lblTestSW1_2V.TabIndex = 23;
            this.lblTestSW1_2V.Text = "ON";
            this.lblTestSW1_2V.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTestSW1_6
            // 
            this.lblTestSW1_6.AutoSize = true;
            this.lblTestSW1_6.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestSW1_6.Location = new System.Drawing.Point(491, 60);
            this.lblTestSW1_6.Name = "lblTestSW1_6";
            this.lblTestSW1_6.Size = new System.Drawing.Size(24, 27);
            this.lblTestSW1_6.TabIndex = 22;
            this.lblTestSW1_6.Text = "6";
            // 
            // lblTestSW1_5
            // 
            this.lblTestSW1_5.AutoSize = true;
            this.lblTestSW1_5.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestSW1_5.Location = new System.Drawing.Point(416, 60);
            this.lblTestSW1_5.Name = "lblTestSW1_5";
            this.lblTestSW1_5.Size = new System.Drawing.Size(24, 27);
            this.lblTestSW1_5.TabIndex = 21;
            this.lblTestSW1_5.Text = "5";
            // 
            // lblTestSW1_4
            // 
            this.lblTestSW1_4.AutoSize = true;
            this.lblTestSW1_4.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestSW1_4.Location = new System.Drawing.Point(340, 60);
            this.lblTestSW1_4.Name = "lblTestSW1_4";
            this.lblTestSW1_4.Size = new System.Drawing.Size(24, 27);
            this.lblTestSW1_4.TabIndex = 20;
            this.lblTestSW1_4.Text = "4";
            // 
            // lblTestSW1_3
            // 
            this.lblTestSW1_3.AutoSize = true;
            this.lblTestSW1_3.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestSW1_3.Location = new System.Drawing.Point(263, 60);
            this.lblTestSW1_3.Name = "lblTestSW1_3";
            this.lblTestSW1_3.Size = new System.Drawing.Size(24, 27);
            this.lblTestSW1_3.TabIndex = 19;
            this.lblTestSW1_3.Text = "3";
            // 
            // lblTestSW1_2
            // 
            this.lblTestSW1_2.AutoSize = true;
            this.lblTestSW1_2.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestSW1_2.Location = new System.Drawing.Point(189, 60);
            this.lblTestSW1_2.Name = "lblTestSW1_2";
            this.lblTestSW1_2.Size = new System.Drawing.Size(24, 27);
            this.lblTestSW1_2.TabIndex = 18;
            this.lblTestSW1_2.Text = "2";
            // 
            // lblTestSW1_1V
            // 
            this.lblTestSW1_1V.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestSW1_1V.Location = new System.Drawing.Point(89, 97);
            this.lblTestSW1_1V.Name = "lblTestSW1_1V";
            this.lblTestSW1_1V.Size = new System.Drawing.Size(70, 27);
            this.lblTestSW1_1V.TabIndex = 17;
            this.lblTestSW1_1V.Text = "ON";
            this.lblTestSW1_1V.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTestSW1_1
            // 
            this.lblTestSW1_1.AutoSize = true;
            this.lblTestSW1_1.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestSW1_1.Location = new System.Drawing.Point(113, 60);
            this.lblTestSW1_1.Name = "lblTestSW1_1";
            this.lblTestSW1_1.Size = new System.Drawing.Size(24, 27);
            this.lblTestSW1_1.TabIndex = 16;
            this.lblTestSW1_1.Text = "1";
            // 
            // btnLine1
            // 
            this.btnLine1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnLine1.FlatAppearance.BorderSize = 0;
            this.btnLine1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLine1.Location = new System.Drawing.Point(15, 129);
            this.btnLine1.Name = "btnLine1";
            this.btnLine1.Size = new System.Drawing.Size(720, 2);
            this.btnLine1.TabIndex = 15;
            this.btnLine1.UseVisualStyleBackColor = false;
            // 
            // lblTestLed
            // 
            this.lblTestLed.AutoSize = true;
            this.lblTestLed.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestLed.Location = new System.Drawing.Point(19, 458);
            this.lblTestLed.Name = "lblTestLed";
            this.lblTestLed.Size = new System.Drawing.Size(50, 27);
            this.lblTestLed.TabIndex = 14;
            this.lblTestLed.Text = "LED";
            // 
            // lblTestTxRxP3
            // 
            this.lblTestTxRxP3.AutoSize = true;
            this.lblTestTxRxP3.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestTxRxP3.Location = new System.Drawing.Point(19, 417);
            this.lblTestTxRxP3.Name = "lblTestTxRxP3";
            this.lblTestTxRxP3.Size = new System.Drawing.Size(111, 27);
            this.lblTestTxRxP3.TabIndex = 13;
            this.lblTestTxRxP3.Text = "TX/RX(P3)";
            // 
            // lblTestControlBP10
            // 
            this.lblTestControlBP10.AutoSize = true;
            this.lblTestControlBP10.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestControlBP10.Location = new System.Drawing.Point(19, 377);
            this.lblTestControlBP10.Name = "lblTestControlBP10";
            this.lblTestControlBP10.Size = new System.Drawing.Size(159, 27);
            this.lblTestControlBP10.TabIndex = 12;
            this.lblTestControlBP10.Text = "Control-B(P10)";
            // 
            // lblTestControlAP9
            // 
            this.lblTestControlAP9.AutoSize = true;
            this.lblTestControlAP9.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestControlAP9.Location = new System.Drawing.Point(19, 338);
            this.lblTestControlAP9.Name = "lblTestControlAP9";
            this.lblTestControlAP9.Size = new System.Drawing.Size(149, 27);
            this.lblTestControlAP9.TabIndex = 11;
            this.lblTestControlAP9.Text = "Control-A(P9)";
            // 
            // lblTest485CommunicationP5
            // 
            this.lblTest485CommunicationP5.AutoSize = true;
            this.lblTest485CommunicationP5.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTest485CommunicationP5.Location = new System.Drawing.Point(19, 297);
            this.lblTest485CommunicationP5.Name = "lblTest485CommunicationP5";
            this.lblTest485CommunicationP5.Size = new System.Drawing.Size(129, 27);
            this.lblTest485CommunicationP5.TabIndex = 10;
            this.lblTest485CommunicationP5.Text = "485通訊(P5)";
            // 
            // lblTest485CommunicationP4
            // 
            this.lblTest485CommunicationP4.AutoSize = true;
            this.lblTest485CommunicationP4.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTest485CommunicationP4.Location = new System.Drawing.Point(19, 254);
            this.lblTest485CommunicationP4.Name = "lblTest485CommunicationP4";
            this.lblTest485CommunicationP4.Size = new System.Drawing.Size(129, 27);
            this.lblTest485CommunicationP4.TabIndex = 9;
            this.lblTest485CommunicationP4.Text = "485通訊(P4)";
            // 
            // lblTestModuleFirmwareVersion
            // 
            this.lblTestModuleFirmwareVersion.AutoSize = true;
            this.lblTestModuleFirmwareVersion.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestModuleFirmwareVersion.Location = new System.Drawing.Point(19, 212);
            this.lblTestModuleFirmwareVersion.Name = "lblTestModuleFirmwareVersion";
            this.lblTestModuleFirmwareVersion.Size = new System.Drawing.Size(138, 27);
            this.lblTestModuleFirmwareVersion.TabIndex = 8;
            this.lblTestModuleFirmwareVersion.Text = "模組韌體版本";
            // 
            // lblTestResultSW2
            // 
            this.lblTestResultSW2.AutoSize = true;
            this.lblTestResultSW2.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestResultSW2.Location = new System.Drawing.Point(19, 139);
            this.lblTestResultSW2.Name = "lblTestResultSW2";
            this.lblTestResultSW2.Size = new System.Drawing.Size(57, 27);
            this.lblTestResultSW2.TabIndex = 7;
            this.lblTestResultSW2.Text = "SW2";
            // 
            // lblTestSW1
            // 
            this.lblTestSW1.AutoSize = true;
            this.lblTestSW1.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestSW1.Location = new System.Drawing.Point(19, 60);
            this.lblTestSW1.Name = "lblTestSW1";
            this.lblTestSW1.Size = new System.Drawing.Size(57, 27);
            this.lblTestSW1.TabIndex = 6;
            this.lblTestSW1.Text = "SW1";
            // 
            // lblTestResultTitle
            // 
            this.lblTestResultTitle.AutoSize = true;
            this.lblTestResultTitle.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTestResultTitle.Location = new System.Drawing.Point(323, 18);
            this.lblTestResultTitle.Name = "lblTestResultTitle";
            this.lblTestResultTitle.Size = new System.Drawing.Size(109, 30);
            this.lblTestResultTitle.TabIndex = 5;
            this.lblTestResultTitle.Text = "檢測結果";
            // 
            // tmrNowDateTime
            // 
            this.tmrNowDateTime.Enabled = true;
            this.tmrNowDateTime.Interval = 1000;
            // 
            // tmrReciproca
            // 
            this.tmrReciproca.Interval = 1000;
            // 
            // tmrLedState
            // 
            this.tmrLedState.Interval = 500;
            // 
            // tmrDetection
            // 
            this.tmrDetection.Interval = 1000;
            // 
            // tmrLedStateFailDelay
            // 
            this.tmrLedStateFailDelay.Interval = 500;
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1248, 753);
            this.Controls.Add(this.grbArea6);
            this.Controls.Add(this.grbArea5);
            this.Controls.Add(this.grbArea4);
            this.Controls.Add(this.grbArea3);
            this.Controls.Add(this.grbArea2);
            this.Controls.Add(this.mns1);
            this.MainMenuStrip = this.mns1;
            this.Name = "formMain";
            this.Text = "彬騰檢測系統";
            this.mns1.ResumeLayout(false);
            this.mns1.PerformLayout();
            this.grbArea2.ResumeLayout(false);
            this.grbArea2.PerformLayout();
            this.grbArea3.ResumeLayout(false);
            this.grbArea4.ResumeLayout(false);
            this.grbArea4.PerformLayout();
            this.grbArea5.ResumeLayout(false);
            this.grbArea5.PerformLayout();
            this.grbArea6.ResumeLayout(false);
            this.grbArea6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mns1;
        private System.Windows.Forms.ToolStripMenuItem mnsSetting;
        private System.Windows.Forms.GroupBox grbArea2;
        private System.Windows.Forms.GroupBox grbArea3;
        private System.Windows.Forms.GroupBox grbArea4;
        private System.Windows.Forms.Label lblLedTest;
        private System.Windows.Forms.GroupBox grbArea5;
        private System.Windows.Forms.GroupBox grbArea6;
        private System.Windows.Forms.Label lblNowTimeTitle;
        private System.Windows.Forms.Label lblModelTitle;
        private System.Windows.Forms.Label lblInspectorTitle;
        private System.Windows.Forms.Label lblSerialNumberTitle;
        private System.Windows.Forms.ComboBox cboModel;
        private System.Windows.Forms.TextBox tbxSerialNumber;
        private System.Windows.Forms.Button btnSkipToNextTest;
        private System.Windows.Forms.Button btnDetection;
        private System.Windows.Forms.Button btnLedNG;
        private System.Windows.Forms.Button btnLedOK;
        private System.Windows.Forms.Label lblNowDateTime;
        private System.Windows.Forms.TextBox tbxInspector;
        private System.Windows.Forms.Label lblDetectionProcessDescriptionTitle;
        private System.Windows.Forms.Label lblReciprocal;
        private System.Windows.Forms.Label lblDetectionProcessDescription;
        private System.Windows.Forms.Label lblTestResultTitle;
        private System.Windows.Forms.Label lblTestLed;
        private System.Windows.Forms.Label lblTestTxRxP3;
        private System.Windows.Forms.Label lblTestControlBP10;
        private System.Windows.Forms.Label lblTestControlAP9;
        private System.Windows.Forms.Label lblTest485CommunicationP5;
        private System.Windows.Forms.Label lblTest485CommunicationP4;
        private System.Windows.Forms.Label lblTestModuleFirmwareVersion;
        private System.Windows.Forms.Label lblTestResultSW2;
        private System.Windows.Forms.Label lblTestSW1;
        private System.Windows.Forms.Button btnLine1;
        private System.Windows.Forms.Button btnLine8;
        private System.Windows.Forms.Button btnLine7;
        private System.Windows.Forms.Button btnLine6;
        private System.Windows.Forms.Button btnLine5;
        private System.Windows.Forms.Button btnLine4;
        private System.Windows.Forms.Button btnLine3;
        private System.Windows.Forms.Button btnLine2;
        private System.Windows.Forms.Label lblTestSW1_6V;
        private System.Windows.Forms.Label lblTestSW1_5V;
        private System.Windows.Forms.Label lblTestSW1_4V;
        private System.Windows.Forms.Label lblTestSW1_3V;
        private System.Windows.Forms.Label lblTestSW1_2V;
        private System.Windows.Forms.Label lblTestSW1_6;
        private System.Windows.Forms.Label lblTestSW1_5;
        private System.Windows.Forms.Label lblTestSW1_4;
        private System.Windows.Forms.Label lblTestSW1_3;
        private System.Windows.Forms.Label lblTestSW1_2;
        private System.Windows.Forms.Label lblTestSW1_1V;
        private System.Windows.Forms.Label lblTestSW1_1;
        private System.Windows.Forms.Label lblTestSW2_2V;
        private System.Windows.Forms.Label lblTestSW2_2;
        private System.Windows.Forms.Label lblTestSW2_1V;
        private System.Windows.Forms.Label lblTestSW2_1;
        private System.Windows.Forms.Label lblTestLedResult;
        private System.Windows.Forms.Label lblTestTxRxP3Result;
        private System.Windows.Forms.Label lblTestControlBP10Result;
        private System.Windows.Forms.Label lblTestControlAP9Result;
        private System.Windows.Forms.Label lblTest485CommunicationP5Result;
        private System.Windows.Forms.Label lblTest485CommunicationP4Result;
        private System.Windows.Forms.Label lblTestModuleFirmwareVersionResult;
        private System.Windows.Forms.Label lblTestSW2Result;
        private System.Windows.Forms.Label lblTestSW1Result;
        private System.Windows.Forms.Timer tmrNowDateTime;
        private System.Windows.Forms.Timer tmrReciproca;
        private System.Windows.Forms.Timer tmrLedState;
        private System.Windows.Forms.Timer tmrDetection;
        private System.Windows.Forms.Timer tmrLedStateFailDelay;
    }
}

