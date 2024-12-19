namespace order_accounting
{
    partial class Regist_Form
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.Password_Text = new System.Windows.Forms.TextBox();
            this.Login_text1 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(524, 78);
            this.panel1.TabIndex = 1;
            // 
            // Password_Text
            // 
            this.Password_Text.Font = new System.Drawing.Font("Microsoft Tai Le", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Password_Text.ForeColor = System.Drawing.Color.Silver;
            this.Password_Text.Location = new System.Drawing.Point(24, 324);
            this.Password_Text.Name = "Password_Text";
            this.Password_Text.Size = new System.Drawing.Size(425, 48);
            this.Password_Text.TabIndex = 6;
            this.Password_Text.Text = "Пароль";
            this.Password_Text.TextChanged += new System.EventHandler(this.Password_Text_TextChanged);
            this.Password_Text.Enter += new System.EventHandler(this.Password_Text_Enter);
            this.Password_Text.Leave += new System.EventHandler(this.Password_Text_Leave);
            // 
            // Login_text1
            // 
            this.Login_text1.Font = new System.Drawing.Font("Microsoft Tai Le", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Login_text1.ForeColor = System.Drawing.Color.Silver;
            this.Login_text1.Location = new System.Drawing.Point(24, 233);
            this.Login_text1.Name = "Login_text1";
            this.Login_text1.Size = new System.Drawing.Size(425, 48);
            this.Login_text1.TabIndex = 5;
            this.Login_text1.Text = "Логин";
            this.Login_text1.TextChanged += new System.EventHandler(this.Login_text1_TextChanged);
            this.Login_text1.Enter += new System.EventHandler(this.Login_text1_Enter);
            this.Login_text1.Leave += new System.EventHandler(this.Login_text1_Leave);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Tai Le", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.Silver;
            this.textBox1.Location = new System.Drawing.Point(24, 414);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(425, 48);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "Подтвердите пароль";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(200, 584);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(148, 50);
            this.button1.TabIndex = 8;
            this.button1.Text = "Добавить\r\nсотрудника";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(354, 584);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(148, 50);
            this.button2.TabIndex = 9;
            this.button2.Text = "Очистить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::order_accounting.Properties.Resources.SibGold;
            this.pictureBox1.Location = new System.Drawing.Point(200, 84);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(125, 125);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // Regist_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 669);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Password_Text);
            this.Controls.Add(this.Login_text1);
            this.Controls.Add(this.panel1);
            this.Name = "Regist_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Регистрация";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox Password_Text;
        private System.Windows.Forms.TextBox Login_text1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}