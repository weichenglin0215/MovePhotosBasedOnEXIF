namespace MovePhotosBasedOnEXIF
{
    partial class Form_MovePhotosBasedOnEXIF
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
            this.textBox_Path = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_Path = new System.Windows.Forms.Button();
            this.listBox_Files = new System.Windows.Forms.ListBox();
            this.label_Status = new System.Windows.Forms.Label();
            this.checkBox_IncludeSubDir = new System.Windows.Forms.CheckBox();
            this.button_Test = new System.Windows.Forms.Button();
            this.button_MovePic = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_Path
            // 
            this.textBox_Path.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Path.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox_Path.Location = new System.Drawing.Point(13, 37);
            this.textBox_Path.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox_Path.Name = "textBox_Path";
            this.textBox_Path.Size = new System.Drawing.Size(1015, 29);
            this.textBox_Path.TabIndex = 0;
            this.textBox_Path.Text = "C:\\Users\\joey\\Pictures\\PicPick截圖\\TEST";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "目錄";
            // 
            // button_Path
            // 
            this.button_Path.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Path.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Path.Location = new System.Drawing.Point(1036, 34);
            this.button_Path.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_Path.Name = "button_Path";
            this.button_Path.Size = new System.Drawing.Size(66, 30);
            this.button_Path.TabIndex = 2;
            this.button_Path.Text = "目錄...";
            this.button_Path.UseVisualStyleBackColor = true;
            this.button_Path.Click += new System.EventHandler(this.button_Path_Click);
            // 
            // listBox_Files
            // 
            this.listBox_Files.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox_Files.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.listBox_Files.FormattingEnabled = true;
            this.listBox_Files.HorizontalScrollbar = true;
            this.listBox_Files.ItemHeight = 20;
            this.listBox_Files.Location = new System.Drawing.Point(13, 115);
            this.listBox_Files.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBox_Files.Name = "listBox_Files";
            this.listBox_Files.Size = new System.Drawing.Size(1198, 784);
            this.listBox_Files.TabIndex = 4;
            // 
            // label_Status
            // 
            this.label_Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_Status.AutoSize = true;
            this.label_Status.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_Status.Location = new System.Drawing.Point(13, 923);
            this.label_Status.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_Status.Name = "label_Status";
            this.label_Status.Size = new System.Drawing.Size(1042, 20);
            this.label_Status.TabIndex = 5;
            this.label_Status.Text = "狀態：目前能辨別 JPG、GIF、PNG、PSD、HEIC。如果該檔案EXIF格式有遺失，或取用該檔案的修改日期。MP4與MOV檔案直接取用修改日期。";
            // 
            // checkBox_IncludeSubDir
            // 
            this.checkBox_IncludeSubDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_IncludeSubDir.AutoSize = true;
            this.checkBox_IncludeSubDir.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBox_IncludeSubDir.Location = new System.Drawing.Point(1110, 37);
            this.checkBox_IncludeSubDir.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox_IncludeSubDir.Name = "checkBox_IncludeSubDir";
            this.checkBox_IncludeSubDir.Size = new System.Drawing.Size(108, 24);
            this.checkBox_IncludeSubDir.TabIndex = 6;
            this.checkBox_IncludeSubDir.Text = "包含子目錄";
            this.checkBox_IncludeSubDir.UseVisualStyleBackColor = true;
            // 
            // button_Test
            // 
            this.button_Test.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Test.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Test.Location = new System.Drawing.Point(853, 72);
            this.button_Test.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_Test.Name = "button_Test";
            this.button_Test.Size = new System.Drawing.Size(131, 30);
            this.button_Test.TabIndex = 12;
            this.button_Test.Text = "僅測試顯示結果";
            this.button_Test.UseVisualStyleBackColor = true;
            this.button_Test.Click += new System.EventHandler(this.button_Test_Click);
            // 
            // button_MovePic
            // 
            this.button_MovePic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_MovePic.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_MovePic.Location = new System.Drawing.Point(1080, 73);
            this.button_MovePic.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_MovePic.Name = "button_MovePic";
            this.button_MovePic.Size = new System.Drawing.Size(131, 30);
            this.button_MovePic.TabIndex = 13;
            this.button_MovePic.Text = "搬移照片";
            this.button_MovePic.UseVisualStyleBackColor = true;
            this.button_MovePic.Click += new System.EventHandler(this.button_MovePic_Click);
            // 
            // Form_MovePhotosBasedOnEXIF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1224, 952);
            this.Controls.Add(this.button_MovePic);
            this.Controls.Add(this.button_Test);
            this.Controls.Add(this.checkBox_IncludeSubDir);
            this.Controls.Add(this.label_Status);
            this.Controls.Add(this.listBox_Files);
            this.Controls.Add(this.button_Path);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_Path);
            this.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form_MovePhotosBasedOnEXIF";
            this.Text = "照片依日期分置目錄 Ver. 0.3.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Path;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_Path;
        private System.Windows.Forms.ListBox listBox_Files;
        private System.Windows.Forms.Label label_Status;
        private System.Windows.Forms.CheckBox checkBox_IncludeSubDir;
        private System.Windows.Forms.Button button_Test;
        private System.Windows.Forms.Button button_MovePic;
    }
}

