using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using GST.Gearshift.Components.Interfaces.USB;
using Soko.Common.Controls;
using Soko.Common.Common;
using GST.Gearshift.Components.Utilities;
using ExtendedListBoxControl.ExtendedListBoxItemClasses;

namespace GST.Gearshift.Components.Forms
{
  public partial class HomeScreenPanel : UserControl
  {

    public event EventHandler viewReportsButtonClicked;
    public event EventHandler installTestsButtonClicked;
    public event EventHandler systemOptionsButtonClicked;
    public event EventHandler canbusUtilsButtonClicked;
    public event EventHandler testManagerButtonClicked;
    public event EventHandler aoUtilsButtonClicked;
    public event EventHandler techSupportButtonClicked;
    public event EventHandler resDocsButtonClicked;
    public event EventHandler ExitButtonClicked;

    public event EventHandler StartTestButtonClicked;



    protected Soko.Common.Controls.NiceTooltip.NiceToolTip _buttonsTooltip;

    public HomeScreenPanel()
    {
      InitializeComponent();

      _buttonsTooltip = new Soko.Common.Controls.NiceTooltip.NiceToolTip();

      // Set customer logo
      customerLogoPanel.BackgroundImage = GST.Gearshift.Components.Utilities.Settings.Instance.CompanyInfoPicture;
      scriptSearchTextBox_TextChanged(null, EventArgs.Empty);
      // Draw suite name and version on logoPanel
      //Bitmap logo = new Bitmap(logoPanel.BackgroundImage);
      //Graphics g = Graphics.FromImage(logo);
      //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
     // Font suiteNameFont = new Font(new FontFamily("Segoe UI"), 30.0f, FontStyle.Bold);
     // Font versionNameFont = new Font(new FontFamily("Segoe UI"), 20.0f, FontStyle.Bold);
     // Point suiteNameLocation = new Point(336, 260);
      //Point versionNameLocation = new Point(suiteNameLocation.X - 15 + (int)(g.MeasureString(GST.Gearshift.Components.Utilities.Settings.AppSuiteName, suiteNameFont).Width), suiteNameLocation.Y + 12);
      //g.DrawString(GST.Gearshift.Components.Utilities.Settings.AppSuiteName, suiteNameFont, new SolidBrush(Color.Black), suiteNameLocation);
      //g.DrawString(GST.Gearshift.Components.Utilities.Settings.SoftwareVersion, versionNameFont, new SolidBrush(Color.Black), versionNameLocation);
      //logoPanel.BackgroundImage = logo;

      SetTooltips();
    }

    private void SetTooltips()
    {
      //_buttonsTooltip.

      _buttonsTooltip.SetToolTip(reportsButton, "Click to view reports\nstored on this machine");
      _buttonsTooltip.SetToolTipTitle(reportsButton, "View Reports");

      _buttonsTooltip.SetToolTip(installTestsButton, "Click to install new\nreports on this machine");
      _buttonsTooltip.SetToolTipTitle(installTestsButton, "Install scripts");

      _buttonsTooltip.SetToolTip(supportButton, "Click to write a message\nto GearShift Technologies Support team");
      _buttonsTooltip.SetToolTipTitle(supportButton, "Query support");

      _buttonsTooltip.SetToolTip(testManagerButton, "Click to edit scripts\nstored on this machine");
      _buttonsTooltip.SetToolTipTitle(testManagerButton, "Test manager");

      _buttonsTooltip.SetToolTip(systemOptions, "Click to edit\nsystem options");
      _buttonsTooltip.SetToolTipTitle(systemOptions, "Options");

      _buttonsTooltip.SetToolTip(canbusUtilsButton, "Click to access\nCAN bus utilities");
      _buttonsTooltip.SetToolTipTitle(canbusUtilsButton, "CAN utilities");

      _buttonsTooltip.SetToolTip(AOButton, "Click to calibrate\nAnalog Outputs");
      _buttonsTooltip.SetToolTipTitle(AOButton, "Calibrate AOa");

      _buttonsTooltip.SetToolTip(exitButton, "Click to close\nthis software");
      _buttonsTooltip.SetToolTipTitle(exitButton, "Exit");

      _buttonsTooltip.SetToolTip(scriptSearchPanel, "Enter text here to filter scripts\n stored on this machine");
      _buttonsTooltip.SetToolTipTitle(scriptSearchPanel, "Search scripts");


    }

    private void reportsButton_Click(object sender, EventArgs e)
    {
      if (viewReportsButtonClicked != null) viewReportsButtonClicked(sender, e);
    }

    private void installTestsButton_Click(object sender, EventArgs e)
    {
      if (installTestsButtonClicked != null) installTestsButtonClicked(sender, e);
    }

    private void systemOptions_Click(object sender, EventArgs e)
    {
      if (systemOptionsButtonClicked != null) systemOptionsButtonClicked(sender, e);
    }

    private void canbusUtilsButton_Click(object sender, EventArgs e)
    {
      if (canbusUtilsButtonClicked != null) canbusUtilsButtonClicked(sender, e);
    }

    private void testManagerButton_Click(object sender, EventArgs e)
    {
      if (testManagerButtonClicked != null) testManagerButtonClicked(sender, e);
    }

    private void AOButton_Click(object sender, EventArgs e)
    {
      if (aoUtilsButtonClicked != null) aoUtilsButtonClicked(sender, e);
    }

    private void supportButton_Click(object sender, EventArgs e)
    {
      if (techSupportButtonClicked != null) techSupportButtonClicked(sender, e);
    }

    private void resDocsButton_Click(object sender, EventArgs e)
    {
      if (resDocsButtonClicked != null) resDocsButtonClicked(sender, e);
    }

    private void exitButton_Click(object sender, EventArgs e)
    {
      if (ExitButtonClicked != null) ExitButtonClicked(sender, e);
    }

    private void scriptSearchTextBox_TextChanged(object sender, EventArgs e)
    {
      SuspendLayout();
      foundTestsContainerPanel.Controls.Clear();
      string[] filePaths = Settings.AvailableTestScriptsPaths;

      int matchingItems = 0;
      int itemHeight = 75;

      foreach (string filePath in filePaths)
      {
        if (Path.GetFileNameWithoutExtension(filePath).ToLower().Contains(scriptSearchTextBox.Text.ToLower()))
        {
          matchingItems++;
          NiceGraphicalLabel item = new NiceGraphicalLabel();
          item.Size = new System.Drawing.Size(foundTestsPanelMain.Width, itemHeight);
          item.TitleText = Path.GetFileNameWithoutExtension(filePath);
          item.BackColor = Color.FromArgb(240, Color.White);
          item.BackColorOnFocus1 = Color.FromArgb(80, 27, 98, 17);
          item.BackColorOnFocus2 = Color.FromArgb(50, 27, 98, 17);
          item.BackColorOnClicked1 = Color.FromArgb(180, 0, 82, 46);
          item.BackColorOnClicked2 = Color.FromArgb(150, 0, 82, 46);
          item.Font = new Font("Segoe UI", 18.0f, FontStyle.Bold);
          item.ForeColor = Color.DimGray;

          TestScript ts;
          try
          {
            ts = TestScript.OpenXml(filePath);
          }
          catch
          {
            ts = new TestScript();
          }
          switch (ts.Gearbox.ControllerType)
          {
            case GearboxControllerType.NON_MECHATRONIC:
            default:
              {
                item.DetailsText = "#GearShift driver";
                break;
              }
            case GearboxControllerType.ZF_6HPxx_1911E:
            case GearboxControllerType.ZF_6HPxx_1911M:
            case GearboxControllerType.ZF_6HPxx_CE:
            case GearboxControllerType.ZF_6HPxx_CM:
            case GearboxControllerType.ZF_6HPxx_TUCE:
            case GearboxControllerType.ZF_6HPxx_TUCM:
            case GearboxControllerType.ZF_6HPxx_WM:
              {
                item.DetailsText = "#Zf6 Code Shift";
                break;
              }
            case GearboxControllerType.NISSAN_RE5:
              {
                item.DetailsText = "#Zf6 Code Shift #Zf6 Flash";
                break;
              }
            case GearboxControllerType.GM6T40:
            case GearboxControllerType.GM6T70:
            case GearboxControllerType.GM6L:
              {
                item.DetailsText = "#Zf6 Code Shift #Zf6 Flash";
                break;
              }
          }


          item.Tag = filePath;

          item.Click += new EventHandler(TestButtonClick);


          foundTestsContainerPanel.Controls.Add(item);

        }
      }

      foundTestsContainerPanel.Size = new Size(foundTestsPanelMain.Width, (itemHeight + 4) * matchingItems);
      foundTestsContainerPanel.Location = new Point(0, 0);
      foundTestsScrollbar.Value = 0;

      ResumeLayout();
    }

    void TestButtonClick(object sender, EventArgs e)
    {
      NiceGraphicalLabel item = (NiceGraphicalLabel)sender;
      //Console.WriteLine((string)item.Tag);
      string filePath = ((String)item.Tag);
      if (StartTestButtonClicked != null) StartTestButtonClicked((object)filePath, e);
    }

    private void foundTestsScrollbar_Scroll(object sender, ScrollEventArgs e)
    {
      // Scrollbar is only enabled if container panel is bigger than main panel
      int scrollSpan = foundTestsContainerPanel.Height - foundTestsPanelMain.Height;

      if (scrollSpan <= 0)
      {
        foundTestsContainerPanel.Location = new Point(0, 0);
      }
      else
      {
        int yPos = (int)(scrollSpan * ((float)foundTestsScrollbar.Value / (float)foundTestsScrollbar.Maximum));
        foundTestsContainerPanel.Location = new Point(0, -(yPos));
      }
    }

    private void HomeScreenPanel_Resize(object sender, EventArgs e)
    {
      Point centerPanelTargetLocation = new Point(47, 104);
      Size centerPaneTargetlSize = new Size(1049, 430);
      Size logoPanelTargetSize = new Size(621, 133);

      int mainPanelMinimumNormalWidth = 1124;
      int mainPanelMinimumNormalHeight = 668;

      SuspendLayout();
      // Sort out the center panel
      if (mainPanel.Width < mainPanelMinimumNormalWidth)
      {
        float xRatio = (float)mainPanel.Width / (float)mainPanelMinimumNormalWidth;
        //Console.WriteLine("RATIO " + xRatio.ToString());
        centerPanel.Size = new Size((int)(centerPaneTargetlSize.Width * xRatio), centerPanel.Width);
      }
      else
      {
        centerPanel.Location = centerPanelTargetLocation;
        centerPanel.Size = centerPaneTargetlSize;
      }
      //Sort out the logo panel
      int newLogoPanelHeight = 0;
      if (mainPanel.Height < mainPanelMinimumNormalHeight)
      {
        newLogoPanelHeight = mainPanel.Height - centerPanel.Bottom;
      }
      else
      {
        newLogoPanelHeight = logoPanelTargetSize.Height;
      }
     // gstLogoPanel.Size = new Size(logoPanelTargetSize.Width, newLogoPanelHeight);
     // gstLogoPanel.Location = new Point(mainPanel.Width - gstLogoPanel.Width, mainPanel.Height - gstLogoPanel.Height);

        ResumeLayout();

      Console.WriteLine(this.Width.ToString() + "  " + this.Height.ToString());
    }
  }
}
