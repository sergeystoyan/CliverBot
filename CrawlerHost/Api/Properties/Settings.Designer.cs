﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cliver.CrawlerHost.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    public sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\_d\\_PROJECTS\\FHR(for Andr" +
            "eas Chermak)\\FhrCliverHost#development\\_data\\CliverCrawlerHost.mdf\";Integrated S" +
            "ecurity=True;Connect Timeout=30")]
        public string CliverCrawlerHostConnectionString {
            get {
                return ((string)(this["CliverCrawlerHostConnectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("100")]
        public int ServiceCheckDurationInMss {
            get {
                return ((int)(this["ServiceCheckDurationInMss"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("crawler_manager@cliversoft.com")]
        public string DefaultAdminEmails {
            get {
                return ((string)(this["DefaultAdminEmails"]));
            }
            set {
                this["DefaultAdminEmails"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("crawler_manager@cliversoft.com")]
        public string EmailSender {
            get {
                return ((string)(this["EmailSender"]));
            }
            set {
                this["EmailSender"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string SmtpHost {
            get {
                return ((string)(this["SmtpHost"]));
            }
            set {
                this["SmtpHost"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("25")]
        public int SmtpPort {
            get {
                return ((int)(this["SmtpPort"]));
            }
            set {
                this["SmtpPort"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("crawler_manager@cliversoft.com")]
        public string SmtpLogin {
            get {
                return ((string)(this["SmtpLogin"]));
            }
            set {
                this["SmtpLogin"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("123456")]
        public string SmtpPassword {
            get {
                return ((string)(this["SmtpPassword"]));
            }
            set {
                this["SmtpPassword"] = value;
            }
        }
    }
}