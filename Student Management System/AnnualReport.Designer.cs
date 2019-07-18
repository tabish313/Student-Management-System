namespace Student_Management_System
{
    partial class AnnualReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnnualReport));
            this.bunifuGradientPanel1 = new Bunifu.Framework.UI.BunifuGradientPanel();
            this.labelcopyright = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnreset = new Syncfusion.WinForms.Controls.SfButton();
            this.btngenerate = new Syncfusion.WinForms.Controls.SfButton();
            this.comboboxclass = new Syncfusion.WinForms.ListView.SfComboBox();
            this.comboboxsection = new Syncfusion.WinForms.ListView.SfComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.bunifuGradientPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboboxclass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboboxsection)).BeginInit();
            this.SuspendLayout();
            // 
            // bunifuGradientPanel1
            // 
            this.bunifuGradientPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(140)))), ((int)(((byte)(166)))));
            this.bunifuGradientPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuGradientPanel1.BackgroundImage")));
            this.bunifuGradientPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bunifuGradientPanel1.Controls.Add(this.labelcopyright);
            this.bunifuGradientPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bunifuGradientPanel1.GradientBottomLeft = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(140)))), ((int)(((byte)(166)))));
            this.bunifuGradientPanel1.GradientBottomRight = System.Drawing.Color.White;
            this.bunifuGradientPanel1.GradientTopLeft = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(140)))), ((int)(((byte)(166)))));
            this.bunifuGradientPanel1.GradientTopRight = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(140)))), ((int)(((byte)(166)))));
            this.bunifuGradientPanel1.Location = new System.Drawing.Point(0, 167);
            this.bunifuGradientPanel1.Name = "bunifuGradientPanel1";
            this.bunifuGradientPanel1.Quality = 10;
            this.bunifuGradientPanel1.Size = new System.Drawing.Size(503, 24);
            this.bunifuGradientPanel1.TabIndex = 11;
            // 
            // labelcopyright
            // 
            this.labelcopyright.BackColor = System.Drawing.Color.Transparent;
            this.labelcopyright.ForeColor = System.Drawing.Color.White;
            this.labelcopyright.Location = new System.Drawing.Point(28, 4);
            this.labelcopyright.Name = "labelcopyright";
            this.labelcopyright.Size = new System.Drawing.Size(446, 17);
            this.labelcopyright.TabIndex = 0;
            this.labelcopyright.Text = "Loading...";
            this.labelcopyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnreset);
            this.groupBox1.Controls.Add(this.btngenerate);
            this.groupBox1.Controls.Add(this.comboboxclass);
            this.groupBox1.Controls.Add(this.comboboxsection);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(482, 143);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Generate";
            // 
            // btnreset
            // 
            this.btnreset.AccessibleName = "Button";
            this.btnreset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnreset.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnreset.Location = new System.Drawing.Point(253, 85);
            this.btnreset.Name = "btnreset";
            this.btnreset.Size = new System.Drawing.Size(118, 25);
            this.btnreset.Style.FocusedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnreset.Style.HoverBackColor = System.Drawing.Color.Silver;
            this.btnreset.Style.PressedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnreset.TabIndex = 3;
            this.btnreset.Text = "Reset";
            this.btnreset.Click += new System.EventHandler(this.btnreset_Click);
            // 
            // btngenerate
            // 
            this.btngenerate.AccessibleName = "Button";
            this.btngenerate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btngenerate.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btngenerate.Location = new System.Drawing.Point(111, 85);
            this.btngenerate.Name = "btngenerate";
            this.btngenerate.Size = new System.Drawing.Size(128, 25);
            this.btngenerate.Style.FocusedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btngenerate.Style.HoverBackColor = System.Drawing.Color.Silver;
            this.btngenerate.Style.PressedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btngenerate.TabIndex = 2;
            this.btngenerate.Text = "Submit";
            this.btngenerate.Click += new System.EventHandler(this.btngenerate_Click);
            // 
            // comboboxclass
            // 
            this.comboboxclass.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboboxclass.Location = new System.Drawing.Point(71, 22);
            this.comboboxclass.Name = "comboboxclass";
            this.comboboxclass.Size = new System.Drawing.Size(155, 21);
            this.comboboxclass.Style.EditorStyle.BorderColor = System.Drawing.Color.Silver;
            this.comboboxclass.Style.EditorStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.comboboxclass.Style.EditorStyle.ForeColor = System.Drawing.Color.DimGray;
            this.comboboxclass.Style.EditorStyle.HoverBorderColor = System.Drawing.Color.DarkGray;
            this.comboboxclass.TabIndex = 0;
            // 
            // comboboxsection
            // 
            this.comboboxsection.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboboxsection.Location = new System.Drawing.Point(309, 22);
            this.comboboxsection.Name = "comboboxsection";
            this.comboboxsection.Size = new System.Drawing.Size(155, 21);
            this.comboboxsection.Style.EditorStyle.BorderColor = System.Drawing.Color.Silver;
            this.comboboxsection.Style.EditorStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.comboboxsection.Style.EditorStyle.ForeColor = System.Drawing.Color.DimGray;
            this.comboboxsection.Style.EditorStyle.HoverBorderColor = System.Drawing.Color.DarkGray;
            this.comboboxsection.TabIndex = 1;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(20, 24);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(38, 16);
            this.label15.TabIndex = 16;
            this.label15.Text = "Class:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(252, 24);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(51, 16);
            this.label16.TabIndex = 17;
            this.label16.Text = "Section:";
            // 
            // AnnualReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(140)))), ((int)(((byte)(166)))));
            this.BorderThickness = 2;
            this.CaptionBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(140)))), ((int)(((byte)(166)))));
            this.CaptionButtonColor = System.Drawing.Color.WhiteSmoke;
            this.CaptionButtonHoverColor = System.Drawing.Color.White;
            this.CaptionForeColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(503, 191);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bunifuGradientPanel1);
            this.MaximizeBox = false;
            this.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(140)))), ((int)(((byte)(166)))));
            this.MinimizeBox = false;
            this.Name = "AnnualReport";
            this.ShowIcon = false;
            this.ShowMaximizeBox = false;
            this.ShowMinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Annual Student Report";
            this.Activated += new System.EventHandler(this.AnnualReport_Activated);
            this.Load += new System.EventHandler(this.AnnualReport_Load);
            this.bunifuGradientPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboboxclass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboboxsection)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Bunifu.Framework.UI.BunifuGradientPanel bunifuGradientPanel1;
        private System.Windows.Forms.Label labelcopyright;
        private System.Windows.Forms.GroupBox groupBox1;
        private Syncfusion.WinForms.ListView.SfComboBox comboboxclass;
        private Syncfusion.WinForms.ListView.SfComboBox comboboxsection;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private Syncfusion.WinForms.Controls.SfButton btngenerate;
        private Syncfusion.WinForms.Controls.SfButton btnreset;
    }
}