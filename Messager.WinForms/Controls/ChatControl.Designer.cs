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
            this.chatMembersList = new System.Windows.Forms.ListBox();
            this.sendBtn = new System.Windows.Forms.Button();
            this.sendMessagePnl = new System.Windows.Forms.Panel();
            this.attachBtn = new System.Windows.Forms.Button();
            this.destructingChk = new System.Windows.Forms.CheckBox();
            this.addChatBtn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.chatPnl = new System.Windows.Forms.Panel();
            this.sendMessagePnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.messageTxt.Text = "Привет!\r\nКак дела?\r\nЧто Нового?\r\n";
            this.messageTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chatMembersList
            // 
            this.chatMembersList.BackColor = System.Drawing.Color.White;
            this.chatMembersList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chatMembersList.Font = new System.Drawing.Font("Fira Code", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chatMembersList.IntegralHeight = false;
            this.chatMembersList.ItemHeight = 15;
            this.chatMembersList.Items.AddRange(new object[] {
            "Никита Бархатов",
            "Александр Тихомиров",
            "Анастасия Соколова",
            "Лев Яшин",
            "Дмитрий Белкин",
            "Андрей Николаев"});
            this.chatMembersList.Location = new System.Drawing.Point(14, 215);
            this.chatMembersList.Name = "chatMembersList";
            this.chatMembersList.Size = new System.Drawing.Size(178, 140);
            this.chatMembersList.TabIndex = 2;
            this.chatMembersList.SelectedValueChanged += new System.EventHandler(this.chatMembersList_SelectedValueChanged);
            // 
            // sendBtn
            // 
            this.sendBtn.BackColor = System.Drawing.Color.Snow;
            this.sendBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.sendBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sendBtn.Font = new System.Drawing.Font("Fira Code", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
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
            this.attachBtn.Font = new System.Drawing.Font("Fira Code", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.attachBtn.ForeColor = System.Drawing.Color.DarkCyan;
            this.attachBtn.Location = new System.Drawing.Point(0, 41);
            this.attachBtn.Name = "attachBtn";
            this.attachBtn.Size = new System.Drawing.Size(87, 26);
            this.attachBtn.TabIndex = 8;
            this.attachBtn.Text = "Прикрепить";
            this.attachBtn.UseVisualStyleBackColor = false;
            this.attachBtn.Click += new System.EventHandler(this.button1_Click);
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
            this.addChatBtn.Font = new System.Drawing.Font("Fira Code", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addChatBtn.ForeColor = System.Drawing.Color.DarkCyan;
            this.addChatBtn.Location = new System.Drawing.Point(14, 361);
            this.addChatBtn.Name = "addChatBtn";
            this.addChatBtn.Size = new System.Drawing.Size(163, 31);
            this.addChatBtn.TabIndex = 8;
            this.addChatBtn.Text = "Добавить чат";
            this.addChatBtn.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(14, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(178, 192);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // chatPnl
            // 
            this.chatPnl.AutoScroll = true;
            this.chatPnl.BackColor = System.Drawing.Color.White;
            this.chatPnl.Location = new System.Drawing.Point(198, 17);
            this.chatPnl.Name = "chatPnl";
            this.chatPnl.Size = new System.Drawing.Size(499, 300);
            this.chatPnl.TabIndex = 7;
            // 
            // ChatControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.addChatBtn);
            this.Controls.Add(this.chatPnl);
            this.Controls.Add(this.sendMessagePnl);
            this.Controls.Add(this.sendBtn);
            this.Controls.Add(this.chatMembersList);
            this.Name = "ChatControl";
            this.Size = new System.Drawing.Size(710, 410);
            this.Load += new System.EventHandler(this.ChatControl_Load);
            this.sendMessagePnl.ResumeLayout(false);
            this.sendMessagePnl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox messageTxt;
        private System.Windows.Forms.ListBox chatMembersList;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.Panel sendMessagePnl;
        private System.Windows.Forms.Button attachBtn;
        private System.Windows.Forms.CheckBox destructingChk;
        private System.Windows.Forms.Button addChatBtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel chatPnl;
    }
}
