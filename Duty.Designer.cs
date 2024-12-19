namespace order_accounting
{
    partial class Duty
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.excavator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bucket = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Driver = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Start = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.End = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.number_of_buckets = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.average_buckets = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.number_of_cars = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.opt = new System.Windows.Forms.DataGridViewImageColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.sqLiteCommand1 = new System.Data.SQLite.SQLiteCommand();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.number,
            this.FileName,
            this.excavator,
            this.Bucket,
            this.Driver,
            this.Start,
            this.End,
            this.number_of_buckets,
            this.average_buckets,
            this.number_of_cars,
            this.opt,
            this.Delete});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(1027, 601);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // number
            // 
            this.number.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.number.Frozen = true;
            this.number.HeaderText = "№ смены";
            this.number.Name = "number";
            this.number.ReadOnly = true;
            this.number.Width = 61;
            // 
            // FileName
            // 
            this.FileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.FileName.HeaderText = "Файл-источник";
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            this.FileName.Width = 51;
            // 
            // excavator
            // 
            this.excavator.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.excavator.HeaderText = "Погрузчик";
            this.excavator.Name = "excavator";
            this.excavator.ReadOnly = true;
            // 
            // Bucket
            // 
            this.Bucket.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Bucket.HeaderText = "Ковш";
            this.Bucket.Name = "Bucket";
            this.Bucket.ReadOnly = true;
            // 
            // Driver
            // 
            this.Driver.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Driver.HeaderText = "Водитель";
            this.Driver.Name = "Driver";
            this.Driver.ReadOnly = true;
            // 
            // Start
            // 
            this.Start.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Start.HeaderText = "Начало смены";
            this.Start.Name = "Start";
            this.Start.ReadOnly = true;
            this.Start.Width = 50;
            // 
            // End
            // 
            this.End.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.End.HeaderText = "Конец смены";
            this.End.Name = "End";
            this.End.ReadOnly = true;
            this.End.Width = 47;
            // 
            // number_of_buckets
            // 
            this.number_of_buckets.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.number_of_buckets.HeaderText = "Кол-во ковшов";
            this.number_of_buckets.Name = "number_of_buckets";
            this.number_of_buckets.ReadOnly = true;
            // 
            // average_buckets
            // 
            this.average_buckets.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.average_buckets.HeaderText = "Cреднее значение ковш/машина";
            this.average_buckets.Name = "average_buckets";
            this.average_buckets.ReadOnly = true;
            // 
            // number_of_cars
            // 
            this.number_of_cars.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.number_of_cars.HeaderText = "Кол-во машин";
            this.number_of_cars.Name = "number_of_cars";
            this.number_of_cars.ReadOnly = true;
            // 
            // opt
            // 
            this.opt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.opt.HeaderText = "";
            this.opt.Image = global::order_accounting.Properties.Resources.search;
            this.opt.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.opt.Name = "opt";
            this.opt.ReadOnly = true;
            this.opt.Width = 5;
            // 
            // Delete
            // 
            this.Delete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Delete.HeaderText = "";
            this.Delete.Image = global::order_accounting.Properties.Resources.delete;
            this.Delete.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            this.Delete.Width = 5;
            // 
            // sqLiteCommand1
            // 
            this.sqLiteCommand1.CommandText = null;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = global::order_accounting.Properties.Resources.search;
            this.dataGridViewImageColumn1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewImageColumn2.HeaderText = "";
            this.dataGridViewImageColumn2.Image = global::order_accounting.Properties.Resources.delete;
            this.dataGridViewImageColumn2.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            // 
            // Duty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 601);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Duty";
            this.Text = "Duty";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Data.SQLite.SQLiteCommand sqLiteCommand1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn number;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn excavator;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bucket;
        private System.Windows.Forms.DataGridViewTextBoxColumn Driver;
        private System.Windows.Forms.DataGridViewTextBoxColumn Start;
        private System.Windows.Forms.DataGridViewTextBoxColumn End;
        private System.Windows.Forms.DataGridViewTextBoxColumn number_of_buckets;
        private System.Windows.Forms.DataGridViewTextBoxColumn average_buckets;
        private System.Windows.Forms.DataGridViewTextBoxColumn number_of_cars;
        private System.Windows.Forms.DataGridViewImageColumn opt;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
    }
}