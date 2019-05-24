namespace GeoJsonCoordinatesTranf
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ChooseOringinFilePath = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.originFilePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.ChangeButton = new System.Windows.Forms.Button();
            this.ChangeResultTextBox = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ConvertProgressBar = new System.Windows.Forms.ProgressBar();
            this.WgsToGcj = new System.Windows.Forms.RadioButton();
            this.WgsToBd = new System.Windows.Forms.RadioButton();
            this.GcjToWgs = new System.Windows.Forms.RadioButton();
            this.GcjToBd = new System.Windows.Forms.RadioButton();
            this.BdToWgs = new System.Windows.Forms.RadioButton();
            this.BdToGcj = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // ChooseOringinFilePath
            // 
            this.ChooseOringinFilePath.Location = new System.Drawing.Point(377, 402);
            this.ChooseOringinFilePath.Name = "ChooseOringinFilePath";
            this.ChooseOringinFilePath.Size = new System.Drawing.Size(73, 25);
            this.ChooseOringinFilePath.TabIndex = 0;
            this.ChooseOringinFilePath.Text = "浏览...";
            this.ChooseOringinFilePath.UseVisualStyleBackColor = true;
            this.ChooseOringinFilePath.Click += new System.EventHandler(this.ChooseOringinFilePath_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "选择坐标系转换类型:";
            // 
            // originFilePath
            // 
            this.originFilePath.Location = new System.Drawing.Point(20, 402);
            this.originFilePath.Name = "originFilePath";
            this.originFilePath.Size = new System.Drawing.Size(351, 25);
            this.originFilePath.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 367);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "选择geojson源文件:";
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(665, 402);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(123, 25);
            this.SaveButton.TabIndex = 7;
            this.SaveButton.Text = "另存为";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ChangeButton
            // 
            this.ChangeButton.Location = new System.Drawing.Point(514, 402);
            this.ChangeButton.Name = "ChangeButton";
            this.ChangeButton.Size = new System.Drawing.Size(98, 25);
            this.ChangeButton.TabIndex = 8;
            this.ChangeButton.Text = "转换";
            this.ChangeButton.UseVisualStyleBackColor = true;
            this.ChangeButton.Click += new System.EventHandler(this.ChangeButton_Click);
            // 
            // ChangeResultTextBox
            // 
            this.ChangeResultTextBox.Location = new System.Drawing.Point(239, 52);
            this.ChangeResultTextBox.Name = "ChangeResultTextBox";
            this.ChangeResultTextBox.Size = new System.Drawing.Size(548, 330);
            this.ChangeResultTextBox.TabIndex = 9;
            this.ChangeResultTextBox.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(236, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "转换结果显示：";
            // 
            // ConvertProgressBar
            // 
            this.ConvertProgressBar.Location = new System.Drawing.Point(354, 19);
            this.ConvertProgressBar.Name = "ConvertProgressBar";
            this.ConvertProgressBar.Size = new System.Drawing.Size(432, 15);
            this.ConvertProgressBar.TabIndex = 15;
            // 
            // WgsToGcj
            // 
            this.WgsToGcj.AutoSize = true;
            this.WgsToGcj.Location = new System.Drawing.Point(20, 63);
            this.WgsToGcj.Name = "WgsToGcj";
            this.WgsToGcj.Size = new System.Drawing.Size(123, 19);
            this.WgsToGcj.TabIndex = 16;
            this.WgsToGcj.TabStop = true;
            this.WgsToGcj.Text = "WGS84 转 GCJ";
            this.WgsToGcj.UseVisualStyleBackColor = true;
            this.WgsToGcj.CheckedChanged += new System.EventHandler(this.WgsToGcj_CheckedChanged_1);
            // 
            // WgsToBd
            // 
            this.WgsToBd.AutoSize = true;
            this.WgsToBd.Location = new System.Drawing.Point(20, 105);
            this.WgsToBd.Name = "WgsToBd";
            this.WgsToBd.Size = new System.Drawing.Size(139, 19);
            this.WgsToBd.TabIndex = 17;
            this.WgsToBd.TabStop = true;
            this.WgsToBd.Text = "WGS84 转 BD-09";
            this.WgsToBd.UseVisualStyleBackColor = true;
            this.WgsToBd.CheckedChanged += new System.EventHandler(this.WgsToBd_CheckedChanged_1);
            // 
            // GcjToWgs
            // 
            this.GcjToWgs.AutoSize = true;
            this.GcjToWgs.Location = new System.Drawing.Point(20, 151);
            this.GcjToWgs.Name = "GcjToWgs";
            this.GcjToWgs.Size = new System.Drawing.Size(123, 19);
            this.GcjToWgs.TabIndex = 18;
            this.GcjToWgs.TabStop = true;
            this.GcjToWgs.Text = "GCJ 转 WGS84";
            this.GcjToWgs.UseVisualStyleBackColor = true;
            this.GcjToWgs.CheckedChanged += new System.EventHandler(this.GcjToWgs_CheckedChanged_1);
            // 
            // GcjToBd
            // 
            this.GcjToBd.AutoSize = true;
            this.GcjToBd.Location = new System.Drawing.Point(20, 192);
            this.GcjToBd.Name = "GcjToBd";
            this.GcjToBd.Size = new System.Drawing.Size(123, 19);
            this.GcjToBd.TabIndex = 19;
            this.GcjToBd.TabStop = true;
            this.GcjToBd.Text = "GCJ 转 BD-09";
            this.GcjToBd.UseVisualStyleBackColor = true;
            this.GcjToBd.CheckedChanged += new System.EventHandler(this.GcjToBd_CheckedChanged_1);
            // 
            // BdToWgs
            // 
            this.BdToWgs.AutoSize = true;
            this.BdToWgs.Location = new System.Drawing.Point(20, 236);
            this.BdToWgs.Name = "BdToWgs";
            this.BdToWgs.Size = new System.Drawing.Size(139, 19);
            this.BdToWgs.TabIndex = 20;
            this.BdToWgs.TabStop = true;
            this.BdToWgs.Text = "BD-09 转 WGS84";
            this.BdToWgs.UseVisualStyleBackColor = true;
            this.BdToWgs.CheckedChanged += new System.EventHandler(this.BdToWgs_CheckedChanged_1);
            // 
            // BdToGcj
            // 
            this.BdToGcj.AutoSize = true;
            this.BdToGcj.Location = new System.Drawing.Point(20, 284);
            this.BdToGcj.Name = "BdToGcj";
            this.BdToGcj.Size = new System.Drawing.Size(123, 19);
            this.BdToGcj.TabIndex = 21;
            this.BdToGcj.TabStop = true;
            this.BdToGcj.Text = "BD-09 转 GCJ";
            this.BdToGcj.UseVisualStyleBackColor = true;
            this.BdToGcj.CheckedChanged += new System.EventHandler(this.BdToGcj_CheckedChanged_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BdToGcj);
            this.Controls.Add(this.BdToWgs);
            this.Controls.Add(this.GcjToBd);
            this.Controls.Add(this.GcjToWgs);
            this.Controls.Add(this.WgsToBd);
            this.Controls.Add(this.WgsToGcj);
            this.Controls.Add(this.ConvertProgressBar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ChangeResultTextBox);
            this.Controls.Add(this.ChangeButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.originFilePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ChooseOringinFilePath);
            this.Name = "Form1";
            this.Text = "geojson坐标系转换工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ChooseOringinFilePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox originFilePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button ChangeButton;
        private System.Windows.Forms.RichTextBox ChangeResultTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar ConvertProgressBar;
        private System.Windows.Forms.RadioButton WgsToGcj;
        private System.Windows.Forms.RadioButton WgsToBd;
        private System.Windows.Forms.RadioButton GcjToWgs;
        private System.Windows.Forms.RadioButton GcjToBd;
        private System.Windows.Forms.RadioButton BdToWgs;
        private System.Windows.Forms.RadioButton BdToGcj;
    }
}

