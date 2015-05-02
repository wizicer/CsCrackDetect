namespace CrackDetect
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picMain = new CrackDetect.EnhancedPictureBox();
            this.btnProcess = new System.Windows.Forms.Button();
            this.btnThinner = new System.Windows.Forms.Button();
            this.btnGauss = new System.Windows.Forms.Button();
            this.btnReloadBinaryPic = new System.Windows.Forms.Button();
            this.btnMarkLineEnd = new System.Windows.Forms.Button();
            this.btnJoin = new System.Windows.Forms.Button();
            this.numShortDistance = new System.Windows.Forms.NumericUpDown();
            this.btnWipeSmallLine = new System.Windows.Forms.Button();
            this.numThinnerTimes = new System.Windows.Forms.NumericUpDown();
            this.numMinLineDistance = new System.Windows.Forms.NumericUpDown();
            this.btnMarkCross = new System.Windows.Forms.Button();
            this.stripStatus = new System.Windows.Forms.StatusStrip();
            this.lblPosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblZoomLevel = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnIntegration = new System.Windows.Forms.Button();
            this.numMaxLineFluctuate = new System.Windows.Forms.NumericUpDown();
            this.numPrecision = new System.Windows.Forms.NumericUpDown();
            this.btnGetLineWidth = new System.Windows.Forms.Button();
            this.btnNewThinner = new System.Windows.Forms.Button();
            this.btnBinary = new System.Windows.Forms.Button();
            this.numBrightThreshold = new System.Windows.Forms.NumericUpDown();
            this.btnGradient = new System.Windows.Forms.Button();
            this.numGradientLevel = new System.Windows.Forms.NumericUpDown();
            this.btnErosion = new System.Windows.Forms.Button();
            this.btnDilation = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnReloadOriginPic = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnReloadLast = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.numMaxHeight = new System.Windows.Forms.NumericUpDown();
            this.numMaxWidth = new System.Windows.Forms.NumericUpDown();
            this.btnJoinPicture = new System.Windows.Forms.Button();
            this.btnOutput = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numShortDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThinnerTimes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinLineDistance)).BeginInit();
            this.stripStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxLineFluctuate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBrightThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGradientLevel)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // picMain
            // 
            this.picMain.Image = null;
            this.picMain.Location = new System.Drawing.Point(12, 12);
            this.picMain.Name = "picMain";
            this.picMain.Percent = 0F;
            this.picMain.Size = new System.Drawing.Size(541, 443);
            this.picMain.TabIndex = 0;
            this.picMain.TabStop = false;
            this.picMain.MouseLeave += new System.EventHandler(this.picMain_MouseLeave);
            this.picMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picMain_MouseMove);
            this.picMain.Click += new System.EventHandler(this.picMain_Click);
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(6, 20);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(75, 27);
            this.btnProcess.TabIndex = 1;
            this.btnProcess.Text = "综合处理";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // btnThinner
            // 
            this.btnThinner.Location = new System.Drawing.Point(6, 53);
            this.btnThinner.Name = "btnThinner";
            this.btnThinner.Size = new System.Drawing.Size(75, 23);
            this.btnThinner.TabIndex = 2;
            this.btnThinner.Text = "细化";
            this.btnThinner.UseVisualStyleBackColor = true;
            this.btnThinner.Click += new System.EventHandler(this.btnThinner_Click);
            // 
            // btnGauss
            // 
            this.btnGauss.Location = new System.Drawing.Point(6, 20);
            this.btnGauss.Name = "btnGauss";
            this.btnGauss.Size = new System.Drawing.Size(28, 23);
            this.btnGauss.TabIndex = 3;
            this.btnGauss.Text = "高斯滤波";
            this.btnGauss.UseVisualStyleBackColor = true;
            this.btnGauss.Click += new System.EventHandler(this.btnGauss_Click);
            // 
            // btnReloadBinaryPic
            // 
            this.btnReloadBinaryPic.Location = new System.Drawing.Point(87, 20);
            this.btnReloadBinaryPic.Name = "btnReloadBinaryPic";
            this.btnReloadBinaryPic.Size = new System.Drawing.Size(75, 23);
            this.btnReloadBinaryPic.TabIndex = 4;
            this.btnReloadBinaryPic.Text = "Reload";
            this.btnReloadBinaryPic.UseVisualStyleBackColor = true;
            this.btnReloadBinaryPic.Click += new System.EventHandler(this.btnReloadBinaryPic_Click);
            // 
            // btnMarkLineEnd
            // 
            this.btnMarkLineEnd.Location = new System.Drawing.Point(6, 82);
            this.btnMarkLineEnd.Name = "btnMarkLineEnd";
            this.btnMarkLineEnd.Size = new System.Drawing.Size(75, 23);
            this.btnMarkLineEnd.TabIndex = 5;
            this.btnMarkLineEnd.Text = "LineEnds";
            this.btnMarkLineEnd.UseVisualStyleBackColor = true;
            this.btnMarkLineEnd.Click += new System.EventHandler(this.btnMarkLineEnd_Click);
            // 
            // btnJoin
            // 
            this.btnJoin.Location = new System.Drawing.Point(6, 111);
            this.btnJoin.Name = "btnJoin";
            this.btnJoin.Size = new System.Drawing.Size(75, 23);
            this.btnJoin.TabIndex = 6;
            this.btnJoin.Text = "JoinShorts";
            this.btnJoin.UseVisualStyleBackColor = true;
            this.btnJoin.Click += new System.EventHandler(this.btnJoin_Click);
            // 
            // numShortDistance
            // 
            this.numShortDistance.Location = new System.Drawing.Point(87, 113);
            this.numShortDistance.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numShortDistance.Name = "numShortDistance";
            this.numShortDistance.Size = new System.Drawing.Size(38, 21);
            this.numShortDistance.TabIndex = 7;
            this.numShortDistance.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // btnWipeSmallLine
            // 
            this.btnWipeSmallLine.Location = new System.Drawing.Point(6, 140);
            this.btnWipeSmallLine.Name = "btnWipeSmallLine";
            this.btnWipeSmallLine.Size = new System.Drawing.Size(75, 23);
            this.btnWipeSmallLine.TabIndex = 8;
            this.btnWipeSmallLine.Text = "Wipe";
            this.btnWipeSmallLine.UseVisualStyleBackColor = true;
            this.btnWipeSmallLine.Click += new System.EventHandler(this.btnWipeSmallLine_Click);
            // 
            // numThinnerTimes
            // 
            this.numThinnerTimes.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numThinnerTimes.Location = new System.Drawing.Point(87, 55);
            this.numThinnerTimes.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numThinnerTimes.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numThinnerTimes.Name = "numThinnerTimes";
            this.numThinnerTimes.Size = new System.Drawing.Size(38, 21);
            this.numThinnerTimes.TabIndex = 9;
            this.numThinnerTimes.Value = new decimal(new int[] {
            18,
            0,
            0,
            0});
            // 
            // numMinLineDistance
            // 
            this.numMinLineDistance.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numMinLineDistance.Location = new System.Drawing.Point(87, 140);
            this.numMinLineDistance.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numMinLineDistance.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numMinLineDistance.Name = "numMinLineDistance";
            this.numMinLineDistance.Size = new System.Drawing.Size(38, 21);
            this.numMinLineDistance.TabIndex = 10;
            this.numMinLineDistance.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // btnMarkCross
            // 
            this.btnMarkCross.Location = new System.Drawing.Point(87, 82);
            this.btnMarkCross.Name = "btnMarkCross";
            this.btnMarkCross.Size = new System.Drawing.Size(75, 23);
            this.btnMarkCross.TabIndex = 11;
            this.btnMarkCross.Text = "Cross";
            this.btnMarkCross.UseVisualStyleBackColor = true;
            this.btnMarkCross.Click += new System.EventHandler(this.btnMarkCross_Click);
            // 
            // stripStatus
            // 
            this.stripStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblPosition,
            this.lblZoomLevel});
            this.stripStatus.Location = new System.Drawing.Point(0, 468);
            this.stripStatus.Name = "stripStatus";
            this.stripStatus.Size = new System.Drawing.Size(772, 22);
            this.stripStatus.TabIndex = 12;
            this.stripStatus.Text = "statusStrip1";
            // 
            // lblPosition
            // 
            this.lblPosition.AutoSize = false;
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(100, 17);
            this.lblPosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblZoomLevel
            // 
            this.lblZoomLevel.AutoSize = false;
            this.lblZoomLevel.Name = "lblZoomLevel";
            this.lblZoomLevel.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.lblZoomLevel.Size = new System.Drawing.Size(60, 17);
            this.lblZoomLevel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnIntegration
            // 
            this.btnIntegration.Location = new System.Drawing.Point(6, 20);
            this.btnIntegration.Name = "btnIntegration";
            this.btnIntegration.Size = new System.Drawing.Size(75, 23);
            this.btnIntegration.TabIndex = 13;
            this.btnIntegration.Text = "integration";
            this.btnIntegration.UseVisualStyleBackColor = true;
            this.btnIntegration.Click += new System.EventHandler(this.btnIntegration_Click);
            // 
            // numMaxLineFluctuate
            // 
            this.numMaxLineFluctuate.Location = new System.Drawing.Point(87, 20);
            this.numMaxLineFluctuate.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numMaxLineFluctuate.Name = "numMaxLineFluctuate";
            this.numMaxLineFluctuate.Size = new System.Drawing.Size(38, 21);
            this.numMaxLineFluctuate.TabIndex = 14;
            this.numMaxLineFluctuate.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // numPrecision
            // 
            this.numPrecision.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numPrecision.Location = new System.Drawing.Point(131, 20);
            this.numPrecision.Name = "numPrecision";
            this.numPrecision.Size = new System.Drawing.Size(38, 21);
            this.numPrecision.TabIndex = 15;
            this.numPrecision.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // btnGetLineWidth
            // 
            this.btnGetLineWidth.Location = new System.Drawing.Point(6, 49);
            this.btnGetLineWidth.Name = "btnGetLineWidth";
            this.btnGetLineWidth.Size = new System.Drawing.Size(75, 23);
            this.btnGetLineWidth.TabIndex = 16;
            this.btnGetLineWidth.Text = "LineWidth";
            this.btnGetLineWidth.UseVisualStyleBackColor = true;
            this.btnGetLineWidth.Click += new System.EventHandler(this.btnGetLineWidth_Click);
            // 
            // btnNewThinner
            // 
            this.btnNewThinner.Location = new System.Drawing.Point(53, 20);
            this.btnNewThinner.Name = "btnNewThinner";
            this.btnNewThinner.Size = new System.Drawing.Size(28, 23);
            this.btnNewThinner.TabIndex = 17;
            this.btnNewThinner.Text = "newThinner";
            this.btnNewThinner.UseVisualStyleBackColor = true;
            this.btnNewThinner.Click += new System.EventHandler(this.btnNewThinner_Click);
            // 
            // btnBinary
            // 
            this.btnBinary.Location = new System.Drawing.Point(6, 20);
            this.btnBinary.Name = "btnBinary";
            this.btnBinary.Size = new System.Drawing.Size(75, 23);
            this.btnBinary.TabIndex = 18;
            this.btnBinary.Text = "binary";
            this.btnBinary.UseVisualStyleBackColor = true;
            this.btnBinary.Click += new System.EventHandler(this.btnBinary_Click);
            // 
            // numBrightThreshold
            // 
            this.numBrightThreshold.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numBrightThreshold.Location = new System.Drawing.Point(87, 49);
            this.numBrightThreshold.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numBrightThreshold.Name = "numBrightThreshold";
            this.numBrightThreshold.Size = new System.Drawing.Size(38, 21);
            this.numBrightThreshold.TabIndex = 19;
            this.numBrightThreshold.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // btnGradient
            // 
            this.btnGradient.Location = new System.Drawing.Point(6, 49);
            this.btnGradient.Name = "btnGradient";
            this.btnGradient.Size = new System.Drawing.Size(75, 23);
            this.btnGradient.TabIndex = 20;
            this.btnGradient.Text = "gradient";
            this.btnGradient.UseVisualStyleBackColor = true;
            this.btnGradient.Click += new System.EventHandler(this.btnGradient_Click);
            // 
            // numGradientLevel
            // 
            this.numGradientLevel.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numGradientLevel.Location = new System.Drawing.Point(131, 49);
            this.numGradientLevel.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numGradientLevel.Name = "numGradientLevel";
            this.numGradientLevel.Size = new System.Drawing.Size(38, 21);
            this.numGradientLevel.TabIndex = 21;
            this.numGradientLevel.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // btnErosion
            // 
            this.btnErosion.Location = new System.Drawing.Point(6, 78);
            this.btnErosion.Name = "btnErosion";
            this.btnErosion.Size = new System.Drawing.Size(75, 23);
            this.btnErosion.TabIndex = 22;
            this.btnErosion.Text = "erosion";
            this.btnErosion.UseVisualStyleBackColor = true;
            this.btnErosion.Click += new System.EventHandler(this.btnErosion_Click);
            // 
            // btnDilation
            // 
            this.btnDilation.Location = new System.Drawing.Point(87, 76);
            this.btnDilation.Name = "btnDilation";
            this.btnDilation.Size = new System.Drawing.Size(75, 23);
            this.btnDilation.TabIndex = 23;
            this.btnDilation.Text = "dilation";
            this.btnDilation.UseVisualStyleBackColor = true;
            this.btnDilation.Click += new System.EventHandler(this.btnDilation_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Controls.Add(this.groupBox4);
            this.flowLayoutPanel1.Controls.Add(this.groupBox3);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(6, 6);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(187, 430);
            this.flowLayoutPanel1.TabIndex = 24;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnReloadOriginPic);
            this.groupBox1.Controls.Add(this.btnBinary);
            this.groupBox1.Controls.Add(this.btnDilation);
            this.groupBox1.Controls.Add(this.numBrightThreshold);
            this.groupBox1.Controls.Add(this.btnErosion);
            this.groupBox1.Controls.Add(this.btnGradient);
            this.groupBox1.Controls.Add(this.numGradientLevel);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(179, 108);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "二值预处理";
            // 
            // btnReloadOriginPic
            // 
            this.btnReloadOriginPic.Location = new System.Drawing.Point(87, 20);
            this.btnReloadOriginPic.Name = "btnReloadOriginPic";
            this.btnReloadOriginPic.Size = new System.Drawing.Size(75, 23);
            this.btnReloadOriginPic.TabIndex = 24;
            this.btnReloadOriginPic.Text = "Reload";
            this.btnReloadOriginPic.UseVisualStyleBackColor = true;
            this.btnReloadOriginPic.Click += new System.EventHandler(this.btnReloadOriginPic_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnProcess);
            this.groupBox2.Controls.Add(this.btnThinner);
            this.groupBox2.Controls.Add(this.btnReloadBinaryPic);
            this.groupBox2.Controls.Add(this.btnMarkLineEnd);
            this.groupBox2.Controls.Add(this.btnJoin);
            this.groupBox2.Controls.Add(this.numShortDistance);
            this.groupBox2.Controls.Add(this.btnMarkCross);
            this.groupBox2.Controls.Add(this.btnWipeSmallLine);
            this.groupBox2.Controls.Add(this.numMinLineDistance);
            this.groupBox2.Controls.Add(this.numThinnerTimes);
            this.groupBox2.Location = new System.Drawing.Point(3, 117);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(179, 169);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "拟合预处理";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnReloadLast);
            this.groupBox4.Controls.Add(this.btnIntegration);
            this.groupBox4.Controls.Add(this.numMaxLineFluctuate);
            this.groupBox4.Controls.Add(this.btnGetLineWidth);
            this.groupBox4.Controls.Add(this.numPrecision);
            this.groupBox4.Location = new System.Drawing.Point(3, 292);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(179, 78);
            this.groupBox4.TabIndex = 26;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "拟合";
            // 
            // btnReloadLast
            // 
            this.btnReloadLast.Location = new System.Drawing.Point(87, 49);
            this.btnReloadLast.Name = "btnReloadLast";
            this.btnReloadLast.Size = new System.Drawing.Size(75, 23);
            this.btnReloadLast.TabIndex = 17;
            this.btnReloadLast.Text = "Reload";
            this.btnReloadLast.UseVisualStyleBackColor = true;
            this.btnReloadLast.Click += new System.EventHandler(this.btnReloadLast_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnOutput);
            this.groupBox3.Controls.Add(this.btnGauss);
            this.groupBox3.Controls.Add(this.btnNewThinner);
            this.groupBox3.Location = new System.Drawing.Point(3, 376);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(179, 49);
            this.groupBox3.TabIndex = 25;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "算法研究";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(559, 9);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(206, 456);
            this.tabControl1.TabIndex = 25;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.flowLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(198, 431);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "拟合";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.numMaxHeight);
            this.tabPage2.Controls.Add(this.numMaxWidth);
            this.tabPage2.Controls.Add(this.btnJoinPicture);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(198, 431);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "拼接";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // numMaxHeight
            // 
            this.numMaxHeight.Location = new System.Drawing.Point(133, 9);
            this.numMaxHeight.Name = "numMaxHeight";
            this.numMaxHeight.Size = new System.Drawing.Size(40, 21);
            this.numMaxHeight.TabIndex = 2;
            this.numMaxHeight.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // numMaxWidth
            // 
            this.numMaxWidth.Location = new System.Drawing.Point(87, 9);
            this.numMaxWidth.Name = "numMaxWidth";
            this.numMaxWidth.Size = new System.Drawing.Size(40, 21);
            this.numMaxWidth.TabIndex = 1;
            this.numMaxWidth.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // btnJoinPicture
            // 
            this.btnJoinPicture.Location = new System.Drawing.Point(6, 6);
            this.btnJoinPicture.Name = "btnJoinPicture";
            this.btnJoinPicture.Size = new System.Drawing.Size(75, 23);
            this.btnJoinPicture.TabIndex = 0;
            this.btnJoinPicture.Text = "join";
            this.btnJoinPicture.UseVisualStyleBackColor = true;
            this.btnJoinPicture.Click += new System.EventHandler(this.btnJoinPicture_Click);
            // 
            // btnOutput
            // 
            this.btnOutput.Location = new System.Drawing.Point(87, 20);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(75, 23);
            this.btnOutput.TabIndex = 18;
            this.btnOutput.Text = "button1";
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // frmMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 490);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.stripStatus);
            this.Controls.Add(this.picMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "裂纹检测测试程序";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.frmMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.frmMain_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numShortDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThinnerTimes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinLineDistance)).EndInit();
            this.stripStatus.ResumeLayout(false);
            this.stripStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxLineFluctuate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBrightThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGradientLevel)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numMaxHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EnhancedPictureBox picMain;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Button btnThinner;
        private System.Windows.Forms.Button btnGauss;
        private System.Windows.Forms.Button btnReloadBinaryPic;
        private System.Windows.Forms.Button btnMarkLineEnd;
        private System.Windows.Forms.Button btnJoin;
        private System.Windows.Forms.NumericUpDown numShortDistance;
        private System.Windows.Forms.Button btnWipeSmallLine;
        private System.Windows.Forms.NumericUpDown numThinnerTimes;
        private System.Windows.Forms.NumericUpDown numMinLineDistance;
        private System.Windows.Forms.Button btnMarkCross;
        private System.Windows.Forms.StatusStrip stripStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblPosition;
        private System.Windows.Forms.Button btnIntegration;
        private System.Windows.Forms.NumericUpDown numMaxLineFluctuate;
        private System.Windows.Forms.NumericUpDown numPrecision;
        private System.Windows.Forms.Button btnGetLineWidth;
        private System.Windows.Forms.Button btnNewThinner;
        private System.Windows.Forms.Button btnBinary;
        private System.Windows.Forms.NumericUpDown numBrightThreshold;
        private System.Windows.Forms.Button btnGradient;
        private System.Windows.Forms.NumericUpDown numGradientLevel;
        private System.Windows.Forms.Button btnErosion;
        private System.Windows.Forms.Button btnDilation;
        private System.Windows.Forms.ToolStripStatusLabel lblZoomLevel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnReloadOriginPic;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnReloadLast;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnJoinPicture;
        private System.Windows.Forms.NumericUpDown numMaxHeight;
        private System.Windows.Forms.NumericUpDown numMaxWidth;
        private System.Windows.Forms.Button btnOutput;
    }
}

