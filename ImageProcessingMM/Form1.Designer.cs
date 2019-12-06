namespace ImageProcessingMM
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.ImageBox = new System.Windows.Forms.PictureBox();
            this.OpenFile = new System.Windows.Forms.Button();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.ChartGroup = new System.Windows.Forms.GroupBox();
            this.RedChannel = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ShowCharts = new System.Windows.Forms.Button();
            this.GreyBtn = new System.Windows.Forms.Button();
            this.ImagePostBox = new System.Windows.Forms.PictureBox();
            this.NormalizeBtn = new System.Windows.Forms.Button();
            this.ShowPostCharts = new System.Windows.Forms.Button();
            this.ScretchBtn = new System.Windows.Forms.Button();
            this.NeagteBtn = new System.Windows.Forms.Button();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.trackBarUpDown = new System.Windows.Forms.NumericUpDown();
            this.TresholdBtn = new System.Windows.Forms.Button();
            this.lowThershold = new System.Windows.Forms.NumericUpDown();
            this.highTreshold = new System.Windows.Forms.NumericUpDown();
            this.tresholdGray = new System.Windows.Forms.Button();
            this.tresholdNeg = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.button1 = new System.Windows.Forms.Button();
            this.Posterization = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.MASK = new System.Windows.Forms.TextBox();
            this.Smoth014 = new System.Windows.Forms.Button();
            this.Smoth111 = new System.Windows.Forms.Button();
            this.Smoth121 = new System.Windows.Forms.Button();
            this.Smoth1k = new System.Windows.Forms.Button();
            this.Smoth0k = new System.Windows.Forms.Button();
            this.KValue = new System.Windows.Forms.NumericUpDown();
            this.K = new System.Windows.Forms.Label();
            this.DetMin14 = new System.Windows.Forms.Button();
            this.DetMin18 = new System.Windows.Forms.Button();
            this.Det1Min24 = new System.Windows.Forms.Button();
            this.DetMin19 = new System.Windows.Forms.Button();
            this.DetMin15 = new System.Windows.Forms.Button();
            this.DetMin215 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.ChartGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RedChannel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePostBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowThershold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.highTreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KValue)).BeginInit();
            this.SuspendLayout();
            // 
            // ImageBox
            // 
            this.ImageBox.Location = new System.Drawing.Point(12, 12);
            this.ImageBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ImageBox.Name = "ImageBox";
            this.ImageBox.Size = new System.Drawing.Size(443, 437);
            this.ImageBox.TabIndex = 0;
            this.ImageBox.TabStop = false;
            // 
            // OpenFile
            // 
            this.OpenFile.Location = new System.Drawing.Point(1096, 10);
            this.OpenFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OpenFile.Name = "OpenFile";
            this.OpenFile.Size = new System.Drawing.Size(96, 38);
            this.OpenFile.TabIndex = 1;
            this.OpenFile.Text = "Load image";
            this.OpenFile.UseVisualStyleBackColor = true;
            this.OpenFile.Click += new System.EventHandler(this.button1_Click);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // ChartGroup
            // 
            this.ChartGroup.Controls.Add(this.RedChannel);
            this.ChartGroup.Location = new System.Drawing.Point(12, 654);
            this.ChartGroup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChartGroup.Name = "ChartGroup";
            this.ChartGroup.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChartGroup.Size = new System.Drawing.Size(1187, 370);
            this.ChartGroup.TabIndex = 2;
            this.ChartGroup.TabStop = false;
            this.ChartGroup.Text = "ChartGroup";
            this.ChartGroup.Visible = false;
            // 
            // RedChannel
            // 
            this.RedChannel.BorderlineColor = System.Drawing.Color.Red;
            chartArea4.Name = "ChartArea1";
            this.RedChannel.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.RedChannel.Legends.Add(legend4);
            this.RedChannel.Location = new System.Drawing.Point(0, 71);
            this.RedChannel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RedChannel.Name = "RedChannel";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.RedChannel.Series.Add(series4);
            this.RedChannel.Size = new System.Drawing.Size(1173, 421);
            this.RedChannel.TabIndex = 0;
            this.RedChannel.Text = "RedChannel";
            this.RedChannel.Click += new System.EventHandler(this.RedChannel_Click);
            // 
            // ShowCharts
            // 
            this.ShowCharts.Location = new System.Drawing.Point(1095, 54);
            this.ShowCharts.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ShowCharts.Name = "ShowCharts";
            this.ShowCharts.Size = new System.Drawing.Size(97, 65);
            this.ShowCharts.TabIndex = 3;
            this.ShowCharts.Text = "Show channel charts";
            this.ShowCharts.UseVisualStyleBackColor = true;
            this.ShowCharts.Click += new System.EventHandler(this.ShowCharts_Click);
            // 
            // GreyBtn
            // 
            this.GreyBtn.Location = new System.Drawing.Point(993, 10);
            this.GreyBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GreyBtn.Name = "GreyBtn";
            this.GreyBtn.Size = new System.Drawing.Size(97, 38);
            this.GreyBtn.TabIndex = 4;
            this.GreyBtn.Text = "Grey";
            this.GreyBtn.UseVisualStyleBackColor = true;
            this.GreyBtn.Click += new System.EventHandler(this.GreyBtn_Click);
            // 
            // ImagePostBox
            // 
            this.ImagePostBox.Location = new System.Drawing.Point(545, 12);
            this.ImagePostBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ImagePostBox.Name = "ImagePostBox";
            this.ImagePostBox.Size = new System.Drawing.Size(443, 437);
            this.ImagePostBox.TabIndex = 5;
            this.ImagePostBox.TabStop = false;
            // 
            // NormalizeBtn
            // 
            this.NormalizeBtn.Location = new System.Drawing.Point(993, 54);
            this.NormalizeBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NormalizeBtn.Name = "NormalizeBtn";
            this.NormalizeBtn.Size = new System.Drawing.Size(97, 65);
            this.NormalizeBtn.TabIndex = 6;
            this.NormalizeBtn.Text = "Normalize";
            this.NormalizeBtn.UseVisualStyleBackColor = true;
            this.NormalizeBtn.Click += new System.EventHandler(this.NormalizeBtn_Click);
            // 
            // ShowPostCharts
            // 
            this.ShowPostCharts.Location = new System.Drawing.Point(1096, 126);
            this.ShowPostCharts.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ShowPostCharts.Name = "ShowPostCharts";
            this.ShowPostCharts.Size = new System.Drawing.Size(96, 69);
            this.ShowPostCharts.TabIndex = 7;
            this.ShowPostCharts.Text = "Show Post Channel Charts";
            this.ShowPostCharts.UseVisualStyleBackColor = true;
            this.ShowPostCharts.Click += new System.EventHandler(this.ShowPostCharts_Click);
            // 
            // ScretchBtn
            // 
            this.ScretchBtn.Location = new System.Drawing.Point(995, 126);
            this.ScretchBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ScretchBtn.Name = "ScretchBtn";
            this.ScretchBtn.Size = new System.Drawing.Size(96, 68);
            this.ScretchBtn.TabIndex = 8;
            this.ScretchBtn.Text = "Scretch";
            this.ScretchBtn.UseVisualStyleBackColor = true;
            this.ScretchBtn.Click += new System.EventHandler(this.ScretchBtn_Click);
            // 
            // NeagteBtn
            // 
            this.NeagteBtn.Location = new System.Drawing.Point(995, 199);
            this.NeagteBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NeagteBtn.Name = "NeagteBtn";
            this.NeagteBtn.Size = new System.Drawing.Size(96, 68);
            this.NeagteBtn.TabIndex = 9;
            this.NeagteBtn.Text = "Negate";
            this.NeagteBtn.UseVisualStyleBackColor = true;
            this.NeagteBtn.Click += new System.EventHandler(this.NeagteBtn_Click);
            // 
            // trackBar
            // 
            this.trackBar.Location = new System.Drawing.Point(1451, 10);
            this.trackBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBar.Maximum = 255;
            this.trackBar.Name = "trackBar";
            this.trackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar.Size = new System.Drawing.Size(56, 256);
            this.trackBar.TabIndex = 10;
            this.trackBar.Scroll += new System.EventHandler(this.trackBar_Scroll);
            // 
            // trackBarUpDown
            // 
            this.trackBarUpDown.Location = new System.Drawing.Point(1488, 10);
            this.trackBarUpDown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBarUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.trackBarUpDown.Name = "trackBarUpDown";
            this.trackBarUpDown.Size = new System.Drawing.Size(57, 22);
            this.trackBarUpDown.TabIndex = 12;
            this.trackBarUpDown.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // TresholdBtn
            // 
            this.TresholdBtn.Location = new System.Drawing.Point(1095, 199);
            this.TresholdBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TresholdBtn.Name = "TresholdBtn";
            this.TresholdBtn.Size = new System.Drawing.Size(97, 68);
            this.TresholdBtn.TabIndex = 13;
            this.TresholdBtn.Text = "Treshold";
            this.TresholdBtn.UseVisualStyleBackColor = true;
            this.TresholdBtn.Click += new System.EventHandler(this.TresholdBtn_Click);
            // 
            // lowThershold
            // 
            this.lowThershold.Location = new System.Drawing.Point(1428, 271);
            this.lowThershold.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lowThershold.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.lowThershold.Name = "lowThershold";
            this.lowThershold.Size = new System.Drawing.Size(121, 22);
            this.lowThershold.TabIndex = 14;
            // 
            // highTreshold
            // 
            this.highTreshold.Location = new System.Drawing.Point(1428, 298);
            this.highTreshold.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.highTreshold.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.highTreshold.Name = "highTreshold";
            this.highTreshold.Size = new System.Drawing.Size(121, 22);
            this.highTreshold.TabIndex = 15;
            // 
            // tresholdGray
            // 
            this.tresholdGray.Location = new System.Drawing.Point(995, 274);
            this.tresholdGray.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tresholdGray.Name = "tresholdGray";
            this.tresholdGray.Size = new System.Drawing.Size(96, 68);
            this.tresholdGray.TabIndex = 16;
            this.tresholdGray.Text = "Treshold gray";
            this.tresholdGray.UseVisualStyleBackColor = true;
            this.tresholdGray.Click += new System.EventHandler(this.tresholdGray_Click);
            // 
            // tresholdNeg
            // 
            this.tresholdNeg.Location = new System.Drawing.Point(1095, 274);
            this.tresholdNeg.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tresholdNeg.Name = "tresholdNeg";
            this.tresholdNeg.Size = new System.Drawing.Size(97, 68);
            this.tresholdNeg.TabIndex = 17;
            this.tresholdNeg.Text = "Treshold neg";
            this.tresholdNeg.UseVisualStyleBackColor = true;
            this.tresholdNeg.Click += new System.EventHandler(this.tresholdNeg_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(993, 348);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 68);
            this.button1.TabIndex = 18;
            this.button1.Text = "Scretch with range";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Posterization
            // 
            this.Posterization.Location = new System.Drawing.Point(1096, 348);
            this.Posterization.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Posterization.Name = "Posterization";
            this.Posterization.Size = new System.Drawing.Size(96, 68);
            this.Posterization.TabIndex = 19;
            this.Posterization.Text = "Posterization";
            this.Posterization.UseVisualStyleBackColor = true;
            this.Posterization.Click += new System.EventHandler(this.Posterization_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(995, 422);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 60);
            this.button2.TabIndex = 20;
            this.button2.Text = "Mask";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // MASK
            // 
            this.MASK.Location = new System.Drawing.Point(728, 514);
            this.MASK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MASK.Name = "MASK";
            this.MASK.Size = new System.Drawing.Size(215, 22);
            this.MASK.TabIndex = 21;
            // 
            // Smoth014
            // 
            this.Smoth014.Location = new System.Drawing.Point(13, 459);
            this.Smoth014.Margin = new System.Windows.Forms.Padding(4);
            this.Smoth014.Name = "Smoth014";
            this.Smoth014.Size = new System.Drawing.Size(65, 80);
            this.Smoth014.TabIndex = 22;
            this.Smoth014.Text = "0 1 0  1 4 1  0 1 0";
            this.Smoth014.UseVisualStyleBackColor = true;
            this.Smoth014.Click += new System.EventHandler(this.Smoth014_Click);
            // 
            // Smoth111
            // 
            this.Smoth111.Location = new System.Drawing.Point(87, 459);
            this.Smoth111.Margin = new System.Windows.Forms.Padding(4);
            this.Smoth111.Name = "Smoth111";
            this.Smoth111.Size = new System.Drawing.Size(52, 80);
            this.Smoth111.TabIndex = 23;
            this.Smoth111.Text = "1 1 1 1 1 1 1 1 1";
            this.Smoth111.UseVisualStyleBackColor = true;
            this.Smoth111.Click += new System.EventHandler(this.Smoth111_Click);
            // 
            // Smoth121
            // 
            this.Smoth121.Location = new System.Drawing.Point(147, 459);
            this.Smoth121.Margin = new System.Windows.Forms.Padding(4);
            this.Smoth121.Name = "Smoth121";
            this.Smoth121.Size = new System.Drawing.Size(52, 80);
            this.Smoth121.TabIndex = 24;
            this.Smoth121.Text = "1 2 1 2 4 2 1 2 1";
            this.Smoth121.UseVisualStyleBackColor = true;
            this.Smoth121.Click += new System.EventHandler(this.Smoth121_Click);
            // 
            // Smoth1k
            // 
            this.Smoth1k.Location = new System.Drawing.Point(207, 459);
            this.Smoth1k.Margin = new System.Windows.Forms.Padding(4);
            this.Smoth1k.Name = "Smoth1k";
            this.Smoth1k.Size = new System.Drawing.Size(52, 80);
            this.Smoth1k.TabIndex = 25;
            this.Smoth1k.Text = "1 1 1 1 k 1 1 1 1";
            this.Smoth1k.UseVisualStyleBackColor = true;
            this.Smoth1k.Click += new System.EventHandler(this.Smoth1k_Click);
            // 
            // Smoth0k
            // 
            this.Smoth0k.Location = new System.Drawing.Point(267, 459);
            this.Smoth0k.Margin = new System.Windows.Forms.Padding(4);
            this.Smoth0k.Name = "Smoth0k";
            this.Smoth0k.Size = new System.Drawing.Size(52, 80);
            this.Smoth0k.TabIndex = 26;
            this.Smoth0k.Text = "0 1 0 1 k 1 0 1 0";
            this.Smoth0k.UseVisualStyleBackColor = true;
            this.Smoth0k.Click += new System.EventHandler(this.Smoth0k_Click);
            // 
            // KValue
            // 
            this.KValue.Location = new System.Drawing.Point(331, 482);
            this.KValue.Margin = new System.Windows.Forms.Padding(4);
            this.KValue.Name = "KValue";
            this.KValue.Size = new System.Drawing.Size(128, 22);
            this.KValue.TabIndex = 27;
            // 
            // K
            // 
            this.K.AutoSize = true;
            this.K.Location = new System.Drawing.Point(327, 463);
            this.K.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.K.Name = "K";
            this.K.Size = new System.Drawing.Size(68, 17);
            this.K.TabIndex = 28;
            this.K.Text = "K (0-100)";
            this.K.Click += new System.EventHandler(this.label1_Click);
            // 
            // DetMin14
            // 
            this.DetMin14.Location = new System.Drawing.Point(16, 546);
            this.DetMin14.Margin = new System.Windows.Forms.Padding(4);
            this.DetMin14.Name = "DetMin14";
            this.DetMin14.Size = new System.Drawing.Size(63, 76);
            this.DetMin14.TabIndex = 29;
            this.DetMin14.Text = "0 -1 0 -1 4 -1 0 -1 0";
            this.DetMin14.UseVisualStyleBackColor = true;
            this.DetMin14.Click += new System.EventHandler(this.DetMin14_Click);
            // 
            // DetMin18
            // 
            this.DetMin18.Location = new System.Drawing.Point(87, 546);
            this.DetMin18.Margin = new System.Windows.Forms.Padding(4);
            this.DetMin18.Name = "DetMin18";
            this.DetMin18.Size = new System.Drawing.Size(64, 76);
            this.DetMin18.TabIndex = 30;
            this.DetMin18.Text = "-1 -1 -1 -1 8 -1 -1 -1 -1";
            this.DetMin18.UseVisualStyleBackColor = true;
            this.DetMin18.Click += new System.EventHandler(this.DetMin18_Click);
            // 
            // Det1Min24
            // 
            this.Det1Min24.Location = new System.Drawing.Point(159, 546);
            this.Det1Min24.Margin = new System.Windows.Forms.Padding(4);
            this.Det1Min24.Name = "Det1Min24";
            this.Det1Min24.Size = new System.Drawing.Size(71, 76);
            this.Det1Min24.TabIndex = 31;
            this.Det1Min24.Text = "1 -2 1 -2 4 -2 1 -2 1";
            this.Det1Min24.UseVisualStyleBackColor = true;
            this.Det1Min24.Click += new System.EventHandler(this.Det1Min24_Click);
            // 
            // DetMin19
            // 
            this.DetMin19.Location = new System.Drawing.Point(237, 546);
            this.DetMin19.Margin = new System.Windows.Forms.Padding(4);
            this.DetMin19.Name = "DetMin19";
            this.DetMin19.Size = new System.Drawing.Size(71, 76);
            this.DetMin19.TabIndex = 32;
            this.DetMin19.Text = "-1 -1 -1 -1 9 -1 -1 -1 -1";
            this.DetMin19.UseVisualStyleBackColor = true;
            this.DetMin19.Click += new System.EventHandler(this.DetMin19_Click);
            // 
            // DetMin15
            // 
            this.DetMin15.Location = new System.Drawing.Point(316, 546);
            this.DetMin15.Margin = new System.Windows.Forms.Padding(4);
            this.DetMin15.Name = "DetMin15";
            this.DetMin15.Size = new System.Drawing.Size(71, 76);
            this.DetMin15.TabIndex = 33;
            this.DetMin15.Text = "0 -1 0 -1 5 -1 0 -1 0";
            this.DetMin15.UseVisualStyleBackColor = true;
            this.DetMin15.Click += new System.EventHandler(this.DetMin15_Click);
            // 
            // DetMin215
            // 
            this.DetMin215.Location = new System.Drawing.Point(395, 546);
            this.DetMin215.Margin = new System.Windows.Forms.Padding(4);
            this.DetMin215.Name = "DetMin215";
            this.DetMin215.Size = new System.Drawing.Size(71, 76);
            this.DetMin215.TabIndex = 34;
            this.DetMin215.Text = "1 -2 1 -2 5 -2 1 -2 1";
            this.DetMin215.UseVisualStyleBackColor = true;
            this.DetMin215.Click += new System.EventHandler(this.DetMin215_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1249, 98);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 45);
            this.button3.TabIndex = 35;
            this.button3.Text = "Median";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1249, 149);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 45);
            this.button4.TabIndex = 36;
            this.button4.Text = "Roberts";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1249, 199);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 45);
            this.button5.TabIndex = 37;
            this.button5.Text = "Prewwit";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(1249, 250);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 45);
            this.button6.TabIndex = 38;
            this.button6.Text = "Sobel";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1564, 1036);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.DetMin215);
            this.Controls.Add(this.DetMin15);
            this.Controls.Add(this.DetMin19);
            this.Controls.Add(this.Det1Min24);
            this.Controls.Add(this.DetMin18);
            this.Controls.Add(this.DetMin14);
            this.Controls.Add(this.K);
            this.Controls.Add(this.KValue);
            this.Controls.Add(this.Smoth0k);
            this.Controls.Add(this.Smoth1k);
            this.Controls.Add(this.Smoth121);
            this.Controls.Add(this.Smoth111);
            this.Controls.Add(this.Smoth014);
            this.Controls.Add(this.MASK);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Posterization);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tresholdNeg);
            this.Controls.Add(this.tresholdGray);
            this.Controls.Add(this.highTreshold);
            this.Controls.Add(this.lowThershold);
            this.Controls.Add(this.TresholdBtn);
            this.Controls.Add(this.trackBarUpDown);
            this.Controls.Add(this.trackBar);
            this.Controls.Add(this.NeagteBtn);
            this.Controls.Add(this.ScretchBtn);
            this.Controls.Add(this.ShowPostCharts);
            this.Controls.Add(this.NormalizeBtn);
            this.Controls.Add(this.ImagePostBox);
            this.Controls.Add(this.GreyBtn);
            this.Controls.Add(this.ShowCharts);
            this.Controls.Add(this.ChartGroup);
            this.Controls.Add(this.OpenFile);
            this.Controls.Add(this.ImageBox);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Image processer";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ChartGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RedChannel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePostBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowThershold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.highTreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox ImageBox;
        private System.Windows.Forms.Button OpenFile;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.GroupBox ChartGroup;
        private System.Windows.Forms.DataVisualization.Charting.Chart RedChannel;
        private System.Windows.Forms.Button ShowCharts;
        private System.Windows.Forms.Button GreyBtn;
        private System.Windows.Forms.PictureBox ImagePostBox;
        private System.Windows.Forms.Button NormalizeBtn;
        private System.Windows.Forms.Button ShowPostCharts;
        private System.Windows.Forms.Button ScretchBtn;
        private System.Windows.Forms.Button NeagteBtn;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.NumericUpDown trackBarUpDown;
        private System.Windows.Forms.Button TresholdBtn;
        private System.Windows.Forms.NumericUpDown highTreshold;
        private System.Windows.Forms.NumericUpDown lowThershold;
        private System.Windows.Forms.Button tresholdNeg;
        private System.Windows.Forms.Button tresholdGray;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Posterization;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox MASK;
        private System.Windows.Forms.Button Smoth111;
        private System.Windows.Forms.Button Smoth014;
        private System.Windows.Forms.Label K;
        private System.Windows.Forms.NumericUpDown KValue;
        private System.Windows.Forms.Button Smoth0k;
        private System.Windows.Forms.Button Smoth1k;
        private System.Windows.Forms.Button Smoth121;
        private System.Windows.Forms.Button DetMin215;
        private System.Windows.Forms.Button DetMin15;
        private System.Windows.Forms.Button DetMin19;
        private System.Windows.Forms.Button Det1Min24;
        private System.Windows.Forms.Button DetMin18;
        private System.Windows.Forms.Button DetMin14;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
    }
}

