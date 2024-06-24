using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Soko.Common.Forms
{
    public class MessageBox : MessageBox<DialogResult>
    {
        #region P/Invoke declarations for standard icon loading
        public enum SystemIcons
        {
            IDI_APPLICATION = 32512,
            IDI_HAND = 32513,
            IDI_QUESTION = 32514,
            IDI_EXCLAMATION = 32515,
            IDI_ASTERISK = 32516,
            IDI_WINLOGO = 32517,
            IDI_WARNING = IDI_EXCLAMATION,
            IDI_ERROR = IDI_HAND,
            IDI_INFORMATION = IDI_ASTERISK,
        }

        [DllImport("user32.dll")]
        static extern IntPtr LoadIcon(IntPtr hInstance, IntPtr lpIconName);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool DestroyIcon(IntPtr hIcon);

        static System.Drawing.Bitmap LoadIconBitmap(SystemIcons iconType)
        {
            IntPtr handle = LoadIcon(IntPtr.Zero, (IntPtr)iconType);
            if (handle == IntPtr.Zero)
            {
              return Soko.Common.Properties.Resources.MessageBox_information;
            }
            try
            {
                System.Drawing.Icon icon = System.Drawing.Icon.FromHandle(handle);
                return icon.ToBitmap();
            }
            finally
            {
                
                DestroyIcon(handle);
            }
        }

        #endregion



        protected MessageBoxButtons standardButtons;
        protected MessageBoxIcon icon;

        public virtual MessageBoxButtons MessageBoxButtons
        {
            get
            {
                return this.standardButtons;
            }
            set
            {
                this.standardButtons = value;

                base.RemoveButtons();

                switch (value)
                {
                    case MessageBoxButtons.AbortRetryIgnore:
                        base.AddButton(System.Windows.Forms.DialogResult.Abort, Soko.Common.Common.Translator.Tr("&Abort"));
                        base.AddButton(System.Windows.Forms.DialogResult.Retry, Soko.Common.Common.Translator.Tr("&Retry"), ButtonType.Accept);
                        base.AddButton(System.Windows.Forms.DialogResult.Ignore, Soko.Common.Common.Translator.Tr("&Ignore"));
                        break;
                    case MessageBoxButtons.OK:
                        base.AddButton(System.Windows.Forms.DialogResult.OK, Soko.Common.Common.Translator.Tr("&OK"), ButtonType.Accept | ButtonType.Cancel);
                        break;
                    case MessageBoxButtons.OKCancel:
                        base.AddButton(System.Windows.Forms.DialogResult.OK, Soko.Common.Common.Translator.Tr("&OK"), ButtonType.Accept);
                        base.AddButton(System.Windows.Forms.DialogResult.Cancel, Soko.Common.Common.Translator.Tr("&Cancel"), ButtonType.Cancel);
                        break;
                    case MessageBoxButtons.RetryCancel:
                        base.AddButton(System.Windows.Forms.DialogResult.Retry, Soko.Common.Common.Translator.Tr("&Retry"), ButtonType.Accept);
                        base.AddButton(System.Windows.Forms.DialogResult.Cancel, Soko.Common.Common.Translator.Tr("&Cancel"), ButtonType.Cancel);
                        break;
                    case MessageBoxButtons.YesNo:
                        base.AddButton(System.Windows.Forms.DialogResult.Yes, Soko.Common.Common.Translator.Tr("&Yes"), ButtonType.Accept);
                        base.AddButton(System.Windows.Forms.DialogResult.No, Soko.Common.Common.Translator.Tr("&No"));
                        break;
                    case MessageBoxButtons.YesNoCancel:
                        base.AddButton(System.Windows.Forms.DialogResult.Yes, Soko.Common.Common.Translator.Tr("&Yes"), ButtonType.Accept);
                        base.AddButton(System.Windows.Forms.DialogResult.No, Soko.Common.Common.Translator.Tr("&No"));
                        base.AddButton(System.Windows.Forms.DialogResult.Cancel, Soko.Common.Common.Translator.Tr("&Cancel"), ButtonType.Cancel);
                        break;
                    default:
                        base.AddButton(System.Windows.Forms.DialogResult.OK, Soko.Common.Common.Translator.Tr("&OK"), ButtonType.Accept);
                        break;
                }
            }
        }

        public virtual MessageBoxIcon MessageBoxIcon
        {
            get
            {
                return this.icon;
            }
            set
            {
                this.icon = value;

                switch (value)
                {
                    case MessageBoxIcon.Error:
                        base.Image = Soko.Common.Properties.Resources.MessageBox_critical;
                        break;
                    case MessageBoxIcon.Question:
                        base.Image = Soko.Common.Properties.Resources.MessageBox_question;
                        break;
                    case MessageBoxIcon.Warning:
                        base.Image = Soko.Common.Properties.Resources.MessageBox_warning;
                        break;
                    case MessageBoxIcon.Information:
                        base.Image = Soko.Common.Properties.Resources.MessageBox_information;
                        break;
                }
            }
        }

        public MessageBox()
            : base()
        {
            this.MessageBoxButtons = MessageBoxButtons.OK;
            this.MessageBoxIcon = MessageBoxIcon.Information;
        }
        public MessageBox(string header, string message)
            : base(header, message)
        {
            this.MessageBoxButtons = MessageBoxButtons.OK;
            this.MessageBoxIcon = MessageBoxIcon.Information;
        }

        public MessageBox(string title, string header, string message)
            : base(title, header, message)
        {
            this.MessageBoxButtons = MessageBoxButtons.OK;
            this.MessageBoxIcon = MessageBoxIcon.Information;
        }

        public MessageBox(string title, string header, string message, string details)
            : base(title, header, message, details)
        {
            this.MessageBoxButtons = MessageBoxButtons.OK;
            this.MessageBoxIcon = MessageBoxIcon.Information;
        }

        public static DialogResult Show(string header, string message)
        {
      MessageBox mb = new MessageBox(header, message);
      return mb.ShowDialog();
        }

        public static DialogResult Show(string header, string message, MessageBoxButtons buttons)
        {
      MessageBox mb = new MessageBox(header, message)
      {
        
        MessageBoxButtons = buttons
      };
      return mb.ShowDialog();
        }

        public static DialogResult Show(string title, string header, string message, MessageBoxButtons buttons)
        {
      MessageBox mb = new MessageBox(title, header, message)
      {
        
        MessageBoxButtons = buttons
      };
      return mb.ShowDialog();
        }

        public static DialogResult Show(string title, string header, string message, string details, MessageBoxButtons buttons)
        {
      MessageBox mb = new MessageBox(title, header, message, details)
      {
        
        MessageBoxButtons = buttons
      };
      return mb.ShowDialog();
        }

        public static DialogResult Show(string header, string message, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
      MessageBox mb = new MessageBox(header, message)
      {
        
        MessageBoxButtons = buttons,
        MessageBoxIcon = icon
      };

      return mb.ShowDialog();
        }

        public static DialogResult Show(string title, string header, string message, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
      MessageBox mb = new MessageBox(title, header, message)
      {
        
        MessageBoxButtons = buttons,
        MessageBoxIcon = icon
      };

      return mb.ShowDialog();
        }

        public static DialogResult Show(string title, string header, string message, string details, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
      MessageBox mb = new MessageBox(title, header, message, details)
      {
        
        MessageBoxButtons = buttons,
        MessageBoxIcon = icon
      };
      return mb.ShowDialog();
        }

        public static DialogResult Show(IWin32Window owner, string header, string message)
        {
      MessageBox mb = new MessageBox(header, message)
      {
        
      };
      return mb.ShowDialog(owner);
        }



        public static DialogResult ShowInfo(string title, string header, string message, MessageBoxButtons buttons)
        {
            return Show(title, header, message, buttons);
        }

        public static DialogResult ShowInfo(string title, string header, string message, string details, MessageBoxButtons buttons)
        {
            return Show(title, header, message, details, buttons);
        }

        public static DialogResult ShowWarning(string title, string header, string message, MessageBoxButtons buttons)
        {
            return Show(title, header, message, buttons, MessageBoxIcon.Warning);
        }

        public static DialogResult ShowWarning(string title, string header, string message, string details, MessageBoxButtons buttons)
        {
            return Show(title, header, message, details, buttons, MessageBoxIcon.Warning);
        }

        public static DialogResult ShowCritical(string title, string header, string message, MessageBoxButtons buttons)
        {
            return Show(title, header, message, buttons, MessageBoxIcon.Error);
        }

        public static DialogResult ShowCritical(string title, string header, string message, string details, MessageBoxButtons buttons)
        {
            return Show(title, header, message, details, buttons, MessageBoxIcon.Error);
        }

        public static DialogResult ShowQuestion(string title, string header, string message, MessageBoxButtons buttons)
        {
            return Show(title, header, message, buttons, MessageBoxIcon.Question);
        }

        public static DialogResult ShowQuestion(string title, string header, string message, string details, MessageBoxButtons buttons)
        {
            return Show(title, header, message, details, buttons, MessageBoxIcon.Question);
        }
    }
}
