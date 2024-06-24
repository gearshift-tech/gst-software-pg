using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Soko.Common.Forms
{
    /// <summary>
    /// Klasa do tworzenia ³adnych okien typu MessageBox z ikonk¹, nag³ówkiem, tekstem
    /// i dowolnymi przyciskami
    /// </summary>
    /// <typeparam name="ReturnType"></typeparam>
    public partial class MessageBox<ReturnType> : Syncfusion.Windows.Forms.MetroForm
  {

      protected class ButtonDescription
      {
        public Control control;
        public ReturnType result;
        public MessageBox<ReturnType> parent;
        private ButtonType buttonType;

        public ButtonType Type
        {
          get { return buttonType; }
          set { buttonType = value; }
        }

        public ButtonDescription( MessageBox<ReturnType> parent, ReturnType result, Control control, ButtonType buttonType )
        {
          this.parent = parent;
          this.result = result;
          this.control = control;
          this.buttonType = buttonType;
        }
        public void ButtonClicked( object sender, EventArgs e )
        {
          this.parent.DialogResult = this.result;
          this.parent.Close();
        }
      }

      [Flags]
      public enum ButtonType { Neutral = 0, Accept, Cancel };

      #region Constants

      private const int CS_NOCLOSE = 0x200;
      private const int HEIGHT_FULL = 411;
      private const int HEIGHT_NORMAL = 250;
      private string detailsMsg = "";//details information

      #endregion  Constants


      #region Private fields

      private ReturnType messageBoxResult;

      private bool closeButtonEnabled = true;

      //protected HorizontalAlignment buttonsAligment;

      /// <summary>
      /// A collection of buttons in the lower pane.
      /// </summary>
      protected Dictionary<ReturnType, ButtonDescription> buttonsCollection = new Dictionary<ReturnType, ButtonDescription>();

      #endregion Private fields


      #region Constructors & finalizer

      public MessageBox()
      {
        InitializeComponent();

        this.Text = Application.ProductName;
        this.Image = Soko.Common.Properties.Resources.MessageBox_information;

        this.Height = HEIGHT_NORMAL;
      }

      public MessageBox( string header, string message )
        : this()
      {
        this.Header = header;
        this.Message = message;
      }

      public MessageBox( string title, string header, string message )
        : this( header, message )
      {
        this.Text = title;
      }

      public MessageBox( string title, string header, string message, string details )
        : this( title, header, message )
      {
        detailsMsg = details;
        this.moreInfoLabel.Visible = true;
      }

      #endregion Constructors & finalizer


      #region Events



      #endregion Events


      #region Properties

      protected override CreateParams CreateParams
      {
        get
        {
          CreateParams cp = base.CreateParams;
          if ( !this.closeButtonEnabled )
          {
            cp.ClassStyle = cp.ClassStyle | CS_NOCLOSE;
          }
          return cp;
        }
      }

      public virtual Image Image
      {
        get { return this.pbImage.Image; }
        set
        {
          this.pbImage.Image = value;
        }
      }

      public virtual string Title
      {
        get
        {
          return this.Text;
        }
        set
        {
          this.Text = value;
        }
      }

      public virtual string Header
      {
        get
        {
          return this.lblHeader.Text;
        }
        set
        {
          this.lblHeader.Text = value;
        }
      }

      public virtual string Message
      {
        get
        {
          return this.lblMessage.Text;
        }
        set
        {
          this.lblMessage.Text = value;
        }
      }

      public new ReturnType DialogResult
      {
        get
        {
          return this.messageBoxResult;
        }
        set
        {
          this.messageBoxResult = value;
        }
      }

      #endregion Properties


      #region Methods

      public new ReturnType ShowDialog()
      {
        DetermineCloseButtonState();
        base.ShowDialog();
        return this.DialogResult;
      }
      public new ReturnType ShowDialog( IWin32Window owner )
      {
        DetermineCloseButtonState();
        base.ShowDialog( owner );
        return this.DialogResult;
      }
      public void AddButton( ReturnType value, string text )
      {
        AddButton( value, text, ButtonType.Neutral );
      }

    /// <summary>
    /// Method adds a button to the lower pane.
    /// </summary>
    /// <param name="value">A value returned from the ShowDialog method when the button is clicked.</param>
    /// <param name="text">A text that will appear on the button.</param>
    /// <param name="buttonType">A type of the button.</param>
    public void AddButton(ReturnType value, string text, ButtonType buttonType)
    {
      Soko.Common.Controls.NiceButton btn = new Soko.Common.Controls.NiceButton
      {
        BackColor = System.Drawing.Color.FromArgb(45,45,45),
        BackColorOnClicked1 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255))))),
        BackColorOnClicked2 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255))))),
        BackColorOnFocus1 = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255))))),
        BackColorOnFocus2 = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255))))),
        BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38))))),
        BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(84)))), ((int)(((byte)(144))))),
        BorderColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255))))),
        BorderWidth = 1,
        ContentPadding = new System.Windows.Forms.Padding(0),
        DrawBackColorOnFocus = true,
        DrawBackgroundImage = false,
        DrawBorderOnFocus = true,
        DrawBorderOnTop = false,
        Font = new System.Drawing.Font("Liberation Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238))),
        ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240))))),

        Size = new System.Drawing.Size(70, 35),
        SupportTransparentBackground = false,
        Text = text,
        TextImageRelation = Soko.Common.Controls.TextRelation.CenterTextNoImage,
        TextImageSpacing = 0
        //Text = text,
        //  Visible = true,
        //  AutoSizeMode = AutoSizeMode.GrowOnly,
        //  AutoSize = true
      };

      ButtonDescription bd = new ButtonDescription( this, value, btn, buttonType );
        btn.Click += new EventHandler( bd.ButtonClicked );
        buttonsCollection.Add( value, bd );
        buttonsPanel.Controls.Add( btn );
        if ( ( buttonType & ButtonType.Accept ) != 0 )
        {
          //this.AcceptButton = btn;
        }
        if ( ( buttonType & ButtonType.Cancel ) != 0 )
        {
          //this.CancelButton = btn;
        }
      }

      /// <summary>
      /// Method removes all buttons from the lower pane.
      /// </summary>
      public void RemoveButtons()
      {
        this.AcceptButton = null;
        this.CancelButton = null;
        foreach ( ButtonDescription bd in buttonsCollection.Values )
        {
        buttonsPanel.Controls.Remove( bd.control );
        }
        buttonsCollection.Clear();
      }

      private void MessageBox_Shown( object sender, EventArgs e )
      {
        Soko.Common.Controls.NiceButton btn = this.AcceptButton as Soko.Common.Controls.NiceButton;
        if ( btn != null )
        {
          btn.Focus();
        }

      }

      private void moreInfoLabel_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
      {
        if ( this.Height == HEIGHT_NORMAL )
        {
        RichTextBox textBox = new RichTextBox
        {
          BorderStyle = BorderStyle.None,
          ForeColor = Color.Black,
          Location = new Point(0, 0),
          Text = detailsMsg,
          Font = new Font("Courier New", 8.25F, FontStyle.Regular),
          Dock = DockStyle.Fill
        };

        Panel panel = new Panel
        {
          BorderStyle = BorderStyle.FixedSingle,
          Location = new Point(4, 223),
          Dock = DockStyle.Bottom,
          Size = new Size(375, 182)
        };

        panel.Controls.Add( textBox );
          this.Controls.Add( panel );
          panel.Dock = DockStyle.Fill;
          panel.BringToFront();

          this.Height = HEIGHT_FULL;
        }
        else
        {
          this.Controls.RemoveAt( 0 );
          this.Height = HEIGHT_NORMAL;
        }
      }

      protected void DetermineCloseButtonState()
      {
        this.closeButtonEnabled = false;

        // ktorys z przyciskow ma wartosc Cancel
        foreach ( ButtonDescription bd in this.buttonsCollection.Values )
        {
          if ( ( bd.Type & ButtonType.Cancel ) != 0 )
          {
            this.closeButtonEnabled = true;
            this.messageBoxResult = bd.result;
            return;
          }
        }
      }

      #endregion Methods



















	}
}