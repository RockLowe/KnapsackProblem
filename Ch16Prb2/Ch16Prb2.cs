/************************************************************/
/*                                                          */
/*  Course: CIS 350 -- Data Structures                      */
/*                                                          */
/*  Project: Ch16Prb2.CSPrj                                 */
/*                                                          */
/*  Source File: Ch16Prb2.CS                                */
/*                                                          */
/*  Programmer: Rocky Lowery                                */
/*                                                          */
/*  Purpose: Solve 0-1 Knapsack Problem using fathoming.    */
/*           Illustrates using a BackgroundWorker object    */
/*           to execute a processor-intensive and long-     */
/*           running method in the background, freeing      */
/*           up the UI to display a processing animation.   */
/*                                                          */
/*  Classes: 1. Ch16Prb2Form : Form                         */
/*           2. Ch16Prb2App                                 */
/*                                                          */
/************************************************************/

using System;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;
using LibUtil;
using LibGArrayList;

/************************************************************/
/*  Begin partial namespace Ch16Prb2                       */
/************************************************************/  
namespace Ch16Prb2
{

  /************************************************************/
  /*  1. Begin main form class Ch16Prb2Form : Form           */
  /************************************************************/ 
  public class Ch16Prb2Form : Form
  {
    const string DATA_FILE_NAME = "KnapsackDat.Txt";

    private Label solutionLabel;
    private Label            numOfRecurCallsLabel;
    private Label            numOfRecurCallsDisplay;
    private Label            percentDisplay;
    private Label            percentlabel;
    private Label            possibleCallsDisplay;
    private Label            possibleCallsLabel;
    private Button           solveButton;
    private Button           cancelButton;
    private MenuItem         openDataItem;
    private MenuItem         exitMenuItem;
    private MainMenu         mainMenu;
    private MenuItem         fileMenuItem;
    private MenuItem         helpMenuItem;
    private MenuItem         aboutMenuItem;
    private Label            elapsedTimeDisplay;
    private Label            elapsedTimeLabel;
    private PictureBox       processingPictureBox;
    private BackgroundWorker backgroundWorker;
    private IContainer       components;
    private bool             backgroundWorkCancelled;
    private int              numOfObjects, capacity;
    private int              numOfRecurCalls, optProfit, optWeight;
    private double           elapsedTime, optRatio = 1.5;
    private GArrayList<int>  unitProfitList = new GArrayList<int>();
    private GArrayList<int> oUnitProfitList = new GArrayList<int>();
    private GArrayList<int>  weightList     = new GArrayList<int>();
    private GArrayList<int> oWeightList     = new GArrayList<int>();
    private GArrayList<int>  xList          = new GArrayList<int>();
    private GArrayList<int>  optXList       = new GArrayList<int>();
    private DataGridView dataGridView1;
    private Label label1;
    private Label weightLabel;
    private Label profitLabel;
    private Timer timer1;
    private double elapse = 0.0;
    private DataGridViewTextBoxColumn objectCol;
    private DataGridViewTextBoxColumn weightCol;
    private DataGridViewTextBoxColumn profitCol;
    private StreamReader     fileIn;

    public Ch16Prb2Form()
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ch16Prb2Form));
      this.solutionLabel = new System.Windows.Forms.Label();
      this.numOfRecurCallsLabel = new System.Windows.Forms.Label();
      this.numOfRecurCallsDisplay = new System.Windows.Forms.Label();
      this.solveButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
      this.fileMenuItem = new System.Windows.Forms.MenuItem();
      this.openDataItem = new System.Windows.Forms.MenuItem();
      this.exitMenuItem = new System.Windows.Forms.MenuItem();
      this.helpMenuItem = new System.Windows.Forms.MenuItem();
      this.aboutMenuItem = new System.Windows.Forms.MenuItem();
      this.percentDisplay = new System.Windows.Forms.Label();
      this.percentlabel = new System.Windows.Forms.Label();
      this.possibleCallsDisplay = new System.Windows.Forms.Label();
      this.possibleCallsLabel = new System.Windows.Forms.Label();
      this.elapsedTimeDisplay = new System.Windows.Forms.Label();
      this.elapsedTimeLabel = new System.Windows.Forms.Label();
      this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
      this.processingPictureBox = new System.Windows.Forms.PictureBox();
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.label1 = new System.Windows.Forms.Label();
      this.weightLabel = new System.Windows.Forms.Label();
      this.profitLabel = new System.Windows.Forms.Label();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.objectCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.weightCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.profitCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
      ((System.ComponentModel.ISupportInitialize)(this.processingPictureBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      this.SuspendLayout();
      // 
      // solutionLabel
      // 
      this.solutionLabel.Location = new System.Drawing.Point(82, 8);
      this.solutionLabel.Name = "solutionLabel";
      this.solutionLabel.Size = new System.Drawing.Size(110, 24);
      this.solutionLabel.TabIndex = 4;
      this.solutionLabel.Text = "Optimal Loading";
      this.solutionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // numOfRecurCallsLabel
      // 
      this.numOfRecurCallsLabel.Location = new System.Drawing.Point(290, 9);
      this.numOfRecurCallsLabel.Name = "numOfRecurCallsLabel";
      this.numOfRecurCallsLabel.Size = new System.Drawing.Size(126, 24);
      this.numOfRecurCallsLabel.TabIndex = 5;
      this.numOfRecurCallsLabel.Text = "Recursive Calls";
      this.numOfRecurCallsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // numOfRecurCallsDisplay
      // 
      this.numOfRecurCallsDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.numOfRecurCallsDisplay.Location = new System.Drawing.Point(290, 33);
      this.numOfRecurCallsDisplay.Name = "numOfRecurCallsDisplay";
      this.numOfRecurCallsDisplay.Size = new System.Drawing.Size(124, 23);
      this.numOfRecurCallsDisplay.TabIndex = 7;
      this.numOfRecurCallsDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // solveButton
      // 
      this.solveButton.Location = new System.Drawing.Point(313, 250);
      this.solveButton.Name = "solveButton";
      this.solveButton.Size = new System.Drawing.Size(75, 22);
      this.solveButton.TabIndex = 8;
      this.solveButton.Text = "Solve";
      this.solveButton.Click += new System.EventHandler(this.solveButton_Click);
      // 
      // cancelButton
      // 
      this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelButton.Enabled = false;
      this.cancelButton.Location = new System.Drawing.Point(313, 310);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 22);
      this.cancelButton.TabIndex = 9;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.Visible = false;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // mainMenu
      // 
      this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.fileMenuItem,
            this.helpMenuItem});
      // 
      // fileMenuItem
      // 
      this.fileMenuItem.Index = 0;
      this.fileMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.openDataItem,
            this.exitMenuItem});
      this.fileMenuItem.Text = "&File";
      // 
      // openDataItem
      // 
      this.openDataItem.Index = 0;
      this.openDataItem.Text = "&Open Data";
      this.openDataItem.Click += new System.EventHandler(this.openDataMenuItem_Click);
      // 
      // exitMenuItem
      // 
      this.exitMenuItem.Index = 1;
      this.exitMenuItem.Text = "&Exit";
      this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
      // 
      // helpMenuItem
      // 
      this.helpMenuItem.Index = 1;
      this.helpMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.aboutMenuItem});
      this.helpMenuItem.Text = "&Help";
      // 
      // aboutMenuItem
      // 
      this.aboutMenuItem.Index = 0;
      this.aboutMenuItem.Shortcut = System.Windows.Forms.Shortcut.F1;
      this.aboutMenuItem.Text = "&About...";
      this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
      // 
      // percentDisplay
      // 
      this.percentDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.percentDisplay.Location = new System.Drawing.Point(290, 145);
      this.percentDisplay.Name = "percentDisplay";
      this.percentDisplay.Size = new System.Drawing.Size(124, 23);
      this.percentDisplay.TabIndex = 11;
      this.percentDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // percentlabel
      // 
      this.percentlabel.Location = new System.Drawing.Point(290, 121);
      this.percentlabel.Name = "percentlabel";
      this.percentlabel.Size = new System.Drawing.Size(126, 24);
      this.percentlabel.TabIndex = 10;
      this.percentlabel.Text = "Percent Processed";
      this.percentlabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // possibleCallsDisplay
      // 
      this.possibleCallsDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.possibleCallsDisplay.Location = new System.Drawing.Point(290, 89);
      this.possibleCallsDisplay.Name = "possibleCallsDisplay";
      this.possibleCallsDisplay.Size = new System.Drawing.Size(124, 23);
      this.possibleCallsDisplay.TabIndex = 13;
      this.possibleCallsDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // possibleCallsLabel
      // 
      this.possibleCallsLabel.Location = new System.Drawing.Point(290, 65);
      this.possibleCallsLabel.Name = "possibleCallsLabel";
      this.possibleCallsLabel.Size = new System.Drawing.Size(126, 24);
      this.possibleCallsLabel.TabIndex = 12;
      this.possibleCallsLabel.Text = "Possible Calls";
      this.possibleCallsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // elapsedTimeDisplay
      // 
      this.elapsedTimeDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.elapsedTimeDisplay.Location = new System.Drawing.Point(290, 205);
      this.elapsedTimeDisplay.Name = "elapsedTimeDisplay";
      this.elapsedTimeDisplay.Size = new System.Drawing.Size(124, 23);
      this.elapsedTimeDisplay.TabIndex = 15;
      this.elapsedTimeDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // elapsedTimeLabel
      // 
      this.elapsedTimeLabel.Location = new System.Drawing.Point(290, 181);
      this.elapsedTimeLabel.Name = "elapsedTimeLabel";
      this.elapsedTimeLabel.Size = new System.Drawing.Size(124, 24);
      this.elapsedTimeLabel.TabIndex = 14;
      this.elapsedTimeLabel.Text = "Elapsed Time";
      this.elapsedTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // backgroundWorker
      // 
      this.backgroundWorker.WorkerSupportsCancellation = true;
      this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
      // 
      // processingPictureBox
      // 
      this.processingPictureBox.Enabled = false;
      this.processingPictureBox.Image = global::Ch16Prb2.Properties.Resources.BlackBert2;
      this.processingPictureBox.Location = new System.Drawing.Point(284, 389);
      this.processingPictureBox.Name = "processingPictureBox";
      this.processingPictureBox.Size = new System.Drawing.Size(130, 8);
      this.processingPictureBox.TabIndex = 17;
      this.processingPictureBox.TabStop = false;
      this.processingPictureBox.Visible = false;
      // 
      // dataGridView1
      // 
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.AllowUserToDeleteRows = false;
      this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.objectCol,
            this.weightCol,
            this.profitCol});
      this.dataGridView1.Location = new System.Drawing.Point(37, 35);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.ReadOnly = true;
      this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
      this.dataGridView1.RowHeadersVisible = false;
      this.dataGridView1.Size = new System.Drawing.Size(201, 318);
      this.dataGridView1.TabIndex = 18;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(43, 394);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(42, 15);
      this.label1.TabIndex = 19;
      this.label1.Text = "Totals:";
      // 
      // weightLabel
      // 
      this.weightLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.weightLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.weightLabel.Location = new System.Drawing.Point(91, 389);
      this.weightLabel.Name = "weightLabel";
      this.weightLabel.Size = new System.Drawing.Size(49, 24);
      this.weightLabel.TabIndex = 20;
      this.weightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // profitLabel
      // 
      this.profitLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.profitLabel.Location = new System.Drawing.Point(159, 389);
      this.profitLabel.Name = "profitLabel";
      this.profitLabel.Size = new System.Drawing.Size(51, 24);
      this.profitLabel.TabIndex = 21;
      this.profitLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // timer1
      // 
      this.timer1.Interval = 1000;
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // objectCol
      // 
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      this.objectCol.DefaultCellStyle = dataGridViewCellStyle1;
      this.objectCol.HeaderText = "Object";
      this.objectCol.Name = "objectCol";
      this.objectCol.ReadOnly = true;
      this.objectCol.Width = 60;
      // 
      // weightCol
      // 
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      this.weightCol.DefaultCellStyle = dataGridViewCellStyle2;
      this.weightCol.HeaderText = "Weight";
      this.weightCol.Name = "weightCol";
      this.weightCol.ReadOnly = true;
      this.weightCol.Width = 60;
      // 
      // profitCol
      // 
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      this.profitCol.DefaultCellStyle = dataGridViewCellStyle3;
      this.profitCol.HeaderText = "Profit";
      this.profitCol.Name = "profitCol";
      this.profitCol.ReadOnly = true;
      this.profitCol.Width = 60;
      // 
      // Ch16Prb2Form
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
      this.ClientSize = new System.Drawing.Size(456, 454);
      this.Controls.Add(this.profitLabel);
      this.Controls.Add(this.weightLabel);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.dataGridView1);
      this.Controls.Add(this.processingPictureBox);
      this.Controls.Add(this.elapsedTimeDisplay);
      this.Controls.Add(this.elapsedTimeLabel);
      this.Controls.Add(this.possibleCallsDisplay);
      this.Controls.Add(this.possibleCallsLabel);
      this.Controls.Add(this.percentDisplay);
      this.Controls.Add(this.percentlabel);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.solveButton);
      this.Controls.Add(this.numOfRecurCallsDisplay);
      this.Controls.Add(this.numOfRecurCallsLabel);
      this.Controls.Add(this.solutionLabel);
      this.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.Menu = this.mainMenu;
      this.Name = "Ch16Prb2Form";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Ch16Prb2 – 0,1 Knapsack Problem Using Fathoming";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ch16Prb2Form_FormClosing);
      ((System.ComponentModel.ISupportInitialize)(this.processingPictureBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }
    #endregion

    /************************************************************/
    /*  Message Handlers                                        */
    /************************************************************/
    private void openDataMenuItem_Click(object sender, System.EventArgs e)
    {
      System.Diagnostics.Process.Start("Notepad",DATA_FILE_NAME);    
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
      
      solveButton.Enabled          = false;  
      cancelButton.Enabled         = true;
      processingPictureBox.Visible = true;   
      processingPictureBox.Enabled = true; 
      InputProblemDef();
      Initialize();
      timer1.Enabled = true;
      startDateTime             = DateTime.Now;
      backgroundWorker.RunWorkerAsync();   // Execute GenProfits() in the background.
      while (backgroundWorker.IsBusy)      // Continue responding to UI events while waiting for   
        Application.DoEvents();            // the background work to finish.
      timeSpan = DateTime.Now - startDateTime;
      timer1.Enabled = false;
      elapsedTime = timeSpan.Minutes*60.0 + timeSpan.Seconds*1.0 + timeSpan.Milliseconds/1000.0;
      if (! backgroundWorkCancelled)
        DisplayResults();
      processingPictureBox.Enabled = false;  
      processingPictureBox.Visible = false;
      cancelButton.Enabled         = false;  
      solveButton.Enabled          = true;
    }

    private void cancelButton_Click(object sender, System.EventArgs e)
    {
      backgroundWorker.CancelAsync();
      backgroundWorkCancelled = true;
      solveButton.Visible = true; cancelButton.Visible = false;
      weightLabel.Text = "";
      profitLabel.Text = "";
      numOfRecurCallsDisplay.Text = "";
      percentDisplay.Text = "";
      possibleCallsDisplay.Text = "";
      elapsedTimeDisplay.Text = " ";
      elapse = 0.0;
    }

    private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      backgroundWorkCancelled = false;
      GenProfits(1, 0, 0);
    }

    private void Ch16Prb2Form_FormClosing(object sender, FormClosingEventArgs e)
    {
      backgroundWorker.CancelAsync(); // if GenProfits is executing in the background, terminate it 
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
        MessageBox.Show(DATA_FILE_NAME+" does not exist","Abort Execution",
                        MessageBoxButtons.OK,MessageBoxIcon.Error);
        Application.Exit();
      }
      unitProfitList.Clear();
      weightList.Clear();
      fileIn.ReadLine();
      numOfObjects = Int32.Parse(fileIn.ReadLine());  
      fileIn.ReadLine();
      words = StringMethods.SpaceDelimit(fileIn.ReadLine()).Split(' ');
      for (int i=1; i<=numOfObjects; i++)
        oWeightList.Add(Int32.Parse(words[i-1]));      
      fileIn.ReadLine();
      words = StringMethods.SpaceDelimit(fileIn.ReadLine()).Split(' ');
      for (int i=1; i<=numOfObjects; i++)
        oUnitProfitList.Add(Int32.Parse(words[i-1])); 
      fileIn.ReadLine();  
      capacity = Int32.Parse(fileIn.ReadLine());
      EffiencySort();
      fileIn.Close();    
    }

    private void Initialize()
    {
      this.dataGridView1.Rows.Clear();
      numOfRecurCalls = 0;
      optProfit       = 0;
      xList.Clear();
      optXList.Clear();
      for (int i=1; i<=numOfObjects; i++)
      {
        xList.Add(0);
        optXList.Add(0);
      }
      weightLabel.Text = "";
      profitLabel.Text = "";
      numOfRecurCallsDisplay.Text = "";
      percentDisplay.Text         = "";
      possibleCallsDisplay.Text   = "";
      elapsedTimeDisplay.Text     = " ";
      cancelButton.Location = solveButton.Location;
      cancelButton.Visible = true; solveButton.Visible = false;
      elapse = 0.0;
    }

    private void EffiencySort()
    {
      int p = 1;
      double ratio;
      for (int k = 1; k <= numOfObjects; k++)
      {
        weightList.Add(0);
        unitProfitList.Add(0);
      }
      for (int i = 1; i <= numOfObjects; i++)
      {
        ratio = (double)oWeightList[i] / (double)oUnitProfitList[i];
        for (p = 1; ratio > (double)weightList[p] / (double)unitProfitList[p] && weightList[p] != 0;)
        {
          p++;
        }
          weightList.InsertAt(p, oWeightList[i]);
          unitProfitList.InsertAt(p, oUnitProfitList[i]);
      }
    }

    private void GenProfits(int position, int totWeight, int totProfit)
    {
      if (! backgroundWorker.CancellationPending)
      {
        numOfRecurCalls++;
        if (position==numOfObjects+1)
        {
          if (totProfit>optProfit)
          {
              optProfit = totProfit; optWeight = totWeight;
              optXList.Copy(xList);
          }
        }
        else
        {
          if (numOfObjects > 25)
          {
            if ((double)totWeight / (double)capacity < .5)
              optRatio = (double)weightList[numOfObjects / 4] / (double)unitProfitList[numOfObjects / 4];
            else if ((double)totWeight/(double)capacity<.6 && (double)totWeight / (double)capacity >= .5)
              optRatio = (double)weightList[numOfObjects / 3] / (double)unitProfitList[numOfObjects / 3];
            else if ((double)totWeight/(double)capacity<.8 && (double)totWeight / (double)capacity >= .6)
              optRatio = (double)weightList[numOfObjects/2]/(double)unitProfitList[numOfObjects / 2];
            else if ((double)totWeight/(double)capacity<1.0&&(double)totWeight / (double)capacity >= .8)
              optRatio = 2.0;
          }
          else if (numOfObjects == 25)
          {
            if ((double)totWeight / (double)capacity < .25)
              optRatio = (double)weightList[numOfObjects / 3] / (double)unitProfitList[numOfObjects / 3];
            else if ((double)totWeight / (double)capacity <.5&&(double)totWeight/(double)capacity >= .25)
              optRatio = (double)weightList[numOfObjects / 2] / (double)unitProfitList[numOfObjects / 2];
            else if ((double)totWeight / (double)capacity < 1.0 &&(double)totWeight/(double)capacity>=.5)
              optRatio = 2.0;
          }
          else 
          {
            if ((double)totWeight / (double)capacity < .25)
              optRatio = (double)weightList[numOfObjects / 2] / (double)unitProfitList[numOfObjects / 2];
            else if ((double)totWeight / (double)capacity < 1.0 && (double)totWeight/(double)capacity>=.25)
              optRatio = 2.0;
          }
          
          if (totWeight+weightList[position]<=capacity &&
            (double)weightList[position]/(double)unitProfitList[position]<=optRatio)
          {
            xList[position] = 1;  GenProfits(position+1, totWeight+weightList[position],
                                             totProfit+unitProfitList[position]);
          }
          xList[position] = 0;  GenProfits(position+1, totWeight, totProfit);

        }
      }
    }

    private void DisplayResults()
    {
      ulong  possibleCalls;
      double percent;
      DataGridViewColumn obj = dataGridView1.Columns[0];
      ListSortDirection direction =ListSortDirection.Ascending;

      for (int i = 1; i<=numOfObjects; i++)
      {
        if (optXList[i]==1)
        {
          this.dataGridView1.Rows.Add(i, oWeightList[i], oUnitProfitList[i]);
        }
      }
      dataGridView1.Sort(obj, direction);
      weightLabel.Text = optWeight.ToString();
      profitLabel.Text = optProfit.ToString();
      possibleCalls = (ulong) Math.Pow(2, numOfObjects + 1) - 1;
      percent       = 100.0 * numOfRecurCalls / possibleCalls;
      numOfRecurCallsDisplay.Text = numOfRecurCalls.ToString("n0");
      percentDisplay.Text         = percent.ToString("n4") + "%";
      possibleCallsDisplay.Text   = possibleCalls.ToString("n0");
      elapsedTimeDisplay.Text     = elapsedTime.ToString("f3") + " sec";
      solveButton.Visible = true; cancelButton.Visible = false; 
      solveButton.Focus();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      ulong possibleCalls; 
      double percent;
      elapse++;
      possibleCalls = (ulong)Math.Pow(2, numOfObjects + 1) - 1;
      percent = 100.0 * numOfRecurCalls / possibleCalls;
      numOfRecurCallsDisplay.Text = numOfRecurCalls.ToString("n0");
      percentDisplay.Text = percent.ToString("n4") + "%";
      possibleCallsDisplay.Text = possibleCalls.ToString("n0");
      elapsedTimeDisplay.Text = elapse.ToString("f3") + " sec";
    }
  }  // End main form class Ch16Prb2Form

  /************************************************************/
  /*  2. Begin application class Ch16Prb2App                 */
  /************************************************************/
  public class Ch16Prb2App
  {
    static void Main()
    {
      Application.Run(new Ch16Prb2Form());
    }
  }  // End application class Ch16Prb2App

}  // End partial namespace Ch16Prb2
