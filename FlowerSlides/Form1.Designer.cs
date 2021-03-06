﻿namespace FlowerSlides
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureTitle = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.thumbsPanel1 = new FlowerSlides.ThumbsPanel();
            this.blendPanel1 = new BlendPanel();
            this.slidePanel1 = new FlowerSlides.SlidePanel();
            this.SuspendLayout();
            // 
            // pictureTitle
            // 
            this.pictureTitle.AutoSize = true;
            this.pictureTitle.Font = new System.Drawing.Font("Lucida Sans Unicode", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pictureTitle.Location = new System.Drawing.Point(335, 9);
            this.pictureTitle.Name = "pictureTitle";
            this.pictureTitle.Size = new System.Drawing.Size(144, 28);
            this.pictureTitle.TabIndex = 1;
            this.pictureTitle.Text = "pictureTitle";
            // 
            // thumbsPanel1
            // 
            this.thumbsPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.thumbsPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.thumbsPanel1.CurrentFolder = null;
            this.thumbsPanel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.thumbsPanel1.Location = new System.Drawing.Point(12, 12);
            this.thumbsPanel1.Name = "thumbsPanel1";
            this.thumbsPanel1.Size = new System.Drawing.Size(740, 488);
            this.thumbsPanel1.TabIndex = 0;
            // 
            // blendPanel1
            // 
            this.blendPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.blendPanel1.Blend = 0F;
            this.blendPanel1.Image1 = null;
            this.blendPanel1.Image2 = null;
            this.blendPanel1.Location = new System.Drawing.Point(775, 472);
            this.blendPanel1.Name = "blendPanel1";
            this.blendPanel1.Size = new System.Drawing.Size(117, 86);
            this.blendPanel1.SizeMode = BlendPanelSizeMode.Zoom;
            this.blendPanel1.TabIndex = 2;
            // 
            // slidePanel1
            // 
            this.slidePanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.slidePanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.slidePanel1.CurrentFile = null;
            this.slidePanel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.slidePanel1.Location = new System.Drawing.Point(778, 59);
            this.slidePanel1.Name = "slidePanel1";
            this.slidePanel1.Size = new System.Drawing.Size(142, 92);
            this.slidePanel1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(947, 570);
            this.Controls.Add(this.thumbsPanel1);
            this.Controls.Add(this.blendPanel1);
            this.Controls.Add(this.slidePanel1);
            this.Controls.Add(this.pictureTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label pictureTitle;
        private BlendPanel blendPanel1;
        private System.Windows.Forms.Timer timer1;
        private ThumbsPanel thumbsPanel1;
        private SlidePanel slidePanel1;

    }
}

