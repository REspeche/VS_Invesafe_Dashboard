﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resource.Views.Dashboard {
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
    public class RetireFunds {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal RetireFunds() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resource.Views.Dashboard.RetireFunds", typeof(RetireFunds).Assembly);
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
        ///   Looks up a localized string similar to Transfer.
        /// </summary>
        public static string btnTransfer {
            get {
                return ResourceManager.GetString("btnTransfer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bank Account.
        /// </summary>
        public static string inpAccountBank {
            get {
                return ResourceManager.GetString("inpAccountBank", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Amount.
        /// </summary>
        public static string inpAmount {
            get {
                return ResourceManager.GetString("inpAmount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Concept.
        /// </summary>
        public static string inpConcept {
            get {
                return ResourceManager.GetString("inpConcept", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Use the ,(comma) only for decimals.
        /// </summary>
        public static string lblAmount {
            get {
                return ResourceManager.GetString("lblAmount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Request Transfer.
        /// </summary>
        public static string lblTransfer {
            get {
                return ResourceManager.GetString("lblTransfer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The maximum amount you can transfer is: &lt;b&gt;$ {{dataHeader.gainamount | number: 0}}&lt;/b&gt;..
        /// </summary>
        public static string msgInfo {
            get {
                return ResourceManager.GetString("msgInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You currently do not have a charged bank account..
        /// </summary>
        public static string msgWarning {
            get {
                return ResourceManager.GetString("msgWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bank Data.
        /// </summary>
        public static string subTitle {
            get {
                return ResourceManager.GetString("subTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You can withdraw funds from your Invesafe account to your bank account provided, provided that the latter is validated in the &lt;a href=&quot;#&quot; ng-click=&quot;clickSlideAjax(&apos;bankaccounts&apos;)&quot;&gt;&quot;Your Bank Accounts&quot;&lt;/a&gt;..
        /// </summary>
        public static string text {
            get {
                return ResourceManager.GetString("text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Withdraw Funds.
        /// </summary>
        public static string title {
            get {
                return ResourceManager.GetString("title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Request Transfer.
        /// </summary>
        public static string titRequestTransfer {
            get {
                return ResourceManager.GetString("titRequestTransfer", resourceCulture);
            }
        }
    }
}