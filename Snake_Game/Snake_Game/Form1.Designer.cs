
namespace Snake_Game
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Update = new System.Windows.Forms.Timer(this.components);
            this.toStartText = new System.Windows.Forms.Label();
            this.snakeText = new System.Windows.Forms.Label();
            this.snakePicture = new System.Windows.Forms.PictureBox();
            this.primaryGrupText = new System.Windows.Forms.GroupBox();
            this.timerBot = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.snakePicture)).BeginInit();
            this.primaryGrupText.SuspendLayout();
            this.SuspendLayout();
            // 
            // Update
            // 
            this.Update.Interval = 200;
            this.Update.Tick += new System.EventHandler(this.Update_Tick);
            // 
            // toStartText
            // 
            this.toStartText.AutoSize = true;
            this.toStartText.BackColor = System.Drawing.Color.Transparent;
            this.toStartText.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toStartText.ForeColor = System.Drawing.Color.Red;
            this.toStartText.Location = new System.Drawing.Point(42, 83);
            this.toStartText.Name = "toStartText";
            this.toStartText.Size = new System.Drawing.Size(351, 51);
            this.toStartText.TabIndex = 4;
            this.toStartText.Text = " Enter to start";
            // 
            // snakeText
            // 
            this.snakeText.AutoSize = true;
            this.snakeText.BackColor = System.Drawing.Color.Transparent;
            this.snakeText.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.snakeText.ForeColor = System.Drawing.Color.Red;
            this.snakeText.Location = new System.Drawing.Point(110, 24);
            this.snakeText.Name = "snakeText";
            this.snakeText.Size = new System.Drawing.Size(208, 69);
            this.snakeText.TabIndex = 5;
            this.snakeText.Text = "Snake";
            // 
            // snakePicture
            // 
            this.snakePicture.Image = global::Snake_Game.Properties.Resources.kisspng_computer_icons_reptile_clip_art_5afa83855ae8381;
            this.snakePicture.Location = new System.Drawing.Point(176, 93);
            this.snakePicture.Name = "snakePicture";
            this.snakePicture.Size = new System.Drawing.Size(119, 88);
            this.snakePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.snakePicture.TabIndex = 6;
            this.snakePicture.TabStop = false;
            // 
            // primaryGrupText
            // 
            this.primaryGrupText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(255)))), ((int)(((byte)(129)))));
            this.primaryGrupText.Controls.Add(this.toStartText);
            this.primaryGrupText.Controls.Add(this.snakeText);
            this.primaryGrupText.Location = new System.Drawing.Point(25, 156);
            this.primaryGrupText.Name = "primaryGrupText";
            this.primaryGrupText.Size = new System.Drawing.Size(434, 156);
            this.primaryGrupText.TabIndex = 7;
            this.primaryGrupText.TabStop = false;
            // 
            // timerBot
            // 
            this.timerBot.Interval = 3500;
            this.timerBot.Tick += new System.EventHandler(this.timerBot_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.primaryGrupText);
            this.Controls.Add(this.snakePicture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Snake";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WASD);
            ((System.ComponentModel.ISupportInitialize)(this.snakePicture)).EndInit();
            this.primaryGrupText.ResumeLayout(false);
            this.primaryGrupText.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private new System.Windows.Forms.Timer Update;
        private System.Windows.Forms.Label toStartText;
        private System.Windows.Forms.Label snakeText;
        private System.Windows.Forms.PictureBox snakePicture;
        private System.Windows.Forms.GroupBox primaryGrupText;
        private System.Windows.Forms.Timer timerBot;
    }
}

