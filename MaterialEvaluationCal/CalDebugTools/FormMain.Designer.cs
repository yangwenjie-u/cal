namespace CalDebugTools
{
    partial class FormMain
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
            this.label1 = new System.Windows.Forms.Label();
            this.pan_table = new System.Windows.Forms.Panel();
            this.txt_wtdbh = new System.Windows.Forms.TextBox();
            this.ck_other = new System.Windows.Forms.CheckBox();
            this.txtdatafiled = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_y = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_yjdbh_to = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_jydbh = new System.Windows.Forms.TextBox();
            this.jyd编号 = new System.Windows.Forms.Label();
            this.btn_Run = new System.Windows.Forms.Button();
            this.btn_Complie = new System.Windows.Forms.Button();
            this.btn_Debug = new System.Windows.Forms.Button();
            this.txt_helper = new System.Windows.Forms.TextBox();
            this.txt_s = new System.Windows.Forms.TextBox();
            this.txt_m = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_jcxmbh = new System.Windows.Forms.TextBox();
            this.tacDebug = new System.Windows.Forms.TabControl();
            this.tapCode = new System.Windows.Forms.TabPage();
            this.ritCode = new System.Windows.Forms.RichTextBox();
            this.tab_debug = new System.Windows.Forms.TabPage();
            this.ritResult = new System.Windows.Forms.TextBox();
            this.tapCompare = new System.Windows.Forms.TabPage();
            this.dataGridViewResult = new System.Windows.Forms.DataGridView();
            this.tab_batch = new System.Windows.Forms.TabPage();
            this.DataGridViewRowBitch = new System.Windows.Forms.DataGridView();
            this.项目管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.查看ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.项目管理2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_StrConver = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_StrReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.zdzd表数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_AddFields = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_uploadFields = new System.Windows.Forms.ToolStripMenuItem();
            this.代码上传ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.数据库配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_fieldSet = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_dataFieldSync = new System.Windows.Forms.ToolStripMenuItem();
            this.pan_table.SuspendLayout();
            this.tacDebug.SuspendLayout();
            this.tapCode.SuspendLayout();
            this.tab_debug.SuspendLayout();
            this.tapCompare.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResult)).BeginInit();
            this.tab_batch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewRowBitch)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择项目编号：";
            // 
            // pan_table
            // 
            this.pan_table.Controls.Add(this.txt_wtdbh);
            this.pan_table.Controls.Add(this.ck_other);
            this.pan_table.Controls.Add(this.txtdatafiled);
            this.pan_table.Controls.Add(this.label7);
            this.pan_table.Controls.Add(this.txt_y);
            this.pan_table.Controls.Add(this.label6);
            this.pan_table.Controls.Add(this.txt_yjdbh_to);
            this.pan_table.Controls.Add(this.label5);
            this.pan_table.Controls.Add(this.txt_jydbh);
            this.pan_table.Controls.Add(this.jyd编号);
            this.pan_table.Controls.Add(this.btn_Run);
            this.pan_table.Controls.Add(this.btn_Complie);
            this.pan_table.Controls.Add(this.btn_Debug);
            this.pan_table.Controls.Add(this.txt_helper);
            this.pan_table.Controls.Add(this.txt_s);
            this.pan_table.Controls.Add(this.txt_m);
            this.pan_table.Controls.Add(this.label4);
            this.pan_table.Controls.Add(this.label3);
            this.pan_table.Controls.Add(this.label2);
            this.pan_table.Controls.Add(this.txt_jcxmbh);
            this.pan_table.Controls.Add(this.label1);
            this.pan_table.Dock = System.Windows.Forms.DockStyle.Top;
            this.pan_table.Location = new System.Drawing.Point(0, 28);
            this.pan_table.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pan_table.Name = "pan_table";
            this.pan_table.Size = new System.Drawing.Size(1277, 129);
            this.pan_table.TabIndex = 1;
            this.pan_table.Paint += new System.Windows.Forms.PaintEventHandler(this.pan_table_Paint);
            // 
            // txt_wtdbh
            // 
            this.txt_wtdbh.Location = new System.Drawing.Point(993, 16);
            this.txt_wtdbh.Margin = new System.Windows.Forms.Padding(4);
            this.txt_wtdbh.Name = "txt_wtdbh";
            this.txt_wtdbh.Size = new System.Drawing.Size(132, 25);
            this.txt_wtdbh.TabIndex = 25;
            // 
            // ck_other
            // 
            this.ck_other.AutoSize = true;
            this.ck_other.Location = new System.Drawing.Point(881, 22);
            this.ck_other.Margin = new System.Windows.Forms.Padding(4);
            this.ck_other.Name = "ck_other";
            this.ck_other.Size = new System.Drawing.Size(89, 19);
            this.ck_other.TabIndex = 24;
            this.ck_other.Text = "乌海数据";
            this.ck_other.UseVisualStyleBackColor = true;
            this.ck_other.CheckedChanged += new System.EventHandler(this.ck_other_CheckedChanged);
            // 
            // txtdatafiled
            // 
            this.txtdatafiled.Enabled = false;
            this.txtdatafiled.Location = new System.Drawing.Point(755, 60);
            this.txtdatafiled.Margin = new System.Windows.Forms.Padding(4);
            this.txtdatafiled.Name = "txtdatafiled";
            this.txtdatafiled.Size = new System.Drawing.Size(132, 25);
            this.txtdatafiled.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(625, 65);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 15);
            this.label7.TabIndex = 23;
            this.label7.Text = "数据表查询字段：";
            // 
            // txt_y
            // 
            this.txt_y.Enabled = false;
            this.txt_y.Location = new System.Drawing.Point(531, 61);
            this.txt_y.Margin = new System.Windows.Forms.Padding(4);
            this.txt_y.Name = "txt_y";
            this.txt_y.Size = new System.Drawing.Size(84, 25);
            this.txt_y.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(468, 65);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 15);
            this.label6.TabIndex = 21;
            this.label6.Text = "数据表";
            // 
            // txt_yjdbh_to
            // 
            this.txt_yjdbh_to.Location = new System.Drawing.Point(591, 18);
            this.txt_yjdbh_to.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_yjdbh_to.Name = "txt_yjdbh_to";
            this.txt_yjdbh_to.Size = new System.Drawing.Size(100, 25);
            this.txt_yjdbh_to.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(527, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 15);
            this.label5.TabIndex = 19;
            this.label5.Text = "to";
            // 
            // txt_jydbh
            // 
            this.txt_jydbh.Location = new System.Drawing.Point(384, 21);
            this.txt_jydbh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_jydbh.Name = "txt_jydbh";
            this.txt_jydbh.Size = new System.Drawing.Size(100, 25);
            this.txt_jydbh.TabIndex = 18;
            // 
            // jyd编号
            // 
            this.jyd编号.AutoSize = true;
            this.jyd编号.Location = new System.Drawing.Point(284, 25);
            this.jyd编号.Name = "jyd编号";
            this.jyd编号.Size = new System.Drawing.Size(77, 15);
            this.jyd编号.TabIndex = 17;
            this.jyd编号.Text = "赤峰JYDBH";
            // 
            // btn_Run
            // 
            this.btn_Run.Location = new System.Drawing.Point(1161, 61);
            this.btn_Run.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Run.Name = "btn_Run";
            this.btn_Run.Size = new System.Drawing.Size(80, 29);
            this.btn_Run.TabIndex = 16;
            this.btn_Run.Text = "运行";
            this.btn_Run.UseVisualStyleBackColor = true;
            this.btn_Run.Click += new System.EventHandler(this.btn_Run_Click);
            // 
            // btn_Complie
            // 
            this.btn_Complie.Location = new System.Drawing.Point(948, 61);
            this.btn_Complie.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Complie.Name = "btn_Complie";
            this.btn_Complie.Size = new System.Drawing.Size(75, 29);
            this.btn_Complie.TabIndex = 15;
            this.btn_Complie.Text = "编译";
            this.btn_Complie.UseVisualStyleBackColor = true;
            this.btn_Complie.Click += new System.EventHandler(this.btn_Complie_Click);
            // 
            // btn_Debug
            // 
            this.btn_Debug.Location = new System.Drawing.Point(1043, 61);
            this.btn_Debug.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Debug.Name = "btn_Debug";
            this.btn_Debug.Size = new System.Drawing.Size(84, 28);
            this.btn_Debug.TabIndex = 14;
            this.btn_Debug.Text = "调试";
            this.btn_Debug.UseVisualStyleBackColor = true;
            this.btn_Debug.Click += new System.EventHandler(this.btn_Debug_Click);
            // 
            // txt_helper
            // 
            this.txt_helper.Enabled = false;
            this.txt_helper.Location = new System.Drawing.Point(333, 62);
            this.txt_helper.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_helper.Name = "txt_helper";
            this.txt_helper.Size = new System.Drawing.Size(127, 25);
            this.txt_helper.TabIndex = 13;
            // 
            // txt_s
            // 
            this.txt_s.Enabled = false;
            this.txt_s.Location = new System.Drawing.Point(193, 60);
            this.txt_s.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_s.Name = "txt_s";
            this.txt_s.Size = new System.Drawing.Size(57, 25);
            this.txt_s.TabIndex = 12;
            // 
            // txt_m
            // 
            this.txt_m.Enabled = false;
            this.txt_m.Location = new System.Drawing.Point(76, 62);
            this.txt_m.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_m.Name = "txt_m";
            this.txt_m.Size = new System.Drawing.Size(57, 25);
            this.txt_m.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(257, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "帮助表：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(149, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "从表：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "主表：";
            // 
            // txt_jcxmbh
            // 
            this.txt_jcxmbh.Location = new System.Drawing.Point(152, 21);
            this.txt_jcxmbh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_jcxmbh.Name = "txt_jcxmbh";
            this.txt_jcxmbh.Size = new System.Drawing.Size(63, 25);
            this.txt_jcxmbh.TabIndex = 1;
            this.txt_jcxmbh.Leave += new System.EventHandler(this.txt_jcxmbh_Leave);
            // 
            // tacDebug
            // 
            this.tacDebug.Controls.Add(this.tapCode);
            this.tacDebug.Controls.Add(this.tab_debug);
            this.tacDebug.Controls.Add(this.tapCompare);
            this.tacDebug.Controls.Add(this.tab_batch);
            this.tacDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tacDebug.Location = new System.Drawing.Point(0, 157);
            this.tacDebug.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tacDebug.Name = "tacDebug";
            this.tacDebug.SelectedIndex = 0;
            this.tacDebug.Size = new System.Drawing.Size(1277, 471);
            this.tacDebug.TabIndex = 3;
            // 
            // tapCode
            // 
            this.tapCode.Controls.Add(this.ritCode);
            this.tapCode.Location = new System.Drawing.Point(4, 25);
            this.tapCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tapCode.Name = "tapCode";
            this.tapCode.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tapCode.Size = new System.Drawing.Size(1269, 442);
            this.tapCode.TabIndex = 0;
            this.tapCode.Text = "代码";
            this.tapCode.UseVisualStyleBackColor = true;
            // 
            // ritCode
            // 
            this.ritCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ritCode.Location = new System.Drawing.Point(3, 2);
            this.ritCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ritCode.Name = "ritCode";
            this.ritCode.Size = new System.Drawing.Size(1263, 438);
            this.ritCode.TabIndex = 1;
            this.ritCode.Text = "";
            this.ritCode.TextChanged += new System.EventHandler(this.ritCode_TextChanged);
            // 
            // tab_debug
            // 
            this.tab_debug.Controls.Add(this.ritResult);
            this.tab_debug.Location = new System.Drawing.Point(4, 25);
            this.tab_debug.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tab_debug.Name = "tab_debug";
            this.tab_debug.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tab_debug.Size = new System.Drawing.Size(1269, 442);
            this.tab_debug.TabIndex = 1;
            this.tab_debug.Text = "调试输出";
            this.tab_debug.UseVisualStyleBackColor = true;
            // 
            // ritResult
            // 
            this.ritResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ritResult.Location = new System.Drawing.Point(3, 2);
            this.ritResult.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ritResult.Multiline = true;
            this.ritResult.Name = "ritResult";
            this.ritResult.Size = new System.Drawing.Size(1263, 438);
            this.ritResult.TabIndex = 0;
            // 
            // tapCompare
            // 
            this.tapCompare.Controls.Add(this.dataGridViewResult);
            this.tapCompare.Location = new System.Drawing.Point(4, 25);
            this.tapCompare.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tapCompare.Name = "tapCompare";
            this.tapCompare.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tapCompare.Size = new System.Drawing.Size(1269, 442);
            this.tapCompare.TabIndex = 2;
            this.tapCompare.Text = "数据比较";
            this.tapCompare.UseVisualStyleBackColor = true;
            // 
            // dataGridViewResult
            // 
            this.dataGridViewResult.AllowUserToAddRows = false;
            this.dataGridViewResult.AllowUserToDeleteRows = false;
            this.dataGridViewResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewResult.Location = new System.Drawing.Point(3, 2);
            this.dataGridViewResult.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewResult.Name = "dataGridViewResult";
            this.dataGridViewResult.ReadOnly = true;
            this.dataGridViewResult.RowHeadersWidth = 51;
            this.dataGridViewResult.RowTemplate.Height = 27;
            this.dataGridViewResult.Size = new System.Drawing.Size(1263, 438);
            this.dataGridViewResult.TabIndex = 0;
            this.dataGridViewResult.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridViewResult_CellPainting);
            // 
            // tab_batch
            // 
            this.tab_batch.Controls.Add(this.DataGridViewRowBitch);
            this.tab_batch.Location = new System.Drawing.Point(4, 25);
            this.tab_batch.Margin = new System.Windows.Forms.Padding(4);
            this.tab_batch.Name = "tab_batch";
            this.tab_batch.Padding = new System.Windows.Forms.Padding(4);
            this.tab_batch.Size = new System.Drawing.Size(1269, 442);
            this.tab_batch.TabIndex = 3;
            this.tab_batch.Text = "批量调试";
            this.tab_batch.UseVisualStyleBackColor = true;
            // 
            // DataGridViewRowBitch
            // 
            this.DataGridViewRowBitch.AllowUserToAddRows = false;
            this.DataGridViewRowBitch.AllowUserToDeleteRows = false;
            this.DataGridViewRowBitch.AllowUserToOrderColumns = true;
            this.DataGridViewRowBitch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridViewRowBitch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridViewRowBitch.Location = new System.Drawing.Point(4, 4);
            this.DataGridViewRowBitch.Margin = new System.Windows.Forms.Padding(4);
            this.DataGridViewRowBitch.Name = "DataGridViewRowBitch";
            this.DataGridViewRowBitch.ReadOnly = true;
            this.DataGridViewRowBitch.RowTemplate.Height = 23;
            this.DataGridViewRowBitch.Size = new System.Drawing.Size(1261, 434);
            this.DataGridViewRowBitch.TabIndex = 0;
            this.DataGridViewRowBitch.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridViewRowBitch_CellMouseDoubleClick);
            // 
            // 项目管理ToolStripMenuItem
            // 
            this.项目管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProjectAdd,
            this.查看ToolStripMenuItem});
            this.项目管理ToolStripMenuItem.Name = "项目管理ToolStripMenuItem";
            this.项目管理ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.项目管理ToolStripMenuItem.Text = "项目管理";
            // 
            // ProjectAdd
            // 
            this.ProjectAdd.Name = "ProjectAdd";
            this.ProjectAdd.Size = new System.Drawing.Size(144, 26);
            this.ProjectAdd.Text = "新增项目";
            this.ProjectAdd.Click += new System.EventHandler(this.ProjectAdd_Click);
            // 
            // 查看ToolStripMenuItem
            // 
            this.查看ToolStripMenuItem.Name = "查看ToolStripMenuItem";
            this.查看ToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.查看ToolStripMenuItem.Text = "上传字段";
            this.查看ToolStripMenuItem.Click += new System.EventHandler(this.查看ToolStripMenuItem_Click);
            // 
            // 项目管理2ToolStripMenuItem
            // 
            this.项目管理2ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tool_StrConver,
            this.tool_StrReplace,
            this.zdzd表数据ToolStripMenuItem});
            this.项目管理2ToolStripMenuItem.Name = "项目管理2ToolStripMenuItem";
            this.项目管理2ToolStripMenuItem.Size = new System.Drawing.Size(66, 24);
            this.项目管理2ToolStripMenuItem.Text = "小工具";
            // 
            // tool_StrConver
            // 
            this.tool_StrConver.Name = "tool_StrConver";
            this.tool_StrConver.Size = new System.Drawing.Size(216, 26);
            this.tool_StrConver.Text = "字符串转换";
            this.tool_StrConver.Click += new System.EventHandler(this.tool_StrConver_Click);
            // 
            // tool_StrReplace
            // 
            this.tool_StrReplace.Name = "tool_StrReplace";
            this.tool_StrReplace.Size = new System.Drawing.Size(216, 26);
            this.tool_StrReplace.Text = "批量替换";
            this.tool_StrReplace.Click += new System.EventHandler(this.tool_StrReplace_Click);
            // 
            // zdzd表数据ToolStripMenuItem
            // 
            this.zdzd表数据ToolStripMenuItem.Name = "zdzd表数据ToolStripMenuItem";
            this.zdzd表数据ToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.zdzd表数据ToolStripMenuItem.Text = "zdzd表数据同步";
            this.zdzd表数据ToolStripMenuItem.Click += new System.EventHandler(this.zdzd表数据ToolStripMenuItem_Click);
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tool_AddFields,
            this.tool_uploadFields,
            this.代码上传ToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(66, 24);
            this.设置ToolStripMenuItem.Text = "数据库";
            // 
            // tool_AddFields
            // 
            this.tool_AddFields.Name = "tool_AddFields";
            this.tool_AddFields.Size = new System.Drawing.Size(216, 26);
            this.tool_AddFields.Text = "添加字段";
            this.tool_AddFields.Click += new System.EventHandler(this.tool_AddFields_Click);
            // 
            // tool_uploadFields
            // 
            this.tool_uploadFields.Name = "tool_uploadFields";
            this.tool_uploadFields.Size = new System.Drawing.Size(216, 26);
            this.tool_uploadFields.Text = "字段上传";
            this.tool_uploadFields.Click += new System.EventHandler(this.tool_uploadFields_Click_1);
            // 
            // 代码上传ToolStripMenuItem
            // 
            this.代码上传ToolStripMenuItem.Name = "代码上传ToolStripMenuItem";
            this.代码上传ToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.代码上传ToolStripMenuItem.Text = "代码上传";
            this.代码上传ToolStripMenuItem.Click += new System.EventHandler(this.代码上传ToolStripMenuItem_Click_1);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.项目管理ToolStripMenuItem,
            this.项目管理2ToolStripMenuItem,
            this.设置ToolStripMenuItem,
            this.数据库配置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1277, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 数据库配置ToolStripMenuItem
            // 
            this.数据库配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tool_fieldSet,
            this.tool_dataFieldSync});
            this.数据库配置ToolStripMenuItem.Name = "数据库配置ToolStripMenuItem";
            this.数据库配置ToolStripMenuItem.Size = new System.Drawing.Size(96, 24);
            this.数据库配置ToolStripMenuItem.Text = "数据库配置";
            // 
            // tool_fieldSet
            // 
            this.tool_fieldSet.Name = "tool_fieldSet";
            this.tool_fieldSet.Size = new System.Drawing.Size(189, 26);
            this.tool_fieldSet.Text = "字段配置";
            this.tool_fieldSet.Click += new System.EventHandler(this.tool_fieldSet_Click);
            // 
            // tool_dataFieldSync
            // 
            this.tool_dataFieldSync.Name = "tool_dataFieldSync";
            this.tool_dataFieldSync.Size = new System.Drawing.Size(189, 26);
            this.tool_dataFieldSync.Text = "数据表字段同步";
            this.tool_dataFieldSync.Click += new System.EventHandler(this.tool_dataFieldSync_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1277, 628);
            this.Controls.Add(this.tacDebug);
            this.Controls.Add(this.pan_table);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormMain";
            this.Text = "Form1";
            this.pan_table.ResumeLayout(false);
            this.pan_table.PerformLayout();
            this.tacDebug.ResumeLayout(false);
            this.tapCode.ResumeLayout(false);
            this.tab_debug.ResumeLayout(false);
            this.tab_debug.PerformLayout();
            this.tapCompare.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResult)).EndInit();
            this.tab_batch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewRowBitch)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pan_table;
        private System.Windows.Forms.TextBox txt_jcxmbh;
        private System.Windows.Forms.Button btn_Debug;
        private System.Windows.Forms.TextBox txt_helper;
        private System.Windows.Forms.TextBox txt_s;
        private System.Windows.Forms.TextBox txt_m;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Complie;
        private System.Windows.Forms.TabControl tacDebug;
        private System.Windows.Forms.TabPage tapCode;
        private System.Windows.Forms.TabPage tab_debug;
        private System.Windows.Forms.TextBox ritResult;
        private System.Windows.Forms.TabPage tapCompare;
        private System.Windows.Forms.DataGridView dataGridViewResult;
        private System.Windows.Forms.ToolStripMenuItem 项目管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ProjectAdd;
        private System.Windows.Forms.ToolStripMenuItem 查看ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 项目管理2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Button btn_Run;
        private System.Windows.Forms.ToolStripMenuItem tool_AddFields;
        private System.Windows.Forms.ToolStripMenuItem tool_uploadFields;
        private System.Windows.Forms.ToolStripMenuItem 代码上传ToolStripMenuItem;
        private System.Windows.Forms.TextBox txt_jydbh;
        private System.Windows.Forms.Label jyd编号;
        private System.Windows.Forms.RichTextBox ritCode;
        private System.Windows.Forms.TextBox txt_yjdbh_to;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tab_batch;
        private System.Windows.Forms.DataGridView DataGridViewRowBitch;
        private System.Windows.Forms.TextBox txt_y;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtdatafiled;
        private System.Windows.Forms.CheckBox ck_other;
        private System.Windows.Forms.TextBox txt_wtdbh;
        private System.Windows.Forms.ToolStripMenuItem 数据库配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tool_fieldSet;
        private System.Windows.Forms.ToolStripMenuItem tool_StrConver;
        private System.Windows.Forms.ToolStripMenuItem tool_StrReplace;
        private System.Windows.Forms.ToolStripMenuItem tool_dataFieldSync;
        private System.Windows.Forms.ToolStripMenuItem zdzd表数据ToolStripMenuItem;
    }
}

