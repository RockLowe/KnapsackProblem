/*************************************************************/
/*                                                           */
/*  Course: CIS 430 - Artificial Intelligence                */
/*                                                           */
/*  Project: Ch3Prb3.CSPrj                                   */
/*                                                           */
/*  Source File: Ch3Prb3.CS                                  */
/*                                                           */
/*  Programmer: Rocky Lowery                                 */
/*                                                           */
/*  Purpose: Recursive solution of the 0-1 Knapsack Problem  */
/*           using minimal pruning. This is a O(2^n)         */
/*           algorithm.                                      */
/*                                                           */
/*  Classes: 1. Ch3Prb3Form : Form                           */
/*           2. Ch3Prb3App                                   */
/*           3. Item                                         */
/*                                                           */
/*************************************************************/

using System;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;
using LibUtil;
using LibGArrayList;

/************************************************************/
/*  Begin partial namespace Ch3Prb3                         */
/************************************************************/
namespace Ch3Prb3
{

  /************************************************************/
  /*  1. Begin main form class Ch3Prb3Form : Form            */
  /************************************************************/
  public class Ch3Prb3Form : Form
  {
    const string DATA_FILE_NAME = "KnapsackDat.Txt";

    private MenuStrip mainMenu;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem openDataToolStripMenuItem;
    private ToolStripMenuItem exitToolStripMenuItem;
    private ToolStripMenuItem helpToolStripMenuItem;
    private ToolStripMenuItem aboutToolStripMenuItem;
    private Label solutionLabel;
    private Label numOfRecurCallsLabel;
    private Label numOfRecurCallsDisplay;
    private Label percentDisplay;
    private Label percentlabel;
    private Label possibleCallsDisplay;
    private Label possibleCallsLabel;
    private Button solveButton;
    private Button cancelButton;
    private Label numOfItemsDisplay;
    private Label numOfItemsLabel;
    private Label capacityDisplay;
    private Label capacityLabel;
    private Label elapsedTimeDisplay;
    private Label elapsedTimeLabel;
    private Label totalsLabel;
    private Label totWeightDisplay;
    private Label totProfitDisplay;
    private DataGridView dataGridView;
    private DataGridViewTextBoxColumn objectCol;
    private DataGridViewTextBoxColumn weightCol;
    private DataGridViewTextBoxColumn profitCol;
    private Timer elapsedTimeTimer;
    private BackgroundWorker bgWorker;
    private IContainer components;
    private bool bgWorkCancelled;
    private int numOfObjects, capacity, optProfit;
    private long possibleCalls, numOfRecurCalls;
    private double percent;
    private double elapsedTime;
    private int[,] xTable, optProfitTable;
    private GArrayList<Item> itemList = new GArrayList<Item>();
    private StreamReader fileIn;

    public Ch3Prb3Form()
    {
      InitializeComponent();
    }

    #region Windows Form Designer generated code
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ch3Prb3Form));
      this.solutionLabel = new System.Windows.Forms.Label();
      this.numOfRecurCallsLabel = new System.Windows.Forms.Label();
      this.numOfRecurCallsDisplay = new System.Windows.Forms.Label();
      this.solveButton = new System.Windows.Forms.Button();
      this.percentDisplay = new System.Windows.Forms.Label();
      this.percentlabel = new System.Windows.Forms.Label();
      this.possibleCallsDisplay = new System.Windows.Forms.Label();
      this.possibleCallsLabel = new System.Windows.Forms.Label();
      this.elapsedTimeDisplay = new System.Windows.Forms.Label();
      this.elapsedTimeLabel = new System.Windows.Forms.Label();
      this.dataGridView = new System.Windows.Forms.DataGridView();
      this.objectCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.weightCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.profitCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.totalsLabel = new System.Windows.Forms.Label();
      this.totWeightDisplay = new System.Windows.Forms.Label();
      this.totProfitDisplay = new System.Windows.Forms.Label();
      this.elapsedTimeTimer = new System.Windows.Forms.Timer(this.components);
      this.mainMenu = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.numOfItemsDisplay = new System.Windows.Forms.Label();
      this.numOfItemsLabel = new System.Windows.Forms.Label();
      this.capacityDisplay = new System.Windows.Forms.Label();
      this.capacityLabel = new System.Windows.Forms.Label();
      this.bgWorker = new System.ComponentModel.BackgroundWorker();
      this.cancelButton = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
      this.mainMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // solutionLabel
      // 
      this.solutionLabel.Location = new System.Drawing.Point(100, 36);
      this.solutionLabel.Name = "solutionLabel";
      this.solutionLabel.Size = new System.Drawing.Size(200, 24);
      this.solutionLabel.TabIndex = 2;
      this.solutionLabel.Text = "Optimal Loading";
      this.solutionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // numOfRecurCallsLabel
      // 
      this.numOfRecurCallsLabel.Location = new System.Drawing.Point(345, 150);
      this.numOfRecurCallsLabel.Name = "numOfRecurCallsLabel";
      this.numOfRecurCallsLabel.Size = new System.Drawing.Size(179, 24);
      this.numOfRecurCallsLabel.TabIndex = 10;
      this.numOfRecurCallsLabel.Text = "Recursive Calls";
      this.numOfRecurCallsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // numOfRecurCallsDisplay
      // 
      this.numOfRecurCallsDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.numOfRecurCallsDisplay.Location = new System.Drawing.Point(345, 175);
      this.numOfRecurCallsDisplay.Name = "numOfRecurCallsDisplay";
      this.numOfRecurCallsDisplay.Size = new System.Drawing.Size(179, 23);
      this.numOfRecurCallsDisplay.TabIndex = 11;
      this.numOfRecurCallsDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // solveButton
      // 
      this.solveButton.BackColor = System.Drawing.SystemColors.Control;
      this.solveButton.ForeColor = System.Drawing.SystemColors.ControlText;
      this.solveButton.Location = new System.Drawing.Point(397, 379);
      this.solveButton.Name = "solveButton";
      this.solveButton.Size = new System.Drawing.Size(75, 22);
      this.solveButton.TabIndex = 0;
      this.solveButton.Text = "Solve";
      this.solveButton.UseVisualStyleBackColor = false;
      this.solveButton.Click += new System.EventHandler(this.solveButton_Click);
      // 
      // percentDisplay
      // 
      this.percentDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.percentDisplay.Location = new System.Drawing.Point(345, 287);
      this.percentDisplay.Name = "percentDisplay";
      this.percentDisplay.Size = new System.Drawing.Size(179, 23);
      this.percentDisplay.TabIndex = 15;
      this.percentDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // percentlabel
      // 
      this.percentlabel.Location = new System.Drawing.Point(345, 263);
      this.percentlabel.Name = "percentlabel";
      this.percentlabel.Size = new System.Drawing.Size(178, 24);
      this.percentlabel.TabIndex = 14;
      this.percentlabel.Text = "Percent Processed";
      this.percentlabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // possibleCallsDisplay
      // 
      this.possibleCallsDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.possibleCallsDisplay.Location = new System.Drawing.Point(345, 231);
      this.possibleCallsDisplay.Name = "possibleCallsDisplay";
      this.possibleCallsDisplay.Size = new System.Drawing.Size(179, 23);
      this.possibleCallsDisplay.TabIndex = 13;
      this.possibleCallsDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // possibleCallsLabel
      // 
      this.possibleCallsLabel.Location = new System.Drawing.Point(345, 207);
      this.possibleCallsLabel.Name = "possibleCallsLabel";
      this.possibleCallsLabel.Size = new System.Drawing.Size(179, 24);
      this.possibleCallsLabel.TabIndex = 12;
      this.possibleCallsLabel.Text = "Possible Calls";
      this.possibleCallsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // elapsedTimeDisplay
      // 
      this.elapsedTimeDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.elapsedTimeDisplay.Location = new System.Drawing.Point(345, 343);
      this.elapsedTimeDisplay.Name = "elapsedTimeDisplay";
      this.elapsedTimeDisplay.Size = new System.Drawing.Size(179, 23);
      this.elapsedTimeDisplay.TabIndex = 17;
      this.elapsedTimeDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // elapsedTimeLabel
      // 
      this.elapsedTimeLabel.Location = new System.Drawing.Point(345, 319);
      this.elapsedTimeLabel.Name = "elapsedTimeLabel";
      this.elapsedTimeLabel.Size = new System.Drawing.Size(179, 24);
      this.elapsedTimeLabel.TabIndex = 16;
      this.elapsedTimeLabel.Text = "Elapsed Time";
      this.elapsedTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // dataGridView
      // 
      this.dataGridView.AllowUserToAddRows = false;
      this.dataGridView.AllowUserToDeleteRows = false;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.objectCol,
            this.weightCol,
            this.profitCol});
      this.dataGridView.Location = new System.Drawing.Point(100, 61);
      this.dataGridView.MultiSelect = false;
      this.dataGridView.Name = "dataGridView";
      this.dataGridView.ReadOnly = true;
      this.dataGridView.RowHeadersVisible = false;
      this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dataGridView.Size = new System.Drawing.Size(200, 308);
      this.dataGridView.TabIndex = 3;
      // 
      // objectCol
      // 
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      this.objectCol.DefaultCellStyle = dataGridViewCellStyle2;
      this.objectCol.HeaderText = "Object";
      this.objectCol.Name = "objectCol";
      this.objectCol.ReadOnly = true;
      this.objectCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      this.objectCol.Width = 60;
      // 
      // weightCol
      // 
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      this.weightCol.DefaultCellStyle = dataGridViewCellStyle3;
      this.weightCol.HeaderText = "Weight";
      this.weightCol.Name = "weightCol";
      this.weightCol.ReadOnly = true;
      this.weightCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      this.weightCol.Width = 60;
      // 
      // profitCol
      // 
      dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      this.profitCol.DefaultCellStyle = dataGridViewCellStyle4;
      this.profitCol.HeaderText = "Profit";
      this.profitCol.Name = "profitCol";
      this.profitCol.ReadOnly = true;
      this.profitCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      this.profitCol.Width = 60;
      // 
      // totalsLabel
      // 
      this.totalsLabel.Location = new System.Drawing.Point(115, 379);
      this.totalsLabel.Name = "totalsLabel";
      this.totalsLabel.Size = new System.Drawing.Size(48, 24);
      this.totalsLabel.TabIndex = 4;
      this.totalsLabel.Text = "Totals";
      this.totalsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // totWeightDisplay
      // 
      this.totWeightDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.totWeightDisplay.Location = new System.Drawing.Point(165, 380);
      this.totWeightDisplay.Name = "totWeightDisplay";
      this.totWeightDisplay.Size = new System.Drawing.Size(50, 23);
      this.totWeightDisplay.TabIndex = 5;
      this.totWeightDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // totProfitDisplay
      // 
      this.totProfitDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.totProfitDisplay.Location = new System.Drawing.Point(225, 380);
      this.totProfitDisplay.Name = "totProfitDisplay";
      this.totProfitDisplay.Size = new System.Drawing.Size(50, 23);
      this.totProfitDisplay.TabIndex = 5;
      this.totProfitDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // elapsedTimeTimer
      // 
      this.elapsedTimeTimer.Interval = 1000;
      this.elapsedTimeTimer.Tick += new System.EventHandler(this.elapsedTimeTimer_Tick);
      // 
      // mainMenu
      // 
      this.mainMenu.BackColor = System.Drawing.SystemColors.Control;
      this.mainMenu.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
      this.mainMenu.Location = new System.Drawing.Point(0, 0);
      this.mainMenu.Name = "mainMenu";
      this.mainMenu.Size = new System.Drawing.Size(624, 24);
      this.mainMenu.TabIndex = 17;
      this.mainMenu.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDataToolStripMenuItem,
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "&File";
      // 
      // openDataToolStripMenuItem
      // 
      this.openDataToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
      this.openDataToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
      this.openDataToolStripMenuItem.Name = "openDataToolStripMenuItem";
      this.openDataToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
      this.openDataToolStripMenuItem.Text = "&Open Data";
      this.openDataToolStripMenuItem.Click += new System.EventHandler(this.openDataMenuItem_Click);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
      this.exitToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
      this.exitToolStripMenuItem.Text = "E&xit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
      // 
      // helpToolStripMenuItem
      // 
      this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
      this.helpToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
      this.helpToolStripMenuItem.Text = "&Help";
      // 
      // aboutToolStripMenuItem
      // 
      this.aboutToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
      this.aboutToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
      this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
      this.aboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
      this.aboutToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
      this.aboutToolStripMenuItem.Text = "&About...";
      this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
      // 
      // numOfItemsDisplay
      // 
      this.numOfItemsDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.numOfItemsDisplay.Location = new System.Drawing.Point(345, 61);
      this.numOfItemsDisplay.Name = "numOfItemsDisplay";
      this.numOfItemsDisplay.Size = new System.Drawing.Size(179, 23);
      this.numOfItemsDisplay.TabIndex = 7;
      this.numOfItemsDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // numOfItemsLabel
      // 
      this.numOfItemsLabel.Location = new System.Drawing.Point(345, 36);
      this.numOfItemsLabel.Name = "numOfItemsLabel";
      this.numOfItemsLabel.Size = new System.Drawing.Size(178, 24);
      this.numOfItemsLabel.TabIndex = 6;
      this.numOfItemsLabel.Text = "Number of Items";
      this.numOfItemsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // capacityDisplay
      // 
      this.capacityDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.capacityDisplay.Location = new System.Drawing.Point(345, 118);
      this.capacityDisplay.Name = "capacityDisplay";
      this.capacityDisplay.Size = new System.Drawing.Size(179, 23);
      this.capacityDisplay.TabIndex = 9;
      this.capacityDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // capacityLabel
      // 
      this.capacityLabel.Location = new System.Drawing.Point(346, 93);
      this.capacityLabel.Name = "capacityLabel";
      this.capacityLabel.Size = new System.Drawing.Size(177, 24);
      this.capacityLabel.TabIndex = 8;
      this.capacityLabel.Text = "Capacity";
      this.capacityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // bgWorker
      // 
      this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
      // 
      // cancelButton
      // 
      this.cancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
      this.cancelButton.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cancelButton.Location = new System.Drawing.Point(478, 379);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 22);
      this.cancelButton.TabIndex = 18;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.UseVisualStyleBackColor = false;
      this.cancelButton.Visible = false;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // Ch3Prb3Form
      // 
      this.AcceptButton = this.solveButton;
      this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
      this.ClientSize = new System.Drawing.Size(624, 411);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.capacityDisplay);
      this.Controls.Add(this.capacityLabel);
      this.Controls.Add(this.numOfItemsDisplay);
      this.Controls.Add(this.numOfItemsLabel);
      this.Controls.Add(this.totProfitDisplay);
      this.Controls.Add(this.totWeightDisplay);
      this.Controls.Add(this.totalsLabel);
      this.Controls.Add(this.dataGridView);
      this.Controls.Add(this.elapsedTimeDisplay);
      this.Controls.Add(this.elapsedTimeLabel);
      this.Controls.Add(this.possibleCallsDisplay);
      this.Controls.Add(this.possibleCallsLabel);
      this.Controls.Add(this.percentDisplay);
      this.Controls.Add(this.percentlabel);
      this.Controls.Add(this.solveButton);
      this.Controls.Add(this.numOfRecurCallsDisplay);
      this.Controls.Add(this.numOfRecurCallsLabel);
      this.Controls.Add(this.solutionLabel);
      this.Controls.Add(this.mainMenu);
      this.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mainMenu;
      this.MaximizeBox = false;
      this.Name = "Ch3Prb3Form";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Ch3Prb3– Recursive Solution of the 0,1 Knapsack Problem using Pruning - O(2^n)";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ch3Smp5Form_FormClosing);
      this.Load += new System.EventHandler(this.Ch3Prb3Form_Load);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
      this.mainMenu.ResumeLayout(false);
      this.mainMenu.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }
    #endregion

    /************************************************************/
    /*  Message Handlers                                        */
    /************************************************************/
    private void Ch3Prb3Form_Load(object sender, EventArgs e)
    {
      cancelButton.Location = solveButton.Location;
    }

    private void openDataMenuItem_Click(object sender, System.EventArgs e)
    {
      System.Diagnostics.Process.Start("Notepad", DATA_FILE_NAME);
    }

    private void exitMenuItem_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void aboutMenuItem_Click(object sender, System.EventArgs e)
    {
      AboutDialog aboutDialog = new AboutDialog();

      aboutDialog.ShowDialog(this);
    }

    private void solveButton_Click(object sender, System.EventArgs e)
    {
      DateTime startDateTime;
      TimeSpan timeSpan;

      solveButton.Visible = false;
      cancelButton.Visible = true;
      InputProblemDef();
      Initialize();
      elapsedTime = 0.0;
      startDateTime = DateTime.Now;
      elapsedTimeTimer.Enabled = true;
      bgWorker.RunWorkerAsync();  // Execute GenProfits() in the background.
      while (bgWorker.IsBusy)     // Continue responding to UI events while waiting for   
        Application.DoEvents();   // the background work to finish.
      elapsedTimeTimer.Enabled = false;
      timeSpan = DateTime.Now - startDateTime;
      elapsedTime = timeSpan.Hours * 3600.0 + timeSpan.Minutes * 60.0 +
                                 timeSpan.Seconds * 1.0 + timeSpan.Milliseconds / 1000.0;
      if (!bgWorkCancelled)
        DisplayResults();
      cancelButton.Visible = false;
      solveButton.Visible = true;
      solveButton.Select();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      bgWorkCancelled = true;
      numOfRecurCallsDisplay.Text = "";
      possibleCallsDisplay.Text = "";
      percentDisplay.Text = "";
      elapsedTimeDisplay.Text = "";
    }

    private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      bgWorkCancelled = false;
      optProfit = GenProfits(1, capacity);
    }

    private void elapsedTimeTimer_Tick(object sender, EventArgs e)
    {
      elapsedTime += 1.0;
      percent = 100.0 * numOfRecurCalls / possibleCalls;
      numOfRecurCallsDisplay.Text = numOfRecurCalls.ToString("n0");
      percentDisplay.Text = percent.ToString("f14") + "%";
      elapsedTimeDisplay.Text = elapsedTime.ToString("f3") + " sec";
    }

    private void Ch3Smp5Form_FormClosing(object sender, FormClosingEventArgs e)
    {
      bgWorkCancelled = true;
    }

    /************************************************************/
    /*  Auxiallary Methods                                      */
    /************************************************************/
    private void InputProblemDef()
    {
      string[] words;

      if (File.Exists(DATA_FILE_NAME))
        fileIn = File.OpenText(DATA_FILE_NAME);
      else
      {
        MessageBox.Show(DATA_FILE_NAME + " does not exist", "Abort Execution",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        Application.Exit();
      }
      itemList.Clear();
      fileIn.ReadLine();
      numOfObjects = Int32.Parse(fileIn.ReadLine());
      for (int n = 1; n <= numOfObjects; n++)
        itemList.Add(new Item(n, 0, 0, 0));
      fileIn.ReadLine();
      words = StringMethods.SpaceDelimit(fileIn.ReadLine()).Split(' ');
      for (int n = 1; n <= numOfObjects; n++)
        itemList[n].Weight = (Int32.Parse(words[n - 1]));
      fileIn.ReadLine();
      words = StringMethods.SpaceDelimit(fileIn.ReadLine()).Split(' ');
      for (int n = 1; n <= numOfObjects; n++)
        itemList[n].UnitProfit = (Int32.Parse(words[n - 1]));
      fileIn.ReadLine();
      capacity = Int32.Parse(fileIn.ReadLine());
      fileIn.Close();
    }

    private void Initialize()
    {
      dataGridView.Rows.Clear();
      numOfRecurCalls = 0;
      optProfit = 0;
      numOfItemsDisplay.Text = numOfObjects.ToString("n0");
      capacityDisplay.Text = capacity.ToString("n0");
      possibleCalls = (long)Math.Pow(2, numOfObjects + 1) - 1;
      possibleCallsDisplay.Text = possibleCalls.ToString("n0");
      xTable = new int[numOfObjects + 1, capacity + 1];
      optProfitTable = new int[numOfObjects + 1, capacity + 1];
      for (int i = 0; i < numOfObjects + 1; i++)
        for (int j = 0; j < capacity + 1; j++)
          optProfitTable[i, j] = -1;
      percentDisplay.Text = "";
      elapsedTimeDisplay.Text = "";
      totWeightDisplay.Text = "";
      totProfitDisplay.Text = "";
      numOfRecurCallsDisplay.Text = "";
    }

    private int GenProfits(int n, int remCapacity)
    {
      int fwdProfit1 = 0, fwdProfit0 = 0, fwdProfit;

      numOfRecurCalls++;
      fwdProfit = 0;
      if (!bgWorkCancelled)
      {
        if (n <= numOfObjects)
        {
          if (optProfitTable[n, remCapacity] == -1)
          {
            // Evaluate putting 0 in the pattern
            fwdProfit0 = GenProfits(n + 1, remCapacity);
            // Evaluate putting 1 in pattern
            if (itemList[n].Weight <= remCapacity)
            {
              fwdProfit1 = itemList[n].UnitProfit + GenProfits(n + 1, remCapacity - itemList[n].Weight);
            }
            // Record the better option, i.e. 1 in position n vs 0 in position n
            if (fwdProfit1 > fwdProfit0)
            {
              fwdProfit = fwdProfit1;
              xTable[n, remCapacity] = 1;
              optProfitTable[n, remCapacity] = fwdProfit;
            }
            else
            {
              fwdProfit = fwdProfit0;
              xTable[n, remCapacity] = 0;
              optProfitTable[n, remCapacity] = fwdProfit;
            }
          }
          else
            fwdProfit = optProfitTable[n, remCapacity];
        }
      }
      return fwdProfit;
    }

    private void DisplayResults()
    {
      int totWeight = 0, remCapacity, n;

      remCapacity = capacity;
      for (n = 1; n <= numOfObjects; n++)
        if (xTable[n, remCapacity] == 1)
        {
          itemList[n].OptX = 1;
          remCapacity -= itemList[n].Weight;
        }
      for (n = 1; n <= numOfObjects; n++)
        if (itemList[n].OptX == 1)
        {
          dataGridView.Rows.Add(itemList[n].Index.ToString(), itemList[n].Weight.ToString(),
                                itemList[n].UnitProfit.ToString());
          totWeight += itemList[n].Weight;
        }
      totWeightDisplay.Text = totWeight.ToString();
      totProfitDisplay.Text = optProfit.ToString();
      percent = 100.0 * numOfRecurCalls / possibleCalls;
      numOfRecurCallsDisplay.Text = numOfRecurCalls.ToString("n0");
      percentDisplay.Text = percent.ToString("f14") + "%";
      elapsedTimeDisplay.Text = elapsedTime.ToString("f3") + " sec";
      dataGridView.Select();
    }
  }  // End main form class Ch3Prb3Form

  /************************************************************/
  /*  2. Begin application class Ch3Prb3App                   */
  /************************************************************/
  public class Ch3Prb3App
  {
    static void Main()
    {
      Application.Run(new Ch3Prb3Form());
    }
  }  // End application class Ch3Prb3App

  /************************************************************/
  /*  3. Begin class Item                                     */
  /************************************************************/
  public class Item : IComparable<Item>
  {
    public int Index;
    public int UnitProfit;
    public int Weight;
    public int OptX;

    public Item(int indexValue, int unitProfitValue, int weightValue,
                int optXValue)
    {
      this.Index = indexValue;
      this.UnitProfit = unitProfitValue;
      this.Weight = weightValue;
      this.OptX = optXValue;
    }

    public int CompareTo(Item item)
    {
      return this.Index - item.Index;
    }

  }  // End application class Ch3Prb3App

}  // End partial namespace Ch3Prb3

