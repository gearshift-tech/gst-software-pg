using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Soko.Common.Controls
{
  public partial class DraggablePanel : UserControl
  {
    private bool isInitialized = false;
    //FlowLayoutPanel innerPanel = new FlowLayoutPanel();
    public DraggablePanel()
    {
      InitializeComponent();



      isInitialized = true;
      innerPanel.Location = new Point(0, 0);
      innerPanel.Size = this.Size;
      innerPanel.ControlAdded += InnerPanel_ControlAdded;
      this.SizeChanged += DraggablePanel_SizeChanged;
      innerPanel.MouseDown += innerpanel_MouseDown;
      innerPanel.MouseMove += innerpanel_MouseMove;



    }

    private void InnerPanel_ControlAdded(object sender, ControlEventArgs e)
    {
      e.Control.MouseDown += innerpanel_MouseDown;
      e.Control.MouseMove += innerpanel_MouseMove;
    }

    Point mouseDownPoint;
    private void innerpanel_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
        this.mouseDownPoint = PointToClient(Cursor.Position);// e.Location;
    }

    private void innerpanel_MouseMove(object sender, MouseEventArgs e)
    {

      if (e.Button != MouseButtons.Left)
        return;
      if ((mouseDownPoint.X == PointToClient(Cursor.Position).X) && (mouseDownPoint.Y == PointToClient(Cursor.Position).Y))
        return;

      Point currAutoS = innerPanel.AutoScrollPosition;
      int y_diff = Math.Abs(mouseDownPoint.Y - PointToClient(Cursor.Position).Y);
      if (mouseDownPoint.Y > PointToClient(Cursor.Position).Y)
      {
        //finger slide UP
        currAutoS.Y = Math.Abs(currAutoS.Y) + y_diff;
      }
      else if (mouseDownPoint.Y < PointToClient(Cursor.Position).Y)
      {
        //finger slide down
        
        if (currAutoS.Y != 0)
          currAutoS.Y = Math.Abs(currAutoS.Y) - y_diff;
      }
      else
      {
        currAutoS.Y = Math.Abs(currAutoS.Y);
      }

      //if (mouseDownPoint.X > PointToClient(Cursor.Position).X)
      //{
      //  //finger slide left
      //  if (currAutoS.X != 0)
      //    currAutoS.X = Math.Abs(currAutoS.X) - 5;
      //}
      //else if (mouseDownPoint.X < PointToClient(Cursor.Position).X)
      //{
      //  //finger slide right
      //  currAutoS.X = Math.Abs(currAutoS.X) + 5;
      //}
      //else
      //{
        currAutoS.X = Math.Abs(currAutoS.X);
      //}
      innerPanel.AutoScrollPosition = currAutoS;
      mouseDownPoint = PointToClient(Cursor.Position); //IMPORTANT

    }




    private void DraggablePanel_SizeChanged(object sender, EventArgs e)
    {
      innerPanel.Size = new Size(this.Width + 20, this.Height);
    }

    public new ControlCollection Controls
    {
      get
      {if (isInitialized)
          return innerPanel.Controls;
        else
          return base.Controls;
      }
    }
  }
}
