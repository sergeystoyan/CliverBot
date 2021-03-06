namespace Cliver.BotGui
{
    partial class WebConfigControl
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
            this.MaxAttemptCount = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.MaxHttpRedirectionCount = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.CrawlTimeIntervalInMss = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.HttpUserAgent = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.MaxDownloadedFileLength = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.l1 = new System.Windows.Forms.Label();
            this.HttpRequestTimeoutInSeconds = new System.Windows.Forms.TextBox();
            this.UseFilesFromCache = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.LogPostRequestParameters = new System.Windows.Forms.CheckBox();
            this.LogDownloadedFiles = new System.Windows.Forms.CheckBox();
            this.label23 = new System.Windows.Forms.Label();
            this.group_box.SuspendLayout();
            this.SuspendLayout();
            // 
            // group_box
            // 
            this.group_box.Controls.Add(this.label23);
            this.group_box.Controls.Add(this.LogPostRequestParameters);
            this.group_box.Controls.Add(this.LogDownloadedFiles);
            this.group_box.Controls.Add(this.label2);
            this.group_box.Controls.Add(this.UseFilesFromCache);
            this.group_box.Controls.Add(this.MaxAttemptCount);
            this.group_box.Controls.Add(this.label15);
            this.group_box.Controls.Add(this.MaxHttpRedirectionCount);
            this.group_box.Controls.Add(this.label10);
            this.group_box.Controls.Add(this.label25);
            this.group_box.Controls.Add(this.CrawlTimeIntervalInMss);
            this.group_box.Controls.Add(this.label1);
            this.group_box.Controls.Add(this.HttpUserAgent);
            this.group_box.Controls.Add(this.label5);
            this.group_box.Controls.Add(this.MaxDownloadedFileLength);
            this.group_box.Controls.Add(this.label3);
            this.group_box.Controls.Add(this.l1);
            this.group_box.Controls.Add(this.HttpRequestTimeoutInSeconds);
            this.group_box.Text = "TestCustom";
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 100000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            // 
            // MaxAttemptCount
            // 
            this.MaxAttemptCount.Location = new System.Drawing.Point(459, 210);
            this.MaxAttemptCount.Name = "MaxAttemptCount";
            this.MaxAttemptCount.Size = new System.Drawing.Size(40, 20);
            this.MaxAttemptCount.TabIndex = 8;
            this.MaxAttemptCount.Text = "5";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(240, 214);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(216, 16);
            this.label15.TabIndex = 66;
            this.label15.Text = "Maximal Count of Attempts:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // MaxHttpRedirectionCount
            // 
            this.MaxHttpRedirectionCount.Location = new System.Drawing.Point(458, 179);
            this.MaxHttpRedirectionCount.Name = "MaxHttpRedirectionCount";
            this.MaxHttpRedirectionCount.Size = new System.Drawing.Size(40, 20);
            this.MaxHttpRedirectionCount.TabIndex = 7;
            this.MaxHttpRedirectionCount.Text = "5";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(240, 183);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(216, 16);
            this.label10.TabIndex = 64;
            this.label10.Text = "Maximal Count of Redirections:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label25
            // 
            this.label25.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label25.Location = new System.Drawing.Point(19, 92);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(483, 2);
            this.label25.TabIndex = 62;
            // 
            // CrawlTimeIntervalInMss
            // 
            this.CrawlTimeIntervalInMss.Location = new System.Drawing.Point(458, 24);
            this.CrawlTimeIntervalInMss.Name = "CrawlTimeIntervalInMss";
            this.CrawlTimeIntervalInMss.Size = new System.Drawing.Size(40, 20);
            this.CrawlTimeIntervalInMss.TabIndex = 0;
            this.CrawlTimeIntervalInMss.Text = "0";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(176, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(269, 16);
            this.label1.TabIndex = 61;
            this.label1.Text = "Time Interval Between HTTP Requests (ms):";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // HttpUserAgent
            // 
            this.HttpUserAgent.Location = new System.Drawing.Point(96, 54);
            this.HttpUserAgent.Multiline = true;
            this.HttpUserAgent.Name = "HttpUserAgent";
            this.HttpUserAgent.Size = new System.Drawing.Size(402, 20);
            this.HttpUserAgent.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 16);
            this.label5.TabIndex = 58;
            this.label5.Text = "Http User Agent:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // MaxDownloadedFileLength
            // 
            this.MaxDownloadedFileLength.Location = new System.Drawing.Point(433, 149);
            this.MaxDownloadedFileLength.Name = "MaxDownloadedFileLength";
            this.MaxDownloadedFileLength.Size = new System.Drawing.Size(65, 20);
            this.MaxDownloadedFileLength.TabIndex = 6;
            this.MaxDownloadedFileLength.Text = "50000";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(200, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(232, 16);
            this.label3.TabIndex = 57;
            this.label3.Text = "Maximal Downloaded File Length (bytes):";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // l1
            // 
            this.l1.Location = new System.Drawing.Point(269, 122);
            this.l1.Name = "l1";
            this.l1.Size = new System.Drawing.Size(183, 16);
            this.l1.TabIndex = 54;
            this.l1.Text = "Http Request Timeout (secs):";
            this.l1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // HttpRequestTimeoutInSeconds
            // 
            this.HttpRequestTimeoutInSeconds.Location = new System.Drawing.Point(458, 119);
            this.HttpRequestTimeoutInSeconds.Name = "HttpRequestTimeoutInSeconds";
            this.HttpRequestTimeoutInSeconds.Size = new System.Drawing.Size(40, 20);
            this.HttpRequestTimeoutInSeconds.TabIndex = 5;
            this.HttpRequestTimeoutInSeconds.Text = "30";
            // 
            // UseFilesFromCache
            // 
            this.UseFilesFromCache.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.UseFilesFromCache.Location = new System.Drawing.Point(28, 183);
            this.UseFilesFromCache.Name = "UseFilesFromCache";
            this.UseFilesFromCache.Size = new System.Drawing.Size(172, 20);
            this.UseFilesFromCache.TabIndex = 4;
            this.UseFilesFromCache.Text = "Use Cached Files";
            this.UseFilesFromCache.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(15, 257);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(483, 2);
            this.label2.TabIndex = 68;
            // 
            // LogPostRequestParameters
            // 
            this.LogPostRequestParameters.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LogPostRequestParameters.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LogPostRequestParameters.Location = new System.Drawing.Point(18, 148);
            this.LogPostRequestParameters.Name = "LogPostRequestParameters";
            this.LogPostRequestParameters.Size = new System.Drawing.Size(182, 26);
            this.LogPostRequestParameters.TabIndex = 3;
            this.LogPostRequestParameters.Text = "Log Post Request Parameters";
            this.LogPostRequestParameters.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LogDownloadedFiles
            // 
            this.LogDownloadedFiles.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LogDownloadedFiles.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LogDownloadedFiles.Location = new System.Drawing.Point(56, 116);
            this.LogDownloadedFiles.Name = "LogDownloadedFiles";
            this.LogDownloadedFiles.Size = new System.Drawing.Size(144, 26);
            this.LogDownloadedFiles.TabIndex = 2;
            this.LogDownloadedFiles.Text = "Log Downloaded Files";
            this.LogDownloadedFiles.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label23
            // 
            this.label23.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label23.Location = new System.Drawing.Point(210, 109);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(2, 130);
            this.label23.TabIndex = 71;
            // 
            // WebConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "WebConfigControl";
            this.group_box.ResumeLayout(false);
            this.group_box.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox MaxAttemptCount;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox MaxHttpRedirectionCount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox CrawlTimeIntervalInMss;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox HttpUserAgent;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox MaxDownloadedFileLength;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label l1;
        private System.Windows.Forms.TextBox HttpRequestTimeoutInSeconds;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox UseFilesFromCache;
        private System.Windows.Forms.CheckBox LogPostRequestParameters;
        private System.Windows.Forms.CheckBox LogDownloadedFiles;
        private System.Windows.Forms.Label label23;
    }
}
