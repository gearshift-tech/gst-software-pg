﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GearShift.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.3.0.0")]
    internal sealed partial class QuickSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static QuickSettings defaultInstance = ((QuickSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new QuickSettings())));
        
        public static QuickSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool WasWhatsNewDisplayed {
            get {
                return ((bool)(this["WasWhatsNewDisplayed"]));
            }
            set {
                this["WasWhatsNewDisplayed"] = value;
            }
        }
    }
}
