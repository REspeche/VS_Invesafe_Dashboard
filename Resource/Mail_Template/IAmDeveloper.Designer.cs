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
    public class IAmDeveloper {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal IAmDeveloper() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resource.Mail_Template.IAmDeveloper", typeof(IAmDeveloper).Assembly);
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
        ///   Looks up a localized string similar to Name: &lt;strong&gt;{0}&lt;/strong&gt;&lt;br/&gt;
        ///Email: {1}&lt;br/&gt;
        ///Phone: {2}&lt;br/&gt;
        ///Message: {3}.
        /// </summary>
        public static string messageHtml {
            get {
                return ResourceManager.GetString("messageHtml", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Name: {0}
        ///Email: {1}
        ///Phone: {2}
        ///Message: {3}.
        /// </summary>
        public static string messageTxt {
            get {
                return ResourceManager.GetString("messageTxt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invesafe - I am Developer: {0}.
        /// </summary>
        public static string subject {
            get {
                return ResourceManager.GetString("subject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mail_Contact.htm.
        /// </summary>
        public static string template {
            get {
                return ResourceManager.GetString("template", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to consultas.desarrollador@invesafe.com.
        /// </summary>
        public static string toMail {
            get {
                return ResourceManager.GetString("toMail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invesafe.
        /// </summary>
        public static string toName {
            get {
                return ResourceManager.GetString("toName", resourceCulture);
            }
        }
    }
}