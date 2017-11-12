namespace Messager.WinForms.Controls
{
    partial class ChatControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatControl));
            this.messageTxt = new System.Windows.Forms.TextBox();
            this.chatList = new System.Windows.Forms.ListBox();
            this.sendBtn = new System.Windows.Forms.Button();
            this.sendMessagePnl = new System.Windows.Forms.Panel();
            this.attachBtn = new System.Windows.Forms.Button();
            this.destructingChk = new System.Windows.Forms.CheckBox();
            this.addChatBtn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.profilePhotoPB = new System.Windows.Forms.PictureBox();
            this.msgPnl = new System.Windows.Forms.Panel();
            this.sendMessagePnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profilePhotoPB)).BeginInit();
            this.SuspendLayout();
            // 
            // messageTxt
            // 
            this.messageTxt.BackColor = System.Drawing.Color.White;
            this.messageTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.messageTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.messageTxt.Location = new System.Drawing.Point(12, 9);
            this.messageTxt.Multiline = true;
            this.messageTxt.Name = "messageTxt";
            this.messageTxt.Size = new System.Drawing.Size(332, 50);
            this.messageTxt.TabIndex = 1;
            this.messageTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chatList
            // 
            this.chatList.BackColor = System.Drawing.Color.White;
            this.chatList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chatList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chatList.IntegralHeight = false;
            this.chatList.ItemHeight = 16;
            this.chatList.Location = new System.Drawing.Point(14, 215);
            this.chatList.Name = "chatList";
            this.chatList.Size = new System.Drawing.Size(178, 140);
            this.chatList.TabIndex = 2;
            this.chatList.SelectedIndexChanged += new System.EventHandler(this.chatList_SelectedIndexChanged);
            // 
            // sendBtn
            // 
            this.sendBtn.BackColor = System.Drawing.Color.Snow;
            this.sendBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.sendBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sendBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sendBtn.ForeColor = System.Drawing.Color.DarkCyan;
            this.sendBtn.Location = new System.Drawing.Point(577, 325);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(120, 67);
            this.sendBtn.TabIndex = 0;
            this.sendBtn.Text = "Отправить";
            this.sendBtn.UseVisualStyleBackColor = false;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // sendMessagePnl
            // 
            this.sendMessagePnl.BackColor = System.Drawing.Color.White;
            this.sendMessagePnl.Controls.Add(this.attachBtn);
            this.sendMessagePnl.Controls.Add(this.destructingChk);
            this.sendMessagePnl.Controls.Add(this.messageTxt);
            this.sendMessagePnl.Location = new System.Drawing.Point(198, 325);
            this.sendMessagePnl.Name = "sendMessagePnl";
            this.sendMessagePnl.Padding = new System.Windows.Forms.Padding(5);
            this.sendMessagePnl.Size = new System.Drawing.Size(372, 67);
            this.sendMessagePnl.TabIndex = 5;
            // 
            // attachBtn
            // 
            this.attachBtn.BackColor = System.Drawing.Color.Snow;
            this.attachBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.attachBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.attachBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.attachBtn.ForeColor = System.Drawing.Color.DarkCyan;
            this.attachBtn.Location = new System.Drawing.Point(0, 41);
            this.attachBtn.Name = "attachBtn";
            this.attachBtn.Size = new System.Drawing.Size(87, 26);
            this.attachBtn.TabIndex = 8;
            this.attachBtn.Text = "Прикрепить";
            this.attachBtn.UseVisualStyleBackColor = false;
            this.attachBtn.Click += new System.EventHandler(this.attachBtn_Click);
            // 
            // destructingChk
            // 
            this.destructingChk.AutoSize = true;
            this.destructingChk.BackColor = System.Drawing.Color.White;
            this.destructingChk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.destructingChk.ForeColor = System.Drawing.Color.DarkCyan;
            this.destructingChk.Location = new System.Drawing.Point(0, 0);
            this.destructingChk.Name = "destructingChk";
            this.destructingChk.Size = new System.Drawing.Size(15, 14);
            this.destructingChk.TabIndex = 1;
            this.destructingChk.UseVisualStyleBackColor = false;
            // 
            // addChatBtn
            // 
            this.addChatBtn.BackColor = System.Drawing.Color.Snow;
            this.addChatBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.addChatBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addChatBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addChatBtn.ForeColor = System.Drawing.Color.DarkCyan;
            this.addChatBtn.Location = new System.Drawing.Point(14, 361);
            this.addChatBtn.Name = "addChatBtn";
            this.addChatBtn.Size = new System.Drawing.Size(163, 31);
            this.addChatBtn.TabIndex = 8;
            this.addChatBtn.Text = "Добавить чат";
            this.addChatBtn.UseVisualStyleBackColor = false;
            this.addChatBtn.Click += new System.EventHandler(this.addChatBtn_Click);
            // 
            // profilePhotoPB
            // 
            this.profilePhotoPB.Image = ((System.Drawing.Image)(resources.GetObject("profilePhotoPB.Image")));
            this.profilePhotoPB.Location = new System.Drawing.Point(14, 17);
            this.profilePhotoPB.Name = "profilePhotoPB";
            this.profilePhotoPB.Size = new System.Drawing.Size(178, 192);
            this.profilePhotoPB.TabIndex = 9;
            this.profilePhotoPB.TabStop = false;
            // 
            // msgPnl
            // 
            this.msgPnl.AutoScroll = true;
            this.msgPnl.BackColor = System.Drawing.Color.White;
            this.msgPnl.Location = new System.Drawing.Point(198, 17);
            this.msgPnl.Name = "msgPnl";
            this.msgPnl.Size = new System.Drawing.Size(499, 300);
            this.msgPnl.TabIndex = 7;
            // 
            // ChatControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.profilePhotoPB);
            this.Controls.Add(this.addChatBtn);
            this.Controls.Add(this.msgPnl);
            this.Controls.Add(this.sendMessagePnl);
            this.Controls.Add(this.sendBtn);
            this.Controls.Add(this.chatList);
            this.Name = "ChatControl";
            this.Size = new System.Drawing.Size(710, 410);
            this.Load += new System.EventHandler(this.ChatControl_Load);
            this.sendMessagePnl.ResumeLayout(false);
            this.sendMessagePnl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profilePhotoPB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox messageTxt;
        private System.Windows.Forms.ListBox chatList;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.Panel sendMessagePnl;
        private System.Windows.Forms.Button attachBtn;
        private System.Windows.Forms.CheckBox destructingChk;
        private System.Windows.Forms.Button addChatBtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox profilePhotoPB;
        private System.Windows.Forms.Panel msgPnl;
    }
}
