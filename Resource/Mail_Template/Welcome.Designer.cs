﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resource.Mail_Template {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Welcome {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Welcome() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resource.Mail_Template.Welcome", typeof(Welcome).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to info@invesafe.com.
        /// </summary>
        public static string fromMail {
            get {
                return ResourceManager.GetString("fromMail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invesafe.
        /// </summary>
        public static string fromName {
            get {
                return ResourceManager.GetString("fromName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hi {0}...&lt;p&gt;Welcome to Invesafe. Please click on the following link to activate your account.&lt;/p&gt;&lt;p&gt;&lt;a href=&quot;{link}account/activeaccount?c={2}&quot;&gt;{link}es/account/activeaccount?c={2}&lt;/a&gt;&lt;/p&gt;.
        /// </summary>
        public static string messageHtml {
            get {
                return ResourceManager.GetString("messageHtml", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hi {0}... Welcome to Invesafe. Please click on the following link to activate your account. {link}es/account/activeaccount?c={2}.
        /// </summary>
        public static string messageTxt {
            get {
                return ResourceManager.GetString("messageTxt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Welcome to Invesafe.
        /// </summary>
        public static string subject {
            get {
                return ResourceManager.GetString("subject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mail.htm.
        /// </summary>
        public static string template {
            get {
                return ResourceManager.GetString("template", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {1}.
        /// </summary>
        public static string toMail {
            get {
                return ResourceManager.GetString("toMail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}.
        /// </summary>
        public static string toName {
            get {
                return ResourceManager.GetString("toName", resourceCulture);
            }
        }
    }
}
